using Microsoft.Data.SqlClient;

namespace TicketPulse.Models
{
    public class GestionRepositorios
    {
        private readonly string _connectionString;

        public GestionRepositorios(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MiConexion");
        }

        public void InsertarUsuario(Usuarios miUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Usuarios (Nombre, Apellido, Fechnac, Email, Contr) VALUES (@Nombre, @Apellido, @Fechnac, @Email, @Contr)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", miUsuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", miUsuario.Apellido);
                    command.Parameters.AddWithValue("@Fechnac", miUsuario.Fechnac);
                    command.Parameters.AddWithValue("@Email", miUsuario.Email);
                    command.Parameters.AddWithValue("@Contr", miUsuario.Contr);


                    command.ExecuteNonQuery();
                }
            }
        }
        public bool ActualizarUsuario(int id, Usuarios usuario)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido,  Fechnac = @Fechnac, Email = @Email WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Fechnac", usuario.Fechnac);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Contr", usuario.Contr);

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
        public bool EliminarUsuario(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sqlUsuarios = "DELETE FROM Conciertos WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sqlUsuarios, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }

                string sqlUsuario = "DELETE FROM Usuarios WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sqlUsuario, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
        public Usuarios ObtenerUsuarios(int id)
        {
            Usuarios usuario = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    usuario = new Usuarios
                    {
                        IdUsuario = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Fechnac = reader.GetDateTime(3),
                        Email = reader.GetString(4),
                        Contr = reader.GetString(5),
                        Nivel = reader.IsDBNull(6) ? (bool?)null : reader.GetBoolean(6)

                    };
                }
            }
            return usuario;
        }
    
        
        public Conciertos ObtenerConciertos(int id)
        {
            Conciertos concierto = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Conciertos WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    concierto = new Conciertos
                    {
                        IdConcierto = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Categoria = reader.GetString(2),
                        Fecha = reader.GetDateTime(3),
                        Descripcion = reader.GetString(4),
                        Lugar = reader.GetString(5),
                        Precio = reader.IsDBNull(6) ? 0.0 : Convert.ToDouble(reader.GetDecimal(6)),
                        Provincia = reader.GetString(7),
                        Pais = reader.GetString(8)

                    };
                }
            }
            return concierto;
        }
        public List<Usuarios> ObtenerUsuarios()
        {
            var listaUsuarios = new List<Usuarios>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                string sql = @"SELECT * FROM Usuarios";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listaUsuarios.Add(new Usuarios
                    {
                        IdUsuario = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido= reader.GetString(2),
                        Fechnac = reader.GetDateTime(3),
                        Email = reader.GetString(4),
                        Contr = reader.GetString(5),
                        Nivel = reader.IsDBNull(6) ? (bool?)null : reader.GetBoolean(6)
                    });


                }

            }
            return listaUsuarios;
        }
        public List<Conciertos> ObtenerConciertos()
        {
            var listaConciertos = new List<Conciertos>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM Conciertos";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listaConciertos.Add(new Conciertos
                    {
                        IdConcierto = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Categoria = reader.GetString(2),
                        Fecha = reader.GetDateTime(3),
                        Descripcion = reader.GetString(4),
                        Lugar = reader.GetString(5),
                        Precio = reader.IsDBNull(6) ? 0.0 : Convert.ToDouble(reader.GetDecimal(6)),
                        Provincia = reader.GetString(7),
                        Pais = reader.GetString(8)
                    });
                }
            }
            return listaConciertos;
        }


        public void InsertarAsistencia(Asistencia miAsistencia)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO AsistenciaConciertos (IdUsuario, IdConcierto, FechaAsistencia, Cantidad, FechaCompra) VALUES (@IdUsuario, @IdConcierto, @FechaAsistencia, @Cantidad, @FechaCompra)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.Parameters.AddWithValue("@IdUsuario", miAsistencia.IdUsuario);
                    command.Parameters.AddWithValue("@IdConcierto", miAsistencia.IdConcierto);
                    command.Parameters.AddWithValue("@FechaAsistencia", miAsistencia.FechaAsistencia);
                    command.Parameters.AddWithValue("@Cantidad", miAsistencia.Cantidad);
                    command.Parameters.AddWithValue("@FechaCompra", miAsistencia.FechaCompra);



                    command.ExecuteNonQuery();
                }
            }
        }

        public List<string> ObtenerCategoriasConciertos()
        {
            var categorias = new List<string>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT DISTINCT Categoria FROM Conciertos";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categorias.Add(reader.GetString(0));
                }
            }
            return categorias;
        }

    }
}
