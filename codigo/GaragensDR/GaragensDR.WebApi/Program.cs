using Autofac;
using Autofac.Extensions.DependencyInjection;
using GaragensDR.Application.AutoMapper;
using GaragensDR.Infra.CrossCutting.Ioc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;
using System.IO.Compression;
using System.Text;
using AutoMapper;
using GaragensDR.Infra.Data.Contexts;
using GaragensDR.Infra.Shared.Contexts;
using GaragensDR.Infra.Shared.Helpers;
using AgroControle.Domain.Config;

SwaggerOptions swaggerOptions = new SwaggerOptions();

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddSingleton(new AppSettings(builder.Configuration));

// Add services to the container.
builder.Configuration.GetSection("Swagger").Bind(swaggerOptions);

#region Infrastructure
builder.Services.AddHttpContextAccessor();
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

//services.AddScoped<IEnviarEmail, SmtpEnviarEmail>();
#endregion

#region Context
// Add services to the container.
builder.Services.AddDbContext<SqlContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<DapperContext>();
#endregion

#region Swagger
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});


builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddMvcCore().AddApiExplorer();
#endregion

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

builder.Services.AddMemoryCache();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new ModuleIOC());
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Logging.ClearProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/Error");

if (app.Environment.IsDevelopment())
{

}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();


