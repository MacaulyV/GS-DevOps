namespace ElectriXDriveUI.ViewModels
{
    public class LoginViewModel
    {
        // Nome do usuário - utilizado para exibir ou identificar o usuário no sistema.
        public string Nome { get; set; }

        // Email do usuário - usado para autenticação e comunicação.
        public string Email { get; set; }

        // Senha do usuário - usada para autenticar o usuário no sistema.
        public string Senha { get; set; }
    }
}
