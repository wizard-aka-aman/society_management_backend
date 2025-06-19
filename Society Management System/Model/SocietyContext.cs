using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; 

namespace Society_Management_System.Model
{
    public class SocietyContext : IdentityDbContext<IdentityUser>
    {

        public SocietyContext(DbContextOptions options) : base(options)
        {
 
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        } 

        public DbSet<Users> Users { get; set; }
        public DbSet<Flats> Flats { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<Complaints> Complaints { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Notices> Notices { get; set; }
        public DbSet<Visitors> Visitors { get; set; }
        public DbSet<Society> Society { get; set; }
        public DbSet<Alarms> Alarms { get; set; }
        public DbSet<Recurring> Recurring { get; set; }

    }
}
