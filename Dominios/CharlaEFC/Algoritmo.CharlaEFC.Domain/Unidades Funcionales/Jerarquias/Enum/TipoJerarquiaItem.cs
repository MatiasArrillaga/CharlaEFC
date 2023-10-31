using Algoritmo.CharlaEFC.Domain.BaseClasses;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Rules;
using Algoritmo.Microservices.Shared.Domain.BaseClasses;
using Algoritmo.Microservices.Shared.Portable.Enums.Jerarquias;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Enum
{
    public class TipoJerarquiaItem: BaseEnumeration, ICharlaEFC
    {
        public static readonly TipoJerarquiaItem Raiz = new TipoRaiz();
        public static readonly TipoJerarquiaItem Rama = new TipoRama();
        public static readonly TipoJerarquiaItem Hoja = new TipoHoja();
        protected TipoJerarquiaItem() { }
        public TipoJerarquiaItem(int value, string displayName) : base(value, displayName) { }

        public static TipoJerarquiaItem FromValue(int value) => FromValue<TipoJerarquiaItem>(value);

        public virtual void Validar(Jerarquia jerarquia,JerarquiaItem item) 
        {
            //Reseteo las rules por si quedaron en memoria.
            Rules.ResetRules();
            //Seteo las reglas para el item.
            Rules.Add(new ControlDatosObligatoriosItemRule(item))
                 .Add(new PermiteHojasYRamasEnMismoNivelRule(WorkContext, item))
                 .Add(new ControlPadresHijosRule(item));
        }

        #region Tipos Especializados
        public class TipoRaiz : TipoJerarquiaItem
        {
            public TipoRaiz() : base((int)TipoItemJerarquia.Raiz, nameof(TipoItemJerarquia.Raiz).ToUpper()) { }

            public override void Validar(Jerarquia jerarquia,JerarquiaItem item)
            {
                Rules.Add(new ControlDatosObligatoriosItemRule(item))
                     .CheckRules();
            }
        }
        public class TipoRama : TipoJerarquiaItem
        {
            public TipoRama() : base((int)TipoItemJerarquia.Rama, nameof(TipoItemJerarquia.Rama).ToUpper()) { }

            public override void Validar(Jerarquia jerarquia, JerarquiaItem item)
            {
                base.Validar(jerarquia, item);

                Rules.CheckRules();
            }
        }
        public class TipoHoja : TipoJerarquiaItem
        {
            public TipoHoja() : base((int)TipoItemJerarquia.Hoja, nameof(TipoItemJerarquia.Hoja).ToUpper()) { }

            public override void Validar(Jerarquia jerarquia, JerarquiaItem item)
            {
                base.Validar(jerarquia, item);

                Rules.Add(new ElTipoDeEntidadDebeCoincidirConElDefinidoEnLaJerarquiaRule(jerarquia, item))
                     .Add(new NoSePuedeJerarquizarEnLaRaizDelArbolRule(item))
                     .CheckRules();
            }
        }
        #endregion
    }
}