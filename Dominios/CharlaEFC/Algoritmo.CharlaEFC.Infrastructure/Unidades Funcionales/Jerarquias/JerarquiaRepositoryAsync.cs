using Algoritmo.CharlaEFC.Domain.BaseClasses;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Enum;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Interfaces;
using Algoritmo.CharlaEFC.Infrastructure.Databases;
using Algoritmo.Microservices.Shared.Infrastructure.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.Infrastructure.Jerarquias.Repositories
{
    // <summary>
    // Repositorio de Asientos para operaciones de lecto/escritura.
    // </summary>
    public partial class JerarquiaRepositoryAsync : GenericRepositoryAsync<Jerarquia, CharlaEFCDbContext>, IJerarquiaRepositoryAsync
    {

        private DbSet<Jerarquia> _jerarquias;
        private DbSet<JerarquiaItem> _jerarquiasItem;
        private DbSet<JerarquiaNivel> _jerarquiaNivel;
        /// <summary>
        /// Genera una nueva instancia del repositorio asignando un 
        /// </summary>
        /// <param name="dbContext"></param>
        public JerarquiaRepositoryAsync(CharlaEFCDbContext dbContext) : base(dbContext)
        {
            _jerarquias = DbContext.Set<Jerarquia>();
            _jerarquiasItem = DbContext.Set<JerarquiaItem>();
            _jerarquiaNivel = DbContext.Set<JerarquiaNivel>();
        }               

        public async Task<JerarquiaItem> LoadItem(Guid itemId)
        {
            //Obtenemos el item con su padre
            var item = await _jerarquiasItem
                            .Include(i=> i.Padre)
                            .Include(i=> i.Hijos)
                            .SingleOrDefaultAsync(i => i.Id.Equals(itemId));

            foreach (var hijo in item.Hijos)
            {
                await LoadItem(hijo.Id);
            }
            return item;
        }

        public async Task<JerarquiaItem> LoadItem(Guid jerarquiaId, object entidadId) 
        {
            if (entidadId is int idInt)
            {

                return await LoadItem(jerarquiaId, idInt);
            }
            if (entidadId is Guid idGuid)
            {

                return await LoadItem(jerarquiaId, idGuid);
            }
            return null;
        }

        private async Task<JerarquiaItem> LoadItem(Guid jerarquiaId, int entidadId)
        {
            //Obtenemos el item con su padre
            var item = await _jerarquiasItem
                            .Include(i => i.Padre)
                            .SingleOrDefaultAsync(i => i.EntidadMaestraId.Equals(entidadId) 
                                                     && i.Jerarquia.Id.Equals(jerarquiaId)
                                                     && i.Tipo is TipoJerarquiaItem.TipoHoja);

            
            return item;
        }
        private async Task<JerarquiaItem> LoadItem(Guid jerarquiaId, Guid entidadId)
        {
            //Obtenemos el item con su padre
            var item = await _jerarquiasItem
                            .Include(i => i.Padre)
                            .SingleOrDefaultAsync(i => i.EntidadTransaccionalId.Equals(entidadId) && i.Jerarquia.Id.Equals(jerarquiaId));

            return item;
        }
    }

}
