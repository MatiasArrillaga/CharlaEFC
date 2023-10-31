using Algoritmo.CharlaEFC.Application.BaseClasses.Common;
using Algoritmo.CharlaEFC.Application.General.CommandHandlers.Strategies;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.General.Commands;
using Algoritmo.CharlaEFC.Portable.General.Responses.Commands;
using Algoritmo.Microservices.Shared.Application.Extensions;
using Algoritmo.Microservices.Shared.Domain.Localization;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.General.CommandHandlers
{
    #region Validator
    public class BorrarItemCommandValidator : AbstractValidator<BorrarItemCommand>
    {
        public BorrarItemCommandValidator()
        {
            RuleFor(a => a.Item)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Item.Jerarquia)
                .NotEmpty()
                .NotNull();
        }
    }
    #endregion
    public class BorrarItemCommandHandler : BaseCommandHandler<BorrarItemCommand, BorrarItemResponse>
    {
        public BorrarItemCommandHandler(IWorkContext workContext) : base(workContext) { }

        /// <inheritdoc cref="BorrarItemCommand"/>
        public override async Task<BorrarItemResponse> HandleDelegate(BorrarItemCommand command, CancellationToken cancellationToken)
        {
            var response = new BorrarItemResponse(command.CorrelationId);

            BorrarItemRecord record = new(command.Item.mapMe<JerarquiaItem>());
            await em.RunAsync(new BorrarItemStrategy<Jerarquia>(WorkContext), record);

            // Vincula un evento para recuperar el id, se ejecuta primero.
            WorkContext.OnSuccess += WorkContext_OnSuccess;
            return response;
        }

        private void WorkContext_OnSuccess(object sender, Microservices.Shared.Domain.Services.Events.WorkContextEvents.OnSuccessfulRequestEventArgs e)
        {
            WorkContext.Services.UnitOfWork.EntitiesVersionController.Disable();
            //El sender es el work context.
            var wc = (IWorkContext)sender;

            // Especializa la respuesta
            var response = (BorrarItemResponse)e.Response;

            response.Message = Localizer.GetRecursoAsync("ItemBorradoCorrectamente").Result;
        }

    }
}
