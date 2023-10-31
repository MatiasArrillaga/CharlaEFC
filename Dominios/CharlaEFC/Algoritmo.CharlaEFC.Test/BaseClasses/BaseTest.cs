using Algoritmo.CharlaEFC.Portable.General.Commands;
using Algoritmo.CharlaEFC.Portable.General.Queries;
using Algoritmo.CharlaEFC.Portable.General.Responses;
using Algoritmo.CharlaEFC.Portable.General.Responses.Commands;
using Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Queries;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Responses;
using Algoritmo.Microservices.Shared.Portable.BaseClassesDTO;
using Algoritmo.Microservices.Shared.Portable.Jerarquias;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using Algoritmo.Microservices.Shared.Portable.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using api = Algoritmo.CharlaEFC.API;
using shared = Algoritmo.Microservices.Shared.Application.Test.BaseClasses;

namespace Algoritmo.CharlaEFC.UnitTest.BaseClasses
{
    public class BaseTest : shared.BaseTest<api.Startup>
    {

        public BaseTest(WebApplicationFactory<api.Startup> factory, shared.JSonParser defaultJsonParser = shared.JSonParser.Newtonsoft) : base(factory, defaultJsonParser) { }


        #region Jerarquías
        #region GetJerarquía
        public async Task<IJerarquiaDTO> GetJerarquia(Guid id)
        {// Arrange (prepara)
            var qry = new GetJerarquiaByIdQuery() { Id = id };

            // Act (actúa)
            var queryResponse = await Ask<GetJerarquiaByIdQuery, GetJerarquiaByIdResponse>("/Jerarquia/GetJerarquiaById", qry);

            return queryResponse.Jerarquia;
        }
        public async Task<IJerarquiaDTO> GetJerarquia(string codigo)
        {  // Arrange (prepara)
            var qry = new GetJerarquiaByCodeQuery() { Codigo = codigo };

            // Act (actúa)
            var queryResponse = await Ask<GetJerarquiaByCodeQuery, GetJerarquiaByCodeResponse>("/Jerarquia/GetJerarquiaByCode", qry);

            return queryResponse.Jerarquia;
        }

        #endregion

        #region Jerarquizar Entidad
        /// <inheritdoc cref="JerarquizarEntidadCommand"/>
        /// <param name="codigo">Código de la nueva hoja</param>
        /// <param name="nombre">Nombre de la nueva hoja</param>
        /// <param name="entity">Entidad a jerarquizar</param>
        /// <param name="itemPadre">Item padre de la nueva hoja</param>
        public async Task<JerarquizarEntidadResponse> JerarquizarEntidad<TJerarquizable>(TJerarquizable entity, JerarquiaItemDTO itemPadre)
            where TJerarquizable : IEntidadJerarquizableDTO
        {
            var cmd = new JerarquizarEntidadCommand(new List<IEntidadJerarquizableDTO>() { entity }, itemPadre);
            return await Do<JerarquizarEntidadCommand, JerarquizarEntidadResponse>("/General/JerarquizarEntidad", cmd);
        }        
        public async Task<JerarquizarEntidadResponse> JerarquizarEntidad<TJerarquizable>(IEnumerable<TJerarquizable> entites, JerarquiaItemDTO itemPadre)
            where TJerarquizable : IEntidadJerarquizableDTO
        {
            var cmd = new JerarquizarEntidadCommand((IEnumerable<IEntidadJerarquizableDTO>)entites, itemPadre);
            return await Do<JerarquizarEntidadCommand, JerarquizarEntidadResponse>("/General/JerarquizarEntidad", cmd);
        }

