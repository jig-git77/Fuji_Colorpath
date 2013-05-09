using System;
using System.Data;
using System.Data.SqlClient;

using Fuji.DataAccess;

public class Measurement
{

    public enum ChartType
    {
        p2pChart = 1,
        Fuji150Patch = 2,
        Sync40Patch = 3,
        ISO24Patch = 4,
        p2pOnyx = 9
    }

    private int _PressRunID = 0;
    public int PressRunID
    {
        get { return _PressRunID; }
        set { _PressRunID = value; }
    }

    /***database id of the measurement ***/
    private int _MeasurementID = 0;
    public int MeasurementID
    {
        get { return _MeasurementID; }
        set { _MeasurementID = value; }
    }

    //content of the measurement file
    private string _MeasurementFile;
    public string MeasurementFile
    {
        get { return _MeasurementFile; }
        set { _MeasurementFile = value; }
    }

    private string _FileName = "";
    public string FileName
    {
        get { return _FileName; }
        set { _FileName = value; }
    }

    private DateTime _UploadDateTime;
    public DateTime UploadDateTime
    {
        get { return _UploadDateTime; }
        set { _UploadDateTime = value; }
    }

    private int _MeasurementTypeID;
    public int MeasurementTypeID
    {
        get { return _MeasurementTypeID; }
        set { _MeasurementTypeID = value; }
    }


    /// <summary>
    /// Measured Patches as a Generic Collection of MeasuredPatch
    /// </summary>
    /// <remarks></remarks>
    private GenericTypedList<MeasuredPatch> _MeasuredPatches = new GenericTypedList<MeasuredPatch>();
    public GenericTypedList<MeasuredPatch> MeasuredPatches
    {
        get { return _MeasuredPatches; }
        set { _MeasuredPatches = value; }
    }




    public void Save()
    {

        SqlCommand objCmd = new SqlCommand();
        try
        {

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "CPSync.uspInsertMeasurement";

            objCmd.Parameters.AddWithValue("@PressRunID", this._PressRunID);
            objCmd.Parameters.AddWithValue("@UploadDateTime", this._UploadDateTime);
            objCmd.Parameters.AddWithValue("@FileName", this._FileName);
            objCmd.Parameters.AddWithValue("@MeasurementFile", this._MeasurementFile);
            objCmd.Parameters.AddWithValue("@MeasurementTypeID", this._MeasurementTypeID);


            objCmd.Parameters.Add(new SqlParameter("@MeasurementID", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));

            //execute the query which will return the output param measurementid
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(objCmd);

            this._MeasurementID = Convert.ToInt32(objCmd.Parameters["@MeasurementID"].Value.ToString());

            foreach (MeasuredPatch objPatch in MeasuredPatches)
            {
                objPatch.MeasurementID = MeasurementID;
                objPatch.Save();
            }


        }
        catch (SqlException ex)
        {
            throw new Exception(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }

    }


    public void SaveDotGain()
    {

        SqlCommand objCmd = new SqlCommand();
        try
        {

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "CPSync.uspInsertColorCharDotGain";

            objCmd.Parameters.AddWithValue("@MeasurementID", this._MeasurementID);

            //execute the query which will return the output param measurementid
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(objCmd);


        }
        catch (SqlException ex)
        {
            throw new Exception(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }

    }


    public void SaveΔF()
    {

        SqlCommand objCmd = new SqlCommand();
        try
        {

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "CPSync.uspInsertColorCharΔF";

            objCmd.Parameters.AddWithValue("@MeasurementID", this._MeasurementID);


            (new Fuji.DataAccess.Database()).ExecuteNonQuery(objCmd);

        }
        catch (SqlException ex)
        {
            throw new Exception(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }

    }


    public void SaveISOGrayab()
    {

        SqlCommand objCmd = new SqlCommand();
        try
        {

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "CPSync.uspInsertColorCharISOGrayab";

            objCmd.Parameters.AddWithValue("@MeasurementID", this._MeasurementID);


            (new Fuji.DataAccess.Database()).ExecuteNonQuery(objCmd);

        }
        catch (SqlException ex)
        {
            throw new Exception(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }

    }

    //Delete measurement and all its data from the database
    public void DeleteMeasurement()
    {

        SqlCommand objCmd = new SqlCommand();
        try
        {

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "CPSync.uspDeleteMeasurement";

            objCmd.Parameters.AddWithValue("@MeasurementID", this._MeasurementID);

            //execute the query which will return the output param measurementid
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(objCmd);

        }
        catch (SqlException ex)
        {
            throw new Exception(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }

    }


}