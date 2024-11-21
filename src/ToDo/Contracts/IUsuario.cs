using System.Runtime.Serialization;
using ToDo.Models;

namespace ToDo.Contracts
{
    public interface IUsuario
    {
        UsuarioResponseDC Create(UsuarioNovoDC usuario);
        UsuarioResponseDC Update(UsuarioDC usuario);
        UsuarioResponseDC Detail(int id);
        void Delete(int[] ids);
        IEnumerable<UsuarioResponseDC> Search();
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

    [DataContract]
    public class UsuarioNovoDC
    {
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Email { get; set; }
    }

    [DataContract]
    public class UsuarioResponseDC
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public ICollection<TarefaResponseDC>? Tarefas { get; set; }

    }
}
