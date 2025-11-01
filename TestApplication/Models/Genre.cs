using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestApplication.Models
{
    [Table("Genre")]
    [Index(nameof(GenreName))]
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        [Required]
        [Column("GenreName", TypeName = "varchar(100)")]
        public required string GenreName { get; set; }

        protected internal ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
