using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using System.Collections.Generic;

namespace Algoritmo.CharlaEFC.Domain.BaseClasses.Interfaces
{
    /// <summary>
    /// <inheritdoc cref="IBaseEntidadMaestraJerarquizable"/>
    /// </summary>
    public interface IEntidadMaestraJerarquizable : IBaseEntidadMaestraJerarquizable
    {
        /// <summary>
        /// <inheritdoc cref="IBaseEntidadMaestraJerarquizable.Hojas"/>
        /// </summary>
        public new IReadOnlyList<IEntidadJerarquizable<IEntidadMaestraJerarquizable>> Hojas { get; }
    }

    /// <summary>
    /// <inheritdoc cref="IBaseEntidadTransaccionalJerarquizable"/>
    /// </summary>
    public interface IEntidadTransaccionalJerarquizable : IBaseEntidadTransaccionalJerarquizable
    {
        /// <summary>
        /// <inheritdoc cref="IBaseEntidadTransaccionalJerarquizable.Hojas"/>
        /// </summary>
        public new IReadOnlyList<IEntidadJerarquizable<IEntidadTransaccionalJerarquizable>> Hojas { get; }
    }

    /// <summary>
    /// <inheritdoc cref="Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces.IBaseEntidadJerarquizable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntidadJerarquizable<T> : IBaseEntidadJerarquizable<T>
    {
        /// <summary>
        /// <inheritdoc cref="IBaseEntidadJerarquizableDTO.JerarquiaItem"/>
        /// </summary>
        public new JerarquiaItem JerarquiaItem { get; }
    }
}
