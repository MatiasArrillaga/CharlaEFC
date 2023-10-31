using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using Algoritmo.Microservices.Shared.Portable.BaseClasses;
using System;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.Queries
{
    /// <summary>
    /// <b>Caso de Uso:</b> CRUD Jerarquías
    /// </summary>
    /// <returns>
    /// <see cref="GetJerarquiaByIdResponse"/>: <i>Response con la Jerarquía obtenida según el id indicado</i>
    /// </returns>
    public class GetJerarquiaByIdQuery : BaseQueryPortable<GetJerarquiaByIdResponse>
    {
        public GetJerarquiaByIdQuery()
        {

        }
        public GetJerarquiaByIdQuery(Guid id)
        {
            Id = id;
        }
        //[RequiredGreaterThanZero(ErrorMessage = "IDRequired")]
        public Guid Id {get;set;}

    }

}

