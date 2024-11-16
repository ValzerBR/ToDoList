using ToDo.Models;

namespace ToDo.Contracts
{
    public interface ITarefa
    {
        Tarefa Create(Tarefa usuario);
        Tarefa Update(Tarefa usuario);
        Tarefa Detail(int id);
        void Delete(int[] ids);
        IEnumerable<Tarefa> Search();
    }
}
