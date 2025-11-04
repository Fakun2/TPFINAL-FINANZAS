using System.Collections.Generic;
using System.Threading.Tasks;

namespace TPFINALFINANZAS.Repositories
{
    // interfaz generica de repositorio para operaciones basicas
    public interface IRepositorio<T> where T : class
    {
        Task<List<T>> ObtenerTodosAsync();
        Task<T?> ObtenerPorIdAsync(int id);
        Task AgregarAsync(T entidad);
        void Actualizar(T entidad);
        void Eliminar(T entidad);
        Task GuardarAsync();
    }
}
