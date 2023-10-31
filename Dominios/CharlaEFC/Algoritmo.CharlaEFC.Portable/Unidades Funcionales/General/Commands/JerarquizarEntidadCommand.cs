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
    /// <b>Caso de Uso:</b> Jerarquizar una Entidad
    /// </summary>
    /// <returns>
    /// <see cref="JerarquizarEntidadResponse"/>: <i>Response con una instancia del item Jerarquizado</i>
    /// </returns>
    public class JerarquizarEntidadCommand : BaseCommandPortable<JerarquizarEntidadResponse>
    {
        public JerarquizarEntidadCommand() { }
        public JerarquizarEntidadCommand(IEnumerable<IEntidadJerarquizableDTO> entidades, JerarquiaItemDTO itemPadre)
        {
            Entidades = entidades;
            ItemPadre = itemPadre;
        }

        public IEnumerable<IEntidadJerarquizableDTO>? Entidades { get; set; } 
        public JerarquiaItemDTO ItemPadre { get; set; } = null!;
    }
}
