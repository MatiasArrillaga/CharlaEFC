using Algoritmo.Microservices.Shared.Portable.BaseClasses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using localDomain = Algoritmo.CharlaEFC.Domain.Services;
using sharedDomain = Algoritmo.Microservices.Shared.Domain;

namespace Algoritmo.CharlaEFC.Application.Behaviours
{
    /// <summary>
    /// Middleware para configuración del contexto de trabajo particular.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class WorkContextBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : BaseRequest, IRequest<TResponse>
        where TResponse : BaseResponse
    {
        private localDomain.IWorkContext _localWorkContext;
        public WorkContextBehaviour(sharedDomain.Services.IWorkContext sharedWorkContext, 
            localDomain.IWorkContext localWorkContext,
            localDomain.IEntityManager entityManager)
        {
            _localWorkContext = localWorkContext;
            // Importante: Configura el contexto local dado que si bien hay una herencia
            // en tiempo de ejecución son instancias diferentes el local y el shared.
            localWorkContext.Configure(sharedWorkContext, entityManager);
            // Vincula el evento shared con los eventos locales.
            sharedWorkContext.OnSuccess += SharedWorkContext_OnSuccess;
            sharedWorkContext.OnFailure += SharedWorkContext_OnFailure;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Configura el request en el wc local.
            _localWorkContext.Configure(request);
            return await next();
        }

        /// <summary>
        /// Ejecuta los eventos configurados localmente a partir del evento publicado por el shared.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SharedWorkContext_OnSuccess(object sender, sharedDomain.Services.Events.WorkContextEvents.OnSuccessfulRequestEventArgs e)
        {
            _localWorkContext.SuccessReached<BaseRequest, BaseResponse>(_localWorkContext, e.Request, e.Response);
        }

        /// <summary>
        /// Ejecuta los eventos configurados localmente a partir del evento publicado por el shared.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SharedWorkContext_OnFailure(object sender, sharedDomain.Services.Events.WorkContextEvents.OnFailedRequestEventArgs e)
        {
            _localWorkContext.FailureReached<BaseRequest, BaseResponse>(_localWorkContext, e.Request, e.Response);
        }
    }
}
