using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DGTAPI.Models;
using DGTAPI.Context;

namespace DGTAPI.Controllers
{
    [Route("api/vehiculo")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly DBContext _vehiculoContext;

        public VehiculoController(DBContext vehiculoContext)
        {
            _vehiculoContext = vehiculoContext;
        }

        // GET: api/<controller>
        [HttpGet("lista de vehiculos")]
        public async Task<ActionResult<IEnumerable<Vehiculos>>> GetAllVehiculo()
        {
            return await _vehiculoContext.Vehiculo.ToListAsync();
        }

        // GET api/vehiculo/5
        [HttpGet("vehiculo especifico {matricula}")]
        public async Task<ActionResult<Vehiculos>> GetIdVehiculo(string matricula)
        {
            var vehiculo = await _vehiculoContext.Vehiculo.FindAsync(matricula);

            if (vehiculo == null)
            {
                var message = string.Format("Matricula = {0} No Existe", matricula);
                return NotFound(message);
            }

            return vehiculo;
        }

        // POST api/vehiculo
        [HttpPost("agrega un vehiculo")]
        public async Task<ActionResult<Vehiculos>> PostVehiculo(Vehiculos item)
        {
            var _thereIsMatricula = await _vehiculoContext.Vehiculo.FindAsync(item.Matricula);

            // Valida si existe la matricula
            if (_thereIsMatricula == null)
            {
                var _thereIsDNI = await _vehiculoContext.Conductor.FindAsync(item.DNI);

                // Valida que exista la DNI del conductor
                if (_thereIsDNI == null)
                {
                    var message = string.Format("Conductor DNI = {0} No Existe", item.DNI);
                    return NotFound(message);
                }

                // Trae la cantidad de vehiculos asignados
                var _countVehiculoConductor = await _vehiculoContext.Vehiculo.Where(c => c.DNI == item.DNI).ToListAsync();

                // Valida que la cantidad de vehiculos asignados no exceda de 10
                if (_countVehiculoConductor.Count < 10)
                {
                    _vehiculoContext.Vehiculo.Add(item);
                    await _vehiculoContext.SaveChangesAsync();
                } else
                {
                    var message = string.Format("Conductor DNI = {0} No debe tener mas de 10 vehiculos asignados", item.DNI);
                    return NotFound(message);
                }

            } else
            {
                var message = string.Format("Matricula = {0} Existe", item.Matricula);
                return NotFound(message);
            }

            return CreatedAtAction(nameof(GetAllVehiculo), new { id = item.Matricula }, item);

        }
    }
}
