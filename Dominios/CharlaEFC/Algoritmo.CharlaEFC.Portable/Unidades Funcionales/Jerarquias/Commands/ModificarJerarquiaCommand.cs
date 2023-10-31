using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.Commands
{
    /// <summary>
    /// <b>Caso de Uso:</b> Se podrá modificar una Jerarquía
    /// </summary>
    /// <returns>
    /// <see cref="ModificarJerarquiaResponse"/>: <i>Response con una instancia de la jerarquía modificada</i>
    /// </returns>    
    public class ModificarJerarquiaCommand:BaseCommandPortable<ModificarJerarquiaResponse>
    {
        public ModificarJerarquiaCommand(){ }
        public ModificarJerarquiaCommand(IJerarquiaDTO jerarquia) 
        {
            Jerarquia = jerarquia;
        }
        public IJerarquiaDTO Jerarquia { get; set; } = null!;
    }
}
