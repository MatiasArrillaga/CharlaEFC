using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.General.Responses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using Algoritmo.Microservices.Shared.Portable.BaseClasses;
using System;

namespace Algoritmo.CharlaEFC.Portable.General.Queries
{
    /// <summary>
    /// <b>Caso de Uso:</b> CRUD Jerarquías
    /// </summary>
    /// <returns>
    /// <see cref="GetJerarquiaByIdResponse"/>: <i>Response con la Jerarquía obtenida según el id indicado</i>
    /// </returns>
    public class GetJerarquiaMinimizadaQuery : BaseQueryPortable<GetJerarquiaMinimizadaResponse>
    {
        public GetJerarquiaMinimizadaQuery()
        {

        }
        public GetJerarquiaMinimizadaQuery(Guid jerarquiaId)
        {
            JerarquiaId = jerarquiaId;
        }
        //[RequiredGreaterThanZero(ErrorMessage = "IDRequired")]
        public Guid JerarquiaId { get; set; } = Guid.Empty;

    }

}

