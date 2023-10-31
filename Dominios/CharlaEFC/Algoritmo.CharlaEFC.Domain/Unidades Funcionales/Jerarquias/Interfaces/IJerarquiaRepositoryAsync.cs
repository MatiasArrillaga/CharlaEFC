using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Interface;
using Algoritmo.Microservices.Shared.Domain.Infrastructure.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Interfaces
{
    //<summary>
    //Extiende la funcionalidad básica del repositorio genérico
    //</summary>
    public interface IJerarquiaRepositoryAsync : IGenericRepositoryAsync<Jerarquia>
    {
        Task<JerarquiaItem> LoadItem(System.Guid itemId);
        Task<JerarquiaItem> LoadItem(System.Guid jerarquiaId, object entidadId); 
    }    

}
