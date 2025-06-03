using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Quote> Quotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .ValueGeneratedNever();
            
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            
            entity.OwnsOne(e => e.Author, author =>
            {
                author.Property(a => a.Value)
                    .HasColumnName("Author")
                    .HasMaxLength(200)
                    .IsRequired();
            });
            
            entity.OwnsOne(e => e.Textt, text =>
            {
                text.Property(t => t.Value)
                    .HasColumnName("Text")
                    .HasMaxLength(1000)
                    .IsRequired();
            });
            
            entity.OwnsOne(e => e.Category, category =>
            {
                category.Property(c => c.Value)
                    .HasColumnName("Category")
                    .HasMaxLength(100)
                    .IsRequired();
            });
        });
    }
}