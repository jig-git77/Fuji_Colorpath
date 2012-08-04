using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;


public partial class OrderInfo : System.Web.UI.Page
{
    protected string orderId;
    protected DataTable table;
    protected SqlConnection ordercon;
    protected SqlCommand expSql;
    protected SqlCommand mangSql;
    protected string connectionString;
    protected DataTable Material;
    protected DataSet matDs;
    protected DataSet mangDS;
    protected SqlDataAdapter matAdapt;
    protected DataSet actvDs;
    protected DataSet expDs;
    protected int actIndex = 0;
    protected int expIndex = 0;
    protected int matIndex = 0;
    protected ArrayList matDemandIdList = new ArrayList();


    protected void Page_Load(object sender, EventArgs e)
    {
        
            //Page Load Functions...
            //if (IsPostBack == false)
            //{
                   
            //Check Session Variables
            if ((Session["SessionInfo"] == null) || (Session["UserID"] == null) || (Session["PassWd"] == null))
            {
                 Response.Redirect("RedirectLogin.aspx", false);
            }

                       
            //Redirect if session expires
            PageBody.Attributes.Add("onLoad", "window.setTimeout(\"window.location.href='RedirectLogin.aspx'\"," + (Session.Timeout * 60 * 1000 + 5000) + ");");
            
            //limit number of characters entered - Billing Notes
            txtBillingNotes.Attributes.Add("onKeyPress","return limitText(this,253);");
            txtBillingNotes.Attributes.Add("onpaste", "return preventPaste(this,253);");

            this.DataGrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid2_ItemDataBound);

            //limit number of characters entered - Resolve Text
            ResolveTxt.Attributes.Add("onKeyPress", "return limitText(this,1800);");
            ResolveTxt.Attributes.Add("onpaste", "return preventPaste(this,1800);");
                              
            Set_Connection();
            Bind_PCode();
            Bind_OrderInfo();
            Bind_Activities();
            Bind_Materials();
            Bind_Expenses();
          
            Close_Connection();
          
