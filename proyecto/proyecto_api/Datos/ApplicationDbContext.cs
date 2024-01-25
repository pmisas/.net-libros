using Microsoft.EntityFrameworkCore;
using proyecto_api.Modelos;

namespace proyecto_api.Datos
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Proyecto> Proyectos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proyecto>().HasData(
                    new Proyecto()
                    {
                        Id = 1,
                        Nombre = "La niña del aro",
                        Descripcion = "Ninguna...",
                        Puntuacion = 2,
                        Terminado = false,
                        FechaPublicacion = DateTime.Now,
                        Autor = "Mimosa Misas"
                    },
                    new Proyecto()
                    {
                        Id = 2,
                        Nombre = "Barbie",
                        Descripcion = "pelicula fenizi...",
                        Puntuacion = 2,
                        Terminado = false,
                        FechaPublicacion = DateTime.Now,
                        Autor = "Juan Misas"
                    }
            );
        }
    }
}
