namespace Algoritmo.CharlaEFC.UnitTest.Jerarquias
{
    //[TestCaseOrderer("Algoritmo.CharlaEFC.UnitTest.Utilities.xUnitOrder.PriorityOrderer", "Algoritmo.CharlaEFC.Test")]
    //public class JerarquiasTest : BaseTest, IDisposable
    //{

    //    private const string URL_CREAR = "/Jerarquia/CrearJerarquia";
    //    private const string URL_MODIFICAR = "/Jerarquia/ModificarJerarquia";
    //    private const string URL_BORRAR = "/Jerarquia/BorrarJerarquia";
    //    private const string URL_GET_BY_ID = "/Jerarquia/GetJerarquiaById";
    //    private const string URL_GET_BY_CODE = "/Jerarquia/GetJerarquiaByCode";
    //    private const string URL_GET_MINI = "/Jerarquia/GetJerarquiaMinimizada";

    //    private const string TEST_CODE = "GRAL";
    //    private new List<Guid> cleanUpList = new List<Guid>();
    //    public JerarquiasTest(WebApplicationFactory<api.Startup> factory) : base(factory)
    //    {
    //        //if ((GetJerarquia(TEST_CODE).Result) is null)
    //        //{
    //        //_ = Crear(GetJerarquia(TEST_CODE, "Test Contabilidad", typeof(AlmacenDTO)).AddRange(GetNiveles())).Result;
    //        //}
    //    }

    //    #region GETTERS
    //    private JerarquiaDTO GetJerarquia(string codigo, string nombre, Type tipoEntidadHoja, bool permiteHojasYRamasEnMismoNivel = false, bool activo= true)
    //    {
    //        return new JerarquiaDTO
    //          (
    //            codigo: (string.IsNullOrEmpty(codigo)) ? rg.RandomString(5) : codigo,
    //            nombre: nombre,
    //            tipoEntidadHoja: tipoEntidadHoja
    //          )
    //        {
    //            PermiteHojasYRamasEnMismoNivel = permiteHojasYRamasEnMismoNivel,
    //            Descripcion = nombre,
    //            Activo = activo
    //        };
    //    }
    //    private async Task<IJerarquiaDTO> AddRamasAJerarquia(IJerarquiaDTO jerarquia)
    //    {
    //        var root = await GetEntity<JerarquiaItemDTO>(jerarquia.Codigo);
    //        var filters = new List<FieldWhereDefinition>()
    //                           {
    //                               new FieldWhereDefinition("Jerarquia.Id",Operador.Equals,jerarquia.Id)
    //                           };
    //        List<JerarquiaItemDTO> arbol = new();
    //        for (int i = 1; i <= 3; i++)
    //        {
    //            var codigo = "ITM" + i;

    //            await AgregarRamaJerarquia(codigo, "Item" + i, root);

    //            arbol = await GetEntities<JerarquiaItemDTO>(null,null,null,1, 2000, new List<FieldOrderDefinition>(), filters);

    //            var item =(JerarquiaItemDTO)arbol.Single(i=>i.Codigo.Equals(codigo));

    //            //var item = new JerarquiaItemDTO("ITM"+i, "Item"+i, Microservices.Shared.Portable.Enums.Jerarquias.TipoItemJerarquia.Rama, jerarquia.Root);// GetItem(jerarquia, "ITM" + i);
    //            //jerarquia.Root.Add(item);
    //            //jerarquia.Add(item);

    //            for (int j = 1; j <= 3; j++)
    //            {
    //                //var hItem = new JerarquiaItemDTO(item.Codigo + j, item.Nombre + j, Microservices.Shared.Portable.Enums.Jerarquias.TipoItemJerarquia.Rama, item);
    //                await AgregarRamaJerarquia(item.Codigo + j, item.Nombre + j, item);
    //                //item.Add(hItem);
    //                //jerarquia.Add(hItem);
    //            }
    //        }

