using System.Reflection;
using AutoMapper;
using Microsoft.OpenApi.Models;
using SocialNetworkApi.Authorization.Jwt;
using SocialNetworkApi.Core;
using SocialNetworkApi.Core.Data;
using SocialNetworkApi.Helpers;
using SocialNetworkApi.Model;
using SocialNetworkApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddDbContext<DataContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
builder.Services.AddControllers();

//automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); });
mapperConfig.AssertConfigurationIsValid();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Social Network API", Version = "v1", Contact = new OpenApiContact(),
            Description = "Last Update: " + DateTime.Now
        });
    c.CustomSchemaIds(x => x.FullName);
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<ICoreService, CoreService>();
builder.Services.AddScoped<IJsonWebTokenService, JsonWebTokenService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseAuthorization();

// custom jwt auth middleware

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();

//for xUnit
public partial class Program { }