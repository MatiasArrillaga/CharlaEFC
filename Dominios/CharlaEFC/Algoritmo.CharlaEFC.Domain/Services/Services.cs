using sharedDomain = Algoritmo.Microservices.Shared.Domain.Services;

namespace Algoritmo.CharlaEFC.Domain.Services
{
    /// <summary>
    /// Clase utilitaria para agrupar los servicios del sistema.
    /// </summary>
    public class Services : sharedDomain.Services
    {
        public new Algoritmo.CharlaEFC.Domain.Services.IEntityManager EntityManager { get; set; }
    }
}
