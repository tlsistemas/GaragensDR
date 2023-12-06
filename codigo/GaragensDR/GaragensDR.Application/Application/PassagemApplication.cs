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
    public class PassagemApplication : IPassagemApplication
    {
        private readonly IPassagemService _service;
        private readonly ILogger<PassagemApplication> _logger;
        private readonly IMapper _mapper;

        public PassagemApplication(IPassagemService service,
            ILogger<PassagemApplication> logger,
            IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<PassagemViewModel>>> Listar(PassagemParams parametros)
        {
            var response = new BasePaginationResponse<IEnumerable<PassagemViewModel>>();

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

                response.Data = _mapper.Map<IEnumerable<PassagemViewModel>>(obj);
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

        public async Task<BaseResponse<bool>> CriarComLista(List<PassagemDTO> passagemDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                if (passagemDto is null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message));
                    return response;
                }
                var listaGaragens = _mapper.Map<List<Passagem>>(passagemDto);
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
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, passagemDto);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<PassagemViewModel>> Criar(PassagemDTO passagemDto)
        {
            var response = new BaseResponse<PassagemViewModel>();

            try
            {
                if (passagemDto is null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message));
                    return response;
                }

                if (passagemDto.Garagem is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.Garagem));
                    return response;
                }

                if (passagemDto.FormaPagamento is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.FormaPagamento));
                    return response;
                }

                if (passagemDto.CarroMarca is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.CarroMarca));
                    return response;
                }

                if (passagemDto.CarroPlaca is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.CarroPlaca));
                    return response;
                }

                if (passagemDto.CarroModelo is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.CarroModelo));
                    return response;
                }

                if (passagemDto.PrecoTotal is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.PrecoTotal));
                    return response;
                }

                var existingObj = _service.Get(x => x.Garagem == passagemDto.Garagem
                                                    && x.CarroPlaca == passagemDto.CarroPlaca).ToList();

                if (existingObj != null && existingObj.Count > 0)
                {
                    response.AddError(Events.EXIST_PROPERTY_GENERIC, "Garagem,CarroPlaca");
                    return response;
                }

                var obj = new Passagem
                {
                    Garagem = passagemDto.Garagem,
                    CarroPlaca = passagemDto.CarroPlaca,
                    PrecoTotal = passagemDto.PrecoTotal,
                    CarroModelo = passagemDto.CarroModelo,
                    CarroMarca = passagemDto.CarroMarca,
                    FormaPagamento = passagemDto.FormaPagamento,
                    DataHoraEntrada = passagemDto.DataHoraEntrada,
                    DataHoraSaida = passagemDto.DataHoraSaida
                };

                await _service.AddAsync(obj);
                response.Data = _mapper.Map<PassagemViewModel>(obj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, passagemDto);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<PassagemViewModel>> Atualizar(PassagemDTO passagemDto)
        {
            var response = new BaseResponse<PassagemViewModel>();

            try
            {
                if (passagemDto is null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message));
                    return response;
                }

                if (passagemDto.Garagem is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.Garagem));
                    return response;
                }

                if (passagemDto.FormaPagamento is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.FormaPagamento));
                    return response;
                }

                if (passagemDto.CarroMarca is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.CarroMarca));
                    return response;
                }

                if (passagemDto.CarroPlaca is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.CarroPlaca));
                    return response;
                }

                if (passagemDto.CarroModelo is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.CarroModelo));
                    return response;
                }

                if (passagemDto.PrecoTotal is null)
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemDto.PrecoTotal));
                    return response;
                }

                var passagem = new Passagem { Key = passagemDto.Key };

                var existingObj = _service.Get(x => x.Id == passagem.Id).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddError(Events.EXIST_PROPERTY_GENERIC, "Passagem");
                    return response;
                }

                existingObj.Id = passagemDto.Id;
                existingObj.Garagem = passagemDto.Garagem;
                existingObj.CarroPlaca = passagemDto.CarroPlaca;
                existingObj.PrecoTotal = passagemDto.PrecoTotal;
                existingObj.CarroModelo = passagemDto.CarroModelo;
                existingObj.CarroMarca = passagemDto.CarroMarca;
                existingObj.FormaPagamento = passagemDto.FormaPagamento;
                existingObj.DataHoraEntrada = passagemDto.DataHoraEntrada;
                existingObj.DataHoraSaida = passagemDto.DataHoraSaida;
                existingObj.Ativo = passagemDto.Ativo;
                existingObj.DataAtualizacao = DateTime.Now;
                await _service.UpdateAsync(existingObj);
                response.Data = _mapper.Map<PassagemViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, passagemDto);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<PassagemViewModel>> Excluir(string passagemKey)
        {
            var response = new BaseResponse<PassagemViewModel>();

            try
            {
                if (passagemKey == "")
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemKey));
                    return response;
                }

                var obj = new Passagem { Key = passagemKey };
                if (obj.Id == 0)
                    obj = new Passagem { Key = passagemKey };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_FOUND.Message, "IdPassagem"));
                    return response;
                }

                await _service.RemoveAsync(existingObj);

                response.Data = _mapper.Map<PassagemViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, passagemKey);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<PassagemViewModel>> Desativar(string passagemKey)
        {
            var response = new BaseResponse<PassagemViewModel>();

            try
            {
                if (passagemKey == "")
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemKey));
                    return response;
                }

                var obj = new Passagem { Key = passagemKey };
                if (obj.Id == 0)
                    obj = new Passagem { Key = passagemKey };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_FOUND.Message, "IdPassagem"));
                    return response;
                }

                existingObj.Ativo = false;
                await _service.UpdateAsync(existingObj);

                response.Data = _mapper.Map<PassagemViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, passagemKey);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            finally
            {
                _service.DisposeAsync();
            }
            return response;
        }

        public async Task<BaseResponse<PassagemViewModel>> Ativar(string passagemKey)
        {
            var response = new BaseResponse<PassagemViewModel>();

            try
            {
                if (passagemKey == "")
                {
                    response.AddErrors(string.Format(Events.INVALID_EMPTY.Message, passagemKey));
                    return response;
                }

                var obj = new Passagem { Key = passagemKey };
                if (obj.Id == 0)
                    obj = new Passagem { Key = passagemKey };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    response.AddErrors(string.Format(Events.SYSTEM_ERROR_NOT_FOUND.Message, "IdPassagem"));
                    return response;
                }

                existingObj.Ativo = true;
                await _service.UpdateAsync(existingObj);

                response.Data = _mapper.Map<PassagemViewModel>(existingObj);
                response.Error = false;
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical((int)EEventId.Application, ex, Events.SYSTEM_ERROR_NOT_HANDLED.Message, passagemKey);
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