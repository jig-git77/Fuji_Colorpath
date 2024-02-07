using Oris.DataAccess;
using OrisWeb.Models;
using System;
using System.Data;
using System.Data.SqlClient;


namespace OrisWeb
{
    public partial class Script : PageBase
    {
        #region Declarations
        //string SchemaName = "CPV.";
        #endregion

        #region Page Load And Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //====== Clearing Error Message ======//
                LblError.Text = "";

                TXT1.Style.Add("WIDTH", "98%");
                TXT2.Style.Add("WIDTH", "98%");

                TXT1.Text = "";
                TXT2.Text = "";

                if (IsPostBack == false)
                {
                    FillDDLTables();
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = Exc.Message;
            }
        }
        protected void BTN1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDL.SelectedIndex == -1)
                { LblError.Text = "No table selected."; }
                else
                {
                    //====== FILLING Dset ======//
                    SqlCommand Cmd = new SqlCommand();
                    Cmd.CommandType = CommandType.Text;
                    Cmd.CommandText = "SP_HELP " + DDL.SelectedValue;
                    DataSet Dset = (new Database()).GetDataSet(Cmd);

                    if (DDL_TYPE.SelectedValue == "" || DDL_TYPE.SelectedValue == "1")
                    { CreateProcedure(Dset); }

                    if (DDL_TYPE.SelectedValue == "" || DDL_TYPE.SelectedValue == "2")
                    { CreateClass(Dset); }
                }
            }
            catch (Exception Exc)
            { LblError.Text = Exc.Message; }
        }
        #endregion

        #region Methods and Functions
        private void FillDDLTables()
        {
            try
            {
                //====== Create Command ======//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "select NAME from sysobjects where xtype = 'U' and Name != 'dtproperties' order by name";
                Cmd.CommandType = CommandType.Text;
                DataTable DTbl = (new Database()).GetDataTable(Cmd);

                DDL.DataSource = DTbl;
                DDL.DataTextField = "NAME";
                DDL.DataValueField = "NAME";
                DDL.DataBind();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void CreateProcedure(DataSet Dset)
        {
            try
            {
                string ExectionStr = "\n\t\tDECLARE @ERROR_NUMBER INT ";
                ExectionStr += "\n\t\tDECLARE @ERROR_SEVERITY INT";
                ExectionStr += "\n\t\tDECLARE @ERROR_STATE INT ";
                ExectionStr += "\n\t\tDECLARE @ERROR_PROCEDURE NVARCHAR(252) ";
                ExectionStr += "\n\t\tDECLARE @ERROR_LINE INT ";
                ExectionStr += "\n\t\tDECLARE @ERROR_MESSAGE NVARCHAR(4000) ";
                ExectionStr += "\n\t\tSELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE() ";
                ExectionStr += "\n\t\tEXEC CPV.spSQLErrorLogINSERT @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE ";

                //============//
                if (Dset.Tables[0].Rows.Count != 0)
                {
                    //======= Start Creating procedure =====//
                    int i = 0;
                    string Arguments = "";
                    TXT1.Text = "";

                    #region Delete all Procedures
                    TXT1.Text += "\n" + "if exists (select id from dbo.sysobjects where id = object_id(N'[CPV].[sp" + DDL.SelectedValue + "INSERT]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)";
                    TXT1.Text += "\n" + "drop procedure [CPV].[sp" + DDL.SelectedValue + "INSERT]";
                    TXT1.Text += "\n" + "GO";
                    TXT1.Text += "\n" + "\n";

                    TXT1.Text += "\n" + "if exists (select id from dbo.sysobjects where id = object_id(N'[CPV].[sp" + DDL.SelectedValue + "UPDATE]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)";
                    TXT1.Text += "\n" + "drop procedure [CPV].[sp" + DDL.SelectedValue + "UPDATE]";
                    TXT1.Text += "\n" + "GO";
                    TXT1.Text += "\n" + "\n";

                    TXT1.Text += "\n" + "if exists (select id from dbo.sysobjects where id = object_id(N'[CPV].[sp" + DDL.SelectedValue + "DELETE]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)";
                    TXT1.Text += "\n" + "drop procedure [CPV].[sp" + DDL.SelectedValue + "DELETE]";
                    TXT1.Text += "\n" + "GO";
                    TXT1.Text += "\n" + "\n";

                    //TXT1.Text += "\n" + "if exists (select id from dbo.sysobjects where id = object_id(N'[CPV].[spSelectAll_" + DDL.SelectedValue + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)";
                    //TXT1.Text += "\n" + "drop procedure [CPV].[spSelectAll_" + DDL.SelectedValue + "]";
                    //TXT1.Text += "\n" + "GO";
                    //TXT1.Text += "\n" + "\n";

                    //TXT1.Text += "\n" + "if exists (select id from dbo.sysobjects where id = object_id(N'[CPV].[sp" + DDL.SelectedValue + "SELECT]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)";
                    //TXT1.Text += "\n" + "drop procedure [CPV].[spSelectSingle_" + DDL.SelectedValue + "]";
                    //TXT1.Text += "\n" + "GO";
                    //TXT1.Text += "\n" + "\n";
                    #endregion

                    #region Create Procedure For Insert
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET QUOTED_IDENTIFIER ON ";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET ANSI_NULLS ON ";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\n/*========== INSERT ==========*/";
                    TXT1.Text += "\n/*\nAUTHOR : System generated script.\n*/";
                    TXT1.Text += "\nCREATE PROCEDURE CPV.sp" + DDL.SelectedValue + "INSERT ";
                    TXT1.Text += "( ";
                    Arguments = "";
                    for (i = 0; i < Dset.Tables[1].Rows.Count; i = i + 1)
                    {
                        if (i > 0)
                        {
                            if (Arguments == "")
                            { Arguments += "@" + Dset.Tables[1].Rows[i]["Column_name"].ToString().Replace("%", "").Replace("%", "") + " " + Dset.Tables[1].Rows[i]["TYPE"].ToString().ToUpper() + GetDataTypesLength(Dset.Tables[1].Rows[i]["TYPE"].ToString(), Dset.Tables[1].Rows[i]["Length"].ToString()); }
                            else
                            { Arguments += " , @" + Dset.Tables[1].Rows[i]["Column_name"].ToString().Replace("%", "") + " " + Dset.Tables[1].Rows[i]["TYPE"].ToString().ToUpper() + GetDataTypesLength(Dset.Tables[1].Rows[i]["TYPE"].ToString(), Dset.Tables[1].Rows[i]["Length"].ToString()); }
                        }

                    }
                    TXT1.Text += Arguments;
                    TXT1.Text += " ) AS ";
                    TXT1.Text += "\nBEGIN";
                    TXT1.Text += "\n\tBEGIN TRY";
                    TXT1.Text += "\n\t\tINSERT INTO " + DDL.SelectedValue;
                    TXT1.Text += "(";
                    Arguments = "";
                    for (i = 0; i < Dset.Tables[1].Rows.Count; i = i + 1)
                    {
                        if (i > 0)
                        {
                            if (Arguments == "")
                            { Arguments += "[" + Dset.Tables[1].Rows[i]["Column_name"].ToString() + "]"; }
                            else
                            { Arguments += " , [" + Dset.Tables[1].Rows[i]["Column_name"].ToString() + "]"; }
                        }
                    }
                    TXT1.Text += Arguments;
                    TXT1.Text += ") VALUES (";
                    Arguments = "";
                    for (i = 0; i < Dset.Tables[1].Rows.Count; i = i + 1)
                    {
                        if (i > 0)
                        {
                            if (Arguments == "")
                            { Arguments += "@" + Dset.Tables[1].Rows[i]["Column_name"].ToString().Replace("%", ""); }
                            else
                            { Arguments += " , @" + Dset.Tables[1].Rows[i]["Column_name"].ToString().Replace("%", ""); }
                        }
                    }
                    TXT1.Text += Arguments;
                    TXT1.Text += ")";
                    TXT1.Text += "\n\tEND TRY";
                    TXT1.Text += "\n\tBEGIN CATCH";
                    TXT1.Text += ExectionStr;
                    TXT1.Text += "\n\tEND CATCH";
                    TXT1.Text += "\nEND";
                    TXT1.Text += "\n";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET QUOTED_IDENTIFIER OFF ";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET ANSI_NULLS ON ";
                    TXT1.Text += "\nGO";
                    #endregion

                    #region Create Procedure For Update
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET QUOTED_IDENTIFIER ON ";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET ANSI_NULLS ON ";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\n/*========== UPDATE ==========*/";
                    TXT1.Text += "\n/*\nAUTHOR : System generated script.\n*/";
                    TXT1.Text += "\nCREATE PROCEDURE CPV.sp" + DDL.SelectedValue + "UPDATE ";
                    TXT1.Text += "( ";
                    Arguments = "";
                    for (i = 0; i < Dset.Tables[1].Rows.Count; i = i + 1)
                    {
                        if (Arguments == "")
                        { Arguments += "@" + Dset.Tables[1].Rows[i]["Column_name"].ToString().Replace("%", "") + " " + Dset.Tables[1].Rows[i]["TYPE"].ToString().ToUpper() + GetDataTypesLength(Dset.Tables[1].Rows[i]["TYPE"].ToString(), Dset.Tables[1].Rows[i]["Length"].ToString()); }
                        else
                        { Arguments += " , @" + Dset.Tables[1].Rows[i]["Column_name"].ToString().Replace("%", "") + " " + Dset.Tables[1].Rows[i]["TYPE"].ToString().ToUpper() + GetDataTypesLength(Dset.Tables[1].Rows[i]["TYPE"].ToString(), Dset.Tables[1].Rows[i]["Length"].ToString()); }
                    }
                    TXT1.Text += Arguments;
                    TXT1.Text += " ) AS ";
                    TXT1.Text += "\nBEGIN";
                    TXT1.Text += "\n\tBEGIN TRY";
                    TXT1.Text += "\n\t\tUPDATE " + DDL.SelectedValue + " SET ";
                    Arguments = "";
                    for (i = 0; i < Dset.Tables[1].Rows.Count; i = i + 1)
                    {
                        if (i > 0)
                        {
                            if (Arguments == "")
                            { Arguments += "[" + Dset.Tables[1].Rows[i]["Column_name"].ToString() + "] = @" + Dset.Tables[1].Rows[i]["Column_name"].ToString().Replace("%", ""); }
                            else
                            { Arguments += " , [" + Dset.Tables[1].Rows[i]["Column_name"].ToString() + "] = @" + Dset.Tables[1].Rows[i]["Column_name"].ToString().Replace("%", ""); }
                        }
                    }
                    TXT1.Text += Arguments;
                    TXT1.Text += " WHERE [" + Dset.Tables[1].Rows[0]["Column_name"].ToString() + "] = @" + Dset.Tables[1].Rows[0]["Column_name"].ToString().Replace("%", "");
                    TXT1.Text += "\n\tEND TRY";
                    TXT1.Text += "\n\tBEGIN CATCH";
                    TXT1.Text += ExectionStr;
                    TXT1.Text += "\n\tEND CATCH";
                    TXT1.Text += "\nEND";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET QUOTED_IDENTIFIER OFF ";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET ANSI_NULLS ON ";
                    TXT1.Text += "\nGO";
                    #endregion

                    #region Create Procedure For Delete
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET QUOTED_IDENTIFIER ON ";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET ANSI_NULLS ON ";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\n/*========== DELETE ==========*/";
                    TXT1.Text += "\n/*\nAUTHOR : System generated script.\n*/";
                    TXT1.Text += "\nCREATE PROCEDURE CPV.sp" + DDL.SelectedValue + "DELETE ";
                    TXT1.Text += "( " + "@" + Dset.Tables[1].Rows[0]["Column_name"].ToString().Replace("%", "") + " " + Dset.Tables[1].Rows[0]["TYPE"].ToString().ToUpper() + GetDataTypesLength(Dset.Tables[1].Rows[0]["TYPE"].ToString(), Dset.Tables[1].Rows[0]["Length"].ToString()) + " ) AS ";
                    TXT1.Text += "\nBEGIN";
                    TXT1.Text += "\n\tBEGIN TRY";
                    TXT1.Text += "\n\t\tDELETE FROM " + DDL.SelectedValue;
                    TXT1.Text += " WHERE [" + Dset.Tables[1].Rows[0]["Column_name"].ToString() + "] = @" + Dset.Tables[1].Rows[0]["Column_name"].ToString().Replace("%", "");
                    TXT1.Text += "\n\tEND TRY";
                    TXT1.Text += "\n\tBEGIN CATCH";
                    TXT1.Text += ExectionStr;
                    TXT1.Text += "\n\tEND CATCH";
                    TXT1.Text += "\nEND";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET QUOTED_IDENTIFIER OFF ";
                    TXT1.Text += "\nGO";
                    TXT1.Text += "\nSET ANSI_NULLS ON ";
                    TXT1.Text += "\nGO";
                    #endregion

                    #region Create Procedure For Select All
                    //TXT1.Text += "\nGO";
                    //TXT1.Text += "\nSET QUOTED_IDENTIFIER ON ";
                    //TXT1.Text += "\nGO";
                    //TXT1.Text += "\nSET ANSI_NULLS ON ";
                    //TXT1.Text += "\nGO";
                    //TXT1.Text += "\n/*========== DELETE ==========*/";
                    //TXT1.Text += "\n/*\nAUTHOR : System generated script.\n*/";
                    //TXT1.Text += "\nCREATE PROCEDURE CPV.spSelectAll_" + DDL.SelectedValue + " ";
                    //TXT1.Text += " AS ";
                    //TXT1.Text += "\nBEGIN";
                    //TXT1.Text += "\n\tBEGIN TRY";
                    //TXT1.Text += "\n\t\tSELECT * FROM " + DDL.SelectedValue;
                    //TXT1.Text += "\n\tEND TRY";
                    //TXT1.Text += "\n\tBEGIN CATCH";
                    //TXT1.Text += ExectionStr;
                    //TXT1.Text += "\n\tEND CATCH";
                    //TXT1.Text += "\nEND";
                    //TXT1.Text += "\nGO";
                    //TXT1.Text += "\nSET QUOTED_IDENTIFIER OFF ";
                    //TXT1.Text += "\nGO";
                    //TXT1.Text += "\nSET ANSI_NULLS ON ";
                    //TXT1.Text += "\nGO";
                    #endregion

                    #region Create Procedure For Select Single
                    //TXT1.Text += "\nGO";
                    //TXT1.Text += "\nSET QUOTED_IDENTIFIER ON ";
                    //TXT1.Text += "\nGO";
                    //TXT1.Text += "\nSET ANSI_NULLS ON ";
                    //TXT1.Text += "\nGO";
                    //TXT1.Text += "\n/*========== DELETE ==========*/";
                    //TXT1.Text += "\n/*\nAUTHOR : System generated script.\n*/";
                    //TXT1.Text += "\nCREATE PROCEDURE CPV.spSelectSingle_" + DDL.SelectedValue + " ";
                    //TXT1.Text += "( " + "@" + Dset.Tables[1].Rows[0]["Column_name"].ToString().Replace("%","") + " " + Dset.Tables[1].Rows[0]["TYPE"].ToString().ToUpper() + GetDataTypesLength(Dset.Tables[1].Rows[0]["TYPE"].ToString(), Dset.Tables[1].Rows[0]["Length"].ToString()) + " ) AS ";
                    //TXT1.Text += "\nBEGIN";
                    //TXT1.Text += "\n\tBEGIN TRY";
                    //TXT1.Text += "\n\t\tSELECT * FROM " + DDL.SelectedValue;
                    //TXT1.Text += " WHERE [" + Dset.Tables[1].Rows[0]["Column_name"].ToString() + "] = @" + Dset.Tables[1].Rows[0]["Column_name"].ToString().Replace("%","");
                    //TXT1.Text += "\n\tEND TRY";
                    //TXT1.Text += "\n\tBEGIN CATCH";
                    //TXT1.Text += ExectionStr;
                    //TXT1.Text += "\n\tEND CATCH";
                    //TXT1.Text += "\nEND";
                    //TXT1.Text += "\nGO";
                    //TXT1.Text += "\nSET QUOTED_IDENTIFIER OFF ";
                    //TXT1.Text += "\nGO";
                    //TXT1.Text += "\nSET ANSI_NULLS ON ";
                    //TXT1.Text += "\nGO";
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void CreateClass(DataSet Dset)
        {
            try
            {
                if (Dset.Tables[0].Rows.Count != 0)
                {
                    //============//
                    int i = 0;
                    string IDENTITYKEY = "";
                    if (Dset.Tables[2].Rows[0]["Identity"].ToString() == "No identity column defined.")
                    { IDENTITYKEY = Dset.Tables[1].Rows[0]["column_name"].ToString().Replace("%", ""); }
                    else
                    { IDENTITYKEY = Dset.Tables[2].Rows[0]["Identity"].ToString(); }

                    TXT2.Text = "";

                    #region SYSTEM
                    TXT2.Text += "\n" + "#region SYSTEM";
                    TXT2.Text += "\n" + "using System;";
                    TXT2.Text += "\n" + "using System.Data ;";
                    TXT2.Text += "\n" + "using System.Data.SqlClient ;";
                    TXT2.Text += "\n" + "#endregion";
                    #endregion

                    #region OTHERS
                    TXT2.Text += "\n\n" + "#region Others";
                    TXT2.Text += "\n" + "using Fuji.DataAccess;";
                    TXT2.Text += "\n" + "#endregion";
                    #endregion

                    #region NAMESPACE
                    TXT2.Text += "\n" + "";
                    TXT2.Text += "\n" + "namespace Fuji.Business ";
                    TXT2.Text += "\n" + "{";

                    #region CLASS
                    TXT2.Text += "\n" + "public abstract class " + DDL.SelectedValue;
                    TXT2.Text += "\n" + "{";
                    TXT2.Text += "\n";

                    #region Variables
                    TXT2.Text += "\n" + "#region Variables";
                    TXT2.Text += "\n private string _TableName = \"" + DDL.SelectedValue + "\";";
                    for (i = 0; i < Dset.Tables[1].Rows.Count; i = i + 1)
                    { TXT2.Text += "\n private " + GetDataTypes(Dset.Tables[1].Rows[i]["type"].ToString()) + " _" + Dset.Tables[1].Rows[i]["column_name"].ToString().Replace("%", "") + " = " + GetDataTypesValue(Dset.Tables[1].Rows[i]["type"].ToString()) + "; "; }
                    TXT2.Text += "\n" + "#endregion";
                    TXT2.Text += "\n";
                    #endregion

                    #region Properties
                    TXT2.Text += "\n" + "#region Properties";
                    TXT2.Text += "\n public string TableName ";
                    TXT2.Text += "\n { ";
                    TXT2.Text += "\n get { return _TableName; } ";
                    TXT2.Text += "\n set {  } ";
                    TXT2.Text += "\n } ";
                    for (i = 0; i < Dset.Tables[1].Rows.Count; i = i + 1)
                    {
                        TXT2.Text += "\n public " + GetDataTypes(Dset.Tables[1].Rows[i]["type"].ToString()) + " " + Dset.Tables[1].Rows[i]["column_name"].ToString() + " ";
                        TXT2.Text += "\n { ";
                        TXT2.Text += "\n get { return _" + Dset.Tables[1].Rows[i]["column_name"].ToString().Replace("%", "") + "; } ";
                        TXT2.Text += "\n set { _" + Dset.Tables[1].Rows[i]["column_name"].ToString().Replace("%", "") + " = value; } ";
                        TXT2.Text += "\n } ";
                    }
                    TXT2.Text += "\n" + "#endregion";
                    #endregion

                    #region Insert
                    TXT2.Text += "\n" + "";
                    TXT2.Text += "\n" + "#region Insert";
                    TXT2.Text += "\n" + "public void Insert()";
                    TXT2.Text += "\n" + "{";
                    TXT2.Text += "\n" + "//========== Execute Command ==========//";
                    TXT2.Text += "\n" + "SqlCommand Cmd = new SqlCommand();";
                    TXT2.Text += "\n" + "Cmd.CommandText = \"CPV.sp" + DDL.SelectedValue + "INSERT\";";
                    TXT2.Text += "\n" + "Cmd.CommandType = CommandType.StoredProcedure;";
                    for (i = 0; i < Dset.Tables[1].Rows.Count; i = i + 1)
                    {
                        if (i > 0)
                        { TXT2.Text += "\n" + "Cmd.Parameters.AddWithValue( \"@" + Dset.Tables[1].Rows[i]["column_name"].ToString().Replace("%", "") + "\" , this." + Dset.Tables[1].Rows[i]["column_name"].ToString().Replace("%", "") + " );"; }
                    }
                    TXT2.Text += "\n" + "(new Database()).ExecuteNonQuery(Cmd);";
                    TXT2.Text += "\n" + "}";
                    TXT2.Text += "\n" + "#endregion";
                    #endregion

                    #region Update
                    TXT2.Text += "\n" + "";
                    TXT2.Text += "\n" + "#region Update";
                    TXT2.Text += "\n" + "public void Update()";
                    TXT2.Text += "\n" + "{";
                    TXT2.Text += "\n" + "//========== Execute Command ==========//";
                    TXT2.Text += "\n" + "SqlCommand Cmd = new SqlCommand();";
                    TXT2.Text += "\n" + "Cmd.CommandType = CommandType.StoredProcedure;";
                    TXT2.Text += "\n" + "Cmd.CommandText = \"CPV.sp" + DDL.SelectedValue + "UPDATE\";";
                    for (i = 0; i < Dset.Tables[1].Rows.Count; i = i + 1)
                    {
                        TXT2.Text += "\n" + "Cmd.Parameters.AddWithValue( \"@" + Dset.Tables[1].Rows[i]["column_name"].ToString().Replace("%", "") + "\" , this." + Dset.Tables[1].Rows[i]["column_name"].ToString().Replace("%", "") + " );";
                    }
                    TXT2.Text += "\n" + "(new Database()).ExecuteNonQuery(Cmd);";
                    TXT2.Text += "\n" + "}";
                    TXT2.Text += "\n" + "#endregion";
                    #endregion

                    #region Delete Any One
                    TXT2.Text += "\n" + "";
                    TXT2.Text += "\n" + "#region Delete";
                    TXT2.Text += "\n" + "public void spDelete()";
                    TXT2.Text += "\n" + "{";
                    TXT2.Text += "\n" + "//========== Execute Command ==========//";
                    TXT2.Text += "\n" + "SqlCommand Cmd = new SqlCommand();";
                    TXT2.Text += "\n" + "Cmd.CommandType = CommandType.StoredProcedure;";
                    TXT2.Text += "\n" + "Cmd.CommandText = \"CPV.sp" + DDL.SelectedValue + "DELETE\";";
                    TXT2.Text += "\n" + "Cmd.Parameters.AddWithValue( \"@" + Dset.Tables[1].Rows[0]["column_name"].ToString() + "\" , this." + Dset.Tables[1].Rows[0]["column_name"].ToString() + " );";
                    TXT2.Text += "\n" + "(new Database()).ExecuteNonQuery(Cmd);";
                    TXT2.Text += "\n" + "}";
                    TXT2.Text += "\n" + "#endregion";
                    #endregion

                    #region Get All Records
                    //TXT2.Text += "\n" + "";
                    //TXT2.Text += "\n" + "#region Get All Records";
                    //TXT2.Text += "\n" + "public DataTable spSelectAll()";
                    //TXT2.Text += "\n" + "{";
                    //TXT2.Text += "\n" + "//========== Execute Command ==========//";
                    //TXT2.Text += "\n" + "SqlCommand Cmd = new SqlCommand();";
                    //TXT2.Text += "\n" + "Cmd.CommandType = CommandType.StoredProcedure;";
                    //TXT2.Text += "\n" + "Cmd.CommandText = \"CPV.spSelectAll_" + DDL.SelectedValue + "\";";
                    //TXT2.Text += "\n" + "return (new Database()).GetDataTable(Cmd);";
                    //TXT2.Text += "\n" + "}";
                    //TXT2.Text += "\n" + "#endregion";
                    #endregion

                    #region Get Single Record
                    //TXT2.Text += "\n" + "";
                    //TXT2.Text += "\n" + "#region Get Single Record";
                    //TXT2.Text += "\n" + "public DataTable spSelectSingle()";
                    //TXT2.Text += "\n" + "{";
                    //TXT2.Text += "\n" + "//========== Execute Command ==========//";
                    //TXT2.Text += "\n" + "SqlCommand Cmd = new SqlCommand();";
                    //TXT2.Text += "\n" + "Cmd.CommandType = CommandType.StoredProcedure;";
                    //TXT2.Text += "\n" + "Cmd.CommandText = \"CPV.spSelectSingle_" + DDL.SelectedValue + "\";";
                    //TXT2.Text += "\n" + "Cmd.Parameters.AddWithValue( \"@" + Dset.Tables[1].Rows[0]["column_name"].ToString() + "\" , this." + Dset.Tables[1].Rows[0]["column_name"].ToString() + " );";
                    //TXT2.Text += "\n" + "return (new Database()).GetDataTable(Cmd);";
                    //TXT2.Text += "\n" + "}";
                    //TXT2.Text += "\n" + "#endregion";
                    #endregion

                    TXT2.Text += "\n" + "";
                    TXT2.Text += "\n" + "}";//CLASS
                    #endregion

                    TXT2.Text += "\n" + "}";//NAMESPACE
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private string GetDataTypes(string StrVal)
        {
            try
            {
                if (StrVal == "int")
                { return "int"; }
                if (StrVal == "tinyint" || StrVal == "smallint")
                { return "short"; }
                if (StrVal == "bigint")
                { return "bigint"; }
                else if (StrVal == "ntext")
                { return "string"; }
                else if (StrVal == "bit")
                { return "bool"; }
                else if (StrVal == "money" || StrVal == "numeric" || StrVal == "decimal")
                { return "decimal"; }
                else
                { return "string"; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private string GetDataTypeValues(string StrVal)
        {
            try
            {
                if (StrVal == "int" || StrVal == "tinyint" || StrVal == "bigint" || StrVal == "smallint")
                { return "0"; }
                else if (StrVal == "ntext")
                { return "\"\""; }
                else if (StrVal == "bit")
                { return "0"; }
                else if (StrVal == "money" || StrVal == "numeric" || StrVal == "decimal")
                { return "0"; }
                else
                { return "\"\""; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private string GetDataTypesValue(string StrVal)
        {
            try
            {
                if (StrVal == "int" || StrVal == "tinyint" || StrVal == "bigint" || StrVal == "smallint" || StrVal == "numeric")
                { return "0"; }
                else if (StrVal == "bit")
                { return "true"; }
                else if (StrVal == "decimal" || StrVal == "money")
                { return "Convert.ToDecimal(\"0\")"; }
                else if (StrVal == "ntext" || StrVal == "varchar")
                { return "\"\""; }
                else
                { return "\"\""; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private string GetDataTypesLength(string StrVal, string Length)
        {
            try
            {
                if (StrVal == "int" || StrVal == "tinyint" || StrVal == "smallint" || StrVal == "bigint" || StrVal == "decimal" || StrVal == "money" || StrVal == "bit" || StrVal == "ntext" || StrVal == "datetime" || StrVal == "uniqueidentifier")
                { return ""; }
                else if (StrVal == "numeric")
                { return "(18,2)"; }
                else
                {
                    if (Length == "-1")
                    { return "(50)"; }
                    else { return "(" + Length + ")"; }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }

}