namespace GestaoVeiculos.Models
{
    /// <summary>
    /// Representa o modelo de um veículo, podendo ser de combustão ou elétrico.
    /// </summary>
    public class VeiculoModelo
    {
        /// <summary>
        /// Tipo do veículo, podendo ser "combustao" ou "eletrico".
        /// </summary>
        /// <remarks>
        /// Determina se o veículo é de combustão ou elétrico, afetando as demais propriedades.
        /// </remarks>
        public string Tipo { get; set; } // "combustao" ou "eletrico"

        /// <summary>
        /// Modelo do veículo.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Marca do veículo.
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Ano de fabricação do veículo.
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// Consumo médio do veículo.
        /// </summary>
        /// <remarks>
        /// Para veículos de combustão, o consumo é medido em quilômetros por litro (km/l).
        /// Para veículos elétricos, é medido em quilowatt-hora por quilômetro (kWh/km).
        /// </remarks>
        public double Consumo_Medio { get; set; }

        /// <summary>
        /// Capacidade total do tanque de combustível do veículo de combustão (em litros).
        /// </summary>
        /// <remarks>
        /// Esta propriedade é aplicável apenas para veículos de combustão.
        /// </remarks>
        public double? Autonomia_Tanque { get; set; } // Apenas para combustão

        /// <summary>
        /// Autonomia do veículo elétrico, ou seja, a distância que pode percorrer com uma carga completa (em quilômetros).
        /// </summary>
        /// <remarks>
        /// Esta propriedade é aplicável apenas para veículos elétricos.
        /// </remarks>
        public double? Autonomia { get; set; } // Apenas para elétrico
    }
}
