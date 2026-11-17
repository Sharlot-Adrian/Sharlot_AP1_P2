using _2doParcialSharlot.Context;
using _2doParcialSharlot.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _2doParcialSharlot.Service
{
    public class VentasService(IDbContextFactory<Contexto> contextFactory) : Aplicada1.Core.IServiceResult<Ventas, int>
    {
        public async Task<Result> Guardar(Ventas Venta)
        {
            if(Venta.Detalle == null || !Venta.Detalle.Any())
                return Result.Failure(new Error("Venta.Detalle == null", "Debe incluir por lo menos un detalle."));

            var result = await Existe(Venta.VentaId) ? await Modificar(Venta) : await Insertar(Venta);

            return result ? Result.Success(true) : Result.Failure(new Error("Error","no se pudo guardar"));
            
        }

        private async Task<bool> Existe(int id)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Ventas.AnyAsync(v => v.VentaId == id);
        }

        private async Task <bool> Insertar(Ventas venta)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            if (!await ValidarDetalle(venta, contexto))
                return false;

            foreach (var detalle in venta.Detalle)
            {
                await AjustarStock(contexto,detalle.ProductoId, -detalle.Cantidad);
            }
            contexto.Ventas.Add(venta);
            return await contexto.SaveChangesAsync() > 0;

        }
        private async Task AjustarStock(Contexto contexto, int productoId, int cantidad)
        {
            var producto = await contexto.Productos.FirstAsync(p => p.ProductoId == productoId);
            producto.Stock += cantidad;
        }

        private async Task <bool> ValidarDetalle(Ventas venta, Contexto contexto)
        {
            foreach (var detalle in venta.Detalle)
            {
                var producto = await contexto.Productos.FindAsync(detalle.ProductoId);

                if (producto == null)
                    return false;

                if (detalle.Cantidad > producto.Stock)
                    return false;
                
                detalle.Precio = producto.Precio;
            }
            venta.Total = venta.Detalle.Sum(d => d.Cantidad * d.Precio);
            return true;
        }

        private async Task<bool> Modificar(Ventas venta)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            var original = await contexto.Ventas
                .Include(d => d.Detalle)
                .FirstOrDefaultAsync(v => v.VentaId == venta.VentaId);

            if (original == null)
                return false;

            foreach (var detalle in original.Detalle)
            {
                await AjustarStock(contexto, detalle.ProductoId, detalle.Cantidad);
            }

            if (!await ValidarDetalle(venta, contexto))
                return false;

            foreach (var detalle in venta.Detalle)
            {
                await AjustarStock(contexto, detalle.ProductoId,-detalle.Cantidad);
            }

            contexto.VentasDetalle.RemoveRange(original.Detalle);
            contexto.VentasDetalle.AddRange(
                venta.Detalle.Select(d => new VentasDetalle
                {
                    VentaId = original.VentaId,
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio
                }));

            original.Fecha = venta.Fecha;
            original.Total = venta.Total;
            
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<Result<Ventas?>> Buscar(int id)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Ventas.Include(v => v.Detalle)
                                        .ThenInclude(d => d.Productos)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(v => v.VentaId == id);
        }

        public async Task<Result> Eliminar(int id)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            var venta = await contexto.Ventas.Include(v => v.Detalle)
                                             .FirstOrDefaultAsync(v => v.VentaId == id);
            if (venta == null)
                return Result.Failure(new Error("venta == null", "Venta no encontrada."));

            foreach (var detalle in venta.Detalle)
            {
                await AjustarStock(contexto, detalle.ProductoId, detalle.Cantidad);
            }

            contexto.VentasDetalle.RemoveRange(venta.Detalle);
            contexto.Ventas.Remove(venta);

            return await contexto.SaveChangesAsync() > 0 ? Result.Success(true) : Result.Failure(new Error("Error al eliminar", "No se pudo eliminar."));
        }

        public async Task<Result<List<Productos>>> GetListProductos(Expression<Func<Productos, bool>> criterio)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Productos.Include(v => v.Detalle)
                                        .Where(criterio)
                                        .AsNoTracking()
                                        .ToListAsync();
        }
        public async Task<Result<List<Ventas>>> GetList(Expression<Func<Ventas, bool>> criterio)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Ventas.Include(v => v.Detalle)
                                        .Where(criterio)
                                        .AsNoTracking()
                                        .ToListAsync();
        }

        public async Task<List<Productos>> ListarProductos()
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Productos.AsNoTracking().ToListAsync();
        }
    }
}
