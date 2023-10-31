using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.Microservices.Shared.Application.BaseClasses.General;

namespace Algoritmo.CharlaEFC.Application.General.Query
{
    /// <summary>
    /// Retorna una lista de entidades según el criterio indicado.
    /// </summary>    
    public class GetEntidadStrategy : BaseGetEntidadStrategy
    {
        public GetEntidadStrategy(IWorkContext workContext, GetEntidadStrategyParams execParams) : base(workContext, execParams) { }
    }
}