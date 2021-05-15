using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimCap.Model
{
    public class CapDig
    {
        [ForeignKey("CapOwnId")]
        public Caps Cap { get; set; }
        [Key,Column(Order = 1)]
        public string UserDig { get; set; }
        [Key,Column(Order = 2)]
        public int CapId { get; set; }

        public DateTime OutTime { get; set; }

    }
}
