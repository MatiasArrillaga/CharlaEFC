using Algoritmo.Microservices.Shared.Portable.BaseClasses;
using System;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses
{
    /// <summary>
    /// Clase base de consulta paginada del tipo Query. Implementa IPagedQuery
    /// Esta clase se debe utilizar para hacer consultas paginadas.
    /// </summary>
    public abstract class BasePagedQueryPortableResponse<T> : BasePagedQueryResponse<T>
    {
        public BasePagedQueryPortableResponse(Guid correlationId) : base(correlationId) { }

        public BasePagedQueryPortableResponse() { }
    }

}
