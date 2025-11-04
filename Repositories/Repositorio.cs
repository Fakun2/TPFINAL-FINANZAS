using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TPFINALFINANZAS.Data;

namespace TPFINALFINANZAS.Repositories
{
    // implementacion generica sobre EF Core para CRUD
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected readonly AppDbContext _ctx;
        protected readonly DbSet<T> _set;

        public Repositorio(AppDbContext ctx)
        {
            _ctx = ctx;
            _set = _ctx.Set<T>();
        }

        public Task<List<T>> ObtenerTodosAsync() => _set.ToListAsync();

        public Task<T?> ObtenerPorIdAsync(int id) => _set.FindAsync(id).AsTask();

        public Task AgregarAsync(T entidad) => _set.AddAsync(entidad).AsTask();

        public void Actualizar(T entidad) => _set.Update(entidad);

        public void Eliminar(T entidad) => _set.Remove(entidad);

        public Task GuardarAsync() => _ctx.SaveChangesAsync();
    }
}
