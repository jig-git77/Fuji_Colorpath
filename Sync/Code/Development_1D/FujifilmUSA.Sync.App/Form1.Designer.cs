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
            this.rtbChartSetting = new System.Windows.Forms.RichTextBox();
            this.rtbDeviceSetting = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOptimizationRunID = new System.Windows.Forms.TextBox();
            this.btnLoadMeasurement = new System.Windows.Forms.Button();
            this.dataGridMeasurement = new System.Windows.Forms.DataGridView();
            this.rbtnProfiler = new System.Windows.Forms.RadioButton();
            this.rbtnValidation = new System.Windows.Forms.RadioButton();
            this.rbtnOptimization = new System.Windows.Forms.RadioButton();
            this.rbtnSpot = new System.Windows.Forms.RadioButton();
            lblReferencePath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdPatchs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMeasurement)).BeginInit();
            this.SuspendLayout();
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
            // btnGetReferenceChart
            // 
            this.btnGetReferenceChart.Location = new System.Drawing.Point(544, 11);
            this.btnGetReferenceChart.Name = "btnGetReferenceChart";
            this.btnGetReferenceChart.Size = new System.Drawing.Size(137, 23);
            this.btnGetReferenceChart.TabIndex = 0;
            this.btnGetReferenceChart.Text = "Get Reference Chart";
            this.btnGetReferenceChart.UseVisualStyleBackColor = true;
            this.btnGetReferenceChart.Click += new System.EventHandler(this.btnGetReferenceChart_Click);
            // 
            // lblProjectID
            // 
            this.lblProjectID.AutoSize = true;
            this.lblProjectID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectID.Location = new System.Drawing.Point(9, 15);
            this.lblProjectID.Name = "lblProjectID";
            this.lblProjectID.Size = new System.Drawing.Size(98, 13);
            this.lblProjectID.TabIndex = 1;
            this.lblProjectID.Text = "Enter Project ID";
            // 
            // txtProjectID
            // 
            this.txtProjectID.Location = new System.Drawing.Point(113, 12);
            this.txtProjectID.Name = "txtProjectID";
            this.txtProjectID.Size = new System.Drawing.Size(100, 20);
            this.txtProjectID.TabIndex = 2;
            // 
            // dtGrdPatchs
            // 
            this.dtGrdPatchs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdPatchs.Location = new System.Drawing.Point(12, 66);
            this.dtGrdPatchs.Name = "dtGrdPatchs";
            this.dtGrdPatchs.Size = new System.Drawing.Size(669, 191);
            this.dtGrdPatchs.TabIndex = 3;
            // 
            // lblRefChartPath
            // 
            this.lblRefChartPath.AutoSize = true;
            this.lblRefChartPath.Location = new System.Drawing.Point(16, 38);
            this.lblRefChartPath.Name = "lblRefChartPath";
            this.lblRefChartPath.Size = new System.Drawing.Size(0, 13);
            this.lblRefChartPath.TabIndex = 5;
            // 
            // btnUploadMeasurement
            // 
            this.btnUploadMeasurement.Location = new System.Drawing.Point(12, 573);
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
            // rtbChartSetting
            // 
            this.rtbChartSetting.Location = new System.Drawing.Point(12, 292);
            this.rtbChartSetting.Name = "rtbChartSetting";
            this.rtbChartSetting.Size = new System.Drawing.Size(311, 238);
            this.rtbChartSetting.TabIndex = 8;
            this.rtbChartSetting.Text = "";
            // 
            // rtbDeviceSetting
            // 
            this.rtbDeviceSetting.Location = new System.Drawing.Point(361, 290);
            this.rtbDeviceSetting.Name = "rtbDeviceSetting";
            this.rtbDeviceSetting.Size = new System.Drawing.Size(320, 240);
            this.rtbDeviceSetting.TabIndex = 9;
            this.rtbDeviceSetting.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Chart Setting";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(364, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Device Setting";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 543);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Enter OptimizationRunID";
            // 
            // txtOptimizationRunID
            // 
            this.txtOptimizationRunID.Location = new System.Drawing.Point(161, 540);
            this.txtOptimizationRunID.Name = "txtOptimizationRunID";
            this.txtOptimizationRunID.Size = new System.Drawing.Size(100, 20);
            this.txtOptimizationRunID.TabIndex = 13;
            // 
            // btnLoadMeasurement
            // 
            this.btnLoadMeasurement.Location = new System.Drawing.Point(280, 540);
            this.btnLoadMeasurement.Name = "btnLoadMeasurement";
            this.btnLoadMeasurement.Size = new System.Drawing.Size(149, 23);
            this.btnLoadMeasurement.TabIndex = 14;
            this.btnLoadMeasurement.Text = "Load Measurements";
            this.btnLoadMeasurement.UseVisualStyleBackColor = true;
            this.btnLoadMeasurement.Click += new System.EventHandler(this.btnLoadMeasurement_Click);
            // 
            // dataGridMeasurement
            // 
            this.dataGridMeasurement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMeasurement.Location = new System.Drawing.Point(12, 602);
            this.dataGridMeasurement.Name = "dataGridMeasurement";
            this.dataGridMeasurement.Size = new System.Drawing.Size(669, 100);
            this.dataGridMeasurement.TabIndex = 15;
            // 
            // rbtnProfiler
            // 
            this.rbtnProfiler.AutoSize = true;
            this.rbtnProfiler.Checked = true;
            this.rbtnProfiler.Location = new System.Drawing.Point(222, 12);
            this.rbtnProfiler.Name = "rbtnProfiler";
            this.rbtnProfiler.Size = new System.Drawing.Size(85, 17);
            this.rbtnProfiler.TabIndex = 16;
            this.rbtnProfiler.TabStop = true;
            this.rbtnProfiler.Text = "Profiler Chart";
            this.rbtnProfiler.UseVisualStyleBackColor = true;
            // 
            // rbtnValidation
            // 
            this.rbtnValidation.AutoSize = true;
            this.rbtnValidation.Location = new System.Drawing.Point(398, 12);
            this.rbtnValidation.Name = "rbtnValidation";
            this.rbtnValidation.Size = new System.Drawing.Size(71, 17);
            this.rbtnValidation.TabIndex = 17;
            this.rbtnValidation.TabStop = true;
            this.rbtnValidation.Text = "Validation";
            this.rbtnValidation.UseVisualStyleBackColor = true;
            // 
            // rbtnOptimization
            // 
            this.rbtnOptimization.AutoSize = true;
            this.rbtnOptimization.Location = new System.Drawing.Point(310, 12);
            this.rbtnOptimization.Name = "rbtnOptimization";
            this.rbtnOptimization.Size = new System.Drawing.Size(82, 17);
            this.rbtnOptimization.TabIndex = 18;
            this.rbtnOptimization.TabStop = true;
            this.rbtnOptimization.Text = "Optimization";
            this.rbtnOptimization.UseVisualStyleBackColor = true;
            // 
            // rbtnSpot
            // 
            this.rbtnSpot.AutoSize = true;
            this.rbtnSpot.Location = new System.Drawing.Point(475, 13);
            this.rbtnSpot.Name = "rbtnSpot";
            this.rbtnSpot.Size = new System.Drawing.Size(47, 17);
            this.rbtnSpot.TabIndex = 19;
            this.rbtnSpot.TabStop = true;
            this.rbtnSpot.Text = "Spot\r\n";
            this.rbtnSpot.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 757);
            this.Controls.Add(this.rbtnSpot);
            this.Controls.Add(this.rbtnOptimization);
            this.Controls.Add(this.rbtnValidation);
            this.Controls.Add(this.rbtnProfiler);
            this.Controls.Add(this.dataGridMeasurement);
            this.Controls.Add(this.btnLoadMeasurement);
            this.Controls.Add(this.txtOptimizationRunID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbDeviceSetting);
            this.Controls.Add(this.rtbChartSetting);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMeasurement)).EndInit();
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
        private System.Windows.Forms.RichTextBox rtbChartSetting;
        private System.Windows.Forms.RichTextBox rtbDeviceSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOptimizationRunID;
        private System.Windows.Forms.Button btnLoadMeasurement;
        private System.Windows.Forms.DataGridView dataGridMeasurement;
        private System.Windows.Forms.RadioButton rbtnProfiler;
        private System.Windows.Forms.RadioButton rbtnValidation;
        private System.Windows.Forms.RadioButton rbtnOptimization;
        private System.Windows.Forms.RadioButton rbtnSpot;
    }
}

