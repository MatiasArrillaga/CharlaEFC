using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.General.Responses;
using Algoritmo.Microservices.Shared.Portable.Interfaces;
using System;

namespace Algoritmo.CharlaEFC.Portable.General.Queries
{
    public class GetEntidadQuery : BasePagedQueryPortable<GetEntidadResponse, object>, IGetEntidadQuery
    {
        public GetEntidadQuery(string tipo)
        {
            Tipo = tipo;
        }

        public string Tipo { get; set; }

        public object? Id { get; set; }

        public string? Codigo { get; set; }

        public Guid? JerarquiaId { get; set; }

        public string? TypeDTOName { get; set; }
        public string? DomainServiceName { get; set; }
    }
}
