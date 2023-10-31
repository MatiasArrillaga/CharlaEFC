using Algoritmo.CharlaEFC.Domain.BaseClasses;
using Algoritmo.CharlaEFC.Domain.BaseClasses.Interfaces;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Enum;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Entities
{
    public partial class JerarquiaItem : EntidadTransaccional, IJerarquiaItem
    {
        protected JerarquiaItem() 
        {
        }

        /// <summary>
        /// Constructor para items del tipo Rama u Hoja, ya que deben tener un padre.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="jerarquia"></param>
        /// <param name="tipo"></param>
        /// <param name="padre"></param>
        public JerarquiaItem(string codigo, string nombre, Jerarquia jerarquia, TipoJerarquiaItem tipo, JerarquiaItem padre) 
            : this(codigo, nombre, jerarquia, tipo)
        {
            Padre = padre;
        }
       
        /// <summary>
        /// Constructor para un item del tipo Raiz, porque el item no tendrá padre.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="jerarquia"></param>
        /// <param name="tipo"></param>
        public JerarquiaItem(string codigo, string nombre, Jerarquia jerarquia, TipoJerarquiaItem tipo):this()
        {
            Codigo = codigo;
            Nombre = nombre;
            Tipo = tipo;
            Jerarquia = jerarquia;    
        }

        public string? Codigo { get; set; } = string.Empty;
        public string? Nombre { get; set; } = string.Empty;
        public int? Nivel { get; set; }
        public string? CodigoConcatenado { get; set; }
        public string? NombreConcatenado { get; set; }
        public TipoJerarquiaItem Tipo { get; set; } = null!; 
        public Jerarquia Jerarquia { get; set; } = null!;
        public JerarquiaItem? Padre { get; protected set; }
        public IReadOnlyList<JerarquiaItem> Hijos => _hijos;

        /// <summary>
        /// Retorna información basica de la entidad jerarquizada para se presentada por pantalla.
        /// </summary>
        public IEntidadInfo? EntidadInformacion
        {
            get
            {
                if (_entidadInformacion is null)
                    EntidadInformacion = GetEntidadInfo(_entidad);

                return _entidadInformacion;
            }
            protected set
            {
                _entidadInformacion = value;
            }
        }

        #region Entidad Jerarquizable
        public string? TipoEntidadFullName { get; private set; }
        public string? TipoEntidadAssembly { get; private set; }
        public int? EntidadMaestraId { get; private set; }
        public Guid? EntidadTransaccionalId { get; private set; }

        private IEntidadJerarquizable? _entidad;
        public IEntidadJerarquizable? Entidad
        {
            get
            {
                Func<Task<IEntidadJerarquizable?>> f = async () => { return await GetEntidad(); };
                if (_entidad is null)
                    Entidad = f().Result;

                return _entidad;
            }
            private set
            {
                _entidad = value;
                EntidadInformacion = GetEntidadInfo(value);
            }
        }

        #endregion

        #region Implementaciones obligatorias de la interfaz base
        [NotMapped]
        IJerarquia IJerarquiaItem.Jerarquia { get => Jerarquia; set => Jerarquia = (Jerarquia)value; }
        [NotMapped]
        IJerarquiaItem? IJerarquiaItem.Padre => Padre;
        [NotMapped]
        BaseTipoJerarquiaItem? IJerarquiaItem.Tipo => throw new NotImplementedException();
        [NotMapped]
        IReadOnlyList<IJerarquiaItem> IJerarquiaItem.Hijos => Hijos;
        #endregion

    }

    public partial class JerarquiaItem
    {
        #region BackingFields       
        private List<JerarquiaItem> _hijos = new();
        private IEntidadInfo? _entidadInformacion;
        #endregion

        #region Administración de Hijos
        public bool Add(JerarquiaItem item)
        {
            return base.AddToList(_hijos, item);
        }
        public bool Remove(JerarquiaItem item)
        {
            return base.RemoveFromList(_hijos, item);
        }
        public bool Update(JerarquiaItem item)
        {
            base.RemoveFromList(_hijos, GetItem(item.Id));

            return base.AddToList(_hijos, item);
        }
        public bool Clear(JerarquiaItem _)
        {
            return base.ClearList(_hijos);
        }

        #region Métodos
        /// <summary>
        /// Recupera un item hijo según el código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IJerarquiaItem? GetItem(Guid id)
        {
            return _hijos.SingleOrDefault(r => r.Id .Equals(id)); 
        }

        /// <summary>
        /// Metodo para cambiar el padre del item.
        /// </summary>
        /// <param name="itemPadre"></param>
        public void CambiarDePadre(JerarquiaItem itemPadre)
        {
            Padre = itemPadre;

        }

        /// <summary>
        /// Quito un item de la lista de hijos
        /// </summary>
        /// <param name="itemMovible"></param>
        internal void SoltarHijo(JerarquiaItem itemMovible)
        {
            var hijo = _hijos.FirstOrDefault(h => h.Codigo.Equals(itemMovible.Codigo));
            if (hijo is not null)
            {
                _hijos.Remove(hijo);
            }
        }

        /// <summary>
        /// Borra un item según el código indicado. Hace un recorrido recursivo dentro de la lista de hijos
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public JerarquiaItem RemoveHijo(string codigo)
        {
            JerarquiaItem itmRmv = null!;

            _hijos.ForEach(i =>
            {
                if (i.Codigo.Equals(codigo))
                {
                    itmRmv = i;
                    return;
                }
                i.Hijos.ToList().ForEach(h =>
                {
                    if (h.Codigo.Equals(codigo))
                    {
                        i.Remove(h);
                        return;
                    };
                    h.RemoveHijo(codigo);
                });
            });

            if (itmRmv is not null) Remove(itmRmv);
            return this;
        }
        #endregion

        #endregion
        
        #region Métodos para la Jerarquización y obtención de una Entidad Jerarquizable
        public void Jerarquizar(IEntidadMaestraJerarquizable entidad)
        {
            _ = entidad ?? throw new NullReferenceException("La entidad a vincular al ítem de jerarquía no puede ser nula.");
            // Si ya está vinculado descarta la operación
            if (Object.ReferenceEquals(Entidad, entidad))
                return;

            Entidad = entidad;
            EntidadMaestraId = (int)entidad.GetId();
            TipoEntidadFullName = entidad.GetType().FullName;
            TipoEntidadAssembly = entidad.GetType().AssemblyQualifiedName;

            entidad.Jerarquizar(this);
        }
        //public void Jerarquizar(IBaseEntidadMaestraJerarquizable entidad) => Jerarquizar(entidad);

        public void Jerarquizar(IEntidadTransaccionalJerarquizable entidad)
        {
            _ = entidad ?? throw new NullReferenceException("La entidad a vincular al ítem de jerarquía no puede ser nula.");
            // Si ya está vinculado descarta la operación
            if (Object.ReferenceEquals(Entidad, entidad))
                return;

            Entidad = entidad;
            EntidadTransaccionalId = (Guid)entidad.GetId();
            TipoEntidadFullName = entidad.GetType().FullName;
            TipoEntidadAssembly = entidad.GetType().AssemblyQualifiedName;

            entidad.Jerarquizar(this);
        }

        //public void Jerarquizar(IBaseEntidadTransaccionalJerarquizable entidad) => Jerarquizar(entidad);
        /// <summary>
        /// Recupera y retorna la instancia de la entidad asociada.
        /// </summary>
        /// <returns></returns>
        private async Task<IEntidadJerarquizable?> GetEntidad()
        {
            if (Tipo is not TipoJerarquiaItem.TipoHoja)
                return null;

            if (TipoEntidadFullName is null)
                return null;

            if (!IsConfigured)
                throw new Exception("No se ha configurado el WorkContext para la instancia.");

            return await WorkContext.Services.EntityManager.GetEntidadAsync(this);
        }

        /// <summary>
        /// Obtiene la información necesaria de la entidad, para ser mostrada por pantalla
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        private IEntidadInfo? GetEntidadInfo(IEntidadJerarquizable? entidad)
        {
            if (entidad is null)
                return null;

            if (entidad is IEntidadMaestraJerarquizable emj)
                return emj.GetEntidadInfo();

            if (entidad is IEntidadTransaccionalJerarquizable etj)
                return etj.GetEntidadInfo();

            throw new Exception($"La entidad {entidad.GetType().Name} no es jerarquizable");
        }
        public Type? GetEntidadIdTipo()
        {
            return EntidadMaestraId is not null
                        ? typeof(int)
                        : EntidadTransaccionalId is not null
                            ? typeof(Guid)
                            : null;
        }
        public object? GetEntidadId()
        {
            return EntidadMaestraId is not null
                    ? (int)EntidadMaestraId
                    : EntidadTransaccionalId is not null
                        ? (Guid)EntidadTransaccionalId
                        : null;
        }
        #endregion

        #region Métodos para la Desjerarquización
        public void Desjerarquizar(IEntidadMaestraJerarquizable entidad)
        {
            entidad.Desjerarquizar(this);
            Padre?.Remove(this);
        }
        public void Desjerarquizar(IEntidadTransaccionalJerarquizable entidad)
        {
            entidad.Desjerarquizar(this);
            Padre?.Remove(this);
        }
        #endregion
    }


}