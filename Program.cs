using BachataApi.Configuration;
using BachataApi.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


//Lee la sección MongoDbSettings del appsettings.json (o de variables de entorno, etc.)
//Crea una instancia de la clase MongoDbSettings con esos valores.
//La registra como un servicio en el contenedor de dependencias (DI) para que puedas inyectarla más tarde.
//builder.Services.Configure<MongoDbSettings>(
builder.Services
    .AddOptions<MongoDbSettings>()
    .Bind(builder.Configuration.GetSection("MongoDbSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart(); // <- Arroja excepción si hay errores de validación al iniciar



builder.Services.AddSingleton<FiguraService>();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Figuras de Baile",
        Version = "v1",
        Description = "Esta API permite gestionar figuras de bachata almacenadas en MongoDB. Incluye operaciones para listar, agregar, editar y eliminar figuras.",
        Contact = new OpenApiContact
        {
            Name = "By Tony",
            Email = "tony@tuproyecto.com",
            Url = new Uri("https://bachataapi-1.onrender.com")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });


    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});



var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
// en un ambiente producivo real no va el Swagger  
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.MapControllers();

app.Run();


