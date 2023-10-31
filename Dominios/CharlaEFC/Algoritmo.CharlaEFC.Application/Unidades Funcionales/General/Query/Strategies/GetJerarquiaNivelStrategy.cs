using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.Microservices.Shared.Application.BaseClasses.General;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
using Algoritmo.Microservices.Shared.Domain.Services.Data;
using Algoritmo.Microservices.Shared.Portable.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.General.Query
{
    /// <summary>
    /// Retorna la lista de departamentos.
    /// </summary>
    public class GetJerarquiaNivelStrategy : GetEntidadStrategy
    {
        public GetJerarquiaNivelStrategy(IWorkContext workContext, GetEntidadStrategyParams execParams) : base(workContext, execParams)
        {
        }

        public override async Task<BasePagedList<TInAndReturnItem>> GetPagedAsync<TInAndReturnItem>(params object[] args)
        {
            // Pide el procesador, informa el personalizado y lo ejecuta.
            var processor = GetProcessor<TInAndReturnItem>(args, getAllProcessorAsync: GetAllAsync<TInAndReturnItem>,getByIdProcessorAsync:GetByIdAsync<TInAndReturnItem>, getByCodeProcessorAsync: GetByCodeAsync<TInAndReturnItem>);
            return await processor(args);
        }

        /// <summary>
        /// Especializa la consulta de acceso a la provincia.
        /// </summary>
        /// <typeparam name="TInAndReturnItem"></typeparam>
        /// <param name="args"></param>
        /// <param name="conf"></param>
        /// <returns></returns>
        public async Task<BasePagedList<TInAndReturnItem>> GetAllAsync<TInAndReturnItem>(object[] args, GraphExplorerConfiguration conf = null)
            where TInAndReturnItem : class, IEntity
        {
            // Carga el repositorio específico de la entidad, de esta forma se puede operar la query en forma personalizada.
            var repo = WorkContext.GetRepository<TInAndReturnItem>();
            var query = repo.Entities                            
                            .Select(t => t);

            // Convierte al tipo de retorno admitido.
            return await ConfigureAndExecuteAsync<TInAndReturnItem>(query);
        }        
        
        public async Task<BasePagedList<TInAndReturnItem>> GetByIdAsync<TInAndReturnItem>(object[] args, GraphExplorerConfiguration conf = null)
            where TInAndReturnItem : class, IEntity
        {
            // Carga el repositorio específico de la entidad, de esta forma se puede operar la query en forma personalizada.
            var repo = WorkContext.GetRepository<TInAndReturnItem>();
            var query = repo.Entities
                   .Select(t => t);

            // Convierte al tipo de retorno admitido.
            return await ConfigureAndExecuteAsync<TInAndReturnItem>(query);
        }
        public async Task<BasePagedList<TInAndReturnItem>> GetByCodeAsync<TInAndReturnItem>(object[] args, GraphExplorerConfiguration conf = null)
            where TInAndReturnItem : class, IEntity
        {
            // Carga el repositorio específico de la entidad, de esta forma se puede operar la query en forma personalizada.
            var repo = WorkContext.GetRepository<TInAndReturnItem>();
            var query = repo.Entities
                   .Select(t => t);

            // Convierte al tipo de retorno admitido.
            return await ConfigureAndExecuteAsync<TInAndReturnItem>(query);
        }
    }
}
