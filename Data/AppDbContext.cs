using Microsoft.EntityFrameworkCore;
using Naitv1.Models;


namespace Naitv1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Actividad> Actividades { get; set; }

        public DbSet<HistorialParticipacion> HistorialParticipaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistorialParticipacion>()
                .HasKey(h => new { h.IdUsuario, h.IdActividad });

            modelBuilder.Entity<HistorialParticipacion>()
                .HasOne(h => h.Usuario)
                .WithMany(u => u.HistorialParticipaciones)
                .HasForeignKey(h => h.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HistorialParticipacion>()
                .HasOne(h => h.Actividad)
                .WithMany(a => a.HistorialParticipantes)
                .HasForeignKey(h => h.IdActividad)
                .OnDelete(DeleteBehavior.NoAction);

        }


    }
}
