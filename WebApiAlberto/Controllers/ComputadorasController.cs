using Microsoft.AspNetCore.Mvc;
using WebApiAlberto.Entitys;

namespace WebApiAlberto.Controllers
{
    [ApiController]
    [Route("api/computadoras")]
    public class ComputadorasController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<Computadoras>> Get()
        {
            return new List<Computadoras>()
            {
                new Computadoras() { Marca = "HP", Modelo = "SG1200"},
                new Computadoras() { Marca = "DELL", Modelo = "INSPIRON329"}
            };
        }

    }
}
