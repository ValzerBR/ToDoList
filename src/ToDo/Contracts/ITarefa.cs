using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ToDo.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ToDo.Contracts
{
    public interface ITarefa
    {
        TarefaResponseDC Create(TarefaDC usuario);
        TarefaResponseDC Update(TarefaDC usuario);
        TarefaResponseDC Detail(int id);
        void Delete(int[] ids);
        IEnumerable<TarefaResponseDC> Search();
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
        public DateTime? DataDeEncerramento { get; set; }
        [DataMember]
        public DateTime DataDeVencimento { get; set; }
        [DataMember]
        public int UsuarioId { get; set; }
        [DataMember]
        public ICollection<int>? CategoriasId { get; set; }
    }

    [DataContract]
    public class TarefaResponseDC
    {
        public string Titulo { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public string DataDeEncerramento { get; set; }
        [DataMember]
        public string DataDeVencimento { get; set; }
        [DataMember]
        public int UsuarioId { get; set; }
        [DataMember]
        public ICollection<CategoriaDC>? Categorias { get; set; }
        [DataMember]
        public string StatusFormatado { get; set; }
        [DataMember]
        public string DataDeCriacao { get; set; }

        private string? GetStatusFormatado(Status status)
        {
            return Enum.GetName(typeof(Status), status);
        }
    }
}
