using Microsoft.EntityFrameworkCore;
using MedBook_RazorPages.Models;

namespace MedBook_RazorPages.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }
        
        public DbSet<Users> Users { get; set; }
        
        public DbSet<MedBook_RazorPages.Models.MedicalService> MedicalService { get; set; }
        
        public DbSet<MedBook_RazorPages.Models.Review> Review { get; set; }
    }
}
