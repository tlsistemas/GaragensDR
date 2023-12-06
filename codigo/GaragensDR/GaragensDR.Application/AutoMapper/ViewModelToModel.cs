using AutoMapper;
using GaragensDR.Application.ViewModel;
using GaragensDR.Domain.Models;

namespace GaragensDR.Application.AutoMapper
{
    public class ViewModelToModel : Profile
    {
        public ViewModelToModel()
        {
            CreateMap<GaragemViewModel, Garagem>();
            CreateMap<FormaPagamentoViewModel, FormaPagamento>();
            CreateMap<PassagemViewModel, Passagem>();
        }
    }
}
