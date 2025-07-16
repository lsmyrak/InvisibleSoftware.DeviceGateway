using InvisibleSoftware.DeviceGateway.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "InvisibleSoftware.DeviceGateway.Api",
        Version = "v1"
    }
    );
}
);

builder.Services.AddDbContext<ApplicationContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql"))
    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
    .EnableSensitiveDataLogging());

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
    typeof(Program).Assembly
));


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
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
