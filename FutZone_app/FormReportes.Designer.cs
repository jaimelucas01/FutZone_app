namespace FutZone_app
{
    partial class FormReportes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartReservas = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerrarReporte = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartReservas)).BeginInit();
            this.SuspendLayout();
            // 
            // chartReservas
            // 
            chartArea2.Name = "ChartArea1";
            this.chartReservas.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartReservas.Legends.Add(legend2);
            this.chartReservas.Location = new System.Drawing.Point(72, 81);
            this.chartReservas.Name = "chartReservas";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartReservas.Series.Add(series2);
            this.chartReservas.Size = new System.Drawing.Size(671, 294);
            this.chartReservas.TabIndex = 0;
            this.chartReservas.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(181, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(474, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Canchas más solicitadas (Histórico)";
            // 
            // btnCerrarReporte
            // 
            this.btnCerrarReporte.Location = new System.Drawing.Point(314, 381);
            this.btnCerrarReporte.Name = "btnCerrarReporte";
            this.btnCerrarReporte.Size = new System.Drawing.Size(159, 46);
            this.btnCerrarReporte.TabIndex = 2;
            this.btnCerrarReporte.Text = "Volver al Menú";
            this.btnCerrarReporte.UseVisualStyleBackColor = true;
            this.btnCerrarReporte.Click += new System.EventHandler(this.btnCerrarReporte_Click);
            // 
            // FormReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCerrarReporte);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chartReservas);
            this.Name = "FormReportes";
            this.Text = "FutZone - Estadísticas de Uso";
            this.Load += new System.EventHandler(this.FormReportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartReservas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartReservas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCerrarReporte;
    }
}