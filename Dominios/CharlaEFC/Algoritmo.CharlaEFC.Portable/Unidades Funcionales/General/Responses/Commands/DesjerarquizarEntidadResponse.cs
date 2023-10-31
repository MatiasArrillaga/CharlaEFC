using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using System;

namespace Algoritmo.CharlaEFC.Portable.General.Responses.Commands
{
    /// <summary>
    /// Response de <see href="DeserarquizarEntidadCommand">JerarquizarEntidadCommand</see>
    /// </summary>
    /// <returns>
    /// <see cref="JerarquiaItemDTO"/>: <i>Response con una instancia del item Jerarquizado</i>
    /// </returns>
    public class DesjerarquizarEntidadResponse : BaseCommandPortableResponse
    {
        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(Guid)"/>
        public DesjerarquizarEntidadResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse"/>
        public DesjerarquizarEntidadResponse() { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(string)"/>
        public DesjerarquizarEntidadResponse(string message) { }


    }
}
