using Microsoft.EntityFrameworkCore;
using LocadoradeVeiculos.Models;
using LocadoradeVeiculos.Data;

namespace LocadoradeVeiculos.Data
{
    public class InicializaBD
    {
        public static void geraTabelas(bool excluiBD)

        {

            using (LocadoraContext _contexto = new LocadoraContext())

            {

                if (excluiBD)

                    _contexto.Database.EnsureDeleted();


                Cliente cliente = new Cliente { NomeCliente = "João", CPF = "123.543.782-54" };

                _contexto.Clientes.Add(cliente);

                Estoque estoque = new Estoque { Modelo = "Gol", Marca = "Volks", Placa = "asd-1234", AnoModelo = "2020", AnoFabricacao = "2019" };

                _contexto.Estoques.Add(estoque);

                _contexto.SaveChanges();

            }

        }
    }
}
