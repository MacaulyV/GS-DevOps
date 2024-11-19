using System.Text.Json.Serialization;

namespace ElectriXDriveUI.ViewModels
{
    public class ResultadoComparacaoViewModel
    {
        [JsonPropertyName("veiculoCombustao")]
        public VeiculoCombustaoResultado VeiculoCombustao { get; set; }

        [JsonPropertyName("veiculoEletrico")]
        public VeiculoEletricoResultado VeiculoEletrico { get; set; }

        [JsonPropertyName("analiseDetalhada")]
        public string AnaliseDetalhada { get; set; }

        [JsonPropertyName("conclusao")]
        public string Conclusao { get; set; }
    }

    public class VeiculoCombustaoResultado
    {
        public string Modelo { get; set; }
        public double QuilometragemMensal { get; set; }
        public double ConsumoMedio { get; set; }
        public double LitrosNecessarios { get; set; }
        public double TanquesNecessarios { get; set; }
    }

    public class VeiculoEletricoResultado
    {
        public string Modelo { get; set; }
        public double QuilometragemMensal { get; set; }
        public double Autonomia { get; set; }
        public double CargasNecessarias { get; set; }
    }
}
