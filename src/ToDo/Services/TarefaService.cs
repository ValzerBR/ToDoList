using System.Data;
using ToDo.Contracts;
using ToDo.Exceptions;
using ToDo.Models;
using ToDo.Repository;
using ToDo.Util;

namespace ToDo.Services
{
    public class TarefaService : ITarefa
    {
        private readonly TarefaRepository _tarefaRepository;
        private readonly CategoriaRepository _categoriaRepository;
        public TarefaService(TarefaRepository tarefaRepository, CategoriaRepository categoriaRepository)
        {
            _tarefaRepository = tarefaRepository;
            _categoriaRepository = categoriaRepository;
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
            
            foreach (var idCategoria in tarefa.CategoriasId)
            {
                var categoriaExistente = _categoriaRepository.GetAll().FirstOrDefault(c => c.Id == idCategoria);

                if (categoriaExistente.IsNull())
                    throw new BusinessException("Categoria não existe.");
                    
                categoriasParaAssociar.Add(categoriaExistente);
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

            if (!tarefa.CategoriasId.IsNull())
            {
                foreach (var idCategoria in tarefa.CategoriasId)
                {
                    var categoria = _categoriaRepository.GetById(idCategoria);
                    if (categoria.IsNull())
                        throw new BusinessException("Categoria não encontrada.");
                    tarefaEntity.Categorias.Add(categoria);
                }
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
