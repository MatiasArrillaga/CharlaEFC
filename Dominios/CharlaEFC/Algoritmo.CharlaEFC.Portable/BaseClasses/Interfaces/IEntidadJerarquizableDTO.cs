using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using Algoritmo.Microservices.Shared.Portable.BaseClassesDTO;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using System;
using System.Collections.Generic;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses.Interfaces
{
    /// <summary>
    /// <inheritdoc cref="IBaseEntidadMaestraJerarquizableDTO"/>
    /// </summary>
    public interface IEntidadMaestraJerarquizableDTO : IBaseEntidadMaestraJerarquizableDTO
    {
        /// <summary>
        /// <inheritdoc cref="IBaseEntidadMaestraJerarquizableDTO.Hojas"/>
        /// </summary>
        public new IReadOnlyList<IEntidadJerarquizableDTO<IEntidadMaestraJerarquizableDTO>> Hojas { get; }
    }

    /// <summary>
    /// <inheritdoc cref="IBaseEntidadTransaccionalJerarquizableDTO"/>
    /// </summary>
    public interface IEntidadTransaccionalJerarquizableDTO : IBaseEntidadTransaccionalJerarquizableDTO
    {
        /// <summary>
        /// <inheritdoc cref="IBaseEntidadTransaccionalJerarquizableDTO.Hojas"/>
        /// </summary>
        public new IReadOnlyList<IEntidadJerarquizableDTO<IBaseEntidadTransaccionalJerarquizableDTO>> Hojas { get; }
    }

    /// <summary>
    /// <inheritdoc cref="Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces.IBaseEntidadJerarquizableDTO{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntidadJerarquizableDTO<T> : IBaseEntidadJerarquizableDTO<T>
    {
        /// <summary>
        /// <inheritdoc cref="IBaseEntidadJerarquizableDTO.JerarquiaItem"/>
        /// </summary>
        public new JerarquiaItemDTO JerarquiaItem { get; }
    }
}
