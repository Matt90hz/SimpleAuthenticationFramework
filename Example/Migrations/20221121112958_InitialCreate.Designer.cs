﻿// <auto-generated />
using Example.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Example.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221121112958_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("Authentication.Models.Subscription", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleKey")
                        .HasColumnType("TEXT");

                    b.HasKey("UserName", "RoleKey");

                    b.HasIndex("RoleKey");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Example.Models.Role", b =>
                {
                    b.Property<string>("RoleKey")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("RoleKey");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleKey = "ADMIN",
                            Description = "User with the highest privileges."
                        },
                        new
                        {
                            RoleKey = "USER",
                            Description = "User with read/write privileges."
                        },
                        new
                        {
                            RoleKey = "GUEST",
                            Description = "User with only read privileges"
                        });
                });

            modelBuilder.Entity("Example.Models.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Authentication.Models.Subscription", b =>
                {
                    b.HasOne("Example.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Example.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
