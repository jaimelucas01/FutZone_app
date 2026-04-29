namespace FutZone_app
{
    partial class FormGrupos
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
            this.txtNombreGrupo = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvGrupos = new System.Windows.Forms.DataGridView();
            this.btnGuardarGrupo = new System.Windows.Forms.Button();
            this.btnEliminarGrupo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrupos)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNombreGrupo
            // 
            this.txtNombreGrupo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombreGrupo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreGrupo.Location = new System.Drawing.Point(27, 57);
            this.txtNombreGrupo.Name = "txtNombreGrupo";
            this.txtNombreGrupo.Size = new System.Drawing.Size(257, 27);
            this.txtNombreGrupo.TabIndex = 0;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(27, 143);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(257, 218);
            this.txtDescripcion.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Grupo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Descripcion";
            // 
            // dgvGrupos
            // 
            this.dgvGrupos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGrupos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGrupos.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvGrupos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGrupos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrupos.Location = new System.Drawing.Point(312, 29);
            this.dgvGrupos.Name = "dgvGrupos";
            this.dgvGrupos.RowHeadersVisible = false;
            this.dgvGrupos.RowHeadersWidth = 51;
            this.dgvGrupos.RowTemplate.Height = 24;
            this.dgvGrupos.Size = new System.Drawing.Size(462, 390);
            this.dgvGrupos.TabIndex = 4;
            // 
            // btnGuardarGrupo
            // 
            this.btnGuardarGrupo.BackColor = System.Drawing.Color.Lime;
            this.btnGuardarGrupo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarGrupo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarGrupo.Location = new System.Drawing.Point(27, 367);
            this.btnGuardarGrupo.Name = "btnGuardarGrupo";
            this.btnGuardarGrupo.Size = new System.Drawing.Size(105, 52);
            this.btnGuardarGrupo.TabIndex = 5;
            this.btnGuardarGrupo.Text = "Agregar";
            this.btnGuardarGrupo.UseVisualStyleBackColor = false;
            this.btnGuardarGrupo.Click += new System.EventHandler(this.btnGuardarGrupo_Click);
            // 
            // btnEliminarGrupo
            // 
            this.btnEliminarGrupo.BackColor = System.Drawing.Color.Red;
            this.btnEliminarGrupo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarGrupo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarGrupo.Location = new System.Drawing.Point(179, 367);
            this.btnEliminarGrupo.Name = "btnEliminarGrupo";
            this.btnEliminarGrupo.Size = new System.Drawing.Size(105, 52);
            this.btnEliminarGrupo.TabIndex = 6;
            this.btnEliminarGrupo.Text = "Eliminar";
            this.btnEliminarGrupo.UseVisualStyleBackColor = false;
            // 
            // FormGrupos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnEliminarGrupo);
            this.Controls.Add(this.btnGuardarGrupo);
            this.Controls.Add(this.dgvGrupos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNombreGrupo);
            this.Name = "FormGrupos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grupos";
            this.Load += new System.EventHandler(this.FormGrupos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrupos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombreGrupo;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvGrupos;
        private System.Windows.Forms.Button btnGuardarGrupo;
        private System.Windows.Forms.Button btnEliminarGrupo;
    }
}