using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DGTAPI.Context;
using DGTAPI.Models;

namespace DGTAPI.Controllers
{
    [Route("api/infraccion")]
    [ApiController]
    public class InfraccionesController : ControllerBase
    {
        private readonly DBContext _infraccionesContext;

        public InfraccionesController(DBContext infraccionesContext)
        {
            _infraccionesContext = infraccionesContext;
        }

        // GET: api/infraccion
        [HttpGet("lista tipo de infracciones")]
        public async Task<ActionResult<IEnumerable<TInfracciones>>> GetAllInfracciones()
        {
            return await _infraccionesContext.Infracciones.ToListAsync();
        }

        // GET api/infraccion/5
        [HttpGet("especifico {id}")]
        public async Task<ActionResult<TInfracciones>> GetIdInfracciones(int id)
        {
            var infracciones = await _infraccionesContext.Infracciones.FindAsync(id);

            if (infracciones == null)
            {
                return NotFound();
            }

            return infracciones;
        }

        // POST api/infraccion
        [HttpPost("agrega un tipo de infraccion")]
        public async Task<ActionResult<TInfracciones>> PostInfracciones(TInfracciones item)
        {
            _infraccionesContext.Infracciones.Add(item);
            await _infraccionesContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllInfracciones), new { id = item.Id }, item);
        }
    }
}
