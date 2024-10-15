using GestorProstectos.Models;
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
    public partial class ProspectoCaptura : Form
    {
        private DatabaseOperations dbOps;
        private int promotorId = 0;

        public ProspectoCaptura(int promotorId)
        {
            InitializeComponent();
            this.promotorId = promotorId;
            dbOps = new DatabaseOperations();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                GuardarProspecto();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidarDatos()
        {
            // Implementa la validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrimerApellido.Text) ||
                string.IsNullOrWhiteSpace(txtCalle.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtColonia.Text) ||
                string.IsNullOrWhiteSpace(txtCodigoPostal.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtRFC.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtRFC.Text, @"^[A-Z&Ñ]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z0-9]{3}$"))
            {
                MessageBox.Show("El formato del RFC no es válido.", "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtTelefono.Text, @"^\d{10}$"))
            {
                MessageBox.Show("El número de teléfono debe tener 10 dígitos.", "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtCodigoPostal.Text, @"^\d{5}$"))
            {
                MessageBox.Show("Favor de ingresar un CP válido", "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void GuardarProspecto()
        {
            int prospectoId = dbOps.CrearProspecto(
                txtNombre.Text,
                txtPrimerApellido.Text,
                txtSegundoApellido.Text,
                txtTelefono.Text,
                txtRFC.Text,
                txtCalle.Text,
                txtNumero.Text,
                txtColonia.Text,
                txtCodigoPostal.Text,
                this.promotorId// PromotorID (asumiendo un ID fijo por ahora)
            );

            MessageBox.Show($"Prospecto creado con éxito. ID: {prospectoId}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


            this.Close();
        }




        private void btnAgregarDocumento_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaDestino = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentos", Path.GetFileName(openFileDialog.FileName));
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaDestino));
                    File.Copy(openFileDialog.FileName, rutaDestino, true);

                    lstDocumentos.Items.Add(Path.GetFileName(openFileDialog.FileName));
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea salir? Se perderán los datos no guardados.",
                                "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ProspectoCaptura_Load(object sender, EventArgs e)
        {

        }

    }
}
