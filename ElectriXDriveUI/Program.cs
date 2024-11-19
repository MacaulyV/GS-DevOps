using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Configuração do HttpClient para acessar a API
builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri("http://191.235.238.91:5286/api/"); // URL base da sua API
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

// Registrar os repositórios e associar às suas interfaces
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IVeiculoCombustaoRepository, VeiculoCombustaoRepository>();
builder.Services.AddScoped<IVeiculoEletricoRepository, VeiculoEletricoRepository>();

// Configuração para usar Controllers e Views
builder.Services.AddControllersWithViews();

// Adicionar suporte a sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configuração do pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Adicionar suporte a sessão
app.UseSession();

// Configuração das rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
