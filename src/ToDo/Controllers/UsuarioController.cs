using Microsoft.AspNetCore.Mvc;
using ToDo.Contracts;
using static ToDo.Util.Controller;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]  // Rota base será /Usuario
    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuarioContract;

        public UsuarioController(IUsuario usuarioContract)
        {
            _usuarioContract = usuarioContract;
        }

        [HttpPost("Create/")]  // Isso vai resultar em /Usuario/Create
        public IActionResult Create([FromBody] UsuarioNovoDC usuario)
        {
            return JsonOptionsUtil.Create(_usuarioContract.Create(usuario));
        }

        [HttpPut("Update/")]  // Isso vai resultar em /Usuario/Update
        public IActionResult Update(UsuarioDC usuario)
        {
            return JsonOptionsUtil.Create(_usuarioContract.Update(usuario));
        }

        [HttpGet("Detail/{id}")]  // Isso vai resultar em /Usuario/Detail/{id}
        public IActionResult Detail(int id)
        {
            return JsonOptionsUtil.Create(_usuarioContract.Detail(id));
        }

        [HttpDelete("Delete/")]  // Isso vai resultar em /Usuario/Delete
        public IActionResult Delete([FromQuery] int[] ids)
        {
            _usuarioContract.Delete(ids);
            return Json(null);
        }

        [HttpGet("Search/")]  // Isso vai resultar em /Usuario/Search
        public IActionResult Search()
        {
            return JsonOptionsUtil.Create(_usuarioContract.Search());
        }

        [HttpGet("GetByEmail")]  // Isso vai resultar em /Usuario/GetByEmail
        public IActionResult GetByEmail(string email)
        {
            return JsonOptionsUtil.Create(_usuarioContract.GetByEmail(email));
        }
    }
}
