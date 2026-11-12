using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KubKarsServerAdmin
{

    public class UserContext : DbContext
    {
        public required string ConnectionString { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Hashed_password { get; set; }
        public string Salt { get; set; }
    
    }
}
