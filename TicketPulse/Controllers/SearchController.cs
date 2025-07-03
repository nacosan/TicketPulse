using Microsoft.AspNetCore.Mvc;
using TicketPulse.Models;

public class SearchController : Controller
{
    private readonly GestionRepositorios listaConciertos;

    public SearchController(GestionRepositorios listaConciertos)
    {
        this.listaConciertos = listaConciertos;
    }

    public IActionResult Index(SearchFilters filtros)
    {
        var conciertos = listaConciertos.ObtenerConciertos();

        // Filtros dinámicos
        if (!string.IsNullOrEmpty(filtros.Categoria))
            conciertos = conciertos.Where(c => c.Categoria == filtros.Categoria).ToList();
        if (filtros.PrecioMin.HasValue)
            conciertos = conciertos.Where(c => c.Precio >= (double)filtros.PrecioMin.Value).ToList();
        if (filtros.PrecioMax.HasValue)
            conciertos = conciertos.Where(c => c.Precio <= (double)filtros.PrecioMax.Value).ToList();

        if (filtros.FechaMin.HasValue)
            conciertos = conciertos.Where(c => c.Fecha >= filtros.FechaMin.Value).ToList();
        if (filtros.FechaMax.HasValue)
            conciertos = conciertos.Where(c => c.Fecha <= filtros.FechaMax.Value).ToList();
        if (!string.IsNullOrEmpty(filtros.Pais))
            conciertos = conciertos.Where(c => c.Pais == filtros.Pais).ToList();
        if (!string.IsNullOrEmpty(filtros.Provincia))
            conciertos = conciertos.Where(c => c.Provincia == filtros.Provincia).ToList();

        // Datos para los filtros desplegables
        ViewBag.Categorias = listaConciertos.ObtenerCategoriasConciertos();
        ViewBag.Paises = conciertos.Select(c => c.Pais).Distinct().ToList();
        ViewBag.Provincias = conciertos.Select(c => c.Provincia).Distinct().ToList();

        return View(conciertos);
    }



// GET: SearchController/Details/5
public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchController/Create
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

        // GET: SearchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchController/Edit/5
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

        // GET: SearchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchController/Delete/5
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

