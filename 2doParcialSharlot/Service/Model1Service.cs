using _2doParcialSharlot.Context;
using _2doParcialSharlot.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _2doParcialSharlot.Service
{
    public class Model1Service(IDbContextFactory<Contexto> contextFactory) : Aplicada1.Core.IServiceResult<Ventas, int>
    {
        public Task<Result<Ventas?>> Buscar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Ventas>>> GetList(Expression<Func<Ventas, bool>> criterio)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Guardar(Ventas entidad)
        {
            throw new NotImplementedException();
        }
    }
}
