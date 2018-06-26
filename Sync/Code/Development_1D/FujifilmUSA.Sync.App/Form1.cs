using FujifilmUSA.Sync.App.Util;
using FujifilmUSA.Sync.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            lblRefChartPath.Text = "http://localhost/FujifilmUSA.Sync.PluginService/PluginData.svc/GetReferenceChart/";

            string projectID = txtProjectID.Text;
            var obj = WebCommon.GetReferenceChart(projectID, "1", "false");
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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnUploadMeasurement_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter =
            "JSON Files (*.json)|*.json|" +
            "Text Files (*.txt)|*.txt|" +
            "All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileContent= File.ReadAllText(openFileDialog1.FileName);
                Measurement obj = JsonConvert.DeserializeObject<Measurement>(fileContent);
                bool result =  WebCommon.SaveMeasurement(obj);
                if (result)
                {
                    lblMessage.Text = "Measurement uploaded successfully.";
                }
            }            

        }        
    }
}
