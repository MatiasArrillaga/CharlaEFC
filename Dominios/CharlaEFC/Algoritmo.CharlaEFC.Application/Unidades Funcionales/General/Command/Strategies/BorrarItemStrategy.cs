using Algoritmo.CharlaEFC.Domain.Jerarquias.Interfaces;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.General.CommandHandlers.Strategies
{
    //TODO - Por ahora va a llegar desde el Command. Pero se debería obtener desde el WC
    public record BorrarItemRecord(IJerarquiaItem Item);
    public class BorrarItemStrategy<TJerarquia> : BehaviourAdapterStrategy
        where TJerarquia:class,IJerarquia
    {
        public BorrarItemStrategy(IWorkContext wc) : base(wc){ }

        public override async Task RunAsync(params object[] args)
        {
            var record = ValidateArgs<BorrarItemRecord>(args);

            var repo = WorkContext.GetRepository<TJerarquia, IJerarquiaRepositoryAsync>();

            var item = await repo.LoadItem(record.Item.Id)
                ?? throw new System.NullReferenceException($"No se pudo cargar el item {record.Item.Nombre}");

            item.Padre.Remove(item);
        }
    }
}
