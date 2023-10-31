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
    /// <b>Caso de Uso:</b> Agregar un Item a una Jerarquía
    /// </summary>
    /// <returns>
    /// <see cref="AgregarRamaResponse"/>: <i>Response con una instancia del item agregado</i>
    /// </returns>
    public class AgregarRamaCommand : BaseCommandPortable<AgregarRamaResponse>
    {
        public AgregarRamaCommand(string codigo, string nombre, JerarquiaItemDTO itemPadre)
        {
            Codigo = codigo;
            Nombre = nombre;
            ItemPadre = itemPadre;
        }

        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public JerarquiaItemDTO ItemPadre { get; set; } = null!;
    }
}
