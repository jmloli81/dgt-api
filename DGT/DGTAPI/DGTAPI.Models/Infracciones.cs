using System;
using System.Collections.Generic;
using System.Text;

namespace DGTAPI.Models
{
    /// <summary>
    /// Clase que permite capturar los tipos de infracciones al sistema
    /// </summary>
    public class Infracciones
    {
        /// <summary>
        /// Identificador de la infracción
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descripción de la infracción
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Puntos a descontar
        /// </summary>
        public int PuntosDesc { get; set; }
    }
}
