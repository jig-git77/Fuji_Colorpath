using FujiFilmSyncExt.Common;
using FujiFilmSyncExt.Helper;
using FujiFilmSyncExt.Utill;
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

namespace FujiFilmSyncExt
{
    public partial class Form1 : Form
    {
        Wedge wedge = new Wedge();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DirectoryInfo dirFile;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Open File Dialog";
            //openDialog.InitialDirectory = "C:\";
            openDialog.Filter = "ICC files (*.icc)|*.icc";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if (openDialog.FileName == null)
                    return;
                dirFile = new DirectoryInfo(openDialog.FileName);
                txtICCfilePath.Text = dirFile.FullName;
                List<Patch> listPatch = new List<Patch>();

                listPatch = ICCHelper.readICC(dirFile);

                //openDialog.FilterIndex = 2;
                //openDialog.RestoreDirectory = true;            
                //Directory fileDir;

                DataTable dt = new DataTable();
                dt.Columns.Add("PatchID", typeof(string));
                dt.Columns.Add("ColorName", typeof(string));
                dt.Columns.Add("Cyan", typeof(string));
                dt.Columns.Add("Magenta", typeof(string));
                dt.Columns.Add("Yellow", typeof(string));
                dt.Columns.Add("Black", typeof(string));
                dt.Columns.Add("L", typeof(string));
                dt.Columns.Add("A", typeof(string));
                dt.Columns.Add("B", typeof(string));
                //dt.Columns.Add("Colorant4", typeof(string));
                foreach (var patch in listPatch)
                {
                    DataRow dr = dt.NewRow();
                    dr["PatchID"] = patch.PatchID.ToString();
                    dr["ColorName"] = patch.ColorName.ToString();
                    dr["Cyan"] = Convert.ToDouble(patch.cie_L).ToString("#0.00");
                    dr["Magenta"] = Convert.ToDouble(patch.cie_a).ToString("#0.00");
                    dr["Yellow"] = Convert.ToDouble(patch.cie_b).ToString("#0.00");
                    dr["Black"] = Convert.ToDouble(patch.cmyk_K).ToString("#0.00");
                    dr["L"] = Convert.ToDouble(patch.cmyk_C).ToString("#0.00");
                    dr["A"] = Convert.ToDouble(patch.cmyk_M).ToString("#0.00");
                    dr["B"] = Convert.ToDouble(patch.cmyk_Y).ToString("#0.00");
                    //dr["Colorant4"] = Convert.ToDouble(patch.colorant4).ToString("#.##");
                    dt.Rows.Add(dr);
                }
                dataGridColor.DataSource = dt;
                wedge.listPatch = listPatch;
            }
        }

        private void btnCreateChart_Click(object sender, EventArgs e)
        {
            if (wedge.listPatch.Count == 0)
            {
                
                MessageBox.Show("Please select icc file");
                return;
            }
                
            ChartUtill utill = new ChartUtill();
            Bitmap image = utill.GenerateColorListChart(wedge);
            pictureBox.Image = image;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save PDF Chart";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dirFile = null;
                if (saveFileDialog.FileName == null)
                    return;

                dirFile = new DirectoryInfo(saveFileDialog.FileName);
                if (dirFile == null)
                {

                    //MessageBox("Please select path",);
                }
                PDFConverter pdfConv = new PDFConverter();
                pdfConv.IT_MemoryBufferSize = 4800000;
                pdfConv.IT_PhotometricInterpretation = "CMYK";
                pdfConv.IT_ResolutionUnit = "cm";
                pdfConv.IT_SaveAlphaChannel = false;
                pdfConv.IT_XResolutionNumerator = 30;
                pdfConv.IT_XResolutionDenominator = 1;
                pdfConv.IT_YResolutionNumerator = 30;
                pdfConv.IT_YResolutionDenominator = 1;

                pdfConv.SaveAsPDF(dirFile, image);
            }
        }
    }
}
