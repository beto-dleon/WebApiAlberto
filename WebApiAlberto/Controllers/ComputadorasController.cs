using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAlberto.Entitys;

namespace WebApiAlberto.Controllers
{
    [ApiController]
    [Route("api/computadoras")]
    public class ComputadorasController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        public ComputadorasController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Computadoras>>> Get()
        {
            return await dbContext.Computadoras.Include(x => x.complementos).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Computadoras computadoras)
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
