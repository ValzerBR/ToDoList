using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Contracts;
using ToDo.GenericRepository;
using ToDo.Models;

namespace ToDo.Repository
{
    public class TarefaRepository : Repository<Tarefa>
    {
        private readonly Context.AppDbContext _context;
        public TarefaRepository(Context.AppDbContext ctx) : base(ctx)
        {
            _context = ctx;
        }
    }
}