    //        arbol = await GetEntities<JerarquiaItemDTO>(null, null, null, 1, 2000, new List<FieldOrderDefinition>(), filters);
    //        Assert.True(arbol.Count == 13, "No se agregaron las ramas correctamente");
    //        //Assert.True(jerarquia.Root.Hijos[0].Hijos[0].Codigo == "ITM11", "El item 'ITM11' no se creó");
    //        //Assert.True(jerarquia.Root.Hijos[0].Hijos.Count == 3, "No se crearon los hijos de el item 'ITM1'");

    //        return jerarquia;
    //    }
    //    private List<JerarquiaNivelDTO> GetNiveles()
    //    {
    //        return new List<JerarquiaNivelDTO>()
    //        {
    //            new JerarquiaNivelDTO(0,"Nivel 0"){ Command=Microservices.Shared.Portable.Enums.EntityCommand.Add },
    //            new JerarquiaNivelDTO(1,"Nivel 1"){ Command=Microservices.Shared.Portable.Enums.EntityCommand.Add },
    //            new JerarquiaNivelDTO(2,"Nivel 2"){ Command=Microservices.Shared.Portable.Enums.EntityCommand.Add }
    //        };
    //    }

    //    //public JerarquiaItemDTO? GetItem(JerarquiaDTO jerarquia, string codigo)
    //    //{
    //    //    return jerarquia.Arbol.SingleOrDefault(a => a.Codigo == codigo);
    //    //}
    //    #endregion

    //    #region CRUD Methods
    //    public async Task<portable.Responses.CrearJerarquiaResponse> Crear(JerarquiaDTO jerarquia)
    //    {
    //        var cmd = new portable.Commands.CrearJerarquiaCommand() { Jerarquia = jerarquia };
    //        // Act (actúa)
    //        var commandResponse = await Do<portable.Commands.CrearJerarquiaCommand, portable.Responses.CrearJerarquiaResponse>(URL_CREAR, cmd);

    //        if (commandResponse.Success) cleanUpList.Add(commandResponse.Jerarquia.Id.Value);

    //        return commandResponse;
    //    }
    //    public async Task<portable.Responses.ModificarJerarquiaResponse> Modificar(JerarquiaDTO jerarquia)
    //    {
    //        var cmd = new portable.Commands.ModificarJerarquiaCommand() { Jerarquia = jerarquia };
    //        // Act (actúa)
    //        var commandResponse = await Do<portable.Commands.ModificarJerarquiaCommand, portable.Responses.ModificarJerarquiaResponse>(URL_MODIFICAR, cmd);
    //        return commandResponse;
    //    }       


    //    public async Task<portable.Responses.BorrarJerarquiaResponse> Borrar(Guid Id)
    //    {
    //        var cmd = new portable.Commands.BorrarJerarquiaCommand() { Id = Id };

    //        // Act (actúa)
    //        var commandResponse = await Do<portable.Commands.BorrarJerarquiaCommand, portable.Responses.BorrarJerarquiaResponse>(URL_BORRAR, cmd);

    //        // Assert (afirma)
    //        return commandResponse;
    //    }
    //    public async Task<portable.Responses.GetJerarquiaByIdResponse> GetById(Guid id)
    //    {
    //        // Arrange (prepara)
    //        var qry = new portable.Queries.GetJerarquiaByIdQuery() { Id = id };

    //        // Act (actúa)
    //        var queryResponse = await Ask<portable.Queries.GetJerarquiaByIdQuery, portable.Responses.GetJerarquiaByIdResponse>(URL_GET_BY_ID, qry);

    //        return queryResponse;
    //    }
    //    public async Task<portable.Responses.GetJerarquiaByCodeResponse> GetByCode(string codigo)
    //    {
    //        // Arrange (prepara)
    //        var qry = new portable.Queries.GetJerarquiaByCodeQuery() { Codigo = codigo };

    //        // Act (actúa)
    //        var queryResponse = await Ask<portable.Queries.GetJerarquiaByCodeQuery, portable.Responses.GetJerarquiaByCodeResponse>(URL_GET_BY_CODE, qry);

