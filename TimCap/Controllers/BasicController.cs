using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("timecap/add")]
        public ApiRes AddItem([Required] string UserId, [Required]string Story, [Required]string Address, [Required]string session)
        {
            _context.Caps.Add(new Cap
            {
                Story = Story,
                InTime = DateTime.Now,
                Address = Address,
                UserId = UserId
            });
            _context.SaveChanges();
            return new ApiRes(ApiCode.Success, "how are you", Story);
        }

        [HttpGet("timecap/remove")]
        public ApiRes Remove([Required] string UserId,[Required] int CapId,[Required] string session)
        {
            var cap = _context.Caps.Find(CapId);
            return new ApiRes(ApiCode.Success, "", cap);
        }


        [HttpPost("timecap/query")]
        public ApiRes Query([Required] string UserId, [Required] string session)
        {
            return new ApiRes(ApiCode.Success, "", null);
        }

        [HttpPost("timecap/dig")]
        public ApiRes Dig([Required] string UserId, [Required] string address, [Required] string session)
        {
            return new ApiRes(ApiCode.Success, "", null);
        }
    }
}
