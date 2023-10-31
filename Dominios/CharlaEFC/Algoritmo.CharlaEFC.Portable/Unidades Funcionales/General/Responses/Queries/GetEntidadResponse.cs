using Algoritmo.CharlaEFC.Portable.BaseClasses;
using System;
using System.Collections.Generic;

namespace Algoritmo.CharlaEFC.Portable.General.Responses
{
    /// <summary>
    /// Response de <see href="GetEntidadResponse"/>
    /// </summary>
    /// <returns>
    /// [Completar]
    /// </returns>
    public class GetEntidadResponse : BasePagedQueryPortableResponse<object>
    {
        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(Guid)"/>
        public GetEntidadResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse"/>
        public GetEntidadResponse() { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(string)"/>
        public GetEntidadResponse(string message) { }
    }
}
