using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimCap.Model
{
    public class Capsule
    {
        [Key]
        [ForeignKey("OutId")]
        public int CapsuleId { get; set; }
        public string Story { get; set; }
        public DateTime InTime { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }

        public Capsule() { }
        public Capsule(string userId, string address, string story)
        {
            UserId = userId;
            Address = address;
            Story = story;
            InTime = DateTime.Now;
        }
    }
}