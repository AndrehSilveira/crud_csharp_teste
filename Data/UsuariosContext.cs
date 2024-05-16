using Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
  public class UsuariosContext : DbContext
  {
    public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options)
    {
    }

    public DbSet<Usuarios> Usuario { get; set; }
  }
}