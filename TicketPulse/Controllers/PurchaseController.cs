using Microsoft.AspNetCore.Mvc;
using TicketPulse.Models;
using System.Linq;

namespace TicketPulse.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly GestionRepositorios listaConciertos;

        public PurchaseController(GestionRepositorios listaConciertos)
        {
            this.listaConciertos = listaConciertos;
        }

        // GET: Purchase/Summary
        public ActionResult Summary(int idConcierto, int cantidadEstandar = 1, int cantidadVip = 1)
        {
            // Primer paso: Obtener el concierto
            var concierto = listaConciertos.ObtenerConciertos(idConcierto);
            if (concierto == null)
            {
                return NotFound();
            }

            // Segundo paso: Obtener datos del usuario (primer usuario como ejemplo)
            var usuarios = listaConciertos.ObtenerUsuarios(1);

            // Datos del usuario (mock si no hay usuarios)
            ViewBag.FullName = usuarios?.Nombre ?? "John Doe";
            ViewBag.Email = usuarios?.Email ?? "john.doe@example.com";
            ViewBag.Fecha = usuarios.Fechnac;

            double precioEstandar = concierto.Precio;
            double precioVip = concierto.Precio * 1.5;

            ViewBag.ConciertoNombre = concierto.Nombre;
            ViewBag.ConciertoFecha = concierto.Fecha.ToString("dd/MM/yyyy");
            ViewBag.CantidadEstandar = cantidadEstandar;
            ViewBag.CantidadVip = cantidadVip;
            ViewBag.PrecioEstandar = precioEstandar;
            ViewBag.PrecioVip = precioVip;
            ViewBag.TotalEstandar = precioEstandar * cantidadEstandar;
            ViewBag.TotalVip = precioVip * cantidadVip;
            ViewBag.TotalGeneral = (precioEstandar * cantidadEstandar) + (precioVip * cantidadVip);

            return View(concierto);
        }
        // POST: PurchaseController/Confirm/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(IFormCollection collection)
        {
            try
            {
                int idUsuario = int.Parse(collection["IdUsuario"]); 
                int idConcierto = int.Parse(collection["IdConcierto"]);
                DateTime fechaAsistencia = DateTime.Parse(collection["FechaAsistencia"]); 
                int cantidad = int.Parse(collection["Cantidad"]); 
                DateTime fechaCompra = DateTime.Parse(collection["FechaCompra"]); 

                var asistencia = new Asistencia
                {
                    IdUsuario = idUsuario,
                    IdConcierto = idConcierto,
                    FechaAsistencia = fechaAsistencia,
                    Cantidad = cantidad,          
                    FechaCompra = fechaCompra    
                };

                listaConciertos.InsertarAsistencia(asistencia);

                TempData["ConciertoNombre"] = collection["ConciertoNombre"];
                TempData["ConciertoFecha"] = collection["ConciertoFecha"];
                TempData["CantidadEstandar"] = collection["cantidadEstandar"];
                TempData["CantidadVip"] = collection["cantidadVip"];
                TempData["PrecioEstandar"] = collection["PrecioEstandar"];
                TempData["PrecioVip"] = collection["PrecioVip"];
                TempData["TotalEstandar"] = collection["TotalEstandar"];
                TempData["TotalVip"] = collection["TotalVip"];
                TempData["TotalGeneral"] = collection["TotalGeneral"];
                TempData["CustomerName"] = collection["fullName"];
                TempData["CustomerEmail"] = collection["email"];

                return RedirectToAction("Confirm");
            }
            catch
            {
                TempData["ErrorMessage"] = "Ha ocurrido un error al procesar tu compra. Inténtalo de nuevo.";
                return RedirectToAction("Summary");
            }
        }

        // GET: Purchase/Confirm
        public ActionResult Confirm()
        {
            var conciertoNombreObj = TempData["ConciertoNombre"];
            ViewBag.ConciertoNombre = conciertoNombreObj is string[] arrNombre ? arrNombre.FirstOrDefault() : conciertoNombreObj?.ToString();

            var conciertoFechaObj = TempData["ConciertoFecha"];
            ViewBag.ConciertoFecha = conciertoFechaObj is string[] arrFecha ? arrFecha.FirstOrDefault() : conciertoFechaObj?.ToString();

            var cantidadEstandarObj = TempData["CantidadEstandar"];
            string cantidadEstandarStr = cantidadEstandarObj is string[] arr1 ? arr1.FirstOrDefault() : cantidadEstandarObj?.ToString();
            ViewBag.CantidadEstandar = Convert.ToInt32(string.IsNullOrEmpty(cantidadEstandarStr) ? "0" : cantidadEstandarStr);

            var cantidadVipObj = TempData["CantidadVip"];
            string cantidadVipStr = cantidadVipObj is string[] arr2 ? arr2.FirstOrDefault() : cantidadVipObj?.ToString();
            ViewBag.CantidadVip = Convert.ToInt32(string.IsNullOrEmpty(cantidadVipStr) ? "0" : cantidadVipStr);

            var precioEstandarObj = TempData["PrecioEstandar"];
            string precioEstandarStr = precioEstandarObj is string[] arr3 ? arr3.FirstOrDefault() : precioEstandarObj?.ToString();
            ViewBag.PrecioEstandar = Convert.ToDouble(string.IsNullOrEmpty(precioEstandarStr) ? "0" : precioEstandarStr);

            var precioVipObj = TempData["PrecioVip"];
            string precioVipStr = precioVipObj is string[] arr4 ? arr4.FirstOrDefault() : precioVipObj?.ToString();
            ViewBag.PrecioVip = Convert.ToDouble(string.IsNullOrEmpty(precioVipStr) ? "0" : precioVipStr);

            var totalEstandarObj = TempData["TotalEstandar"];
            string totalEstandarStr = totalEstandarObj is string[] arr5 ? arr5.FirstOrDefault() : totalEstandarObj?.ToString();
            ViewBag.TotalEstandar = Convert.ToDouble(string.IsNullOrEmpty(totalEstandarStr) ? "0" : totalEstandarStr);

            var totalVipObj = TempData["TotalVip"];
            string totalVipStr = totalVipObj is string[] arr6 ? arr6.FirstOrDefault() : totalVipObj?.ToString();
            ViewBag.TotalVip = Convert.ToDouble(string.IsNullOrEmpty(totalVipStr) ? "0" : totalVipStr);

            var totalGeneralObj = TempData["TotalGeneral"];
            string totalGeneralStr = totalGeneralObj is string[] arr7 ? arr7.FirstOrDefault() : totalGeneralObj?.ToString();
            ViewBag.TotalGeneral = Convert.ToDouble(string.IsNullOrEmpty(totalGeneralStr) ? "0" : totalGeneralStr);

            var customerNameObj = TempData["CustomerName"];
            ViewBag.CustomerName = customerNameObj is string[] arr8 ? arr8.FirstOrDefault() : customerNameObj?.ToString();

            var customerEmailObj = TempData["CustomerEmail"];
            ViewBag.CustomerEmail = customerEmailObj is string[] arr9 ? arr9.FirstOrDefault() : customerEmailObj?.ToString();

            return View();
        }

        // GET: Purchase/Success
        public ActionResult Success()
        {
            return View();
        }

    }
}
