using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Rules
{
    public class PermiteHojasYRamasEnMismoNivelRule : BaseRule
    {
        private IWorkContext _workContext;
        private JerarquiaItem _item;

        /// <summary>
        /// <b>BusinessRule:</b> no permitir que hayan hojas y ramas en el mismo nivel si en la jerarquía no lo determina<br/> 
        /// <i>Controlo que si en la jerarquía está definido que no permite hojas y ramas en el mismo nivel, todos los item de jerarquía de un nivel sean del mismo tipo.</i><br/> 
        /// </summary>
        /// <param name="arbol">Árbol jerárquico</param>
        /// <param name="item">Rama del árbol</param>
        /// <returns><i><b>IsBroken:</b> Retorna verdadero cuando la regla no se cumpla</i></returns>
        public PermiteHojasYRamasEnMismoNivelRule(IWorkContext workContext,JerarquiaItem item)
        {
            _workContext = workContext;
            _item = item;
        }
        /// <inheritdoc cref="IBusinessRule.IsBroken"/>
        public override bool IsBroken() {

            if (!_item.Jerarquia.PermiteHojasYRamasEnMismoNivel) 
            {
                //var repo = _workContext.GetRepository<JerarquiaItem>();
                //var query = repo.Entities
                //                .Where(i=> i.Jerarquia.Id.Equals(_item.Jerarquia.Id) &&
                //                           i.Nivel.Equals(_item.Nivel))
                //                .Select(t => t).AsEnumerable()
                //                .GroupBy(i=> i.Tipo);

                //// si no retornó nada, se cumple la regla porque estoy agregando el primer item del nivel
                //if (!query.Any()) return false;

                //if (query.Count() > 1) 
                //{
                //    AddErrorMessage("Se detectaron hojas y ramas en el mismo nivel cuando la jerarquía esta definida para no permitirlo");
                //}

                //var tipoDelNivel = query.First().Key;

                ////Si es de distinto tipo al agregado recientemente, rompe la regla
                //if(!_item.Tipo.Equals(tipoDelNivel)) return true;
            }
            return false;
        }

    }
}