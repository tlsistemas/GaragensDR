using AutoMapper;
using GaragensDR.Application.Interfaces;
using GaragensDR.Application.Parameters;
using GaragensDR.Application.ViewModel;
using GaragensDR.Domain.DTO;
using GaragensDR.Domain.Enums;
using GaragensDR.Domain.Interfaces.Services;
using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases;
using Microsoft.Extensions.Logging;
using System.Net;

namespace GaragensDR.Application.Application
{
    public class FormaPagamentoApplication : IFormaPagamentoApplication
    {
        private readonly IFormaPagamentoService _service;
        private readonly ILogger<FormaPagamentoApplication> _logger;
        private readonly IMapper _mapper;

        public FormaPagamentoApplication(IFormaPagamentoService service,
            ILogger<FormaPagamentoApplication> logger,
            IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<FormaPagamentoViewModel>>> Listar(FormaPagamentoParams parametros)
        {
            var response = new BasePaginationResponse<IEnumerable<FormaPagamentoViewModel>>();

            try
            {

                var obj = await _service.GetByParamsAsync(parametros.Filter(), parametros.OrderBy, parametros.Include);

                response.Count = obj.Count();

                if (parametros.Skip.HasValue)
                {
                    obj = obj.Skip(parametros.Skip.Value);
                }

                if (parametros.Take.HasValue && parametros.Take.Value > 0)
                {
                    obj = obj.Take(parametros.Take.Value);
                }

                if (parametros.OrderBy != string.Empty)
                {
                    obj = obj.OrderBy(x => parametros.OrderBy);
                }

                response.Data = _mapper.Map<IEnumerable<FormaPagamentoViewModel>>(obj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, parametros);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<bool>> CriarComLista(List<FormaPagamentoDTO> formaPagamentoDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                if (formaPagamentoDto is null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message));
                    return response;
                }
                var listaGaragens = _mapper.Map<List<FormaPagamento>>(formaPagamentoDto);
                await _service.AdicionarLista(listaGaragens);
                response.Data = true;
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, formaPagamentoDto);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<FormaPagamentoViewModel>> Criar(FormaPagamentoDTO formaPagamentoDto)
        {
            var response = new BaseResponse<FormaPagamentoViewModel>();

            try
            {
                if (formaPagamentoDto is null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message));
                    return response;
                }

                if (formaPagamentoDto.Codigo is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, formaPagamentoDto.Codigo));
                    return response;
                }

                if (formaPagamentoDto.Descricao is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, formaPagamentoDto.Descricao));
                    return response;
                }

                var existingObj = _service.Get(x => x.Codigo == formaPagamentoDto.Codigo).ToList();

                if (existingObj != null && existingObj.Count > 0)
                {
                    response.AddError(Events.EXIST_PROPERTY_GENERIC, "Garagem,CarroPlaca");
                    return response;
                }

                var obj = new FormaPagamento
                {
                    Codigo = formaPagamentoDto.Codigo,
                    Descricao = formaPagamentoDto.Descricao
                };

                await _service.AddAsync(obj);
                response.Data = _mapper.Map<FormaPagamentoViewModel>(obj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, formaPagamentoDto);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<FormaPagamentoViewModel>> Atualizar(FormaPagamentoDTO formaPagamentoDto)
        {
            var response = new BaseResponse<FormaPagamentoViewModel>();

            try
            {
                if (formaPagamentoDto is null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message));
                    return response;
                }

                if (formaPagamentoDto.Codigo is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, formaPagamentoDto.Codigo));
                    return response;
                }

                if (formaPagamentoDto.Descricao is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, formaPagamentoDto.Descricao));
                    return response;
                }

                var FormaPagamento = new FormaPagamento { Key = formaPagamentoDto.Key };

                var existingObj = _service.Get(x => x.Id == FormaPagamento.Id).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddError(Events.EXIST_PROPERTY_GENERIC, "FormaPagamento");
                    return response;
                }

                existingObj.Id = formaPagamentoDto.Id;
                existingObj.Codigo = formaPagamentoDto.Codigo;
                existingObj.Descricao = formaPagamentoDto.Descricao;
                existingObj.DataAtualizacao = DateTime.Now;
                await _service.UpdateAsync(existingObj);
                response.Data = _mapper.Map<FormaPagamentoViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, formaPagamentoDto);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<FormaPagamentoViewModel>> Excluir(string formaPagamentoKey)
        {
            var response = new BaseResponse<FormaPagamentoViewModel>();

            try
            {
                if (formaPagamentoKey == "")
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, formaPagamentoKey));
                    return response;
                }

                var obj = new FormaPagamento { Key = formaPagamentoKey };
                if (obj.Id == 0)
                    obj = new FormaPagamento { Key = formaPagamentoKey };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_FOUND.Message, "IdFormaPagamento"));
                    return response;
                }

                await _service.RemoveAsync(existingObj);

                response.Data = _mapper.Map<FormaPagamentoViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, formaPagamentoKey);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<FormaPagamentoViewModel>> Desativar(string formaPagamentoKey)
        {
            var response = new BaseResponse<FormaPagamentoViewModel>();

            try
            {
                if (formaPagamentoKey == "")
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, formaPagamentoKey));
                    return response;
                }

                var obj = new FormaPagamento { Key = formaPagamentoKey };
                if (obj.Id == 0)
                    obj = new FormaPagamento { Key = formaPagamentoKey };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_FOUND.Message, "IdFormaPagamento"));
                    return response;
                }

                existingObj.Ativo = false;
                await _service.UpdateAsync(existingObj);

                response.Data = _mapper.Map<FormaPagamentoViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, formaPagamentoKey);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<FormaPagamentoViewModel>> Ativar(string formaPagamentoKey)
        {
            var response = new BaseResponse<FormaPagamentoViewModel>();

            try
            {
                if (formaPagamentoKey == "")
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, formaPagamentoKey));
                    return response;
                }

                var obj = new FormaPagamento { Key = formaPagamentoKey };
                if (obj.Id == 0)
                    obj = new FormaPagamento { Key = formaPagamentoKey };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_FOUND.Message, "IdFormaPagamento"));
                    return response;
                }

                existingObj.Ativo = true;
                await _service.UpdateAsync(existingObj);

                response.Data = _mapper.Map<FormaPagamentoViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, formaPagamentoKey);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task Dispose()
        {
            _service.DisposeAsync();
        }
    }
}