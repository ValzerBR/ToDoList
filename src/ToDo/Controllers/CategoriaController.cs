using Microsoft.AspNetCore.Mvc;
using ToDo.Contracts;

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
    }
}
