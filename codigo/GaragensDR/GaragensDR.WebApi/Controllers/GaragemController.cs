using GaragensDR.Application.Interfaces;
using GaragensDR.Application.Parameters;
using GaragensDR.Application.ViewModel;
using GaragensDR.Domain.DTO;
using GaragensDR.Infra.Shared.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GaragensDR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaragemController : BaseController
    {
        private readonly IGaragemApplication _applicationServiceGaragem;

        public GaragemController(IGaragemApplication applicationServiceGaragem)
        {
            _applicationServiceGaragem = applicationServiceGaragem;
        }

        /// <summary>
        /// Listar Garagem
        /// </summary>
        /// <response code="200">Lista de Garagem processada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<GaragemViewModel>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "GaragemListar")]
        public async Task<IActionResult> Get([FromQuery] GaragemParams parametros)
        {
            return Ok(await _applicationServiceGaragem.Listar(parametros));
        }

        //POST api/values
        /// <summary>
        /// Criar Garagem
        /// </summary>
        /// <response code="200">Garagem criada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "GaragemCriar")]
        [Route("Lista")]
        public async Task<IActionResult> Post([FromBody] List<GaragemDTO> GaragemDTO)
        {
            return Ok(await _applicationServiceGaragem.CriarComLista(GaragemDTO));
        }

        //POST api/values
        /// <summary>
        /// Criar Garagem
        /// </summary>
        /// <response code="200">Garagem criada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "GaragemCriar")]
        public async Task<IActionResult> Post([FromBody] GaragemDTO GaragemDTO)
        {
            return Ok(await _applicationServiceGaragem.Criar(GaragemDTO));
        }

        //PUT api/values/5
        /// <summary>
        /// Atualizar Garagem
        /// </summary>
        /// <response code="200">Garagem alterada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "GaragemAtualizar")]
        public async Task<IActionResult> Put([FromBody] GaragemDTO GaragemDTO)
        {
            return Ok(await _applicationServiceGaragem.Atualizar(GaragemDTO));
        }

        //DELETE api/values/5
        /// <summary>
        /// Excluir Garagem
        /// </summary>
        /// <response code="200">Garagem removida com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "GaragemExcluir")]
        public async Task<IActionResult> Delete([FromQuery] string key)
        {
            return Ok(await _applicationServiceGaragem.Excluir(key));
        }

        //ENABLE DISABLE api/values/[key]/[status]  
        [HttpPost]
        [Route("Status")]
        public async Task<IActionResult> EnableDisable([FromQuery] string key, bool status)
        {
            if (status)
                return Ok(await _applicationServiceGaragem.Ativar(key));
            else
                return Ok(await _applicationServiceGaragem.Desativar(key));
        }
    }
}
