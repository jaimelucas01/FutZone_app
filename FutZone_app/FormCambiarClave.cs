using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace FutZone_app
{
    public partial class FormCambiarClave : Form
    {
        public FormCambiarClave()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validar campos vacíos
            if (string.IsNullOrEmpty(txtActual.Text) || string.IsNullOrEmpty(txtNueva.Text) || string.IsNullOrEmpty(txtConfirmar.Text))
            {
                MessageBox.Show("Por favor, completá todos los campos.");
                return;
            }

            // 2. Verificar clave actual usando nuestra nueva clase Seguridad
            string claveActualEncriptada = Seguridad.EncriptarSHA256(txtActual.Text);
            if (!VerificarClaveActual(claveActualEncriptada))
            {
                MessageBox.Show("La contraseña actual es incorrecta.");
                return;
            }

            // 3. Verificar igualdad de nuevas claves
            if (txtNueva.Text != txtConfirmar.Text)
            {
                MessageBox.Show("Las nuevas contraseñas no coinciden.");
                return;
            }

            // 4. Validar complejidad (RNF9)
            if (!ValidarComplejidad(txtNueva.Text))
            {
                MessageBox.Show("La nueva clave no cumple los requisitos (Mínimo 8 caracteres, 1 Mayúscula, 1 Número y 1 Símbolo).");
                return;
            }

            // 5. Actualizar en SQL con la nueva encriptación
            try
            {
                ActualizarClaveEnSQL(Seguridad.EncriptarSHA256(txtNueva.Text));
                MessageBox.Show("¡Contraseña actualizada con éxito!");

                // LIMPIEZA CORRECTA (Sin errores de Label)
                txtActual.Clear();
                txtNueva.Clear();
                txtConfirmar.Clear();
                // Si tenés algún label de estado, usá .Text = ""
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        // RNF9: Validación de seguridad
        private bool ValidarComplejidad(string clave)
        {
            bool tieneMayuscula = clave.Any(char.IsUpper);
            bool tieneNumero = clave.Any(char.IsDigit);
            bool tieneSimbolo = clave.Any(c => !char.IsLetterOrDigit(c));
            bool largoCorrecto = clave.Length >= 8;

            return tieneMayuscula && tieneNumero && tieneSimbolo && largoCorrecto;
        }

        // RNF7: Encriptación
        private string Encriptar(string texto)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                var sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    sb.Append(bytes[i].ToString("x2"));
                return sb.ToString();
            }
        }

        // Función nueva para verificar la contraseña vieja en la DB
        private bool VerificarClaveActual(string claveEncriptada)
        {
            string cadenaConexion = @"Data Source=Manu\SQLEXPRESS;Initial Catalog=FutZone_DB;Integrated Security=True";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT COUNT(*) FROM Usuarios WHERE ID_Usuario = @id AND Clave = @pass";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", Sesion.ID_Usuario);
                    cmd.Parameters.AddWithValue("@pass", claveEncriptada);

                    int resultado = (int)cmd.ExecuteScalar();
                    return resultado > 0; // Retorna true si encontró coincidencia
                }
            }
        }

        // Función para guardar la nueva
        private void ActualizarClaveEnSQL(string nuevaClaveEncriptada)
        {
            string cadenaConexion = @"Data Source=Manu\SQLEXPRESS;Initial Catalog=FutZone_DB;Integrated Security=True";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "UPDATE Usuarios SET Clave = @pass WHERE ID_Usuario = @id";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@pass", nuevaClaveEncriptada);
                    cmd.Parameters.AddWithValue("@id", Sesion.ID_Usuario);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contraseña actualizada con éxito.");
                    this.Close();
                }
            }
        }
    }
}