using Microsoft.AspNetCore.Mvc;
using ToDo.Contracts;
using ToDo.Models;
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

        [HttpPost("/Create/")]
        public IActionResult Create(Categoria categoria)
        {
            return JsonOptionsUtil.Create(_categoriaContract.Create(categoria));
        }

        [HttpPut("/Update/")]
        public IActionResult Update(Categoria categoria)
        {
            return JsonOptionsUtil.Create(_categoriaContract.Update(categoria));
        }

        [HttpGet("/Detail/{id}")]
        public IActionResult Detail(int id)
        {
            return JsonOptionsUtil.Create(_categoriaContract.Detail(id));
        }

        [HttpDelete("/Delete/{ids}")]
        public IActionResult Delete(int[] ids)
        {
            _categoriaContract.Delete(ids);
            return Json(null);
        }

        [HttpGet("/Search/")]
        public IActionResult Search()
        {
            return JsonOptionsUtil.Create(_categoriaContract.Search());
        }
    }
}
