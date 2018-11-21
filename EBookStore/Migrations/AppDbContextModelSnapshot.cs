﻿// <auto-generated />
using EBookStore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EBookStore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EBookStore.Model.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new { CategoryId = 1, Name = "None" },
                        new { CategoryId = 2, Name = "Comedy" }
                    );
                });

            modelBuilder.Entity("EBookStore.Model.Ebook", b =>
                {
                    b.Property<int>("EbookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasMaxLength(120);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Keywords")
                        .HasMaxLength(120);

                    b.Property<int>("LanguageId");

                    b.Property<string>("MIME")
                        .HasMaxLength(100);

                    b.Property<int>("PublicationYear");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<int>("UserId");

                    b.HasKey("EbookId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("UserId");

                    b.ToTable("Ebooks");
                });

            modelBuilder.Entity("EBookStore.Model.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");

                    b.HasData(
                        new { LanguageId = 1, Name = "Serbian" }
                    );
                });

            modelBuilder.Entity("EBookStore.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("UserId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Users");

                    b.HasData(
                        new { UserId = 1, CategoryId = 1, Firstname = "Marko", Lastname = "Markovic", Password = "AQAAAAEAACcQAAAAEH07BVTixP4CRndI3496bymdZWTYRBn07B8XqtW4gKWS4Wk0sZzgycd9IV6YYvldhw==", Type = "Admin", Username = "marko" },
                        new { UserId = 2, CategoryId = 1, Firstname = "Darko", Lastname = "Stankic", Password = "AQAAAAEAACcQAAAAED+taTt3FKCn9IBWlyjovonTgACz+FE+AbSy3JaDqyhAQytvABSPa/Z4SeFDeYekHg==", Type = "User", Username = "darko" }
                    );
                });

            modelBuilder.Entity("EBookStore.Model.Ebook", b =>
                {
                    b.HasOne("EBookStore.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EBookStore.Model.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EBookStore.Model.User", "Use")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EBookStore.Model.User", b =>
                {
                    b.HasOne("EBookStore.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
