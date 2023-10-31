using localApp = Algoritmo.CharlaEFC.Application.Services;
using localDomain = Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.Microservices.Shared.Application.Configurations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Algoritmo.CharlaEFC.Application.Behaviours;

namespace Algoritmo.CharlaEFC.Application.Configurations

{
    /// <summary> 
    /// Extensión de la clase services collection.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Método Extensión de la clase service collection. Se llama al SharedServiceExtensions.AddApplicationLayer y se le envía el assembly del proyecto actual
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplicationLayer(this IServiceCollection services)
        
        {
            SharedServiceExtensions.AddApplicationLayer(services, Assembly.GetExecutingAssembly());

            // Punto de extensión para incorporar servicios específicos del microservicio, atención al orden que se agregan.
            // ...
            services.AddScoped<localDomain.IWorkContext, localApp.WorkContext>();
            // Configurador del contexto local
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(WorkContextBehaviour<,>));
            // Administrador de entidades
            services.AddScoped<localDomain.IEntityManager, localApp.EntityManager>();
        }
    }
}
