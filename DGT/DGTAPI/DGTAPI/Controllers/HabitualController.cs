using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DGTAPI.Context;
using DGTAPI.Models;

namespace DGTAPI.Controllers
{
    [Route("api/habitual")]
    public class HabitualController : ControllerBase
    {
        private readonly DBContext _infraccionesHabitual;

        public HabitualController(DBContext infraccionesHabitual)
        {
            _infraccionesHabitual = infraccionesHabitual;
        }

        //// GET: api/infraccionHabitual/6/
        [HttpGet("infracciones")]
        //public async Task<ActionResult<List<IGrouping<int, RInfracciones>>>> GetHabitual()
        public async Task<ActionResult<IEnumerable<object>>> InfraccionHabitual()
        {
            var habitual = await _infraccionesHabitual.VehiculoInfraccion.GroupBy(x => x.TipoInfraccion).Select(c => new { TipoInfraccion = c.Key, Count = c.Count() }).OrderBy(i => i.Count).Take(5).ToListAsync();
            List<object> list = new List<object>();

            foreach(var item in habitual)
            {
                var TipoInfraccion = _infraccionesHabitual.Infracciones.FindAsync(item.TipoInfraccion);
                list.Add(new {TipoInfraccion.Result.Descripcion, item.Count});                
            }
            return list;
        }
    }
}
