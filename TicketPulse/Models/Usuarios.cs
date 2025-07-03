namespace TicketPulse.Models
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public DateTime Fechnac { get; set; }
        public string Email { get; set; }

        public string Contr { get; set; }

        public bool? Nivel { get; set; }

    }
}
