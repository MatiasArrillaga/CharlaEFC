using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;

namespace Algoritmo.CharlaEFC.Domain.BaseClasses
{
    /// <summary>
    /// Clase base para entidades maestras.
    /// </summary>
    public class EntidadMaestra : BaseCharlaEFC, IMasterEntity
    {
        public string Codigo { get; set; } = null!;


    }
}
