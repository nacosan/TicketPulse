using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketPulse.Models;

namespace TicketPulse.Controllers
{
    public class HomeController : Controller
    {
        private readonly GestionRepositorios listaConciertos;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, GestionRepositorios listaConciertos)
        {
            _logger = logger;
            this.listaConciertos = listaConciertos;
        }

        public IActionResult Index()
        {
            var conciertos = listaConciertos.ObtenerConciertos() ?? new List<Conciertos>();
            var categorias = listaConciertos.ObtenerCategoriasConciertos() ?? new List<string>();

            ViewBag.Categorias = categorias;
            return View(conciertos);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
