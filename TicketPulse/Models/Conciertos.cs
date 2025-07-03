namespace TicketPulse.Models
{
    public class Conciertos
    {
        public int IdConcierto { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }

        public DateTime Fecha { get; set; }
        public string Lugar { get; set; }

        public string Provincia { get; set; }

        public string Pais { get; set; }

        public string Descripcion { get; set; }

        public double Precio { get; set; }

    }
}
