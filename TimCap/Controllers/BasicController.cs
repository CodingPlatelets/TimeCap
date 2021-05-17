using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Caching.Memory;
using TimCap.DAO;
using TimCap.Model;
using TimCap.Services;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using TimCap.Tools;

namespace TimCap.Controllers
{
    [Route("api/timecap")]
    [ApiController]
    public class BasicController : ControllerBase
    {

        private readonly TimeCapContext _context;
        private readonly IMemoryCache _cache;
        // private Session _session;
        private readonly CookieOptions _cookieOption;
        private readonly MemoryCacheEntryOptions _cacheOption;
        private readonly LoginService _service;
        private readonly ILogger _logger;

        public BasicController(TimeCapContext context, /*DistributedSession session,*/ LoginService service, ILogger<BasicController> logger, IMemoryCache cache)
        {
            _context = context;
            // _session = session;
            _cookieOption = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(10)
            };
            _cacheOption = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
            _service = service;
            _cache = cache;
            _logger = logger;
        }
        

        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet("test")]
        public string Func()
        {
            return "ok";
        }

        /// <summary>
        /// 华师登录
        /// </summary>
        /// <param name="userid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost("loginccnu")]
        public ApiResponse LoginCcnu([Required] string userid,[Required] string pwd)
        {
            _logger.LogInformation("login ccnu");
            string session = EncrypTool.Sha256(userid) + EncrypTool.GenerateFakeFinger();
            var ccnuUser = new User
            {
                sno = userid,
                password = pwd
            };
            var apiRes = _service.LoginThrougthCcnu(ccnuUser).Result;
            if (apiRes.Code == ApiCode.Error)
            {
                return apiRes;
            }

            Response.Cookies.Append("session", session, _cookieOption);
            _cache.Set(session, userid, _cacheOption);
            return new ApiResponse(ApiCode.Success, "登录成功", null);
        }

        /// <summary>
        /// 武理登录
        /// </summary>
        /// <returns></returns>
        [HttpGet("loginwut")]
        public RedirectResult LoginWut()
        {
            _logger.LogInformation("login wut");
            return Redirect(
                "http://ias.sso.itoken.team/portal.php?posturl=http://saicem.top:5905/api/timecap/callback");
        }

        /// <summary>
        /// 武理登录回调接口
        /// </summary>
        /// <param name="continueurl"></param>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("callback")]
        public ApiResponse LoginCallBack([FromForm]string continueurl, [FromForm] string user,[FromForm] string token)
        {
            var jsonUser = JsonSerializer.Deserialize<WhutUserInfo>(HttpUtility.UrlDecode(user));
            var sno = jsonUser.Sno;
            string session = EncrypTool.Sha256(sno) + EncrypTool.GenerateFakeFinger();
            Response.Cookies.Append("session", session, _cookieOption);
            _cache.Set(session, sno, _cacheOption);
            return new ApiResponse(ApiCode.Success, "登录成功", null);
        }

        /// <summary>
        /// 添加一个胶囊
        /// </summary>
        /// <param name="story">故事</param>
        /// <param name="address">地点</param>
        /// <returns></returns>
        [HttpPost("add")]
        public ApiResponse AddItem([Required] string address, [Required] string story)
        {
            _logger.LogInformation($"add {address} {story}");
            if (!Request.Cookies.TryGetValue("session", out string session) || !_cache.TryGetValue(session, out string userId))
            {
                return new ApiResponse(ApiCode.Error, "用户未登录", null);
            }

            _context.Capsules.Add(new Capsule(userId, address, story));
            _context.SaveChanges();
            return new ApiResponse(ApiCode.Success, "添加胶囊成功", story);
        }

        /// <summary>
        /// 删除用户的胶囊
        /// </summary>
        /// <param name="capid">胶囊Id</param>
        /// <returns></returns>
        [HttpDelete("remove")]
        public ApiResponse Remove([Required] int capid)
        {
            _logger.LogInformation($"remove {capid}");
            if (!Request.Cookies.TryGetValue("session", out string session) || !_cache.TryGetValue(session, out string userId))
            {
                return new ApiResponse(ApiCode.Error, "用户未登录", null);
            }

            var cap = _context.Capsules.Find(capid);
            if (cap == null)
            {
                return new ApiResponse(ApiCode.Error, "不存在此胶囊", null);
            }

            if (cap.UserId == userId)
            {
                _context.Remove(cap);
                _context.SaveChanges();
                return new ApiResponse(ApiCode.Success, "胶囊删除成功", null);
            }

            return new ApiResponse(ApiCode.Error, "没有权限", null);
        }

        /// <summary>
        /// 查询用户拥有的胶囊
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/own")]
        public ApiResponse CapsQueryOwn()
        {
            if (!Request.Cookies.TryGetValue("session", out string session) || !_cache.TryGetValue(session, out string userId))
            {
                return new ApiResponse(ApiCode.Error, "用户未登录", null);
            }

            var caps = (from item in _context.Capsules
                where item.UserId == userId
                select item).AsNoTracking();
            return new ApiResponse(ApiCode.Success, "查询成功", caps);
        }

        /// <summary>
        /// 查询用户挖到的胶囊
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/dig")]
        public ApiResponse CapsQueryDig()
        {
            if (!Request.Cookies.TryGetValue("session", out string session) || !_cache.TryGetValue(session, out string userId))
            {
                return new ApiResponse(ApiCode.Error, "用户未登录", null);
            }

            var caps = (from c in _context.Capsules
                       where (from item in _context.CapsuleDigs
                              where item.UserDig == userId
                              select item.CapsuleId).Contains(c.CapsuleId)
                              select c).AsNoTracking();
            return new ApiResponse(ApiCode.Success, "查询成功", caps);
        }

        /// <summary>
        /// 挖时光胶囊
        /// </summary>
        /// <param name="address">挖掘地点</param>
        /// <returns></returns>
        [HttpPost("dig")]
        public ApiResponse Dig([Required] string address)
        {
            if (!Request.Cookies.TryGetValue("session", out string session) || !_cache.TryGetValue(session, out string userId))
            {
                return new ApiResponse(ApiCode.Error, "用户未登录", null);
            }

            var capIds = (from c in _context.Capsules 
                         where c.Address == address && !(from item in _context.CapsuleDigs
                                                         where item.UserDig == userId
                                                         select item.CapsuleId).Contains(c.CapsuleId)
                        select c.CapsuleId).ToList();
            if (!capIds.Any())
            {
                return new ApiResponse(ApiCode.Success, "该地区没有瓶子了", -1);
            }
            var rand = new Random();
            var capId = capIds[rand.Next(capIds.Count)];
            var cap = _context.Capsules.Find(capId);
            _context.CapsuleDigs.Add(new CapsuleDig(userId, capId));
            _context.SaveChanges();
            return new ApiResponse(ApiCode.Success, "成功挖到了", cap);
        }
    }
}