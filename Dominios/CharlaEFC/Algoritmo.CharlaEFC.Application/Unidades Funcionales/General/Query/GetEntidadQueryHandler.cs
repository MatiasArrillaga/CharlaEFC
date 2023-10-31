using Algoritmo.CharlaEFC.Domain.Services;
using Algoritmo.CharlaEFC.Portable.General.Queries;
using Algoritmo.CharlaEFC.Portable.General.Responses;
using Algoritmo.Microservices.Shared.Application.BaseClasses.General;
using System.Threading;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Application.General.Queries
{
    /// <summary>
    /// Clase controladora de la petición GetEntidad
    /// </summary>
    public class GetEntidadQueryHandler : BaseGetEntidadQueryHandler<GetEntidadQuery, GetEntidadResponse, object>
    {
        /// <inheritdoc cref="BaseQueryHandler.BaseQueryHandler(IWorkContext)"/>
        public GetEntidadQueryHandler(IWorkContext context) : base(context)
        {
            ConfigureEntityMappingsResolver();
        }

        private void ConfigureEntityMappingsResolver()
        {
            // IMPORTANTE: Aquí se deben configurar los resolvers.
            // 1: cadena con el nombre del tipo de la entidad.
            // - Desde aquí es un record -
            // 2: Tipo de la entidad.
            // 3: Tipo del DTO que representa a la entidad.
            // 4: Tipo de la strategy que accede a los datos.

            //AddInventoryResolver(new(typeof(CategoriaImpuestoCodigoFiscal), typeof(CategoriaImpuestoCodigoFiscalDTO), typeof(GetCategoriaImpuestoCodigoFiscalStrategy)));
           
        }
        public override async Task<GetEntidadResponse> HandleDelegate(GetEntidadQuery query, CancellationToken cancellationToken)
        {
            return await base.HandleDelegate(query, cancellationToken);
        }

    }
}
