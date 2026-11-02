using _2doParcialSharlot.Models;
using Microsoft.EntityFrameworkCore;

namespace _2doParcialSharlot.Context
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
        public DbSet<Model1> Model1 { get; set; }
    }
}
