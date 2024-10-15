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
    public partial class ListadoProspecto : Form
    {
        private DatabaseOperations dbOps;
        private string tipoUsuario;
        private int promotorId;
        public ListadoProspecto(int promotorId, string tipoUsuario)
        {
            InitializeComponent();
            dbOps = new DatabaseOperations();
            this.tipoUsuario = tipoUsuario;
            this.promotorId = promotorId;
            CargarListaProspectos(promotorId);

            dvgProspectos.CellContentClick += new DataGridViewCellEventHandler(dvgProspectos_CellContentClick);
        }

        private void CargarListaProspectos(int promotorId)
        {
            DataTable dt;
            if (this.tipoUsuario == "Supervisor")
            {
                dt = dbOps.ObtenerProspectos(); // Obtener todos los prospectos
            }
            else
            {
                dt = dbOps.ObtenerProspectosPorPromotor(promotorId); // Nuevo método para obtener solo los prospectos del promotor
            }
            dvgProspectos.DataSource = dt;

            if (!dvgProspectos.Columns.Contains("btnVerDetalle"))
            {
                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.Name = "btnVerDetalle";
                btnColumn.HeaderText = "Acciones";
                btnColumn.Text = "Ver Detalle";
                btnColumn.UseColumnTextForButtonValue = true;
                dvgProspectos.Columns.Add(btnColumn);
            }

            if (dvgProspectos.Columns.Contains("ProspectoID"))
            {
                dvgProspectos.Columns["ProspectoID"].Visible = false;
            }
        }

        private void btnNuevoProspecto_Click(object sender, EventArgs e)
        {
            using (var formCaptura = new ProspectoCaptura(promotorId))
            {
                if (formCaptura.ShowDialog() == DialogResult.OK)
                {
                    CargarListaProspectos(promotorId);
                }
            }
        }

        private void btnVerProspecto_Click(object sender, EventArgs e)
        {
            if (dvgProspectos.SelectedRows.Count > 0)
            {
                int prospectoId = Convert.ToInt32(dvgProspectos.SelectedRows[0].Cells["ProspectoID"].Value);
                using (var formDetalle = new ProspectoDetalle(prospectoId, tipoUsuario))
                {
                    formDetalle.ShowDialog();
                    CargarListaProspectos(promotorId); // Actualizar la lista después de ver/editar un prospecto
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un prospecto para ver sus detalles.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dvgProspectos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dvgProspectos.Columns["btnVerDetalle"].Index && e.RowIndex >= 0)
            {
                int prospectoId = Convert.ToInt32(dvgProspectos.Rows[e.RowIndex].Cells["ProspectoID"].Value);
                AbrirDetalleProspecto(prospectoId);
            }
        }

        private void AbrirDetalleProspecto(int prospectoId)
        {
            using (var formDetalle = new ProspectoDetalle(prospectoId, tipoUsuario))
            {
                formDetalle.ShowDialog();
                CargarListaProspectos(promotorId); // Actualizar la lista después de ver/editar un prospecto
            }
        }


        private void DvgProspectos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dvgProspectos.Cursor = Cursors.Hand;
            }
        }

        private void DvgProspectos_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dvgProspectos.Cursor = Cursors.Default;
        }

        private void Salir(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmar cierre de sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
                Login login = new Login();
                login.Show();
            }
        }
    }
}

