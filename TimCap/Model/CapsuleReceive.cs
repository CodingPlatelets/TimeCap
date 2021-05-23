using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimCap.Model
{
    public class CapsuleReceive
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 故事
        /// </summary>
        [MaxLength(800)]
        public string Story { get; set; }
    }
}
