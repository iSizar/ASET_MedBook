using Microsoft.EntityFrameworkCore;

namespace MedBook_RazorPages.Models
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }
        
        public DbSet<Users> Users { get; set; }
    }
}
