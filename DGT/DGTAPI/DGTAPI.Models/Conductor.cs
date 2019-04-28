using System;
using System.Collections.Generic;
using System.Text;

namespace DGTAPI.Models
{
    /// <summary>
    /// Clase que permite la captura de los datos del conductor
    /// </summary>
    public class Conductor
    {
        /// <summary>
        /// Id-DNI del conductor (identificador del conductor)
        /// </summary>
        public string DNI { get; set; }

        /// <summary>
        /// Nombre(s) del conductor
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Apellidos del conductor
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Puntos cargados al conductor
        /// </summary>
        public int Puntos { get; set; }
    }
}
