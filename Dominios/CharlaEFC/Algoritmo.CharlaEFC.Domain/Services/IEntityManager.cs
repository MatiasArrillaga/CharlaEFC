using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Services.Data;
using System.Threading.Tasks;
using shared = Algoritmo.Microservices.Shared.Domain;

namespace Algoritmo.CharlaEFC.Domain.Services
{
    /// <summary>
    /// Servicio para administración general de entidades.
    /// </summary>
    public interface IEntityManager : shared.Services.IEntityManager
    {
        /// <summary>
        /// Configura el manager para el contexto indicado.
        /// </summary>
        /// <param name="workContext">Contexto local de operación</param>
        void Configure(IWorkContext workContext);

        /// <summary>
        /// Retorna la jerarquía correspondiente al id indicado ya configurada.<br/>
        /// <i>Internamente se configura la jerarquía, 
        /// se configuran sus hojas, 
        /// y se cargan las entidades si es que tiene </i>
        /// </summary>
        /// <param name="id"></param>
        public Task<IJerarquia?> GetJerarquiaByIdAsync(object id, GraphExplorerConfiguration? graphExplorer = null);

        /// <summary>
        /// Retorna la jerarquía correspondiente al código indicado ya configurada.        
        /// <i>Internamente se configura la jerarquía, 
        /// se configuran sus hojas, 
        /// y se cargan las entidades si es que tiene </i>
        /// </summary>
        public Task<IJerarquia?> GetJerarquiaByCodeAsync(string code, GraphExplorerConfiguration? graphExplorer = null);

        /// <summary>
        /// Retorna la entidad vinculada al ítem de jerarquía.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public Task<IEntidadJerarquizable?> GetEntidadAsync(IJerarquiaItem jerarquiaItem);
    }
}
