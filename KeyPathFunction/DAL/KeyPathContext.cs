using System;
using System.Collections.Generic;
using KeyPathFunctions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KeyPathFunctions.DAL;

public partial class KeyPathContext : DbContext
{
    private readonly IConfiguration _configuration;

    public KeyPathContext()
    {
    }

    public KeyPathContext(DbContextOptions<KeyPathContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<HeroModel> Heroes { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Retrieve the connection string from local.settings.json when in development and the function apps connection strings when deployed to Azure
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HeroModel>(entity =>
        {
            entity.HasKey(e => e.IdHero);

            entity.ToTable("Hero");

            entity.Property(e => e.IdHero).HasColumnName("ID_Hero");
            entity.Property(e => e.Alignment)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.EyeColor)
                .HasMaxLength(23)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.HairColor)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Publisher)
                .HasMaxLength(17)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
