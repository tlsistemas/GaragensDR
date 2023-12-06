using AutoMapper;
using GaragensDR.Application.AutoMapper;
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
    public class GaragemApplication : IGaragemApplication
    {
        private readonly IGaragemService _service;
        private readonly ILogger<GaragemApplication> _logger;
        private readonly IMapper _mapper;

        public GaragemApplication(IGaragemService service,
            ILogger<GaragemApplication> logger,
            IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<GaragemViewModel>>> Listar(GaragemParams parametros)
        {
            var response = new BasePaginationResponse<IEnumerable<GaragemViewModel>>();

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

                response.Data = _mapper.Map<IEnumerable<GaragemViewModel>>(obj);
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

        public async Task<BaseResponse<GaragemViewModel>> Criar(GaragemDTO garagemDto)
        {
            var response = new BaseResponse<GaragemViewModel>();

            try
            {
                if (garagemDto is null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message));
                    return response;
                }

                if (garagemDto.Nome is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, garagemDto.Nome));
                    return response;
                }

                var existingObj = _service.Get(x => x.Nome == garagemDto.Nome).ToList();

                if (existingObj != null && existingObj.Count > 0)
                {
                    response.AddError(Events.EXIST_PROPERTY_GENERIC, "GaragemNome");
                    return response;
                }

                var obj = new Garagem
                {
                    Nome = garagemDto.Nome,
                    DataCriacao = DateTime.Now
                };

                await _service.AddAsync(obj);
                response.Data = _mapper.Map<GaragemViewModel>(obj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, garagemDto);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<GaragemViewModel>> Atualizar(GaragemDTO garagemDto)
        {
            var response = new BaseResponse<GaragemViewModel>();

            try
            {
                if (garagemDto is null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message));
                    return response;
                }

                if (garagemDto.Nome is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, garagemDto.Nome));
                    return response;
                }

                var existingObj = _service.Get(x => x.Nome == garagemDto.Nome).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddError(Events.EXIST_PROPERTY_GENERIC, "Garagem");
                    return response;
                }

                existingObj.Id = garagemDto.Id;
                existingObj.Nome = garagemDto.Nome;
                existingObj.Ativo = garagemDto.Ativo;
                existingObj.DataAtualizacao = DateTime.Now;
                await _service.UpdateAsync(existingObj);
                response.Data = _mapper.Map<GaragemViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, garagemDto);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<GaragemViewModel>> Excluir(string garagemKey)
        {
            var response = new BaseResponse<GaragemViewModel>();

            try
            {
                if (garagemKey == "")
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, garagemKey));
                    return response;
                }

                var obj = new Garagem { Key = garagemKey };
                if (obj.Id == 0)
                    obj = new Garagem { Key = garagemKey };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_FOUND.Message, "IdGaragem"));
                    return response;
                }

                await _service.RemoveAsync(existingObj);

                response.Data = _mapper.Map<GaragemViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, garagemKey);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<GaragemViewModel>> Desativar(string garagemKey)
        {
            var response = new BaseResponse<GaragemViewModel>();

            try
            {
                if (garagemKey == "")
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, garagemKey));
                    return response;
                }

                var obj = new Garagem { Key = garagemKey };
                if (obj.Id == 0)
                    obj = new Garagem { Key = garagemKey };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_FOUND.Message, "IdGaragem"));
                    return response;
                }

                existingObj.Ativo = false;
                await _service.UpdateAsync(existingObj);

                response.Data = _mapper.Map<GaragemViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, garagemKey);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<GaragemViewModel>> Ativar(string garagemKey)
        {
            var response = new BaseResponse<GaragemViewModel>();

            try
            {
                if (garagemKey == "")
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, garagemKey));
                    return response;
                }

                var obj = new Garagem { Key = garagemKey };
                if (obj.Id == 0)
                    obj = new Garagem { Key = garagemKey };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_FOUND.Message, "IdGaragem"));
                    return response;
                }

                existingObj.Ativo = true;
                await _service.UpdateAsync(existingObj);

                response.Data = _mapper.Map<GaragemViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, garagemKey);
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