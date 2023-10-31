using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using System.ComponentModel.DataAnnotations;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.Queries
{
    /// <summary>
    /// <b>Caso de Uso:</b> CRUD Jerarquías
    /// </summary>
    /// <returns>
    /// <see cref="GetJerarquiaByCodeResponse"/>: <i>Response con la Jerarquía obtenida según el código indicado</i>
    /// </returns>
    public class GetJerarquiaByCodeQuery : BaseQueryPortable<GetJerarquiaByCodeResponse>
    {
        public GetJerarquiaByCodeQuery()
        {

        }
        public GetJerarquiaByCodeQuery(string codigo)
        {
            Codigo = codigo;
        }
        [Required]
        public string Codigo { get; set; } = string.Empty;
    }

}