    //        return queryResponse;
    //    }
    //    #endregion

    //    #region TESTs
    //    #region Commands
    //    [Fact, TestPriority(10)]
    //    public async Task C10_CrearJerarquia()
    //    {
    //        var commandResponse = await Crear(GetJerarquia("","TEST!", typeof(AlmacenDTO)));

    //        // Assert (afirma)
    //        Assert.True(commandResponse.Success, commandResponse.Message);
    //        Assert.True(commandResponse.Jerarquia is not null, "La jerarquía llegó null");
    //        Assert.True(commandResponse.Jerarquia is JerarquiaDTO, "La jerarquía no es del tipo correcto");
    //    }

    //    [Fact, TestPriority(20)]
    //    public async Task C20_ModificarJerarquia()
    //    {
    //        //Obtengo la Jerarquía y modifico su nombre
    //        var jerarquia = (JerarquiaDTO)(await Crear(GetJerarquia("", "TEST!", typeof(AlmacenDTO)))).Jerarquia;
    //        jerarquia.Descripcion = "Jerarquía " + DateTime.Now.ToString("G");
    //        jerarquia.AddRange(GetNiveles());
    //        jerarquia.Command = Microservices.Shared.Portable.Enums.EntityCommand.Update;

    //        //Hago el Modificar
    //        var commandResponse = await Modificar(jerarquia);

    //        //Obtengo la Jerarquía modificada
    //        jerarquia = (JerarquiaDTO)await GetEntity<JerarquiaDTO>(jerarquia.Id.Value);

    //        // Assert (afirma)
    //        Assert.True(commandResponse.Success, commandResponse.Message);
    //        Assert.True(jerarquia is not null, "La jerarquía volvió null.");
    //        Assert.True(jerarquia.Descripcion.Equals(jerarquia.Descripcion), "La jerarquía no cambio de descripción.");
    //        Assert.True(jerarquia.Niveles.Count > 0, "La jerarquía volvió sin niveles.");
    //        Assert.True(jerarquia is JerarquiaDTO, "La jerarquía no es del tipo correcto.");
    //    }

    //    [Fact, TestPriority(30)]
    //    public async Task C30_BorrarJerarquia()
    //    {
    //        var id = (await Crear(GetJerarquia("", "TEST!", typeof(AlmacenDTO)))).Jerarquia.Id.Value;
    //        var commandResponse = await Borrar(id);

    //        if (commandResponse.Success) cleanUpList.Remove(id);

    //        // Assert (afirma)
    //        Assert.True(commandResponse.Success, commandResponse.Message);

    //        var queryResponse = await GetById(id);

    //        Assert.True(queryResponse.Jerarquia is null, queryResponse.Message);
    //    }

    //    [Fact, TestPriority(40)]
    //    public async Task C40_CrearJerarquiaConNiveles()
    //    {
    //        var commandResponse = await Crear(GetJerarquia("", "Jerarquía con Niveles!", typeof(AlmacenDTO)).AddRange(GetNiveles()));

    //        // Assert (afirma)
    //        Assert.True(commandResponse.Success, commandResponse.Message);
    //        Assert.True(commandResponse.Jerarquia is not null, "La jerarquía volvió null.");
    //        Assert.True(commandResponse.Jerarquia.Niveles.Count > 0, "La jerarquía volvió sin niveles.");
    //        Assert.True(commandResponse.Jerarquia is JerarquiaDTO, "La jerarquía no es del tipo correcto.");
    //    }        

    //    [Fact, TestPriority(50)]
    //    public async Task C50_AgregarRamasALaJerarquia()
    //    {
    //        var jerarquia =(JerarquiaDTO) (await Crear(GetJerarquia("", "Árbol Con 3 Niveles", typeof(AlmacenDTO)).AddRange(GetNiveles()))).Jerarquia;
    //        Assert.True(jerarquia is not null, "Falló la creación de la jerarquía");

