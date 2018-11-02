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
            modelBuilder.Entity<Category>().HasData(
                new Category() { CategoryId=1,Name="Comedy"});
            modelBuilder.Entity<User>().HasData(
                new User() {UserId=1,Firstname="Marko",Lastname="Markovic",Username="marko",Password="123",Type="Admin",CategoryId=1 });
           
            modelBuilder.Entity<Language>().HasData(new Language() { LanguageId = 1, Name = "Serbian" });
        }
    }
}
