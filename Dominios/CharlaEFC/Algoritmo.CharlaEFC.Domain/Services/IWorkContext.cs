using shared = Algoritmo.Microservices.Shared.Domain;

namespace Algoritmo.CharlaEFC.Domain.Services
{
    /// <inheritdoc cref="Algoritmo.Microservices.Shared.Domain.Services.IWorkContext"/>
    public interface IWorkContext : Algoritmo.Microservices.Shared.Domain.Services.IWorkContext
    {
        /// <summary>
        /// Servicios de sistema más locales.
        /// </summary>
        public new Services Services { get; protected set; }

        /// <summary>
        /// Configura el contexto local inscribiéndolo en un contexto más amplio.
        /// </summary>
        /// <param name="sharedWorkContext">Instancia creada por DI</param>
        /// <param name="entityManager">Instancia creada por DI</param>
        public void Configure(shared.Services.IWorkContext sharedWorkContext, IEntityManager entityManager);
    }
}
