using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketPulse.Models;

namespace TicketPulse.Controllers
{
    public class AllCategoriesController : Controller
    {
        private readonly GestionRepositorios listaConciertos;

        public AllCategoriesController(GestionRepositorios listaConciertos)
        {
            this.listaConciertos = listaConciertos;
        }


        // GET: AllCategoriesController
        public ActionResult Index()
        {
            var categorias = listaConciertos.ObtenerCategoriasConciertos();
            if (categorias == null)
                categorias = new List<string>();
            return View(categorias);
        }


        // GET: AllCategoriesController/Details/5
        public ActionResult Details(string categoria)
        {
            // Obtén todos los conciertos de esa categoría
            var conciertos = listaConciertos.ObtenerConciertos()
                .Where(c => c.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                .ToList();

            ViewBag.Categoria = categoria;
            return View(conciertos);
        }

        // GET: AllCategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllCategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AllCategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AllCategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AllCategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AllCategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
