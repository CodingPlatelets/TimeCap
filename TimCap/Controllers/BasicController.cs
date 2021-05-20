using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimCap.Controllers
{
    public class BasicController : Controller
    {
        [HttpGet("test")]
        public string Func()
        {
            return "ok";
        }
    }
}
