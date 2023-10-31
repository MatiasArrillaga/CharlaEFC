using sharedDomain = Algoritmo.Microservices.Shared.Domain.Services;

namespace Algoritmo.CharlaEFC.Domain.Services
{
    /// <summary>
    /// Clase base para estrategias.
    /// </summary>
    public class BehaviourAdapterStrategy : sharedDomain.BehaviourAdapterStrategy
    {
        public new IWorkContext WorkContext { get; set; }
        public IEntityManager em { get; set; }
        public BehaviourAdapterStrategy(IWorkContext workContext) : base(workContext)
        {
            WorkContext = workContext;
            em = workContext.Services.EntityManager;
        }
    }
}
