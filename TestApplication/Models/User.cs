using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TestApplication.Models
{
    public class User
    {
        public User(string name, string email, string password) { 
            Name = name;
            Email = email;
            Password = password;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [NotNull]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address format.")]
        [NotNull]
        public string Email { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 200 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        [NotNull]
        public string Password { get; set; }

        public string Role {  get; set; }
        
    }
}
