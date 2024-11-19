using ElectriXDriveUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace ElectriXDriveUI.Controllers
{
    public class VeiculoEletricoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Construtor que injeta a dependência de IHttpClientFactory para fazer requisições HTTP.
        public VeiculoEletricoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: VeiculoEletrico - Busca a lista de veículos elétricos da API e envia para a view.
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("APIClient");

            var response = await client.GetAsync("VeiculoEletricoApi");
            if (response.IsSuccessStatusCode)
            {
                // Lê a resposta e envia os veículos para a view.
                var veiculos = await response.Content.ReadAsAsync<IEnumerable<VeiculoEletricoViewModel>>();
                return View(veiculos);
            }
            else
            {
                // Em caso de erro, retorna uma lista vazia para evitar falhas na view.
                return View(new List<VeiculoEletricoViewModel>());
            }
        }

        // GET: VeiculoEletrico/Create - Renderiza o formulário para criar um novo veículo elétrico.
        public IActionResult Create()
        {
            return View();
        }

        // POST: VeiculoEletrico/Create - Processa o formulário e cria um novo veículo elétrico.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VeiculoEletricoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("APIClient");
                var response = await client.PostAsJsonAsync("VeiculoEletricoApi", model);

                if (response.IsSuccessStatusCode)
                {
                    // Redireciona para a lista se a criação for bem-sucedida.
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Em caso de erro, adiciona uma mensagem para ser exibida na view.
                    ModelState.AddModelError(string.Empty, "Erro ao adicionar veículo. Tente novamente.");
                }
            }
            return View(model);
        }

        // GET: VeiculoEletrico/Edit/5 - Carrega os dados de um veículo elétrico específico para edição.
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"VeiculoEletricoApi/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Retorna os dados do veículo para a view de edição.
                var veiculo = await response.Content.ReadAsAsync<VeiculoEletricoViewModel>();
                return View(veiculo);
            }
            else
            {
                // Retorna "NotFound" se o veículo não for encontrado.
                return NotFound();
            }
        }

        // POST: VeiculoEletrico/Edit/5 - Processa a edição dos dados de um veículo elétrico.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VeiculoEletricoViewModel model)
        {
            // Verifica se os IDs são válidos
            if (id <= 0 || model.ID_Usuario <= 0)
            {
                ModelState.AddModelError(string.Empty, "ID do veículo ou do usuário inválido.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("APIClient");

                try
                {
                    // Serializa os dados do veículo e envia uma requisição PUT para a API.
                    var requestUri = $"VeiculoEletricoApi/{id}";
                    var jsonContent = new StringContent(JsonSerializer.Serialize(new
                    {
                        ID_Usuario = model.ID_Usuario,
                        Modelo = model.Modelo,
                        Marca = model.Marca,
                        Ano = model.Ano
                    }), System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(requestUri, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Caso ocorra erro, adiciona uma mensagem ao ModelState.
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"Erro ao editar veículo: {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    // Loga qualquer exceção que ocorra durante a requisição.
                    ModelState.AddModelError(string.Empty, $"Ocorreu um erro ao tentar editar o veículo: {ex.Message}");
                }
            }
            return View(model);
        }

        // GET: VeiculoEletrico/Delete/5 - Deleta um veículo elétrico específico.
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("APIClient");
            var response = await client.DeleteAsync($"VeiculoEletricoApi/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Redireciona para a lista de veículos se a exclusão for bem-sucedida.
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Retorna "BadRequest" em caso de falha na exclusão.
                return BadRequest();
            }
        }
    }
}
