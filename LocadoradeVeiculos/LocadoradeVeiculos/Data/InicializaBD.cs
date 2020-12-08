using Microsoft.EntityFrameworkCore;
using LocadoradeVeiculos.Models;
using Microsoft.Extensions.DependencyInjection;
using LocadoradeVeiculos.Data;
using System.Linq;
using System;

namespace LocadoradeVeiculos.Data
{
    public class InicializaBD
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LocadoraContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LocadoraContext>>()))
            {
                if (context.Clientes.Any())
                {
                    return;
                }

                context.Clientes.AddRange(
                    new Cliente
                    {
                        NomeCliente = "Sally",
                        CPF = "12345678945"
                    },

                    new Cliente
                    {
                        NomeCliente = "Joao",
                        CPF = "1278978945"
                    }
                );
                context.SaveChanges();

                //public static void geraTabelas(bool excluiBD)

                //{

                //    using (LocadoraContext _contexto = new LocadoraContext())

                //    {

                //        if (excluiBD)

                //            _contexto.Database.EnsureDeleted();


                //        Cliente cliente = new Cliente { NomeCliente = "João", CPF = "123.543.782-54" };

                //        _contexto.Clientes.Add(cliente);

                //        Estoque estoque = new Estoque { Modelo = "Gol", Marca = "Volks", Placa = "asd-1234", AnoModelo = "2020", AnoFabricacao = "2019" };

                //        _contexto.Estoques.Add(estoque);

                //        _contexto.SaveChanges();

            }
        }
    }
}

