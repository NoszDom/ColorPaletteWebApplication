﻿// <auto-generated />
using System;
using ColorPaletteApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ColorPaletteApp.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211119101226_NavigationProperties3")]
    partial class NavigationProperties3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ColorPaletteApp.Domain.Models.ColorPalette", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Colors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorID");

                    b.HasIndex("UserId");

                    b.ToTable("ColorPalette");
                });

            modelBuilder.Entity("ColorPaletteApp.Domain.Models.Save", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ColorPaletteID")
                        .HasColumnType("int");

                    b.Property<int?>("ColorPaletteId")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColorPaletteID");

                    b.HasIndex("ColorPaletteId");

                    b.HasIndex("UserID");

                    b.HasIndex("UserId");

                    b.ToTable("Save");
                });

            modelBuilder.Entity("ColorPaletteApp.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ColorPaletteApp.Domain.Models.ColorPalette", b =>
                {
                    b.HasOne("ColorPaletteApp.Domain.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ColorPaletteApp.Domain.Models.User", null)
                        .WithMany("CreatedPalettes")
                        .HasForeignKey("UserId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("ColorPaletteApp.Domain.Models.Save", b =>
                {
                    b.HasOne("ColorPaletteApp.Domain.Models.ColorPalette", "ColorPalette")
                        .WithMany()
                        .HasForeignKey("ColorPaletteID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ColorPaletteApp.Domain.Models.ColorPalette", null)
                        .WithMany("Saves")
                        .HasForeignKey("ColorPaletteId");

                    b.HasOne("ColorPaletteApp.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ColorPaletteApp.Domain.Models.User", null)
                        .WithMany("Saves")
                        .HasForeignKey("UserId");

                    b.Navigation("ColorPalette");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ColorPaletteApp.Domain.Models.ColorPalette", b =>
                {
                    b.Navigation("Saves");
                });

            modelBuilder.Entity("ColorPaletteApp.Domain.Models.User", b =>
                {
                    b.Navigation("CreatedPalettes");

                    b.Navigation("Saves");
                });
#pragma warning restore 612, 618
        }
    }
}