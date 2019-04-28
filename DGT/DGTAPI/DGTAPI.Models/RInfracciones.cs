using System;
using System.Collections.Generic;
using System.Text;

namespace DGTAPI.Models
{
    /// <summary>
    /// Clase que permite el registro de infracciones 
    /// </summary>
    public class RInfracciones
    {
        /// <summary>
        /// Fecha del registro de la Infracción
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Hora del registro de la Infracción
        /// </summary>
        public DateTime Hora { get; set; }

        /// <summary>
        /// Identificador del conductor
        /// </summary>
        public string DNI { get; set; }

        /// <summary>
        /// Matrícula del vehículo
        /// </summary>
        public string Matricula { get; set; }
    }
}
