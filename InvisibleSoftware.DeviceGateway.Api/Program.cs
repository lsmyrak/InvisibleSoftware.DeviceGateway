using Autofac;
using Autofac.Extensions.DependencyInjection;
using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Api;
using InvisibleSoftware.DeviceGateway.Application.Auth.Commands;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using InvisibleSoftware.DeviceGateway.Infrastructure;
using InvisibleSoftware.DeviceGateway.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var authenticationSetting = builder.Configuration.GetSection("Authentication").Get<AuthenticationSetting>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "InvisibleSoftware.DeviceGateway.Api",
        Version = "v1"
    });
  
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Wpisz token JWT jako: Bearer {token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddDbContext<ApplicationContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql"))
    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
    .EnableSensitiveDataLogging());

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = authenticationSetting.JwtIssuer,
        ValidAudience = authenticationSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSetting.JwtKey))
    };
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
   
    containerBuilder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<HistoryService>().As<IHistoryService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<MqttService>().As<IMqttService>().InstancePerLifetimeScope();
    containerBuilder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
    .AsImplementedInterfaces();
});


var applicationAssembly = typeof(LoginCommand).Assembly;

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),
        applicationAssembly
    )
);



builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy", c =>
{
    c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    try
    {
        Console.WriteLine("Sprawdzanie i wykonywanie migracji...");
        dbContext.Database.Migrate();
        Console.WriteLine("Migracje wykonane pomyœlnie.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Wyst¹pi³ b³¹d podczas migracji: {ex.Message}");
    }
}

app.UseCors("CorsPolicy");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
