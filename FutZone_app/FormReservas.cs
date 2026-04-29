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
    public partial class FormReservas : Form
    {
        string cadenaConexion = @"Data Source=Manu\SQLEXPRESS;Initial Catalog=FutZone_DB;Integrated Security=True";

        int idReservaSeleccionada = 0;

        public FormReservas()
        {
            InitializeComponent();
        }

        private void FormReservas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarCanchas();
            ActualizarGrillaReservas();
        }

        private void CargarClientes()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT ID_Cliente, Nombre + ' ' + Apellido AS NombreCompleto FROM Clientes WHERE Estado = 'ACTIVO'";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                cmbClientes.DataSource = dt;
                cmbClientes.DisplayMember = "NombreCompleto";
                cmbClientes.ValueMember = "ID_Cliente";
            }
        }

        private void CargarCanchas()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT ID_Cancha, Nombre + ' ($' + CAST(PrecioHora AS VARCHAR) + ')' AS DetalleCancha FROM Canchas WHERE Estado = 'ACTIVA'";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                cmbCanchas.DataSource = dt;
                cmbCanchas.DisplayMember = "DetalleCancha";
                cmbCanchas.ValueMember = "ID_Cancha";
            }
        }

        private void ActualizarGrillaReservas()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    // PASO 3.1: Agregamos ID_Reserva y Pagado a la consulta
                    string query = @"SELECT R.ID_Reserva, R.HoraReserva, C.Nombre AS Cancha, 
                                     CL.Nombre + ' ' + CL.Apellido AS Cliente, R.Total, 
                                     CASE WHEN R.Pagado = 1 THEN 'SÍ' ELSE 'NO' END AS [¿Pagado?]
                                     FROM Reservas R
                                     JOIN Canchas C ON R.ID_Cancha = C.ID_Cancha
                                     JOIN Clientes CL ON R.ID_Cliente = CL.ID_Cliente
                                     WHERE R.FechaReserva = @fecha";

                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                    adaptador.SelectCommand.Parameters.AddWithValue("@fecha", dtpFecha.Value.Date);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);
                    dgvReservas.DataSource = dt;

                    dgvReservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // PASO 3.2: Ocultamos la columna del ID para que el admin no la vea
                    if (dgvReservas.Columns.Contains("ID_Reserva"))
                    {
                        dgvReservas.Columns["ID_Reserva"].Visible = false;
                    }

                    dgvReservas.Columns["HoraReserva"].HeaderText = "Hora";
                    dgvReservas.Columns["Cancha"].HeaderText = "Cancha / Recurso";
                    dgvReservas.Columns["Cliente"].HeaderText = "Nombre del Cliente";
                    dgvReservas.Columns["Total"].HeaderText = "Precio ($)";
                    CalcularTotalDia();
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("Error al cargar grilla: " + ex.Message);
                }
            }
        }
        private void CalcularTotalDia()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    // Sumamos la columna Total solo de las reservas que ya fueron marcadas como pagadas (Pagado = 1)
                    string query = "SELECT SUM(Total) FROM Reservas WHERE FechaReserva = @fecha AND Pagado = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.Date);

                        object resultado = cmd.ExecuteScalar();

                        // Si no hay cobros, el resultado es null o DBNull
                        if (resultado != null && resultado != DBNull.Value)
                        {
                            decimal total = Convert.ToDecimal(resultado);
                            lblTotalRecaudado.Text = "Total Recaudado: " + total.ToString("C"); // "C" le da formato moneda
                        }
                        else
                        {
                            lblTotalRecaudado.Text = "Total Recaudado: $0,00";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Opcional: mostrar error si falla la conexión
                }
            }
        }


        private void btnReservar_Click(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedValue == null || cmbCanchas.SelectedValue == null || string.IsNullOrEmpty(txtHora.Text))
            {
                MessageBox.Show("Por favor, completá todos los campos.");
                return;
            }

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string queryValidar = @"SELECT COUNT(*) FROM Reservas 
                                            WHERE ID_Cancha = @idCan 
                                            AND FechaReserva = @fecha 
                                            AND HoraReserva = @hora";

                    using (SqlCommand cmdValidar = new SqlCommand(queryValidar, conexion))
                    {
                        cmdValidar.Parameters.AddWithValue("@idCan", cmbCanchas.SelectedValue);
                        cmdValidar.Parameters.AddWithValue("@fecha", dtpFecha.Value.Date);
                        cmdValidar.Parameters.AddWithValue("@hora", txtHora.Text);

                        int ocupado = (int)cmdValidar.ExecuteScalar();
                        if (ocupado > 0)
                        {
                            MessageBox.Show("Lo siento, esa cancha ya está reservada para ese día y hora.");
                            return;
                        }
                    }

                    // --- CAMBIO PARA AUDITORÍA ---
                    // Agregamos ID_Usuario en el INSERT
                    string queryInsert = @"INSERT INTO Reservas (ID_Cliente, ID_Cancha, FechaReserva, HoraReserva, Total, Pagado, ID_Usuario) 
                       VALUES (@idCli, @idCan, @fecha, @hora, @total, 0, @idUser)";

                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conexion))
                    {
                        cmdInsert.Parameters.AddWithValue("@idCli", cmbClientes.SelectedValue);
                        cmdInsert.Parameters.AddWithValue("@idCan", cmbCanchas.SelectedValue);
                        cmdInsert.Parameters.AddWithValue("@fecha", dtpFecha.Value.Date);
                        cmdInsert.Parameters.AddWithValue("@hora", txtHora.Text);
                        cmdInsert.Parameters.AddWithValue("@total", decimal.Parse(txtTotal.Text));

                        // Aquí usamos la clase Sesion para la Auditoría
                        cmdInsert.Parameters.AddWithValue("@idUser", Sesion.ID_Usuario);

                        cmdInsert.ExecuteNonQuery();
                        MessageBox.Show("¡Reserva confirmada!");

                        ActualizarGrillaReservas();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar la reserva: " + ex.Message);
                }
            }
        }

        private void cmbCanchas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCanchas.SelectedValue == null || cmbCanchas.SelectedIndex == -1) return;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT PrecioHora FROM Canchas WHERE ID_Cancha = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", cmbCanchas.SelectedValue);
                        object resultado = cmd.ExecuteScalar();
                        if (resultado != null)
                        {
                            txtTotal.Text = resultado.ToString();
                        }
                    }
                }
                catch { }
            }
        }

        // PASO 3.3: Capturamos el ID cuando el usuario hace clic en la grilla
        private void dgvReservas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            ActualizarGrillaReservas();
            CalcularTotalDia();
        }

        private void dgvReservas_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que se haya hecho clic en una fila y no en el título de la columna
            if (e.RowIndex >= 0)
            {
                // Guardamos el ID en nuestra variable
                idReservaSeleccionada = Convert.ToInt32(dgvReservas.Rows[e.RowIndex].Cells["ID_Reserva"].Value);

                // Cambiamos el texto del botón para confirmar que se seleccionó
                btnCobrar.Text = "Cobrar Reserva #" + idReservaSeleccionada;
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (idReservaSeleccionada == 0)
            {
                MessageBox.Show("Por favor, seleccioná una reserva de la lista primero.");
                return;
            }

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    // Cambiamos el estado de 0 a 1 en la base de datos
                    string query = "UPDATE Reservas SET Pagado = 1 WHERE ID_Reserva = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", idReservaSeleccionada);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("¡Pago registrado con éxito!");

                        // Limpiamos la selección y refrescamos la grilla para que diga "SÍ" en Pagado
                        idReservaSeleccionada = 0;
                        btnCobrar.Text = "Marcar como Pagado";
                        ActualizarGrillaReservas();
                        CalcularTotalDia();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar el pago: " + ex.Message);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}