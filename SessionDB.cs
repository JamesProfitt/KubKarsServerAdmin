using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KubKarsServerAdmin
{
    public class SessionContext : DbContext
    {
        public required string ConnectionString { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }

    public class Session
    {
        [Key]
        public string Sid { get; set; }

        public string Expired { get; set; }
        public string Sess { get; set; }

    }
}
