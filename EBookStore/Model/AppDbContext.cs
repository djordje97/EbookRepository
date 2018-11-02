using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Model
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Ebook> Ebooks { get; set; }

        public AppDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>();
           
            modelBuilder.Entity<Category>().HasData(
                new Category() { CategoryId=1,Name="Comedy"});
            modelBuilder.Entity<User>().HasData(
                new User() {UserId=1,Firstname="Marko",Lastname="Markovic",Username="marko",Password=hasher.HashPassword(null,"123"),Type="Admin",CategoryId=1 },
                new User() { UserId = 2, Firstname = "Darko", Lastname = "Stankic", Username = "darko", Password = hasher.HashPassword(null, "123"), Type = "User", CategoryId = 1 }
                );
           
            modelBuilder.Entity<Language>().HasData(new Language() { LanguageId = 1, Name = "Serbian" });
        }
    }
}
