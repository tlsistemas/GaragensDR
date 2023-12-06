using GaragensDR.Application.Interfaces;
using GaragensDR.Application.Parameters;
using GaragensDR.Application.ViewModel;
using GaragensDR.Domain.DTO;
using GaragensDR.Infra.Shared.Bases;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GaragensDR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassagemController : BaseController
    {
        private readonly IPassagemApplication _applicationServicePassagem;

        public PassagemController(IPassagemApplication applicationServicePassagem)
        {
            _applicationServicePassagem = applicationServicePassagem;
        }

        /// <summary>
        /// Listar Passagem
        /// </summary>
        /// <response code="200">Lista de Passagem processada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<PassagemViewModel>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "PassagemListar")]
        public async Task<IActionResult> Get([FromQuery] PassagemParams parametros)
        {
            return Ok(await _applicationServicePassagem.Listar(parametros));
        }

        //POST api/values
        /// <summary>
        /// Criar Passagens
        /// </summary>
        /// <response code="200">Passagens criada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "PassagemCriar")]
        [Route("Lista")]
        public async Task<IActionResult> Post([FromBody] List<PassagemDTO> GaragemDTO)
        {
            return Ok(await _applicationServicePassagem.CriarComLista(GaragemDTO));
        }

        //POST api/values
        /// <summary>
        /// Criar Passagem
        /// </summary>
        /// <response code="200">Passagem criada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "PassagemCriar")]
        public async Task<IActionResult> Post([FromBody] PassagemDTO PassagemDTO)
        {
            return Ok(await _applicationServicePassagem.Criar(PassagemDTO));
        }

        //PUT api/values/5
        /// <summary>
        /// Atualizar Passagem
        /// </summary>
        /// <response code="200">Passagem alterada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "PassagemAtualizar")]
        public async Task<IActionResult> Put([FromBody] PassagemDTO PassagemDTO)
        {
            return Ok(await _applicationServicePassagem.Atualizar(PassagemDTO));
        }

        //DELETE api/values/5
        /// <summary>
        /// Excluir Passagem
        /// </summary>
        /// <response code="200">Passagem removida com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "PassagemExcluir")]
        public async Task<IActionResult> Delete([FromQuery] string key)
        {
            return Ok(await _applicationServicePassagem.Excluir(key));
        }

        //ENABLE DISABLE api/values/[key]/[status]  
        [HttpPost]
        [Route("Status")]
        public async Task<IActionResult> EnableDisable([FromQuery] string key, bool status)
        {
            if (status)
                return Ok(await _applicationServicePassagem.Ativar(key));
            else
                return Ok(await _applicationServicePassagem.Desativar(key));
        }
    }
}
