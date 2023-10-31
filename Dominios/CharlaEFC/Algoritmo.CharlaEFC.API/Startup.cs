using Algoritmo.Microservices.Shared.API.BaseClasses;
using Algoritmo.CharlaEFC.Application.Configurations;
using Algoritmo.CharlaEFC.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Algoritmo.CharlaEFC.API
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            //Se inyectan las clases del application
            services.AddApplicationLayer();
            //Se inyectan las clases de la infraestructura
            services.AddPersistenceInfrastructure(Configuration);

        }
    }
}
