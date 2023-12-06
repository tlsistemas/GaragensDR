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
    public class FormaPagamentoController : BaseController
    {
        private readonly IFormaPagamentoApplication _applicationServiceFormaPagamento;

        public FormaPagamentoController(IFormaPagamentoApplication applicationServiceFormaPagamento)
        {
            _applicationServiceFormaPagamento = applicationServiceFormaPagamento;
        }

        /// <summary>
        /// Listar Formas de Pagamento
        /// </summary>
        /// <response code="200">Lista de Formas de Pagamento processado com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<FormaPagamentoViewModel>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "FormaPagamentoListar")]
        public async Task<IActionResult> Get([FromQuery] FormaPagamentoParams parametros)
        {
            return Ok(await _applicationServiceFormaPagamento.Listar(parametros));
        }

        //POST api/values
        /// <summary>
        /// Criar Forma de Pagamento
        /// </summary>
        /// <response code="200">Forma de Pagamento criado com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "FormaPagamentoCriar")]
        public async Task<IActionResult> Post([FromBody] FormaPagamentoDTO FormaPagamentoDTO)
        {
            return Ok(await _applicationServiceFormaPagamento.Criar(FormaPagamentoDTO));
        }

        //PUT api/values/5
        /// <summary>
        /// Atualizar Forma de Pagamento
        /// </summary>
        /// <response code="200">Forma de Pagamento alterado com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "FormaPagamentoAtualizar")]
        public async Task<IActionResult> Put([FromBody] FormaPagamentoDTO FormaPagamentoDTO)
        {
            return Ok(await _applicationServiceFormaPagamento.Atualizar(FormaPagamentoDTO));
        }

        //DELETE api/values/5
        /// <summary>
        /// Excluir Forma de Pagamento
        /// </summary>
        /// <response code="200">Forma de Pagamento removido com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "FormaPagamentoExcluir")]
        public async Task<IActionResult> Delete([FromQuery] string key)
        {
            return Ok(await _applicationServiceFormaPagamento.Excluir(key));
        }

        //ENABLE DISABLE api/values/[key]/[status]  
        [HttpPost]
        [Route("Status")]
        public async Task<IActionResult> EnableDisable([FromQuery] string key, bool status)
        {
            if (status)
                return Ok(await _applicationServiceFormaPagamento.Ativar(key));
            else
                return Ok(await _applicationServiceFormaPagamento.Desativar(key));
        }
    }
}
