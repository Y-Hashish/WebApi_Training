using Microsoft.EntityFrameworkCore;

namespace WebAPI_Training.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Department> departments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        { }
    }
}
