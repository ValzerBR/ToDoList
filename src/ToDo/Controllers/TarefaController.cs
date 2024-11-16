using Microsoft.AspNetCore.Mvc;
using ToDo.Contracts;
using static ToDo.Util.Controller;
using ToDo.Models;

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

        [HttpPost("/Create/")]
        public IActionResult Create(Tarefa tarefa)
        {
            return JsonOptionsUtil.Create(_tarefaContract.Create(tarefa));
        }

        [HttpPut("/Update/")]
        public IActionResult Update(Tarefa tarefa)
        {
            return JsonOptionsUtil.Create(_tarefaContract.Update(tarefa));
        }

        [HttpGet("/Detail/{id}")]
        public IActionResult Detail(int id)
        {
            return JsonOptionsUtil.Create(_tarefaContract.Detail(id));
        }

        [HttpDelete("/Delete/{ids}")]
        public IActionResult Delete(int[] ids)
        {
            _tarefaContract.Delete(ids);
            return Json(null);
        }

        [HttpGet("/Search/")]
        public IActionResult Search()
        {
            return JsonOptionsUtil.Create(_tarefaContract.Search());
        }
    }
}
