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
using Microsoft.Extensions.Configuration;

namespace TimCap.Controllers
{
    [Route("timecap")]
    [ApiController]
    public class TimeCapController : ControllerBase
    {
        private readonly TimeCapContext _context;
        private readonly IMemoryCache _cache;

        public TimeCapController(TimeCapContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        /// <summary>
        /// 添加一个胶囊
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public ApiResponse AddItem([Required] CapsuleReceive capsuleReceive)
        {
            if(!IsValidUser(out string userId))
            {
                return new ApiResponse(ApiCode.Error, "用户未登录", null);
            }

            _context.Capsules.Add(new Capsule(capsuleReceive, userId));
            _context.SaveChanges();
            return new ApiResponse(ApiCode.Success, "添加胶囊成功", null);
        }

        

        /// <summary>
        /// 删除用户的胶囊
        /// </summary>
        /// <param name="capid">胶囊Id</param>
        /// <returns></returns>
        [HttpDelete("remove")]
        public ApiResponse Remove([Required] int capid)
        {
            if (!IsValidUser(out string userId))
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
        /// 查询用户埋的胶囊
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/own")]
        public ApiResponse CapsQueryOwn()
        {
            if (!IsValidUser(out string userId))
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
            if (!IsValidUser(out string userId))
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
        /// 挖时间胶囊
        /// </summary>
        /// <param name="address">挖掘地点</param>
        /// <returns></returns>
        [HttpPost("dig")]
        public ApiResponse Dig([Required] string address)
        {
            if (!IsValidUser(out string userId))
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

        private bool IsValidUser(out string userId)
        {
            if (!Request.Cookies.TryGetValue("SESSIONID", out string sessionId) || !_cache.TryGetValue(sessionId, out userId))
            {
                userId = null;
                return false;
            }
            return true;
        }
    }
}