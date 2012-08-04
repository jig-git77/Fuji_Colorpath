using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
    public class ProblemSpec
    {
        

        public  ProblemSpec(string orderid)
        {
            this._Order_id = orderid;
        }


        private string _Order_id =  "";
        public string Order_id
        {
            get { return _Order_id; }
            set { _Order_id = value; }
        }

        private string _ProblemStat = String.Empty;
        public string ProblemStat
        {
            get { return _ProblemStat; }
            set { _ProblemStat = value; }
        }

        private  string _mObject = String.Empty;     //device + serial number
        public   string mObject
        {
             get { return _mObject; }
        }


        private string _Deviation = String.Empty;
        public string Deviation
        {
            get { return _Deviation; }
            set { _Deviation = value; }
        }
        

        private string _ObjectIsNot = String.Empty;
        public string ObjectIsNot
        {
            get { return _ObjectIsNot; }
            set { _ObjectIsNot = value; }
        }
        
        private string _DeviationIsNot = String.Empty;
        public string DeviationIsNot
        {
            get { return _DeviationIsNot; }
            set { _DeviationIsNot = value; }
        }



        private string _Location = String.Empty;
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }


        private string _LocationIsNot = String.Empty;
        public string LocationIsNot
        {
            get { return _LocationIsNot; }
            set { _LocationIsNot = value; }
        }



        private string _OntheObject  = String.Empty;
        public string OntheObject
        {
            get { return _OntheObject; }
            set { _OntheObject = value; }
        }

        //When First
        private string _WhenFirst = String.Empty;
        public string WhenFirst
        {
            get { return _WhenFirst; }
            set { _WhenFirst = value; }
        }
    
    
        //Since When
        private string _SinceWhen = String.Empty;
        public string SinceWhen
        {
            get { return _SinceWhen; }
            set { _SinceWhen = value; }
        }
    
        //When Not First
        private string _WhenFirstNot = String.Empty;
        public string WhenFirstNot
        {
            get { return _WhenFirstNot; }
            set { _WhenFirstNot = value; }
        }

    
        //life cycle
        private string _LifeCycle = String.Empty;
        public string LifeCycle
        {
            get { return _LifeCycle; }
            set { _LifeCycle = value; }
        }

        //life cycle is not
        private string _LifeCycleIsNot = String.Empty;
        public string LifeCycleIsNot
        {
            get { return _LifeCycleIsNot; }
            set { _LifeCycleIsNot = value; }
        }


        //How many objects
        private string _NumOfObjects = String.Empty;
        public string NumOfObject
        {
            get { return _NumOfObjects; }
            set { _NumOfObjects = value; }
        }        


        //How many objects (is not)
        private string _NumOfObjectsIsNot = String.Empty;
        public string NumOfObjectsIsNot
        {
            get { return _NumOfObjectsIsNot; }
            set { _NumOfObjectsIsNot = value; }
        }

        //size of deviation
        private string _SizeOfDeviation = String.Empty;
        public string SizeOfDeviation
        {
            get { return _SizeOfDeviation; }
            set { _SizeOfDeviation = value; }
        }

        //How many deviations
        private string _NumOfDeviation = String.Empty;
        public string NumOfDeviation
        {
            get { return _NumOfDeviation; }
            set { _NumOfDeviation = value; }
        }

        ///Trend
        private Int16 _Trend;
        public Int16 Trend
        {
            get { return _Trend; }
            set { _Trend = value; }
        }

        private string _TRCNotes = String.Empty;
        public string TRCNotes
        {
            get { return _TRCNotes; }
            set { _TRCNotes = value; }
        }     


        #region uspGetProblemSpec
        /// <summary>
        /// Get problem statement for the service call
        /// </summary>
        /// <returns>returns the problemspec Data based on the servicd call </returns>
        public void LoadData()
        {

            DataTable DT = null;

            try
            {
               
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "dbo.uspGetProblemSpec";
                Cmd.Parameters.AddWithValue("@order_id", _Order_id);

                DT = (new DataAccess.Database()).GetDataTable(Cmd);

                if (DT != null && DT.Rows.Count > 0 )
                {
                        
                    //this._ProblemStat       =   DT.Rows[0]["ProblemStat"].ToString();
                    this._mObject           =   DT.Rows[0]["bpart_id"].ToString() + " " + DT.Rows[0]["serial_no"].ToString();
                 
                    this._Deviation         =   DT.Rows[0]["problem_desc"].ToString();

                    if (DT.Rows[0]["ProblemStatement"] != DBNull.Value)
                    {
                        this._ProblemStat = DT.Rows[0]["ProblemStatement"].ToString();               
                    }   
                    else
                    {
                        this._ProblemStat = this._mObject + " " + this._Deviation;              //Chirag Changed 
                    }



                    
                    if (DT.Rows[0]["ObjectIsNot"] != DBNull.Value) 
                    {
                        this._ObjectIsNot = DT.Rows[0]["ObjectIsNot"].ToString();
                    }
                    else 
                    {
                         this._ObjectIsNot = "<Need more data>";
                    }

                    if (DT.Rows[0]["DeviationIsNot"] != DBNull.Value)
                    {
                        this._DeviationIsNot = DT.Rows[0]["DeviationIsNot"].ToString();
                    }
                    else
                    {
                        this._DeviationIsNot = "<Need more data>";
                    }

                    if (DT.Rows[0]["Location"] != DBNull.Value)
                    {
                        this._Location = DT.Rows[0]["Location"].ToString();
                    }
                    else
                    {
                        this._Location = "<Need more data>";
                    }



                    if (DT.Rows[0]["LocationIsNot"] != DBNull.Value) 
                    {
                        this._LocationIsNot = DT.Rows[0]["LocationIsNot"].ToString();
                    }
                    else
                    {
                        this._LocationIsNot = "<Need more data>";
                    }

                    if (DT.Rows[0]["OntheObject"] != DBNull.Value)
                    {
                        this._OntheObject = DT.Rows[0]["OntheObject"].ToString();
                    }
                    else 
                    {
                        this._OntheObject = "<Need more data>";   
                    }


                    if (DT.Rows[0]["WhenFirst"] != DBNull.Value)
                    {
                        this._WhenFirst = DT.Rows[0]["WhenFirst"].ToString();
                    } 
                    else 
                    {
                        this._WhenFirst =  "<Need more data>"; 
                    }


                    if (DT.Rows[0]["WhenFirstNot"] != DBNull.Value)
                    {
                        this._WhenFirstNot = DT.Rows[0]["WhenFirstNot"].ToString();              
                    }
                    else
                    {
                        this._WhenFirstNot = "<Need more data>";    
                    }

                    if (DT.Rows[0]["SinceWhen"] != DBNull.Value)
                    {
                        this._SinceWhen = DT.Rows[0]["SinceWhen"].ToString();
                    }   
                    else
	                {
                        this._SinceWhen = "<Need more data>";           
	                }

                    if (DT.Rows[0]["LifeCycle"] != DBNull.Value)
                    {
                        this._LifeCycle = DT.Rows[0]["LifeCycle"].ToString();
                    }
                    else
                    {
                        this._LifeCycle = "<Need more data>";    
                    }


                    if (DT.Rows[0]["LifeCycleIsNot"] != DBNull.Value)
                    {
                        this._LifeCycleIsNot = DT.Rows[0]["LifeCycleIsNot"].ToString();
                    }
                    else
                    {
                        this._LifeCycleIsNot = "<Need more data>";
                    }

    

                    if (DT.Rows[0]["NumOfObject"] != DBNull.Value)
                    {
                        this._NumOfObjects = DT.Rows[0]["NumOfObject"].ToString();
                    }
                    else
                    {
                        this._NumOfObjects = "<Need more data>";
                    }

                    if (DT.Rows[0]["NumOfObjectsIsNot"] != DBNull.Value)
                    {
                        this._NumOfObjectsIsNot = DT.Rows[0]["NumOfObjectsIsNot"].ToString(); 
                    }
                    else 
                    {
                        this._NumOfObjectsIsNot = "<Need more data>";
                    }

                    if (DT.Rows[0]["SizeOfDeviation"] != DBNull.Value)
                    {
                        this._SizeOfDeviation = DT.Rows[0]["SizeOfDeviation"].ToString();
                    }
                    else
                    {
                         this._SizeOfDeviation   =  "<Need more data>";
                    }


                    if (DT.Rows[0]["NumOfDeviation"] != DBNull.Value)
                    {
                        this._NumOfDeviation = DT.Rows[0]["NumOfDeviation"].ToString();
                    }
                    else
                    {
                        this._NumOfDeviation = "<Need more data>";
                    }

                    if (DT.Rows[0]["TRCNotes"] != DBNull.Value)
                    {
                        this._TRCNotes = DT.Rows[0]["TRCNotes"].ToString();     
                    }
                    else
                    {
                        this._TRCNotes = "<Need more data>";
                    }

                  


                    //trend
                    if (DT.Rows[0]["Trend"] != DBNull.Value)
                    {
                        this._Trend = Convert.ToInt16(DT.Rows[0]["Trend"]);
                    }
                    else
                    {
                        this._Trend = 0;
                    }


                }    

            }
            catch (Exception ex)
            {
                throw(ex);
            }



        }
        #endregion 
 

        #region CheckProblemSpecExists
        /// <summary>
        /// Get problem statement for the service call
        /// </summary>
        /// <returns>returns the problemspec Data based on the servicd call </returns>
        public SqlDataReader CheckProblemSpecExists(string orderid)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "SELECT 1 FROM KTProblemSpec WHERE OrderID =  @order_id";
            Cmd.Parameters.AddWithValue("@order_id", orderid);

            return (new DataAccess.Database()).GetDataReader(Cmd);
        }
        #endregion 
       

        //public void UpdateProblemSpec
        //{


        //}

        public void SaveData()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader DR = this.CheckProblemSpecExists(this._Order_id);

                if (DR != null && DR.HasRows == true)
                {
                    //update
                    Cmd.CommandText = "dbo.uspUpdateProblemStat";
                }
                else
                {
                    //insert
                    Cmd.CommandText = "dbo.uspInsertProblemStat";
                }

                       
                Cmd.Parameters.AddWithValue("@orderid", this._Order_id);

                Cmd.Parameters.AddWithValue("@ProblemStatement", this._ProblemStat);

                //What
                if (this._Deviation == null)
                    Cmd.Parameters.AddWithValue("@Deviation", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@Deviation", this._Deviation);

                if (this._ObjectIsNot == null)
                    Cmd.Parameters.AddWithValue("@ObjectIsNot", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@ObjectIsNot", this._ObjectIsNot);


                if (this._DeviationIsNot == null)
                    Cmd.Parameters.AddWithValue("@DeviationIsNot", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@DeviationIsNot", this._DeviationIsNot);      

                //Where
                if (this._Location == null)
                    Cmd.Parameters.AddWithValue("@Location", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@Location", this._Location);

                if (this._LocationIsNot == null)
                    Cmd.Parameters.AddWithValue("@LocationIsNot", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@LocationIsNot", this._LocationIsNot);


                if (this._OntheObject == null)                   
                    Cmd.Parameters.AddWithValue("@OntheObject", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@OntheObject", this._OntheObject);

    
                //When
                if (this._WhenFirst == null)
                    Cmd.Parameters.AddWithValue("@WhenFirst", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@WhenFirst", this._WhenFirst);
    
                if (this._WhenFirstNot == null)
                    Cmd.Parameters.AddWithValue("@WhenFirstNot", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@WhenFirstNot", this._WhenFirstNot);

                if (this._SinceWhen == null)
                    Cmd.Parameters.AddWithValue("@SinceWhen", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@SinceWhen", this._SinceWhen);

                if (this._LifeCycle == null)
                    Cmd.Parameters.AddWithValue("@LifeCycle", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@LifeCycle", this._LifeCycle);

                if (this._LifeCycleIsNot == null)
                    Cmd.Parameters.AddWithValue("@LifeCycleIsNot", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@LifeCycleIsNot", this._LifeCycleIsNot);    



                //Extent
                if (this._NumOfObjects == null)
                    Cmd.Parameters.AddWithValue("@NumOfObject", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@NumOfObject", this._NumOfObjects);


                if (this._NumOfObjectsIsNot == null)
                    Cmd.Parameters.AddWithValue("@NumOfObjectsIsNot", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@NumOfObjectsIsNot", this._NumOfObjectsIsNot);

                if (this._SizeOfDeviation == null)
                    Cmd.Parameters.AddWithValue("@SizeOfDeviation", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@SizeOfDeviation", this._SizeOfDeviation);   


                if (this._NumOfDeviation == null)
                    Cmd.Parameters.AddWithValue("@NumOfDeviation", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@NumOfDeviation", this._NumOfDeviation);
                
             
                Cmd.Parameters.AddWithValue("@Trend", this._Trend); 
                
                //TRC Notes
                if (this._TRCNotes == null)
                    Cmd.Parameters.AddWithValue("@TRCNotes", DBNull.Value);
                else
                    Cmd.Parameters.AddWithValue("@TRCNotes", this._TRCNotes);

                (new DataAccess.Database()).ExecuteNonQuery(Cmd);
                    
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }







    }
}