    //        jerarquia = (JerarquiaDTO)await AddRamasAJerarquia(jerarquia);
    //        Assert.True(jerarquia is not null, "Falló el agregado de ramas de la jerarquía");

    //    }

    //    [Fact, TestPriority(60)]
    //    public async Task C60_JerarquizarEntidad()
    //    {
    //        var jerarquia = (await GetJerarquia("GRAL"));

    //        var root = GetItem(jerarquia, jerarquia.Codigo);

    //        await AgregarRamaJerarquia("JITEM", "JITEM", root);

    //        var entidad = await GetEntity<AlmacenDTO>("BRAGA")
    //            ?? throw new Exception($"No se pudo recuperar la entidad BRAGA");

    //        var item = await GetEntity<JerarquiaItemDTO>("JITEM");
    //        var jCommandResponse = await JerarquizarEntidad(new List<AlmacenDTO> { entidad }, item);

    //        List<AlmacenDTO> hojas = new();
    //        try
    //        {
    //            hojas = await GetEntities<AlmacenDTO>(null, "RAMA", jerarquiaId: item.Id, 1, 200000, new List<FieldOrderDefinition>(), new List<FieldWhereDefinition>());

    //            Assert.True(jCommandResponse.Success, jCommandResponse.Message);
    //            Assert.True(hojas.Any(), $"La entidad no se jerarquizó correctamente");
    //        }
    //        catch (Exception ex)
    //        {
    //            Assert.True(false, ex.Message);
    //        }
    //        finally
    //        {
    //            await BorrarItemJerarquia(item);
    //        }
    //    }

    //    [Fact, TestPriority(61)]
    //    public async Task C61_Desjerarquizar()
    //    {
    //        var jerarquia = (await GetJerarquia("GRAL"));

    //        var root = GetItem(jerarquia, jerarquia.Codigo);

    //        await AgregarRamaJerarquia("JITEM", "JITEM", root);

    //        var entidad = await GetEntity<AlmacenDTO>("BRAGA")
    //            ?? throw new Exception($"No se pudo recuperar la entidad BRAGA");

    //        var item = await GetEntity<JerarquiaItemDTO>("JITEM");
    //        var jCommandResponse = await JerarquizarEntidad(new List<AlmacenDTO> { entidad }, item);

    //        List<AlmacenDTO> hojas = new();
    //        try
    //        {
    //            hojas = await GetEntities<AlmacenDTO>(null, "RAMA", jerarquiaId: item.Id, 1, 200000, new List<FieldOrderDefinition>(), new List<FieldWhereDefinition>());

    //            Assert.True(jCommandResponse.Success, jCommandResponse.Message);
    //            Assert.True(hojas.Any(), $"La entidad no se jerarquizó correctamente");

    //            await DesjerarquizarEntidad(new List<AlmacenDTO> { entidad }, jerarquia);
    //            Assert.True(jCommandResponse.Success, jCommandResponse.Message);
    //            Assert.True(hojas.Any(), $"La entidad no se desjerarquizó correctamente");
    //        }
    //        catch (Exception ex)
    //        {
    //            Assert.True(false, ex.Message);
    //        }
    //        finally
    //        {
    //            await BorrarItemJerarquia(item);
    //        }
    //    }

    //    [Fact, TestPriority(69)]
    //    public async Task C69_BorrarItem()
    //    {
    //        //var jerarquia = (await GetJerarquia("GRAL"));
    //        var root = await GetEntity<JerarquiaItemDTO>("GRAL");

    //        await AgregarRamaJerarquia("DEL", "Item a borrar", root);

    //        var item = await GetEntity<JerarquiaItemDTO>("DEL");

    //        await AgregarRamaJerarquia("DEL1", "Hijo de del", item);

    //        var modificarResponse = await BorrarItemJerarquia(item);

    //        Assert.True(modificarResponse.Success, modificarResponse.Message);
    //    }
    //    [Fact, TestPriority(70)]
    //    public async Task C70_MoverItem()
    //    {
    //        var jerarquia = (JerarquiaDTO)(await Crear(GetJerarquia("", "Test Mover Hoja", typeof(AlmacenDTO)).AddRange(GetNiveles()))).Jerarquia;

