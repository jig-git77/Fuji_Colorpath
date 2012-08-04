using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using WJS;

public class BusinessLogic
{

    public enum SurveyType : int
    {
        NoSurvey = 0,
        PSGSurvey = 1,
        InstallSurvey = 2,
        BreakFixSurvey = 3
    }


    public static void UpdateCallStatusToOnSite(string OrderID, System.DateTime ClientDateTime)
    {
        SqlConnection objConn = null;
        SqlCommand objCmd = null;
        try
        {
            if (OrderID == null || OrderID == string.Empty)
            {
                return;
            }
            else
            {
                objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnovationConnectionString"].ConnectionString);
                objConn.Open();
                objCmd = new SqlCommand();
                objCmd.CommandText = "dbo.uspUpdateSAOnSiteStatus";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Connection = objConn;

                objCmd.Parameters.AddWithValue("@svc_call_id", OrderID);
                objCmd.Parameters.AddWithValue("@arrive_dt", ClientDateTime.ToShortDateString());
                objCmd.Parameters.AddWithValue("@arrive_tm", ClientDateTime.ToShortTimeString());
                objCmd.ExecuteNonQuery();
            }

        }
        catch (Exception)
        {
            
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            if (objCmd != null)
            {
                objCmd = null;
            }
        }
    }


    /// <summary>
    /// Check Serial Number has been inserted into the db for the install activity
    /// </summary>
    /// <param name="OrderID"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static bool CheckAddSerialNumberForInstall(string OrderID)
    {

        if (OrderID == null || OrderID == string.Empty)
        {
            return false;
        }
        SqlConnection objConn = null;
        SqlCommand objCmd = null;
        SqlDataReader dr = null;

        try
        {

            objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnovationConnectionString"].ConnectionString);
            objConn.Open();
            objCmd = new SqlCommand();
            objCmd.CommandText = "uspWebCallClosing_CheckInstallForSerialNum";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Connection = objConn;

            SqlParameter objParam = new SqlParameter("@Order_ID", SqlDbType.VarChar, 15);
                                    
            objParam.Value = OrderID;

            objCmd.Parameters.Add(objParam);

            dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr.HasRows)
            {
                dr.Read();
                if (dr["Result"].ToString().ToUpper() == "TRUE")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            objCmd = null;
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            objConn = null;
        }

     
        
    }


    /// <summary>
    /// Check what type of survey we need to send
    /// 0 indicates not need to send survey, 1 indicates PSG survey , 2 indicates install and 3 indicates break/fix
    /// </summary>
    /// <param name="order_id"></param>
    /// <param name="pcode_id"></param>
    /// <remarks></remarks>
    public static SurveyType ValidateCallForSurveyEmail(string order_id, string pcode_id)
    {

	    if (order_id == null || order_id == string.Empty)
	    {
		    return SurveyType.NoSurvey;
	    }
	    else if (pcode_id == null || pcode_id == string.Empty)
	    {
		    return SurveyType.NoSurvey;
	    }

	    SqlConnection objConn = null;
	    SqlCommand objCmd = null;
	    SqlDataReader dr = null;

	    try
	    {

		    objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnovationConnectionString"].ConnectionString);
		    objConn.Open();
		    objCmd = new SqlCommand();

		    objCmd.CommandText = "uspWebCallClosing_CheckCallForSurveyType";
		    objCmd.CommandType = CommandType.StoredProcedure;
		    objCmd.Connection = objConn;

		    SqlParameter objParam = new SqlParameter("@Order_ID", SqlDbType.VarChar,15);
		    objParam.Value = order_id;

		    objCmd.Parameters.Add(objParam);

		    dr = objCmd.ExecuteReader();

		    if (dr.HasRows)
		    {
			    dr.Read();


			    if (dr.GetSqlInt32(0) == 1)
			    {
				    return SurveyType.PSGSurvey;
			    }
                else if (dr.GetSqlInt32(0) == 2)
			    {
				    return SurveyType.InstallSurvey;
			    }
                else if (dr.GetSqlInt32(0) == 3)
			    {
				    return SurveyType.BreakFixSurvey;
			    }
		    }
		    else
		    {
		    	return SurveyType.NoSurvey;
		    }

	    }
	    catch (Exception )
	    {
		    return SurveyType.NoSurvey;
	    }
	    finally
	    {
		    objCmd = null;
		    objConn.Close();
		    objConn = null;
	    }
        return SurveyType.NoSurvey;
	   
    }

    /// <summary>
    /// Parse List of Email Address to get Customer Email
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string ParseEmailToSendSurvey(string email_addresses)
    {
	    try
	    {
    
	    	if (email_addresses == null || email_addresses == string.Empty)
		    {
			    return string.Empty;
			  
		    }

		    char[] sep = new char[3];
		    sep[0] = Convert.ToChar(",");
		    sep[1] = Convert.ToChar(";");

		    string[] Email_List = email_addresses.Split(sep,StringSplitOptions.RemoveEmptyEntries);

		    foreach (string email in Email_List)
		    {
			    if (email.Contains("@fujifilmgs.com") == true)
			    {
				    continue;
			    }
			    else
			    {
				    Regex RegExForCell = new Regex("^\\d{5}");
				    Match M = RegExForCell.Match(email.Trim());
				    if (M.Success)
				    {
					    continue;
				    }
			    }
			    return email;
		    }
		        return string.Empty;
        
	    }
    	catch (Exception )
	    {
		    return string.Empty;
	    }

    }

