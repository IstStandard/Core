// TO SharedLib

namespace Core.Database;

using Core.Database.Models;
using Microsoft.EntityFrameworkCore;
public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; init; }

    private IConfiguration _configuration;
    

    public ApplicationContext(IConfiguration configuration)
    {
        _configuration = configuration;
        // Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration["Database:ConnectionString"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Uid);
            entity.Property(x => x.Uid).HasDefaultValueSql("uuid_generate_v4()"); // 
            entity
                .Property(x => x.Fullname)
                .HasComputedColumnSql("\"LastName\" || \"FirstName\" || \"Patronymic\"", true)
                .ValueGeneratedOnAdd();
            entity.Property(x => x.FirstName).HasDefaultValueSql("''").HasMaxLength(125);
            entity.Property(x => x.LastName).HasDefaultValueSql("''").HasMaxLength(125);
            entity.Property(x => x.Patronymic).HasDefaultValueSql("''").HasMaxLength(125);
            entity.Property(x => x.Permissions).HasDefaultValueSql("0");
            entity.Property(x => x.Status).HasDefaultValueSql("-1");

            entity.Property(x => x.Password).HasMaxLength(128);
            entity.Property(x => x.Email).HasMaxLength(256);
            entity.Property(x => x.Username).HasMaxLength(128);
            entity.Property(x => x.AddedDate).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<BlackList>(entity =>
        {
            entity.HasKey(x => x.Uid);
            entity.Property(x => x.Uid).HasDefaultValueSql("uuid_generate_v4()"); //
            entity.Property(x => x.Reason).HasMaxLength(1024);
            entity.Property(x => x.AddedTime).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(x => x.Uid);
            entity.Property(e => e.Uid).HasDefaultValueSql("uuid_generate_v4()");
            entity.HasOne(d => d.User)
                .WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserUid);
        });

        base.OnModelCreating(modelBuilder);
    }
}
