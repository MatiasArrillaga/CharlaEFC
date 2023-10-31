using Algoritmo.CharlaEFC.Application.General.Query;
using Algoritmo.CharlaEFC.Domain.BaseClasses.Interfaces;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.Microservices.Shared.Application.BaseClasses.General;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Services.Data;
using Algoritmo.Microservices.Shared.Portable.Enums.Jerarquias;
using Algoritmo.Microservices.Shared.Portable.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.General.CommandHandlers.Strategies
{
    //TODO - Por ahora va a llegar desde el Command. Pero se debería obtener desde el WC
    public record JerarquizarEntidadRecord(Jerarquia Jerarquia,JerarquiaItem ItemPadre, IEnumerable<IEntidadJerarquizable> Entidades);
    public class JerarquizarEntidadStrategy : BehaviourAdapterStrategy
    {
        public JerarquizarEntidadStrategy(IWorkContext wc) : base(wc){ }

        public override async Task RunAsync(params object[] args)
        {
            var record = ValidateArgs<JerarquizarEntidadRecord>(args);

            var jerarquia = await GetEntidad<Jerarquia, GetJerarquiaStrategy>(record.Jerarquia.Id)
                    ?? throw new NullReferenceException($"No se encontró la jerarquía {record.Jerarquia.Nombre}");

            await em.RunAsync(new TrackRamasStrategy<JerarquiaItem>(WorkContext), new TrackRamasRecord(record.Jerarquia.Id));

            var itemPadre = await GetEntidad<JerarquiaItem, GetJerarquiaItemStrategy>(record.ItemPadre.Id)
                ?? throw new NullReferenceException($"No se encontró el item {record.ItemPadre.Nombre}");

            if (itemPadre.Tipo is Domain.Jerarquias.Enum.TipoJerarquiaItem.TipoRaiz)
                throw new ApplicationException("No es posible jerarquizar sobre la raíz de la jerarquía");

            foreach (var e in record.Entidades.ToList())
            {
                //trackeo la entidad que se quiere jerarquizar
                var entidad = await GetEntidad(e);

                //Creo una Hoja con los datos recibidos y la agrego en el árbol.
                var itemHoja = (JerarquiaItem)jerarquia.AddHoja(itemPadre);

                //Jerarquizo el item
                if (entidad is IEntidadMaestraJerarquizable emj)
                {
                    itemHoja.Jerarquizar(emj);
                }
                else if (entidad is IEntidadTransaccionalJerarquizable etj)
                {
                    itemHoja.Jerarquizar(etj);
                }
                else
                {
                    throw new System.Exception($"La entidad {entidad.GetType().Name } no es jerarquizable");
                }
            }

            //Valido que la modificación sea correcta, antes de hacer commit
            jerarquia.ValidarModificacion(WorkContext);
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
