using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using System;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.Responses
{
    /// <summary>
    /// Response de <see href="GetJerarquiaByIdQuery"/>
    /// </summary>
    /// <returns>
    /// <see cref="JerarquiaBaseDTO"/>: <i>Response con la Jerarquía obtenida según el id indicado</i>
    /// </returns>
    public class GetJerarquiaByIdResponse : BaseQueryPortableResponse
    {
        /// <inheritdoc cref="BaseQueryPortableResponse.BaseQueryResponse(Guid)"/>
        public GetJerarquiaByIdResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseQueryPortableResponse.BaseQueryResponse"/>
        public GetJerarquiaByIdResponse() { }

        /// <inheritdoc cref="BaseQueryPortableResponse.BaseQueryResponse(string)"/>
        public GetJerarquiaByIdResponse(string message) { }

        public IJerarquiaDTO? Jerarquia { get; set; }

    }
}
