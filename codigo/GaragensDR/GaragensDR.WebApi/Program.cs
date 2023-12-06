
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GaragensDR.Application.AutoMapper;
using GaragensDR.Domain.Config;
using GaragensDR.Infra.CrossCutting.Ioc;
using GaragensDR.Infra.Data.Contexts;
using GaragensDR.Infra.Shared.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GaragensDR.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton(new AppSettings(builder.Configuration));
            #region Context
            // Add services to the container.
            builder.Services.AddDbContext<SqlContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddSingleton<DapperContext>();
            #endregion
            // Add services to the container.
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new ModuleIOC());
                });

            #region Localization
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
                options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
                options.RequestCultureProviders.Clear();
            });
            #endregion

            #region AutoMapper
            builder.Services.AddAutoMapper(typeof(ModelToViewModel), typeof(ViewModelToModel));
            builder.Services.AddAutoMapper(typeof(ModelToDto), typeof(DtoToModel));
            #endregion


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}