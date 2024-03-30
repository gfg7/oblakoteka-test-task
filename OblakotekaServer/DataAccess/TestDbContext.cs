using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OblakotekaServer.DataAccess.Models;

namespace OblakotekaServer.DataAccess;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC272F277EC7");

            entity.ToTable("Product", "TestSchema", tb => tb.HasTrigger("Product_Trigger"));

            entity.HasIndex(e => e.Name, "IX_Product_Name").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
