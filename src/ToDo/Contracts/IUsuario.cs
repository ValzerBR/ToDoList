using System.Runtime.Serialization;
using ToDo.Models;

namespace ToDo.Contracts
{
    public interface IUsuario
    {
        Usuario Create(UsuarioDC usuario);
        Usuario Update(UsuarioDC usuario);
        Usuario Detail(int id);
        void Delete(int[] ids);
        IEnumerable<Usuario> Search();
    }

    [DataContract]
    public class UsuarioDC
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
}
