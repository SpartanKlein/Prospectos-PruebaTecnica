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
    public partial class Login : Form
    {

        private DatabaseOperations dbOps;

        public Login()
        {
            InitializeComponent();
            dbOps = new DatabaseOperations();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUser.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, ingrese su usuario y contraseña.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int promotorId = AutenticarUsuario(usuario, password);
            if (promotorId > 0)
            {
                string tipoUsuario = dbOps.ObtenerTipoUsuario(promotorId);
                ListadoProspecto formListado = new ListadoProspecto(promotorId, tipoUsuario);
                formListado.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int AutenticarUsuario(string usuario, string password)
        {
            return dbOps.VerificarCredenciales(usuario, password);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
