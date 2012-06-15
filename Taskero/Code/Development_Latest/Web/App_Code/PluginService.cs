using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel.Web;
using System.Net;

 
 
namespace MeasurementPlugInSvc
{

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PluginService" in code, svc and config file together.
//By switching WCF services to ASP.NET compatibility mode, you ensure that .svc requests are treated identically to .asmx requests 
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class PluginService : IPluginService
{
    public void SaveMeasurement(Measurement objMeasurement)
    {

        try
        {
            Save(objMeasurement);
        }
        catch (Exception ex)
        {
            //convert incoming object to json so it can be send as an attachment Ref:http://pietschsoft.com/post/2008/02/NET-35-JSON-Serialization-using-the-DataContractJsonSerializer.aspx
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(objMeasurement.GetType());
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            serializer.WriteObject(ms,objMeasurement);
            string json = Encoding.Default.GetString(ms.ToArray());

            //this overloaded method sends attachment (by passing object)
            Fuji.Common.WebMails.SendErrorEmailWithAttachment(ex, json);
            ErrorData errorData = new ErrorData("Error:", "There was an error while saving your measurment...");
            throw new WebFaultException<ErrorData>(errorData, System.Net.HttpStatusCode.InternalServerError);

        }

    }

    public ReferenceChart GetReferenceChart(int ProjectID)//ChartTypeID)
    {
        ReferenceChart RefChart = new ReferenceChart();
        try
        {
            if (ProjectID == -1)
                throw new Exception("Invalid Project");

            RefChart.ProjectID = ProjectID;
            RefChart.GetChartData();
        }
        catch (Exception ex)
        {
            //convert incoming object to json so it can be send as an attachment Ref:http://pietschsoft.com/post/2008/02/NET-35-JSON-Serialization-using-the-DataContractJsonSerializer.aspx
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(ProjectID.GetType());
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            serializer.WriteObject(ms, ProjectID.ToString());
            string json = Encoding.Default.GetString(ms.ToArray());

            Fuji.Common.WebMails.SendErrorEmailWithAttachment(ex, json);
        
            ErrorData errorData = new ErrorData("Error in GetReferenceChart", ex.Message);
            throw new WebFaultException<ErrorData>(errorData, System.Net.HttpStatusCode.InternalServerError);
        }

        return RefChart;
    }

    /// <summary>
    /// Gets the status of the service
    /// </summary>
    /// <returns></returns>
    public string GetStatus()
    {
        return "Ready";
    }




    /// <summary>
    /// Save Measurement into the Database
    /// </summary>
    /// <param name="objMeasurement"></param>
    private void Save(Measurement objMeasurement)
    {
 
       try
       {
            if (objMeasurement != null && objMeasurement.MeasuredPatches.Count > 0)
            {
                objMeasurement.Save();
                objMeasurement.SaveDotGain();

                //if G7 then calculate ΔF and G7NPDC
                if (objMeasurement.ProjectTypeID == 1) objMeasurement.SaveΔF();
                if (objMeasurement.ProjectTypeID == 1) objMeasurement.CalculateG7NPDC();

                //if ISO Calculate Gray a*b*
                if (objMeasurement.ProjectTypeID == 2) objMeasurement.SaveISOGrayab();

             
            }
       }
       catch (Exception ex)
       {

           throw new Exception(ex.ToString());
           
       }
        

    }


   

}

}