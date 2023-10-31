using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Rules
{
    public class LaEntidadEsUnicaEnTodoElArbolRule : BaseRule
    {
        private List<JerarquiaItem> _hojas;
        private IEntity _entidadHoja;

        /// <summary>
        /// <b>BusinessRule:</b> Una entidad debe pertenecer a una única hoja del árbol.<br/> 
        /// <i>Cada entidad que se vincula a un árbol puede estar en una sola ubicación de la jerarquía, no puede tener dos padres.</i><br/> 
        /// </summary>
        /// <param name="hojas">Árbol jerárquico</param>
        /// <param name="entidadHoja">Entidad a asignar a la hoja</param>
        /// <returns><i><b>IsBroken:</b> Retorna verdadero cuando la entidad asignada ya pertenece a otra hoja del mismo árbol<br/></i> </returns>
        public LaEntidadEsUnicaEnTodoElArbolRule(IReadOnlyList<JerarquiaItem> hojas, IEntity entidadHoja)
        {
            _hojas = (List<JerarquiaItem>)hojas;
            _entidadHoja = entidadHoja;
        }
        /// <inheritdoc cref="IBusinessRule.IsBroken"/>
        public override bool IsBroken() 
        {
            return _hojas.Count(i=> i.GetEntidadId().Equals(_entidadHoja?.Id)) > 1;
        }
    }
}
