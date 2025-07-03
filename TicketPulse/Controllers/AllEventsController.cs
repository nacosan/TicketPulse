using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketPulse.Models;

namespace TicketPulse.Controllers
{
    public class AllEventsController : Controller
    {
        GestionRepositorios listaConciertos;
        public AllEventsController(GestionRepositorios listaConciertos)
        {
            this.listaConciertos = listaConciertos;
        }
        // GET: AllEventsController
        public IActionResult Index()
        {
            var conciertos = listaConciertos.ObtenerConciertos();
            if (conciertos == null)
                conciertos = new List<Conciertos>();
            return View(conciertos);
        }



        // GET: AllEventsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AllEventsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllEventsController/Create
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

        // GET: AllEventsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AllEventsController/Edit/5
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

        // GET: AllEventsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AllEventsController/Delete/5
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
