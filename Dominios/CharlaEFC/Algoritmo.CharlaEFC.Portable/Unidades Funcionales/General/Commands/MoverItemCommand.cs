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
    /// <b>Caso de Uso:</b> Mover un Item de una rama a la otra
    /// </summary>
    /// <returns>
    /// <see cref="JerarquizarEntidadResponse"/>: <i>Response con una instancia del item movido</i>
    /// </returns>
    public class MoverItemCommand : BaseCommandPortable<MoverItemResponse>
    {
        public MoverItemCommand(Guid idDestino, JerarquiaItemDTO item)
        {
            IdDestino = idDestino;
            Item = item;
        }

        public Guid IdDestino { get; set; }

        public JerarquiaItemDTO Item { get; set; } = null!;
    }
}
