using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using System;

namespace Algoritmo.CharlaEFC.Portable.General.Responses.Commands
{
    /// <summary>
    /// Response de <see href="ModificarItemCommand">ModificarItemCommand</see>
    /// </summary>
    /// <returns>
    /// <see cref="JerarquiaItemDTO"/>: <i>Response con una instancia del item modificado</i>
    /// </returns>
    public class ModificarItemResponse : BaseCommandPortableResponse
    {
        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(Guid)"/>
        public ModificarItemResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse"/>
        public ModificarItemResponse() { }

        /// <inheritdoc cref="BaseCommandPortableResponse.BaseCommandResponse(string)"/>
        public ModificarItemResponse(string message) { }

    }
}
