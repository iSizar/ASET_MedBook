using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedBook_RazorPages.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string password { get; set; }
    
        public string email { get; set; }

        public int accountType { get; set; }
    }
}
