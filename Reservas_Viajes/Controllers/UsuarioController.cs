using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Reservas_Viajes.Models;
using System.Linq;
using System.Security.Claims;
using Reservas_Viajes.Data;

namespace Reservas_Viajes.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Data.ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar el formulario de registro
        public IActionResult Registrar()
        {
            return View();
        }

        // Acción para procesar el registro del usuario
        [HttpPost]
        public IActionResult Registrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("IniciarSesion");
            }

            return View(usuario); // Retorna al formulario en caso de error
        }

        // Acción para mostrar el formulario de inicio de sesión
        public IActionResult IniciarSesion()
        {
            return View();
        }

        // Acción para procesar el inicio de sesión
        [HttpPost]
        public IActionResult IniciarSesion(string correo, string contraseña)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.CorreoElectronico == correo && u.Contraseña == contraseña);
            if (usuario != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.Email, usuario.CorreoElectronico)
                };

                var identity = new ClaimsIdentity(claims, "login");
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(principal);

                return RedirectToAction("Perfil");
            }

            ModelState.AddModelError("", "Credenciales incorrectas");
            return View();
        }

        // Acción para mostrar el perfil del usuario
        public IActionResult Perfil()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == usuarioId);
            return View(usuario);
        }

        // Acción para cerrar sesión
        public IActionResult CerrarSesion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("IniciarSesion");
        }
    }
}
