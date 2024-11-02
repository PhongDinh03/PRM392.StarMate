using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;


public partial class ZodiacTinderContext : DbContext
{
    public ZodiacTinderContext(DbContextOptions<ZodiacTinderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<LikeZodiac> LikeZodiacs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Zodiac> Zodiacs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Friend>(entity =>
        {
            entity.ToTable("Friend");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.FriendNavigation).WithMany(p => p.FriendFriendNavigations)
                .HasForeignKey(d => d.FriendId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Friend_User1");

            entity.HasOne(d => d.User).WithMany(p => p.FriendUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Friend_User");
        });

        modelBuilder.Entity<LikeZodiac>(entity =>
        {
            entity.ToTable("LikeZodiac");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.HasOne(d => d.User).WithMany(p => p.LikeZodiacs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikeZodiac_User");

            entity.HasOne(d => d.ZodiacLike).WithMany(p => p.LikeZodiacs)
                .HasForeignKey(d => d.ZodiacLikeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikeZodiac_Zodiac");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("gender");

            entity.HasOne(d => d.Zodiac).WithMany(p => p.Users)
                .HasForeignKey(d => d.ZodiacId)
                .HasConstraintName("FK_User_Zodiac");
        });

        modelBuilder.Entity<Zodiac>(entity =>
        {
            entity.ToTable("Zodiac");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