        /// <inheritdoc cref="JerarquizarEntidadCommand"/>
        /// <param name="codigo">Código de la nueva hoja</param>
        /// <param name="nombre">Nombre de la nueva hoja</param>
        /// <param name="entity">Entidad a jerarquizar</param>
        /// <param name="jerarquia">Jerarquía de la cual se obtendrá el item padre</param>
        /// <param name="codigoItemPadre">Código del item padre de la nueva hoja</param>
        public async Task<JerarquizarEntidadResponse> JerarquizarEntidad<TJerarquizable>(TJerarquizable entity, IJerarquiaDTO jerarquia, string codigoItemPadre)
            where TJerarquizable : IEntidadJerarquizableDTO
        {
            var itemPadre = GetItem(jerarquia, codigoItemPadre)
                        ?? throw new NullReferenceException($"No se encontró el item padre {codigoItemPadre}");

            return await JerarquizarEntidad(entity, itemPadre);
        }        
        public async Task<JerarquizarEntidadResponse> JerarquizarEntidad<TJerarquizable>(IEnumerable<TJerarquizable> entities, IJerarquiaDTO jerarquia, string codigoItemPadre)
            where TJerarquizable : IEntidadJerarquizableDTO
        {
            var itemPadre = GetItem(jerarquia, codigoItemPadre)
                        ?? throw new NullReferenceException($"No se encontró el item padre {codigoItemPadre}");

            return await JerarquizarEntidad(entities, itemPadre);
        }
        #endregion

        #region Desjerarquizar Entidad
        /// <inheritdoc cref="DesjerarquizarEntidadCommand"/>
        /// <param name="codigo">Código de la nueva hoja</param>
        /// <param name="nombre">Nombre de la nueva hoja</param>
        /// <param name="entity">Entidad a jerarquizar</param>
        /// <param name="itemPadre">Item padre de la nueva hoja</param>
        public async Task<DesjerarquizarEntidadResponse> DesjerarquizarEntidad<TJerarquizable>(TJerarquizable entity, IJerarquiaDTO jerarquia)
            where TJerarquizable : IEntidadJerarquizableDTO
        {
            return await DesjerarquizarEntidad(new List<IEntidadJerarquizableDTO>() { entity }, jerarquia);
        }
        public async Task<DesjerarquizarEntidadResponse> DesjerarquizarEntidad<TJerarquizable>(IEnumerable<TJerarquizable> entites, IJerarquiaDTO jerarquia)
            where TJerarquizable : IEntidadJerarquizableDTO
        {
            Guid id = jerarquia.Id ?? Guid.Empty;
            var cmd = new DesjerarquizarEntidadCommand(id, (IEnumerable<IEntidadJerarquizableDTO>)entites);
            return await Do<DesjerarquizarEntidadCommand, DesjerarquizarEntidadResponse>("/General/DesjerarquizarEntidad", cmd);
        }
        #endregion

        #region Agregar Rama  
        /// <inheritdoc cref="AgregarRamaCommand"/>
        /// <param name="codigo">Código de la nueva rama</param>
        /// <param name="nombre">Nombre de la nueva rama</param>
        /// <param name="itemPadre">Item padre de la nueva rama</param>
        public async Task<AgregarRamaResponse> AgregarRamaJerarquia(string codigo, string nombre, JerarquiaItemDTO itemPadre)
        {
            var cmd = new AgregarRamaCommand(codigo, nombre, itemPadre);
            return await Do<AgregarRamaCommand, AgregarRamaResponse>("/General/AgregarRama", cmd);
        }

        /// <inheritdoc cref="AgregarRamaCommand"/>
        /// <param name="codigo">Código de la nueva rama</param>
        /// <param name="nombre">Nombre de la nueva rama</param>
        /// <param name="jerarquia">Jerarquía de la cual se obtendrá el item padre</param>
        /// <param name="codigoItemPadre">Código del item padre de la nueva rama</param>
        public async Task<AgregarRamaResponse> AgregarRamaJerarquia(string codigo, string nombre, JerarquiaDTO jerarquia, string codigoItemPadre)
        {
            var itemPadre = GetItem(jerarquia, codigoItemPadre);
            return await AgregarRamaJerarquia(codigo, nombre, itemPadre);
        }
        #endregion

        #region MoverItem
        /// <inheritdoc cref="MoverItemCommand"/>
        /// <param name="codigoDestino">Código del item al cual se moverá el item seleccionado</param>
        /// <param name="item">Item a mover</param>
        public async Task<MoverItemResponse> MoverItemJerarquia(string codigoDestino, JerarquiaItemDTO item)
        {
            Guid.TryParse(codigoDestino, out Guid idDestino);
            var cmd = new MoverItemCommand(idDestino, item);
            return await Do<MoverItemCommand, MoverItemResponse>("/General/MoverItem", cmd);

        }

