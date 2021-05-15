using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimCap.Model
{
    public class Cap
    {
        public int Id { get; set; }
        public string Story { get; set; }
        public DateTime Time { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }

    }
}