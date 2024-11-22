using ToDo.Contracts;
using ToDo.Exceptions;
using ToDo.Models;
using ToDo.Repository;
using ToDo.Util;

namespace ToDo.Services
{
    public class CategoriaService : ICategoria
    {
        private readonly CategoriaRepository _categoriaRepository;
        private readonly Context.Context _context;
        public CategoriaService(CategoriaRepository categoriaRepository, Context.Context ctx)
        {
            _categoriaRepository = categoriaRepository;
            _context = ctx;
        }

        private CategoriaDC? FormataCategoria(Categoria? obj)
        {
            if (obj.IsNull())
                return null;

            return new CategoriaDC
            {
                Nome = obj.Nome,
                Id = obj.Id
            };
        }

        public CategoriaDC? Detail(int id)
        {
            return FormataCategoria(_categoriaRepository.GetById(id));
        }

        public void Delete(int[] ids)
        {
            foreach (int idCategoria in ids)
            {
                Categoria categoria = _categoriaRepository.GetById(idCategoria);
                if (!categoria.IsNull())
                    _categoriaRepository.Delete(categoria);
            }
        }

        public void DeleteCategoriaFromTarefa(int tarefaId, int idCategoria)
        {
            Categoria? categoria = _categoriaRepository.GetById(idCategoria);

            if (categoria.IsNull())
                throw new BusinessException("Categoria não existe no sistema.");

            var tarefa = categoria.Tarefas.Where(t => t.Id == tarefaId).FirstOrDefault();

            if (tarefa.IsNull())
                throw new BusinessException($"A categoria {categoria.Nome} não está associada à tarefa.");

            _categoriaRepository.Delete(categoria);
        }

        public IEnumerable<CategoriaDC> Search(string? nome)
        {
            _categoriaRepository.GetAll();
            if (!String.IsNullOrEmpty(nome))
            {

            }
        }

        public CategoriaDC Create(CategoriaNovaDC categoria)
        {
            if (_categoriaRepository.GetAll().Any(w => w.Nome == categoria.Nome))
                throw new BusinessException("Categoria já existe.");

            Categoria novaCategoria = _categoriaRepository.Save(new Categoria
            {
                Nome = categoria.Nome,
            });

            return new CategoriaDC
            {
                Id = novaCategoria.Id,
                Nome = novaCategoria.Nome
            };
        }

        public CategoriaDC Update(CategoriaDC categoria)
        {
            var categoriaEntity = _categoriaRepository.GetById(categoria.Id);
            categoriaEntity.Nome = categoria.Nome;
            _categoriaRepository.Save(categoriaEntity);

            return categoria;
        }
    }
}
