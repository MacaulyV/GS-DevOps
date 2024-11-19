using System.ComponentModel.DataAnnotations;

namespace ElectriXDriveUI.ViewModels
{
    public class VeiculoCombustaoViewModel
    {
        // ID do veículo de combustão - identificador único do veículo.
        public int ID_Veiculo_Combustao { get; set; }

        // ID do usuário - identificador do proprietário do veículo.
        // Campo obrigatório, se não for informado, um erro será exibido.
        [Required(ErrorMessage = "ID do usuário é obrigatório.")]
        public int ID_Usuario { get; set; }

        // Modelo do veículo (ex.: Fiesta, Corolla, etc.).
        // Campo obrigatório, se não for informado, um erro será exibido.
        [Required(ErrorMessage = "Modelo é obrigatório.")]
        public string Modelo { get; set; }

        // Marca do veículo (ex.: Ford, Toyota, etc.).
        // Campo obrigatório, se não for informado, um erro será exibido.
        [Required(ErrorMessage = "Marca é obrigatória.")]
        public string Marca { get; set; }

        // Ano de fabricação do veículo.
        // Campo obrigatório, se não for informado, um erro será exibido.
        [Required(ErrorMessage = "Ano é obrigatório.")]
        public int Ano { get; set; }

        // Quilometragem mensal do veículo.
        // Campo obrigatório, se não for informado, um erro será exibido.
        [Required(ErrorMessage = "Quilometragem Mensal é obrigatória.")]
        public int Quilometragem_Mensal { get; set; }

        // Consumo médio do veículo em quilômetros por litro.
        public double Consumo_Medio { get; set; }

        // Autonomia do tanque em quilômetros.
        public int Autonomia_Tanque { get; set; }

        // Tipo de combustível utilizado pelo veículo (ex.: Gasolina, Diesel).
        public string Tipo_Combustivel { get; set; }
    }
}
