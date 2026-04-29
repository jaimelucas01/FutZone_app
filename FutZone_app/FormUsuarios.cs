using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutZone_app
{
    public partial class FormUsuarios : Form
    {
        public FormUsuarios()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validamos que no haya campos vacíos
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtClave.Text))
            {
                MessageBox.Show("Usuario y Clave son obligatorios.");
                return;
            }

            // 2. ENCRIPTACIÓN (Modelo de Seguridad)
            // Usamos la clase que ya creamos para que la clave no sea legible
            string claveCifrada = Seguridad.EncriptarSHA256(txtClave.Text.Trim());

            // 3. CONEXIÓN A LA BASE DE DATOS
            string cadena = @"Data Source=Manu\SQLEXPRESS;Initial Catalog=FutZone_DB;Integrated Security=True";

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                // El query de inserción. Nota que ponemos 'ACTIVO' por defecto
                string query = "INSERT INTO Usuarios (Usuario, Clave, Nombre, Apellido, Email, Estado) " +
                               "VALUES (@user, @pass, @nom, @ape, @mail, 'ACTIVO')";

                SqlCommand cmd = new SqlCommand(query, conexion);

                // Pasamos los valores de los TextBoxes a los parámetros del SQL
                cmd.Parameters.AddWithValue("@user", txtUsuario.Text.Trim());
                cmd.Parameters.AddWithValue("@pass", claveCifrada); // Guardamos la versión cifrada
                cmd.Parameters.AddWithValue("@nom", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@ape", txtApellido.Text.Trim());
                cmd.Parameters.AddWithValue("@mail", txtEmail.Text.Trim());

                try
                {
                    conexion.Open();
                    cmd.ExecuteNonQuery(); // Ejecuta la orden en SQL
                    MessageBox.Show("Usuario creado con éxito.");

                    // Limpiamos los campos para el próximo usuario
                    txtUsuario.Clear();
                    txtClave.Clear();
                    txtNombre.Clear();
                    txtApellido.Clear();
                    txtEmail.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message);
                }
            }
        }

        private void cmbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            cmbGrupo.Items.Add("Admin");
            cmbGrupo.Items.Add("Encargado");
            cmbGrupo.SelectedIndex = 0; // Selecciona el primero por defecto
        }
    }
}
