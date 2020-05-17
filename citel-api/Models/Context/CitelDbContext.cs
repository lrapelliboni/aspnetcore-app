using System;
using citelapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace citelapi.Models.Context
{
    public partial class CitelDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public CitelDbContext()
        {
        }

        public CitelDbContext(DbContextOptions<CitelDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;User Id=root;Password=rootpass;Database=citel");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(b => b.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
