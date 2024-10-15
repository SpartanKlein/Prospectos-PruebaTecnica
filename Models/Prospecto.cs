using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GestorProstectos.Models
{
    public class Prospecto
    {
        public int ProspectoID { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string RFC { get; set; }
        public string Estatus { get; set; }
        public string ObservacionesRechazo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int PromotorID { get; set; }

        // Propiedades de navegación
        public virtual Promotor Promotor { get; set; }
        public virtual ICollection<Documento> Documentos { get; set; }

        public Prospecto()
        {
            Documentos = new HashSet<Documento>();
            FechaCreacion = DateTime.Now;
            Estatus = "Enviado"; // Estado inicial al crear un prospecto
        }

        // Métodos auxiliares
        public string NombreCompleto()
        {
            return $"{Nombre} {PrimerApellido} {SegundoApellido}".Trim();
        }

        public string DireccionCompleta()
        {
            return $"{Calle} {Numero}, {Colonia}, CP {CodigoPostal}";
        }

        public bool EsAutorizado()
        {
            return Estatus.Equals("Autorizado", StringComparison.OrdinalIgnoreCase);
        }

        public bool EsRechazado()
        {
            return Estatus.Equals("Rechazado", StringComparison.OrdinalIgnoreCase);
        }

        public void Autorizar()
        {
            Estatus = "Autorizado";
            ObservacionesRechazo = null;
        }

        public void Rechazar(string observaciones)
        {
            Estatus = "Rechazado";
            ObservacionesRechazo = observaciones;
        }
    }

    public class Documento
    {
        public int DocumentoID { get; set; }
        public int ProspectoID { get; set; }
        public string NombreDocumento { get; set; }
        public string RutaArchivo { get; set; }
        public string NombreOriginalArchivo { get; set; }
        public string TipoArchivo { get; set; }
        public long TamanoArchivo { get; set; }
        public DateTime FechaCarga { get; set; }

        public virtual Prospecto Prospecto { get; set; }
    }

    public class Promotor
    {
        public int PromotorID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string TipoUsuario { get; set; }


        public virtual ICollection<Prospecto> Prospectos { get; set; }

        public Promotor()
        {
            Prospectos = new HashSet<Prospecto>();
        }
    }
}
