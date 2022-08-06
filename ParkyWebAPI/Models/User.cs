using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkyWebAPI.Models
{
    public class User
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
        public string? role { get; set; }
        [NotMapped]
        public string? token { get; set; }

    }
}
