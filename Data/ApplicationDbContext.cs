using Microsoft.EntityFrameworkCore;
using ReportesMVC.Models;

namespace ReportesMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }  
        public DbSet<Persona> Personas { get; set; }

        //  protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);

        //     modelBuilder.Entity<Persona>().Property(p => p.TelefonoOCelular)
        //         .HasColumnName("TelefonoOCelular1");
        // }


    }
}
