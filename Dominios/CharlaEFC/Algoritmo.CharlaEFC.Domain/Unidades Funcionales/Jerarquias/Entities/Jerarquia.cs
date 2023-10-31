using Algoritmo.CharlaEFC.Domain.BaseClasses;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Enum;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Rules;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
using Algoritmo.Microservices.Shared.Domain.Exceptions;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Entities
{
    public partial class Jerarquia : EntidadTransaccional, IJerarquia, IAggregateRoot
    {
        protected Jerarquia() { }
        public Jerarquia(string codigo, string nombre, Type tipoEntidadHoja)
        {
            Codigo = codigo;
            Nombre = nombre;
            _tipoEntidadHoja = tipoEntidadHoja;
        }

        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        [NotMapped]
        ///<inheritdoc cref="IJerarquiaBase.TipoEntidadHoja"/>
        public Type TipoEntidadHoja => _tipoEntidadHoja??Type.GetType(TipoEntidadFullName);

        public string TipoEntidadAssembly { get; private set; }
        public string TipoEntidadFullName { get; protected set; } = string.Empty;

        public bool PermiteHojasYRamasEnMismoNivel { get; set; }

        ///<inheritdoc cref="IJerarquiaBase.Niveles"/>
        public IReadOnlyList<JerarquiaNivel> Niveles => _niveles;

        ///<inheritdoc cref="IJerarquiaBase.Root"/>
        [NotMapped]
        public JerarquiaItem Root => _arbol.Single(r => r.Tipo is TipoJerarquiaItem.TipoRaiz);
        public string RootJs
        {
            get
            {                
                return _rootJs;
            }
            protected set
            {
                _rootJs = value;
            }
        }
        ///<inheritdoc cref="IJerarquiaBase.Arbol"/>
        public IReadOnlyList<JerarquiaItem> Arbol => _arbol;
        public IReadOnlyList<JerarquiaItem> Hojas => Arbol.Where(t => t.Tipo is TipoJerarquiaItem.TipoHoja).ToList();
        public bool Activo { get; set; }


        #region Implementación obligatoria de interfaz base
        [NotMapped]
        IJerarquiaItem IJerarquia.Root => Root;
        [NotMapped]
        IReadOnlyList<IJerarquiaNivel> IJerarquia.Niveles => Niveles;
        [NotMapped]
        IReadOnlyList<IJerarquiaItem> IJerarquia.Hojas => Hojas;
        [NotMapped]
        IReadOnlyList<IJerarquiaItem> IJerarquia.Arbol => Arbol;

        Type IJerarquia.TipoEntidadHoja => TipoEntidadHoja;
        #endregion
    }
    public partial class Jerarquia
    {
        #region Backing Fields
        private List<JerarquiaNivel> _niveles = new();
        private JerarquiaItem? _root = default;
        private string _rootJs = string.Empty;
        private List<JerarquiaItem> _arbol = new();
        private Type _tipoEntidadHoja = null!;
        #endregion

        #region JsonSettings
        // Newtonsoft.Json
        private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            NullValueHandling = NullValueHandling.Include,
            Formatting = Formatting.Indented,
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All

        };
        #endregion

        #region Validaciones
        private void AgregarReglasComunes(Microservices.Shared.Domain.Services.IWorkContext context)
        {
            Rules.Add(new ControlDatosObligatoriosRule(this));            
            ValidarArbol(context);
            ValidarNiveles();
        }

        private void ValidarNiveles()
        {
            _niveles.ForEach(n => Rules.Add(new ControlDatosObligatoriosNivelRule(n))
                                       .Add(new UnaJerarquiaNivelEsUnicaParaElNivelJerarquicoRule(this, n)));
            CheckRules();
        }

        /// <summary>
        /// Método para realizar la validación de cada item en su contexto.
        /// </summary>
        private void ValidarArbol(Microservices.Shared.Domain.Services.IWorkContext context)
        {
            //Recorro uno por uno los items del árbol para validarlos según su tipo
            _arbol.ForEach(i =>
            {
                i.Tipo.Configure(context);
                i.Tipo.Validar(this, i);
            });
        }

        public void ValidarBorrado(Microservices.Shared.Domain.Services.IWorkContext context)
        {
        }
        public void ValidarCreacion(Microservices.Shared.Domain.Services.IWorkContext context)
        {
            AgregarReglasComunes(context);
            CheckRules();
        }
        public void ValidarPreModificacion()
        {
            //var em = WorkContext.Services.EntityManager;
            //var jerarquiaOld = (Jerarquia)em.GetJerarquiaByIdAsync(Id).Result.GetValueOrDefault(this);
            var r = WorkContext.Services.UnitOfWorkManager.GetReadOnlyUnitOfWork().Repository<Jerarquia>();
            var jerarquiaOld = r.Entities.SingleOrDefault(t => t.Id == this.Id);
            CheckRule(new NoCambiarTipoDeJerarquiaSiYaTengoHojasRule(jerarquiaOld, this));
            CheckRules();
        }

        public void ValidarModificacion(Microservices.Shared.Domain.Services.IWorkContext context)
        {
            AgregarReglasComunes(WorkContext ?? context);
            CheckRules();
        }
        #endregion

        #region Administración de Niveles
        public bool Add(JerarquiaNivel nivel)
        {
            if (!base.AddToList(_niveles, nivel)) return false;

            return true;
        }
        public bool Update(JerarquiaNivel nivel)
        {
            //Si el nivel no existe, retorno falso.
            if (GetNivel(nivel.Nivel) is null) return false;

            //Borro el item viejo
            base.RemoveFromList(_niveles, GetNivel(nivel.Nivel));

            //Agrego el item nuevo actualizando la jerarquía con el nuevo nivel.
            return Add(nivel);
        }
        public bool Remove(JerarquiaNivel nivel)
        {
            return base.RemoveFromList(_niveles, nivel);
        }
        public bool Clear(JerarquiaNivel _)
        {
            return base.ClearList(_niveles);
        }

        #region Métodos
        #region GetNivel
        /// <inheritdoc cref="IJerarquiaBase.GetNivel(int)"/>
        public IJerarquiaNivel? GetNivel(int nivel)
        {
            return _niveles.SingleOrDefault(n => n.Nivel == nivel);
        }
        #endregion
        #endregion
        #endregion

        #region Administración de Árbol
        public bool Add(JerarquiaItem item)
        {
            item.GrupoEconomicoId = this.GrupoEconomicoId;
            item.EmpresaId = this.EmpresaId;
            if (!base.AddToList(_arbol, item)) return false;

            //Root = GetArbolFromList(_arbol);
            return true;
        }
        public bool Update(JerarquiaItem item)
        {
            var ji = GetItem(item.Id);
            if (ji is null) return false;

            if (!Remove(ji)) return false;

            return Add(item);
        }
        public bool Remove(JerarquiaItem item)
        {
            if (!base.RemoveFromList(_arbol, item)) return false;

            //Root = GetArbolFromList(_arbol);
            return true;
        }
        public bool Clear(JerarquiaItem _)
        {
            return base.ClearList(_arbol);
        }


        #region Métodos      
        public bool ArbolInitialize()
        {
            //Inicializo el árbol
            var j = new JerarquiaItem(Codigo, Nombre, this, TipoJerarquiaItem.Raiz);
            return Add(j);
        }      
        
        public  void LoadArbol(List<JerarquiaItem> arbol)
        {
            _arbol = arbol;
        }


        #region GetItem
        /// <summary>
        /// <inheritdoc cref="IJerarquia.GetItem(IEntity)"/>
        /// </summary>
        public JerarquiaItem? GetItem(IEntity item)
        {
            return _arbol.Where(r => r.Tipo is TipoJerarquiaItem.TipoHoja).SingleOrDefault(r => r.Entidad?.Id == item.Id);
        }
        IJerarquiaItem? IJerarquia.GetItem(IEntity item) => GetItem(item);
     
        /// <summary>
        /// <inheritdoc cref="IJerarquia.GetItem(Guid)"/>
        /// </summary>
        public JerarquiaItem? GetItem(Guid id)

        {
            return _arbol.SingleOrDefault(i => i.Id.Equals(id));
        }
        IJerarquiaItem? IJerarquia.GetItem(Guid id) => GetItem(id);

        #endregion

        public Jerarquia? ConfigurarHojas(IWorkContext workContext)
        {
            Hojas.ToList().ForEach(h => h.Configure(workContext));
            return this;
        }
        IJerarquia? IJerarquia.ConfigurarHojas(Microservices.Shared.Domain.Services.IWorkContext workContext)=>ConfigurarHojas((IWorkContext)workContext);

        public Jerarquia CargarEntidades()
        {
            Hojas.Where(h => h.WorkContext is not null).ToList().ForEach(h => _ = h.Entidad);
            return this;
        }
        IJerarquia IJerarquia.CargarEntidades() => CargarEntidades();

        /// <summary>
        /// <inheritdoc cref="IJerarquia.AddRama(string, string, IJerarquiaItem)"/>
        /// </summary>
        public JerarquiaItem AddHijo(JerarquiaItem itemHijo)
        {
            _arbol.Single(i => i.Id == itemHijo.Padre.Id).Add(itemHijo);
            Add(itemHijo);
            return itemHijo;
        }
        IJerarquiaItem IJerarquia.AddHijo(IJerarquiaItem itemHijo) => AddHijo((JerarquiaItem)itemHijo);

        /// <summary>
        /// <inheritdoc cref="IJerarquia.AddRama(string, string, IJerarquiaItem)"/>
        /// </summary>
        public JerarquiaItem AddRama(string codigo, string nombre, JerarquiaItem itemPadre)
        {
            var item = new JerarquiaItem(codigo, nombre, this, TipoJerarquiaItem.FromValue((int)Microservices.Shared.Portable.Enums.Jerarquias.TipoItemJerarquia.Rama), itemPadre);

            AddHijo(item);

            return item;
        }
        IJerarquiaItem IJerarquia.AddRama(string codigo, string nombre, IJerarquiaItem itemPadre) => AddRama(codigo, nombre, (JerarquiaItem)itemPadre);

        /// <summary>
        /// <inheritdoc cref="IJerarquia.AddHoja(string, string, IJerarquiaItem)"/>
        /// </summary>
        public JerarquiaItem AddHoja(JerarquiaItem itemPadre)
        {
            //Creo una Hoja con los datos recibidos
            var itemHoja = new JerarquiaItem(null, null, this, TipoJerarquiaItem.FromValue((int)Microservices.Shared.Portable.Enums.Jerarquias.TipoItemJerarquia.Hoja), itemPadre);

            AddHijo(itemHoja);

            return itemHoja;
        }
        IJerarquiaItem IJerarquia.AddHoja(IJerarquiaItem itemPadre) => AddHoja((JerarquiaItem)itemPadre);

        public Jerarquia RemoveHijo(string codigo)
        {
            _arbol.RemoveAll(i => i.Codigo?.Equals(codigo) ?? false);
            return this;
        }


        IJerarquia IJerarquia.RemoveHijo(string codigo) => RemoveHijo(codigo);

        /// <summary>
        /// <inheritdoc cref="IJerarquia.MoverItem(IJerarquiaItem, IJerarquiaItem)"/>
        /// </summary>
        public void MoverItem(JerarquiaItem itemMovible, JerarquiaItem itemDestino)
        {
            //Agrego el hijo al nuevo padre
            itemDestino.Add(itemMovible);

            //Cambio de padre
            itemMovible.CambiarDePadre(itemDestino);
        }

        public void MoverItem(IJerarquiaItem itemMovible, IJerarquiaItem itemDestino) => MoverItem((JerarquiaItem)itemMovible, (JerarquiaItem)itemDestino);

        #endregion
        #endregion

        /// <summary>
        /// <inheritdoc cref="IJerarquia.SetEntityType(Type)"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Jerarquia SetEntityType(Type type)
        {
            _tipoEntidadHoja = type;
            if (type is null) return this;

            TipoEntidadFullName = type.FullName;
            TipoEntidadAssembly = type.AssemblyQualifiedName;
            return this;
        }
        IJerarquia IJerarquia.SetEntityType(Type type) => SetEntityType(type);
    }
}

