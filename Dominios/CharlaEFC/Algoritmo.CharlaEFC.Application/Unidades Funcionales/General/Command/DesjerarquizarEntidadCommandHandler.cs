using Algoritmo.CharlaEFC.Application.BaseClasses.Common;
using Algoritmo.CharlaEFC.Application.General.CommandHandlers.Strategies;
using Algoritmo.CharlaEFC.Application.General.Query;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.General.Commands;
using Algoritmo.CharlaEFC.Portable.General.Responses.Commands;
using Algoritmo.Microservices.Shared.Application.BaseClasses.General;
using Algoritmo.Microservices.Shared.Application.Extensions;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Localization;
using Algoritmo.Microservices.Shared.Domain.Services.Data;
using Algoritmo.Microservices.Shared.Portable.Interfaces;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
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
    public class DesjerarquizarEntidadCommandValidator : AbstractValidator<DesjerarquizarEntidadCommand>
    {
        public DesjerarquizarEntidadCommandValidator()
        {
            RuleFor(a => a.Entidades)
                .NotEmpty()
                .NotNull();
        }
    }
    #endregion
    public class DesjerarquizarEntidadCommandHandler : BaseCommandHandler<DesjerarquizarEntidadCommand, DesjerarquizarEntidadResponse>
    {
        public DesjerarquizarEntidadCommandHandler(IWorkContext workContext) : base(workContext) { }

        /// <inheritdoc cref="DesjerarquizarEntidadCommand"/>
        public override async Task<DesjerarquizarEntidadResponse> HandleDelegate(DesjerarquizarEntidadCommand command, CancellationToken cancellationToken)
        {
            WorkContext.Services.UnitOfWork.EntitiesVersionController.Disable();

            var response = new DesjerarquizarEntidadResponse(command.CorrelationId);
            var entityType = WorkContext.Services.DTOManager.GetMappedType(command.Entidades.First().GetType()).FirstOrDefault();
            var entidades = command.Entidades.Select(entidad => ((IMappable)entidad).mapMe(entityType)).Cast<IEntidadJerarquizable>();

            DesjerarquizarEntidadRecord record = new(command.JerarquiaId,
                                                     entidades);

            await em.RunAsync(new DesjerarquizarEntidadStrategy(WorkContext), record);

            // Vincula un evento para recuperar el id, se ejecuta primero.
            WorkContext.OnSuccess += WorkContext_OnSuccess;

            return response;
        }
        private void WorkContext_OnSuccess(object sender, Microservices.Shared.Domain.Services.Events.WorkContextEvents.OnSuccessfulRequestEventArgs e)
        {
            //El sender es el work context.
            var wc = (IWorkContext)sender;

            // Especializa la respuesta

            var response = (DesjerarquizarEntidadResponse)e.Response;
            
            response.Message = Localizer.GetRecursoAsync("ItemJerarquizadoCorrectamente").Result;
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

        /// <summary>
        /// Obtiene la entidad que se quiere jerarquizar
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        private async Task<IEntity> GetEntidad(IEntidadJerarquizableDTO entidad)
        {
            // Determina el tipo de entidad en función del tipo del DTO
            var entityType = WorkContext.Services.DTOManager.GetMappedType(entidad.GetType()).FirstOrDefault();
            var entidadAux = (IEntity)((IMappable)entidad).mapMe(entityType);

            // Carga la entidad a vincular, es otro agregado que hay que modificar.
            return await em.GetByIdAsync(GraphExplorerConfiguration.GetDefault(), entidadAux);
        }        
        #endregion
    }
}
