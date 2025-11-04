using System.Collections.Generic;
using System.Threading.Tasks;
using TPFINALFINANZAS.Models;

namespace TPFINALFINANZAS.Repositories
{
    // operaciones especificas de gasto
    public interface IGastoRepositorio : IRepositorio<Gasto>
    {
        Task<List<Gasto>> ObtenerConTodoAsync();
    }
}
