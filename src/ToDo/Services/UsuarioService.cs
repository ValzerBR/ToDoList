using ToDo.Contracts;
using ToDo.Exceptions;
using ToDo.Models;
using ToDo.Repository;
using ToDo.Util;

namespace ToDo.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly UsuarioRepository _usuarioRepository;
        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        private UsuarioResponseDC? FormataUsuario(Usuario? obj)
        {
            if (obj.IsNull())
                return null;

            return new UsuarioResponseDC
            {
                Id = obj.Id,
                Nome = obj.Nome,
                Email = obj.Email,
                Tarefas = !obj.Tarefas.IsNull() ? obj.Tarefas.Select(t => new TarefaResponseDC
                {
                    Id = t.Id,
                    DataDeEncerramento = t.DataDeEncerramento.ToDateBR(),
                    DataDeVencimento = t.DataDeVencimento.ToDateBR(),
                    DataDeCriacao = t.DataDeCriacao.ToDateBR(),
                    Descricao = t.Descricao,
                    Titulo = t.Titulo,
                    Status = t.Status,
                    UsuarioId = t.UsuarioId,
                    Categorias = t.Categorias.Select(w => new CategoriaDC
                    {
                        Id = w.Id,
                        Nome = w.Nome
                    }).ToList()

                }).ToList() : null
            };
        }

        public UsuarioResponseDC Create(UsuarioNovoDC usuario)
        {
            var user = _usuarioRepository.Save(new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email
            });

            return FormataUsuario(user);
        }

        public UsuarioResponseDC Update(UsuarioDC usuario)
        {
            //Como eu posso melhorar esse método para que eu não precise ficar settando as propriedades do meu usuário toda vez que eu chamar um Update?

            var usuarioExistente = _usuarioRepository.GetById(usuario.Id);
            if (usuarioExistente.IsNull())
                throw new BusinessException("Usuário não existe.");

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;

            var user = _usuarioRepository.Save(usuarioExistente);
            return FormataUsuario(user);
        }

        public void Delete(int[] ids)
        {
            foreach (int idUsuario in ids)
            {
                Usuario usuario = _usuarioRepository.GetById(idUsuario);
                if (!usuario.IsNull())
                    _usuarioRepository.Delete(usuario);
            }
        }

        public UsuarioResponseDC Detail(int id)
        {
            Usuario usuario = _usuarioRepository.GetById(id);
            return FormataUsuario(usuario);
        }

        public IEnumerable<UsuarioResponseDC> Search()
        {
            return _usuarioRepository.GetAll().Select(u => FormataUsuario(u)).ToList();
        }

        public UsuarioResponseDC GetByEmail(string email)
        {
            Usuario usuario = _usuarioRepository.GetAll().Where(w => w.Email == email).FirstOrDefault();
            var response = FormataUsuario(usuario);
            return response;
        }
    }
}
