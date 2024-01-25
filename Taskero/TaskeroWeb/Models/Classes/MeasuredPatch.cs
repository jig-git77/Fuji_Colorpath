using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Taskero.DataAccess;

namespace TaskeroWeb.Models.Classes
{
    [DataContract]
    public class MeasuredPatch
    {


        /***database id of the measurement ***/
        private int _MeasurementID = 0;
        public int MeasurementID
        {
            get { return _MeasurementID; }
            set { _MeasurementID = value; }
        }



        private string _SampleName;
        [DataMember(Order = 1)]
        public string SampleName
        {
            get { return _SampleName; }
            set { _SampleName = value; }
        }

        private decimal _CMYK_C = 0;
        [DataMember(Order = 2)]
        public decimal CMYK_C
        {
            get { return _CMYK_C; }
            set { _CMYK_C = value; }
        }


        private decimal _CMYK_M = 0;
        [DataMember(Order = 3)]
        public decimal CMYK_M
        {
            get { return _CMYK_M; }
            set { _CMYK_M = value; }
        }


        private decimal _CMYK_Y = 0;
        [DataMember(Order = 4)]
        public decimal CMYK_Y
        {
            get { return _CMYK_Y; }
            set { _CMYK_Y = value; }
        }

        private decimal _CMYK_K = 0;
        [DataMember(Order = 5)]
        public decimal CMYK_K
        {
            get { return _CMYK_K; }
            set { _CMYK_K = value; }
        }


        private CIELab _LAB;

        [DataMember(Order = 6)]
        public CIELab LAB
        {
            get { return _LAB; }
            set { _LAB = value; }
        }


        private CIEXYZ _XYZ;
        [DataMember(Order = 7)]
        public CIEXYZ XYZ
        {
            get { return _XYZ; }
            set { _XYZ = value; }
        }



        private Density _Density;
        [DataMember(Order = 8)]
        public Density Density
        {
            get { return _Density; }
            set { _Density = value; }
        }


        private int _PatchTypeID = 0;
        [DataMember(Order = 9)]
        public int PatchTypeID
        {
            get { return _PatchTypeID; }
            set { _PatchTypeID = value; }
        }



        public void Save()
        {

            try
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "CPSync.uspInsertMeasuredPatch";

                Cmd.Parameters.AddWithValue("@MeasurementID", this._MeasurementID);
                Cmd.Parameters.AddWithValue("@SampleName", this._SampleName);

                Cmd.Parameters.AddWithValue("@CMYK_C", this._CMYK_C);
                Cmd.Parameters.AddWithValue("@CMYK_M", this.CMYK_M);
                Cmd.Parameters.AddWithValue("@CMYK_Y", this.CMYK_Y);
                Cmd.Parameters.AddWithValue("@CMYK_K", this.CMYK_K);

                Cmd.Parameters.AddWithValue("@CIELAB_L", this._LAB.L);
                Cmd.Parameters.AddWithValue("@CIELAB_a", this._LAB.A);
                Cmd.Parameters.AddWithValue("@CIELAB_b", this._LAB.B);

                Cmd.Parameters.AddWithValue("@CIEXYZ_X", this._XYZ.X);
                Cmd.Parameters.AddWithValue("@CIEXYZ_Y", this._XYZ.Y);
                Cmd.Parameters.AddWithValue("@CIEXYZ_Z", this._XYZ.Z);

                Cmd.Parameters.AddWithValue("@Density_V", this._Density.Visual);
                Cmd.Parameters.AddWithValue("@Density_C", this._Density.Red);
                Cmd.Parameters.AddWithValue("@Density_M", this._Density.Green);
                Cmd.Parameters.AddWithValue("@Density_Y", this._Density.Blue);

                (new Database()).ExecuteNonQuery(Cmd);




            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }


        }

    }
}