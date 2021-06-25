using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore_redis_cache.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Book>().HasKey(b => b.BookId);
            builder.Entity<Book>().HasOne(b => b.Publisher).WithMany(p => p.Books).HasForeignKey(b => b.PublisherId);
            builder.Entity<Publisher>().HasKey(p => p.PublisherId);
        }
    }
}
