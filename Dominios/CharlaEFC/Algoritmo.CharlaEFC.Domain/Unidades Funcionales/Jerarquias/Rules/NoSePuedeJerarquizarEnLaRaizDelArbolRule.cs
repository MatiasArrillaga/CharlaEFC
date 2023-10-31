using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Enum;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Services;
using System.Linq;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Rules
{ 
    public class NoSePuedeJerarquizarEnLaRaizDelArbolRule : BaseRule
    {
        private JerarquiaItem _jerarquiaItem;
        /// <summary>
        /// <b>BusinessRule:</b> No se puede jerarquizar una entidad en la raiz del arbol<br/>  
        /// </summary>
        /// <returns>
        /// <i><b>IsBroken:</b> Retorna verdadero cuando un item hoja no tiene un padre </i>
        /// </returns>
        public NoSePuedeJerarquizarEnLaRaizDelArbolRule(JerarquiaItem jerarquiaItem)
        {
            _jerarquiaItem = jerarquiaItem;
        }

        /// <inheritdoc cref="IBusinessRule.IsBroken"/>
        public override bool IsBroken() 
        {
            return _jerarquiaItem.Padre.Tipo is TipoJerarquiaItem.TipoRaiz; 
        }
    }
}