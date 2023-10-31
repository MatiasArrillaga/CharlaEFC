using Algoritmo.CharlaEFC.API;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Commands;
using Algoritmo.CharlaEFC.Portable.Jerarquias.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Algoritmo.CharlaEFC.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JerarquiaController : BaseApiController
    {
        #region Jerarquía
        /// <summary>
        /// Método para crear un Jerarquía
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Retorna el Id de la Dimensión generado</returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CrearJerarquia(CrearJerarquiaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Método para actualizar un Jerarquía
        /// </summary>
        /// <param name="command"></param>
        [HttpPost("[Action]")]
        public async Task<IActionResult> ModificarJerarquia(ModificarJerarquiaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Método para eliminar un Jerarquía
        /// </summary>
        /// <param name="command"></param>
        [HttpPost("[Action]")]
        public async Task<IActionResult> BorrarJerarquia(BorrarJerarquiaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Método para obtener una Jerarquía según un Id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Retorna una Jerarquia</returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetJerarquiaById(GetJerarquiaByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        
        /// <summary>
        /// Método para obtener una Jerarquía por Código.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Retorna una Jerarquía</returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetJerarquiaByCode(GetJerarquiaByCodeQuery query)
        {
            return Ok(await Mediator.Send(query));
        }      

        #endregion

    }
}