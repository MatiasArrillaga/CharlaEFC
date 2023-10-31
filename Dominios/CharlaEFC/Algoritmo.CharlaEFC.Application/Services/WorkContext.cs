using Algoritmo.Microservices.Shared.Domain.Infrastructure.Interfaces;
using Algoritmo.Microservices.Shared.Portable.BaseClasses;
using Algoritmo.CharlaEFC.Domain.BaseClasses;
using AutoMapper;
using localDomain = Algoritmo.CharlaEFC.Domain.Services;
using shared = Algoritmo.Microservices.Shared.Domain;
using sharedApp = Algoritmo.Microservices.Shared.Application.Services;

namespace Algoritmo.CharlaEFC.Application.Services
{
    /// <inheritdoc cref="localDomain.IWorkContext"/>
    public class WorkContext : sharedApp.WorkContext, localDomain.IWorkContext
    {
        /// <inheritdoc cref="localDomain.IWorkContext.Services"/>
        /// Oculta el miembro base creando uno de un nuevo tipo especializado para el microservicio.
        localDomain.Services localDomain.IWorkContext.Services { get; set; } = new localDomain.Services();

        public WorkContext() : base()
        {
        }
        public WorkContext(IMapper mapper) : base(mapper)
        {
        }

        /// <inheritdoc cref="localDomain.IWorkContext.Configure(shared.Services.IWorkContext, localDomain.IEntityManager)"/>
        public void Configure(shared.Services.IWorkContext sharedWorkContext, localDomain.IEntityManager entityManager)
        {
            SharedWorkContext = sharedWorkContext;
            // Configura la instancia local que opera con el contexto local
            var me = (localDomain.IWorkContext)this;
            me.Services.DateTimeManager = sharedWorkContext.Services.DateTimeManager;
            me.Services.UnitOfWork = sharedWorkContext.Services.UnitOfWork;
            me.Services.UnitOfWorkManager = sharedWorkContext.Services.UnitOfWorkManager;
            me.Services.ReadOnlyUnitOfWork = sharedWorkContext.Services.ReadOnlyUnitOfWork;
            me.Services.EntityManager = entityManager;
            me.Services.LogManager = sharedWorkContext.Services.LogManager;
            me.Services.DataBagManager = sharedWorkContext.Services.DataBagManager;
            me.Services.GlobalIdentifierManager = sharedWorkContext.Services.GlobalIdentifierManager;
            me.Services.GlobalConfiguration = sharedWorkContext.Services.GlobalConfiguration;
            me.Services.DTOManager = sharedWorkContext.Services.DTOManager;
            me.Request = sharedWorkContext.Request;
            // Establece el servicio de dominio para el que opera.
            me.DomainService = typeof(ICharlaEFC);

            // Establece el dominio en que está operando, es un dato que se establece de menor a mayor a diferencia del resto.
            sharedWorkContext.DomainService = me.DomainService;

            // Configura el EM para el contexto local
            me.Services.EntityManager.Configure(this);

            // Request que dio origen a la instancia.
            me.Request = sharedWorkContext.Request;

            // Configura la instancia base que opera con el contexto base y
            // en determinadas condiciones es invocada.
            base.Services.DateTimeManager = me.Services.DateTimeManager;
            base.Services.UnitOfWork = me.Services.UnitOfWork;
            base.Services.UnitOfWorkManager = me.Services.UnitOfWorkManager;
            base.Services.ReadOnlyUnitOfWork = me.Services.ReadOnlyUnitOfWork;
            base.Services.EntityManager = (sharedApp.EntityManager)me.Services.EntityManager;
            base.Services.DataBagManager = me.Services.DataBagManager;
            base.Services.LogManager = me.Services.LogManager;
            base.Services.GlobalIdentifierManager = me.Services.GlobalIdentifierManager;
            base.Services.GlobalConfiguration = me.Services.GlobalConfiguration;
            base.Services.DTOManager = me.Services.DTOManager;
            base.Request = me.Request;
            base.DomainService = me.DomainService;
        }

        /// <inheritdoc cref="shared.Services.IWorkContext.Configure(BaseRequest)"/>
        public override void Configure(BaseRequest request)
        {
            ((localDomain.IWorkContext)this).Request = request;
            base.Configure(request);
        }
        public override void Configure(IUnitOfWork uow)
        {
            // Local heredada
            base.Configure(uow);
            // Local reemplazada
            (this as localDomain.IWorkContext).Services.UnitOfWork = uow;
            // Shared compuesta
            SharedWorkContext.Services.UnitOfWork = uow;
        }
    }
}
