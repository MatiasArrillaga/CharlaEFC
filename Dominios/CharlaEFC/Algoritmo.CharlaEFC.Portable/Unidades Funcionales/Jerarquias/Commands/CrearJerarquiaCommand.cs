using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using System;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.Commands
{
    /// <summary>
    /// <b>Caso de Uso:</b> Se podrá dar de alta una Jerarquía. A demás, una vez creada la misma, se dará de alta automáticamente su primer árbol jerárquico de nivel 0.<br/>
    /// <i>Una Jerarquía permite definir las condiciones bajo las que se puede crear su respectivo árbol jerarquizado </i>
    /// </summary>
    /// <returns>
    /// <see cref="CrearJerarquiaResponse"/>: <i>Response con una instancia de la nueva jerarquía creada y su árbol</i>
    /// </returns>
    public class CrearJerarquiaCommand: BaseCommandPortable<CrearJerarquiaResponse>
    {
        public CrearJerarquiaCommand(){}
        public CrearJerarquiaCommand(IJerarquiaDTO jerarquia) 
        {
            Jerarquia = jerarquia;
        }
        public IJerarquiaDTO Jerarquia { get; set; } = null!;

    }
}

