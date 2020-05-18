namespace Equipment_rental
{
    partial class VseIzposoje
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nadzornik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Porocilo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Oprema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Placnik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatumOd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatumDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Nadzornik,
            this.Porocilo,
            this.Oprema,
            this.Placnik,
            this.DatumOd,
            this.DatumDo});
            this.dataGridView1.Location = new System.Drawing.Point(23, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(742, 364);
            this.dataGridView1.TabIndex = 0;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(24, 434);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(75, 23);
            this.metroButton1.TabIndex = 1;
            this.metroButton1.Text = "Izbrisi";
            this.metroButton1.Click += new System.EventHandler(this.MetroButton1_Click);
            // 
            // Id
            // 
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            // 
            // Nadzornik
            // 
            this.Nadzornik.HeaderText = "Nadzornik";
            this.Nadzornik.Name = "Nadzornik";
            // 
            // Porocilo
            // 
            this.Porocilo.HeaderText = "Poročilo";
            this.Porocilo.Name = "Porocilo";
            // 
            // Oprema
            // 
            this.Oprema.HeaderText = "Oprema";
            this.Oprema.Name = "Oprema";
            // 
            // Placnik
            // 
            this.Placnik.HeaderText = "Placnik";
            this.Placnik.Name = "Placnik";
            // 
            // DatumOd
            // 
            this.DatumOd.HeaderText = "Datum Od";
            this.DatumOd.Name = "DatumOd";
            // 
            // DatumDo
            // 
            this.DatumDo.HeaderText = "Datum Do";
            this.DatumDo.Name = "DatumDo";
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(106, 434);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(75, 23);
            this.metroButton2.TabIndex = 2;
            this.metroButton2.Text = "Uredi";
            this.metroButton2.Click += new System.EventHandler(this.MetroButton2_Click_1);
            // 
            // VseIzposoje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 514);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.Name = "VseIzposoje";
            this.Resizable = false;
            this.Text = "Vse Izposoje";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private MetroFramework.Controls.MetroButton metroButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nadzornik;
        private System.Windows.Forms.DataGridViewTextBoxColumn Porocilo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Oprema;
        private System.Windows.Forms.DataGridViewTextBoxColumn Placnik;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatumOd;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatumDo;
        private MetroFramework.Controls.MetroButton metroButton2;
    }
}