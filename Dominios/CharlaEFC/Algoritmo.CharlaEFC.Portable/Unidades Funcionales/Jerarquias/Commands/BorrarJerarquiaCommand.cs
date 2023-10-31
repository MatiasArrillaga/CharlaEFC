using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using Algoritmo.Microservices.Shared.Portable.BaseClasses;
using System;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.Commands
{
    /// <summary>
    /// <b>Caso de Uso:</b> Se podrá eliminar una jerarquía
    /// </summary>
    /// <returns>
    /// <see cref="BorrarJerarquiaResponse"/>
    /// </returns>
    public class BorrarJerarquiaCommand : BaseCommandPortable<BorrarJerarquiaResponse>
    {
        public BorrarJerarquiaCommand(){ }
        public BorrarJerarquiaCommand(Guid id)
        {
            Id = id;
        }
        //[RequiredGreaterThanZero(ErrorMessage = "IDRequired")]
        public Guid Id { get; set; }

    }
}