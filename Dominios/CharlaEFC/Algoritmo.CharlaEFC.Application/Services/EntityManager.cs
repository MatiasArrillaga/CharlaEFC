using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Services.Data;
using System;
using System.Reflection;
using System.Threading.Tasks;
using sharedApp = Algoritmo.Microservices.Shared.Application;

namespace Algoritmo.CharlaEFC.Application.Services
{
    /// <inheritdoc cref="IEntityManager"/>
    public class EntityManager : sharedApp.Services.EntityManager, IEntityManager
    {
        /// <summary>
        /// Reemplaza el contexto de trabajo para el tipo local.
        /// </summary>
        public new IWorkContext WorkContext { get; set; }

        /// <inheritdoc cref="IEntityManager.Configure(IWorkContext)"/>
        public void Configure(IWorkContext workContext)
        {
            // Configura la instancia local que opera con el contexto local
            WorkContext = workContext;

            // Configura la instancia base que opera con el contexto base y
            // en determinadas condiciones es invocada.
            base.WorkContext = WorkContext;
        }

        public async Task<IJerarquia?> GetJerarquiaByCodeAsync(string code, GraphExplorerConfiguration? graphExplorer=null)
        {
            graphExplorer = graphExplorer ?? GraphExplorerConfiguration.GetFull();
            var j = await WorkContext.Services.EntityManager.GetByCodeAsync<Jerarquia>(graphExplorer, code);
            if (j is null) return null;

            ConfigurarJerarquia(graphExplorer, j);

            return j;
        }

        public async Task<IJerarquia?> GetJerarquiaByIdAsync(object id, GraphExplorerConfiguration? graphExplorer = null)
        {
            graphExplorer = graphExplorer ?? GraphExplorerConfiguration.GetFull();
            var j = await WorkContext.Services.EntityManager.GetByIdAsync<Jerarquia>(graphExplorer, id);
            if (j is null) return null;

            return j;
        }
        private void ConfigurarJerarquia(GraphExplorerConfiguration graphExplorer, Jerarquia j)
        {
            j.Configure(WorkContext);

            if (graphExplorer.Depth.Equals(Depth.Full))
            {
                j.ConfigurarHojas(WorkContext)
                 .CargarEntidades();
            }
        }

        private static MethodInfo? _getByIdAsyncGenericMethod = default;
        private static MethodInfo? _getByIdAsyncSpecificMethod = default;
        private static Type? _getByIdAsyncSpecificType = default;
        public async Task<IEntidadJerarquizable?> GetEntidadAsync(IJerarquiaItem jerarquiaItem)
        {
            // Recupera el tipo de la entidad
            var entityType = System.Type.GetType(jerarquiaItem.TipoEntidadAssembly, false, true)
                ?? throw new NullReferenceException($"No se pudo recuperar el tipo { jerarquiaItem.TipoEntidadFullName }.");

            // Recupera y cachea el método genérico
            _getByIdAsyncGenericMethod = _getByIdAsyncGenericMethod
                ?? WorkContext.Services.EntityManager.GetType().GetMethod(nameof(IEntityManager.GetByIdAsync), new Type[] { typeof(GraphExplorerConfiguration), typeof(object[]) })
                ?? throw new NullReferenceException($"No se pudo recuperar el método { nameof(IEntityManager.GetByIdAsync) }.");

            // Recupera y cachea el método específico.
            _getByIdAsyncSpecificMethod = _getByIdAsyncSpecificType == entityType
                ? _getByIdAsyncSpecificMethod
                : _getByIdAsyncGenericMethod.MakeGenericMethod(entityType);
            // Guarda el último tipo invocado.
            _getByIdAsyncSpecificType = entityType;

            // Invoca el método con el id concreto de la entidad
            object? v = default;
            try
            {
                var ps = new object[] { GraphExplorerConfiguration.GetDefault(), new object[] { jerarquiaItem.GetEntidadId() } };
                var t = _getByIdAsyncSpecificMethod.Invoke(WorkContext.Services.EntityManager, ps);
                // y recupero el resultado de una operación asíncrona.
                await (t as Task).ConfigureAwait(false);

                var rProp = t.GetType().GetProperty("Result");
                v = rProp.GetValue(t);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo crear la recuperar la instancia { jerarquiaItem.GetEntidadId() } de la entidad { jerarquiaItem.TipoEntidadFullName } para el ítem de jerarquía { jerarquiaItem.Id }.", ex);
            }
            return v is not null ? v as IEntidadJerarquizable : null;
        }

       
    }
}
