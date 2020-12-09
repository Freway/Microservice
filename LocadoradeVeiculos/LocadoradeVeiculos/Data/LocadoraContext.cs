using Microsoft.EntityFrameworkCore;
using LocadoradeVeiculos.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace LocadoradeVeiculos.Data
{
    public class LocadoraContext : DbContext
    {
        public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options)
        {
            //Cria a base de dados e suas respectivas tabelas se não existir
            Database.EnsureCreated();
            //Exclui a base de dados se existir
            //Database.EnsureDeleted();
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<Log> Log { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locacao>()
                .HasOne(p => p.Cliente)
                .WithMany(b => b.Locacaos)                
                .HasForeignKey(p => p.CPF);

            modelBuilder.Entity<Locacao>()
                .HasOne(p => p.Estoque)
                .WithMany(b => b.Locacaos)
                .HasForeignKey(p => p.IdEstoque);            

        }

    }
}
