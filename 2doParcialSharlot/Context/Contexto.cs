using _2doParcialSharlot.Models;
using Microsoft.EntityFrameworkCore;

namespace _2doParcialSharlot.Context
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<VentasDetalle> VentasDetalle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Productos>().HasData(
                new Productos
                {
                    ProductoId = 1,
                    Nombre = "Latte de Caramelo",
                    Precio = 210

                },
                new Productos
                {
                    ProductoId = 2,
                    Nombre = "Cappuchino",
                    Precio = 200

                },
                new Productos
                {
                    ProductoId = 3,
                    Nombre = "Americano",
                    Precio = 120

                },
                new Productos
                {
                    ProductoId = 4,
                    Nombre = "Frape",
                    Precio = 150

                },
                new Productos
                {
                    ProductoId = 5,
                    Nombre = "Espresso",
                    Precio = 150

                });
        }
    }
}
