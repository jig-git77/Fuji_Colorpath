using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using Telerik.WebControls;
using System.Web.UI;

namespace Fuji.Business
{
    public class FilterColumnByDate : Telerik.WebControls.GridTemplateColumn
    {
        #region Contstructor
        public FilterColumnByDate()
        {

        }
        #endregion

        #region Events
        protected override void SetupFilterControls(TableCell cell)
        {
            //========= Create the user control for the datepicker of the =========//
            Control control = cell.Page.LoadControl("~/DatePicker.ascx");
            //Control control1 = cell.Page.LoadControl("~/DatePicker1.ascx");
            //========= Assign the id of the User control of the datepicker =========//
            
            control.ID = "Combo" + this.UniqueName;            
            //========= Load the user control of the datepicker =========//
            cell.Controls.Add(control);
            //=========== Find the button from the datepicker ==========//
            Button Button1 = (Button)control.FindControl("Button1");
            //=========== Assign the commang argument to the button==========//
            Button1.CommandArgument = this.UniqueName;
        }

        protected override void SetCurrentFilterValueToControl(TableCell cell)
        {
            base.SetCurrentFilterValueToControl(cell);
            //=========== Find the panel from the cell ==========//
            Panel PNL_DATE_PICKER = (Panel)cell.Controls[0].FindControl("PNL_DATE_PICKER");
            //=========== Find the date picker from the control ==========//
            RadDatePicker Rdp_Start = (RadDatePicker)PNL_DATE_PICKER.FindControl("RadDatePicker1");
            //=========== Check whether the start date session is null or not ==========//
            if (Fuji.Common.Sessions.GetSession("StartDate") != null )
            {
                //=========== Get the value of the date from thje session of the date ==========//
                DateTime startDate = DateTime.Parse(Fuji.Common.Sessions.GetSession("StartDate").ToString());
                //=========== Assign the date from the session of the selected date of the datepicker ==========//
                if (startDate != DateTime.MinValue)
                {
                    Rdp_Start.SelectedDate = startDate;
                }
            }
            if (Fuji.Common.Sessions.GetSession("StartDate2") != null)
            {
                //=========== Get the value of the date from thje session of the date ==========//
                DateTime startDate = DateTime.Parse(Fuji.Common.Sessions.GetSession("StartDate2").ToString());
                //=========== Assign the date from the session of the selected date of the datepicker ==========//
                if (startDate != DateTime.MinValue)
                {
                    Rdp_Start.SelectedDate = startDate;
                }
            }
        }
        #endregion
    }
}
