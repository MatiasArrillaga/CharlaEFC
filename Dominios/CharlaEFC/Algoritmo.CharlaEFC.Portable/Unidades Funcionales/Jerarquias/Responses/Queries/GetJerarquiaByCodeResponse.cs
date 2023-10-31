using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using System;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.Responses
{
    /// <summary>
    /// Response de <see href="GetJerarquiaByCodeQuery"/>
    /// </summary>
    /// <returns>
    /// <see cref="JerarquiaBaseDTO"/>: <i>Response con la Jerarquía obtenida según el código indicado</i>
    /// </returns>
    public class GetJerarquiaByCodeResponse : BaseQueryPortableResponse
    {
        /// <inheritdoc cref="BaseQueryPortableResponse.BaseQueryResponse(Guid)"/>
        public GetJerarquiaByCodeResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseQueryPortableResponse.BaseQueryResponse"/>
        public GetJerarquiaByCodeResponse() { }

        /// <inheritdoc cref="BaseQueryPortableResponse.BaseQueryResponse(string)"/>
        public GetJerarquiaByCodeResponse(string message) { }

        public IJerarquiaDTO? Jerarquia { get; set; }

    }
}
