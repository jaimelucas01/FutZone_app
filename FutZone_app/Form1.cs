using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FutZone_app
{
    public partial class Form1 : Form
    {
        // Centralizamos la cadena de conexión
        string cadenaConexion = @"Data Source=Manu\SQLEXPRESS;Initial Catalog=FutZone_DB;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string usuarioInput = txtUsuario.Text.Trim();
                string claveInput = txtContraseña.Text.Trim();

                if (string.IsNullOrEmpty(usuarioInput) || string.IsNullOrEmpty(claveInput))
                {
                    MessageBox.Show("Por favor, ingrese usuario y contraseña.");
                    return;
                }

                // USAMOS LA CLASE SEGURIDAD (RNF7)
                string claveEncriptada = Seguridad.EncriptarSHA256(claveInput);

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    // RF1: Autenticar identidad y verificar estado ACTIVO
                    string query = "SELECT ID_Usuario, Usuario, Nombre, Apellido, Email FROM Usuarios WHERE Usuario = @user AND Clave = @pass AND Estado = 'ACTIVO'";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@user", usuarioInput);
                        cmd.Parameters.AddWithValue("@pass", claveEncriptada);

                        SqlDataReader leer = cmd.ExecuteReader();

                        if (leer.Read())
                        {
                            // Guardamos los datos en la clase Sesion para usarlos en toda la app
                            Sesion.ID_Usuario = leer.GetInt32(0);
                            Sesion.Usuario = leer.GetString(1);
                            Sesion.Nombre = leer.GetString(2);
                            Sesion.Apellido = leer.GetString(3);
                            Sesion.Email = leer.GetString(4);

                            MessageBox.Show("¡Bienvenido " + Sesion.Nombre + "!");

                            FormPrincipal menu = new FormPrincipal();
                            menu.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
        }
    }
}