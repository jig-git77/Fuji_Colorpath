namespace FujifilmUSA.Sync.App
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
            System.Windows.Forms.Label lblReferencePath;
            this.btnGetReferenceChart = new System.Windows.Forms.Button();
            this.lblProjectID = new System.Windows.Forms.Label();
            this.txtProjectID = new System.Windows.Forms.TextBox();
            this.dtGrdPatchs = new System.Windows.Forms.DataGridView();
            this.lblRefChartPath = new System.Windows.Forms.Label();
            this.btnUploadMeasurement = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblMessage = new System.Windows.Forms.Label();
            lblReferencePath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdPatchs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetReferenceChart
            // 
            this.btnGetReferenceChart.Location = new System.Drawing.Point(219, 6);
            this.btnGetReferenceChart.Name = "btnGetReferenceChart";
            this.btnGetReferenceChart.Size = new System.Drawing.Size(143, 23);
            this.btnGetReferenceChart.TabIndex = 0;
            this.btnGetReferenceChart.Text = "Get Reference Chart";
            this.btnGetReferenceChart.UseVisualStyleBackColor = true;
            this.btnGetReferenceChart.Click += new System.EventHandler(this.btnGetReferenceChart_Click);
            // 
            // lblProjectID
            // 
            this.lblProjectID.AutoSize = true;
            this.lblProjectID.Location = new System.Drawing.Point(9, 11);
            this.lblProjectID.Name = "lblProjectID";
            this.lblProjectID.Size = new System.Drawing.Size(82, 13);
            this.lblProjectID.TabIndex = 1;
            this.lblProjectID.Text = "Enter Project ID";
            // 
            // txtProjectID
            // 
            this.txtProjectID.Location = new System.Drawing.Point(98, 8);
            this.txtProjectID.Name = "txtProjectID";
            this.txtProjectID.Size = new System.Drawing.Size(100, 20);
            this.txtProjectID.TabIndex = 2;
            // 
            // dtGrdPatchs
            // 
            this.dtGrdPatchs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdPatchs.Location = new System.Drawing.Point(12, 35);
            this.dtGrdPatchs.Name = "dtGrdPatchs";
            this.dtGrdPatchs.Size = new System.Drawing.Size(753, 224);
            this.dtGrdPatchs.TabIndex = 3;
            // 
            // lblReferencePath
            // 
            lblReferencePath.AutoSize = true;
            lblReferencePath.Location = new System.Drawing.Point(375, 11);
            lblReferencePath.Name = "lblReferencePath";
            lblReferencePath.Size = new System.Drawing.Size(0, 13);
            lblReferencePath.TabIndex = 4;
            lblReferencePath.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblRefChartPath
            // 
            this.lblRefChartPath.AutoSize = true;
            this.lblRefChartPath.Location = new System.Drawing.Point(375, 11);
            this.lblRefChartPath.Name = "lblRefChartPath";
            this.lblRefChartPath.Size = new System.Drawing.Size(0, 13);
            this.lblRefChartPath.TabIndex = 5;
            // 
            // btnUploadMeasurement
            // 
            this.btnUploadMeasurement.Location = new System.Drawing.Point(12, 294);
            this.btnUploadMeasurement.Name = "btnUploadMeasurement";
            this.btnUploadMeasurement.Size = new System.Drawing.Size(123, 23);
            this.btnUploadMeasurement.TabIndex = 6;
            this.btnUploadMeasurement.Text = "Upload Measurement";
            this.btnUploadMeasurement.UseVisualStyleBackColor = true;
            this.btnUploadMeasurement.Click += new System.EventHandler(this.btnUploadMeasurement_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 346);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 425);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnUploadMeasurement);
            this.Controls.Add(this.lblRefChartPath);
            this.Controls.Add(lblReferencePath);
            this.Controls.Add(this.dtGrdPatchs);
            this.Controls.Add(this.txtProjectID);
            this.Controls.Add(this.lblProjectID);
            this.Controls.Add(this.btnGetReferenceChart);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdPatchs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetReferenceChart;
        private System.Windows.Forms.Label lblProjectID;
        private System.Windows.Forms.TextBox txtProjectID;
        private System.Windows.Forms.DataGridView dtGrdPatchs;
        private System.Windows.Forms.Label lblRefChartPath;
        private System.Windows.Forms.Button btnUploadMeasurement;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblMessage;
    }
}

