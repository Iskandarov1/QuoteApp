using App.Domain.Entities;
using App.Domain.Entities.Subscribe;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
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

        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsActive).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.LastNotificationSent).IsRequired(false);
            entity.Property(e => e.PreferredNotificationMethod).HasConversion<int>().IsRequired();
            entity.Property(e => e.LastSentQuoteText).HasMaxLength(500).HasColumnName("LastSentQuoteText");
            entity.Property(e => e.LastSentQuoteAuthor).HasMaxLength(100).HasColumnName("LastSentQuoteAuthor");
            
            
            entity.OwnsOne(e => e.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(256)
                    .IsRequired(false);

                email.HasIndex(e => e.Value)
                    .IsUnique();
            });
            
            entity.OwnsOne(e => e.PhoneNumber, phone =>
            {
                phone.Property(e => e.Value)
                    .HasColumnName("PhoneNumber")
                    .HasMaxLength(20)
                    .IsRequired(false);

                phone.HasIndex(e => e.Value)
                    .IsUnique();
            });
        });
    }
}