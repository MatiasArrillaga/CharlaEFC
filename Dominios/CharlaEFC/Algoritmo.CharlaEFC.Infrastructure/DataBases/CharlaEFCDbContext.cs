using Algoritmo.CharlaEFC.Domain.BaseClasses;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.Microservices.Shared.Domain.BaseClasses.Domain.Interface;
using Algoritmo.Microservices.Shared.Domain.Infrastructure.Interfaces;
using Algoritmo.Microservices.Shared.Domain.Services;
using Algoritmo.Microservices.Shared.Infrastructure.DataBases;
using Algoritmo.Microservices.Shared.Infrastructure.DataBases.BaseEnumeration.Commons;
using Algoritmo.Microservices.Shared.Infrastructure.DataBases.BaseEnumeration.Dapper;
using Algoritmo.Microservices.Shared.Infrastructure.DataBases.BaseEnumeration.EFCore;
using Algoritmo.Microservices.Shared.Infrastructure.DataBases.Commnon;
using Algoritmo.Microservices.Shared.Infrastructure.DataBases.Interceptors;
using Algoritmo.Microservices.Shared.Infrastructure.DataBases.Jerarquias.Common;
using Algoritmo.Microservices.Shared.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using static Algoritmo.CharlaEFC.Domain.BaseClasses.EntidadMaestraJerarquizable;

namespace Algoritmo.CharlaEFC.Infrastructure.Databases
{
    /// <summary>
    /// Una instancia de CharlaEFCDbContext representa una sesión de base de datos y puede ser utilizada para consultar o guardar instancias de nuestras entidades.
    /// </summary>
    public class CharlaEFCDbContext : AlgoritmoDbContext, ICharlaEFC
    {
        #region DBSets

        #region Jerarquías
        public DbSet<Jerarquia> Jerarquia { get; set; }
        public DbSet<JerarquiaItem> JerarquiaItem { get; set; }
        public DbSet<JerarquiaNivel> JerarquiaNivel { get; set; }
        #endregion
        #endregion

        /// <summary>
        /// Mecanismo para establecimiento del contexto de dominio por comando.
        /// </summary>
        private AlgoritmoDomainContextCommandInterceptor<CharlaEFCDbContext> domainContextCommandInterceptor = new();

        /// <summary>
        /// Genera una nueva instancia de CharlaEFCDbContext
        /// </summary>
        /// <param name="options"></param>
        public CharlaEFCDbContext(DbContextOptions<CharlaEFCDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Constructor específico para activación personalizada.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="workContext"></param>
        /// <param name="onlyForCustomActivation">Este valor solo impide que sea el tipo sea creado por el DI</param>
        public CharlaEFCDbContext(DbContextOptions options, IWorkContext workContext, int onlyForCustomActivation) : base(options, workContext)
        {
        }

        /// <summary>
        /// Método mediante el cual se crea el modelo de datos.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Debugger.Launch();  // descomentar si se desea debuguear las configurations
            base.OnModelCreating(modelBuilder);



            var excluded = modelBuilder.GetBaseEnumerationTypes();

            modelBuilder.IncludeTypesInMigration(excludedTypes: excluded, domainServices: new[]{ typeof(Domain.BaseClasses.ICharlaEFC),
                                                                    typeof(Idbo) });

            modelBuilder.ConfigureBaseEnumerationForEntityFrameworkCore();
            modelBuilder.ConfigureBaseEnumerationForDapper();

            // Configura las jerarquías
            modelBuilder.ConfigureJerarquias<EntidadJerarquia<EntidadMaestraJerarquizable>>();

            // Configura los índices únicos por default
            modelBuilder.ConfigureUniqueIndexes(excluded: new Type[] {});

            // modelBuilder.Seed();

            base.OnAfterModelCreating(modelBuilder);
        }

        /// <summary>
        /// Acciones en tiempo de configuración.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Previene la interceptación de comandos en tiempo de diseño.
            if (BuildingTime == AlgoritmoDbContextBuildingTime.Application)
            {
                optionsBuilder.AddInterceptors(domainContextCommandInterceptor);
            }
        }

        public override IAlgoritmoDbConnection GetConnection()
        {
            AlgoritmoDbConnection c = new(Database.GetDbConnection(), this);
            return c;
        }
    }

}
