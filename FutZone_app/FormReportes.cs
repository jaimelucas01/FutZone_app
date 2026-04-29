using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Necesario para que reconozca SeriesChartType

namespace FutZone_app
{
    public partial class FormReportes : Form
    {
        // Tu cadena de conexión centralizada
        string cadenaConexion = @"Data Source=Manu\SQLEXPRESS;Initial Catalog=FutZone_DB;Integrated Security=True";

        public FormReportes()
        {
            InitializeComponent();
        }

        private void FormReportes_Load(object sender, EventArgs e)
        {
            ConfigurarGrafico();
            CargarDatosGrafico();
        }

        private void ConfigurarGrafico()
        {
            chartReservas.Series.Clear();
            chartReservas.Titles.Clear();
            chartReservas.Titles.Add("Uso de Canchas por Reserva");

            // Creamos la serie
            var serie = chartReservas.Series.Add("Reservas");

            // Puedes usar 'Column' (Barras) o 'Pie' (Torta)
            serie.ChartType = SeriesChartType.Column;

            // Muestra el número exacto arriba de la barra
            serie.IsValueShownAsLabel = true;
        }

        private void CargarDatosGrafico()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // Hacemos un JOIN para traer el NOMBRE de la cancha y no solo el ID
                string query = @"SELECT C.Nombre, COUNT(R.ID_Reserva) as Cantidad 
                                 FROM Canchas C 
                                 LEFT JOIN Reservas R ON C.ID_Cancha = R.ID_Cancha 
                                 GROUP BY C.Nombre";

                SqlCommand cmd = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader leer = cmd.ExecuteReader();

                    while (leer.Read())
                    {
                        // Agregamos al gráfico: Eje X = Nombre de cancha, Eje Y = Cantidad
                        chartReservas.Series["Reservas"].Points.AddXY(leer["Nombre"].ToString(), leer["Cantidad"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar gráfico: " + ex.Message);
                }
            }
        }

        private void btnCerrarReporte_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra esta ventana y vuelve al FormPrincipal
        }
    }
}