            //}
    }

    protected void Set_Connection()
    {
        //Establish a connection, use an existing one from the connection pool
        connectionString = ConfigurationManager.ConnectionStrings["EnovationConnectionString"].ConnectionString;
        ordercon = new SqlConnection(connectionString);
        ordercon.Open();

        //get the order ID from previous page
        orderId = Request.QueryString["order_id"].ToString();
    }
    protected void Bind_OrderInfo()
    {
        string commandtxt = "select order_line.order_id, order_line.cust_company_id, order_line.cust_company_descr, order_line.site_company_id," + " " +
            "order_line.site_company_descr, order_line.po_id, company.z_udf_key_1 as region, " + " " +
            "open_date =convert( datetime, convert( varchar(10), order_line.open_date, 101 ) + ' ' + convert( varchar(8), order_line.open_time, 108 ) )," + " " +
            "install_date = (select item_inst.install_date from item_inst where item_inst.item_id=service_call.item_id)," + " " +
            "contract_descr = (select cconth.descr from cconth where cconth.cconth_id=order_line.cconth_id)," + " " +
            "service_call.callt_id as request_type, order_line.fax_phone_id ,order_line.ship_to_address_id, bpart.gl_type_id as manufacturer, service_call.bpart_id as model, service_call.serial_no, order_line.cconth_id," + " " +
            "warr_type=(select descr from warr_type where warr_type.warr_type_id=service_call.warr_type_id), " + " " +
            "service_call.pcode_id, service_call.cause_id, service_call.rcode_id, order_line.problem_desc, service_call_env.resolve_text, order_line.is_resolved," + " " +
            "address.address_1, address.city, address.state_prov_id, address.zip, address.email_id, order_line.caller_name, order_line.phone_id" + " " +
            ",order_env.back_end_env as billing_notes,modules_desc as personsignoff  " +
            "from order_line left outer join service_call on order_line.order_id=service_call.order_id  left outer join service_call_env on order_line.order_id=service_call_env.order_id" + " " +
            "left outer join company on order_line.site_company_id=company.company_id  left outer join bpart on service_call.bpart_id = bpart.bpart_id " + " " +
            "left outer join address on order_line.ship_to_address_id=address.address_id " +
            "left outer join order_env on order_env.order_id =  order_line.order_id " +
            "where order_line.order_id='" + orderId + "'";

        //Remember to add exception handling code here
        SqlDataAdapter orderAdapt = new SqlDataAdapter(commandtxt, ordercon);
        DataSet orderDs = new DataSet();

        if (this.IsPostBack)
        {
            orderDs = (DataSet)Session["OrderDS"];
        }
        else
        {

            orderAdapt.Fill(orderDs);

            //Fill Session DS
            Session["OrderDS"] = orderDs;
            //iterate over the dataset and populate Header fields on the page
            foreach (DataRow dr in orderDs.Tables[0].Rows)
            {
                OrderTxt.Text = dr["order_id"].ToString();
                CustTxt.Text = dr["cust_company_id"].ToString();
                CustNameTxt.Text = dr["cust_company_descr"].ToString();
                SiteTxt.Text = dr["site_company_id"].ToString();
                SiteNameTxt.Text = dr["site_company_descr"].ToString();
                POTxt.Text = dr["po_id"].ToString();
                RgnTxt.Text = dr["region"].ToString();
                OpenDtTxt.Text = dr["open_date"].ToString();
                ReqTxt.Text = dr["request_type"].ToString();
                ManuTxt.Text = dr["manufacturer"].ToString();
                SerialTxt.Text = dr["serial_no"].ToString();
                ModelTxt.Text = dr["model"].ToString();
                InstTxt.Text = dr["install_date"].ToString(); 
                ContTxt.Text = dr["cconth_id"].ToString();
                WarrTxt.Text = dr["warr_type"].ToString();
                PDescTxt.Text = dr["problem_desc"].ToString();
                ResolveTxt.Text = dr["resolve_text"].ToString();
                StreetTxt.Text = dr["address_1"].ToString();
                CityTxt.Text = dr["city"].ToString();
                StateTxt.Text = dr["state_prov_id"].ToString();
                ZipTxt.Text = dr["zip"].ToString();
                EmailTxt.Text = dr["email_id"].ToString();
                txtBillingNotes.Text = dr["billing_notes"].ToString();
                txtServiceSignOff.Text = dr["personsignoff"].ToString();
                if (dr["fax_phone_id"].ToString() != "")  //chirag changed this on 02/08/2008
                {
                    //Check whethere we already have 1 else append 1  Chirag changed on 06/04/08
                    string fax_no = dr["fax_phone_id"].ToString().Trim();
                    if (fax_no.StartsWith("1"))
                    {
                        FaxTxt.Text = fax_no;
                    }
                    else {
                        FaxTxt.Text = "1-" + fax_no;
                    }           
        
                    
                }
                else
                {
                    FaxTxt.Text = dr["fax_phone_id"].ToString();
                }                              
                ContDescTxt.Text = dr["contract_descr"].ToString();
                ContactText.Text = dr["caller_name"].ToString();
                PhoneTxt.Text = dr["phone_id"].ToString();

                if (dr["pcode_id"].ToString() != "")
                {
                    PcodeDDList.SelectedValue = dr["pcode_id"].ToString();
                }
                
                if (dr["rcode_id"].ToString() != "")
                {
                    RcodeDDList.SelectedValue = dr["rcode_id"].ToString();
                }
                if (dr["cause_id"].ToString() != "")
                {
                    CCodeDDList.SelectedValue = dr["cause_id"].ToString();
                }


            }
        }
    }

    protected void Bind_PCode()
    {

        if (!IsPostBack)
        {
            SqlCommand objCmd = new SqlCommand();

            DataTable pcode = new DataTable();

            objCmd.CommandText = "uspGetpcode_id";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Connection = ordercon;

            SqlParameter objParam = new SqlParameter("@order_id", SqlDbType.VarChar, 15);
            objParam.Value = orderId;
            objCmd.Parameters.Add(objParam);

            SqlDataAdapter objADP = new SqlDataAdapter(objCmd);
            objADP.Fill(pcode);

            if (pcode != null && pcode.Rows.Count > 0 )
            {
                PcodeDDList.DataSource = pcode;
                PcodeDDList.DataTextField = "descr";
                PcodeDDList.DataValueField = "pcode_id";
                PcodeDDList.DataBind();
            } 
            else
            {
                 PcodeDDList.DataSource = null;
                 PcodeDDList.DataBind();
                
            }   
            
         }
                  
        
      
        
    }


    protected void Bind_Activities()
    {
        //create Activities List for the selected Order
        string actvCommandTxt = "select demand.bpart_descr, demand.bpart_id, demand_labor.duration, demand_labor.arrive_dt," + " " +
                    "demand_labor.arrive_tm, demand.actual_dt, demand.actual_tm,demand.demand_id, demand.is_fullfiled,(select SUM(a.qty) from demand a, demand_link_xref where demand_link_xref.fr_demand_id = demand.demand_id and a.demand_id = demand_link_xref.to_demand_id )as travel_distance from demand_labor" + " " +
                    "inner join demand on demand_labor.demand_id=demand.demand_id where demand.bpart_gcl_id='Labor' and demand.order_id='" + orderId + "'";

        SqlDataAdapter orderAdapt1 = new SqlDataAdapter(actvCommandTxt, ordercon);
        DataSet actvDs = new DataSet();
        orderAdapt1.Fill(actvDs);
        DataColumn[] keys = new DataColumn[1];
        keys[0] = actvDs.Tables[0].Columns["demand_id"];
        //Set the primary key for the dataset
        actvDs.Tables[0].PrimaryKey = keys;

        DataGrid1.DataSource = actvDs;
        DataGrid1.DataKeyField = "demand_id";
        if (!IsPostBack)
        {
            DataGrid1.DataBind();

          
            //Set the selected values in Dropdown List on PageLoad
            for (int j = 0; j <= DataGrid1.Items.Count - 1; j++)
            {
                Label lbl= (Label)DataGrid1.Items[j].FindControl("State");
                DataRow rr = actvDs.Tables[0].Rows[j];
                if (rr["is_fullfiled"].ToString() == "Y")
                {
                    lbl.Text = "Fullfilled";

                    DataGrid1.Items[j].Enabled = false;
                }
                else
                {
                    lbl.Text = "Open";
                    DataGrid1.Items[j].Enabled = true;

                    //set duration column to zero 
                    TextBox txtDuration = (TextBox)DataGrid1.Items[j].FindControl("TextBoxDur");
                    txtDuration.Text = String.Empty;
                     
                }
         
                
            }
             
        }

    }

    protected void Bind_Materials()
    {
        //Create materials list
        string matCommandTxt = "select demand.bpart_id, demand.qty , demand_material.z_udf_float_1," + " " +
                     "demand.bpart_descr , demand.demand_id, demand.comment_text,  case when demand.bpart_id in ('3010108132', '3010106120' , '3010110416', '3010110596') THEN 1 else 0 end as acuity_printhead, isnull(z_udf_float_2,0) as z_udf_float_2 from demand  inner join demand_material on demand.demand_id=demand_material.demand_id" + " " +
                     "where demand.order_id='" + orderId + "'";

        matAdapt = new SqlDataAdapter(matCommandTxt, ordercon);
        //Set the update command
        matAdapt.UpdateCommand = new SqlCommand("update demand_material set z_udf_float_1=@Usage ,z_udf_float_2 = @Reason where demand_id=@DemandID", ordercon);
        matAdapt.UpdateCommand.Parameters.Add("@Usage",
           SqlDbType.VarChar, sizeof(System.Double), "z_udf_float_1");
        matAdapt.UpdateCommand.Parameters.Add("@Reason", SqlDbType.Int, sizeof(System.Int32), "z_udf_float_2");
        matAdapt.UpdateCommand.Parameters.Add("@DemandID",
           SqlDbType.Int, sizeof(System.Int32), "demand_id");

        matDs = new DataSet(); //get data from database in a dataset        
        
        Material = new DataTable();
        DataColumn[] keys = new DataColumn[1];

        if (this.IsPostBack)
        {
            matDs = (DataSet)Session["MatDS"];
        }
        else
        {
            //matDs.Tables.Add(Material);
            matAdapt.Fill(matDs);

            if (matDs != null && matDs.Tables[0].Rows.Count > 0)    
            {

                pnlMaterials.Visible = true;

                //session variable setting
                Session["MatDS"] = matDs;

                keys[0] = matDs.Tables[0].Columns["demand_id"];
                //Set the primary key for the dataset
                matDs.Tables[0].PrimaryKey = keys;

                DataGrid2.DataSource = matDs;
                DataGrid2.DataKeyField = "demand_id";

                DataGrid2.DataBind();

                //Set the selected values in Dropdown List after update
                for (int j = 0; j <= DataGrid2.Items.Count - 1; j++)
                {
                    DropDownList dd = (DropDownList)DataGrid2.Items[j].FindControl("UsageList");
                    DataRow rr = matDs.Tables[0].Rows[j];
                    dd.SelectedValue = rr["z_udf_float_1"].ToString();

                    DropDownList ddlReason = (DropDownList)DataGrid2.Items[j].FindControl("ddlReplaceReason");
                    if (ddlReason.Visible == true)
                    {
                        //DropDownList ddlReason = (DropDownList)DataGrid2.Items[j].FindControl("ddlReplaceReason");
                        ddlReason.SelectedValue = rr["z_udf_float_2"].ToString();

                    }

                    if (rr["z_udf_float_1"].ToString() == "1" || rr["z_udf_float_1"].ToString() == "2")
                    {
                       
                        DataGrid2.Items[j].Style.Add("background-color", "#DFDFDF");
                           
                    }
                    
                }
                
            }
            else
            {
               pnlMaterials.Visible = false;
            }

        }     

        
    }

    protected void Bind_Expenses()
    {
        //Create Expense List
        string ExpCommandTxt = "select demand.bpart_id, demand.bpart_id + ' - ' + demand.bpart_descr  as bpart_desc, demand.cost, demand.demand_id from demand where demand.bpart_gcl_id='Expense' and demand.bpart_id<>'d_travel' and demand.bpart_id<>'t_travel' and demand.order_id='" + orderId + "'";

        SqlDataAdapter expAdapt = new SqlDataAdapter(ExpCommandTxt, ordercon);
        expDs = new DataSet();
        DataColumn[] keys = new DataColumn[1];

        if (this.IsPostBack)
        {
            expDs = (DataSet)Session["ExpDS"];           
        }
        else
        {
            expAdapt.Fill(expDs);

            DataGrid3.DataSource = expDs;
            DataGrid3.DataBind();

            //Assign to session variable
            Session["ExpDS"] = expDs;
                     
            //Set the selected values in Dropdown List after update
            for (int j = 0; j <= DataGrid3.Items.Count - 1; j++)
            {
                DropDownList dd = (DropDownList)DataGrid3.Items[j].FindControl("ExpDropDownList");
                DataRow rr = expDs.Tables[0].Rows[j];
                dd.SelectedValue = rr["bpart_id"].ToString();
            }
                       

        }
       
    }

   
    
    protected void Close_Connection()
    {
        this.ordercon.Close();
    }    

   
    //Add Expenses Button Click
    protected void Button1_Click(object sender, EventArgs e)
    {
      
        DataRow dr = this.expDs.Tables[0].NewRow();
        int addPos = this.expDs.Tables[0].Rows.Count;
        this.expDs.Tables[0].Rows.InsertAt(dr, addPos);
        Session["ExpDS"] = expDs;
        DataGrid3.DataSource = expDs;
        DataGrid3.DataBind();

        //Set the selected values in Dropdown List after update
        for (int j = 0; j <= DataGrid3.Items.Count - 1; j++)
        {
            DropDownList dd = (DropDownList)DataGrid3.Items[j].FindControl("ExpDropDownList");
            DataRow rr = expDs.Tables[0].Rows[j];
            dd.SelectedValue = rr["bpart_id"].ToString();


          
        }

    }

    //Populate DropDown List in Expenses through the database
    public DataTable GetGenreTable()
    {
        DataTable dtExpense = new DataTable();
        if (Application["GenreTable"] == null)
        {
            DataRow dr;
            expSql = new SqlCommand("select bpart_id,bpart_id + ' - ' + descr  as bpart_desc from bpart where bpart_gcl_id='Expense'", ordercon);
            DataColumn dc1 = new DataColumn("bpart_id");
            DataColumn dc2 = new DataColumn("bpart_desc");
           
            dtExpense.Columns.Add(dc1);
             dtExpense.Columns.Add(dc2);
            if (this.ordercon.State == ConnectionState.Closed)
            {
                this.ordercon.Open();
            }
            SqlDataReader dreader = expSql.ExecuteReader();
            while (dreader.Read())
            {
                dr = dtExpense.NewRow();
                dr[0] = dreader[0];
                dr[1] = dreader[1];
                dtExpense.Rows.Add(dr);
            }
                        
        }
        else
        {
            dtExpense = (DataTable)Application["GenreTable"];
        }
        this.ordercon.Close();
        return dtExpense;
    }

    //SAVE ORDER 
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        
        //check for email address validation
        Regex emailregex = new Regex(@"^(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\b([,;]\s?)?)*$");
        Match m1 = emailregex.Match(EmailTxt.Text);
        if (m1.Success)
        {
            //do nothing
        }
        else
        {
            ErrorLabel.Text = "Please provide valid email address(es)";
            ErrorLabel.ForeColor = System.Drawing.Color.Red;
            return;

        }

        if (txtServiceSignOff.Text.Trim() == "")
        {
            ErrorLabel.Text = "You must enter customer name who authorized this work";
            ErrorLabel.ForeColor = System.Drawing.Color.Red;
            return;
        }    

         
            //Added By Chirag For Insert button click problem
            //iterate over activities and check for unfulfilled ones
            for (int m = 0; m <= DataGrid4.Items.Count - 1; m++)
            {
                DropDownList dd = (DropDownList)DataGrid4.Items[m].FindControl("MangDropDownList");
                if (dd.SelectedValue != "")
                {
                    //Throw Error Cannot resolve Order with unfulfilled activities
                    ErrorLabel.Text = "Click insert button for Management Comments before clicking submit";
                    ErrorLabel.ForeColor = System.Drawing.Color.Red;
                    return;
                }

            }

            string p_code, r_code, c_code, resolve_txt, email, fax_no, po_id;

            Boolean is_resolved;
            if (CheckBox1.Checked == true)
                is_resolved = true;
            else
                is_resolved = false;

            p_code = PcodeDDList.SelectedValue.ToString();
            r_code = RcodeDDList.SelectedValue.ToString();
            c_code = CCodeDDList.SelectedValue.ToString();
            resolve_txt = ResolveTxt.Text.ToString();
            email = EmailTxt.Text.ToString();
            fax_no = FaxTxt.Text.ToString();
            po_id = POTxt.Text.ToString();


            if (is_resolved)
            {

                      
                    //iterate over activities and check for unfulfilled ones
                    for (int j = 0; j <= DataGrid1.Items.Count - 1; j++)
                    {

                        Label lbl = (Label)DataGrid1.Items[j].FindControl("State");
                        if (lbl.Text != "Fullfilled")
                        {
                            //Throw Error Cannot resolve Order with unfulfilled activities
                            ErrorLabel.Text = "Cannot resolve Order with open activities.";
                            ErrorLabel.ForeColor = System.Drawing.Color.Red;
                            return;
                        }                       

                    }

                    //iterate over materials and check whether parts was used or not used
                    for (int k = 0; k <= DataGrid2.Items.Count - 1; k++)
                    {
                        
                        CheckBox cb;
                        cb = (CheckBox)DataGrid2.Items[k].FindControl("MatCheck");

                      

                        if (cb.Checked)
                        {
                            DropDownList ddusage; 
                            ddusage = (DropDownList)DataGrid2.Items[k].FindControl("UsageList");
                            if (ddusage.SelectedValue.ToString() == "0")
                            {
                                //Throw Error Cannot resolve Order with unfulfilled activities
                                ErrorLabel.Text = "Please update materials grid  - part was used or not used.";
                                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }

                    }
                    

                    if (p_code == "" || r_code == "")
                    {
                        //Throw error and return
                        ErrorLabel.Text = "Problem Code and Repair Code are Mandatory";
                        ErrorLabel.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    if (BusinessLogic.CheckAddSerialNumberForInstall(orderId) == false)
                    {
                        ErrorLabel.Text = "Our records indicates that there is an install activity for this call. In order to close this call you must add a serial number using management activities.";
                        ErrorLabel.ForeColor = System.Drawing.Color.Red;
                        return;

                    }

                    /**** Check ETA date is greater than call open date ****/
                    //if (BusinessLogic.CheckETADate(orderId) == false)
                    //{
                    //    ErrorLabel.Text = "ETA date is earlier than calll open date. This call can not be closed online. Call Stefanie.";
                    //    ErrorLabel.ForeColor = System.Drawing.Color.Red;
                    //    return;
                    //}
                    
                    //Update Order
                    // Open Session for order update

                    if ((Session["SessionInfo"] == null) || (Session["UserID"] == null) || (Session["PassWd"] == null))
                    {
                        Response.Redirect("RedirectLogin.aspx", false);
                    }
                    SEALAPI.Broker session = (SEALAPI.Broker)Session["SessionInfo"];
                    int iSession=0;
                    
                    string ls_res;
                        
                    try {
                    iSession = session.OpenSession("default", Session["UserID"].ToString(), Session["PassWd"].ToString());
                    }
                    catch (Exception )
                    {
                        //do nothing
                    }
                    
                    if (iSession < 1)
                    {
                        //Throw error and return
                        ErrorLabel.Text = "API Broker Error. Please log off and login again. We apologize for any inconvenience.";
                        ErrorLabel.ForeColor = System.Drawing.Color.Red;
                        return;

                    }

                    //Update Billing Notes
                    if (txtBillingNotes.Text != "")
                    {
                        if (ordercon.State == ConnectionState.Closed)
                        {
                            ordercon.Open();
                        }

                        string BillNotesCmd = "UPDATE order_env SET back_end_env = '" + txtBillingNotes.Text.Replace("'","''") + "'  WHERE order_id = '" + orderId + "'";
                        SqlCommand updateBillNotes = new SqlCommand(BillNotesCmd, ordercon);
                        updateBillNotes.ExecuteNonQuery();
                    }


                    ///** Service Apporved By for JSOX Date: 08/25/2008 Table Name order_env  field name: modules_desc
                    if (txtServiceSignOff.Text != "")
                    {
                        string strsql = "UPDATE order_env SET modules_desc = '" + new string(' ', 2) + txtServiceSignOff.Text.Replace("'", "''").ToString() + "' WHERE order_id = '" + orderId + "'";
                        if (ordercon.State == ConnectionState.Closed)
                        {
                            ordercon.Open();
                        }
                        //Set the insert command
                        SqlCommand objCmd = new SqlCommand(strsql, ordercon);

                        objCmd.ExecuteNonQuery();

                    }


                    //Order itself
                    ls_res = session.SendService(iSession, "set module=service_order");
                    if (ls_res == "") ls_res = session.SendBO(iSession, "retrieve order_line.order_id='" + orderId + "'");
                    if (ls_res == "") ls_res = session.SendBO(iSession, "setitem service_call_pcode_id='" + p_code + "' service_call_rcode_id='" + r_code + "' service_call_cause_id='" + c_code + "' address_email_id='" + email + "' address_fax_phone_id='" + fax_no + "' service_call_env_resolve_text='" + resolve_txt.Replace("'","''") + "' order_line_po_id='" + po_id + "' order_line_is_resolved ='Y'");

                    if (ls_res == "") ls_res = session.SendBO(iSession, "save");


                   

                    //Send Report from the application
                    string send_report = System.Configuration.ConfigurationManager.AppSettings["ReportSettings"];

                    

                    if (send_report.ToLower() == "y")
                    {
                        //insert row into the custom_field_xref  table
                        //Create insert command

                        string custCommandTxt= string.Empty;
                        try
                        {
                            if (ordercon.State == ConnectionState.Closed)
                            {
                                ordercon.Open();
                            }

                            custCommandTxt = "insert into custom_field_xref (table_name,table_key,z_udf_string_1,z_udf_string_2) values ('service_order'," + " '" + orderId + "', " + "'" + email + "', " + "'" + fax_no + "')";

                            //Set the insert command
                            SqlCommand insertCommand = new SqlCommand(custCommandTxt, ordercon);

                            // custAdapt.InsertCommand = new SqlCommand(custCommandTxt);
                            insertCommand.ExecuteNonQuery();

                            if (ls_res == "") ls_res = session.SendBO(iSession, "send_report_from_appc " + orderId);

                        }
                        catch (Exception )
                        {
                           //FGSU_TOOLS.ExceptionHandling.LogExceptionToWinEvengLog("Call Closing",custCommandTxt + ' '  + ex.ToString(),System.Diagnostics.EventLogEntryType.Error);
                            
                        }
                       
                    }

                    session.CloseSession(iSession);

         

                    ///** Interested in Taskero/PSG Checkbox    * */
                    if (ChkTaskero.Checked == true)
                    {
                        string strsql = "UPDATE service_call SET is_engineering = 'Y' WHERE order_id = '" + orderId + "'";
                        if (ordercon.State == ConnectionState.Closed)
                        {
                            ordercon.Open();
                        }
                        //Set the insert command
                        SqlCommand objCmd = new SqlCommand(strsql, ordercon);

                        objCmd.ExecuteNonQuery();

                        if (ordercon.State == ConnectionState.Open)
                        {
                            ordercon.Close();

                        }
                        ordercon = null;

                    }
              
                    
                    /*
                     * Need a setting in the web.config file to send Survey Email 
                     * Validate email address and save into table for the survey               
                    */
                    string send_survey = ConfigurationManager.AppSettings["SendSurveyEmail"];
                    if (send_survey.ToLower() == "y" && email != String.Empty)
                    {

                        BusinessLogic.SurveyType surveytype;
                        surveytype = BusinessLogic.ValidateCallForSurveyEmail(orderId, p_code);
                        if (surveytype != BusinessLogic.SurveyType.NoSurvey)
                        {
                            string emailadd = BusinessLogic.ParseEmailToSendSurvey(email.Trim());
                            if (emailadd != String.Empty)
                            {
                                BusinessLogic.SaveEmailToSendSurvey(orderId, emailadd.Trim(), Convert.ToInt32(surveytype));
                            }
                        }

                    }

                    
                   
                    //go to Queue
                    Response.Redirect("AssignedOrders.aspx");      
                                

            }

            else //save the order anyway
      
            {
                // Open Session for order update
                if ((Session["SessionInfo"] == null) || (Session["UserID"] == null) || (Session["PassWd"] == null))
                {
                    Response.Redirect("RedirectLogin.aspx", false);
                }
                SEALAPI.Broker session = (SEALAPI.Broker)Session["SessionInfo"];
                int iSession=0;
                string ls_res;
                
                try {
                iSession = session.OpenSession("default", Session["UserID"].ToString(), Session["PassWd"].ToString());
                }
                catch (Exception) {
                }

                if (iSession < 1)
                {
                    //Throw error and return
                    ErrorLabel.Text = "API Broker Error. Please log off and login again. We apologize for any inconvenience.";
                    ErrorLabel.ForeColor = System.Drawing.Color.Red;
                    return;

                }

                //Order itself
                ls_res = session.SendService(iSession, "set module=service_order");
                if (ls_res == "") ls_res = session.SendBO(iSession, "retrieve order_line.order_id='" + orderId + "'");
                if (ls_res == "") ls_res = session.SendBO(iSession, "setitem service_call_pcode_id='" + p_code + "' service_call_rcode_id='" + r_code + "' service_call_cause_id='" + c_code + "' address_email_id='" + email + "' address_fax_phone_id='" + fax_no + "' service_call_env_resolve_text='" + resolve_txt + "' order_line_po_id='" + po_id + "'");

                if (ls_res == "") ls_res = session.SendBO(iSession, "save");

                session.CloseSession(iSession);

            }      
        

    }

    protected void DataGrid3_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            String expense = drv["bpart_id"].ToString();
            DropDownList ddl = (DropDownList)e.Item.FindControl("ExpDropDownList");
            ddl.DataSource = GetGenreTable();
            ddl.DataBind();
            ddl.SelectedIndex =ddl.Items.IndexOf(ddl.Items.FindByText(expense));
                       
        }
       

    }
      
        
    protected void DataGrid2_ItemDataBound(object sender,  System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
       
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
                
                DropDownList ddlReason = (DropDownList)(e.Item.FindControl("ddlReplaceReason"));

                if (e.Item.Cells[7].Text.ToString() == "1")
                {

                    ddlReason.Visible = true;
                    //DataGrid2.Columns[8].Visible = true;
                    //e.Item.Cells[8].Controls[0].Visible = true;

                    //DropDownList ddlReason = (DropDownList)(e.Item.FindControl("ddlReplaceReason"));

                    if (ddlReason != null)
                    {
                        DataRowView drv = (DataRowView)e.Item.DataItem;
                        if (drv["z_udf_float_2"].ToString() != "0")
                        {
                            ddlReason.SelectedIndex = Convert.ToInt32(drv["z_udf_float_2"]);
                        }
                        else
                        {
                            ddlReason.SelectedIndex = 0;
                        }

                    }

                }
                else
                {
                    //e.Item.Cells[8].Visible = false;

                    ddlReason.Visible = false;
                }

            }
             

        
    }

    
    protected void matUpdate_Click(object sender, EventArgs e)
    {
        int i;
        DataGridItem dgi;
        DropDownList ddusage;
        CheckBox cb ;
        DropDownList PHReason;
        
        for (i = 0; i <= DataGrid2.Items.Count - 1; i++)
        {
            dgi = DataGrid2.Items[i];
            cb = (CheckBox)dgi.FindControl("MatCheck");

            if (cb.Checked)
            {
                //fetch updatable value
                ddusage = (DropDownList)dgi.FindControl("UsageList");
                string usage = ddusage.SelectedValue.ToString();


                PHReason = (DropDownList)dgi.FindControl("ddlReplaceReason");
                string reason = PHReason.SelectedValue.ToString();

                if (usage == "0")
                {
                    MatErrorLabel.Text = "Please select whether the part was used or not used.";
                    return; 
                }
                //check reason printheas was replaced
                if (PHReason.Visible == true)
                {
                    if (reason == "0" && usage == "1")
                    {
                        MatErrorLabel.Text = "If used, specify a reason for acuity printhead replacement";
                        return;
                    }

                }
                //get key
                string demand_id = DataGrid2.DataKeys[i].ToString();
                DataRow r = matDs.Tables[0].Rows.Find(demand_id);

                // Updates the dataset table.
                //r["Description"] =
                r.BeginEdit();
                r["z_udf_float_1"] = int.Parse(usage);
                r["z_udf_float_2"] = int.Parse(reason);
                r.EndEdit();
                cb.Checked = false;
                
                
            }

        }

        if (this.ordercon.State == ConnectionState.Closed)
        {
            ordercon.Open();
        }

        //Create materials list
        string matCommandTxt = "select demand.bpart_id, demand.qty , demand_material.z_udf_float_1," + " " +
                     "demand.bpart_descr , demand.demand_id, demand.comment_text,  case when demand.bpart_id in ('3010108132', '3010106120' , '3010110416') THEN 1 else 0 end as acuity_printhead, isnull(z_udf_float_2,0) as z_udf_float_2 from demand  inner join demand_material on demand.demand_id=demand_material.demand_id" + " " +
                     "where demand.order_id='" + orderId + "'";

         matAdapt = new SqlDataAdapter(matCommandTxt, ordercon);
        //Set the update command
       
        matAdapt.UpdateCommand = new SqlCommand();
        matAdapt.UpdateCommand.Connection = ordercon;
        //matAdapt.UpdateCommand = new SqlCommand("update demand_material set z_udf_float_1=@Usage ,z_udf_float_2 = @reason where demand_id=@DemandID", ordercon);
        matAdapt.UpdateCommand.CommandText = "uspWebCallClosing_UpdateDemandMaterial";
        matAdapt.UpdateCommand.CommandType = CommandType.StoredProcedure;

        matAdapt.UpdateCommand.Parameters.Add("@Usage",
           SqlDbType.VarChar, sizeof(System.Double), "z_udf_float_1");
        matAdapt.UpdateCommand.Parameters.Add("@reason", SqlDbType.Int, sizeof(System.Int32), "z_udf_float_2");
        matAdapt.UpdateCommand.Parameters.Add("@demand_id",
           SqlDbType.Int, sizeof(System.Int32), "demand_id");
        


        matAdapt.Update(matDs);
        matDs.Clear();
        matAdapt.Fill(matDs);
        Session["MatDS"] = matDs;
        DataGrid2.DataSource = matDs;
        DataGrid2.DataBind();

        //Set the selected value after update
        for (int j = 0; j <= DataGrid2.Items.Count - 1; j++)
        {
            DropDownList dd = (DropDownList)DataGrid2.Items[j].FindControl("UsageList");
            DataRow rr = matDs.Tables[0].Rows[j];
            dd.SelectedValue = rr["z_udf_float_1"].ToString();

            
            DropDownList ddlReason = (DropDownList)DataGrid2.Items[j].FindControl("ddlReplaceReason");
            ddlReason.SelectedValue = rr["z_udf_float_2"].ToString();

            
        }
        //DataGrid2.Focus();
        DataGrid2.Enabled = false;
        ordercon.Close();       

        if (MatErrorLabel.Text != String.Empty) 
        {
            MatErrorLabel.Text = String.Empty;
        }    

    }
    
    
    protected void ActvUpdate_Click(object sender, EventArgs e)
    {
        int i,m;
        DataGridItem dgi,dgActivity;
        //DropDownList ddusage;
        TextBox tb;
       
       // string key;
        CheckBox cb,cbox;
        
        //Data Validation for Activity Grid, exit function if there some errors
        for (m = 0; m <= DataGrid1.Items.Count - 1; m++)
        {
            dgActivity = DataGrid1.Items[m];
            cbox = (CheckBox)dgActivity.FindControl("ActvCheck");

            if (cbox.Checked)
            {
                                   
                DateTime sd, st,dtStartDT, dtEndDT;
                Decimal dc;
                string dt, duration;
                
                //Validate Duration 
                tb = (TextBox)dgActivity.FindControl("TextBoxDur");
                duration = tb.Text.ToString();
                if (Decimal.TryParse(duration, out dc) == false)
                {
                    ActErrorLabel.Text = "One or more activity you are trying to update requires duration or enter 0";
                    return;
                }

                tb = (TextBox)dgActivity.FindControl("TextBoxStDate");
                dt = tb.Text.ToString();
                DateTime.TryParse(dt, out sd);
                if (sd == DateTime.MinValue)
                {
                    ActErrorLabel.Text = "One or more activity you are trying to update has an invalid start date!";
                    return;
                }
               
                tb = (TextBox)dgActivity.FindControl("TextBoxStTime");
                dt = tb.Text.ToString();
                DateTime.TryParse(dt, out dtStartDT);
                if (dtStartDT == DateTime.MinValue)
                {
                    ActErrorLabel.Text = "One or more activity you are trying to update has an invalid start time!";
                    return;
                }
                
                tb = (TextBox)dgActivity.FindControl("TextBoxEndDate");
                dt = tb.Text.ToString();
                DateTime.TryParse(dt, out st);
                if (st == DateTime.MinValue   )     {
                    ActErrorLabel.Text = "One or more activity you are trying update has an invalid end date!";
                    return;
                } else if ( st > DateTime.Now.AddDays(1.0) )
                {
                    ActErrorLabel.Text = "Future dates are not allowed as activity completion date!";
                    return;                        

                }
               
                tb = (TextBox)dgActivity.FindControl("TextBoxEndTime");
                dt = tb.Text.ToString();
                DateTime.TryParse(dt, out dtEndDT);
                if (dtEndDT == DateTime.MinValue)
                {
                    ActErrorLabel.Text = "One or more activity you are trying update has an invalid end time!";
                    return;
                }             
                
            }

        }


      
        for (i = 0; i <= DataGrid1.Items.Count - 1; i++)
        {
            dgi = DataGrid1.Items[i];
            cb = (CheckBox)dgi.FindControl("ActvCheck");
            
            if (cb.Checked)
            {
                //get key
                string stDate,  endDate;                
                TimeSpan stTime, endTime;
                DateTime sd, st;
                               
                string demand_id = DataGrid1.DataKeys[i].ToString();
                string dt, duration, travelDist;
                
                tb = (TextBox) dgi.FindControl("TextBoxDur");
                duration = tb.Text.ToString();
              
                tb = (TextBox) dgi.FindControl("TextBoxStDate");
                dt = tb.Text.ToString();
                sd = System.DateTime.Parse(dt).Date;
                stDate = sd.Month + "/" + sd.Day + "/" + sd.Year;

                tb = (TextBox)dgi.FindControl("TextBoxStTime");
                dt = tb.Text.ToString();
                stTime = System.DateTime.Parse(dt).TimeOfDay;

                tb = (TextBox)dgi.FindControl("TextBoxEndDate");
                dt = tb.Text.ToString();
                st = System.DateTime.Parse(dt).Date;
                endDate = st.Month + "/" + st.Day + "/" + st.Year;

                tb = (TextBox)dgi.FindControl("TextBoxEndTime");
                dt = tb.Text.ToString();
                endTime = System.DateTime.Parse(dt).TimeOfDay;

                tb = (TextBox)dgi.FindControl("TextBoxTravel");
                travelDist = tb.Text.ToString();
                              

                //update through API
                //Labor activities
                if ((Session["SessionInfo"] == null) || (Session["UserID"] == null) || (Session["PassWd"] == null))
                {
                    Response.Redirect("RedirectLogin.aspx", false);
                }
                
                SEALAPI.Broker session = (SEALAPI.Broker)Session["SessionInfo"];
                int iSession=0;
                string ls_res;

                try
                {
                    iSession = session.OpenSession("default", Session["UserID"].ToString(), Session["PassWd"].ToString()); 
                }
                catch (Exception)
                {
                  // do nothing                 
                }
                
                if (iSession < 1)
                {
                    ActErrorLabel.Text = "API Broker Error. Please log off and login again. We apologize for any inconvenience.";
                    ActErrorLabel.ForeColor = System.Drawing.Color.Red;
                    return; //Message Login Failed

                }
                ls_res = session.SendService(iSession, "set module=demand_labor_xref");
                if (ls_res == "") ls_res = session.SendBO(iSession, "set key=" + orderId + " validation=off i_cust_id='" + CustTxt.Text + "' i_order_type='service_order' i_sa_person ='" + Session["UserID"] + "'");
                if (ls_res == "") ls_res = session.SendBO(iSession, "retrieve demand.demand_id=" + demand_id);

                if (ls_res == "") ls_res = session.SendBO(iSession, "setitem demand_labor_arrive_dt='" + stDate + "' demand_labor_arrive_tm ='" + stTime + "' demand_is_fullfiled ='Y' demand_actual_dt='" + endDate + "' demand_actual_tm='" + endTime + "' demand_labor_duration=" + duration + " demand_labor_stop_code_id = 'RESOLVED' demand_sa_person_id='" + Session["UserID"] + "'");
                if (ls_res == "") ls_res = session.SendBO(iSession, "save");

                
                //update Expense
                if (travelDist != "")
                {
                    ls_res = session.SendService(iSession, "set module=web_demand_travel_xref");
                    if (ls_res == "") ls_res = session.SendBO(iSession, "go new demand_order_id='" + orderId + "' demand_link_xref_fr_demand_id = " + demand_id + " demand_bpart_id='d_travel' demand_demand_type_id='Travel_To' demand_qty =" + travelDist);
                    if (ls_res == "") ls_res = session.SendBO(iSession, "save");
                }

                //Close Session
                session.CloseSession(iSession); //close session

            }
           
        }
        //Refresh Page
        this.Response.Redirect(Page.Request.Url.ToString());
        
       
    }
    protected void ExpUpdate_Click(object sender, EventArgs e)
    {

        //string p_code = DropDownList1.SelectedValue.ToString();
        int i;
        DataGridItem dgi;
        DropDownList ddexp;
        CheckBox cb;
        TextBox tb;


        for (i = 0; i <= DataGrid3.Items.Count - 1; i++)
        {
            dgi = DataGrid3.Items[i];
            cb = (CheckBox)dgi.FindControl("ExpCheck");

            if (cb.Checked)
            {
               
                string bpartID, cost;

                //get key
                tb = (TextBox)dgi.FindControl("TextBoxDemand");
                string demand_id = tb.Text.ToString();
                
                
                //fetch updatable value
                ddexp = (DropDownList)dgi.FindControl("ExpDropDownList");
                bpartID = ddexp.SelectedValue.ToString();

                tb = (TextBox)dgi.FindControl("TextBoxCost");
                cost = tb.Text.ToString();
               


                // Open Session
                if ((Session["SessionInfo"] == null) || (Session["UserID"] == null) || (Session["PassWd"] == null))
                {
                    Response.Redirect("RedirectLogin.aspx", false);
                }
                SEALAPI.Broker session = (SEALAPI.Broker)Session["SessionInfo"];
                int iSession=0;
                string ls_res;
          
                try
                {
                    iSession = session.OpenSession("default", Session["UserID"].ToString(), Session["PassWd"].ToString());
                }
                catch (Exception)
                {
                                       
                }
                if (iSession < 1)
                {
                    ExpErrorLabel.Text = "API Broker Error. Please log off and login again. We apologize for any inconvenience.";
                    ExpErrorLabel.ForeColor = System.Drawing.Color.Red;
                    return; //Message Login Failed

                }
                //expense start
                ls_res = session.SendService(iSession, "set module=demand_expense_xref");
                if (ls_res == "") ls_res = session.SendBO(iSession, "set key=" + orderId + " validation=off i_cust_id='" + CustTxt.Text + "' i_order_type='service_order' i_sa_person ='" + Session["UserID"] + "'");
               
                if (demand_id == "")
                {
                    //new demand creation
                    if (ls_res == "") ls_res = session.SendBO(iSession, "go new  demand_bpart_id='" + bpartID + "' demand_cost =" + cost);
                }
                else
                {
                    if (ls_res == "") ls_res = session.SendBO(iSession, "retrieve demand.demand_id=" + demand_id);
                    if (ls_res == "") ls_res = session.SendBO(iSession, "setitem demand_bpart_id='" + bpartID + "' demand_cost =" + cost);
                }


                if (ls_res == "") ls_res = session.SendBO(iSession, "save");

                //expense ends
                cb.Checked = false;
                session.CloseSession(iSession);
                

               
            }

        }
        
        //set Focus on Expenses
        DataGrid3.Focus();
       
    }



    protected void BackList_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("EnovationLogin.aspx", false);
        }
        else
        {
            Response.Redirect("AssignedOrders.aspx");
        }
    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {

        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("EnovationLogin.aspx");
    }

    protected void AddMngt_Click(object sender, EventArgs e)
    {
        
        
        //Create dummy dataset with a blank row
        string MangCommandTxt = "select '' as bpart_id, '' as descr";

        SqlDataAdapter mangAdapt = new SqlDataAdapter(MangCommandTxt, ordercon);
        mangDS = new DataSet();      
        mangAdapt.Fill(mangDS);

        DataGrid4.DataSource = mangDS;
        DataGrid4.DataBind();
        //Assign values to dropdown list
        DropDownList ddm = (DropDownList)DataGrid4.Items[0].FindControl("MangDropDownList");
       
        ddm.DataSource = GetMangData();
        ddm.DataBind();
        DataGrid4.Focus();

    }

    //Populate DropDown List in Expenses through the database
    public DataTable GetMangData()
    {
        DataTable dtExpense = new DataTable();
        if (Application["MangTable"] == null)
        {
            DataRow dr;
            mangSql = new SqlCommand("SELECT bpart.bpart_id,  bpart.descr FROM bpart,actv WHERE ( bpart.bpart_id = actv.bpart_id )  and actv.is_management = 'Y' and is_astea = 'N' order by bpart.bpart_id", ordercon);
            DataColumn dc = new DataColumn("bpart_id");
            dtExpense.Columns.Add(dc);
            if (this.ordercon.State == ConnectionState.Closed)
            {
                this.ordercon.Open();
            }
            SqlDataReader dreader = mangSql.ExecuteReader();
            while (dreader.Read())
            {
                dr = dtExpense.NewRow();
                dr[0] = dreader[0];
                dtExpense.Rows.Add(dr);
            }

        }
        else
        {
            dtExpense = (DataTable)Application["MangTable"];
        }
        this.ordercon.Close();
        return dtExpense;
    }
    protected void MangInsert_Click(object sender, EventArgs e)
    {
        int i;
        DataGridItem dgi;
        DropDownList ddmang;
        //CheckBox cb;
        TextBox tb;


        for (i = 0; i <= DataGrid4.Items.Count - 1; i++)
        {
            dgi = DataGrid4.Items[i];
            string bpartID, comment;                

            //fetch updatable value
            ddmang = (DropDownList)dgi.FindControl("MangDropDownList");
            bpartID = ddmang.SelectedValue.ToString();

            tb = (TextBox)dgi.FindControl("TextBoxComment");
            comment = tb.Text.ToString();

            // Open Session
            if ((Session["SessionInfo"] == null) || (Session["UserID"] == null) || (Session["PassWd"] == null))
            {
                Response.Redirect("EnovationLogin.aspx", false);
            }
            SEALAPI.Broker session = (SEALAPI.Broker)Session["SessionInfo"];
            int iSession=0;
            string ls_res;
            try
            {
                iSession = session.OpenSession("default", Session["UserID"].ToString(), Session["PassWd"].ToString());
            }
            catch (Exception)
            {                
               
            }
            if (iSession < 1)
            {
                MangErrorLabel.Text = "Cannot Insert Comments. Please log off and login again.";
                MangErrorLabel.ForeColor = System.Drawing.Color.Red;
                return; //Message Login Failed

            }
            //management activity insert start
            ls_res = session.SendService(iSession, "set module=demand_mngt_xref");
            if (ls_res == "") ls_res = session.SendBO(iSession, "set key=" + orderId + " validation=off i_cust_id='" + CustTxt.Text + "' i_order_type='service_order'");

            if (ls_res == "") ls_res = session.SendBO(iSession, "go new");
            if (ls_res == "") ls_res = session.SendBO(iSession, "setitem demand_bpart_id='" + bpartID + "' demand_comment_text ='" + comment+"'");


            if (ls_res == "") ls_res = session.SendBO(iSession, "save");

            //management actv insert ends
            session.CloseSession(iSession);
            mangDS = null;
            ErrorLabel.Text = "";
            DataGrid4.DataSource = mangDS;
            DataGrid4.DataBind();

        }
        
    }
    protected void SaImgButton_Click(object sender, ImageClickEventArgs e)
    {
        //Create dummy dataset with a blank row
        DataSet saDS;
        string SaCommandTxt = "select person.email_id, person.fax_phone_id from person, employee where "+ 
        " person.person_id=employee.person_id and employee.person_id='"+Session["UserID"].ToString()+"'";

        SqlDataAdapter saAdapt = new SqlDataAdapter(SaCommandTxt, ordercon);
        saDS = new DataSet();
        saAdapt.Fill(saDS);
        if (saDS.Tables[0].Rows.Count > 0)
        {
            string email = saDS.Tables[0].Rows[0]["email_id"].ToString();
            string fax = saDS.Tables[0].Rows[0]["fax_phone_id"].ToString();
            if (EmailTxt.Text == "")
            {
                EmailTxt.Text += email;
            }
            else
            {
                EmailTxt.Text += ";" + email;
            }

            SaImgButton.Enabled = false;
            
              
        }
                    

    }

    //Author: Chirag Shah Last Modified Date: 01/21/2008
    public void Page_Error(object sender, EventArgs e)
    {
        
        try
        { 
        Exception objErr = Server.GetLastError().GetBaseException();
        string err = "<b>Error Caught in Page_Error event</b><hr><br>" +
                "<br><b>Error in: </b>" + Request.Url.ToString() +
                "<br><b>Error Message: </b>" + objErr.Message.ToString() +
                "<br><b>Stack Trace:</b><br>" +   objErr.StackTrace.ToString();
        Response.Write(err.ToString());
        
        /*  Insert error in to the database table */
        Set_Connection();

        //insert row into the error log  table
        //Create insert command
        if (ordercon.State == ConnectionState.Closed)
        {
            ordercon.Open();
        }

        string errCommandTxt = "insert into callclosing_errorlog (Error_URL,Error_Message,StackTrace) values (" + "'" + Request.Url.ToString() + "', " + "'" + objErr.Message.ToString() + "', " + "'" + objErr.StackTrace.ToString() + "')";

        //Set the insert command
        SqlCommand insertCommand = new SqlCommand(errCommandTxt, ordercon);
        // custAdapt.InsertCommand = new SqlCommand(custCommandTxt);
        insertCommand.ExecuteNonQuery();

        Close_Connection();
        
        Server.ClearError();
        }
        catch (Exception) 
        {
            
        }


    }

    //Cancle button Management Activities Grid
    protected void btnCancelMgt_Click(object sender, EventArgs e)
    {
        DataGrid4.DataSource = null;
        DataGrid4.DataBind();
        MangErrorLabel.Text = "";
        if (ErrorLabel.Text == "Click insert button for Management Comments before clicking submit")
        {
            ErrorLabel.Text = "";
        }
        

    }

    protected void btnCancelExpense_Click(object sender, EventArgs e)
    {
        this.expDs.Tables[0].Rows.Clear();
        DataGrid3.DataSource = null;
        DataGrid3.DataBind();
        ExpErrorLabel.Text = "";
    }

    //validate time using regular expressions
    public bool IsValidTime(string thetime)
    {
        Regex checktime = new Regex(@"^((0?[1-9]|1[012])(:[0-5]\d){0,2}(\ [AP]M))$|^([01]\d|2[0-3])(:[0-5]\d){0,2}$");

        return checktime.IsMatch(thetime);
    }

    //Save Resolve Text for that Call
    protected void btnSaveResolveTxt_Click(object sender, EventArgs e)
    {
        string resolve_txt;

        resolve_txt = ResolveTxt.Text.ToString();

       //open connection to the db
      
        Set_Connection();

       
        //Check whether connection is open 
        if (ordercon.State == ConnectionState.Closed)
        {
            ordercon.Open();
        }

        //save resolve text into the db ResolveTxt
        string RTextCmdTxt = "UPDATE service_call_env SET resolve_text =  '" + resolve_txt.Replace("'","''") + "' WHERE order_id = '" + orderId + "'" ;

        SqlCommand CmdObj = new SqlCommand(RTextCmdTxt, ordercon);

        CmdObj.ExecuteNonQuery();

        Close_Connection();
        
       //

    }


    protected void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            //Durtion Textbox
            TextBox TextBoxDur = (TextBox)e.Item.FindControl("TextBoxDur");
            if (TextBoxDur != null)
            {
                TextBoxDur.Attributes.Add("onkeydown", "TabOnEnterKey();");
            }

            //Start Date
            TextBox TextBoxStDate = (TextBox)e.Item.FindControl("TextBoxStDate");
            if (TextBoxStDate != null)
            {
                TextBoxStDate.Attributes.Add("onkeydown", "TabOnEnterKey();");
            }

            //Start Time
            TextBox TextBoxStTime = (TextBox)e.Item.FindControl("TextBoxStTime");
            if (TextBoxStTime != null)
            {
                TextBoxStTime.Attributes.Add("onkeydown", "TabOnEnterKey();");
            }

            //End Date
            TextBox TextBoxEndDate = (TextBox)e.Item.FindControl("TextBoxEndDate");
            if (TextBoxEndDate != null)
            {
                TextBoxEndDate.Attributes.Add("onkeydown", "TabOnEnterKey();");
            }

            //TextBoxEndTime
            TextBox TextBoxEndTime = (TextBox)e.Item.FindControl("TextBoxEndTime");
            if (TextBoxEndTime != null)
            {
                TextBoxEndTime.Attributes.Add("onkeydown", "TabOnEnterKey();");
            }

            //TextBoxTravel
            TextBox TextBoxTravel = (TextBox)e.Item.FindControl("TextBoxTravel");
            if (TextBoxTravel != null)
            {
                TextBoxTravel.Attributes.Add("onKeyPress", "SwallowEnter();");
            }

        }
    }
       
}  //class ends

