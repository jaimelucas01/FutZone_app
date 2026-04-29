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
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Esto mata el proceso completo al cerrar la ventana
        }

        private void miPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPerfil perfil = new FormPerfil();
            perfil.ShowDialog(); // ShowDialog hace que no puedas tocar el menú hasta cerrar el perfil
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCambiarClave frm = new FormCambiarClave();
            frm.ShowDialog();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClientes formCli = new FormClientes();
            formCli.ShowDialog();
        }

        private void reservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReservas ventanaReservas = new FormReservas();
            ventanaReservas.ShowDialog(); // Esto abre la ventana y no te deja tocar la de atrás hasta cerrar esta
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // 1. Bienvenida personalizada: Esto suma puntos en "Experiencia de Usuario"
            this.Text = "FutZone - Usuario: " + Sesion.Nombre + " (" + Sesion.Usuario + ")";

            // 2. Control de Acceso (Modelo de Autorización)
            // Usamos ToLower() para que no importe si escribiste "Admin" o "admin"
            if (Sesion.Usuario.ToLower() != "admin")
            {
                // Es preferible ocultar el menú completo para que el encargado 
                // no intente tocar lo que no le corresponde.
                seguridadToolStripMenuItem.Visible = false;

                // OPCIONAL: Si querés que vea la pestaña pero no entre a Canchas/Parrilleros
                // canchasParrillerosToolStripMenuItem.Visible = false;
            }
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1(); // Volvemos a la pantalla de Login
            login.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Cierra todo el programa
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsuarios frm = new FormUsuarios();
            frm.ShowDialog(); // Esto abre la ventana de usuarios
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGrupos frm = new FormGrupos();
            frm.ShowDialog();
        }

        private void estadisticasDeUsoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cambiamos 'FormEstadisticas' por el nombre real de tu formulario
            FormReportes frm = new FormReportes();
            frm.ShowDialog();
        }
    }
    }

