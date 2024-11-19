using Microsoft.AspNetCore.Mvc;

namespace ElectriXDriveUI.Controllers
{
    public class ComparacaoController : Controller
    {
        // Método responsável por carregar a view principal da página de Comparação.
        public IActionResult Index()
        {
            return View();
        }
    }
}
