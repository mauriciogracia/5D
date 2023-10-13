using Microsoft.EntityFrameworkCore;
using WebApi.Models;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    public DbSet<Permission> Permisos { get; set; }
    public DbSet<PermissionType> TiposPermisos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Permission>()
            .HasOne<PermissionType>() // Assuming PermissionType is the related entity
            .WithMany(tp => tp.Permisos)
            .HasForeignKey(p => p.TipoPermisoId);

        modelBuilder.Entity<PermissionType>()
            .HasMany(tp => tp.Permisos)
            .WithOne()
            .HasForeignKey(p => p.TipoPermisoId);
    }
}