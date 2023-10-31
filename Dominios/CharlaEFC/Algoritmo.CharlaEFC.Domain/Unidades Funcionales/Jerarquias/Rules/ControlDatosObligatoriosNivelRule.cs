using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.Localization;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Rules
{
    public class ControlDatosObligatoriosNivelRule : BaseRule
    {
        private readonly JerarquiaNivel _nivel;

        /// <summary>
        /// <b>BusinessRule</b><i>: ControlDatosObligatoriosNivelRule<br/> 
        /// <i>Controla los siguientes campos obligatorios:</i><br/> 
        ///     + <b>JerarquiaNivel</b><br/>
        ///     -- Nivel<br/>
        ///     -- Nombre<br/>
        ///     -- Jerarquía<br/>
        /// </summary>
        /// <param name="niveles"></param>
        /// <returns><i><b>IsBroken:</b> Retorna verdadero cuando la regla no se cumpla</i></returns>
        public ControlDatosObligatoriosNivelRule(JerarquiaNivel nivel)
        {
            _nivel = nivel;
        }
        /// <inheritdoc cref="IBusinessRule.IsBroken"/>
        public override bool IsBroken() 
        {
            if (string.IsNullOrEmpty(_nivel.Nombre))
            { 
                AddErrorMessage(Localizer.GetRecursoAsync("FaltaNombre").Result); 
            }
            if (_nivel.Nivel == -1)
            { 
                AddErrorMessage(Localizer.GetRecursoAsync("FaltaNivel").Result); 
            }

            if (_nivel.Jerarquia is null)
            {
                AddErrorMessage(Localizer.GetRecursoAsync("FaltaJerarquia").Result);
            }

            return HasErrorMessages();
        }
    }
}