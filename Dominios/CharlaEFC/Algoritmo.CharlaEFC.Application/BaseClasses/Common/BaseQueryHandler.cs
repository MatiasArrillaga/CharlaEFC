using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Domain.Services;
using MediatR;
using sharedCommon = Algoritmo.Microservices.Shared.Application.BaseClasses.Common;
using sharedDomain = Algoritmo.Microservices.Shared.Domain;
using localDomain = Algoritmo.CharlaEFC.Domain.Services;
using System;


namespace Algoritmo.CharlaEFC.Application.BaseClasses.Common
{
    /// <summary>
    /// Clase base para handler de comandos de CharlaEFC.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class BaseQueryHandler<TRequest, TResponse> : sharedCommon.BaseQueryHandler<TRequest, TResponse>
        where TRequest : BaseQueryPortable<TResponse>, IRequest<TResponse>
        where TResponse : BaseQueryPortableResponse
    {
        /// <summary>
        /// Acceso directo al Administrador de Entidades.
        /// Importante: Opera con la instancia local del work context.
        /// </summary>
        public new localDomain.IEntityManager em => WorkContext.Services.EntityManager ?? throw new NullReferenceException();
        /// <summary>
		/// Retorna o establece el contexto de ejecución del handler.
		/// </summary>
        public new IWorkContext WorkContext { get; set; }

        /// <inheritdoc cref="shared.BaseQueryHandler{TRequest, TResponse}.BaseQueryHandler(IWorkContext)"/>
        public BaseQueryHandler(IWorkContext workContext) : base((sharedDomain.Services.IWorkContext)workContext)
        {
            // Setea la instancia local, la general ya fue configurada previamente.
            WorkContext = workContext;
        }
    }
}
