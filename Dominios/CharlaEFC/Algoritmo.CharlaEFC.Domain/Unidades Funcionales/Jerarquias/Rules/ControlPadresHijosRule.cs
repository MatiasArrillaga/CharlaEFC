using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.Localization;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Rules
{
    public class ControlPadresHijosRule : BaseRule
    {
        //private List<JerarquiaItem> _arbol;
        private JerarquiaItem _item;

        /// <summary>
        /// <b>BusinessRule</b><i> (USER:106272 - FE03)</i>: 
        /// Dado un item de la jerarquía del tipo <b>Rama</b> u <b>Hoja</b>, debe tener un padre.<br/>
        /// Así como también, el padre debe tenerlo como hijo.<br/> 
        /// <i><b>Nota:</b><br/>
        ///    - El unico item que no tiene Padre es el tipo <b>Raíz</b>, dado que él es el origen de todo el árbol.<br/>
        /// </i>
        /// </summary>
        /// <param name="item">Item de tipo Rama u Hoja</param>
        /// <returns><i><b>IsBroken:</b> Retorna verdadero cuando un item hoja no tiene un padre </i></returns>
        /// <remarks>Esta validación se pasa a controlar solo por base de datos. Mantenemos la regla para controlar que el item tenga padre.</remarks>
        public ControlPadresHijosRule(JerarquiaItem item)
        {
            //_arbol = (List<JerarquiaItem>)arbol;
            _item = item;
        }
        /// <inheritdoc cref="IBusinessRule.IsBroken"/>
        public override bool IsBroken() 
        {
            //Controlo que el item tenga un padre
            if (_item.Padre is null)
            {
                AddErrorMessage(Localizer.GetRecursoAsync("ItemSinPadre",_item.Codigo).Result);
                return true;
            }

            ////Controlo que exista el padre en la jerarquía
            //var padre= _arbol.SingleOrDefault(r => r.Id.Equals(_item.Padre.Id) && r.Codigo.Equals(_item.Padre.Codigo));
            //if (padre is null)
            //{
            //    AddErrorMessage(Localizer.GetRecursoAsync("PadreNoExiste", _item.Padre.Codigo).Result);
            //    return true;
            //}

            ////Controlo que el padre tenga al item como su hijo
            //var hijo = padre.Hijos.SingleOrDefault(r => r.Codigo.Equals(_item.Codigo) && r.Id.Equals(_item.Id));
            ////var hijo = _item.Padre.Hijos.SingleOrDefault(r => r.Id.Equals(_item.Id));
            //if (hijo is null)
            //{
            //    AddErrorMessage(Localizer.GetRecursoAsync("ItemNoEsHijoDePadre", padre.Codigo, _item.Codigo).Result);
            //    return true;
            //}

            return false;
        }      

    }
}