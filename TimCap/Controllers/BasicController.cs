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
        public ApiRes AddItem([Required]string Story, [Required]string Address, string UserId)
        {
            _context.Caps.Add(new Cap
            {
                Story = Story,
                InTime = DateTime.Now,
                Address = Address,
                UserId = UserId
            });
            return new ApiRes(ApiCode.Success, "how are you", Story);
        }
    }
}
