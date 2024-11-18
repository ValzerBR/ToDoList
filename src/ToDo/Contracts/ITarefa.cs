using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ToDo.Models;

namespace ToDo.Contracts
{
    public interface ITarefa
    {
        Tarefa Create(TarefaDC usuario);
        Tarefa Update(TarefaDC usuario);
        Tarefa Detail(int id);
        void Delete(int[] ids);
        IEnumerable<Tarefa> Search();
    }

    [DataContract]
    public class TarefaDC
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public Status Status { get; set; }
        [DataMember]
        public DateTime DataDeCriacao { get; set; }
        [DataMember]
        public DateTime? DataDeEncerramento { get; set; }
        [DataMember]
        public DateTime DataDeVencimento { get; set; }
        [DataMember]
        public int UsuarioId { get; set; }
        [DataMember]
        public ICollection<CategoriaDC>? Categorias { get; set; }
    }
}
