using AutoMapper;
using Etiqa.DataAccess;
using Etiqa.Domain;
using Etiqa.Domain.Context;
using Etiqa.Repository;
using Etiqa.Repository.Contract;
using Etiqa.Services.Contract;
using Etiqa.Services.Services;
using Microsoft.EntityFrameworkCore;
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

    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
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
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}


