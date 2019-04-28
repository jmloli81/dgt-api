using System;
using System.Collections.Generic;
using System.Text;

namespace DGTAPI.Models
{
    /// <summary>
    /// Clase que permite la captura de los datos del vehiculo
    /// </summary>
    public class Vehiculos
    {
        /// <summary>
        /// Id-Matrícula del Vehículo
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// Marca del Vehículo
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Modelo del Vehículo
        /// </summary>
        public int Modelo { get; set; }

        /// <summary>
        /// Id-DNI conductor del Vehículo
        /// </summary>
        public string DNI { get; set; }
    }
}
