using Algoritmo.Microservices.Shared.Portable.BaseClasses;
using MediatR;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses
{
    public abstract class BaseQueryPortable<TQueryResponse> : BaseQuery, IRequest<TQueryResponse> where TQueryResponse : BaseQueryPortableResponse
    {
    }
}
