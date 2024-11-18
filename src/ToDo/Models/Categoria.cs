using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Categoria
    {
        [Key]
        [Required]
        public virtual int Id { get; set; }
        [Required]
        public virtual string Nome { get; set; }

        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
