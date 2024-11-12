using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reservas_Viajes.Models
{
    public class Usuario
    {
        public int Id { get; set; } // Identificador único del usuario

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } // Nombre completo del usuario

        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; } // Correo electrónico del usuario

        [Required]
        [StringLength(100)]
        public string Contraseña { get; set; } // Contraseña del usuario

        [Phone]
        public string Telefono { get; set; } // Número de teléfono del usuario

        public List<Reserva> Reservas { get; set; } // Lista de reservas realizadas por el usuario
    }
}
