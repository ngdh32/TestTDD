using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTddInfra.Entities.EF;

namespace TestTddInfra
{
    public class BookDbContext : DbContext
    {
        public DbSet<EfBook> Books { get; set; }
        public DbSet<EfAuthor> Authors { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EfBook>()
                .HasOne(p => p.Author)
                .WithMany(b => b.Books);
        }
    }
}
