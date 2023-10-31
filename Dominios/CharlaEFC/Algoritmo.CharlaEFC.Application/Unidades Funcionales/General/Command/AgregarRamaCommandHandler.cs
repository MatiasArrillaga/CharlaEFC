using Algoritmo.CharlaEFC.Application.BaseClasses.Common;
using Algoritmo.CharlaEFC.Application.General.CommandHandlers.Strategies;
using Algoritmo.CharlaEFC.Application.General.Query;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.General.Commands;
using Algoritmo.CharlaEFC.Portable.General.Responses.Commands;
using Algoritmo.Microservices.Shared.Application.BaseClasses.General;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Localization;
using Algoritmo.Microservices.Shared.Portable.Utils;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.General.CommandHandlers
{
    #region Validator
    public class AgregarRamaCommandValidator : AbstractValidator<AgregarRamaCommand>
    {
        public AgregarRamaCommandValidator()
        {
            RuleFor(a => a.Codigo)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10);

            RuleFor(a => a.Nombre)
                .NotEmpty()
                .NotNull()
                .MaximumLength(150);

            RuleFor(a => a.ItemPadre)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.ItemPadre.Jerarquia)
                .NotEmpty()
                .NotNull();
        }
    }
    #endregion
    public class AgregarRamaCommandHandler : BaseCommandHandler<AgregarRamaCommand, AgregarRamaResponse>
    {
        public AgregarRamaCommandHandler(IWorkContext workContext) : base(workContext) { }

        /// <inheritdoc cref="AgregarRamaCommand"/>
        public override async Task<AgregarRamaResponse> HandleDelegate(AgregarRamaCommand command, CancellationToken cancellationToken)
        {
            var response = new AgregarRamaResponse(command.CorrelationId);

            var jerarquia = await GetEntidad<Jerarquia, GetJerarquiaStrategy>(command.ItemPadre.Jerarquia.Id.Value)
                   ?? throw new NullReferenceException($"No se encontró la jerarquía {command.ItemPadre.Jerarquia.Nombre}");


            await em.RunAsync(new TrackRamasStrategy<JerarquiaItem>(WorkContext), new TrackRamasRecord(jerarquia.Id));

            var itemPadre = await GetEntidad<JerarquiaItem, GetJerarquiaItemStrategy>(command.ItemPadre.Id.Value)
                         ?? throw new NullReferenceException($"No se encontró el item {command.ItemPadre.Nombre}");

            var item = jerarquia.AddRama(command.Codigo, command.Nombre, itemPadre);

            //Valido que la modificación sea correcta, antes de hacer commit
            jerarquia.ValidarModificacion(WorkContext);


            WorkContext.Services.DataBagManager.Add(command.CorrelationId, jerarquia);
            WorkContext.OnSuccess += WorkContext_OnSuccess;

            return response;
        }

        private void WorkContext_OnSuccess(object sender, Microservices.Shared.Domain.Services.Events.WorkContextEvents.OnSuccessfulRequestEventArgs e)
        {

            WorkContext.Services.UnitOfWork.EntitiesVersionController.Disable();
            //El sender es el work context.
            var wc = (IWorkContext)sender;

            // Especializa la respuesta

            var response = (AgregarRamaResponse)e.Response;
            var jerarquia = wc.Services.DataBagManager.Pop<IJerarquia>(wc.Request.CorrelationId);

            response.Message = Localizer.GetRecursoAsync("ItemAgregadoCorrectamente").Result;
        }

        #region Methods

        /// <summary>
        /// Metodo local que ejecuta la estrategia indicada y retorna una unica entidad. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TStrategy"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<TEntity?> GetEntidad<TEntity, TStrategy>(Guid? id)
            where TEntity : class, IEntity
            where TStrategy : GetEntidadStrategy
        {
            var sttgyParams = new GetEntidadStrategyParams(id, null, null,
                                               PaginationInfo: new BasePaginationInfo() { Start = 1, Length = 1 },
                                               Filters: new List<FieldWhereDefinition>(),
                                               OrderBy: new List<FieldOrderDefinition>());

            var sttgy = (GetEntidadStrategy)Activator.CreateInstance(typeof(TStrategy), new object[] { WorkContext, sttgyParams });

            var result = await em.GetPagedAsync<TEntity>(sttgy, sttgyParams);

            return result.Items.FirstOrDefault();
        }

        #endregion
    }
}
