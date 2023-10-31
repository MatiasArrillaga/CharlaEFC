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
    /// <b>Caso de Uso:</b> Desjerarquizar una Entidad
    /// </summary>
    /// <returns>
    /// <see cref="DesjerarquizarEntidadResponse"/>: <i>Response con el resultado de la desjerarquización</i>
    /// </returns>
    public class DesjerarquizarEntidadCommand : BaseCommandPortable<DesjerarquizarEntidadResponse>
    {
        public DesjerarquizarEntidadCommand() { }
        public DesjerarquizarEntidadCommand(Guid jerarquiaId, IEnumerable<IEntidadJerarquizableDTO> entidades)
        {
            JerarquiaId = jerarquiaId;
            Entidades = entidades;
        }

        public Guid JerarquiaId { get; set; } = Guid.Empty;
        public IEnumerable<IEntidadJerarquizableDTO> Entidades { get; set; } = null!;
    }
}
