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

        public Usuario Create(UsuarioDC usuario)
        {
            return _usuarioRepository.Save(new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email
            });
        }

        public Usuario Update(UsuarioDC usuario)
        {
            //Como eu posso melhorar esse método para que eu não precise ficar settando as propriedades do meu usuário toda vez que eu chamar um Update?

            var usuarioExistente = _usuarioRepository.GetById(usuario.Id);

            if (usuarioExistente.IsNull())
                throw new BusinessException("Usuário não existe.");

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;

            return _usuarioRepository.Save(usuarioExistente);
        }

        public void Delete(int[] ids)
        {
            foreach(int idUsuario in ids)
            {
                Usuario usuario = _usuarioRepository.GetById(idUsuario);
                if(!usuario.IsNull())
                    _usuarioRepository.Delete(usuario);
            }
        }

        public Usuario Detail(int id)
        {
            Usuario usuario = _usuarioRepository.GetById(id);
            return usuario;
        }

        public IEnumerable<Usuario> Search()
        {
            return _usuarioRepository.GetAll().ToList();
        }

        
    }
}
