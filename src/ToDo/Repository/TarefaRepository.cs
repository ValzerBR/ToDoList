using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.GenericRepository;
using ToDo.Models;

namespace ToDo.Repository
{
    public class TarefaRepository : Repository<Tarefa>
    {
        public TarefaRepository(Context.Context ctx) : base(ctx)
        {
        }
    }
}
