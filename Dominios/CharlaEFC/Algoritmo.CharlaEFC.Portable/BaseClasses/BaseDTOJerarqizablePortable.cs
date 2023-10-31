using Algoritmo.CharlaEFC.Portable.BaseClasses.Interfaces;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using Algoritmo.Microservices.Shared.Portable.BaseClassesDTO;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses
{
    public class EntidadMaestraJerarquizableDTO : EntidadMaestraDTO, IBaseEntidadMaestraJerarquizableDTO
    {
        public string Codigo { get; set; } = string.Empty;

        public void Jerarquizar(IJerarquiaItemDTO jerarquiaItem)
        {
            throw new NotImplementedException();
        }

        public class EntidadJerarquiaDTO<T> : BaseDTO, IEntidadJerarquizableDTO<T>
            where T : EntidadMaestraJerarquizableDTO
        {
            public EntidadJerarquiaDTO(JerarquiaItemDTO jerarquiaItem, T entidad)
            {
                JerarquiaItem = jerarquiaItem;
                Entidad = entidad;
            }
            protected EntidadJerarquiaDTO()
            {
            }
            public JerarquiaItemDTO JerarquiaItem { get; private set; } = null!;
            public T Entidad { get; private set; } = null!;

            IJerarquiaItemDTO IBaseEntidadJerarquizableDTO<T>.JerarquiaItem => JerarquiaItem;
        }
        public class EntidadMaestraInfoDTO : IEntidadMaestraInfoDTO
        {
            protected EntidadMaestraInfoDTO() { }
            public EntidadMaestraInfoDTO(int id, string codigo, string descripcion)
            {
                Id = id;
                Codigo = codigo;
                Descripcion = descripcion;
            }

            public int Id { get; protected set; }

            public string Codigo { get; protected set; } = string.Empty;

            public string Descripcion { get; protected set; } = string.Empty;

            object IEntidadInfoDTO.Id => Id;
        }

        public virtual IReadOnlyList<IEntidadJerarquizableDTO<IBaseEntidadMaestraJerarquizableDTO>> Hojas => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadMaestraJerarquizableDTO) } debe reemplazar esta propiedad con su tipo concreto");

        IReadOnlyList<IBaseEntidadJerarquizableDTO<IBaseEntidadMaestraJerarquizableDTO>> IBaseEntidadMaestraJerarquizableDTO.Hojas => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadMaestraJerarquizableDTO) } debe reemplazar esta propiedad con su tipo concreto");

        public virtual IEntidadMaestraInfoDTO GetEntidadInfo() => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadMaestraJerarquizableDTO) } debe reemplazar este metodo por su implementación concreta");
    }
    public class EntidadTransaccionalJerarquizableDTO: EntidadTransaccionalDTO, IBaseEntidadTransaccionalJerarquizableDTO
    {        
        #region ID Transaccional
        //public new Guid? Id { get; set; }
        private Guid? id;
        public new Guid? Id
        {
            get
            {
                return ITransactionalEntityDTO.EvaluateId(id, TemporalId);
            }
            set
            {
                id = value;
            }
        }
        #endregion

        public void Jerarquizar(IJerarquiaItemDTO jerarquiaItem)
        {
            throw new NotImplementedException();
        }

        public class EntidadJerarquiaDTO<T> : BaseDTO, IEntidadJerarquizableDTO<T>
            where T : EntidadTransaccionalJerarquizableDTO
        {
            public EntidadJerarquiaDTO(JerarquiaItemDTO jerarquiaItem, T entidad)
            {
                JerarquiaItem = jerarquiaItem;
                Entidad = entidad;
            }
            protected EntidadJerarquiaDTO()
            {
            }
            public JerarquiaItemDTO JerarquiaItem { get; private set; } = null!;
            public T Entidad { get; private set; } = null!;

            IJerarquiaItemDTO IBaseEntidadJerarquizableDTO<T>.JerarquiaItem => JerarquiaItem;
        }
        public class EntidadTransaccionalInfoDTO : IEntidadTransaccionalInfoDTO
        {
            protected EntidadTransaccionalInfoDTO() { }
            public EntidadTransaccionalInfoDTO(Guid id,  string descripcion)
            {
                Id = id;
                Descripcion = descripcion;
            }

            public Guid Id { get; protected set; }
            public string Descripcion { get; protected set; } = string.Empty;
            object IEntidadInfoDTO.Id => Id;
        }

        public virtual IReadOnlyList<IEntidadJerarquizableDTO<IBaseEntidadTransaccionalJerarquizableDTO>> Hojas => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadTransaccionalJerarquizableDTO) } debe reemplazar esta propiedad con su tipo concreto");

        IReadOnlyList<IBaseEntidadJerarquizableDTO<IBaseEntidadTransaccionalJerarquizableDTO>> IBaseEntidadTransaccionalJerarquizableDTO.Hojas => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadTransaccionalJerarquizableDTO) } debe reemplazar esta propiedad con su tipo concreto");

        public virtual IEntidadTransaccionalInfoDTO GetEntidadInfo() => throw new NotImplementedException($"El tipo derivado de { nameof(EntidadTransaccionalJerarquizableDTO) } debe reemplazar este metodo por su implementación concreta");

    }
}
