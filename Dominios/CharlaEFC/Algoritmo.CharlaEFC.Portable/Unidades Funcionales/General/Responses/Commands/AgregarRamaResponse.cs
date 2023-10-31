using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using System;

namespace Algoritmo.CharlaEFC.Portable.General.Responses.Commands
{
    /// <summary>
    /// Response de <see href="AgregarRamaCommand">AgregarRamaCommand</see>
    /// </summary>
    /// <returns>
    /// <see cref="JerarquiaItemDTO"/>: <i>Response con una instancia del item agregado</i>
    /// </returns>
    public class AgregarRamaResponse : BaseCommandPortableResponse
    {
        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(Guid)"/>
        public AgregarRamaResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse"/>
        public AgregarRamaResponse() { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(string)"/>
        public AgregarRamaResponse(string message) { }


    }
}
