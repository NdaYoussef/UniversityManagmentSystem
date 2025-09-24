
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniManagementSystem.Domain.Models;
namespace UniManagementSystem.Infrastructure.DBContext
{
    public class UniSystemContext : IdentityDbContext<ApplicationUser>
    {
        public UniSystemContext(DbContextOptions<UniSystemContext> options)
               : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
