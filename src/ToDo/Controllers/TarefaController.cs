using Microsoft.AspNetCore.Mvc;
using ToDo.Contracts;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : Controller
    {
        private readonly ITarefa _tarefaContract;
        public TarefaController(ITarefa tarefaContract)
        {
            _tarefaContract = tarefaContract;
        }
    }
}
