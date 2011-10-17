using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CPSync
{
    public class ParseMeasurementFile
    {


        Measurement objMeasurement = new Measurement();

        

        // string strFileContent =  System.IO.File.ReadAllText("C:\\Development\\Profiling\\Files\\Files for comparision\\for chirag\\140 pacth Spectral.txt");

        int idxStart;     //Index of the row where Patch Data Starts
        int idxEnd;       //Index of the row where Patch Data Ends  
        DataSet objDS = new DataSet();

        //First we separate the content of the file into rows using the carriage return and newline as line separator
        char[] SplitLineAt = new char[] { '\r', '\n' };

        string[] Rows;

        /// <summary>
        /// Status of the uploaded file
        /// </summary>
        public enum enumsUploadFileStatus
        {
            Success = 0,
            ErrorParsing = 1,
            InvalidFile = 2,
            NoSpectralData = 3,
            Failed = 4,
            ExceptionThrown = 5
        }

        /// <summary>
        /// Check file content for sanity, it 
        /// </summary>
        /// <param name="FileContent"></param>
        /// <returns></returns>
        public enumsUploadFileStatus ValidateFile(string FileContent)
        {
            enumsUploadFileStatus Status = enumsUploadFileStatus.Success;
            try
            {
                if (FileContent.IndexOf("BEGIN_DATA", StringComparison.Ordinal) == -1)
                {
                    return enumsUploadFileStatus.InvalidFile;
                }

                if (FileContent.IndexOf("END_DATA", StringComparison.Ordinal) == -1)
                {
                    return enumsUploadFileStatus.InvalidFile;
                }

                if ((FileContent.IndexOf("nm450", StringComparison.Ordinal) == -1) & (FileContent.IndexOf("SPECTRAL_450", StringComparison.Ordinal) == -1))
                {
                    return enumsUploadFileStatus.NoSpectralData;
                }

                return Status;
            }
            catch (Exception )
            {
                return enumsUploadFileStatus.ExceptionThrown;
            }
        }


        /// <summary>
        /// Parse the uploaded file and save in the database
        /// </summary>
        /// <param name="strFileContent"></param>
        /// <param name="filename"></param>
        /// <returns>upload status enums </returns>
        public string ParseMeasurment(int PressRunID,string strFileContent, string filename,DateTime LocalDate)
        {

          
            //http://www.codeproject.com/KB/database/DataSetFrmDelimTxt.aspx
            try
            {

                if (strFileContent.Contains("ColorPort") == true)
                {
                    strFileContent = strFileContent.Replace("SAMPLE_ID", "SAMPLE_NAME");
                }

                //Returns and newlines 
                Rows = strFileContent.Split(SplitLineAt,StringSplitOptions.RemoveEmptyEntries);

                for (int idxColFormat = 0; idxColFormat <= Rows.Length - 1; idxColFormat++)
                {
                    if (string.Compare(Rows[idxColFormat].Replace("\t", string.Empty), "BEGIN_DATA_FORMAT", false) == 0)
                    {
                        idxColFormat = idxColFormat + 1;
                        ExtractColumns(idxColFormat);
                        break; 
                    }
                }

                //Iterate through the rows and find out the start and end position of the patch data
                for (int iCount = 0; iCount <= Rows.Length - 1; iCount++)
                {
                    if (string.Compare(Rows[iCount].Replace("\t", string.Empty), "BEGIN_DATA", false) == 0)
                    {
                        idxStart = iCount + 1;
                    }
                    if (string.Compare(Rows[iCount].Replace("\t", string.Empty), "END_DATA", false) == 0)
                    {
                        idxEnd = iCount - 1;
                    }
                }

                for (int iCount = idxStart; iCount <= idxEnd; iCount++)
                {
                    ExtractSpectralData(Rows[iCount]);
                }

                //validate file by counting pathces and verifying position of the paper patch
                if ((objDS.Tables["SpectralData"].Rows.Count == 300) || (objDS.Tables["SpectralData"].Rows.Count == 150) || (objDS.Tables["SpectralData"].Rows.Count == 40) || (objDS.Tables["SpectralData"].Rows.Count == 24) )
                {
                    if (objDS.Tables["SpectralData"].Rows.Count == 300)
                    {

                        objMeasurement.MeasurementTypeID = (int)(Measurement.ChartType.p2pChart);  // measured chart is a p2p chart


                        ////check patch E1 is a paper patch
                       
                        DataColumn[] dcol = new DataColumn[1];
                        dcol[0] = objDS.Tables["SpectralData"].Columns["SAMPLE_NAME"];

                        objDS.Tables["SpectralData"].PrimaryKey = dcol;

                        DataRow drow = null;
                        drow = objDS.Tables["SpectralData"].Rows.Find("E1");
                        if (drow != null)
                        {
                            if (Convert.ToDecimal(drow["CMYK_C"]) != 0.0M & Convert.ToDecimal(drow["CMYK_M"]) != 0.0M & Convert.ToDecimal(drow["CMYK_Y"]) != 0.0M & Convert.ToDecimal(drow["CMYK_K"]) != 0.0M)
                            {

                                return "patch E1 (p2p chart) is not a paper patch";
                                //return enumsUploadFileStatus.InvalidFile;
                            }
                        }
                        else
                        {
                            return "invalid file. missing sample name in the file";
                            //return enumsUploadFileStatus.InvalidFile;
                        }

                    }
                    else if (objDS.Tables["SpectralData"].Rows.Count == 150)
                    {
                        objMeasurement.MeasurementTypeID = (int)(Measurement.ChartType.Fuji150Patch);

                        //check patch A13 is a paper patch
                        DataColumn[] dcol = new DataColumn[1];
                        dcol[0] = objDS.Tables["SpectralData"].Columns["SAMPLE_NAME"];

                        objDS.Tables["SpectralData"].PrimaryKey = dcol;

                        DataRow drow = null;
                        drow = objDS.Tables["SpectralData"].Rows.Find("A13");
                        if (drow != null)
                        {
                            if (Convert.ToDecimal(drow["CMYK_C"]) != 0.0M & Convert.ToDecimal(drow["CMYK_M"]) != 0.0M & Convert.ToDecimal(drow["CMYK_Y"]) != 0.0M & Convert.ToDecimal(drow["CMYK_K"]) != 0.0M)
                            {
                                return "patch A13 is not a paper patch in fuji 150 chart";
                                //return enumsUploadFileStatus.InvalidFile;
                            }
                        }
                        else
                        {
                            return "invalid file. missing sample name in fuji 150 patch measurement file";    
                            //return enumsUploadFileStatus.InvalidFile;
                        }
                    }
                    else if (objDS.Tables["SpectralData"].Rows.Count == 40)
                    {
                        objMeasurement.MeasurementTypeID = (int)(Measurement.ChartType.Sync40Patch);

                        //check patch A13 is a paper patch
                        DataColumn[] dcol = new DataColumn[1];
                        dcol[0] = objDS.Tables["SpectralData"].Columns["SAMPLE_NAME"];

                        objDS.Tables["SpectralData"].PrimaryKey = dcol;

                        DataRow drow = null;
                        drow = objDS.Tables["SpectralData"].Rows.Find("A1");
                        if (drow != null)
                        {
                            if (Convert.ToDecimal(drow["CMYK_C"]) != 0.0M & Convert.ToDecimal(drow["CMYK_M"]) != 0.0M & Convert.ToDecimal(drow["CMYK_Y"]) != 0.0M & Convert.ToDecimal(drow["CMYK_K"]) != 0.0M)
                            {
                                return "patch A1 is not a paper patch in sync 40 chart";
                                //return enumsUploadFileStatus.InvalidFile;
                            }
                        }
                        else
                        {
                            return "invalid file. missing sample name in sync 40 measurement file";
                            //return enumsUploadFileStatus.InvalidFile;
                        }
                    }
                    else if (objDS.Tables["SpectralData"].Rows.Count == 24)
                    {
                        objMeasurement.MeasurementTypeID = (int)(Measurement.ChartType.ISO24Patch);

                        //check patch A13 is a paper patch
                        DataColumn[] dcol = new DataColumn[1];
                        dcol[0] = objDS.Tables["SpectralData"].Columns["SAMPLE_NAME"];

                        objDS.Tables["SpectralData"].PrimaryKey = dcol;

                        DataRow drow = null;
                        drow = objDS.Tables["SpectralData"].Rows.Find("A1");
                        if (drow != null)
                        {
                            if (Convert.ToDecimal(drow["CMYK_C"]) != 0.0M & Convert.ToDecimal(drow["CMYK_M"]) != 0.0M & Convert.ToDecimal(drow["CMYK_Y"]) != 0.0M & Convert.ToDecimal(drow["CMYK_K"]) != 0.0M)
                            {
                                return "patch A1 is not a paper patch in ISO 24 chart";
                                //return enumsUploadFileStatus.InvalidFile;
                            }
                        }
                        else
                        {
                            return "invalid file. missing sample name in ISO 24 patch measurement file";
                           
                        }
                    }
                   
                }
                else
                {
            
                    return "invalid measurement file - only p2p, fuji150, sync 40 or ISO 24 patch charts can be measured ";
                  
                }



                foreach (DataRow Row in objDS.Tables["SpectralData"].Rows)
                {
                    MeasuredPatch objMeasuredPatch = new MeasuredPatch();

                    if (objDS.Tables["SpectralData"].Columns.Contains("SAMPLE_ID") == true)
                    {
                        objMeasuredPatch.SampleID = Convert.ToInt32(Row["SampleID"]);
                    }

                    objMeasuredPatch.CMYK_C = Convert.ToDecimal(Row["CMYK_C"]);
                    objMeasuredPatch.CMYK_M = Convert.ToDecimal(Row["CMYK_M"]);
                    objMeasuredPatch.CMYK_Y = Convert.ToDecimal(Row["CMYK_Y"]);
                    objMeasuredPatch.CMYK_K = Convert.ToDecimal(Row["CMYK_K"]);


                    if (objDS.Tables["SpectralData"].Columns.Contains("SAMPLE_NAME") == true)
                    {
                        objMeasuredPatch.SampleName = Convert.ToString(Row["SAMPLE_NAME"]);
                    }

                    if (objDS.Tables["SpectralData"].Columns.Contains("nm340") == true)
                    {
                        objMeasuredPatch.Spectral340 = Convert.ToDouble(Row["nm340"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_340") == true)
                    {
                        objMeasuredPatch.Spectral340 = Convert.ToDouble(Row["SPECTRAL_340"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_340, objMeasuredPatch.Spectral340);



                    if (objDS.Tables["SpectralData"].Columns.Contains("nm350") == true)
                    {
                        objMeasuredPatch.Spectral350 = Convert.ToDouble(Row["nm350"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_350") == true)
                    {
                        objMeasuredPatch.Spectral350 = Convert.ToDouble(Row["SPECTRAL_350"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_350, objMeasuredPatch.Spectral350);


                    if (objDS.Tables["SpectralData"].Columns.Contains("nm360") == true)
                    {
                        objMeasuredPatch.Spectral360 = Convert.ToDouble(Row["nm360"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_360") == true)
                    {
                        objMeasuredPatch.Spectral360 = Convert.ToDouble(Row["SPECTRAL_360"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_360,objMeasuredPatch.Spectral360);


                    if (objDS.Tables["SpectralData"].Columns.Contains("nm370") == true)
                    {
                        objMeasuredPatch.Spectral370 = Convert.ToDouble(Row["nm370"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_370") == true)
                    {
                        objMeasuredPatch.Spectral370 = Convert.ToDouble(Row["SPECTRAL_370"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_370, objMeasuredPatch.Spectral370);


                    if (objDS.Tables["SpectralData"].Columns.Contains("nm380") == true)
                    {
                        objMeasuredPatch.Spectral380 = Convert.ToDouble(Row["nm380"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_380") == true)
                    {
                        objMeasuredPatch.Spectral380 = Convert.ToDouble(Row["SPECTRAL_380"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_380,objMeasuredPatch.Spectral380);


                    if (objDS.Tables["SpectralData"].Columns.Contains("nm390") == true)
                    {
                        objMeasuredPatch.Spectral390 = Convert.ToDouble(Row["nm390"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_390") == true)
                    {
                        objMeasuredPatch.Spectral390 = Convert.ToDouble(Row["SPECTRAL_390"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_390, objMeasuredPatch.Spectral390);


                    if (objDS.Tables["SpectralData"].Columns.Contains("nm400") == true)
                    {
                        objMeasuredPatch.Spectral400 = Convert.ToDouble(Row["nm400"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_400") == true)
                    {
                        objMeasuredPatch.Spectral400 = Convert.ToDouble(Row["SPECTRAL_400"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_400, objMeasuredPatch.Spectral400);


                    if (objDS.Tables["SpectralData"].Columns.Contains("nm410") == true)
                    {
                        objMeasuredPatch.Spectral410 = Convert.ToDouble(Row["nm410"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_410") == true)
                    {
                        objMeasuredPatch.Spectral410 = Convert.ToDouble(Row["SPECTRAL_410"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_410,objMeasuredPatch.Spectral410);


                    if (objDS.Tables["SpectralData"].Columns.Contains("nm420") == true)
                    {
                        objMeasuredPatch.Spectral420 = Convert.ToDouble(Row["nm420"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_420") == true)
                    {
                        objMeasuredPatch.Spectral420 = Convert.ToDouble(Row["SPECTRAL_420"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_420,objMeasuredPatch.Spectral420);


                    if (objDS.Tables["SpectralData"].Columns.Contains("nm430") == true)
                    {
                        objMeasuredPatch.Spectral430 = Convert.ToDouble(Row["nm430"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_430") == true)
                    {
                        objMeasuredPatch.Spectral430 = Convert.ToDouble(Row["SPECTRAL_430"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_430,objMeasuredPatch.Spectral430);


                    if (objDS.Tables["SpectralData"].Columns.Contains("nm440") == true)
                    {
                        objMeasuredPatch.Spectral440 = Convert.ToDouble(Row["nm440"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_440") == true)
                    {
                        objMeasuredPatch.Spectral440 = Convert.ToDouble(Row["SPECTRAL_440"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_440,objMeasuredPatch.Spectral440);

                   
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm450") == true)
                    {
                        objMeasuredPatch.Spectral450 = Convert.ToDouble(Row["nm450"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_450") == true)
                    {
                        objMeasuredPatch.Spectral450 = Convert.ToDouble(Row["SPECTRAL_450"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_450,objMeasuredPatch.Spectral450);



                    if (objDS.Tables["SpectralData"].Columns.Contains("nm460") == true)
                    {
                        objMeasuredPatch.Spectral460 = Convert.ToDouble(Row["nm460"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_460") == true)
                    {
                        objMeasuredPatch.Spectral460 = Convert.ToDouble(Row["SPECTRAL_460"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_460,objMeasuredPatch.Spectral460);


                    if (objDS.Tables["SpectralData"].Columns.Contains("nm470") == true)
                    {
                        objMeasuredPatch.Spectral470 = Convert.ToDouble(Row["nm470"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_470") == true)
                    {
                        objMeasuredPatch.Spectral470 = Convert.ToDouble(Row["SPECTRAL_470"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_470,objMeasuredPatch.Spectral470);

                    //480    
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm480") == true)
                    {
                        objMeasuredPatch.Spectral480 = Convert.ToDouble(Row["nm480"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_480") == true)
                    {
                        objMeasuredPatch.Spectral480 = Convert.ToDouble(Row["SPECTRAL_480"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_480, objMeasuredPatch.Spectral480);

                    //490    
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm490") == true)
                    {
                        objMeasuredPatch.Spectral490 = Convert.ToDouble(Row["nm490"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_490") == true)
                    {
                        objMeasuredPatch.Spectral490 = Convert.ToDouble(Row["SPECTRAL_490"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_490,objMeasuredPatch.Spectral490);

                    //500    
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm500") == true)
                    {
                        objMeasuredPatch.Spectral500 = Convert.ToDouble(Row["nm500"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_500") == true)
                    {
                        objMeasuredPatch.Spectral500 = Convert.ToDouble(Row["SPECTRAL_500"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_500,objMeasuredPatch.Spectral500);

                    //510   
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm510") == true)
                    {
                        objMeasuredPatch.Spectral510 = Convert.ToDouble(Row["nm510"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_510") == true)
                    {
                        objMeasuredPatch.Spectral510 = Convert.ToDouble(Row["SPECTRAL_510"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_510,objMeasuredPatch.Spectral510);


                    //520   
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm520") == true)
                    {
                        objMeasuredPatch.Spectral520 = Convert.ToDouble(Row["nm520"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_520") == true)
                    {
                        objMeasuredPatch.Spectral520 = Convert.ToDouble(Row["SPECTRAL_520"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_520, objMeasuredPatch.Spectral520);


                    //530   
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm530") == true)
                    {
                        objMeasuredPatch.Spectral530 = Convert.ToDouble(Row["nm530"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_530") == true)
                    {
                        objMeasuredPatch.Spectral530 = Convert.ToDouble(Row["SPECTRAL_530"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_530, objMeasuredPatch.Spectral530);


                    //540   
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm540") == true)
                    {
                        objMeasuredPatch.Spectral540 = Convert.ToDouble(Row["nm540"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_540") == true)
                    {
                        objMeasuredPatch.Spectral540 = Convert.ToDouble(Row["SPECTRAL_540"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_540, objMeasuredPatch.Spectral540);


                    //550   
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm550") == true)
                    {
                        objMeasuredPatch.Spectral550 = Convert.ToDouble(Row["nm550"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_550") == true)
                    {
                        objMeasuredPatch.Spectral550 = Convert.ToDouble(Row["SPECTRAL_550"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_550,objMeasuredPatch.Spectral550);


                    //560
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm560") == true)
                    {
                        objMeasuredPatch.Spectral560 = Convert.ToDouble(Row["nm560"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_560") == true)
                    {
                        objMeasuredPatch.Spectral560 = Convert.ToDouble(Row["SPECTRAL_560"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_560,objMeasuredPatch.Spectral560);


                    //570
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm570") == true)
                    {
                        objMeasuredPatch.Spectral570 = Convert.ToDouble(Row["nm570"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_570") == true)
                    {
                        objMeasuredPatch.Spectral570 = Convert.ToDouble(Row["SPECTRAL_570"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_570, objMeasuredPatch.Spectral570);


                    //580
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm580") == true)
                    {
                        objMeasuredPatch.Spectral580 = Convert.ToDouble(Row["nm580"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_580") == true)
                    {
                        objMeasuredPatch.Spectral580 = Convert.ToDouble(Row["SPECTRAL_580"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_580, objMeasuredPatch.Spectral580);


                    //590
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm590") == true)
                    {
                        objMeasuredPatch.Spectral590 = Convert.ToDouble(Row["nm590"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_590") == true)
                    {
                        objMeasuredPatch.Spectral590 = Convert.ToDouble(Row["SPECTRAL_590"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_590,objMeasuredPatch.Spectral590);


                    //600
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm600") == true)
                    {
                        objMeasuredPatch.Spectral600 = Convert.ToDouble(Row["nm600"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_600") == true)
                    {
                        objMeasuredPatch.Spectral600 = Convert.ToDouble(Row["SPECTRAL_600"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_600, objMeasuredPatch.Spectral600);


                    //610
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm610") == true)
                    {
                        objMeasuredPatch.Spectral610 = Convert.ToDouble(Row["nm610"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_610") == true)
                    {
                        objMeasuredPatch.Spectral610 = Convert.ToDouble(Row["SPECTRAL_610"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_610,objMeasuredPatch.Spectral610);


                    //620
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm620") == true)
                    {
                        objMeasuredPatch.Spectral620 = Convert.ToDouble(Row["nm620"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_620") == true)
                    {
                        objMeasuredPatch.Spectral620 = Convert.ToDouble(Row["SPECTRAL_620"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_620, objMeasuredPatch.Spectral620);


                    //630
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm630") == true)
                    {
                        objMeasuredPatch.Spectral630 = Convert.ToDouble(Row["nm630"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_630") == true)
                    {
                        objMeasuredPatch.Spectral630 = Convert.ToDouble(Row["SPECTRAL_630"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_630,objMeasuredPatch.Spectral630);


                    //640
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm640") == true)
                    {
                        objMeasuredPatch.Spectral640 = Convert.ToDouble(Row["nm640"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_640") == true)
                    {
                        objMeasuredPatch.Spectral640 = Convert.ToDouble(Row["SPECTRAL_640"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_640,objMeasuredPatch.Spectral640);


                    //650
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm650") == true)
                    {
                        objMeasuredPatch.Spectral650 = Convert.ToDouble(Row["nm650"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_650") == true)
                    {
                        objMeasuredPatch.Spectral650 = Convert.ToDouble(Row["SPECTRAL_650"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_650,objMeasuredPatch.Spectral650);


                    //660
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm660") == true)
                    {
                        objMeasuredPatch.Spectral660 = Convert.ToDouble(Row["nm660"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_660") == true)
                    {
                        objMeasuredPatch.Spectral660 = Convert.ToDouble(Row["SPECTRAL_660"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_660,objMeasuredPatch.Spectral660);


                    //670
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm670") == true)
                    {
                        objMeasuredPatch.Spectral670 = Convert.ToDouble(Row["nm670"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_670") == true)
                    {
                        objMeasuredPatch.Spectral670 = Convert.ToDouble(Row["SPECTRAL_670"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_670, objMeasuredPatch.Spectral670);


                    //680
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm680") == true)
                    {
                        objMeasuredPatch.Spectral680 = Convert.ToDouble(Row["nm680"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_680") == true)
                    {
                        objMeasuredPatch.Spectral680 = Convert.ToDouble(Row["SPECTRAL_680"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_680, objMeasuredPatch.Spectral680);


                    //690
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm690") == true)
                    {
                        objMeasuredPatch.Spectral690 = Convert.ToDouble(Row["nm690"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_690") == true)
                    {
                        objMeasuredPatch.Spectral690 = Convert.ToDouble(Row["SPECTRAL_690"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_690, objMeasuredPatch.Spectral690);


                    //700
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm700") == true)
                    {
                        objMeasuredPatch.Spectral700 = Convert.ToDouble(Row["nm700"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_700") == true)
                    {
                        objMeasuredPatch.Spectral700 = Convert.ToDouble(Row["SPECTRAL_700"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_700,objMeasuredPatch.Spectral700);


                    //710
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm710") == true)
                    {
                        objMeasuredPatch.Spectral710 = Convert.ToDouble(Row["nm710"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_710") == true)
                    {
                        objMeasuredPatch.Spectral710 = Convert.ToDouble(Row["SPECTRAL_710"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_710,objMeasuredPatch.Spectral710);

                
                    //720
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm720") == true)
                    {
                        objMeasuredPatch.Spectral720 = Convert.ToDouble(Row["nm720"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_720") == true)
                    {
                        objMeasuredPatch.Spectral720 = Convert.ToDouble(Row["SPECTRAL_700"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_720,objMeasuredPatch.Spectral720);


                    //730
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm730") == true)
                    {
                        objMeasuredPatch.Spectral730 = Convert.ToDouble(Row["nm730"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_730") == true)
                    {
                        objMeasuredPatch.Spectral730 = Convert.ToDouble(Row["SPECTRAL_730"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_730,objMeasuredPatch.Spectral730);


                    //740
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm740") == true)
                    {
                        objMeasuredPatch.Spectral740 = Convert.ToDouble(Row["nm740"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_740") == true)
                    {
                        objMeasuredPatch.Spectral740 = Convert.ToDouble(Row["SPECTRAL_740"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_740, objMeasuredPatch.Spectral740);


                    //750
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm750") == true)
                    {
                        objMeasuredPatch.Spectral750 = Convert.ToDouble(Row["nm750"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_750") == true)
                    {
                        objMeasuredPatch.Spectral750 = Convert.ToDouble(Row["SPECTRAL_750"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_750,objMeasuredPatch.Spectral750);


                    //760
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm760") == true)
                    {
                        objMeasuredPatch.Spectral760 = Convert.ToDouble(Row["nm760"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_760") == true)
                    {
                        objMeasuredPatch.Spectral760 = Convert.ToDouble(Row["SPECTRAL_760"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_760, objMeasuredPatch.Spectral760);


                    //770
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm770") == true)
                    {
                        objMeasuredPatch.Spectral770 = Convert.ToDouble(Row["nm770"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_770") == true)
                    {
                        objMeasuredPatch.Spectral770 = Convert.ToDouble(Row["SPECTRAL_770"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_770,objMeasuredPatch.Spectral770);


                    //780
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm780") == true)
                    {
                        objMeasuredPatch.Spectral780 = Convert.ToDouble(Row["nm780"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_780") == true)
                    {
                        objMeasuredPatch.Spectral780 = Convert.ToDouble(Row["SPECTRAL_780"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_780, objMeasuredPatch.Spectral780);


                    //790
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm790") == true)
                    {
                        objMeasuredPatch.Spectral790 = Convert.ToDouble(Row["nm790"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_790") == true)
                    {
                        objMeasuredPatch.Spectral790 = Convert.ToDouble(Row["SPECTRAL_790"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_790,objMeasuredPatch.Spectral790);


                    //800
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm800") == true)
                    {
                        objMeasuredPatch.Spectral800 = Convert.ToDouble(Row["nm800"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_800") == true)
                    {
                        objMeasuredPatch.Spectral800 = Convert.ToDouble(Row["SPECTRAL_800"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_800, objMeasuredPatch.Spectral800);


                    //810
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm810") == true)
                    {
                        objMeasuredPatch.Spectral810 = Convert.ToDouble(Row["nm810"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_810") == true)
                    {
                        objMeasuredPatch.Spectral810 = Convert.ToDouble(Row["SPECTRAL_810"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_810, objMeasuredPatch.Spectral810);


                    //820
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm820") == true)
                    {
                        objMeasuredPatch.Spectral820 = Convert.ToDouble(Row["nm820"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_820") == true)
                    {
                        objMeasuredPatch.Spectral820 = Convert.ToDouble(Row["SPECTRAL_820"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)MeasuredPatch.Spectrum.SPECTRAL_820,objMeasuredPatch.Spectral820);


                    //830
                    if (objDS.Tables["SpectralData"].Columns.Contains("nm830") == true)
                    {
                        objMeasuredPatch.Spectral830 = Convert.ToDouble(Row["nm830"]);
                    }
                    else if (objDS.Tables["SpectralData"].Columns.Contains("SPECTRAL_830") == true)
                    {
                        objMeasuredPatch.Spectral830 = Convert.ToDouble(Row["SPECTRAL_830"]);
                    }
                    objMeasuredPatch.SetPatchSpectrum((int)(MeasuredPatch.Spectrum.SPECTRAL_830),objMeasuredPatch.Spectral830);


                    /*** Status T Density Only it used to be in past for ISO statusE density **/
                    //if ( objMeasurement.MeasurementTypeID == (int)(Measurement.ChartType.ISO24Patch) )
                    //{
                    //    objMeasuredPatch.Density = ColorMath.CalcStatusEDensity(objMeasuredPatch.GetPatchSpectrum());
                    //}
                    //else
                    //{
                    //    objMeasuredPatch.Density = ColorMath.CalcStatusTDensity(objMeasuredPatch.GetPatchSpectrum());
                   // }
                    objMeasuredPatch.Density = ColorMath.CalcStatusTDensity(objMeasuredPatch.GetPatchSpectrum());
                    

                    objMeasuredPatch.XYZ = ColorMath.CalcCIEXYZ(objMeasuredPatch.GetPatchSpectrum());
                    if (objMeasuredPatch.XYZ != null)
                    {
                        objMeasuredPatch.LAB = ColorMath.XYZtoLab(objMeasuredPatch.XYZ);
                    }

                   objMeasurement.MeasuredPatches.Add(objMeasuredPatch);

                }

                objMeasurement.MeasurementFile = strFileContent;
                objMeasurement.PressRunID = PressRunID;
                objMeasurement.UploadDateTime = LocalDate;
                objMeasurement.FileName = filename;

                objMeasurement.Save();
                objMeasurement.SaveDotGain();

                if (objMeasurement.MeasurementTypeID != (int)(Measurement.ChartType.ISO24Patch))
                objMeasurement.SaveΔF();

                GetCurve obj = new GetCurve(objMeasurement.MeasurementID.ToString(), objMeasurement.PressRunID);
                obj.GetG7NPDC(objMeasurement);
                
                
                //System.Diagnostics.Debugger.Break();

                return "Success";
                //return enumsUploadFileStatus.Success;
            }
            catch (Exception exc)
            {

                //to do if there is an error remove measurement from the database
                if (objMeasurement.MeasurementID != 0)
                {
                    objMeasurement.DeleteMeasurement();
                }

                return exc.ToString();

                      
            }
        }



        /// <summary>
        /// Extract Data From the Measurement File, and load them into the dataset
        /// </summary>
        /// <param name="CurrentRow"></param>
        /// <remarks></remarks>
        private void ExtractSpectralData(string CurrentRow)
        {

            try
            {
                char[] SplitFieldsAt = { '\t' };  //Tab
                string[] Items = CurrentRow.Split(SplitFieldsAt, StringSplitOptions.RemoveEmptyEntries);

                objDS.Tables["SpectralData"].Rows.Add(Items);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        /// <summary>
        /// create dataset with columns (based on sample data_format 
        /// line between BEGIN_DATA_FORMAT and END_DATA_FORMAT
        /// </summary>
        /// <param name="idxColFormat"></param>
        private void ExtractColumns(int idxColFormat)
        {
            try
            {
                char[] SplitFieldsAt = { '\t' };  //Tab
                string[] columns = Rows[idxColFormat].Split(SplitFieldsAt);

                objDS.Tables.Add("SpectralData");

                foreach (string col in columns)
                {

                    bool added = false;
                    string next = "";
                    int i = 0;
                    while (!added)
                    {
                        //Build the column name and remove any unwanted characters.
                        string columnname = col + next;
                        columnname = columnname.Replace("#", "");
                        columnname = columnname.Replace("'", "");
                        columnname = columnname.Replace("&", "");

                        //See if the column already exists
                        if (!objDS.Tables["SpectralData"].Columns.Contains(columnname))
                        {
                            //if it doesn't then we add it here and mark it as added
                            objDS.Tables["SpectralData"].Columns.Add(columnname);
                            added = true;
                        }
                        else
                        {
                            //if it did exist then we increment the sequencer and try again.
                            System.Math.Max(System.Threading.Interlocked.Increment(ref i), i - 1);
                            next = "_" + i.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        

    }
  
    
}
