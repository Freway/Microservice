using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LocadoradeVeiculos.Data;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;

namespace LocadoradeVeiculos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Locadora de Veiculos API",
                    Version = "v1",
                    Description = "Este é um exemplo de microsserviço baseado em dados/CRUD"
                });
            });

            //services.AddDbContext<LocadoraContext>(options =>
            //{
                //options.UseSqlServer(Configuration["Server=(localdb)\\MSSQLLocalDB;Database=Locadora;Trusted_Connection=True;MultipleActiveResultSets=true;"],
                //sqlServerOptionsAction: sqlOptions =>
                //{
                //    sqlOptions.MigrationsAssembly(
                //        typeof(Startup).GetTypeInfo().Assembly.GetName().Name);

                ////Configurando persistencia de Conexão:
                //    sqlOptions.
                //EnableRetryOnFailure(maxRetryCount: 5,
                //maxRetryDelay: TimeSpan.FromSeconds(30),
                //errorNumbersToAdd: null);

                //});

                // Changing default behavior when client evaluation occurs to throw.
                // Default in EFCore would be to log warning when client evaluation is done.
            //    options.ConfigureWarnings(warnings => warnings.Throw(
            //        RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));
            //});

            services.AddDbContext<LocadoraContext>(opt =>
            opt.UseSqlServer(Configuration.GetConnectionString("Locadora")));
                

            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*,ILoggerFactory loggerFactory*/)
        {
            app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
