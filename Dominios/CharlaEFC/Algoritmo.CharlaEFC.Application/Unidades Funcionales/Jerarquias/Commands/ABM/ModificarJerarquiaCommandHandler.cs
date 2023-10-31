using Algoritmo.CharlaEFC.Application.BaseClasses.Common;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Commands;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using Algoritmo.Microservices.Shared.Application.Extensions;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Localization;
using Algoritmo.Microservices.Shared.Domain.Services.Data;
using Algoritmo.Microservices.Shared.Portable.BaseClassesDTO;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using FluentValidation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.Jerarquias.CommandHandlers
{
    #region Validator
    public class ModificarJerarquiaCommandValidator : AbstractValidator<ModificarJerarquiaCommand>
    {
        public ModificarJerarquiaCommandValidator()
        {
            RuleFor(a => a.Jerarquia)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Jerarquia.Id)
                .NotEmpty()
                .NotNull();
            //.NotEqual(0);
        }
    }
    #endregion

    public class ModificarJerarquiaCommandHandler : BaseCommandHandler<ModificarJerarquiaCommand, ModificarJerarquiaResponse>
    {
        public ModificarJerarquiaCommandHandler(IWorkContext context) : base(context) { }
        public override async Task<ModificarJerarquiaResponse> HandleDelegate(ModificarJerarquiaCommand command, CancellationToken cancellationToken)
        {
            WorkContext.Services.UnitOfWork.EntitiesVersionController.Disable();
            //se genera un nuevo response ligado al CorrelationID de la query.
            //de esta forma se mantiene una relación entre ambos por ID
            var response = new ModificarJerarquiaResponse(command.CorrelationId);

            //Mapeo el command a la clase Jerarquía
            var jerarquia = ((IJerarquia)em.ToEntity(command.Jerarquia));

            // Dado que se usa el mismo handler para modificar distintos tipos de Jerarquías,
            // se invoca una sobrecarga del UpdateAsync que a partir de la interfaz la entidad modifica según la Jerarquías que corresponde
            var graf = new GraphExplorerConfiguration();
            graf.Depth = Depth.Full;
            graf.Include<JerarquiaNivel>();

            var updateMode = new UpdateConfiguration()
            {
                Mode = UpdateConfiguration.UpdateMode.Differential,
                GraphExplorerConfiguration = graf
            };

            await em.UpdateAsync(jerarquia, updateMode, true);

            // Vincula un evento para recuperar el id, se ejecuta primero.
            WorkContext.OnSuccess += WorkContext_OnSuccess;
            return response;
        }
        private void WorkContext_OnSuccess(object sender, Microservices.Shared.Domain.Services.Events.WorkContextEvents.OnSuccessfulRequestEventArgs e)
        {
            //El sender es el work context.
            var wc = (IWorkContext)sender;

            // Especializa la respuesta
            var response = (ModificarJerarquiaResponse)e.Response;

            // Mapea ya con los datos generados por la Base de Datos
            response.Message = Localizer.GetRecursoAsync("EntidadActualizadaCorrectamente").Result;
        }
    }
}
