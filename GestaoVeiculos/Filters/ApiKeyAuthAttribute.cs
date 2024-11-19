using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;

namespace GestaoVeiculos.Filters
{
    /// <summary>
    /// Atributo de autorização para validar a API Key nas requisições.
    /// </summary>
    /// <remarks>
    /// Este filtro de autorização verifica se a requisição contém uma API Key válida no cabeçalho.
    /// Caso a chave seja inválida ou esteja ausente, o acesso será negado.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";

        /// <summary>
        /// Método de autorização para verificar a validade da API Key fornecida na requisição.
        /// </summary>
        /// <param name="context">Contexto do filtro de autorização que contém informações sobre a requisição.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Obtém a configuração do serviço de injeção de dependência
            var configuration = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));

            // Obtém a API Key armazenada na configuração
            var apiKey = configuration.GetValue<string>("ApiKey");

            // Verifica se o cabeçalho da requisição contém a API Key esperada e se ela é válida
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey) || apiKey != potentialApiKey)
            {
                // Se a API Key não for válida ou estiver ausente, retorna uma resposta de não autorizado (401)
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
