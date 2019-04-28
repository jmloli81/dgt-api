using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DGTAPI.Context;
using DGTAPI.Models;

namespace DGTAPI.Controllers
{
    [Route("api/conductor")]
    [ApiController]
    public class ConductorController : ControllerBase
    {
        private readonly DBContext _conductorContext;

        public ConductorController(DBContext conductorContext)
        {
            _conductorContext = conductorContext;
        }

        // GET: api/conductor
        // Devuelve la lista de conductores registrados
        [HttpGet("lista total de conductores")]
        public async Task<ActionResult<IEnumerable<Conductor>>> GetAllCondutor()
        {
            var listConductor = await _conductorContext.Conductor.Include(x => x.Vehiculos).ToListAsync();
            return listConductor;
        }

        //// GET api/conductorinfracciones/5
        //// Devuelve lista de infracciones de un conductor en especifico por el DNI
        //[HttpGet("infracciones del conductor {dni}")]
        //public async Task<ActionResult<IEnumerable<RInfracciones>>> GetIdConductor(string dni)
        //{
        //    var conductor = await _conductorContext.VehiculoInfraccion.Where(i => i.DNI == dni).ToListAsync();
        //    if (conductor == null) 
        //    {
        //        var message = string.Format("Conductor DNI = {0} No Existe", dni);
        //        return NotFound(message);
        //    }
        //    return conductor;
        //}

        // GET api/conductorinfracciones/5
        // Devuelve lista de infracciones de un conductor en especifico por el DNI
        [HttpGet("infracciones del conductor {dni}")]
        public async Task<ActionResult<IEnumerable<object>>> GetIdConductor(string dni)
        {
            var conductor = await _conductorContext.VehiculoInfraccion.Where(i => i.DNI == dni).ToListAsync();

            if (conductor.Count == 0)
            {
                var message = string.Format("Conductor DNI = {0} No Existe", dni);
                return NotFound(message);
            }

            List<object> list = new List<object>();

            foreach(var item in conductor)
            {
                var tipoInfraccion = await _conductorContext.Infracciones.Where(x => x.Id == item.TipoInfraccion).Select(x => new { x.Descripcion }).FirstOrDefaultAsync();
                list.Add(new {item.DNI, item.Matricula, tipoInfraccion, item.FechaHora});
            }
            
            return list;
        }

        ////// GET: api/infraccionHabitual/6/
        //[HttpGet("infracciones")]
        ////public async Task<ActionResult<List<IGrouping<int, RInfracciones>>>> GetHabitual()
        //public async Task<ActionResult<IEnumerable<object>>> InfraccionHabitual()
        //{
        //    var habitual = await _infraccionesHabitual.VehiculoInfraccion.GroupBy(x => x.TipoInfraccion).Select(c => new { TipoInfraccion = c.Key, Count = c.Count() }).OrderBy(i => i.Count).Take(5).ToListAsync();
        //    List<object> list = new List<object>();

        //    foreach (var item in habitual)
        //    {
        //        var TipoInfraccion = _infraccionesHabitual.Infracciones.FindAsync(item.TipoInfraccion);
        //        list.Add(new { TipoInfraccion.Result.Descripcion, item.Count });
        //    }
        //    return list;
        //}

        // POST api/conductor
        // Ingresa un nuevo conductor
        [HttpPost("agrega un conductor")]
        public async Task<ActionResult<Conductor>> PostConductor(Conductor item)
        {
            var _thereIsDNI = await _conductorContext.Conductor.FindAsync(item.DNI);

            // Valida si existe el conductor
            if (_thereIsDNI == null)
            {
                // Si no viene la clave del chofer se agrega a la relacion conductor - vehiculo
                if (!item.Vehiculos[0].DNI.Equals(item.DNI))
                {
                    item.Vehiculos[0].DNI = item.DNI;
                }

                var _thereIsMatricula = await _conductorContext.Vehiculo.FindAsync(item.Vehiculos[0].Matricula);

                // Valida si existe la matricula
                if (_thereIsMatricula != null)
                {
                    var message = string.Format("Matricula = {0} Existe", item.Vehiculos[0].Matricula);
                    return NotFound(message);
                }

                _conductorContext.Conductor.Add(item);
                await _conductorContext.SaveChangesAsync();
            } else
            {
                var message = string.Format("Conductor DNI = {0} Existe", item.DNI);
                return NotFound(message);
            }

            return CreatedAtAction(nameof(GetAllCondutor), new { id = item.DNI }, item);
        }
    }
}
