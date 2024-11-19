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

// Adicionar serviços ao contêiner de injeção de dependências.
builder.Services.AddControllersWithViews();

/// <summary>
/// Configura o DbContext para usar o banco de dados Oracle.
/// </summary>
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

/// <summary>
/// Configura os repositórios para injeção de dependência.
/// </summary>
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IVeiculoCombustaoRepository, VeiculoCombustaoRepository>();
builder.Services.AddScoped<IVeiculoEletricoRepository, VeiculoEletricoRepository>();

/// <summary>
/// Adiciona o serviço personalizado para veículos.
/// </summary>
builder.Services.AddScoped<VeiculoService>();

/// <summary>
/// Configura a chave secreta para geração de tokens JWT.
/// </summary>
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

/// <summary>
/// Configura a autenticação JWT para proteger a aplicação.
/// </summary>
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Em produção, use true para garantir o uso de HTTPS.
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // Valida a chave usada para assinar o token JWT.
        IssuerSigningKey = new SymmetricSecurityKey(key), // A chave de assinatura do token.
        ValidateIssuer = false, // Para produção, valide o emissor.
        ValidateAudience = false // Para produção, valide a audiência.
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
/// Configura o Swagger para a documentação da API.
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Gestão de Veículos API",
        Version = "v1",
        Description = "API para gerenciamento e comparação de veículos",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Seu Nome",
            Email = "seuemail@dominio.com"
        }
    });

    // Configurar autenticação via API Key no Swagger
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Chave de API necessária para acessar os endpoints. Insira no cabeçalho: ApiKey {sua chave}",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "ApiKey",
        Scheme = "ApiKeyScheme"
    });

    // Configurar autenticação JWT no Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Por favor, insira o token JWT no formato: Bearer {seu token}"
    });

    // Requisitos de segurança para proteger os endpoints
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
/// Configura o pipeline de requisição HTTP.
/// </summary>
if (app.Environment.IsDevelopment())
{
    // Configuração para ambiente de desenvolvimento
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Configuração para ambiente de produção
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirecionamento para HTTPS por segurança
app.UseStaticFiles(); // Habilita arquivos estáticos

// Middleware de Tratamento de Exceções
app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting(); // Configuração de rotas

// Configuração de CORS
app.UseCors("PermitirTudo");

// Middleware de Autenticação e Autorização
app.UseAuthentication();
app.UseAuthorization();

// Mapeamento dos controladores
app.MapControllers();

app.Run(); // Executa a aplicação
