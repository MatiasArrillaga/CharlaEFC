using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.Microservices.Shared.Portable.Jerarquias;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using System;
using System.Collections.Generic;

namespace Algoritmo.CharlaEFC.Portable.General.Responses
{
    /// <summary>
    /// Response de <see href="GetJerarquiaByIdQuery"/>
    /// </summary>
    /// <returns>
    /// <see cref="JerarquiaBaseDTO"/>: <i>Response con la Jerarquía obtenida según el id indicado</i>
    /// </returns>
    public class GetJerarquiaMinimizadaResponse : BaseQueryPortableResponse
    {
        /// <inheritdoc cref="BaseQueryPortableResponse.BaseQueryResponse(Guid)"/>
        public GetJerarquiaMinimizadaResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseQueryPortableResponse.BaseQueryResponse"/>
        public GetJerarquiaMinimizadaResponse() { }

        /// <inheritdoc cref="BaseQueryPortableResponse.BaseQueryResponse(string)"/>
        public GetJerarquiaMinimizadaResponse(string message) { }

        public JerarquiaItemMinDTO? Jerarquia { get; set; } = null!;
        public List<IJerarquiaItemDTO> Detalle { get; set; } = null!;
        
    }
}
