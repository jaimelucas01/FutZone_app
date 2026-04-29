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
    public partial class FormGrupos : Form
    {
        public FormGrupos()
        {
            InitializeComponent();
        }

        private void btnGuardarGrupo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreGrupo.Text))
            {
                MessageBox.Show("El nombre del grupo es obligatorio.");
                return;
            }

            string cadena = @"Data Source=Manu\SQLEXPRESS;Initial Catalog=FutZone_DB;Integrated Security=True";

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                // Insertamos el nuevo grupo en la tabla correspondiente
                string query = "INSERT INTO Grupos (Nombre, Descripcion) VALUES (@nombre, @desc)";
                SqlCommand cmd = new SqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@nombre", txtNombreGrupo.Text.Trim());
                cmd.Parameters.AddWithValue("@desc", txtDescripcion.Text.Trim());

                try
                {
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Grupo creado correctamente.");

                    txtNombreGrupo.Clear();
                    txtDescripcion.Clear();
                    ActualizarGrillaGrupos(); // Función para refrescar la lista
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        // Pegá esto abajo del botón de guardar, antes de la última llave }
        private void ActualizarGrillaGrupos()
        {
            string cadena = @"Data Source=Manu\SQLEXPRESS;Initial Catalog=FutZone_DB;Integrated Security=True";
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                // Traemos los datos de la tabla Grupos
                string query = "SELECT ID_Grupo, Nombre, Descripcion FROM Grupos";
                SqlDataAdapter da = new SqlDataAdapter(query, conexion);
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    dgvGrupos.DataSource = dt; // Asegurate que tu Grilla se llame dgvGrupos
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la grilla: " + ex.Message);
                }
            }
        }

        private void FormGrupos_Load(object sender, EventArgs e)
        {
            ActualizarGrillaGrupos();
        }
    }
}
