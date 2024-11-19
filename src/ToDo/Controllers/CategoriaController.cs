using Microsoft.AspNetCore.Mvc;
using ToDo.Contracts;
using ToDo.Models;
using ToDo.Repository;
using static ToDo.Util.Controller;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ICategoria _categoriaContract;
        public CategoriaController(ICategoria categoriaContract)
        {
            _categoriaContract = categoriaContract;
        }

        [HttpGet("Detail/Categoria/{id}")]
        public IActionResult Detail(int id)
        {
            return JsonOptionsUtil.Create(_categoriaContract.Detail(id));
        }

        [HttpDelete("Delete/Categoria/")]
        public IActionResult Delete([FromQuery] int[] ids)
        {
            _categoriaContract.Delete(ids);
            return Json(null);
        }

        [HttpDelete("Delete/CategoriaDeTarefa/")]
        public IActionResult DeleteCategoriaFromTarefa([FromQuery] int tarefaId, [FromQuery] int categoriaId)
        {
            _categoriaContract.DeleteCategoriaFromTarefa(tarefaId, categoriaId);
            return Json(null);
        }
        
        [HttpPost("Create/Categoria/")]
        public IActionResult Create(CategoriaDC categoria)
        {
            _categoriaContract.Create(categoria);
            return Json(null);
        }

    }
}
