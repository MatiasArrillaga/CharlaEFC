using Algoritmo.CharlaEFC.Application.BaseClasses.Common;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Commands;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using Algoritmo.Microservices.Shared.Application.Extensions;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Localization;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using FluentValidation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.Jerarquias.CommandHandlers
{
    #region Validator
    public class CrearJerarquiaCommandValidator : AbstractValidator<CrearJerarquiaCommand>
    {
        public CrearJerarquiaCommandValidator()
        {
            RuleFor(a => a.Jerarquia)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Jerarquia.Codigo)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10);

            RuleFor(a => a.Jerarquia.Nombre)
                .NotEmpty()
                .NotNull()
                .MaximumLength(150);

            RuleFor(a => a.Jerarquia.TipoEntidadFullName)
                .NotEmpty()
                .NotNull();
            RuleFor(a => a.Jerarquia.TipoEntidadAssembly)
                .NotEmpty()
                .NotNull();

        }
    }
    #endregion
    public class CrearJerarquiaCommandHandler : BaseCommandHandler <CrearJerarquiaCommand, CrearJerarquiaResponse>
    {
        public CrearJerarquiaCommandHandler(IWorkContext workContext) : base(workContext) { }

        /// <summary>
        /// Proceso por el cual se podrá crear una Jerarquía. <br/>
        /// <i>Una Jerarquía permite definir las condiciones bajo las que se puede crear su respectivo árbol jerarquizado </i>
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response del tipo <see cref="CrearJerarquiaResponse"/> con una instancia de la jerarquía creada</returns>
        public override async Task<CrearJerarquiaResponse> HandleDelegate(CrearJerarquiaCommand command, CancellationToken cancellationToken)
        {             
            var response = new CrearJerarquiaResponse(command.CorrelationId);
            
            var jerarquia = ((IJerarquia)em.ToEntity(command.Jerarquia))
                            .SetEntityType(WorkContext.Services.DTOManager.GetMappedType(Type.GetType(command.Jerarquia.TipoEntidadAssembly)).FirstOrDefault());

            //Al crearse una nueva jerarquía, se inicializa el árbol con el primer item Raíz.
            //Este item tendrá el mismo Código y Nombre que la Jerarquía.
            jerarquia.ArbolInitialize();

            // Dado que se usa el mismo handler para crear distintos tipos de jerarquías,
            // se invoca una sobrecarga del CreateAsync que a partir de la interfaz la entidad y lo grabe como tal
            jerarquia = (IJerarquia)await em.CreateAsync(jerarquia);
            WorkContext.Services.DataBagManager.Add($"dtoType_{WorkContext.Request.CorrelationId}", command.Jerarquia.GetType());
            WorkContext.Services.DataBagManager.Add(response.CorrelationId, jerarquia);

            // Vincula un evento para recuperar el id, se ejecuta primero.
            WorkContext.OnSuccess += WorkContext_OnSuccess;

            return response;
        }

        private void WorkContext_OnSuccess(object sender, Microservices.Shared.Domain.Services.Events.WorkContextEvents.OnSuccessfulRequestEventArgs e)
        {
            //El sender es el work context.
            var wc = (IWorkContext)sender;

            // Especializa la respuesta

            var response = (CrearJerarquiaResponse)e.Response;
            var jerarquia = wc.Services.DataBagManager.Pop<IJerarquia>(wc.Request.CorrelationId);
            var dtoType = wc.Services.DataBagManager.Pop<Type>($"dtoType_{WorkContext.Request.CorrelationId}");

            jerarquia.SetEntityType(WorkContext.Services.DTOManager.GetMappedType(Type.GetType(jerarquia.TipoEntidadAssembly)).FirstOrDefault());

            // Mapea ya con los datos generados por la Base de Datos
            response.Jerarquia = (IJerarquiaDTO)jerarquia.mapMe(dtoType);
            response.Message = Localizer.GetRecursoAsync("EntidadCreadaCorrectamente").Result;
        }
    }
}
