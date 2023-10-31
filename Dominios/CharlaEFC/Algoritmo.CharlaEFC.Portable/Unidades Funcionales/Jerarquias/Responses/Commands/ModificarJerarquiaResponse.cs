using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using System;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.Responses
{
    /// <summary>     
    /// Response de <see href="UpdateJerarquiaCommand">UpdateJerarquiaCommand</see>
    /// </summary>
    /// <returns>     
    /// <see cref="JerarquiaDTO"/>: <i>Response con una instancia del Item modificado</i>
    /// </returns>     
    public class ModificarJerarquiaResponse : BaseCommandPortableResponse
    {
        public ModificarJerarquiaResponse(Guid correlationId) : base(correlationId) { }

        public ModificarJerarquiaResponse() { }

        public ModificarJerarquiaResponse(string message) { }

    }
}
