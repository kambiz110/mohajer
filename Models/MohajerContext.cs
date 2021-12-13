using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Entity
{
    public class MohajerContext : DbContext
    {

        public MohajerContext(DbContextOptions<MohajerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>().HasNoKey();

            modelBuilder.Entity<Comment>()
            .Property(b => b.AtInserted).HasColumnType("date");

            modelBuilder.Entity<Category>(category =>
            {
                category.HasMany(c => c.Children)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId);
            });

            modelBuilder.Entity<Category>()
                 .HasMany(c => c.Prodocts)
                 .WithOne(e => e.Category);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Inserted = DateTime.Now,
                    Updated = DateTime.Now,
                    IsActive = true,
                    Email = "kampergig@gmail.com",
                    First_Name = "Kambiz",
                    Last_Name = "zare",
                    Password = "123",
                    Phone = "09108496094",
                    Role = "Admin",
                    Image = @"/UploudProfile/avatar.png"
                }
            );
            modelBuilder.Entity<Category>().HasData(
              new Category
              {
                  Id = 1,
                  Title = "مذهبی",
                  ParentCategoryId = null
              });

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Prodoct> Prodoctes { get; set; }
        public DbSet<Category> Categores { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Nazarat> Nazarats { get; set; }
        public DbSet<Tage> Tages { get; set; }
        public DbSet<Order> Orders { get; set; }
  

    }
}