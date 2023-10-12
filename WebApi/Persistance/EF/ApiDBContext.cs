using Microsoft.EntityFrameworkCore;
using WebApi.Models;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    public DbSet<Permiso> Permisos { get; set; }
    public DbSet<TipoPermiso> TiposPermisos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Permiso>()
            .HasOne(p => p.TipoPermiso)
            .WithMany(tp => tp.Permisos)  
            .HasForeignKey(p => p.TipoPermiso);

        modelBuilder.Entity<TipoPermiso>()
            .HasMany(tp => tp.Permisos)
            .WithOne(p => p.TipoPermiso)
            .HasForeignKey(p => p.TipoPermiso);
    }
}