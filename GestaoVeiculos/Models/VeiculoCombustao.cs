using GestaoVeiculos.Models;

namespace GestaoVeiculos.Models
{
    /// <summary>
    /// Representa um veículo de combustão no sistema de gestão de veículos.
    /// </summary>
    public class VeiculoCombustao
    {
        /// <summary>
        /// Identificador único do veículo de combustão.
        /// </summary>
        public int ID_Veiculo_Combustao { get; set; }

        /// <summary>
        /// Identificador do usuário proprietário do veículo de combustão.
        /// </summary>
        public int ID_Usuario { get; set; }

        /// <summary>
        /// Modelo do veículo de combustão.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Marca do veículo de combustão.
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Ano de fabricação do veículo de combustão.
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// Quilometragem mensal percorrida pelo veículo de combustão (em km).
        /// </summary>
        public double Quilometragem_Mensal { get; set; }

        /// <summary>
        /// Consumo médio de combustível do veículo de combustão (em km/l).
        /// </summary>
        public double Consumo_Medio { get; set; }

        /// <summary>
        /// Capacidade total do tanque de combustível do veículo de combustão (em litros).
        /// </summary>
        public double Autonomia_Tanque { get; set; }

        /// <summary>
        /// Tipo de combustível utilizado pelo veículo de combustão (por exemplo, gasolina, diesel).
        /// </summary>
        public string Tipo_Combustivel { get; set; }

        /// <summary>
        /// Propriedade de navegação para o relacionamento com o usuário proprietário do veículo.
        /// </summary>
        /// <remarks>
        /// Essa propriedade estabelece a relação entre o veículo de combustão e seu proprietário, permitindo o acesso aos detalhes do usuário.
        /// </remarks>
        public Usuario Usuario { get; set; }
    }
}
