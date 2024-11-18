using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models
{
    public class Tarefa
    {
        [Key]
        [Required]
        public virtual int Id { get; set; }
        [Required]
        public virtual string Titulo { get; set; }
        public virtual string Descricao { get; set; }
        [Required]
        public virtual Status Status { get; set; }
        [Required]
        public virtual DateTime DataDeCriacao { get; set; }
        public virtual DateTime? DataDeEncerramento { get; set; }
        [Required]
        public virtual DateTime DataDeVencimento { get; set; }
        [Required]
        public virtual int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Categoria>? Categorias { get; set; }
    }
}
