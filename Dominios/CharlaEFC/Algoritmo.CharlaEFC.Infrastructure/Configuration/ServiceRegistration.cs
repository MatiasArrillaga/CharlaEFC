using Algoritmo.CharlaEFC.Domain.Jerarquias.Interfaces;
using Algoritmo.CharlaEFC.Infrastructure.Databases;
using Algoritmo.CharlaEFC.Infrastructure.Jerarquias.Repositories;
using Algoritmo.Microservices.Shared.Domain.Infrastructure.Interfaces;
using Algoritmo.Microservices.Shared.Infrastructure.Services.Repositories;
using Algoritmo.Microservices.Shared.Infrastructure.Services.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Algoritmo.CharlaEFC.Infrastructure.Configuration
{
    /// <summary>
    /// Extensión de la clase services collection.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Método Extensión de la clase service collection.
        /// Se mapea el DBContext, la unidad de trabajo y todos los repositorios
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Agrego la colección como servicio para poder explorarla en otros puntos del flujo.
            services.AddSingleton<IServiceCollection>(services);

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                //services.AddDbContext<CharlaEFCDbContext>(options =>
                //    options.UseInMemoryDatabase("CharlaEFCDb"));
            }
            else
            {
                // Cada contexto debe configurarse con resiliencia y SIN indicar el string de conexión ya que la misma se provee de forma externa.
                services.AddDbContext<CharlaEFCDbContext>(options => options.UseSqlServer(sqlOptions =>
                {
                    // Resiliencia de la conexión
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    sqlOptions.MigrationsAssembly(typeof(CharlaEFCDbContext).Assembly.FullName);
                }));
            }

            services.AddScoped<IUnitOfWork, Transactional>();

            #region Repositorios
            // Deben registrarse los tipos genéricos y los específicos para que puedan ser resueltos por el UnitOfWorkManager.
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<,>));




            #region Jerarquías
            services.AddTransient<IJerarquiaRepositoryAsync, JerarquiaRepositoryAsync>();
            services.AddTransient<IJerarquiaRepositoryAsync, JerarquiaReadOnlyRepositoryAsync>();
            #endregion

            #endregion

        }
    }
}
