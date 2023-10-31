using Algoritmo.CharlaEFC.Application.BaseClasses.Common;
using Algoritmo.CharlaEFC.Application.General.Query;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.General.Queries;
using Algoritmo.CharlaEFC.Portable.General.Responses;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using Algoritmo.Microservices.Shared.Application.BaseClasses.General;
using Algoritmo.Microservices.Shared.Application.Extensions;
using Algoritmo.Microservices.Shared.Portable.Enums.Jerarquias;
using Algoritmo.Microservices.Shared.Portable.Jerarquias;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Portable.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.General.Queries
{
    /// <summary>
    /// Clase controladora de la petición GetJerarquiaMinimizada
    /// </summary>
    public class GetJerarquiaMinimizadaQueryHandler : BaseQueryHandler<GetJerarquiaMinimizadaQuery, GetJerarquiaMinimizadaResponse>
    {
        /// <inheritdoc cref="BaseQueryHandler.BaseQueryHandler(IWorkContext)"/>
        public GetJerarquiaMinimizadaQueryHandler(IWorkContext context) : base(context) { }

        public override async Task<GetJerarquiaMinimizadaResponse> HandleDelegate(GetJerarquiaMinimizadaQuery query, CancellationToken cancellationToken)
        {
            //se genera un nuevo response ligado al CorrelationID de la query.
            //de esta forma se mantiene una relación entre ambos por ID
            var response = new GetJerarquiaMinimizadaResponse(query.CorrelationId);

            var filters = new List<FieldWhereDefinition>()
                                {
                                    new FieldWhereDefinition($"{nameof(JerarquiaItem.Jerarquia)}.{nameof(JerarquiaItem.Jerarquia.Id)}",Operador.Equals,query.JerarquiaId),
                                    new FieldWhereDefinition($"{nameof(JerarquiaItem.Tipo)}",Operador.NotEqual,(int)TipoItemJerarquia.Hoja)
                                };

            var sttgyParams = new GetEntidadStrategyParams(null, null, null, 
                                              PaginationInfo: new BasePaginationInfo() { Start = 1, Length = 10000 },
                                              Filters: filters,
                                              OrderBy: new List<FieldOrderDefinition>());

            var result = await em.GetPagedAsync<JerarquiaItem>(new GetJerarquiaItemStrategy(WorkContext, sttgyParams), sttgyParams);

            var itemsMin = result.Items.Select(i=> GetItemMinimizado(i));
            var items = result.Items.Select(i => i.mapMe<JerarquiaItemDTO>()).Cast<IJerarquiaItemDTO>().ToList();

            response.Jerarquia = GetArbolFromList(itemsMin);
            response.Detalle = items;
            return response;
        }

        /// <summary>
        /// Metodo mediante el cual se genera el árbol de jerarquía DTO a partir de una lista plana de items minimizados
        /// </summary>
        /// <param name="arbol"></param>
        /// <returns></returns>
        private JerarquiaItemMinDTO GetArbolFromList(IEnumerable<JerarquiaItemMinDTO> arbol)
        {
            //Obtengo el item Root que sería la raiz del árbol.
            var _root = arbol.Single(r => r.TipoItem.Equals(TipoItemJerarquia.Raiz)) ?? null!;

            // Agrupo todos los items por Código de padre, dejando afuera el root.(Lo hago por código y no por Id, porque el id puede no estar seteado aun)
            var lookup = arbol
                .Where(h => !h.TipoItem.Equals(TipoItemJerarquia.Raiz))
                .Select(h => h)
                .ToLookup(h => h.CodigoPadre);

            if (lookup.Count != 0)
            {
                //Asigno a cada padre su hijo.
                foreach (var h in lookup.SelectMany(x => x))
                {
                    h.Hijos = lookup[h.Codigo].ToList();
                }

                //Finalmente, asigno todos los hijos a la propiedad Hijos del root.
                _root.Hijos = lookup.First().ToList();
            }

            return _root;
        }

        private JerarquiaItemMinDTO GetItemMinimizado(JerarquiaItem i)
        {
            return new JerarquiaItemMinDTO(i.Id, i.Codigo, i.Nombre, i.Padre?.Codigo, (TipoItemJerarquia)i.Tipo.Value);
        }
    }
}





