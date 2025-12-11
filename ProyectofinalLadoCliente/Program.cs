using ProyectofinalLadoCliente.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ConversionService>();

var app = builder.Build();

app.UseFileServer();

app.MapControllers();
app.Run();
