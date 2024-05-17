using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace birthday.Models;

public partial class dbEntities : DbContext
{
    public dbEntities()
    {
    }

    public dbEntities(DbContextOptions<dbEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:dbconn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users_1");

            entity.Property(e => e.Bless).HasMaxLength(600);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
