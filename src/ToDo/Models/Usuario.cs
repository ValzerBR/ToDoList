using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Usuario
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
