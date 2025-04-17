using BachataApi.Configuration;
using BachataApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Agrega configuración extra si hace falta (opcional)
builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddEnvironmentVariables();
});


// Add services to the container.
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));



builder.Services.AddSingleton<FiguraService>();
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

app.MapControllers();

app.Run();


