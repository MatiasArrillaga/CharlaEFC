using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using System.Linq;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Rules
{
    public class NoCambiarTipoDeJerarquiaSiYaTengoHojasRule : BaseRule
    {
        private Jerarquia _jerarquia;
        private Jerarquia _jerarquiaOld;

        /// <summary>
        /// <b>BusinessRule</b>: 
        /// No se podrá cambiar el tipo de jerarquía item si el árbol ya tiene hojas.<br/> 
        /// </summary>
        /// <param name="jerarquia"/>
        /// <returns><i><b>IsBroken:</b> Retorna verdadero cuando la jerarquía cambió de tipo de entidad hoja y ya tiene hojas asignadas  </i></returns>
        public NoCambiarTipoDeJerarquiaSiYaTengoHojasRule(Jerarquia jerarquiaOld,Jerarquia jerarquia)
        {
            _jerarquiaOld = jerarquiaOld;
            _jerarquia = jerarquia;
        }
        /// <inheritdoc cref="IBusinessRule.IsBroken"/>
        public override bool IsBroken() 
        {
            if (!_jerarquia.Hojas.Any())
                return false;

            return _jerarquiaOld.TipoEntidadFullName != _jerarquia.TipoEntidadFullName;
        }      

    }
}