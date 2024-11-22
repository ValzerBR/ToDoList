using Microsoft.AspNetCore.Mvc;
using ToDo.Contracts;
using ToDo.Models;
using ToDo.Util;
using static ToDo.Util.Controller;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITarefa _tarefaContract;
        public TarefaController(ITarefa tarefaContract)
        {
            _tarefaContract = tarefaContract;
        }


        [HttpPost("/Create/Tarefa/")]
        public IActionResult Create([FromBody] TarefaNovaDC tarefa)
        {
            return JsonOptionsUtil.Create(_tarefaContract.Create(tarefa));
        }

        [HttpPut("/Update/Tarefa/")]
        public IActionResult Update(TarefaDC tarefa)
        {
            return JsonOptionsUtil.Create(_tarefaContract.Update(tarefa));
        }

        [HttpGet("/Detail/Tarefa/{id}")]
        public IActionResult Detail(int id)
        {
            return JsonOptionsUtil.Create(_tarefaContract.Detail(id));
        }

        [HttpDelete("/Delete/Tarefa/")]
        public IActionResult Delete([FromQuery] int[] ids)
        {
            _tarefaContract.Delete(ids);
            return Json(null);
        }

        [HttpGet("/Search/Tarefa/")]
        public IActionResult Search(string? descricao, int? status, int? idCategoria, string? titulo)
        {
            return JsonOptionsUtil.Create(_tarefaContract.Search(descricao, status, idCategoria, titulo).ToList());
        }
    }
}
