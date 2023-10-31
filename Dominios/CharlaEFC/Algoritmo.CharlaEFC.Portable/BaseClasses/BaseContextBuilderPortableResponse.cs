using System;
using System.Collections.Generic;
using System.Text;
using Algoritmo.Microservices.Shared.Portable.BaseClasses;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses
{
    public abstract class BaseContextBuilderPortableResponse : BaseContextBuilderResponse
    {
        public BaseContextBuilderPortableResponse(Guid correlationId) : base(correlationId) { }

        public BaseContextBuilderPortableResponse() { }

        public BaseContextBuilderPortableResponse(string message) { }

    }
}
