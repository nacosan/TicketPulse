using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketPulse.Models;

namespace TicketPulse.Controllers
{
    public class ConciertoController : Controller
    {
        GestionRepositorios listaConciertos;
        public ConciertoController(GestionRepositorios listaConciertos)
        {
            this.listaConciertos = listaConciertos;
        }
        // GET: ConciertoController
        public IActionResult Index()
        {
            var conciertos = listaConciertos.ObtenerConciertos();
            if (conciertos == null)
                conciertos = new List<Conciertos>();
            return View(conciertos);
        }



        // GET: ConciertoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ConciertoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConciertoController/Create
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

        // GET: ConciertoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ConciertoController/Edit/5
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

        // GET: ConciertoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ConciertoController/Delete/5
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
