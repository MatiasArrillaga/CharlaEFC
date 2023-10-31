using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.Localization;
using Algoritmo.Microservices.Shared.Portable.Enums.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Rules
{

    public class ControlDatosObligatoriosRule : BaseRule
    {
        private readonly Jerarquia _jerarquia;

        /// <summary>
        /// <b>BusinessRule</b><i> (USER:121566 - FE4, FE5)</i>: ControlDatosObligatoriosRule<br/> 
        /// <i>Controla los siguientes campos obligatorios:</i><br/> 
        ///     + <b>Jerarquía</b><br/>
        ///     -- Código<br/>
        ///     -- Nombre<br/>
        ///     -- TipoEntidadHoja<br/>
        ///     -- Árbol<br/>
        ///     + <b>JerarquíaItem</b><br/>
        ///     -- Código<br/>
        ///     -- Nombre<br/>
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns><i><b>IsBroken:</b> Retorna verdadero cuando la regla no se cumpla</i></returns>
        public ControlDatosObligatoriosRule(Jerarquia jerarquia)
        {
            _jerarquia = jerarquia;
        }
        /// <inheritdoc cref="IBusinessRule.IsBroken"/>
        public override bool IsBroken() => RuleIsBroken();

        private bool RuleIsBroken()
        {
            if (string.IsNullOrEmpty(_jerarquia.Codigo))
            { AddErrorMessage(Localizer.GetRecursoAsync("FaltaCodigo").Result); }

            if (string.IsNullOrEmpty(_jerarquia.Nombre))
            { AddErrorMessage(Localizer.GetRecursoAsync("FaltaNombre").Result); }

            if (string.IsNullOrEmpty(_jerarquia.TipoEntidadFullName))
            { AddErrorMessage(Localizer.GetRecursoAsync("FaltaTipoEntidadHoja").Result); } 
                       
            return HasErrorMessages();
        }      
    }


}

