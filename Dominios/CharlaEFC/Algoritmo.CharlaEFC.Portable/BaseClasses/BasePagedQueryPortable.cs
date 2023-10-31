using Algoritmo.Microservices.Shared.Portable.BaseClasses;
using MediatR;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses
{
    public abstract class BasePagedQueryPortable<TPagedQueryResponse, T> : BasePagedQuery, IRequest<TPagedQueryResponse>
        where TPagedQueryResponse : BasePagedQueryPortableResponse<T>

    {

    }
}
