using Microsoft.AspNetCore.Mvc;
using ToDo.Contracts;
using static ToDo.Util.Controller;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuarioContract;
        public UsuarioController(IUsuario usuarioContract)
        {
            _usuarioContract = usuarioContract;
        }

        [HttpPost("/Create/")]
        public IActionResult Create(Usuario usuario)
        {
            return JsonOptionsUtil.Create(_usuarioContract.Create(usuario));
        }

        [HttpPut("/Update/")]
        public IActionResult Update(Usuario usuario)
        {
            return JsonOptionsUtil.Create(_usuarioContract.Update(usuario));
        }

        [HttpGet("/Detail/{id}")]
        public IActionResult Detail(int id)
        {
            return JsonOptionsUtil.Create(_usuarioContract.Detail(id));
        }

        [HttpDelete("/Delete/{ids}")]
        public IActionResult Delete(int[] ids)
        {
            _usuarioContract.Delete(ids);
            return Json(null);
        }

        [HttpGet("/Search/")]
        public IActionResult Search()
        {
            return JsonOptionsUtil.Create(_usuarioContract.Search());
        }
    }
}
