using ToDo.Contracts;
using ToDo.Models;
using ToDo.Repository;
using ToDo.Util;

namespace ToDo.Services
{
    public class TarefaService : ITarefa
    {
        private readonly TarefaRepository _tarefaRepository;
        public TarefaService(TarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public Tarefa Create(TarefaDC tarefa)
        {
            //Resolver o problema de não conseguir criar uma tarefa com uma categoria que já exista no sistema.

            return _tarefaRepository.Save(new Tarefa
            {
                Id = tarefa.Id,
                DataDeCriacao = tarefa.DataDeCriacao,
                DataDeEncerramento = tarefa.DataDeEncerramento,
                DataDeVencimento = tarefa.DataDeVencimento,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status,
                Titulo = tarefa.Titulo,
                UsuarioId = tarefa.UsuarioId,
                Categorias = tarefa.Categorias?.Select(tarefa => new Categoria
                {
                    Id = tarefa.Id == 0 ? 0 : tarefa.Id,
                    Nome = tarefa.Nome
                }).ToList()
            });
        }
        public Tarefa Update(TarefaDC tarefa)
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

            return _tarefaRepository.Save(tarefaEntity);
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

        public Tarefa Detail(int id)
        {
            return _tarefaRepository.GetById(id);
        }

        public IEnumerable<Tarefa> Search()
        {
            return _tarefaRepository.GetAll().ToList();
        }
    }
}
