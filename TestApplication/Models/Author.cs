using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApplication.Models
{
    [Table("Authors")]
    [Index(nameof(Name), IsUnique = true)]
    public class Author
    {
        public Author()
        {
        }

        public Author(string name)
        {
            this.Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("authorName", TypeName = "varchar(300)")]
        public string Name { get; set; } = string.Empty;

        // Optional: navigation property for related books
        protected internal ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
