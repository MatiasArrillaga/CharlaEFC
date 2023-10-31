using Algoritmo.CharlaEFC.Application.BaseClasses.Common;
using Algoritmo.CharlaEFC.Application.General.Query;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Enum;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.General.Commands;
using Algoritmo.CharlaEFC.Portable.General.Responses.Commands;
using Algoritmo.Microservices.Shared.Application.BaseClasses.General;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
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
    public class MoverItemCommandValidator : AbstractValidator<MoverItemCommand>
    {
        public MoverItemCommandValidator()
        {
            //RuleFor(a => a.CodigoDestino)
            //    .NotEmpty()
            //    .NotNull()
            //    .MaximumLength(150);

            RuleFor(a => a.Item)
                .NotEmpty()
                .NotNull();
        }
    }
    #endregion
    public class MoverItemCommandHandler : BaseCommandHandler<MoverItemCommand, MoverItemResponse>
    {
        public MoverItemCommandHandler(IWorkContext workContext) : base(workContext) { }

        /// <inheritdoc cref="MoverItemCommand"/>
        public override async Task<MoverItemResponse> HandleDelegate(MoverItemCommand command, CancellationToken cancellationToken)
        {
            WorkContext.Services.UnitOfWork.EntitiesVersionController.Disable();
            var response = new MoverItemResponse(command.CorrelationId);

            var jerarquia = await GetEntidad<Jerarquia, GetJerarquiaStrategy>(command.Item.Jerarquia.Id.Value)
                   ?? throw new NullReferenceException($"No se encontró la jerarquía {command.Item.Jerarquia.Nombre}");

            var itemMovible = await GetEntidad<JerarquiaItem, GetJerarquiaItemStrategy>(command.Item.Id.Value)
              ?? throw new NullReferenceException($"No se encontró el item {command.Item.Nombre}");

            var itemDestino = await GetEntidad<JerarquiaItem, GetJerarquiaItemStrategy>(command.IdDestino)
                        ?? throw new NullReferenceException($"No se encontró el destino {command.IdDestino}");
           
            if (itemDestino.Tipo is TipoJerarquiaItem.TipoHoja) 
            { 
                throw new Exception($"El item Destino {itemDestino.Nombre} no puede ser una hoja"); 
            }


            jerarquia.MoverItem(itemMovible, itemDestino);

            //Valido que la modificación sea correcta, antes de hacer commit
            jerarquia.ValidarModificacion(WorkContext);

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

            var response = (MoverItemResponse)e.Response;
            response.Message = Localizer.GetRecursoAsync("ItemMovidoCorrectamente").Result;
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

