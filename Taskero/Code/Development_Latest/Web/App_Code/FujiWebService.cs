#region Using System
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
#endregion

#region Using Application
using Fuji.Common;
using Fuji.Business;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
#endregion


/// <summary>
/// Summary description for FujiWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FujiWebService : System.Web.Services.WebService
{

    public FujiWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public XmlDocument GetSyncMeasurement(Int32 PressRunID)
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncMeasurementData ObjSyncMeasurement = new CPSyncMeasurementData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncMeasurement = new DataTable();
            DataSet DS = new DataSet();
            ObjSyncMeasurement.PressRunID = PressRunID;
            DS = ObjSyncMeasurement.uspGetMeasurementByPressRunID();
            return GetXMLFromDataset(DS);
        }
        catch (Exception Exc)
        {            
            throw Exc;
        }   
    }

    [WebMethod]
    public XmlDocument DeleteSyncMeasurement(Int32 PressRunID, String SerialNo, String FileName, Int32 MeasurementID)
    {
        try
        {
            CPSyncMeasurementData ObjSyncMeasurement = new CPSyncMeasurementData();

            string FileNamePath = Server.MapPath("~") + "/UploadFiles/" + SerialNo + "/" + PressRunID.ToString() + "/" + FileName;
            ObjSyncMeasurement.MeasurementID = MeasurementID;
            ObjSyncMeasurement.uspDeleteMeasurement();
            if (File.Exists(FileNamePath))
                System.IO.File.Delete(FileNamePath);
            
            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncMeasurement = new DataTable();
            DataSet DS = new DataSet();
            ObjSyncMeasurement.PressRunID = PressRunID;
            DS = ObjSyncMeasurement.uspGetMeasurementByPressRunID();
            return GetXMLFromDataset(DS); 
        }
        catch (Exception Exc)
        {
            throw Exc;
        }


    }

    private XmlDocument GetXMLFromDataset(DataSet DSResult)
    {
        StringWriter XmlString = new StringWriter();
        XmlDocument XMLDoc = new XmlDocument();
        DSResult.WriteXml(XmlString);
        string strXML = XmlString.ToString();
        if (strXML.Contains("NewDataSet")) { strXML = strXML.Replace("NewDataSet", "ItemCollection"); }
        if (strXML.Contains("Table")) { strXML = strXML.Replace("Table", "Item"); }
        XMLDoc.LoadXml(strXML);
        return XMLDoc;
    }
    

}

