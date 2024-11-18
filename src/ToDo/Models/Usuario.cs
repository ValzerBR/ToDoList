using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Usuario
    {
        [Required]
        [Key]
        public virtual int Id { get; set; }
        [Required]
        public virtual string Nome { get; set; }
        [Required]
        public virtual string Email { get; set; }

        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
