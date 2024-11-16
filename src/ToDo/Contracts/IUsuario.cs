using ToDo.Models;

namespace ToDo.Contracts
{
    public interface IUsuario
    {
        Usuario Create(Usuario usuario);
        Usuario Update(Usuario usuario);
        Usuario Detail(int id);
        void Delete(int[] ids);
        IEnumerable<Usuario> Search();
    }
}
