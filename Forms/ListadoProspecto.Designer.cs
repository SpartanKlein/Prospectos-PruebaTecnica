namespace GestorProstectos.Forms
{
    partial class ListadoProspecto
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
            dvgProspectos = new DataGridView();
            label1 = new Label();
            btnNuevoProspecto = new Button();
            btnSalir = new Button();
            ((System.ComponentModel.ISupportInitialize)dvgProspectos).BeginInit();
            SuspendLayout();
            // 
            // dvgProspectos
            // 
            dvgProspectos.AccessibleName = "dvgProspectos";
            dvgProspectos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgProspectos.Location = new Point(26, 76);
            dvgProspectos.Name = "dvgProspectos";
            dvgProspectos.Size = new Size(762, 345);
            dvgProspectos.TabIndex = 0;
            dvgProspectos.CellContentClick += dvgProspectos_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 32);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 1;
            label1.Text = "Prospectos";
            // 
            // btnNuevoProspecto
            // 
            btnNuevoProspecto.Location = new Point(536, 35);
            btnNuevoProspecto.Name = "btnNuevoProspecto";
            btnNuevoProspecto.Size = new Size(75, 23);
            btnNuevoProspecto.TabIndex = 2;
            btnNuevoProspecto.Text = "Agregar";
            btnNuevoProspecto.UseVisualStyleBackColor = true;
            btnNuevoProspecto.Click += btnNuevoProspecto_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(684, 427);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += Salir;
            // 
            // ListadoProspecto
            // 
            AccessibleName = "dvgProspectos";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 474);
            Controls.Add(btnSalir);
            Controls.Add(btnNuevoProspecto);
            Controls.Add(label1);
            Controls.Add(dvgProspectos);
            Name = "ListadoProspecto";
            Text = "ListadoProspecto";
            ((System.ComponentModel.ISupportInitialize)dvgProspectos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dvgProspectos;
        private Label label1;
        private Button btnNuevoProspecto;
        private DataGridViewTextBoxColumn Actions;
        private Button btnSalir;
    }
}