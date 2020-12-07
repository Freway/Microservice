using Microsoft.EntityFrameworkCore;
using LocadoradeVeiculos.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace LocadoradeVeiculos.Data
{
    public class LocadoraContext : DbContext
    {
        public LocadoraContext()
        {
        }

        public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options)
       //public LocadoraContext():base(LocadoraContext)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locacao>()
                .HasOne(p => p.Cliente)
                .WithMany(b => b.Locacaos)                
                .HasForeignKey(p => p.IdCliente);

            modelBuilder.Entity<Locacao>()
                .HasOne(p => p.Estoque)
                .WithMany(b => b.Locacaos)
                .HasForeignKey(p => p.IdEstoque);

        }

    }
}
