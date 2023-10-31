using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Enum;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.Localization;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Rules
{
    public class ControlDatosObligatoriosItemRule : BaseRule
    {
        private readonly JerarquiaItem _item;

        /// <summary>
        /// <b>BusinessRule</b><i> (USER:121566 - FE4, FE5)</i>: ControlDatosObligatoriosRule<br/> 
        /// <i>Controla los siguientes campos obligatorios:</i><br/> 
        ///     + <b>JerarquiaItem</b><br/>
        ///     -- Código<br/>
        ///     -- Nombre<br/>
        ///     -- Nivel<br/>
        ///     -- Jerarquía<br/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns><i><b>IsBroken:</b> Retorna verdadero cuando la regla no se cumpla</i></returns>
        public ControlDatosObligatoriosItemRule(JerarquiaItem item)
        {
            _item = item;
        }
        /// <inheritdoc cref="IBusinessRule.IsBroken"/>
        public override bool IsBroken() 
        {
            if (string.IsNullOrEmpty(_item.Codigo) && _item.Tipo is not TipoJerarquiaItem.TipoHoja)
            { 
                AddErrorMessage(Localizer.GetRecursoAsync("FaltaCodigo").Result); 
            }

            if (string.IsNullOrEmpty(_item.Nombre) && _item.Tipo is not TipoJerarquiaItem.TipoHoja)
            { 
                AddErrorMessage(Localizer.GetRecursoAsync("FaltaNombre").Result); 
            }

            if (_item.Jerarquia is null)
            {
                AddErrorMessage(Localizer.GetRecursoAsync("FaltaJerarquia").Result);
            }

            return HasErrorMessages();
        }
    }
}