using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Models;

public partial class ZodiacTinderContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ZodiacTinderContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ZodiacTinderContext(DbContextOptions<ZodiacTinderContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<LikeZodiac> LikeZodiacs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Zodiac> Zodiacs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("ZodiacTinderDatabase");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Friend>(entity =>
        {
            entity.ToTable("Friend");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
        });

        modelBuilder.Entity<LikeZodiac>(entity =>
        {
            entity.ToTable("LikeZodiac");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
        });

        modelBuilder.Entity<Zodiac>(entity =>
        {
            entity.ToTable("Zodiac");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
