using System.Data;
using System.Configuration;
using System.Data.SqlClient;


public class DatabaseOperations
    {
        private string connectionString;

        public DatabaseOperations()
        {
            connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        }

        private int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        private DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                    return dt;
                }
            }
        }
    // Crear un nuevo prospecto
    public int CrearProspecto(string nombre, string primerApellido, string segundoApellido,
                              string calle, string numero, string colonia, string codigoPostal,
                              string telefono, string rfc, int promotorId)
    {
        string query = @"INSERT INTO Prospectos (Nombre, PrimerApellido, SegundoApellido, Calle, 
                         Numero, Colonia, CodigoPostal, Telefono, RFC, Estatus, PromotorID) 
                         VALUES (@Nombre, @PrimerApellido, @SegundoApellido, @Calle, @Numero, 
                         @Colonia, @CodigoPostal, @Telefono, @RFC, 'Enviado', @PromotorID); 
                         SELECT SCOPE_IDENTITY();";

        SqlParameter[] parameters = {
            new SqlParameter("@Nombre", nombre),
            new SqlParameter("@PrimerApellido", primerApellido),
            new SqlParameter("@SegundoApellido", segundoApellido ?? (object)DBNull.Value),
            new SqlParameter("@Calle", calle),
            new SqlParameter("@Numero", numero),
            new SqlParameter("@Colonia", colonia),
            new SqlParameter("@CodigoPostal", codigoPostal),
            new SqlParameter("@Telefono", telefono),
            new SqlParameter("@RFC", rfc),
            new SqlParameter("@PromotorID", promotorId)
        };

        return Convert.ToInt32(ExecuteQuery(query, parameters).Rows[0][0]);
    }

    // Obtener un prospecto por ID

    public DataTable ObtenerProspectos()
    {
        string query = "SELECT ProspectoID, Nombre, PrimerApellido, SegundoApellido, Estatus FROM Prospectos";
        return ExecuteQuery(query);
    }

    public DataTable ObtenerProspectosPorPromotor(int promotorId)
    {
        string query = "SELECT ProspectoID, Nombre, PrimerApellido, SegundoApellido, Estatus FROM Prospectos WHERE PromotorID = @PromotorID";
        SqlParameter[] parameters = { new SqlParameter("@PromotorID", promotorId) };
        return ExecuteQuery(query, parameters);
    }

    public DataTable ObtenerProspecto(int prospectoId)
    {
        string query = "SELECT * FROM Prospectos WHERE ProspectoID = @ProspectoID";
        SqlParameter[] parameters = { new SqlParameter("@ProspectoID", prospectoId) };
        return ExecuteQuery(query, parameters);
    }

    // Actualizar el estatus de un prospecto
    public int ActualizarEstatusProspecto(int prospectoId, string nuevoEstatus, string observaciones = null)
    {
        string query = @"UPDATE Prospectos SET Estatus = @Estatus, 
                         ObservacionesRechazo = @Observaciones 
                         WHERE ProspectoID = @ProspectoID";

        SqlParameter[] parameters = {
            new SqlParameter("@ProspectoID", prospectoId),
            new SqlParameter("@Estatus", nuevoEstatus),
            new SqlParameter("@Observaciones", observaciones ?? (object)DBNull.Value)
        };

        return ExecuteNonQuery(query, parameters);
    }

    // Eliminar un prospecto
    public int EliminarProspecto(int prospectoId)
    {
        string query = "DELETE FROM Prospectos WHERE ProspectoID = @ProspectoID";
        SqlParameter[] parameters = { new SqlParameter("@ProspectoID", prospectoId) };
        return ExecuteNonQuery(query, parameters);
    }

    // Agregar un documento
    public int AgregarDocumento(int prospectoId, string nombreDocumento, string rutaArchivo,
                                string nombreOriginalArchivo, long tamanoArchivo)
    {
        string query = @"INSERT INTO Documentos (ProspectoID, NombreDocumento, RutaArchivo, 
                         NombreOriginalArchivo, TipoArchivo, TamanoArchivo) 
                         VALUES (@ProspectoID, @NombreDocumento, @RutaArchivo, 
                         @NombreOriginalArchivo, 'PDF', @TamanoArchivo); 
                         SELECT SCOPE_IDENTITY();";

        SqlParameter[] parameters = {
            new SqlParameter("@ProspectoID", prospectoId),
            new SqlParameter("@NombreDocumento", nombreDocumento),
            new SqlParameter("@RutaArchivo", rutaArchivo),
            new SqlParameter("@NombreOriginalArchivo", nombreOriginalArchivo),
            new SqlParameter("@TamanoArchivo", tamanoArchivo)
        };

        return Convert.ToInt32(ExecuteQuery(query, parameters).Rows[0][0]);
    }

    // Obtener documentos de un prospecto
    public DataTable ObtenerDocumentosProspecto(int prospectoId)
    {
        string query = "SELECT * FROM Documentos WHERE ProspectoID = @ProspectoID";
        SqlParameter[] parameters = { new SqlParameter("@ProspectoID", prospectoId) };
        return ExecuteQuery(query, parameters);
    }


    public int VerificarCredenciales(string email, string password)
    {
        string query = "SELECT PromotorID FROM Promotores WHERE Usuario = @User AND Contrasena = @Password";
        SqlParameter[] parameters = {
        new SqlParameter("@User", email),
        new SqlParameter("@Password", password) // Nota: En una aplicación real, deberías usar hash para las contraseñas
    };
        DataTable dt = ExecuteQuery(query, parameters);
        return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["PromotorID"]) : -1;
    }

    public string ObtenerTipoUsuario(int promotorId)
    {
        string query = "SELECT TipoUsuario FROM Promotores WHERE PromotorID = @PromotorID";
        SqlParameter[] parameters = { new SqlParameter("@PromotorID", promotorId) };
        DataTable dt = ExecuteQuery(query, parameters);
        return dt.Rows.Count > 0 ? dt.Rows[0]["TipoUsuario"].ToString() : null;
    }
}
