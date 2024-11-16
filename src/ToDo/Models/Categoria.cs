using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Categoria
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