    //Check ETA date (techinician's estimated arrival date is greater than call open date
    public static bool CheckETADate(string OrderID)
    {

        try
        {

            if (OrderID == null || OrderID == string.Empty)
            {
                return false;
            }
            else
            {
                SqlConnection objConn = null;
                SqlCommand objCmd = null;
                SqlDataReader dr = null;
                string strSQL = null;

                strSQL = "SELECT order_line.order_id " + "FROM order_line LEFT OUTER JOIN service_call_env on order_line.order_id=service_call_env.order_id " + "WHERE order_line.order_id = '" + OrderID + "' AND DATEDIFF(s,DATEADD(second, DATEDIFF(second, 0, order_line.open_time), DATEADD(day, DATEDIFF(day, 0, order_line.open_date), 0)), DATEADD(second, DATEDIFF(second, 0, service_call_env.eta_time), DATEADD(day, DATEDIFF(day, 0, service_call_env.eta_date), 0))) > 0 ";

                objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnovationConnectionString"].ConnectionString);
                objConn.Open();
                objCmd = new SqlCommand();
                objCmd.CommandText = strSQL;
                objCmd.CommandType = CommandType.Text;
                objCmd.Connection = objConn;

                dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["order_id"].ToString() != string.Empty)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }


            }


        }
        catch (Exception)
        {
            return false;
        }
     
       
    }


    /// <summary>
    /// Save Email Address to Send Survey Email
    /// </summary>
    /// <remarks></remarks>
    public static void SaveEmailToSendSurvey(string order_id, string email, int SurveyTypeID)
    {

        SqlConnection objConn = null;
        SqlCommand objCmd = null;

        try
        {
            objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnovationConnectionString"].ConnectionString);
            objConn.Open();
            objCmd = new SqlCommand();

            objCmd.CommandText = "uspWebCallClosing_SaveEmailForSurvey";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Connection = objConn;

            SqlParameter objParam1 = new SqlParameter("@order_id", SqlDbType.VarChar, 15);
            objParam1.Value = order_id;
            objCmd.Parameters.Add(objParam1);

            SqlParameter objParam2 = new SqlParameter("@email", SqlDbType.VarChar, 50);
            objParam2.Value = email;
            objCmd.Parameters.Add(objParam2);

            SqlParameter objParam3 = new SqlParameter("@surveytype_id", SqlDbType.Int, 8);
            objParam3.Value = SurveyTypeID;
            objCmd.Parameters.Add(objParam3);


            int i = 0;
            i = objCmd.ExecuteNonQuery();

        }
        catch (Exception )
        {

        }
        finally
        {
            objCmd = null;
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            objConn = null;
        }

    }

    public DataTable GetCheckList(int _CheckListTypeID)
    {

        DataTable DTbl = new DataTable();

        SqlConnection objConn = null;
        objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnovationConnectionString"].ConnectionString);

        SqlCommand Cmd = new SqlCommand();
        Cmd.Connection = objConn;
        Cmd.CommandType = CommandType.StoredProcedure;
        Cmd.CommandText = "dbo.uspWebCallClosing_GetCheckList";
        Cmd.Parameters.AddWithValue("@CheckListTypeID", _CheckListTypeID);
      
         SqlDataAdapter DA = new SqlDataAdapter();
         DA.SelectCommand = Cmd;
         DA.Fill(DTbl);

         return DTbl;
        
        //return (new DataAccess.Database()).GetDataTable(Cmd);
    }


    public static DateTime GetOnSiteConfirmDateTime(string order_id)
    {  
        SqlConnection objConn = null;
        SqlCommand objCmd = new SqlCommand();
        string State = null;
        DateTime ConfirmDt;
        try
        {

           objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnovationConnectionString"].ConnectionString);
           objConn.Open();

           objCmd.CommandText = "select state_prov_id from order_line inner join address on order_line.ship_to_address_id = address.address_id where order_id = '" + order_id + "'";
           objCmd.CommandType = CommandType.Text;
           objCmd.Connection = objConn;

           SqlDataReader objReader = objCmd.ExecuteReader();

            if (objReader.HasRows)
            {

                objReader.Read();
                State = objReader.GetValue(0).ToString();
            }

            if (State != null)
            {
                string SourceTimeZone = "Eastern Standard Time";
                //Server's Time Zone Always Eastern Time

                string CustomerTimeZone = string.Empty;

                switch (State.ToUpper())
                {
                    case "CA":
                    case "OR":
                    case "WA":
                    case "NV":
                        //Changes based on the stagte
                        CustomerTimeZone = "Pacific Standard Time";
                        break;
                    case "MT":
                    case "ID":
                    case "WY":
                    case "UT":
                    case "CO":
                    case "AZ":
                    case "NM":
                        //Changes based on the state
                        CustomerTimeZone = "Mountain Standard Time";
                        break;
                    case "TX":
                    case "LA":
                    case "AL":
                    case "MS":
                    case "AR":
                    case "OK":
                    case "IL":
                    case "MO":
                    case "KS":
                    case "NE":
                    case "IA":
                    case "WI":
                    case "ND":
                    case "SD":
                    case "MN":
                        //Changes based on the state
                        CustomerTimeZone = "Central Standard Time";
                        break;
                    case "HI":
                        CustomerTimeZone = "Hawaiian Standard Time";
                        break;
                    case "AK":
                        CustomerTimeZone = "Alaskan Standard Time";
                        break;
                    default:
                        CustomerTimeZone = "Eastern Standard Time";
                        break;
                }
                ConfirmDt = WJS.Utils.TimeZoneInformation.ToLocalTime(SourceTimeZone, DateTime.Now, CustomerTimeZone);
            }
            else
            {
                ConfirmDt = DateTime.MinValue;
            }
        } 

        catch 
        {
            if (objCmd != null)
            {
                objCmd = null;
            }
            ConfirmDt = DateTime.MinValue;
        }

        return ConfirmDt;

    }
}
