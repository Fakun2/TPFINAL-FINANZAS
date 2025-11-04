using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TPFINALFINANZAS.Data;
using TPFINALFINANZAS.Models;

namespace TPFINALFINANZAS.Repositories
{
    // acceso a datos de gastos incluyendo categoria y usuario
    public class GastoRepositorio : Repositorio<Gasto>, IGastoRepositorio
    {
        public GastoRepositorio(AppDbContext ctx) : base(ctx) {}

        public Task<List<Gasto>> ObtenerConTodoAsync() =>
            _ctx.Gastos.Include(g => g.Categoria).Include(g => g.Usuario).ToListAsync();
    }
}
