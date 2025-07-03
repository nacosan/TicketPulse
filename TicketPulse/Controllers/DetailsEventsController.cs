using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketPulse.Models;

namespace TicketPulse.Controllers
{
    public class DetailsEventsController : Controller
    {
        private static readonly Dictionary<string, string> CodigosProvincias = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    { "Álava", "01" }, { "Albacete", "02" }, { "Alicante", "03" }, { "Almería", "04" }, { "Ávila", "05" },
    { "Badajoz", "06" }, { "Islas Baleares", "07" }, { "Barcelona", "08" }, { "Burgos", "09" },
    { "Cáceres", "10" }, { "Cádiz", "11" }, { "Castellón", "12" }, { "Ciudad Real", "13" }, { "Córdoba", "14" },
    { "La Coruña", "15" }, { "Cuenca", "16" }, { "Gerona", "17" }, { "Granada", "18" }, { "Guadalajara", "19" },
    { "Guipúzcoa", "20" }, { "Huelva", "21" }, { "Huesca", "22" }, { "Jaén", "23" }, { "León", "24" },
    { "Lérida", "25" }, { "Lugo", "27" }, { "Madrid", "28" }, { "Málaga", "29" }, { "Murcia", "30" },
    { "Navarra", "31" }, { "Orense", "32" }, { "Asturias", "33" }, { "Palencia", "34" }, { "Las Palmas", "35" },
    { "Pontevedra", "36" }, { "Salamanca", "37" }, { "Santa Cruz de Tenerife", "38" }, { "Cantabria", "39" },
    { "Segovia", "40" }, { "Sevilla", "41" }, { "Soria", "42" }, { "Tarragona", "43" }, { "Teruel", "44" },
    { "Toledo", "45" }, { "Valencia", "46" }, { "Valladolid", "47" }, { "Vizcaya", "48" }, { "Zamora", "49" },
    { "Zaragoza", "50" }, { "Ceuta", "51" }, { "Melilla", "52" }
};

        private readonly GestionRepositorios listaConciertos;

        public DetailsEventsController(GestionRepositorios listaConciertos)
        {
            this.listaConciertos = listaConciertos;
        }

        // GET: DetailsEventsController
        public IActionResult Index()
        {
            var conciertos = listaConciertos.ObtenerConciertos();
            if (conciertos == null)
                conciertos = new List<Conciertos>();
            return View(conciertos);
        }
        // GET: DetailsEventsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            // Primero: Obtener el concierto
            var concierto = listaConciertos.ObtenerConciertos(id);
            if (concierto == null)
            {
                return NotFound();
            }

            // Segundo: Obtener el código de provincia
            string codigoProvincia = null;
            CodigosProvincias.TryGetValue(concierto.Provincia, out codigoProvincia);

            if (string.IsNullOrEmpty(codigoProvincia))
            {
                ViewBag.EstadoCieloHoy = "No disponible";
                ViewBag.TemperaturaMaxHoy = "-";
                ViewBag.TemperaturaMinHoy = "-";
          
                return View(concierto);
            }

            // Tercero: Consultar la API del tiempo
            dynamic weatherData = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"https://www.el-tiempo.net/api/json/v2/provincias/{codigoProvincia}";
                    var response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        weatherData = JsonConvert.DeserializeObject<dynamic>(json);
                    }
                }
            }
            catch
            {
                weatherData = null;
            }

            // Cuarto: Procesar datos meteorológicos
            if (weatherData != null)
            {
                // Busca la ciudad capital
                var ciudad = ((IEnumerable<dynamic>)weatherData.ciudades)
                    .FirstOrDefault(c =>
                        c.name.ToString().Equals(concierto.Provincia, StringComparison.OrdinalIgnoreCase) ||
                        c.name.ToString().Equals(weatherData.provincia?.CAPITAL_PROVINCIA?.ToString(), StringComparison.OrdinalIgnoreCase)
                    );

                // Si no encuentra la capital, toma la primera ciudad de la provincia
                if (ciudad == null)
                    ciudad = ((IEnumerable<dynamic>)weatherData.ciudades).FirstOrDefault();

                // Para HOY (datos de la ciudad)
                ViewBag.EstadoCieloHoy = ciudad?.stateSky?.description?.ToString();
                ViewBag.TemperaturaMaxHoy = ciudad?.temperatures?.max?.ToString();
                ViewBag.TemperaturaMinHoy = ciudad?.temperatures?.min?.ToString();

           
            }

            else
            {
                ViewBag.EstadoCieloHoy = "Error API";
                ViewBag.TemperaturaMaxHoy = "-";
                ViewBag.TemperaturaMinHoy = "-";
                ViewBag.EstadoCieloMañana = "Error API";
                ViewBag.TemperaturaMaxMañana = "-";
                ViewBag.TemperaturaMinMañana = "-";
            }

            return View(concierto);
        }


        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try { return RedirectToAction(nameof(Index)); }
            catch { return View(); }
        }

        public ActionResult Edit(int id) => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try { return RedirectToAction(nameof(Index)); }
            catch { return View(); }
        }

        public ActionResult Delete(int id) => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try { return RedirectToAction(nameof(Index)); }
            catch { return View(); }
        }
    }
}
