using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TimCap.DAO;
using TimCap.Model;

namespace TimCap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly TimeCapContext _context;
        public BasicController(TimeCapContext context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public string Func()
        {
            return "ok";
        }

        /// <summary>
        /// 添加一个胶囊
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="Story">故事</param>
        /// <param name="Address">地点</param>
        /// <param name="session">鉴权</param>
        /// <returns></returns>
        [HttpPost("timecap/add")]
        public ApiRes AddItem([Required] string UserId, [Required]string Story, [Required]string Address, [Required]string session)
        {
            _context.Caps.Add(new CapOwn
            {
                Story = Story,
                InTime = DateTime.Now,
                Address = Address,
                UserId = UserId
            });
            _context.SaveChanges();
            return new ApiRes(ApiCode.Success, "添加胶囊成功", Story);
        }

        /// <summary>
        /// 删除用户的胶囊
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="CapId">胶囊Id</param>
        /// <param name="session">鉴权</param>
        /// <returns></returns>
        [HttpGet("timecap/remove")]
        public ApiRes Remove([Required] string UserId,[Required] int CapId,[Required] string session)
        {
            var cap = _context.CapOwns.Find(CapId);
            if (cap == null)
            {
                return new ApiRes(ApiCode.Error, "不存在此胶囊", null);
            }
            if (cap.UserId == UserId)
            {
                _context.Remove(cap);
                _context.SaveChanges();
                return new ApiRes(ApiCode.Success, "胶囊删除成功", null);
            }
            return new ApiRes(ApiCode.Error, "用户错误", null);
        }

        /// <summary>
        /// 查询用户拥有的胶囊
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="session">鉴权</param>
        /// <returns></returns>
        [HttpPost("timecap/query/own")]
        public ApiRes CapsQueryOwn([Required] string UserId, [Required] string session)
        {
            var caps = (from item in _context.CapOwns
                        where item.UserId == UserId
                        select item).AsNoTracking();
            return new ApiRes(ApiCode.Success, "查询成功", caps);
        }

        /// <summary>
        /// 查询用户挖到的胶囊
        /// </summary>
        [HttpPost("timecap/query/dig")]
        public ApiRes CapsQueryDig([Required] string UserId, [Required] string session)
        {
            var caps = (from item in _context.CapDigs
                        where item.CapDigId == UserId
                        select item)
            return new ApiRes(ApiCode.Success, "查询成功", caps)
        }

        [HttpPost("timecap/dig")]
        public ApiRes Dig([Required] string UserId, [Required] string address, [Required] string session)
        {
            return new ApiRes(ApiCode.Success, "", null);
        }
    }
}
