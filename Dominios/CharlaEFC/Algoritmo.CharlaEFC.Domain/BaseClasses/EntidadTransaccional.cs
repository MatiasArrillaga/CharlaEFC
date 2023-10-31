using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
using System;

namespace Algoritmo.CharlaEFC.Domain.BaseClasses
{
    /// <summary>
    /// Clase base para entidades transaccionales.
    /// </summary>
    public class EntidadTransaccional : BaseCharlaEFC, ITransactionalEntity
    {
        public new Guid Id { get; set; }
    }
}
