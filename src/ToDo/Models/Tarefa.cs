using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models
{
    public class Tarefa
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public DateTime DataDeCriacao { get; set; }
        public DateTime? DataDeEncerramento { get; set; }
        [Required]
        public DateTime DataDeVencimento { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        public ICollection<Categoria> Categorias { get; set; }
    }
}
