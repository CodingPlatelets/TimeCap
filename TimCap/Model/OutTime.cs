using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimCap.Model
{
    public class OutTime
    {
        [Key]
        public int OutId { get; set; }

        public DateTime OTime { get; set; }

    }
}
