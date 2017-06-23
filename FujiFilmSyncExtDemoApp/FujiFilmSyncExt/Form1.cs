using FujiFilmSyncExt.Common;
using FujiFilmSyncExt.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net.Http.Formatting;
using System.IO.Compression;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

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
            try
            {
                Object r = new object();
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
                    this.Cursor = Cursors.WaitCursor;
                    listPatch = ICCHelper.readICC(dirFile);

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
                    foreach (var patch in listPatch)
                    {
                        DataRow dr = dt.NewRow();
                        dr["PatchID"] = patch.PatchID.ToString();
                        dr["ColorName"] = patch.ColorName.ToString();
                        dr["Cyan"] = Convert.ToDouble(patch.Cmyk_C).ToString("#0.00");
                        dr["Magenta"] = Convert.ToDouble(patch.Cmyk_M).ToString("#0.00");
                        dr["Yellow"] = Convert.ToDouble(patch.Cmyk_Y).ToString("#0.00");
                        dr["Black"] = Convert.ToDouble(patch.Cmyk_K).ToString("#0.00");
                        dr["L"] = Convert.ToDouble(patch.Cie_L).ToString("#0.00");
                        dr["A"] = Convert.ToDouble(patch.Cie_a).ToString("#0.00");
                        dr["B"] = Convert.ToDouble(patch.Cie_b).ToString("#0.00");
                        //dr["Colorant4"] = Convert.ToDouble(patch.colorant4).ToString("#.##");
                        dt.Rows.Add(dr);

                    }
                    dataGridColor.DataSource = dt;
                    this.Cursor = Cursors.Default;
                    wedge.listPatch = listPatch;
                    lblUserMessage.Text = "";
                    txtFilePath.Text = "";
                    txtFilePath.Visible = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void btnCreateChart_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(txtICCfilePath.Text)))
                {
                    MessageBox.Show("Please select ICC file first.");
                    return;
                }
                IccFileParams obj = new IccFileParams();                
                obj.Configurations.NumberOfPatches = 1872;
                obj.Configurations.ChartPages = 1;
                obj.Configurations.PatchWidth = 27;
                obj.Configurations.PatchHeight = 27;
                obj.Configurations.ChartBorder = 40;
                obj.Configurations.ChartSizeW = 0;
                obj.Configurations.ChartSizeH = 0;
                obj.Configurations.RowsPerChart = 24;
                obj.Configurations.ColumsPerChart = 26;
                obj.Configurations.Vgap = 2;
                obj.Configurations.Hgap = 0;
                obj.Configurations.GapLeval = 75;
                obj.Configurations.PreviousL = 0;
                obj.Configurations.ChartName = "Pantone Test Chart";
                obj.Patches = wedge.listPatch;
                //AsyncDownload(obj);
                DownloadZipFile(obj);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("File can't be downloaded. Reason: " + ex.Message);
            }
        }

        public void DownloadZipFile(IccFileParams obj)
        {
            try
            {
                //string BaseUri = "http://dev.taskero.com/Services/CPSyncPDFService/";
                string BaseUri = "http://dev.taskero.com/Services/CPSyncPDFService/";
                string methodUrl = "api/Chart/GenerateFiles";
                using (HttpClient httpClient = new HttpClient())
                {
                    this.Cursor = Cursors.WaitCursor;                    
                    StringContent content = new StringContent(JsonConvert.SerializeObject(obj), null, "application/json");
                    var result = httpClient.PostAsync(BaseUri + methodUrl, content).Result;
                    var resultString = result.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<BaseResponse<FileResponse>>(resultString);                    
                    this.Cursor = Cursors.Default;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        //WebClient client = new WebClient();
                        //client.DownloadDataCompleted += (sender, args) => client_DownloadDataCompleted(response.FileName, args);
                        //client.DownloadDataAsync(new Uri(response.FilePath));
                        //resultMessage = "File Downloaded";                        
                        if (response.Data != null)
                        {
                            lblUserMessage.Text = "Chart has been generated successfully. Please download from the path given below.";
                            txtFilePath.Text = response.Data.FilePath;
                            txtFilePath.Visible = true;
                        }
                        else
                        {
                            lblUserMessage.Text = "There is some issue generating chart, Please try again.";
                            txtFilePath.Text = "";
                            txtFilePath.Visible = false;
                        }
                    }
                    else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
                    {
                        lblUserMessage.Text = "Validation failed." + response.Error.UserMessage;
                        txtFilePath.Text = "";
                        txtFilePath.Visible = false;
                    }
                    else if (result.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        lblUserMessage.Text = "There is some issue generating chart, Please try again.";
                        txtFilePath.Text = "";
                        txtFilePath.Visible = false;
                    }
                    else if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        lblUserMessage.Text = "Incorrect service path.Please check.";
                        txtFilePath.Text = "";
                        txtFilePath.Visible = false;
                    }
                    else
                    {
                        lblUserMessage.Text = "There is some issue generating chart, Reason: " + response.Error.UserMessage;
                        txtFilePath.Text = "";
                        txtFilePath.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AsyncDownload(IccFileParams obj)
        {
            try
            {
                for (int index = 0; index < 1; index++)
                    Task.Run(() => DownloadZipFile(obj));
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        void client_DownloadDataCompleted(string fileName, DownloadDataCompletedEventArgs e)
        {
            try
            {
                string dirName = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "/ZipFiles";
                if (!Directory.Exists(dirName))
                    Directory.CreateDirectory(dirName);
                //string fullDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

                string filepath = dirName + "/" + fileName;
                File.WriteAllBytes(filepath, e.Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
