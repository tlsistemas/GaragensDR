using Autofac;
using GaragensDR.Application.Application;
using GaragensDR.Application.Interfaces;
using GaragensDR.Domain.Interfaces.Repositories;
using GaragensDR.Domain.Interfaces.Services;
using GaragensDR.Domain.Models;
using GaragensDR.Domain.Services;
using GaragensDR.Infra.Data.Repositories;

namespace GaragensDR.Infra.CrossCutting.Ioc
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region IOC Application
            builder.RegisterType<GaragemApplication>().As<IGaragemApplication>();
            builder.RegisterType<PassagemApplication>().As<IPassagemApplication>();
            builder.RegisterType<FormaPagamentoApplication>().As<IFormaPagamentoApplication>();
            #endregion

            #region IOC Services
            builder.RegisterType<GaragemService>().As<IGaragemService>();
            builder.RegisterType<PassagemService>().As<IPassagemService>();
            builder.RegisterType<FormaPagamentoService>().As<IFormaPagamentoService>();
            #endregion

            #region IOC Repositories SQL
            builder.RegisterType<GaragemRepository>().As<IGaragemRepository>();
            builder.RegisterType<PassagemRepository>().As<IPassagemRepository>();
            builder.RegisterType<FormaPagamentoRepository>().As<IFormaPagamentoRepository>();
            #endregion
        }
    }
}
