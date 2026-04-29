using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutZone_app
{
    public partial class FormPerfil : Form
    {
        public FormPerfil()
        {
            InitializeComponent();
        }

        private void FormPerfil_Load(object sender, EventArgs e)
        {
            // Esto mete los datos de la Sesion adentro de los cuadritos
            txtUsuario.Text = Sesion.Usuario;
            txtNombre.Text = Sesion.Nombre;
            txtApellido.Text = Sesion.Apellido;
            txtEmail.Text = Sesion.Email;
        }
    }
}
