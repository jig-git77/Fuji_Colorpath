namespace FujiFilmSyncExt
{
    partial class Form1
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtICCfilePath = new System.Windows.Forms.TextBox();
            this.btnCreateChart = new System.Windows.Forms.Button();
            this.dataGridColor = new System.Windows.Forms.DataGridView();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(46, 12);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtICCfilePath
            // 
            this.txtICCfilePath.Location = new System.Drawing.Point(127, 15);
            this.txtICCfilePath.Name = "txtICCfilePath";
            this.txtICCfilePath.Size = new System.Drawing.Size(679, 20);
            this.txtICCfilePath.TabIndex = 1;
            // 
            // btnCreateChart
            // 
            this.btnCreateChart.Location = new System.Drawing.Point(813, 15);
            this.btnCreateChart.Name = "btnCreateChart";
            this.btnCreateChart.Size = new System.Drawing.Size(75, 23);
            this.btnCreateChart.TabIndex = 2;
            this.btnCreateChart.Text = "Create Chart";
            this.btnCreateChart.UseVisualStyleBackColor = true;
            this.btnCreateChart.Click += new System.EventHandler(this.btnCreateChart_Click);
            // 
            // dataGridColor
            // 
            this.dataGridColor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridColor.Location = new System.Drawing.Point(46, 59);
            this.dataGridColor.Name = "dataGridColor";
            this.dataGridColor.Size = new System.Drawing.Size(842, 150);
            this.dataGridColor.TabIndex = 3;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(46, 235);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(842, 391);
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 638);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.dataGridColor);
            this.Controls.Add(this.btnCreateChart);
            this.Controls.Add(this.txtICCfilePath);
            this.Controls.Add(this.btnSelect);
            this.Name = "Form1";
            this.Text = "Color Sync Ext";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtICCfilePath;
        private System.Windows.Forms.Button btnCreateChart;
        private System.Windows.Forms.DataGridView dataGridColor;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}

