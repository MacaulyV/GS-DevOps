using Microsoft.EntityFrameworkCore;
using GestaoVeiculos.Data;
using GestaoVeiculos.Repositories;
using GestaoVeiculos.Filters;
using GestaoVeiculos.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GestaoVeiculos.Repositories.Interfaces;
using GestaoVeiculos.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Adicionar servi�os ao cont�iner de inje��o de depend�ncias.
builder.Services.AddControllersWithViews();

/// <summary>
/// Configura o DbContext para usar o banco de dados Oracle.
/// </summary>
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

/// <summary>
/// Configura os reposit�rios para inje��o de depend�ncia.
/// </summary>
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IVeiculoCombustaoRepository, VeiculoCombustaoRepository>();
builder.Services.AddScoped<IVeiculoEletricoRepository, VeiculoEletricoRepository>();

/// <summary>
/// Adiciona o servi�o personalizado para ve�culos.
/// </summary>
builder.Services.AddScoped<VeiculoService>();

/// <summary>
/// Configura a chave secreta para gera��o de tokens JWT.
/// </summary>
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

/// <summary>
/// Configura a autentica��o JWT para proteger a aplica��o.
/// </summary>
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Em produ��o, use true para garantir o uso de HTTPS.
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // Valida a chave usada para assinar o token JWT.
        IssuerSigningKey = new SymmetricSecurityKey(key), // A chave de assinatura do token.
        ValidateIssuer = false, // Para produ��o, valide o emissor.
        ValidateAudience = false // Para produ��o, valide a audi�ncia.
    };
});

/// <summary>
/// Configura CORS para permitir qualquer origem - apenas para desenvolvimento.
/// </summary>
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

/// <summary>
/// Configura o Swagger para a documenta��o da API.
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Gest�o de Ve�culos API",
        Version = "v1",
        Description = "API para gerenciamento e compara��o de ve�culos",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Seu Nome",
            Email = "seuemail@dominio.com"
        }
    });

    // Configurar autentica��o via API Key no Swagger
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Chave de API necess�ria para acessar os endpoints. Insira no cabe�alho: ApiKey {sua chave}",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "ApiKey",
        Scheme = "ApiKeyScheme"
    });

    // Configurar autentica��o JWT no Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Por favor, insira o token JWT no formato: Bearer {seu token}"
    });

    // Requisitos de seguran�a para proteger os endpoints
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new List<string>()
        },
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

/// <summary>
/// Configura o pipeline de requisi��o HTTP.
/// </summary>
if (app.Environment.IsDevelopment())
{
    // Configura��o para ambiente de desenvolvimento
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Configura��o para ambiente de produ��o
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirecionamento para HTTPS por seguran�a
app.UseStaticFiles(); // Habilita arquivos est�ticos

// Middleware de Tratamento de Exce��es
app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting(); // Configura��o de rotas

// Configura��o de CORS
app.UseCors("PermitirTudo");

// Middleware de Autentica��o e Autoriza��o
app.UseAuthentication();
app.UseAuthorization();

// Mapeamento dos controladores
app.MapControllers();

app.Run(); // Executa a aplica��o
