using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using System;

namespace Algoritmo.CharlaEFC.Portable.General.Responses.Commands
{
    /// <summary>
    /// Response de <see href="JerarquizarEntidadCommand">JerarquizarEntidadCommand</see>
    /// </summary>
    /// <returns>
    /// <see cref="JerarquiaItemDTO"/>: <i>Response con una instancia del item Jerarquizado</i>
    /// </returns>
    public class JerarquizarEntidadResponse : BaseCommandPortableResponse
    {
        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(Guid)"/>
        public JerarquizarEntidadResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse"/>
        public JerarquizarEntidadResponse() { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(string)"/>
        public JerarquizarEntidadResponse(string message) { }


    }
}
