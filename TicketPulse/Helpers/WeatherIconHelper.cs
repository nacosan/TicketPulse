namespace TicketPulse.Helpers
{
    public static class WeatherIconHelper
    {
        public static string GetWeatherIcon(string estado)
        {
            if (string.IsNullOrEmpty(estado)) return "https://cdn-icons-png.flaticon.com/512/1163/1163661.png";
            estado = estado.ToLower();
            if (estado.Contains("despejado") || estado.Contains("soleado"))
                return "https://cdn-icons-png.flaticon.com/512/869/869869.png";
            if (estado.Contains("nubes") || estado.Contains("nublado"))
                return "https://cdn-icons-png.flaticon.com/512/414/414825.png";
            if (estado.Contains("lluvia"))
                return "https://cdn-icons-png.flaticon.com/512/414/414974.png";
            if (estado.Contains("tormenta"))
                return "https://cdn-icons-png.flaticon.com/512/1146/1146869.png";
            if (estado.Contains("niebla"))
                return "https://cdn-icons-png.flaticon.com/512/4005/4005901.png";
            return "https://cdn-icons-png.flaticon.com/512/1163/1163661.png";
        }
    }
}
