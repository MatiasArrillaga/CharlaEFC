using Algoritmo.Microservices.Shared.Domain.BaseClasses.Dominio;
using Algoritmo.CharlaEFC.Domain.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace Algoritmo.CharlaEFC.Domain.BaseClasses
{
    public abstract class BaseCharlaEFC: BaseEntidadSeguridad, ICharlaEFC
    { 
        /// <summary>
      /// Retorna o establece el contexto de trabajo local.
      /// </summary>
        [NotMapped]
        public new IWorkContext? WorkContext { get; private set; }
        public override bool Configure(Microservices.Shared.Domain.Services.IWorkContext workContext)
        {
            WorkContext = workContext as IWorkContext;
            return base.Configure(workContext);
        }
    }
}
