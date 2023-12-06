using AutoMapper;
using GaragensDR.Application.ViewModel;
using GaragensDR.Domain.Models;

namespace GaragensDR.Application.AutoMapper
{
    public class ModelToViewModel : Profile
    {
        public ModelToViewModel()
        {
            CreateMap<Garagem, GaragemViewModel>();
            CreateMap<Passagem, PassagemViewModel>();
            CreateMap<FormaPagamento, FormaPagamentoViewModel>();
        }
    }
}