    //        jerarquia = (JerarquiaDTO)await AddRamasAJerarquia(jerarquia);


    //        var root = GetItem(jerarquia, jerarquia.Codigo);
    //        await AgregarRamaJerarquia("JITEM", "JITEM", root);

    //        var entidad = await GetEntity<AlmacenDTO>("BRAGA");

    //        var itemMovible = await GetEntity<JerarquiaItemDTO>("ITM1");
    //        var itemDestino = await GetEntity<JerarquiaItemDTO>("JITEM");

    //        var hoja = (await JerarquizarEntidad(new List<AlmacenDTO> { entidad }, itemMovible));

    //        var commandResponse = await MoverItemJerarquia(itemDestino.Id.ToString(), itemMovible);

    //        Assert.True(commandResponse.Success, commandResponse.Message);
    //        //Assert.True(((JerarquiaDTO)commandResponse.Jerarquia).Arbol.First(i=>i.Codigo.Equals("ITM1")).Nivel==3, "El nivel del item devuelto no es el correcto");
    //        //Assert.True(((JerarquiaDTO)commandResponse.Jerarquia).Arbol.First(i => i.Codigo.Equals("ITM1")).CodigoConcatenado.EndsWith(";ITM3;ITM31;ITM1"), commandResponse.Message);


    //        await BorrarItemJerarquia(itemDestino);
    //    }

    //    [Fact, TestPriority(80)]
    //    public async Task C80_ModificarItem()
    //    {
    //        var jerarquia = (JerarquiaDTO)(await Crear(GetJerarquia("", "Test Mover Hoja", typeof(AlmacenDTO)))).Jerarquia;

    //        jerarquia = (JerarquiaDTO)await AddRamasAJerarquia(jerarquia);
    //        var item = GetItem(jerarquia, "ITM1");
    //        item.Nombre = "Item 1 modificado";

    //        item.Add(new JerarquiaItemDTO("TST", "Esto no se puede hacer", Microservices.Shared.Portable.Enums.Jerarquias.TipoItemJerarquia.Rama, item, jerarquia));

    //        var commandResponse = await ModificarItemJerarquia(item);

    //        jerarquia = (JerarquiaDTO)await GetJerarquia(jerarquia.Codigo);

    //        Assert.True(commandResponse.Success, commandResponse.Message);
    //        Assert.True(GetItem(jerarquia, "ITM1").Nombre.Equals(item.Nombre), "El nombre del item recuperado no es el correcto");
    //        Assert.True(GetItem(jerarquia, "TST") is null, "No se debió crear el item TST");

    //        await BorrarItemJerarquia(item);
    //    }

    //    [Theory]
    //    [InlineData(JerarquiasRules.ElTipoDeEntidadDebeCoincidirConElDefinidoEnLaJerarquiaRule)]
    //    [InlineData(JerarquiasRules.LaEntidadEsUnicaEnTodoElArbolRule)]
    //    [InlineData(JerarquiasRules.PermiteHojasYRamasEnMismoNivelRule)]
    //    [InlineData(JerarquiasRules.UnaJerarquiaNivelEsUnicaParaElNivelJerarquicoRule)]
    //    [InlineData(JerarquiasRules.ControlPadresHijosRule)] 
    //    [InlineData(JerarquiasRules.NoCambiarTipoDeJerarquiaSiYaTengoHojas)] 
    //    public async Task CKR80_CheckRulesTest(string checkRuleName)
    //    {
    //        var jerarquia = (JerarquiaDTO)(await Crear(GetJerarquia("", "TEST!", typeof(AlmacenDTO)))).Jerarquia;

