using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAlberto.Entitys;
using WebApiAlberto.Filtros;
using WebApiAlberto.Services;

namespace WebApiAlberto.Controllers
{
    [ApiController]
    [Route("api/computadoras")]
    public class ComputadorasController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<ComputadorasController> logger;
        public ComputadorasController(ApplicationDbContext context, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<ComputadorasController> logger)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            //throw new NotImplementedException();
            //logger.LogInformation("Durante la ejecución");
            return Ok(new
            {
                LibrosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                LibrosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                LibrosControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<Computadoras>>> Get()
        {
            //throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de las computadoras");
            logger.LogWarning("Se obtiene el listado de computadoras!");
            service.EjecutarJob();
            return await dbContext.Computadoras.Include(x => x.complementos).ToListAsync();
        }

        [HttpGet("lista/primero")]
        public async Task<ActionResult<Computadoras>> PrimeroEnLista([FromHeader] int valor, [FromQuery] string computadora, [FromQuery] int computadoraId)//Estos from se usaron como prueba *Quitarlos después*
        {
            return await dbContext.Computadoras.Include(x => x.complementos).FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Computadoras>> GetForId(int id)
        {
            var compu = await dbContext.Computadoras.Include(x => x.complementos).FirstOrDefaultAsync(x => x.Id == id);

            if(compu == null)
            {
                logger.LogError("No se encuentra el registro");
                return NotFound();
            }

            logger.LogWarning("Se obtiene la computadora con ID: " + id);
            return compu;
        }

        [HttpGet("{marca}")]
        public async Task<ActionResult<Computadoras>> GetForMarca([FromRoute]string marca)
        {
            return await dbContext.Computadoras.Include(x => x.complementos).FirstOrDefaultAsync(x => x.Marca.Contains(marca));  
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Computadoras computadoras)
        {
            dbContext.Add(computadoras);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Computadoras computadoras, int id)
        {
            if(computadoras.Id != id)
            {
                return BadRequest("El ID de la computadora no coincide con el establecido en la URL");
            }

            dbContext.Update(computadoras);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Computadoras.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("No se encontró el recurso");
            }

            dbContext.Remove(new Computadoras()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
