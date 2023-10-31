using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.BaseClasses;
using MediatR;
using System;
using localDomain = Algoritmo.CharlaEFC.Domain.Services;
using sharedCommon = Algoritmo.Microservices.Shared.Application.BaseClasses.Common;
using sharedDomain = Algoritmo.Microservices.Shared.Domain;

namespace Algoritmo.CharlaEFC.Application.BaseClasses.Common
{
	/// <summary>
	/// Clase base para handlers de comandos de CharlaEFC.
	/// </summary>
	/// <typeparam name="TRequest"></typeparam>
	/// <typeparam name="TResponse"></typeparam>
	public class BaseCommandHandler<TRequest, TResponse> : sharedCommon.BaseCommandHandler<TRequest, TResponse>
		where TRequest : BaseCommandPortable<TResponse>, IRequest<TResponse>
		where TResponse : BaseCommandPortableResponse
	{ 
		/// <summary>
		/// Acceso directo al Administrador de Entidades.
		/// </summary>
		public new localDomain.IEntityManager em => WorkContext.Services.EntityManager ?? throw new NullReferenceException();
		/// <summary>
		/// Retorna o establece el contexto de ejecución del handler.
		/// </summary>
		public new IWorkContext WorkContext{ get; set; }

		/// <inheritdoc cref="shared.BaseCommandHandler{TRequest, TResponse}.BaseCommandHandler(IWorkContext)"/>
		public BaseCommandHandler(IWorkContext workContext) : base((sharedDomain.Services.IWorkContext)workContext)
		{
			// Setea la instancia local, la general ya fue configurada previamente.
			WorkContext = workContext;
		}
	}
	public static class BaseCommandHandlerExtensions
	{
		/// <inheritdoc cref="sharedCommon.BaseCommandHandlerExtensions.AsTransactionFullScoped{TRequest, TResponse}(sharedCommon.BaseCommandHandler{TRequest, TResponse})"/>
		public static void AsTransactionFullScoped<TRequest, TResponse>(this BaseCommandHandler<TRequest, TResponse> handler)
			where TRequest : BaseCommandPortable<TResponse>, IRequest<TResponse>
			where TResponse : BaseCommandPortableResponse
		{
			var suow = handler.WorkContext?.Services?.UnitOfWorkManager?.GetScopedUnitOfWork<TRequest>();
			handler?.WorkContext?.Configure(suow);
		}
	}
}
