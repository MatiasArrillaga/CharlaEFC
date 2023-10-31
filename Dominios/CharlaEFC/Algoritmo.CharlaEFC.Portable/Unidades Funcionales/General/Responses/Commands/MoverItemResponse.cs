using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using System;

namespace Algoritmo.CharlaEFC.Portable.General.Responses.Commands
{
    /// <summary>
    /// Response de <see href="MoverItemCommand">MoverItemCommand</see>
    /// </summary>
    /// <returns>
    /// <see cref="JerarquiaItemDTO"/>: <i>Response con una instancia del item agregado</i>
    /// </returns>
    public class MoverItemResponse : BaseCommandPortableResponse
    {
        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(Guid)"/>
        public MoverItemResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse"/>
        public MoverItemResponse() { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(string)"/>
        public MoverItemResponse(string message) { }

    }
}
