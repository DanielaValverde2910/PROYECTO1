using Microsoft.AspNetCore.Mvc;
using Reservas_Viajes.Data;
using Reservas_Viajes.Models;
using System.Linq;

namespace Reservas_Viajes.Controllers
{
    public class BusquedaController : Controller
    {
        private readonly Data.ApplicationDbContext _context;

        public BusquedaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar el formulario de búsqueda
        public IActionResult Index()
        {
            return View();
        }

        // Acción para realizar la búsqueda de rutas de buses
        [HttpPost]
        public IActionResult Buscar(string origen, string destino, DateTime fecha)
        {
            var rutasDisponibles = _context.Rutas_Buses
                .Where(r => r.Origen == origen && r.Destino == destino && r.HorarioSalida.Date == fecha.Date)
                .ToList();

            return View("ResultadosBusqueda", rutasDisponibles); // Redirige a la vista con los resultados
        }
    }
}
