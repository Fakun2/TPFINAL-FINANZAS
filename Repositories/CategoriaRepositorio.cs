using TPFINALFINANZAS.Data;
using TPFINALFINANZAS.Models;

namespace TPFINALFINANZAS.Repositories
{
    // repositorio concreto de categoria
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        public CategoriaRepositorio(AppDbContext ctx) : base(ctx) {}
    }
}
