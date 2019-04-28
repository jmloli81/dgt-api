using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGTAPI.Models;

namespace DGTAPI.Models
{
    /// <summary>
    /// Clase que permite el registro de infracciones 
    /// </summary>
    public class RInfracciones
    {
        /// <summary>
        /// Identificador de la infracción
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha del registro de la Infracción
        /// </summary>
        public DateTime FechaHora { get; set; }

        /// <summary>
        /// Conductor
        /// </summary>
        public string DNI { get; set; }

        /// <summary>
        /// Vehículo
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// Tipo Infraccion
        /// </summary>
        public int TipoInfraccion { get; set; }
    }
}
