using AutoMapper;
using GaragensDR.Domain.DTO;
using GaragensDR.Domain.Models;

namespace GaragensDR.Application.AutoMapper
{
    public class ModelToDto : Profile
    {
        public ModelToDto()
        {
            #region Acessos 
            CreateMap<Garagem, GaragemDTO>();
            CreateMap<FormaPagamento, FormaPagamentoDTO>();
            CreateMap<Passagem, PassagemDTO>();
            #endregion
        }

    }
}
