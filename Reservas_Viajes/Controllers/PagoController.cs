using Microsoft.AspNetCore.Mvc;
using Reservas_Viajes.Data;
using Reservas_Viajes.Models;
using System.Linq;

namespace Reservas_Viajes.Controllers
{
    public class PagoController : Controller
    {
        private readonly Data.ApplicationDbContext _context;

        public PagoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para iniciar el pago
        public IActionResult RealizarPago(int reservaId)
        {
            var reserva = _context.Reservas.FirstOrDefault(r => r.Id == reservaId);
            if (reserva == null)
            {
                return NotFound();
            }

            // Lógica de pago (puede integrar un API de pago aquí)
            reserva.PagoRealizado = true; // Confirmamos que el pago se realizó
            _context.SaveChanges();

            return RedirectToAction("ResumenReserva", "Reserva", new { id = reservaId });
        }
    }
}
