using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Contracts;
using ToDo.GenericRepository;
using ToDo.Models;

namespace ToDo.Repository
{
    public class TarefaRepository : Repository<Tarefa>
    {

        private readonly Context.Context _context;
        public TarefaRepository(Context.Context ctx) : base(ctx)
        {
            _context = ctx;
        }

        public override void Delete(Tarefa tarefa)
        {
            _dbSet.Remove(tarefa);

            var categorias = tarefa.Categorias.ToList();
            _context.SaveChanges();

            foreach (var categoria in categorias)
            {
                var tarefas = _context.Tarefas.Count();

                bool tt = _context.Tarefas.Where(w => w.Id == categoria.Id).Any();
                var outrasTarefasComEssaCategoria = _context.Tarefas
                                                             .Any(w => w.Id == categoria.Id);

                if (!outrasTarefasComEssaCategoria)
                    _context.Categorias.Remove(categoria);

                _context.SaveChanges();
            }
        }
    }
}
