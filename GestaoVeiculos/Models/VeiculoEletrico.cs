using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GestaoVeiculos.Models
{
    /// <summary>
    /// Representa um veículo elétrico no sistema de gestão de veículos.
    /// </summary>
    public class VeiculoEletrico
    {
        /// <summary>
        /// Identificador único do veículo elétrico.
        /// </summary>
        public int ID_Veiculo_Eletrico { get; set; }

        /// <summary>
        /// Identificador do usuário proprietário do veículo elétrico.
        /// </summary>
        public int ID_Usuario { get; set; }

        /// <summary>
        /// Modelo do veículo elétrico.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Marca do veículo elétrico.
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Ano de fabricação do veículo elétrico.
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// Consumo médio de energia do veículo elétrico (em kWh por km).
        /// </summary>
        public double Consumo_Medio { get; set; }

        /// <summary>
        /// Autonomia do veículo elétrico, ou seja, a distância que ele pode percorrer com uma carga completa (em quilômetros).
        /// </summary>
        public int Autonomia { get; set; }

        /// <summary>
        /// Propriedade de navegação para o relacionamento com o usuário proprietário do veículo.
        /// </summary>
        /// <remarks>
        /// Esta propriedade é ignorada durante a serialização JSON para evitar ciclos de referência.
        /// </remarks>
        [JsonIgnore]
        public Usuario Usuario { get; set; }
    }
}
