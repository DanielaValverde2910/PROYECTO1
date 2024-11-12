using System;
using System.ComponentModel.DataAnnotations;

namespace Reservas_Viajes.Models
{
    public class Reserva
    {
        public int Id { get; set; } // Identificador único de la reserva

        [Required]
        public int UsuarioId { get; set; } // Identificador del usuario que realizó la reserva
        public Usuario Usuario { get; set; } // Usuario que realizó la reserva

        [Required]
        public int RutaBusId { get; set; } // Identificador de la ruta de bus seleccionada
        public RutaBus RutaBus { get; set; } // Ruta de bus seleccionada

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El número de asiento debe ser válido")]
        public int AsientoSeleccionado { get; set; } // Asiento seleccionado por el usuario

        [Required]
        public bool PagoRealizado { get; set; } // Estado del pago (true si pagado, false si no pagado)

        public DateTime FechaReserva { get; set; } // Fecha en que se realizó la reserva

        public Reserva()
        {
            FechaReserva = DateTime.Now; // Asigna la fecha y hora actual cuando se crea una nueva reserva
        }
    }
}
