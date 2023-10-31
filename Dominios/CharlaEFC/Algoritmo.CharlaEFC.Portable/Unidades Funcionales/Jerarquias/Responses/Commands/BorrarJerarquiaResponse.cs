using Algoritmo.CharlaEFC.Portable.BaseClasses;
using System;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.Responses
{
    /// <summary>     
    /// Response de <see href="DeleteJerarquiaCommand">DeleteJerarquiaCommand</see> 
    /// </summary>
    public class BorrarJerarquiaResponse : BaseCommandPortableResponse
    {
        public BorrarJerarquiaResponse(Guid correlationId) : base(correlationId) { }

        public BorrarJerarquiaResponse() { }

        public BorrarJerarquiaResponse(string message) { }

    }
}
