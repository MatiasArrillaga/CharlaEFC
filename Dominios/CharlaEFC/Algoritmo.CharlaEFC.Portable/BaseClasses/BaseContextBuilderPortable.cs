using Algoritmo.Microservices.Shared.Portable.BaseClasses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses
{
    public abstract class BaseContextBuilderPortable<TContextBuilderResponse> : BaseContextBuilder, IRequest<TContextBuilderResponse> where TContextBuilderResponse : BaseContextBuilderPortableResponse
    {

    }
}
