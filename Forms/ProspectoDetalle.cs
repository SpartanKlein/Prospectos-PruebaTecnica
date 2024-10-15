using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestorProstectos.Forms
{
    public partial class ProspectoDetalle : Form
    {

        private DatabaseOperations dbOps;
        private string tipoUsuario;
        private int prospectoId;

        public ProspectoDetalle(int prospectoId, string tipoUsuario)
        {
            InitializeComponent();
            this.tipoUsuario = tipoUsuario;
            this.prospectoId = prospectoId;
            dbOps = new DatabaseOperations();

            this.Shown += FormDetalleProspecto_Load;
        }

        private void FormDetalleProspecto_Load(object sender, EventArgs e)
        {
            CargarDatosProspecto();
            CargarDocumentosProspecto();
            ConfigurarInterfaz();
        }

        private void ConfigurarInterfaz()
        {
            if (tipoUsuario == "Promotor")
            {
                btnAutorizar.Visible = false;
                btnRechazar.Visible = false;
                // Deshabilitar edición de campos si es necesario
                txtNombre.ReadOnly = true;
                txtPrimerApellido.ReadOnly = true;
                txtSegundoApellido.ReadOnly = true;
                txtRFC.ReadOnly = true;
                txtTelefono.ReadOnly = true;
                txtCalle.ReadOnly = true;
                txtColonia.ReadOnly = true;
                txtNumero.ReadOnly = true;
                txtCodigoPostal.ReadOnly = true;
                txtObservaciones.ReadOnly = true;

                // ... (hacer lo mismo para los demás campos)
            }
        }
        private void CargarDatosProspecto()
        {
            DataTable dt = dbOps.ObtenerProspecto(this.prospectoId);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtNombre.Text = row["Nombre"].ToString();
                txtPrimerApellido.Text = row["PrimerApellido"].ToString();
                txtSegundoApellido.Text = row["SegundoApellido"].ToString();
                txtCalle.Text = row["Calle"].ToString();
                txtNumero.Text = row["Numero"].ToString();
                txtColonia.Text = row["Colonia"].ToString();
                txtCodigoPostal.Text = row["CodigoPostal"].ToString();
                txtTelefono.Text = row["Telefono"].ToString();
                txtRFC.Text = row["RFC"].ToString();
                lblEstatus.Text = row["Estatus"].ToString();
            }
        }

        private void CargarDocumentosProspecto()
        {
            DataTable dt = dbOps.ObtenerDocumentosProspecto(prospectoId);
            /*lstDocumentos.DataSource = dt;
            lstDocumentos.DisplayMember = "NombreDocumento";
            lstDocumentos.ValueMember = "DocumentoID";*/
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea autorizar este prospecto?", "Confirmar autorización",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dbOps.ActualizarEstatusProspecto(prospectoId, "Autorizado", txtObservaciones.Text);
                MessageBox.Show("El prospecto ha sido autorizado.", "Autorización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
               
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea rechazar este prospecto?", "Confirmar autorización",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dbOps.ActualizarEstatusProspecto(prospectoId, "Rechazado", txtObservaciones.Text);
                MessageBox.Show("El prospecto ha sido rechazado.", "Rechazo registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }


}

