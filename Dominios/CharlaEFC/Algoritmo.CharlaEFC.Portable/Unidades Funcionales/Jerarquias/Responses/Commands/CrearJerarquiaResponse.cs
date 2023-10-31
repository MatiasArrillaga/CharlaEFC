using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using System;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.Responses
{
    /// <summary>
    /// Response de <see href="CreateJerarquiaCommand">CreateJerarquiaCommand</see>
    /// </summary>
    /// <returns>
    /// <see cref="JerarquiaBaseDTO"/>: <i> Response con una instancia de la nueva jerarquía creada</i><br/>
    /// </returns>

    public class CrearJerarquiaResponse : BaseCommandPortableResponse
    {
        public CrearJerarquiaResponse(Guid correlationId) : base(correlationId) { }

        public CrearJerarquiaResponse() { }

        public CrearJerarquiaResponse(string message) { }
        public IJerarquiaDTO Jerarquia { get; set; }

    }
}
