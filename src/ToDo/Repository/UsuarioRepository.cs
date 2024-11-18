using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.GenericRepository;
using ToDo.Models;

namespace ToDo.Repository
{
    public class UsuarioRepository : Repository<Usuario>
    {
        public UsuarioRepository(Context.Context ctx) : base(ctx)
        {
        }
    }
}
