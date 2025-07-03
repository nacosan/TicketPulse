namespace TicketPulse.Models
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }

        public int IdUsuario { get; set; }
        public int IdConcierto { get; set; }

        public int Cantidad { get; set; }


        public Conciertos Concierto { get; set; }
        public Usuarios Usuario { get; set; }

        public DateTime FechaAsistencia { get; set; }
        public DateTime FechaCompra { get; set; }



    }
}
