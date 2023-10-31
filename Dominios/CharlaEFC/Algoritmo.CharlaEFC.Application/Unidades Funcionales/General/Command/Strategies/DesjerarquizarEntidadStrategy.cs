using Algoritmo.CharlaEFC.Application.General.Query;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.Microservices.Shared.Application.BaseClasses.General;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Services.Data;
using Algoritmo.Microservices.Shared.Portable.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.General.CommandHandlers.Strategies
{
    //TODO - Por ahora va a llegar desde el Command. Pero se debería obtener desde el WC
    public record DesjerarquizarEntidadRecord(Guid JerarquiaId, IEnumerable<IEntidadJerarquizable> Entidades);
    public class DesjerarquizarEntidadStrategy : BehaviourAdapterStrategy
    {
        public DesjerarquizarEntidadStrategy(IWorkContext wc) : base(wc) { }

        public override async Task RunAsync(params object[] args)
        {
            var record = ValidateArgs<DesjerarquizarEntidadRecord>(args);

            //Preparo la query con todos los items de una jerarquía específica
            var repo = WorkContext.GetRepository<JerarquiaItem>();
            var query = repo.Entities
                            .Where(j => j.Jerarquia.Id.Equals(record.JerarquiaId));

            foreach (var entidad in record.Entidades.ToList())
            {
                //filtro la query para obtener la hoja específica 
                var itemHoja = query.Single(j => j.EntidadMaestraId.Equals(entidad.Id));
                //Obtengo la entidad completa, necesito las hojas
                var entity = await em.GetByIdAsync(GraphExplorerConfiguration.GetFull(), entidad);

                entity.Desjerarquizar(itemHoja);

                BorrarItemRecord borrarItemRecord = new(itemHoja);
                await em.RunAsync(new BorrarItemStrategy<Jerarquia>(WorkContext), borrarItemRecord);
            }

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
        private async Task<IEntity> GetEntidad(IEntidadJerarquizable entidad)
        {
            //// Determina el tipo de entidad en función del tipo del DTO
            //var entityType = WorkContext.Services.DTOManager.GetMappedType(entidad.GetType()).FirstOrDefault();
            //var entidadAux = (IEntity)((IMappable)entidad).mapMe(entityType);

            // Carga la entidad a vincular, es otro agregado que hay que modificar.
            return await em.GetByIdAsync(GraphExplorerConfiguration.GetDefault(), entidad);
        }
        #endregion

    }
}
