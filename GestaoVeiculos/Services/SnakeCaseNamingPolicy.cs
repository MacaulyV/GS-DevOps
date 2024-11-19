using System.Text.Json;

namespace GestaoVeiculos.Services
{
    /// <summary>
    /// Classe para definir uma política de nomenclatura que converte nomes em snake_case para PascalCase.
    /// </summary>
    /// <remarks>
    /// Essa política é útil quando precisamos manipular nomes de propriedades de entrada/saída para que sigam uma convenção específica
    /// (por exemplo, ao integrar com APIs externas que utilizam snake_case).
    /// </remarks>
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        /// <summary>
        /// Converte o nome de snake_case para PascalCase.
        /// </summary>
        /// <param name="name">Nome original em snake_case.</param>
        /// <returns>Nome convertido para PascalCase.</returns>
        public override string ConvertName(string name)
        {
            // Converte snake_case para PascalCase
            return ToPascalCase(name);
        }

        /// <summary>
        /// Método auxiliar que converte um nome de snake_case para PascalCase.
        /// </summary>
        /// <param name="name">Nome original em snake_case.</param>
        /// <returns>Nome convertido para PascalCase.</returns>
        private string ToPascalCase(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            // Divide o nome em partes pelo caractere "_"
            var parts = name.Split('_');
            for (int i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                if (part.Length > 0)
                {
                    // Converte o primeiro caractere de cada parte para maiúsculo e junta o restante da string
                    parts[i] = char.ToUpperInvariant(part[0]) + part.Substring(1);
                }
            }

            // Junta todas as partes em uma única string no formato PascalCase
            return string.Join(string.Empty, parts);
        }
    }
}
