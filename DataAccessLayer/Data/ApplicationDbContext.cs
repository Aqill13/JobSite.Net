using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Work> Works { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Candidate - Field əlaqəsi
            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Field)
                .WithMany(f => f.Candidates)
                .HasForeignKey(c => c.FieldId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Work - Category əlaqəsi
            modelBuilder.Entity<Work>()
                .HasOne(w => w.Category)
                .WithMany(c => c.Works)
                .HasForeignKey(w => w.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Work - Field əlaqəsi
            modelBuilder.Entity<Work>()
                .HasOne(w => w.Field)
                .WithMany(f => f.Works)
                .HasForeignKey(w => w.FieldId)
                .OnDelete(DeleteBehavior.Restrict); 

            // City - Work əlaqəsi
            modelBuilder.Entity<Work>()
                .HasOne(w => w.City)
                .WithMany(c => c.Works)
                .HasForeignKey(w => w.CityId)
                .OnDelete(DeleteBehavior.Restrict); 
        }

    }

}
