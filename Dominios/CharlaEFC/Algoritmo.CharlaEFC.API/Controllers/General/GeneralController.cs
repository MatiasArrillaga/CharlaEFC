using Algoritmo.CharlaEFC.Portable.General.Commands;
using Algoritmo.CharlaEFC.Portable.General.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneralController : BaseApiController
    {
        /// <summary>
        /// Método para obtener la entidad.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Retorna lista de entidad</returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetEntidad(GetEntidadQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        #region Jerarquías
        /// <inheritdoc cref="JerarquizarEntidadCommand"/>
        [HttpPost("[Action]")]
        public async Task<IActionResult> JerarquizarEntidad(JerarquizarEntidadCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Método para Desjerarquizar una entidad
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Retorna una Jerarquía</returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DesjerarquizarEntidad(DesjerarquizarEntidadCommand query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <inheritdoc cref="AgregarRamaCommand"/>
        [HttpPost("[Action]")]
        public async Task<IActionResult> AgregarRama(AgregarRamaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <inheritdoc cref="MoverItemCommand"/>
        [HttpPost("[Action]")]
        public async Task<IActionResult> MoverItem(MoverItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <inheritdoc cref="BorrarItemCommand"/>
        [HttpPost("[Action]")]
        public async Task<IActionResult> BorrarItem(BorrarItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <inheritdoc cref="ModificarItemCommand"/>
        [HttpPost("[Action]")]
        public async Task<IActionResult> ModificarItem(ModificarItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Método para obtener una Jerarquía por Código.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Retorna una Jerarquía</returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetJerarquiaMinimizada(GetJerarquiaMinimizadaQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        #endregion

    }
}