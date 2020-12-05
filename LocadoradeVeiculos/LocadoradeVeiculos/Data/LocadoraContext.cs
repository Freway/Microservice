using Microsoft.EntityFrameworkCore;
using LocadoradeVeiculos.Models;

namespace LocadoradeVeiculos.Data
{
    public class LocadoraContext: DbContext
    {
        public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options) 
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

    }
}
