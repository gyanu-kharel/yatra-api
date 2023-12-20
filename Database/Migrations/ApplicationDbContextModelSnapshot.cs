﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using YatraBackend.Database;

#nullable disable

namespace YatraBackend.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("YatraBackend.Database.Models.Domain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Domains");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a8f34d64-1816-4b81-90cf-36a3219d95ed"),
                            IsActive = true,
                            Name = "Health"
                        },
                        new
                        {
                            Id = new Guid("bfdce290-a7f7-4273-a234-f732e2c02a3f"),
                            IsActive = true,
                            Name = "Education"
                        },
                        new
                        {
                            Id = new Guid("8473dc88-ec14-4286-b65f-8ffcd88f1edb"),
                            IsActive = true,
                            Name = "Tourism"
                        },
                        new
                        {
                            Id = new Guid("226baf74-100f-4b9f-b35f-f02d8e73c4f3"),
                            IsActive = true,
                            Name = "Transport"
                        },
                        new
                        {
                            Id = new Guid("441eb000-ab2b-4c91-a398-35445f094428"),
                            IsActive = true,
                            Name = "Finance"
                        },
                        new
                        {
                            Id = new Guid("d4c548c3-e6de-4014-b44f-c599a48cc49a"),
                            IsActive = true,
                            Name = "Agriculture"
                        },
                        new
                        {
                            Id = new Guid("d16268bb-897d-48a3-937a-e48d1439a872"),
                            IsActive = true,
                            Name = "Fashion"
                        },
                        new
                        {
                            Id = new Guid("881ac600-7b21-4b32-bbd1-8b70b6854384"),
                            IsActive = true,
                            Name = "Social Media"
                        },
                        new
                        {
                            Id = new Guid("8a0e1887-9764-4258-89a2-1ff35f98d4a2"),
                            IsActive = true,
                            Name = "E-commerce"
                        });
                });

            modelBuilder.Entity("YatraBackend.Database.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2b019de2-ff60-4830-8f10-b3a78c313308"),
                            Description = "Administrative roles and permissions",
                            IsActive = true,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("ec8c2dc7-2302-4619-8ad7-1b320b7f3575"),
                            Description = "Basic user roles and permissions",
                            IsActive = true,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("YatraBackend.Database.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("YatraBackend.Database.Models.User", b =>
                {
                    b.HasOne("YatraBackend.Database.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
