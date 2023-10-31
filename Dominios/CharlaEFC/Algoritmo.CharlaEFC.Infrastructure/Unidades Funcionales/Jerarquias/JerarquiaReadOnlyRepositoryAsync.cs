using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Interfaces;
using Algoritmo.CharlaEFC.Infrastructure.Databases;
using Algoritmo.Microservices.Shared.Infrastructure.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Infrastructure.Jerarquias.Repositories
{
    /// <summary>
    /// Repositorio de asientos para operaciones de solo lectura.
    /// </summary>
    public partial class JerarquiaReadOnlyRepositoryAsync : GenericReadOnlyRepositoryAsync<Jerarquia, CharlaEFCDbContext>, IJerarquiaRepositoryAsync
    {
        private DbSet<Jerarquia> _jerarquias;
        public JerarquiaReadOnlyRepositoryAsync(CharlaEFCDbContext dbContext) : base(dbContext)
        {
            _jerarquias = dbContext.Jerarquia;
        }

        public Task<JerarquiaItem> LoadItem(System.Guid itemId)
        {
            throw new System.NotImplementedException();
        }

        public Task<JerarquiaItem> LoadItem(System.Guid jerarquiaId, object entidadId)
        {
            throw new System.NotImplementedException();
        }
    }
}