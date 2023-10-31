using System;
using Algoritmo.Microservices.Shared.Portable.BaseClasses;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses
{
    /// <summary>
    /// Clase Base del tipo CommandResponse, propia del microservicio. Hereda de BaseCommandResponse general.
    /// Se utiliza para enviar la respuesta de un Command
    /// </summary>
    public abstract class BaseCommandPortableResponse : BaseCommandResponse
    {
        /// <inheritdoc cref="BaseCommandResponse.BaseCommandResponse(Guid)"/>
        public BaseCommandPortableResponse(Guid correlationId) : base(correlationId) { }

        /// <inheritdoc cref="BaseCommandResponse.BaseCommandResponse"/>
        public BaseCommandPortableResponse() { }

        /// <inheritdoc cref="BaseCommandResponse.BaseCommandResponse(string)"/>
        public BaseCommandPortableResponse(string message) { }
    }
}
