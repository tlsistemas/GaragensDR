using AutoMapper;
using GaragensDR.Domain.DTO;
using GaragensDR.Domain.Models;

namespace GaragensDR.Application.AutoMapper
{
    public class DtoToModel : Profile
    {
        public DtoToModel()
        {
            CreateMap<PassagemDTO, Passagem>();
            CreateMap<GaragemDTO, Garagem>();
            CreateMap<FormaPagamentoDTO, FormaPagamento>();
        }

    }
}
