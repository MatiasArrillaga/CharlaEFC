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
    /// <b>Caso de Uso:</b> Borrar un item de la Jerarquía
    /// </summary>
    /// <returns>
    /// <see cref="JerarquizarEntidadResponse"/>: <i>Response con una instancia del item agregado</i>
    /// </returns>
    public class BorrarItemCommand : BaseCommandPortable<BorrarItemResponse>
    {
        public BorrarItemCommand(JerarquiaItemDTO item)
        {
            Item = item;
        }

        public JerarquiaItemDTO Item { get; set; } = null!;
    }
}
