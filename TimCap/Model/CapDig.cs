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
        [Key]
        public int CapDigId { get; set; }
        [ForeignKey("CapOwnId")]
        public Caps Cap { get; set; }
        public string UserDig { get; set; }
        public int CapId { get; set; }

        public DateTime OutTime { get; set; }

    }
}
