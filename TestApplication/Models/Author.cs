using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApplication.Models
{
    [Table("Authors")]
    [Index(nameof(Name))]
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("authorName", TypeName = "varchar(300)")]
        public string Name { get; set; } = string.Empty;

        // Optional: navigation property for related books
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
