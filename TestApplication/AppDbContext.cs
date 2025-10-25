using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestApplication.Models;

namespace TestApplication.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor that accepts options
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets represent tables
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
