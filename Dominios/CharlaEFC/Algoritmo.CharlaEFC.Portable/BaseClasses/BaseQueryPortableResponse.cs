using Algoritmo.Microservices.Shared.Portable.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses
{
    public abstract class BaseQueryPortableResponse : BaseQueryResponse
    {
        public BaseQueryPortableResponse(Guid correlationId) : base(correlationId) { }

        public BaseQueryPortableResponse() { }

        public BaseQueryPortableResponse(string message) { }

    }
}
