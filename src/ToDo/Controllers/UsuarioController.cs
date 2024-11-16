using Microsoft.AspNetCore.Mvc;
using ToDo.Contracts;

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
    }
}
