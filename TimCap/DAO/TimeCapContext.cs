using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimCap.Model;

namespace TimCap.DAO
{
    public class TimeCapContext : DbContext
    {
        public DbSet<Capsule> Capsules { get; set; }
        public DbSet <CapsuleDig> CapsuleDigs { get; set; }

        public TimeCapContext(DbContextOptions<TimeCapContext> options): base(options)
        {
        }

        public TimeCapContext()
        {
        }

        //初始化数据库
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CapsuleDig>().HasKey(c => new { c.CapsuleId, c.UserDig });
        }
    }
}
