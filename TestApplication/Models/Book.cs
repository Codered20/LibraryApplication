using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestApplication.Models
{
    [Table("Books")]
    [Index(nameof(Title))]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(150)]
        [Column("Book_Title", TypeName = "varchar(150)")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "date")]
        public DateOnly Published { get; set; }

        [Required]
        public DateOnly EntryDate { get; set; }

        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        public Author Author { get; set; } = null!;
        public Genre Genre { get; set; } = null!;

        [Column("Availability")]
        public int Available { get; set; }

        [NotMapped]
        public string DisplayInfo => $"{Title} ({Published.Year})";
    }
}
