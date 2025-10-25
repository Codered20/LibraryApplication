using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApplication.Models
{
    [Table("Genre")]
    [Index(nameof(GenreName))]
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("GenreName", TypeName = "varchar(100)")]
        public string GenreName { get; set; }

        ICollection<Book> books { get; set; } = new List<Book>();

    }
}
