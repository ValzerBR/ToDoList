using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.GenericRepository;
using ToDo.Models;

namespace ToDo.Repository
{
    public class CategoriaRepository : Repository<Categoria>
    {
        public CategoriaRepository(Context.Context ctx) : base(ctx)
        {
        }
    }
}
