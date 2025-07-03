namespace TicketPulse.Models
{
    public class SearchFilters
    {
        public string Categoria { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public DateTime? FechaMin { get; set; }
        public DateTime? FechaMax { get; set; }
        public string Pais { get; set; }
        public string Provincia { get; set; }
    }

}
