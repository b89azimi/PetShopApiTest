using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PetShopProject.Test.Models
{
    public class Pet
    {
        public long Id { get; set; }
        public Category Category { get; set; }
        public long CategoryId { get; set; } // Foreign key for Category
        public string Name { get; set; }
        public List<PhotoUrl> PhotoUrls { get; set; } = new List<PhotoUrl>(); // Navigation property for photo URLs
        public List<PetTag> PetTags { get; set; } = new List<PetTag>(); // Navigation property for tags
        public string Status { get; set; } // Values: "available", "pending", "sold"
    }
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options) { }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; } 
        public DbSet<PhotoUrl> PhotoUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // تنظیم رابطه چند-به-چند بین Pet و Tag
            modelBuilder.Entity<PetTag>()
                .HasKey(pt => new { pt.PetId, pt.TagId });

            modelBuilder.Entity<PetTag>()
                .HasOne(pt => pt.Pet)
                .WithMany(p => p.PetTags)
                .HasForeignKey(pt => pt.PetId);

            modelBuilder.Entity<PetTag>()
                .HasOne(pt => pt.Tag)
                .WithMany()
                .HasForeignKey(pt => pt.TagId);
        }
    }
}
