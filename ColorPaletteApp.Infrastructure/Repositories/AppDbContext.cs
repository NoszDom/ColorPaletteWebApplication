using ColorPaletteApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Infrastructure.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ColorPalette> ColorPalettes { get; set; }
        public DbSet<Save> Saves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .ToTable("User")
               .HasMany(e => e.Saves)
               .WithOne(p => p.User)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
             .ToTable("User")
             .HasMany(e => e.CreatedPalettes)
             .WithOne(p => p.Creator)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ColorPalette>()
                .ToTable("ColorPalette")
                .HasOne(e => e.Creator)
                .WithMany(p => p.CreatedPalettes)
                .HasForeignKey(e=> e.CreatorID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ColorPalette>()
               .ToTable("ColorPalette")
               .HasMany(e => e.Saves)
               .WithOne(p => p.ColorPalette)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Save>()
                .ToTable("Save")
                .HasOne(e => e.User)
                .WithMany(p => p.Saves)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Save>()
                .ToTable("Save")
                .HasOne(e => e.ColorPalette)
                .WithMany(p => p.Saves)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
