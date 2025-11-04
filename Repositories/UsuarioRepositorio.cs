using TPFINALFINANZAS.Data;
using TPFINALFINANZAS.Models;

namespace TPFINALFINANZAS.Repositories
{
    // repositorio concreto de usuario
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(AppDbContext ctx) : base(ctx) {}
    }
}
