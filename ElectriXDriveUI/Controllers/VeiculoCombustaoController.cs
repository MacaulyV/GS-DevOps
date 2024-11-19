using ElectriXDriveUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ElectriXDriveUI.Controllers
{
    public class VeiculoCombustaoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<VeiculoCombustaoController> _logger;

        // Construtor que injeta a dependência de IHttpClientFactory e ILogger para fazer requisições HTTP e logar eventos importantes.
        public VeiculoCombustaoController(IHttpClientFactory httpClientFactory, ILogger<VeiculoCombustaoController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: VeiculoCombustao - Carrega a lista de veículos de combustão a partir da API.
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("APIClient");

            try
            {
                var response = await client.GetAsync("VeiculoCombustaoApi");
                if (response.IsSuccessStatusCode)
                {
                    // Se a resposta for bem-sucedida, lê os veículos e envia para a view.
                    var veiculos = await response.Content.ReadAsAsync<IEnumerable<VeiculoCombustaoViewModel>>();
                    return View(veiculos);
                }
                else
                {
                    // Loga um aviso caso a requisição não tenha sucesso.
                    _logger.LogWarning("Falha ao buscar os veículos de combustão. Código de status: {StatusCode}", response.StatusCode);
                    return View(new List<VeiculoCombustaoViewModel>());
                }
            }
            catch (HttpRequestException ex)
            {
                // Captura e loga qualquer erro que possa ocorrer durante a requisição.
                _logger.LogError(ex, "Erro ao buscar veículos de combustão da API.");
                return View(new List<VeiculoCombustaoViewModel>());
            }
        }

        // GET: VeiculoCombustao/Create - Renderiza o formulário para adicionar um novo veículo.
        public IActionResult Create()
        {
            return View();
        }

        // POST: VeiculoCombustao/Create - Processa a criação de um novo veículo.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VeiculoCombustaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("APIClient");

                try
                {
                    // Envia uma requisição POST para a API com os dados do veículo.
                    var response = await client.PostAsJsonAsync("VeiculoCombustaoApi", model);

                    if (response.IsSuccessStatusCode)
                    {
                        // Redireciona para a lista de veículos se a criação for bem-sucedida.
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Loga um erro e adiciona uma mensagem ao ModelState em caso de falha.
                        var errorContent = await response.Content.ReadAsStringAsync();
                        _logger.LogError("Erro ao adicionar veículo de combustão: {Error}", errorContent);
                        ModelState.AddModelError(string.Empty, "Erro ao adicionar veículo. Tente novamente.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError(ex, "Erro ao adicionar veículo de combustão.");
                    ModelState.AddModelError(string.Empty, $"Ocorreu um erro: {ex.Message}");
                }
            }
            return View(model);
        }

        // GET: VeiculoCombustao/Edit/5 - Busca os dados de um veículo específico para edição.
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("APIClient");
            try
            {
                var response = await client.GetAsync($"VeiculoCombustaoApi/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Se encontrado, retorna os dados do veículo para a view de edição.
                    var veiculo = await response.Content.ReadAsAsync<VeiculoCombustaoViewModel>();
                    return View(veiculo);
                }
                else
                {
                    _logger.LogWarning("Veículo de combustão não encontrado. ID: {Id}", id);
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao buscar veículo de combustão para edição. ID: {Id}", id);
                return NotFound();
            }
        }

        // POST: VeiculoCombustao/Edit/5 - Processa a edição dos dados de um veículo existente.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VeiculoCombustaoViewModel model)
        {
            _logger.LogInformation("Edit action called with ID: {Id}", id);

            // Verifica se o ID da rota corresponde ao ID do veículo no modelo
            if (id != model.ID_Veiculo_Combustao)
            {
                ModelState.AddModelError(string.Empty, "ID do veículo inválido.");
                _logger.LogWarning("ID mismatch: Route ID {RouteId}, Model ID {ModelId}", id, model.ID_Veiculo_Combustao);
                return View(model);
            }

            // Verifica se o ID do usuário é válido
            if (model.ID_Usuario <= 0)
            {
                ModelState.AddModelError(string.Empty, "ID do usuário inválido.");
                _logger.LogWarning("Invalid ID_Usuario: {ID_Usuario}", model.ID_Usuario);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("APIClient");

                try
                {
                    // Serializa os dados do veículo em JSON para enviar à API.
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var jsonContent = new StringContent(JsonSerializer.Serialize(new
                    {
                        ID_Usuario = model.ID_Usuario,
                        Modelo = model.Modelo,
                        Marca = model.Marca,
                        Ano = model.Ano,
                        Quilometragem_Mensal = model.Quilometragem_Mensal,
                        Tipo_Combustivel = model.Tipo_Combustivel
                    }, options), System.Text.Encoding.UTF8, "application/json");

                    _logger.LogInformation("Sending PUT request to API with data: {Data}", await jsonContent.ReadAsStringAsync());

                    // Envia uma requisição PUT para atualizar os dados do veículo na API.
                    var response = await client.PutAsync($"VeiculoCombustaoApi/{id}", jsonContent);

                    _logger.LogInformation("Received response from API: {StatusCode}", response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Caso haja erro, loga a resposta e adiciona a mensagem ao ModelState.
                        var errorContent = await response.Content.ReadAsStringAsync();
                        _logger.LogError("Error response from API: {Error}", errorContent);
                        ModelState.AddModelError(string.Empty, $"Erro ao editar veículo: {errorContent}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError(ex, "Erro ao editar veículo de combustão.");
                    ModelState.AddModelError(string.Empty, $"Ocorreu um erro: {ex.Message}");
                }
            }
            return View(model);
        }

        // GET: VeiculoCombustao/Delete/5 - Deleta um veículo específico.
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("APIClient");

            try
            {
                // Envia uma requisição DELETE para remover o veículo da API.
                var response = await client.DeleteAsync($"VeiculoCombustaoApi/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Caso haja erro ao deletar, loga o erro e retorna uma BadRequest.
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Erro ao excluir veículo de combustão. ID: {Id}, Error: {Error}", id, errorContent);
                    return BadRequest();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao excluir veículo de combustão. ID: {Id}", id);
                return BadRequest();
            }
        }
    }
}
