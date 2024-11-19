using System.Data;
using ToDo.Contracts;
using ToDo.Models;
using ToDo.Repository;
using ToDo.Util;

namespace ToDo.Services
{
    public class TarefaService : ITarefa
    {
        private readonly TarefaRepository _tarefaRepository;
        private readonly Context.Context _context;
        public TarefaService(TarefaRepository tarefaRepository, Context.Context ctx)
        {
            _tarefaRepository = tarefaRepository;
            _context = ctx;
        }

        private TarefaResponseDC? FormataTarefa(Tarefa obj)
        {
            if (obj.IsNull())
                return null;

            return new TarefaResponseDC
            {
                DataDeEncerramento = obj.DataDeEncerramento.ToDateBR(),
                DataDeVencimento = obj.DataDeVencimento.ToDateBR(),
                DataDeCriacao = obj.DataDeCriacao.ToDateBR(),
                Descricao = obj.Descricao,
                Titulo = obj.Titulo,
                StatusFormatado = obj.Status.GetEnumName(),
                UsuarioId = obj.UsuarioId,
                Categorias = obj.Categorias.Select(w => new CategoriaDC
                {
                    Id = w.Id,
                    Nome = w.Nome
                }).ToList()
            };
        }

        public TarefaResponseDC Create(TarefaDC tarefa)
        {
            var categoriasParaAssociar = new List<Categoria>();

            foreach (CategoriaDC categoria in tarefa.Categorias)
            {
                var categoriaExistente = _context.Categorias.FirstOrDefault(c => c.Id == categoria.Id);

                if (categoriaExistente != null)
                    categoriasParaAssociar.Add(categoriaExistente);
                else
                {
                    var novaCategoria = new Categoria
                    {
                        Nome = categoria.Nome
                    };

                    _context.Categorias.Add(novaCategoria);
                    categoriasParaAssociar.Add(novaCategoria);
                }
            }

            var tarefaParaSalvar = new Tarefa
            {
                Id = tarefa.Id,
                DataDeCriacao = DateTime.Now,
                DataDeEncerramento = tarefa.DataDeEncerramento,
                DataDeVencimento = tarefa.DataDeVencimento,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status,
                Titulo = tarefa.Titulo,
                UsuarioId = tarefa.UsuarioId,
                Categorias = categoriasParaAssociar
            };

            _tarefaRepository.Save(tarefaParaSalvar);
            return FormataTarefa(tarefaParaSalvar);
        }

        public TarefaResponseDC Update(TarefaDC tarefa)
        {
            var tarefaEntity = _tarefaRepository.GetById(tarefa.Id);

            if (tarefaEntity.IsNull())
                throw new Exception("Tarefa não encontrada");

            tarefaEntity.DataDeEncerramento = tarefa.DataDeEncerramento;
            tarefaEntity.DataDeVencimento = tarefa.DataDeVencimento;
            tarefaEntity.Descricao = tarefa.Descricao;
            tarefaEntity.Status = tarefa.Status;
            tarefaEntity.Titulo = tarefa.Titulo;
            tarefaEntity.UsuarioId = tarefa.UsuarioId;

            // Se Categorias forem passadas, atualiza as categorias
            if (tarefa.Categorias != null)
            {
                tarefaEntity.Categorias = tarefa.Categorias.Select(c => new Categoria
                {
                    Id = c.Id,
                    Nome = c.Nome
                }).ToList();
            }

            _tarefaRepository.Save(tarefaEntity);
            return FormataTarefa(tarefaEntity);
        }

        public void Delete(int[] ids)
        {
            foreach (int idTarefa in ids)
            {
                Tarefa tarefa = _tarefaRepository.GetById(idTarefa);
                if (!tarefa.IsNull())
                    _tarefaRepository.Delete(tarefa);
            }
        }

        public TarefaResponseDC Detail(int id)
        {
            return FormataTarefa(_tarefaRepository.GetById(id));
        }

        public IEnumerable<TarefaResponseDC> Search()
        {
            return _tarefaRepository.GetAll().Select(w => FormataTarefa(w)).ToList();
        }
    }
}
