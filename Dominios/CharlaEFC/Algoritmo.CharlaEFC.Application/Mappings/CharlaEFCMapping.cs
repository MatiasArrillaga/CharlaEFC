using Algoritmo.Microservices.Shared.Application.Mappings;

namespace Algoritmo.CharlaEFC.Application.Mappings
{
    /// <summary>
    /// Clase donde se establecen los distintos mapeos de clases. El modo de mapear es < QueTipoDeClaseTengo , QueTipoDeClaseQuiero >
    /// </summary>
    public class CharlaEFCMapping : DefaultProfile
    {
        /// <summary>
        /// Genera una instancia de mapeos de la unidad funcional asientos
        /// </summary>
        public CharlaEFCMapping()
        {
           

        }

    }
}