    //        switch (checkRuleName)
    //        {
    //            case JerarquiasRules.ElTipoDeEntidadDebeCoincidirConElDefinidoEnLaJerarquiaRule:
    //                {
    //                    //Configuro el Tipo de entidad hoja como ProductoCharlaEFCeableDTO
    //                    {
    //                        jerarquia = (JerarquiaDTO)(await Crear(GetJerarquia("", "TEST!", typeof(ProductoCharlaEFCeableDTO)))).Jerarquia;
    //                        jerarquia = (JerarquiaDTO)await AddRamasAJerarquia(jerarquia);
    //                    }

    //                    var entidad = await GetEntity<AlmacenDTO>("BRAGA");
    //                    var commandErrorResponse = await JerarquizarEntidad(new List<AlmacenDTO> { entidad }, jerarquia, "ITM12");

    //                    Assert.True(!commandErrorResponse.Success && commandErrorResponse.Message.Contains(checkRuleName), commandErrorResponse.Message);


    //                    break;
    //                }
    //            case JerarquiasRules.LaEntidadEsUnicaEnTodoElArbolRule:
    //                {
    //                    //Se agregan Ramas a la Jerarquía
    //                    {
    //                        jerarquia.PermiteHojasYRamasEnMismoNivel = false;

    //                        jerarquia= (JerarquiaDTO)await AddRamasAJerarquia(jerarquia);
    //                    }

    //                    //Se Jerarquiza el Tipo Operación CEN
    //                    {
    //                        var entidad = await GetEntity<AlmacenDTO>("BRAGA");
    //                        _ = await JerarquizarEntidad(new List<AlmacenDTO> { entidad }, jerarquia, "ITM12");
    //                        jerarquia = (JerarquiaDTO)await GetJerarquia(jerarquia.Id.Value);
    //                    }

    //                    //Se vuelve a Jerarquizar el Tipo Operación BRAGA

    //                    var entidadRepetida = await GetEntity<AlmacenDTO>("BRAGA");
    //                    var commandResponseError = await JerarquizarEntidad(new List<AlmacenDTO> { entidadRepetida }, jerarquia, "ITM12");

    //                    Assert.True(!commandResponseError.Success && commandResponseError.Message.Contains(checkRuleName), commandResponseError.Message);

    //                    break;
    //                }
    //            case JerarquiasRules.PermiteHojasYRamasEnMismoNivelRule:
    //                {
    //                    //Se agregan Ramas a la Jerarquía
    //                    {
    //                        jerarquia.PermiteHojasYRamasEnMismoNivel = false;

    //                        jerarquia = (JerarquiaDTO)await AddRamasAJerarquia(jerarquia);
    //                    }

    //                    //Se Jerarquiza una cuenta
    //                    {
    //                        var entidad = await GetEntity<AlmacenDTO>("BRAGA");
    //                        _ = await JerarquizarEntidad(new List<AlmacenDTO> { entidad }, jerarquia, "ITM12");

    //                        jerarquia = (JerarquiaDTO)await GetJerarquia(jerarquia.Id.Value);
    //                    }

    //                    var item = await GetEntity<JerarquiaItemDTO>("ITM12");
    //                    //Se intenta agregar una rama con el mismo nivel de una hoja.
    //                    var commandErrorResponse = await AgregarRamaJerarquia("I121", "I121", item);

    //                    Assert.True(!commandErrorResponse.Success && commandErrorResponse.Message.Contains(checkRuleName), commandErrorResponse.Message);

    //                    break;
    //                }
    //            case JerarquiasRules.UnaJerarquiaNivelEsUnicaParaElNivelJerarquicoRule:
    //                {
    //                    jerarquia.AddRange(GetNiveles());
    //                    jerarquia.Add(new JerarquiaNivelDTO(1, "Nivel 1/1"));

    //                    var commandErrorResponse = await Modificar(jerarquia);

    //                    Assert.True(!commandErrorResponse.Success && commandErrorResponse.Message.Contains(checkRuleName), commandErrorResponse.Message);

    //                    break;
    //                }
    //            case JerarquiasRules.ControlPadresHijosRule:
    //                {
    //                    jerarquia = (JerarquiaDTO)await AddRamasAJerarquia(jerarquia);

