using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace FujifilmUSA.Sync.Common
{
    public class ExportFile<T>
    {
        string FileName { get; set; }
        IList<T> lst { get; set; }
        StringBuilder header;
        StringBuilder content;

        /// <summary>
        /// Converts the list into tab seperated txt file.
        /// </summary>
        /// <param name="List"></param>
        /// <param name="FileName">The full file name including the extension.</param>
        public ExportFile(IList<T> list, string fileName)
        {
            lst = list;
            FileName = fileName;
        }


        /// <summary>
        /// Export file 
        /// </summary>
        public void ExecuteResult()
        {
            Type entType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);

            header = new StringBuilder(10 * lst.Count * properties.Count);
            content = new StringBuilder(10 * lst.Count * properties.Count);

            GetContent(properties);

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] value = encoding.GetBytes(string.Format("{0}{1}{2}", header.ToString(), Environment.NewLine, content.ToString()));
            Utilities.DownloadFile(value, FileName);
        }

        public string ExportCSV()
        {
            Type entType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);

            header = new StringBuilder(10 * lst.Count * properties.Count);
            content = new StringBuilder(10 * lst.Count * properties.Count);

            GetContent(properties);

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] value = encoding.GetBytes(string.Format("{0}{1}{2}", header.ToString(), Environment.NewLine, content.ToString()));

            string csvFolderPath = System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CsvFile/");
            if (!Directory.Exists(csvFolderPath))
                Directory.CreateDirectory(csvFolderPath);
            string filePath = csvFolderPath + FileName;
            File.WriteAllBytes(filePath, value);
            return filePath;
        }


        private void GetContent(PropertyDescriptorCollection properties)
        {
            string delementer = new System.IO.FileInfo(FileName).Extension.Equals(".txt") ? "\t" : ",";
            string strWordWapper = new System.IO.FileInfo(FileName).Extension.Equals(".txt") ? string.Empty : "\"";

            int count = 0;
            foreach (T dr in lst)
            {
                StringBuilder csvRow = new StringBuilder();

                for (int c = 0; c < properties.Count; c++)
                {
                    if ((properties[c].PropertyType).Namespace == "System")
                    {
                        object columnValue = properties[c].GetValue(dr);
                        if (columnValue == null || Convert.ToString(columnValue) == "0")
                        {
                        }
                        else
                        {
                            if (count == 0)
                            {
                                header.Append(CleanCSVString(properties[c].Name.Equals("SampleName") ? "Name" : properties[c].Name, strWordWapper));
                                header.Append(delementer);
                            }
                            csvRow.Append(CleanCSVString(columnValue.ToString(), strWordWapper));
                            csvRow.Append(delementer);
                        }
                    }
                    else
                    {
                        PropertyDescriptorCollection innerProp = TypeDescriptor.GetProperties(properties[c].PropertyType);
                        if (properties[c].GetValue(dr) != null)
                        {
                            var innerList = properties[c].GetValue(dr);
                            for (int i = 0; i < innerProp.Count; i++)
                            {
                                object columnValue = innerProp[i].GetValue(innerList);
                                if (columnValue == null || Convert.ToString(columnValue) == "0")
                                {
                                }
                                else
                                {
                                    if (count == 0)
                                    {
                                        header.Append(CleanCSVString(innerProp[i].Name, strWordWapper));
                                        header.Append(delementer);
                                    }
                                    csvRow.Append(CleanCSVString(columnValue.ToString(), strWordWapper));
                                    csvRow.Append(delementer);
                                }
                            }
                        }
                    }
                }
                content.AppendLine(csvRow.ToString());
                count++;
            }
        }
        protected string CleanCSVString(string input, string strWordWapper)
        {
            return string.Format("{0}{1}{0}", strWordWapper, input.Replace("\"", "\"\"").Replace("\r\n", " ").Replace("\r", " ").Replace("\n", ""));
        }

        ~ExportFile()
        {
        }
    }
}
