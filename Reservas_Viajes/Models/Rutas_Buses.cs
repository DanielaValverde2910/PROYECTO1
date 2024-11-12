using System;
using System.ComponentModel.DataAnnotations;

namespace Reservas_Viajes.Models
{
    public class RutaBus
    {
        public int Id { get; set; } // Identificador único de la ruta

        [Required]
        [StringLength(100)]
        public string Destino { get; set; } // Destino del viaje

        [Required]
        [StringLength(100)]
        public string Origen { get; set; } // Origen del viaje

        [Required]
        public DateTime HorarioSalida { get; set; } // Fecha y hora de salida

        [Required]
        public DateTime HorarioLlegada { get; set; } // Fecha y hora de llegada

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0")]
        public double Precio { get; set; } // Precio del billete

        public int AsientosDisponibles { get; set; } // Número de asientos disponibles en el bus
    }
}