    //                    #region ItemSinPadre
    //                    var item = new JerarquiaItemDTO("TST", "TEST", Microservices.Shared.Portable.Enums.Jerarquias.TipoItemJerarquia.Rama, null);
    //                    jerarquia.Add(item);

    //                    var commandResponse = await Modificar(jerarquia);

    //                    Assert.True(!commandResponse.Success && commandResponse.Message.Contains(checkRuleName), commandResponse.Message);
    //                    Assert.True(!commandResponse.Success && commandResponse.Message.Contains("ItemSinPadre"), commandResponse.Message);
    //                    #endregion

    //                    #region PadreNoExiste
    //                    jerarquia.Remove(item);
    //                    var itemB = new JerarquiaItemDTO("TST2", "TST2", Microservices.Shared.Portable.Enums.Jerarquias.TipoItemJerarquia.Rama, new JerarquiaItemDTO("TST3", "TEST", Microservices.Shared.Portable.Enums.Jerarquias.TipoItemJerarquia.Rama, null,jerarquia));
    //                    jerarquia.Add(itemB);

    //                    var commandErrorResponse = await Modificar(jerarquia);

    //                    Assert.True(!commandErrorResponse.Success && commandErrorResponse.Message.Contains(checkRuleName), commandErrorResponse.Message);
    //                    Assert.True(!commandErrorResponse.Success && commandErrorResponse.Message.Contains("PadreNoExiste"), commandErrorResponse.Message);
    //                    #endregion
    //                    break;
    //                }
    //            case JerarquiasRules.NoCambiarTipoDeJerarquiaSiYaTengoHojas:
    //                {
    //                    //Se agregan Ramas a la Jerarquía
    //                    {
    //                        jerarquia.PermiteHojasYRamasEnMismoNivel = false;

    //                        jerarquia = (JerarquiaDTO)await AddRamasAJerarquia(jerarquia);
    //                    }

    //                    //Se Jerarquiza una cuenta
    //                    {
    //                        var entidad = await GetEntity<AlmacenDTO>("BRAGA");
    //                        _ = await JerarquizarEntidad(new List<AlmacenDTO> { entidad }, jerarquia, "ITM12");

    //                        jerarquia = (JerarquiaDTO)await GetJerarquia(jerarquia.Id.Value);
    //                    }

    //                    jerarquia.TipoEntidadHoja = typeof(ProductoCharlaEFCeableDTO);
    //                    var commandErrorResponse = await Modificar(jerarquia);

    //                    Assert.True(!commandErrorResponse.Success && commandErrorResponse.Message.Contains(checkRuleName), commandErrorResponse.Message);
    //                    break;
    //                }
    //        }
    //    }
    //    #endregion

    //    #region Queries
    //    [Theory, TestPriority(100)]
    //    [InlineData(1)]
    //    [InlineData(100)]
    //    public async Task Q10_GetJerarquiaById(int id)
    //    {
    //        // Arrange (prepara)
    //        if (id == 1)
    //        {
    //            // Arrange (prepara)
    //            var commandResponse = await GetByCode(TEST_CODE);

    //            if (commandResponse.Success && commandResponse.Jerarquia is not null)
    //            {
    //                // Act (actúa)
    //                var queryResponse = await GetById(commandResponse.Jerarquia.Id.Value);

    //                // Assert (afirma)
    //                Assert.True(queryResponse.Success && queryResponse.Jerarquia is not null, queryResponse.Message);

    //            }
    //        }
    //        else
    //        {
    //            // Act (actúa)
    //            var queryResponse = await GetById(Guid.NewGuid());

    //            // Assert (afirma)
    //            Assert.True(queryResponse.Success && queryResponse.Jerarquia is null, queryResponse.Message);
    //        }
    //    }

    //    [Theory, TestPriority(200)]
    //    [InlineData(TEST_CODE)]
    //    public async Task Q20_GetJerarquiaByCode(string codigo)
    //    {
    //        var queryResponse = await GetByCode(codigo);

