using Algoritmo.CharlaEFC.Application.BaseClasses.Common;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Queries;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using Algoritmo.Microservices.Shared.Application.Extensions;
using Algoritmo.Microservices.Shared.Domain.Services.Data;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.Jerarquias.Queries
{
    /// <summary>
    /// Clase controladora de la petición GetJerarquiaById
    /// </summary>
    public class GetJerarquiaByIdQueryHandler : BaseQueryHandler<GetJerarquiaByIdQuery, GetJerarquiaByIdResponse>
    {
        /// <inheritdoc cref="BaseQueryHandler.BaseQueryHandler(IWorkContext)"/>
        public GetJerarquiaByIdQueryHandler(IWorkContext context) : base(context) { }

        public override async Task<GetJerarquiaByIdResponse> HandleDelegate(GetJerarquiaByIdQuery query, CancellationToken cancellationToken)
        {
            //se genera un nuevo response ligado al CorrelationID de la query.
            //de esta forma se mantiene una relación entre ambos por ID
            var response = new GetJerarquiaByIdResponse(query.CorrelationId);

            //obtengo la Jerarquía
            var jerarquia = await em.GetJerarquiaByIdAsync(query.Id, GraphExplorerConfiguration.GetDefault());

            //Retorno la jerarquía con el tipo de entidad convertido a DTO
            jerarquia?.SetEntityType(WorkContext.Services.DTOManager.GetMappedType(Type.GetType(jerarquia.TipoEntidadAssembly)).FirstOrDefault());

            var dtoType = WorkContext.Services.DTOManager.GetMappedType(jerarquia?.GetType()).FirstOrDefault();
            response.Jerarquia = (IJerarquiaDTO)jerarquia?.mapMe(dtoType);
            return response;
        }
    }
}