        /// <inheritdoc cref="MoverItemCommand"/>
        /// <param name="codigoDestino">Código del item al cual se moverá el item seleccionado</param>
        /// <param name="jerarquia">Jerarquía de la cual se obtendrá el item</param>
        /// <param name="codigo">Código del item a mover</param>
        public async Task<MoverItemResponse> MoverItemJerarquia(string codigoDestino, JerarquiaDTO jerarquia, string codigo)
        {
            var item = GetItem(jerarquia, codigo);
            return await MoverItemJerarquia(codigoDestino, item);

        }
        #endregion

        #region BorrarItem 
        /// <inheritdoc cref="BorrarItemCommand"/>
        /// <param name="item">Item a borrar</param>
        public async Task<BorrarItemResponse> BorrarItemJerarquia(JerarquiaItemDTO item)
        {
            var cmd = new BorrarItemCommand(item);
            return await Do<BorrarItemCommand, BorrarItemResponse>("/General/BorrarItem", cmd);

        }

        /// <inheritdoc cref="BorrarItemCommand"/>
        /// <param name="jerarquia">Jerarquía de la cual se obtendrá el item</param>
        /// <param name="codigo">Código del item a borrar</param>
        public async Task<BorrarItemResponse> BorrarItemJerarquia(IJerarquiaDTO jerarquia, string codigo)
        {
            var item = GetItem(jerarquia, codigo);
            return await BorrarItemJerarquia(item);
        }
        #endregion

        #region Modificar Item        
        /// <inheritdoc cref="ModificarItemCommand"/>
        /// <param name="item">Item a modificar</param>
        public async Task<ModificarItemResponse> ModificarItemJerarquia(JerarquiaItemDTO item)
        {
            var cmd = new ModificarItemCommand(item);
            return await Do<ModificarItemCommand, ModificarItemResponse>("/General/ModificarItem", cmd);
        }

        #endregion

        #region GetItem
        /// <summary>
        /// Metodo para encontrar una rama según el código indicado.
        /// </summary>
        /// <param name="jerarquia"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public async Task<JerarquiaItemDTO?> GetItem(string codigo)
        {
            return await GetEntity<JerarquiaItemDTO>(codigo);
        }        
        /// <summary>
        /// Metodo para encontrar una rama según el código indicado.
        /// </summary>
        /// <param name="jerarquia"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public JerarquiaItemDTO? GetItem(IJerarquiaDTO jerarquia, string codigo)
        {
            var filters = new List<FieldWhereDefinition>()
                                {
                                    new FieldWhereDefinition("Jerarquia.Id",Operador.Equals,jerarquia.Id),
                                    new FieldWhereDefinition("Codigo",Operador.Equals,codigo)
                                };
            var entidades = GetEntities<JerarquiaItemDTO>(null, null, null, 1, 2000, new List<FieldOrderDefinition>(), filters).Result;

            return entidades.FirstOrDefault();
        }

        #endregion

        #region GetJerarquiaMinimizada
        public async Task<JerarquiaItemMinDTO> GetJerarquiaMinimizada(JerarquiaDTO item)
        {
            return await GetJerarquiaMinimizada(item.Id.Value);

        }       
        public async Task<JerarquiaItemMinDTO> GetJerarquiaMinimizada(Guid jerarquiaId)
        {
            var qry = new GetJerarquiaMinimizadaQuery(jerarquiaId);
            var result =  await Ask<GetJerarquiaMinimizadaQuery, GetJerarquiaMinimizadaResponse>("/General/GetJerarquiaMinimizada", qry);


            Assert.True(result.Success, result.Message);
            Assert.True(result.Jerarquia is not null, "La jerarquía llegó null");
            Assert.True(result.Jerarquia is JerarquiaItemMinDTO, "La jerarquía no es del tipo correcto");

            if (!result.Success)
                throw new Exception(result.Message);

            return result.Jerarquia;
        }
        #endregion        
        #endregion


        #region GetEntities

        /// <summary>
        /// <inheritdoc cref="shared.BaseTest{TStartup}.GetEntity{TItemListDTO}(object, string)"/>
        /// </summary>
        public override async Task<TItemListDTO> GetEntity<TItemListDTO>(object id, string entityTypeName = null)
        {
            var entityType = entityTypeName ?? typeof(TItemListDTO).Name.Replace("DTO", string.Empty);

            var list = await Ask<TItemListDTO, GetEntidadQuery, GetEntidadResponse>(entityType: entityType, id: id);

            Assert.True(list is not null && list.Items.Any(), "La entidad " + entityType + " id: " + id + " no existe");
            return list.Items.FirstOrDefault();

        }

