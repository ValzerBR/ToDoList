using System.Runtime.Serialization;
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

    [DataContract]
    public class CategoriaDC
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nome { get; set; }
    }
}
