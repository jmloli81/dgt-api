using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DGTAPI.Context;
using DGTAPI.Models;

namespace DGTAPI.Controllers
{
    [Route("api/registroinfraccion")]
    [ApiController]
    public class RegistroInfraController : ControllerBase
    {
        private readonly DBContext _infraccionesContextR;

        public RegistroInfraController(DBContext infraccionesContext)
        {
            _infraccionesContextR = infraccionesContext;
        }

        // GET: api/registroinfraccion
        [HttpGet("lista vehiculo infraccion")]
        public async Task<ActionResult<IEnumerable<RInfracciones>>> GetAllRegistroInfracciones()
        {
            return await _infraccionesContextR.VehiculoInfraccion.ToListAsync();
        }

        // Post api/registroinfraccion
        [HttpPost("registro de vehiculo infraccionado")]
        public async Task<ActionResult<RInfracciones>> PostRegistroInfracciones(RInfracciones item)
        {
            var conductor = await _infraccionesContextR.Conductor.FindAsync(item.DNI);
            var vehiculo = await _infraccionesContextR.Vehiculo.FindAsync(item.Matricula);
            var infraccion = await _infraccionesContextR.Infracciones.FindAsync(item.TipoInfraccion);

            if (conductor == null)
            {
                var message = string.Format("Conductor DNI = {0} No Existe", item.DNI);
                return NotFound(message);
            }

            if (vehiculo == null)
            {
                var message = string.Format("Vehiculo Matricula = {0} No Existe", item.Matricula);
                return NotFound(message);
            }

            if (infraccion == null)
            {
                var message = string.Format("Tipo de Infraccion = {0} No Existe", item.TipoInfraccion);
                return NotFound(message);
            }

            // Valida si existe la matricula con ese tipo de infraccion
            var existeInfraMatricula = await _infraccionesContextR.VehiculoInfraccion.Where(c => c.TipoInfraccion == item.TipoInfraccion && c.Matricula == item.Matricula).ToListAsync();

            if (existeInfraMatricula.Count > 0)
            {
                var message = string.Format("Existe este tipo de infracción = {0} para la Matricula = {1} No Existe", item.TipoInfraccion, item.Matricula);
                return NotFound(message);
            }

            //Se aplica el descuento al conductor
            conductor.Puntos = conductor.Puntos - infraccion.PuntosDesc;
            _infraccionesContextR.Conductor.Update(conductor);

            _infraccionesContextR.VehiculoInfraccion.Add(item);
            await _infraccionesContextR.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllRegistroInfracciones), new { id = item.DNI }, item);
        }
    }
}
