using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GestaoVeiculos.Middleware
{
    /// <summary>
    /// Middleware para tratamento global de exceções, capturando erros não tratados e retornando uma resposta padronizada.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Construtor do ExceptionMiddleware.
        /// </summary>
        /// <param name="next">Delegado que representa o próximo middleware na pipeline de requisições.</param>
        /// <param name="logger">Instância de logger para registrar as exceções ocorridas.</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoca o middleware para tratar exceções durante o processamento da requisição.
        /// </summary>
        /// <param name="httpContext">Contexto HTTP atual que contém informações sobre a requisição e a resposta.</param>
        /// <returns>Tarefa assíncrona que representa a execução do middleware.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Passa a requisição para o próximo middleware
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Loga a exceção e manipula a resposta de erro
                _logger.LogError(ex, "Ocorreu uma exceção não tratada.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Método auxiliar para lidar com a exceção e criar uma resposta padronizada.
        /// </summary>
        /// <param name="context">Contexto HTTP que está sendo tratado.</param>
        /// <param name="exception">A exceção que foi lançada durante o processamento da requisição.</param>
        /// <returns>Tarefa assíncrona que escreve a resposta de erro no contexto da resposta HTTP.</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Define o tipo de conteúdo e o status da resposta
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Cria o objeto de resposta para o erro
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ocorreu um erro interno no servidor.",
                Detailed = exception.Message // Nota: Remover detalhes em ambientes de produção para segurança
            };

            // Serializa o objeto de resposta para JSON
            var responseJson = JsonSerializer.Serialize(response);

            // Escreve a resposta JSON no corpo da resposta HTTP
            return context.Response.WriteAsync(responseJson);
        }
    }
}
