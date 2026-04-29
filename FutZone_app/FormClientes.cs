using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FutZone_app
{
    public partial class FormClientes : Form
    {
        string cadenaConexion = @"Data Source=Manu\SQLEXPRESS;Initial Catalog=FutZone_DB;Integrated Security=True";

        public FormClientes()
        {
            InitializeComponent();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    // Traemos solo los activos para cumplir con la integridad del sistema
                    string query = "SELECT ID_Cliente, Nombre, Apellido, DNI, Telefono, Email FROM Clientes WHERE Estado = 'ACTIVO'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);
                    dgvClientes.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar clientes: " + ex.Message);
                }
            }
        }

        // --- BOTÓN GUARDAR (NUEVO CLIENTE) ---
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación de campos vacíos
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtDNI.Text))
            {
                MessageBox.Show("Nombre y DNI son obligatorios.");
                return;
            }

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    string query = "INSERT INTO Clientes (Nombre, Apellido, DNI, Telefono, Email, Estado) VALUES (@nom, @ape, @dni, @tel, @mail, 'ACTIVO')";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@ape", txtApellido.Text);
                        cmd.Parameters.AddWithValue("@dni", txtDNI.Text);
                        cmd.Parameters.AddWithValue("@tel", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@mail", txtEmail.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cliente guardado correctamente.");

                        LimpiarCampos();
                        ActualizarGrilla();
                    }
                }
                // CAMBIO AQUÍ: Capturamos específicamente SqlException
                catch (SqlException ex)
                {
                    // El número 2627 o 2601 indica violación de clave única (DNI repetido)
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        MessageBox.Show("¡Error! Ya existe un cliente registrado con ese DNI.",
                                        "Dato Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // Opcional: enfocar el texto para corregirlo rápido
                        txtDNI.Focus();
                        txtDNI.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("Error de base de datos: " + ex.Message);
                    }
                }
                // Catch general por si ocurre otro tipo de error (fuera de SQL)
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message);
                }
            }
        }

        // --- BOTÓN EDITAR (MODIFICAR EXISTENTE) ---
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un cliente de la lista para modificar.");
                return;
            }

            string idCliente = dgvClientes.CurrentRow.Cells["ID_Cliente"].Value.ToString();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    string query = @"UPDATE Clientes 
                                     SET Nombre=@nom, Apellido=@ape, DNI=@dni, Telefono=@tel, Email=@mail 
                                     WHERE ID_Cliente=@id";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@ape", txtApellido.Text);
                        cmd.Parameters.AddWithValue("@dni", txtDNI.Text);
                        cmd.Parameters.AddWithValue("@tel", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@id", idCliente);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cliente actualizado con éxito.");

                        LimpiarCampos();
                        ActualizarGrilla();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar: " + ex.Message);
                }
            }
        }

        // --- FUNCIÓN PARA PASAR DATOS DE LA GRILLA A LOS CAMPOS ---
        // Vinculá este evento desde el rayito (Eventos) de la grilla -> CellClick
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtApellido.Text = fila.Cells["Apellido"].Value.ToString();
                txtDNI.Text = fila.Cells["DNI"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                txtEmail.Text = fila.Cells["Email"].Value.ToString();
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDNI.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
        }

        // --- BOTÓN ELIMINAR (BAJA LÓGICA) ---
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null) return;

            string idCliente = dgvClientes.CurrentRow.Cells["ID_Cliente"].Value.ToString();
            DialogResult respuesta = MessageBox.Show("¿Estás seguro de eliminar este cliente?", "Confirmar", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "UPDATE Clientes SET Estado = 'INACTIVO' WHERE ID_Cliente = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", idCliente);
                        cmd.ExecuteNonQuery();
                        ActualizarGrilla();
                        LimpiarCampos();
                    }
                }
            }
        }
    }
}