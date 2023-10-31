using Algoritmo.CharlaEFC.Domain.BaseClasses.Interfaces;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Services;
using System;
using System.Collections.Generic;

namespace Algoritmo.CharlaEFC.Domain.BaseClasses
{

    /// <summary>
    /// Representa una entidad MAESTRA que puede estar incluida en una jerarquía.
    /// </summary>
    public class EntidadMaestraJerarquizable : EntidadMaestra, IEntidadMaestraJerarquizable
    {
        /// <inheritdoc cref="IEntidadJerarquizable{T}"/>
        public class EntidadJerarquia<T> : BaseEntidadSeguridad, ICharlaEFC, IEntidadJerarquizable<T>
            where T : EntidadMaestraJerarquizable
        {
            public EntidadJerarquia(JerarquiaItem jerarquiaItem, T entidad)
            {
                JerarquiaItem = jerarquiaItem;
                Entidad = entidad;
                //Agrego esta condición, porque cuando desjerarquizo la entidad y la jerarquía llegan en null
                if (entidad is not null)
                {
                    GrupoEconomicoId = entidad.GrupoEconomicoId;
                    EmpresaId = entidad.EmpresaId;
                }
            }
            protected EntidadJerarquia()
            {
            }
            public JerarquiaItem JerarquiaItem { get; private set; } = null!;
            public T Entidad { get; private set; } = null!;
            public override bool Configure(IWorkContext workContext)
            {
                JerarquiaItem.Configure(workContext);
                return base.Configure(workContext);
            }
            IJerarquiaItem IBaseEntidadJerarquizable<T>.JerarquiaItem => JerarquiaItem;
        }

        public class EntidadMaestraInfo : IEntidadMaestraInfo
        {
            protected EntidadMaestraInfo() { }
            public EntidadMaestraInfo(int id, string codigo, string descripcion)
            {
                Id = id;
                Codigo = codigo;
                Descripcion = descripcion;
            }

            public int Id { get; protected set; }

            public string Codigo { get; protected set; } = string.Empty;

            public string Descripcion { get; protected set; } = string.Empty;

            object IEntidadInfo.Id => Id;
        }

        public virtual void Jerarquizar(IJerarquiaItem jerarquiaItem) 
        {
            jerarquiaItem.Jerarquizar(this);
        }
        public virtual void Desjerarquizar(IJerarquiaItem jerarquiaItem)
        {
            jerarquiaItem.Desjerarquizar(this);
        }

        public virtual IReadOnlyList<IEntidadJerarquizable<IEntidadMaestraJerarquizable>> Hojas => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadMaestraJerarquizable) } debe reemplazar esta propiedad con su tipo concreto");

        #region Implementación Obligatoria de la interfaz
        IReadOnlyList<Microservices.Shared.Domain.Jerarquias.Interfaces.IBaseEntidadJerarquizable<IBaseEntidadMaestraJerarquizable>> IBaseEntidadMaestraJerarquizable.Hojas => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadMaestraJerarquizable) } debe reemplazar esta propiedad con su tipo concreto");
        #endregion

        public virtual IEntidadMaestraInfo GetEntidadInfo() => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadMaestraJerarquizable) } debe reemplazar este metodo por su implementación concreta");
    }

    /// <summary>
    /// Representa una entidad TRANSACCIONAL que puede estar incluida en una jerarquía.
    /// </summary>
    public class EntidadTransaccionalJerarquizable : EntidadTransaccional, IEntidadTransaccionalJerarquizable
    {
        /// <inheritdoc cref="IEntidadJerarquizable{T}"/>
        public class EntidadJerarquia<T> : BaseEntidadSeguridad, ICharlaEFC, IEntidadJerarquizable<T>
            where T : EntidadTransaccionalJerarquizable
        {
            public EntidadJerarquia(JerarquiaItem jerarquiaItem, T entidad)
            {
                JerarquiaItem = jerarquiaItem;
                Entidad = entidad;
                //Agrego esta condición, porque cuando desjerarquizo la entidad y la jerarquía llegan en null
                if (entidad is not null)
                {
                    GrupoEconomicoId = entidad.GrupoEconomicoId;
                    EmpresaId = entidad.EmpresaId;
                }
            }
            protected EntidadJerarquia()
            {
            }
            public JerarquiaItem JerarquiaItem { get; private set; } = null!;
            public T Entidad { get; private set; } = null!;
            public override bool Configure(IWorkContext workContext)
            {
                JerarquiaItem.Configure(workContext);
                return base.Configure(workContext);
            }
            #region Implementaciones Obligatorias de la interfaz
            IJerarquiaItem IBaseEntidadJerarquizable<T>.JerarquiaItem => JerarquiaItem;
            #endregion
        }

        public class EntidadTransaccionalInfo : IEntidadTransaccionalInfo
        {
            protected EntidadTransaccionalInfo() { }
            public EntidadTransaccionalInfo(Guid id, string descripcion)
            {
                Id = id;
                Descripcion = descripcion;
            }

            public Guid Id { get; protected set; } 
            public string Descripcion { get; protected set; } = string.Empty;

            object IEntidadInfo.Id => Id;
        }

        public virtual void Jerarquizar(IJerarquiaItem jerarquiaItem)
        {
            jerarquiaItem.Jerarquizar(this);
        }

        public IEntidadTransaccionalInfo GetEntidadInfo()
        {
            throw new NotImplementedException();
        }

        public virtual void Desjerarquizar(IJerarquiaItem jerarquiaItem)
        {
            jerarquiaItem.Desjerarquizar(this);
        }

        public virtual IReadOnlyList<IEntidadJerarquizable<IEntidadTransaccionalJerarquizable>> Hojas => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadTransaccionalJerarquizable) } debe reemplazar esta propiedad con su tipo concreto");
        #region Implementación Obligatoria de la interfaz
        IReadOnlyList<Microservices.Shared.Domain.Jerarquias.Interfaces.IBaseEntidadJerarquizable<IBaseEntidadTransaccionalJerarquizable>> IBaseEntidadTransaccionalJerarquizable.Hojas => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadMaestraJerarquizable) } debe reemplazar esta propiedad con su tipo concreto");
        #endregion
    }
}
