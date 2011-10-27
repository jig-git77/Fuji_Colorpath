#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class CPSyncPrintConditionData : CPSyncPrintCondition
    {


        #region uspInsertUpdatePrintCondition
        /// <summary>
        /// Insert And Update new CPSync Print Condition
        /// </summary>
        public void uspInsertUpdateCPSyncPrintCondition()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspUpsertPrintCondition";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@ProjectID",this.ProjectID);           
            if(this.PaperManufacturer.ToString()!="")
                Cmd.Parameters.AddWithValue("@PaperManufacturer", this.PaperManufacturer);
            if (this.PaperName.ToString() != "")
                Cmd.Parameters.AddWithValue("@PaperName", this.PaperName);
            if (this.PaperFinish.ToString() != "")
                Cmd.Parameters.AddWithValue("@PaperFinish", this.PaperFinish);
            if (this.PaperGrade.ToString() != "")
                Cmd.Parameters.AddWithValue("@PaperGrade", this.PaperGrade);
            if (this.PaperWeight.ToString() != "")
                Cmd.Parameters.AddWithValue("@PaperWeight", this.PaperWeight);
            if (this.PaperWeight.ToString() != "")
                Cmd.Parameters.AddWithValue("@PaperWhite", this.PaperWhite);

            if (this.InkManufacturer.ToString() != "")
                Cmd.Parameters.AddWithValue("@InkManufacturer", this.InkManufacturer);
            if (this.InkSetName.ToString() != "")
                Cmd.Parameters.AddWithValue("@InkSetName", this.InkSetName);

            if (this.BlanketManufacturer.ToString() != "")
                Cmd.Parameters.AddWithValue("@BlanketManufacturer", this.BlanketManufacturer);
            if (this.BlanketName.ToString() != "")
                Cmd.Parameters.AddWithValue("@BlanketName", this.BlanketName);
            if (this.BlanketAge.ToString() != "")
                Cmd.Parameters.AddWithValue("@BlanketAge", this.BlanketAge);
            if (this.FountainSolutionMfg.ToString() != "")
                Cmd.Parameters.AddWithValue("@FountainSolutionMfg", this.FountainSolutionMfg);
            if (this.FountainSolutionName.ToString() != "")
                Cmd.Parameters.AddWithValue("@FountainSolutionName", this.FountainSolutionName);

            Cmd.Parameters.AddWithValue("@ISO2846Compliant", this.ISO2846Compliant);

            if (this.Ink1.ToString() != "")
                Cmd.Parameters.AddWithValue("@Ink1", this.Ink1);
            if (this.Ink2.ToString() != "")
                Cmd.Parameters.AddWithValue("@Ink2", this.Ink2);
            if (this.Ink3.ToString() != "")
                Cmd.Parameters.AddWithValue("@Ink3", this.Ink3);
            if (this.Ink4.ToString() != "")
                Cmd.Parameters.AddWithValue("@Ink4", this.Ink4);

            if (this.DotShape.ToString() != "")
                Cmd.Parameters.AddWithValue("@DotShape", this.DotShape);

            Cmd.Parameters.AddWithValue("@DotSize", this.DotSize);

            if (this.ScreeningType.ToString() != "")
                Cmd.Parameters.AddWithValue("@ScreeningType", this.ScreeningType);

            
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion


        #region uspInsertPrintCond
        /// <summary>
        /// Inserts new CPSync Print Target
        /// </summary>
        public void uspInsertCPSyncPrintCond(int _ProjectID, StringBuilder _SB)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspInsertPrintCond";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@ProjectID", _ProjectID);
            Cmd.Parameters.AddWithValue("@PrintCond", _SB.ToString());
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspGetPrintCondition
        /// <summary>
        /// Get Print Condition By Project ID
        /// </summary>
        /// <param name="_PressRunID">ProjectID</param>
        /// <returns></returns>
        public DataTable uspGetPrintCondition(int _ProjectID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetPrintCondition";
            Cmd.Parameters.AddWithValue("@ProjectID", _ProjectID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }

        #endregion

        #region uspGetPrintCond
        /// <summary>
        /// Get Print Condition By Project ID
        /// </summary>
        /// <param name="_PressRunID">ProjectID</param>
        /// <returns></returns>
        public DataTable uspGetPrintCond(int _ProjectID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetPrintCond";
            Cmd.Parameters.AddWithValue("@ProjectID", _ProjectID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }

        #endregion
    }
}
