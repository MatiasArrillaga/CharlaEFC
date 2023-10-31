using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Services;
using System.Linq;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Rules
{ 
    public class UnaJerarquiaNivelEsUnicaParaElNivelJerarquicoRule : BaseRule
    {
        private Jerarquia _jerarquia;
        private JerarquiaNivel _nivel;
        /// <summary>
        /// <b>BusinessRule:</b> No pueden existir mas de un nivel de Jerarquía para un mismo nivel de ramas<br/>  
        /// </summary>
        /// <returns>
        /// <i><b>IsBroken:</b> Retorna verdadero cuando un item hoja no tiene un padre </i>
        /// </returns>
        public UnaJerarquiaNivelEsUnicaParaElNivelJerarquicoRule(Jerarquia jerarquia, JerarquiaNivel nivel)
        {
            _jerarquia = jerarquia;
            _nivel = nivel;
        }

        /// <inheritdoc cref="IBusinessRule.IsBroken"/>
        public override bool IsBroken() =>_jerarquia.Niveles.Count(n=> n.Nivel == _nivel.Nivel)>1; 

    }
}