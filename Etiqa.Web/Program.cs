using AutoMapper;
using Etiqa.DataAccess;
using Etiqa.Domain;
using Etiqa.Domain.Context;
using Etiqa.Providers;
using Etiqa.Providers.Contracts;
using Etiqa.Repository;
using Etiqa.Repository.Contract;
using Etiqa.Security;
using Etiqa.Services;
using Etiqa.Services.Contract;
using Etiqa.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Registering DB Context.
{
    builder.Services.AddDbContext<EtiqaDbContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    });
}
// Add services to the container.
{
    builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
    builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
    builder.Services.AddScoped(typeof(IUserSkillRepository), typeof(UserSkillRepository));

    builder.Services.AddScoped<IUserProvider, UserProvider>();

    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddScoped(typeof(ICacheService), typeof(CacheService));

    builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

    builder.Services.AddScoped(typeof(ApiKeyAuthFilterAsync));

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.DocInclusionPredicate((docName, api) =>
        {
            var routeTemplate = api.RelativePath;
            if (!string.IsNullOrEmpty(routeTemplate) && routeTemplate.Equals("error"))
                return false;
            return true;
        });

        c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
        {
            Description = "Api Key is required to access the API.",
            Type = SecuritySchemeType.ApiKey,
            Name = "x-api-key",
            In = ParameterLocation.Header,
            Scheme = "ApiKeySchema"
        });

        var scheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "ApiKey"
            },
            In = ParameterLocation.Header,
        };

        var requirement = new OpenApiSecurityRequirement
        {
            { scheme, new List<string>() }
        };
        c.AddSecurityRequirement(requirement);
    });
}
// Configure auto-mapper.
{
    var mapperConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new MappingProfile());
    });
    IMapper mapper = mapperConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    //app.UseMiddleware<ApiKeyAuthMiddleware>();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}


