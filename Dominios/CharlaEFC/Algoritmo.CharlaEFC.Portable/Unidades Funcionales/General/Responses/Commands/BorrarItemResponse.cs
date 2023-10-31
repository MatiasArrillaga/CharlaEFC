using Algoritmo.CharlaEFC.Portable.BaseClasses;
using System;

namespace Algoritmo.CharlaEFC.Portable.General.Responses.Commands
{
    /// <summary>
    /// Response de <see href="BorrarItemCommand">BorrarItemCommand</see>
    /// </summary>
    /// <returns>
    /// Retorna la jerarquía actualizada
    /// </returns>
    public class BorrarItemResponse : BaseCommandPortableResponse
    {
        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(Guid)"/>
        public BorrarItemResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse"/>
        public BorrarItemResponse() { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(string)"/>
        public BorrarItemResponse(string message) { }

    }
}
