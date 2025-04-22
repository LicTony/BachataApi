using BachataApi.Configuration;
using BachataApi.DTOs;
using BachataApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


//Lee la sección MongoDbSettings del appsettings.json (o de variables de entorno, etc.)
//Crea una instancia de la clase MongoDbSettings con esos valores.
//La registra como un servicio en el contenedor de dependencias (DI) para que puedas inyectarla más tarde.
builder.Services
    .AddOptions<MongoDbSettings>()
    .Bind(builder.Configuration.GetSection("MongoDbSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart(); // <- Arroja excepción si hay errores de validación al iniciar

builder.Services
    .AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetSection("Jwt"))
    .ValidateDataAnnotations()
    .ValidateOnStart(); // <- Arroja excepción si hay errores de validación al iniciar

builder.Services
    .AddOptions<UserSettings>()
    .Bind(builder.Configuration.GetSection("User"))
    .ValidateDataAnnotations()
    .ValidateOnStart(); // <- Arroja excepción si hay errores de validación al iniciar


var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings!.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key!))
    };
});


builder.Services.AddSingleton<FiguraService>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<UserService>();

builder.Services.AddControllers();

//Personalizar la respuesta de errores
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(ms => ms.Value?.Errors.Count > 0)
            .SelectMany(kvp => kvp.Value!.Errors
                .Select(e => new ErrorItem
                {
                    Field = kvp.Key,
                    Message = e.ErrorMessage
                }))
            .ToList();

        var errorResponse = new ErrorResponse
        {
            StatusCode = 400,
            Message = "Solicitud inválida. Por favor revise los campos.",
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});


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
            Url = new Uri($"https://bachataapi-1.onrender.com")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri($"https://opensource.org/licenses/MIT")
        }
    });


    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer", //  importante
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Ingresa el JWT.\n\nEjemplo: eyJhbGciOiJIUzI1NiIsInR...",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });




    //Para que salga las descripciones en el swagger
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


app.MapGet("/", () =>
{
    return Results.Json(new
    {
        mensaje = "Estoy viva!",
        fecha = DateTime.Now.ToString("yyyy-MM-dd"),
        hora = DateTime.Now.ToString("HH:mm:ss")
    });
}).ExcludeFromDescription();


//Mensajes en español
var defaultCulture = new CultureInfo("es-AR"); // o "es-ES"
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = [defaultCulture],
    SupportedUICultures = [defaultCulture]
};

app.UseRequestLocalization(localizationOptions);


app.Run();


