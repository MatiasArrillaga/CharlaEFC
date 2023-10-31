using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
using System;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Rules
{
    public class ElTipoDeEntidadDebeCoincidirConElDefinidoEnLaJerarquiaRule : BaseRule
    {
        private Jerarquia _jerarquia;
        private JerarquiaItem _hoja;

        /// <summary>
        /// <b>BusinessRule:</b> El tipo de entidad de la hoja debe coincidir con el definido en la jerarquía del árbol<br/> 
        /// <i>Controla que la entidad que se está asignando a la hoja, sea del mismo tipo que se definió en la jerarquía</i><br/> 
        /// </summary>
        /// <param name="jerarquia">Jerarquía del árbol</param>
        /// <param name="hoja">Hoja del árbol con información de la entidad</param>
        /// <returns><i><b>IsBroken:</b> Retorna verdadero cuando la regla no se cumpla</i></returns>
        public ElTipoDeEntidadDebeCoincidirConElDefinidoEnLaJerarquiaRule(Jerarquia jerarquia, JerarquiaItem hoja)
        {
            _jerarquia = jerarquia;
            _hoja = hoja;
        }
        /// <inheritdoc cref="IBusinessRule.IsBroken"/>
        public override bool IsBroken()
        {
        

            var jerarquiaType = Type.GetType(_jerarquia.TipoEntidadAssembly);
            var entidadHojaType = Type.GetType(_hoja.TipoEntidadAssembly);

            //if (_hoja is null) return false;

            if (entidadHojaType.Equals(jerarquiaType)) return false;

            return !entidadHojaType.IsSubclassOf(jerarquiaType);
        }


    }
}