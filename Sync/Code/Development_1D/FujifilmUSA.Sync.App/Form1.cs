using FujifilmUSA.Sync.App.Util;
using FujifilmUSA.Sync.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FujifilmUSA.Sync.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnGetReferenceChart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProjectID.Text))
            {
                MessageBox.Show("Plese Enter Project ID");
                return;
            }

            

            string projectID = txtProjectID.Text;
            bool isSpotProject = rbtnSpot.Checked;
            string chartType = "1";
            if (rbtnOptimization.Checked)
                chartType = "2";
            else if (rbtnValidation.Checked)
                chartType = "3";
            

            var obj = WebCommon.GetReferenceChart(projectID, chartType, isSpotProject.ToString());
            dtGrdPatchs.DataSource = null;
            dtGrdPatchs.AutoGenerateColumns = false;
            dtGrdPatchs.ColumnCount = 6;

            dtGrdPatchs.Columns[0].Name = "SampleId";
            dtGrdPatchs.Columns[0].HeaderText = "SampleId";
            dtGrdPatchs.Columns[0].DataPropertyName = "SampleId";

            dtGrdPatchs.Columns[1].Name = "SampleName";
            dtGrdPatchs.Columns[1].HeaderText = "SampleName";
            dtGrdPatchs.Columns[1].DataPropertyName = "SampleName";

            dtGrdPatchs.Columns[2].Name = "CmykC";
            dtGrdPatchs.Columns[2].HeaderText = "CmykC";
            dtGrdPatchs.Columns[2].DataPropertyName = "CmykC";

            dtGrdPatchs.Columns[3].Name = "CmykM";
            dtGrdPatchs.Columns[3].HeaderText = "CmykM";
            dtGrdPatchs.Columns[3].DataPropertyName = "CmykM";

            dtGrdPatchs.Columns[4].Name = "CmykY";
            dtGrdPatchs.Columns[4].HeaderText = "CmykY";
            dtGrdPatchs.Columns[4].DataPropertyName = "CmykY";

            dtGrdPatchs.Columns[5].Name = "CmykK";
            dtGrdPatchs.Columns[5].HeaderText = "CmykK";
            dtGrdPatchs.Columns[5].DataPropertyName = "CmykK";

            dtGrdPatchs.DataSource = obj.ReferenceCharts;

            rtbChartSetting.Text = JsonConvert.SerializeObject(obj.ChartSetting, Formatting.Indented);
            rtbDeviceSetting.Text = JsonConvert.SerializeObject(obj.DeviceSetting, Formatting.Indented);

            lblRefChartPath.Text = ConfigurationManager.AppSettings["PluginServicePath"].ToString() + "/api/Plugin/GetReferenceChart?projectId=" + projectID + "&referenceChartDisplayType=" + chartType + "&IsSpotProjectType=" + isSpotProject.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnUploadMeasurement_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOptimizationRunID.Text))
            {
                MessageBox.Show("Please enter OptimizationRunID of ProjectID " + txtProjectID.Text);
                return;
            }
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter =
            "JSON Files (*.json)|*.json|" +
            "Text Files (*.txt)|*.txt|" +
            "All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileContent = File.ReadAllText(openFileDialog1.FileName);
                Measurement obj = JsonConvert.DeserializeObject<Measurement>(fileContent);
                obj.ProjectId = Convert.ToInt32(txtProjectID.Text);
                obj.OptimizeRunId = Convert.ToInt32(txtOptimizationRunID.Text);
                bool result = WebCommon.SaveMeasurement(obj);
                if (result)
                {
                    //lblMessage.Text = "Measurement uploaded successfully.";
                    BindMeasurements();
                    MessageBox.Show("Measurement uploaded successfully.");
                }
                else
                {
                    MessageBox.Show("Measurement upload unsuccessful.");
                }
            }

        }

        private void btnLoadMeasurement_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOptimizationRunID.Text))
            {
                MessageBox.Show("Please enter OptimizationRunID.");
                return;
            }
            BindMeasurements();
        }

        private void dataGridMeasurement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridMeasurement.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (dataGridMeasurement.Rows.Count != e.RowIndex)
                {
                    string measurementID = dataGridMeasurement.Rows[e.RowIndex].Cells[0].Value.ToString();
                    bool result = WebCommon.DeleteMeasurement(measurementID);
                    if (result)
                    {
                        BindMeasurements();
                        MessageBox.Show("Measurement deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Measurement delete uncsuccessful.");
                    }
                }
            }
        }

        void BindMeasurements()
        {

            List<Measurement> measurements = new List<Measurement>();
            measurements = WebCommon.GetMeasurementByOptimizationRun(txtOptimizationRunID.Text);
            dataGridMeasurement.DataSource = null;
            dataGridMeasurement.AutoGenerateColumns = false;
            dataGridMeasurement.ColumnCount = 3;

            dataGridMeasurement.Columns[0].Name = "MeasurementId";
            dataGridMeasurement.Columns[0].HeaderText = "MeasurementId";
            dataGridMeasurement.Columns[0].DataPropertyName = "MeasurementId";

            dataGridMeasurement.Columns[1].Name = "MeasurementName";
            dataGridMeasurement.Columns[1].HeaderText = "Measurement Name";
            dataGridMeasurement.Columns[1].DataPropertyName = "MeasurementName";

            dataGridMeasurement.Columns[2].Name = "UploadDate";
            dataGridMeasurement.Columns[2].HeaderText = "Upload Date";
            dataGridMeasurement.Columns[2].DataPropertyName = "UploadDate";


            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.Text = "Delete";
            deleteColumn.UseColumnTextForButtonValue = true;
            dataGridMeasurement.Columns.Add(deleteColumn);
            dataGridMeasurement.AutoSize = true;
            dataGridMeasurement.AllowUserToAddRows = false;
            dataGridMeasurement.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridMeasurement.CellClick += new DataGridViewCellEventHandler(dataGridMeasurement_CellClick);
            dataGridMeasurement.DataSource = measurements;
        }

    }
}
