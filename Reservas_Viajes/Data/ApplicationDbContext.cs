using Microsoft.EntityFrameworkCore;
using Reservas_Viajes.Models;
using System.Collections.Generic;

namespace Reservas_Viajes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Definimos las tablas de la base de datos
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RutaBus> Rutas_Buses { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}
