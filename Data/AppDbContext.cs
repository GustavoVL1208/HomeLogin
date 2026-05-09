using Microsoft.EntityFrameworkCore;
using ViziLogin.Models;

namespace ViziLogin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Servico> Servicos { get; set; }

        public DbSet<Avaliacao> Avaliacao { get; set; }
    }
}