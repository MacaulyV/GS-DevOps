using ElectriXDriveUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElectriXDriveUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Construtor para injetar a dependência de IHttpClientFactory, que é usada para fazer chamadas HTTP.
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Login - Carrega a página de login.
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login - Método que processa as informações de login.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            // Verifica se os dados do formulário estão válidos
            if (ModelState.IsValid)
            {
                // Cria um objeto com os dados do usuário para enviar para a API
                var usuario = new
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    Senha = model.Senha
                };

                // Cria um cliente HTTP usando a fábrica de clientes configurada
                var client = _httpClientFactory.CreateClient("APIClient");

                // Envia uma requisição POST para a API com os dados do usuário
                var response = await client.PostAsJsonAsync("UsuarioApi", usuario);

                if (response.IsSuccessStatusCode)
                {
                    // Se o registro for bem-sucedido, redireciona para a página inicial
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Caso ocorra um erro, adiciona uma mensagem de erro para ser exibida na view
                    ModelState.AddModelError(string.Empty, "Erro ao registrar usuário. Tente novamente.");
                }
            }

            // Retorna a mesma view com o modelo caso ocorra algum problema no registro ou validação
            return View(model);
        }
    }
}
