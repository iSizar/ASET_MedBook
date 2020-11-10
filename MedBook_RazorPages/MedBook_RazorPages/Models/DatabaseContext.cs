using Microsoft.EntityFrameworkCore;
using MedBook_RazorPages.Models;

namespace MedBook_RazorPages.Models
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }
        
        public DbSet<Users> Users { get; set; }
        
        public DbSet<MedicalService> MedicalService { get; set; }
        
        public DbSet<Review> Review { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
    }
}
