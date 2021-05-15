using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimCap.Model
{
    public class CapsuleDig
    {
        [ForeignKey("CapsuleId")]
        public Capsule Capsule { get; set; }
        [Key,Column(Order = 1)]
        public string UserDig { get; set; }
        [Key,Column(Order = 2)]
        public int CapsuleId { get; set; }
        public DateTime DigTime { get; set; }

        public CapsuleDig() { }
        public CapsuleDig(string userId, int capId)
        {
            UserDig = userId;
            CapsuleId = capId;
            DigTime = DateTime.Now;
        }
    }
}
