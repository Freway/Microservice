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

            }
        }
    }
}