    //        Assert.True(queryResponse.Success && queryResponse.Jerarquia is not null, queryResponse.Message);
    //    }
    //    #endregion
    //    #endregion

    //    public void Dispose()
    //    {
    //        if (!CleanUp) return;
    //        cleanUpList.ForEach(id => Borrar(id).Wait());
    //        cleanUpList.Clear();
    //    }
    //}

    //public static class JerarquiasRules
    //{
    //    /// <summary>
    //    /// <inheritdoc cref="Algoritmo.CharlaEFC.Domain.Jerarquias.Rules.ElTipoDeEntidadDebeCoincidirConElDefinidoEnLaJerarquiaRule.ElTipoDeEntidadDebeCoincidirConElDefinidoEnLaJerarquiaRule(Domain.Jerarquias.Entities.Jerarquia, Microservices.Shared.Domain.BaseClasses.Interface.IEntity)"/>
    //    /// </summary>
    //    public const string ElTipoDeEntidadDebeCoincidirConElDefinidoEnLaJerarquiaRule = nameof(ElTipoDeEntidadDebeCoincidirConElDefinidoEnLaJerarquiaRule);
    //    /// <summary>
    //    /// <inheritdoc cref="Algoritmo.CharlaEFC.Domain.Jerarquias.Rules.LaEntidadEsUnicaEnTodoElArbolRule.LaEntidadEsUnicaEnTodoElArbolRule(IReadOnlyList{Domain.Jerarquias.Entities.JerarquiaItem}, Microservices.Shared.Domain.BaseClasses.Interface.IEntity)"/>
    //    /// </summary>
    //    public const string LaEntidadEsUnicaEnTodoElArbolRule = nameof(LaEntidadEsUnicaEnTodoElArbolRule);
    //    /// <summary>
    //    /// <inheritdoc cref="Algoritmo.CharlaEFC.Domain.Jerarquias.Rules.PermiteHojasYRamasEnMismoNivelRule.PermiteHojasYRamasEnMismoNivelRule(IReadOnlyList{Domain.Jerarquias.Entities.JerarquiaItem}, Domain.Jerarquias.Entities.JerarquiaItem)"/>
    //    /// </summary>
    //    public const string PermiteHojasYRamasEnMismoNivelRule = nameof(PermiteHojasYRamasEnMismoNivelRule);
    //    /// <summary>
    //    /// <inheritdoc cref="Algoritmo.CharlaEFC.Domain.Jerarquias.Rules.UnaJerarquiaNivelEsUnicaParaElNivelJerarquicoRule.UnaJerarquiaNivelEsUnicaParaElNivelJerarquicoRule(Domain.Jerarquias.Entities.Jerarquia, Domain.Jerarquias.Entities.JerarquiaNivel)"/>
    //    /// </summary>
    //    public const string UnaJerarquiaNivelEsUnicaParaElNivelJerarquicoRule = nameof(UnaJerarquiaNivelEsUnicaParaElNivelJerarquicoRule);
    //    /// <summary>
    //    /// <inheritdoc cref="Algoritmo.CharlaEFC.Domain.Jerarquias.Rules.ControlPadresHijosRule.ControlPadresHijosRule(IReadOnlyList{Domain.Jerarquias.Entities.JerarquiaItem}, Domain.Jerarquias.Entities.JerarquiaItem)"/>
    //    /// </summary>
    //    public const string ControlPadresHijosRule = nameof(ControlPadresHijosRule);          
    //    /// <summary>
    //    /// <inheritdoc cref="Algoritmo.CharlaEFC.Domain.Jerarquias.Rules.NoCambiarTipoDeJerarquiaSiYaTengoHojasRule.NoCambiarTipoDeJerarquiaSiYaTengoHojas(Domain.Jerarquias.Entities.Jerarquia)"/>
    //    /// </summary>
    //    public const string NoCambiarTipoDeJerarquiaSiYaTengoHojas = nameof(NoCambiarTipoDeJerarquiaSiYaTengoHojas);        

    //}
    //}
}