        /// <summary>
        /// <inheritdoc cref="shared.BaseTest{TStartup}.GetEntity{TItemListDTO}(string, string)"/>
        /// </summary>
        public override async Task<TItemListDTO> GetEntity<TItemListDTO>(string codigo, string entityTypeName = null)
        {
            var entityType = entityTypeName ?? typeof(TItemListDTO).Name.Replace("DTO", string.Empty);

            var list = await Ask<TItemListDTO, GetEntidadQuery, GetEntidadResponse>(entityType: entityType, codigo: codigo);

            //Assert.True(list is not null && list.Items.Any(), "La entidad " + entityType + " código: " + codigo + " no existe");
            return list.Items.FirstOrDefault();
        }

        /// <summary>
        /// <inheritdoc cref="shared.BaseTest{TStartup}.GetEntities{TItemListDTO}(string)"/>
        /// </summary>
        public override async Task<List<TItemListDTO>> GetEntities<TItemListDTO>(string entityType)
        {
            var list = await Ask<TItemListDTO, GetEntidadQuery, GetEntidadResponse>(entityType: entityType);
            Assert.True(list is not null && list.Items.Any(), "La lista de entidades <" + entityType + "> esta vacía");
            return list.Items;

        }
        public async Task<List<TItemListDTO>> GetEntities<TItemListDTO>(object? id=null, string? codigo=null, Guid? jerarquiaId=null, int? start = null, int? length = null,
                                                                        List<FieldOrderDefinition> orderByDefinitions = null,
                                                                        List<FieldWhereDefinition> whereDefinitions = null)
        {
            var entityType =  typeof(TItemListDTO).Name.Replace("DTO", string.Empty);

            var list = await Ask<TItemListDTO, GetEntidadQuery, GetEntidadResponse>(id:id,
                                                                                    codigo: codigo,
                                                                                    jerarquiaId: jerarquiaId,
                                                                                    entityType: entityType, 
                                                                                    start: start, 
                                                                                    length: length,
                                                                                    orderByDefinitions: orderByDefinitions,
                                                                                    whereDefinitions: whereDefinitions);
            //Assert.True(list is not null && list.Items.Any(), "La lista de entidades <" + entityType + "> esta vacía");
            return list.Items;

        }

        /// <summary>
        /// <inheritdoc cref="shared.BaseTest{TStartup}.GetEntities{TItemListDTO}(List{FieldWhereDefinition}?, List{FieldOrderDefinition}?, string)"/>
        /// </summary>
        public override async Task<List<TItemListDTO>> GetEntities<TItemListDTO>(List<FieldWhereDefinition>? whereDefinitions = null, List<FieldOrderDefinition>? orderByDefinitions = null, string entityTypeName = null)
        {
            var entityType = entityTypeName ?? typeof(TItemListDTO).Name.Replace("DTO", string.Empty);
            var list = await Ask<TItemListDTO, GetEntidadQuery, GetEntidadResponse>(entityType: entityType, orderByDefinitions: orderByDefinitions, whereDefinitions: whereDefinitions);
            Assert.True(list is not null && list.Items.Any(), "La lista de entidades <" + entityType + "> esta vacía");
            return list.Items;
        }

        /// <summary>
        /// <inheritdoc cref="shared.BaseTest{TStartup}.GetEntityId{TItemListDTO}(string, string?)"/>
        /// </summary>
        public override async Task<int> GetEntityId<TItemListDTO>(string codigo, string entityTypeName = null)
        {
            var entityType = entityTypeName ?? typeof(TItemListDTO).Name.Replace("DTO", string.Empty);

            var list = await Ask<TItemListDTO, GetEntidadQuery, GetEntidadResponse>(entityType: entityType, codigo: codigo);

            Assert.True(list is not null && list.Items.Any(), "La entidad " + entityType + " código: " + codigo + " no existe");
            return list.Items.Any() ? (list.Items.First() as IEntityDTO).Id : 0;

        }
        #endregion
    }
}
