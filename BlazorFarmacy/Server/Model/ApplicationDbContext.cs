using BlazorFarmacy.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;


namespace BlazorFarmacy.Server.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Producto> Productos { get; set; }

    }
}
