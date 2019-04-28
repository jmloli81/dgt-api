using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGTAPI.Models;

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
        /// Conductor del vehiculo
        /// </summary>
        public string DNI { get; set; }

    }
}
