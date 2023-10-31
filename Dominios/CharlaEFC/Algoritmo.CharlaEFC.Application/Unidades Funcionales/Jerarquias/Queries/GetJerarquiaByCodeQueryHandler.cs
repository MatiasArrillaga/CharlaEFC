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
    /// Clase controladora de la petición GetDefinicionOperacionCVTByCode
    /// </summary>
    public class GetJerarquiaByCodeQueryHandler : BaseQueryHandler<GetJerarquiaByCodeQuery, GetJerarquiaByCodeResponse>
    {
        /// <inheritdoc cref="BaseQueryHandler.BaseQueryHandler(IWorkContext)"/>
        public GetJerarquiaByCodeQueryHandler(IWorkContext context) : base(context) { }

        public override async Task<GetJerarquiaByCodeResponse> HandleDelegate(GetJerarquiaByCodeQuery query, CancellationToken cancellationToken)
        {
            //se genera un nuevo response ligado al CorrelationID de la query.
            //de esta forma se mantiene una relación entre ambos por ID
            var response = new GetJerarquiaByCodeResponse(query.CorrelationId);

            //obtengo la jerarquía
            var jerarquia = await em.GetJerarquiaByCodeAsync(query.Codigo,GraphExplorerConfiguration.GetDefault());

            //Retorno la jerarquía con el tipo de entidad convertido a DTO
            jerarquia?.SetEntityType(WorkContext.Services.DTOManager.GetMappedType(Type.GetType(jerarquia.TipoEntidadAssembly)).FirstOrDefault());

            var dtoType = WorkContext.Services.DTOManager.GetMappedType(jerarquia?.GetType()).FirstOrDefault();
            response.Jerarquia = (IJerarquiaDTO)jerarquia?.mapMe(dtoType);
            return response;
        }
    }
}





