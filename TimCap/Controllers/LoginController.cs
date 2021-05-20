using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using TimCap.Model;
using TimCap.Services;
using TimCap.Tools;

namespace TimCap.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {

        private readonly CookieOptions _cookieOption;
        private readonly MemoryCacheEntryOptions _cacheOption;
        private readonly LoginService _loginService;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        public LoginController(IConfiguration configuration, LoginService loginService, ILogger<LoginController> logger, IMemoryCache cache)
        {
            var ExpireMin = configuration.GetValue<int>("CookieExpiration");
            _cookieOption = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(ExpireMin)
            };
            _cacheOption = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(ExpireMin)
            };
            _loginService = loginService;
            _logger = logger;
            _cache = cache;
        }

        /// <summary>
        /// 华师登录
        /// </summary>
        /// <param name="userid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost("ccnu")]
        public async Task<ApiResponse> LoginCcnu([Required] string userid, [Required] string pwd)
        {
            if(!Regex.IsMatch(userid, "^\\d+$"))
            {
                return new ApiResponse(ApiCode.Error, "账号格式错误 账号应只包含数字", null);
            }

            var ccnuUser = new User
            {
                Sno = userid,
                Password = pwd
            };
            var apiRes = await _loginService.LoginThrougthCcnu(ccnuUser);
            if (apiRes.Code == ApiCode.Error)
            {
                return apiRes;
            }
            string sessionId = EncrypTool.Sha256(userid) + EncrypTool.GenerateFakeFinger();
            Response.Cookies.Append("SESSIONID", sessionId, _cookieOption);
            _cache.Set(sessionId, userid, _cacheOption);
            return new ApiResponse(ApiCode.Success, "登录成功", null);
        }

        /// <summary>
        /// 武理登录
        /// </summary>
        /// <returns></returns>
        [HttpGet("wut")]
        public RedirectResult LoginWut()
        {
            _logger.LogInformation("login wut");
            return Redirect(
                "http://ias.sso.itoken.team/portal.php?posturl=https://api.saicem.top/login/wut/callback");
        }

        /// <summary>
        /// 武理登录回调接口
        /// </summary>
        /// <param name="continueurl"></param>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("wut/callback")]
        public ApiResponse LoginCallBack([FromForm] string continueurl, [FromForm] string user, [FromForm] string token)
        {
            var jsonUser = JsonSerializer.Deserialize<WhutUserInfo>(HttpUtility.UrlDecode(user));
            var sno = jsonUser.Sno;
            string sessionId = EncrypTool.Sha256(sno) + EncrypTool.GenerateFakeFinger();
            Response.Cookies.Append("SESSIONID", sessionId, _cookieOption);
            _cache.Set(sessionId, sno, _cacheOption);
            return new ApiResponse(ApiCode.Success, "登录成功", null);
        }
    }
}
