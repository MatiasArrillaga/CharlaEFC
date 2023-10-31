using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.General.Responses.Commands;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Portable.General.Commands
{
    /// <summary>
    /// <b>Caso de Uso:</b> Modificar un Item de una Jerarquía
    /// </summary>
    /// <returns>
    /// <see cref="JerarquizarEntidadResponse"/>: <i>Response con una instancia del item modificado</i>
    /// </returns>
    public class ModificarItemCommand : BaseCommandPortable<ModificarItemResponse>
    {
        public ModificarItemCommand(JerarquiaItemDTO item)
        {
            Item = item;
        }

        public JerarquiaItemDTO Item { get; set; } = null!;
    }
}
