using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Reservas_Viajes.Data;
using Reservas_Viajes.Models;

namespace ReservasViajes.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para ver los detalles de la ruta y seleccionar un asiento
        public IActionResult DetallesRuta(int id)
        {
            var rutaBus = _context.Rutas_Buses
                                  .FirstOrDefault(r => r.Id == id);

            if (rutaBus == null)
            {
                return NotFound();
            }

            return View(rutaBus);
        }

        // Acción para seleccionar un asiento en una ruta y proceder con la reserva
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SeleccionarAsiento(int rutaBusId, int asientoSeleccionado)
        {
            // Verificar que el asiento seleccionado sea válido
            var rutaBus = _context.Rutas_Buses.FirstOrDefault(r => r.Id == rutaBusId);
            if (rutaBus == null || asientoSeleccionado < 1 || asientoSeleccionado > rutaBus.AsientosDisponibles)
            {
                return RedirectToAction("DetallesRuta", new { id = rutaBusId });
            }

            // Crear una nueva reserva para el usuario
            var reserva = new Reserva
            {
                UsuarioId = 1, // ID de ejemplo, debe ser el ID del usuario autenticado
                RutaBusId = rutaBusId,
                AsientoSeleccionado = asientoSeleccionado,
                PagoRealizado = false, // Inicialmente, el pago no se ha realizado
                FechaReserva = DateTime.Now
            };

            _context.Reservas.Add(reserva);
            _context.SaveChanges();

            return RedirectToAction("FormularioReserva", new { id = reserva.Id });
        }

        // Acción para mostrar el formulario de pago
        public IActionResult FormularioReserva(int id)
        {
            var reserva = _context.Reservas
                                   .Include(r => r.RutaBus)
                                   .FirstOrDefault(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // Acción para procesar el pago
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcesarPago(int reservaId, string metodoPago)
        {
            var reserva = _context.Reservas
                                   .FirstOrDefault(r => r.Id == reservaId);

            if (reserva == null)
            {
                return NotFound();
            }

            // Simulación de procesamiento de pago
            if (metodoPago == "Tarjeta" || metodoPago == "PayPal")
            {
                reserva.PagoRealizado = true; // Marcar como pagado
                _context.SaveChanges();
                return RedirectToAction("ConfirmacionReserva", new { id = reserva.Id });
            }

            // Si el pago no se realiza correctamente, redirigir nuevamente al formulario
            return RedirectToAction("FormularioReserva", new { id = reserva.Id });
        }

        // Acción para mostrar la confirmación de la reserva
        public IActionResult ConfirmacionReserva(int id)
        {
            var reserva = _context.Reservas
                                   .Include(r => r.RutaBus)
                                   .FirstOrDefault(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }
    }
}
