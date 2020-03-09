using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using Oris.DB_interface;
using FujiFilmDesktopApp.TaskeroColorTuner;

namespace FujiFilmDesktopApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUploadMeasurement_Click(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionEventRaised);
            try
            {
                foreach (string item in openFileDialog1.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(item);
                    Thread.Sleep(1000);
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXml(fileInfo.FullName);
                    SendDataToTaskero(dataSet);                    
                }
                txtFilePath.Text = "";
                MessageBox.Show("File uploaded successfully.");                               
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                string str = "An application error occurred. Please contact the adminstrator with the following information:\n\n";
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }
                EventLog eventLog = new EventLog()
                {
                    Source = "TaskeroColorTunerWeb"
                };
                eventLog.WriteEntry(string.Concat(str, exception.Message, "\n\nStack Trace:\n", exception.StackTrace));
            }
        }

        private static string GetXMLPath()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            int length = location.Length - 1;
            while (location.Substring(length, 1) != "\\")
            {
                length--;
            }
            string str = location.Substring(length, location.Length - length);
            str = str.Substring(0, str.Length - 4);
            string folderPath = @"D:\Docs\FujiFilm\senddataTU3\senddataTU3\SendData\senddata_example\XML\";
            return folderPath;
        }

        private static void SendDataToTaskero(DataSet DS)
        {
            ColorpathTunerClient colorpathTunerClient = null;           
            try
            {
                try
                {
                    colorpathTunerClient = new ColorpathTunerClient();                   
                    colorpathTunerClient.GetData(DS);
                }
                catch (Exception exception)
                {                   
                    EventLog.WriteEntry("IN Application Exception Create", exception.Message + "Trace" +
   exception.StackTrace, EventLogEntryType.Error, 121, short.MaxValue);
                    throw new Exception(string.Concat("Error in Module Send Data To Taskero: ", exception.ToString()));
                }
            }
            finally
            {
                colorpathTunerClient.Close();
            }
        }

        private static void UnhandledExceptionEventRaised(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception exceptionObject = (Exception)e.ExceptionObject;
                string str = "An application error occurred. Please contact the adminstrator with the following information:\n\n";
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }
                EventLog eventLog = new EventLog()
                {
                    Source = "ThreadException"
                };
                eventLog.WriteEntry(string.Concat(str, exceptionObject.Message, "\n\nStack Trace:\n", exceptionObject.StackTrace));
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                try
                {
                    MessageBox.Show("Fatal Non-UI Error", string.Concat("Fatal Non-UI Error. Could not write the error to the event log. Reason: ", exception.Message), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        public class clsCompareFileInfo : IComparer
        {
            public clsCompareFileInfo()
            {
            }

            public int Compare(object x, object y)
            {
                FileInfo fileInfo = (FileInfo)x;
                FileInfo fileInfo1 = (FileInfo)y;
                return DateTime.Compare(fileInfo.LastWriteTime, fileInfo1.LastWriteTime);
            }
        }

        public class SendData_db_context : Oris_db_context
        {           
            public SendData_db_context(string p_odbc_connection_string) : base(p_odbc_connection_string)
            {
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                txtFilePath.Text = openFileDialog1.FileName;
            }
        }
    }
}
