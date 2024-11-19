using Microsoft.AspNetCore.Mvc;

namespace ElectriXDriveUI.Controllers
{
    public class HomeController : Controller
    {
        // Este método 'Index' retorna a view principal da página inicial.
        // Basicamente, ele é responsável por carregar o conteúdo da página "Home".
        public IActionResult Index()
        {
            return View();
        }
    }
}
