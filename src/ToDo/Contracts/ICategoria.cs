using ToDo.Models;

namespace ToDo.Contracts
{
    public interface ICategoria
    {
        Categoria Create(Categoria categoria);
        Categoria Update(Categoria categoria);
        Categoria Detail(int id);
        void Delete(int[] ids);
        IEnumerable<Categoria> Search();
    }
}
