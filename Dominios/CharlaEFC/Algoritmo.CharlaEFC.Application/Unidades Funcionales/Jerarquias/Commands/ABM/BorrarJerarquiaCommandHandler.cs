using Algoritmo.CharlaEFC.Application.BaseClasses.Common;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Commands;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using Algoritmo.Microservices.Shared.Domain.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.Jerarquias.CommandHandlers
{
    public class BorrarJerarquiaCommandHandler : BaseCommandHandler<BorrarJerarquiaCommand, BorrarJerarquiaResponse>
    {
        public BorrarJerarquiaCommandHandler(IWorkContext context) : base(context) { }

        public override async Task<BorrarJerarquiaResponse> HandleDelegate(BorrarJerarquiaCommand command, CancellationToken cancellationToken)
        {
            var response = new BorrarJerarquiaResponse(command.CorrelationId);

            //Elimino la jerarquía
            await em.DeleteAsync<Jerarquia>(command.Id);

            response.Message = Localizer.GetRecursoAsync("EntidadEliminadaCorrectamente").Result;

            return response;
        }
    }
}
