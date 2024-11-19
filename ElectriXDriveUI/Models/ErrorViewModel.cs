namespace ElectriXDriveUI.Models
{
    public class ErrorViewModel
    {
        // ID da requisição - usado para identificar e rastrear erros específicos.
        public string? RequestId { get; set; }

        // Propriedade que indica se o RequestId deve ser exibido.
        // Retorna verdadeiro se RequestId não for nulo ou vazio, ajudando na exibição de informações de depuração.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
