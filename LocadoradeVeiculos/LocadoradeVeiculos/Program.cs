using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using LocadoradeVeiculos.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LocadoradeVeiculos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var cliente = new Cliente () { NomeCliente = "João", CPF = "123.543.782-54" };
            
            Console.WriteLine(JsonSerializer.Serialize<Cliente>(cliente));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
