using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Portable.Enums.Jerarquias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.General.CommandHandlers.Strategies
{
    public record TrackRamasRecord(Guid JerarquiaId);
    public class TrackRamasStrategy<TJerarquiaItem> : BehaviourAdapterStrategy
        where TJerarquiaItem:class,IJerarquiaItem
    {
        public TrackRamasStrategy(IWorkContext wc) : base(wc){ }

        public override async Task RunAsync(params object[] args)
        {
            var record = ValidateArgs<TrackRamasRecord>(args);

            var tipoHoja = Domain.Jerarquias.Enum.TipoJerarquiaItem.FromValue((int)TipoItemJerarquia.Hoja);
            var repo = WorkContext.GetRepository<TJerarquiaItem>();
            var arbol = repo.Entities
                        .Include(i => i.Padre)
                        .Where(i => !i.Tipo.Equals(tipoHoja))
                        .Where(i => i.Jerarquia.Id.Equals(record.JerarquiaId)).ToList();
        }
    }
}
