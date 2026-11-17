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

        
    }
}
