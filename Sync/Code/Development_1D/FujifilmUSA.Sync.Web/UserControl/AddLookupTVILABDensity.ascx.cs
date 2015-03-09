using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using System.IO;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddLookupTVILABDensity : BaseControl, IViewLookupTVILABDensity
    {
        #region "Declaration"
        private PresenterLookupTVILABDensity _presenterlookupTVILABDensity;

        private const string _recSync1DLookupValues = "lblHeaderResource1.Text";
        private const string _recPlzSelectPrintStandardType = "PlzSelectPrintStandardType";
        private const string _recSelectPrintStandardTypeText = "selectPrintStandardTypeText";

        #endregion

        public AddLookupTVILABDensity()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        protected override void OnInit(EventArgs e)
        {
            _presenterlookupTVILABDensity = new PresenterLookupTVILABDensity(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
          
            if (Page.IsPostBack) return;

            lblHeader.Text = GetResourceString(_recSync1DLookupValues); //"Add Sync1D Lookup Values";            
            BindPrintStandardTypeDropDown();
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            PrintStandardTypeID = Convert.ToInt16(cmbPrintStandardType.SelectedValue);
            PrintStandardLAB = GetTargetLAB();
            PrintStandardTVI = GetPrintStandardTVIList();
            PrintStandardDensity = GetProjectTargetDensityList();
            if (PrintStandardLAB.Count == 0 || PrintStandardTVI.Count == 0 || PrintStandardDensity.Count == 0)
            {
                fujiStatusbar.DisplayMessage(Common.ResourceManager.GetResourceMessage("Error_AddLookupTVILABDensity_MissingData"), false);
            }
            else
            {
                _presenterlookupTVILABDensity.SaveLookupTVILABDensity();
                if (IsErrorOccured) return;
                Response.Redirect("Settings.aspx");
            }
        }

        protected void cmbPrintStandardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrintStandardTypeID = Convert.ToInt16(cmbPrintStandardType.SelectedValue);
            rbt_ProjectType.SelectedIndex = -1;
            BindPrintStandardLAB();
            BindPrintStandardTVI();
            BindPrintStandardDensity();
        }

        protected void lvLAB_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //----Just use this event to remove NULL tolerances display in textboxes when we edit MetricTargetPatchPreset----//
            if (e.Item is ListViewDataItem)
            {
                ListViewDataItem item = (ListViewDataItem)e.Item;

                TextBox txtTargetL = (TextBox)e.Item.FindControl("txtTargetL");
                if (txtTargetL.Text == "-1")
                {
                    txtTargetL.Text = "";
                }
                TextBox txtTargeta = (TextBox)e.Item.FindControl("txtTargeta");
                if (txtTargeta.Text == "-1")
                {
                    txtTargeta.Text = "";
                }
                TextBox txtTargetb = (TextBox)e.Item.FindControl("txtTargetb");
                if (txtTargetb.Text == "-1")
                {
                    txtTargetb.Text = "";
                }
            }
        }

        protected void rbt_ProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectTypeID = Convert.ToInt16(rbt_ProjectType.SelectedValue);
            BindDummyPrintStandardLAB();
        }

        #region Methods
        private void BindPrintStandardTypeDropDown()
        {
            cmbPrintStandardType.Items.Clear();

            _presenterlookupTVILABDensity.PrintStandardTypeList();
            if (!IsErrorOccured)
            {
                cmbPrintStandardType.DataTextField = "Value";
                cmbPrintStandardType.DataValueField = "Key";
                cmbPrintStandardType.DataSource = PrintStandardTypeList;
                cmbPrintStandardType.DataBind();
            }

            cmbPrintStandardType.Items.Insert(0, new ListItem(GetResourceString(_recSelectPrintStandardTypeText), "0"));//-- Select Print Standard Type --
        }

        private void BindPrintStandardTVI()
        {
            ClearPrintStandardTVI();
            _presenterlookupTVILABDensity.GetPrintStandardTVI();
            if (!IsErrorOccured)
            {
                BindPrintStandardTVI(PrintStandardTVI);
            }
        }

        private void BindPrintStandardLAB()
        {
            _presenterlookupTVILABDensity.GetPrintStandardLAB();
            if (!IsErrorOccured)
            {
                lvLAB.DataSource = PrintStandardLAB;
                lvLAB.DataBind();
                if (PrintStandardLAB.Count == 0)
                {
                    TR_FirstTime.Visible = true;
                }
                else
                {
                    TR_FirstTime.Visible = false;
                }
            }
        }

        private void BindPrintStandardDensity()
        {
            ClearPrintStandardDensity();
            _presenterlookupTVILABDensity.GetPrintStandardDensity();
            if (!IsErrorOccured)
            {
                BindPrintStandardDensity(PrintStandardDensity);
            }
        }

        private void BindPrintStandardTVI(List<Entity.CPSync1D.PrintStandardTVI> PrintStandardTVI)
        {
            foreach (var item in PrintStandardTVI)
            {
                switch (item.PatchType.Type.ToString())
                {
                    //--Cyan for all CMY patches----//
                    case "Cyan10": case "Cyan_10":
                        txtTVI10CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan20": case "Cyan_20":
                        txtTVI20CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan25": case "Cyan_25": case "Cyan Quarter Tone": case "Cyan_Quarter_Tone":
                        txtTVI25CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan30": case "Cyan_30":
                        txtTVI30CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan40": case "Cyan_40":
                        txtTVI40CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan50": case "Cyan_50": case "Cyan Half Tone": case "Cyan_Half_Tone":
                        txtTVI50CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan60": case "Cyan_60":
                        txtTVI60CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan70": case "Cyan_70":
                        txtTVI70CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan75": case "Cyan_75": case "Cyan Three Quarter Tone": case "Cyan_Three_Quarter_Tone":
                        txtTVI75CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan80": case "Cyan_80":
                        txtTVI80CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan90": case "Cyan_90":
                        txtTVI90CMY.Text = item.TVI.ToString();
                        break;
                    //----For Black patches----//
                    case "Black10": case "Black_10":
                        txtTVI10K.Text = item.TVI.ToString();
                        break;
                    case "Black20": case "Black_20":
                        txtTVI20K.Text = item.TVI.ToString();
                        break;
                    case "Black25": case "Black_25": case "Black Quarter Tone": case "Black_Quarter_Tone":
                        txtTVI25K.Text = item.TVI.ToString();
                        break;
                    case "Black30": case "Black_30":
                        txtTVI30K.Text = item.TVI.ToString();
                        break;
                    case "Black40": case "Black_40":
                        txtTVI40K.Text = item.TVI.ToString();
                        break;
                    case "Black50": case "Black_50": case "Black Half Tone": case "Black_Half_Tone":
                        txtTVI50K.Text = item.TVI.ToString();
                        break;
                    case "Black60": case "Black_60":
                        txtTVI60K.Text = item.TVI.ToString();
                        break;
                    case "Black70": case "Black_70":
                        txtTVI70K.Text = item.TVI.ToString();
                        break;
                    case "Black75": case "Black_75": case "Black Three Quarter Tone": case "Black_Three_Quarter_Tone":
                        txtTVI75K.Text = item.TVI.ToString();
                        break;
                    case "Black80": case "Black_80":
                        txtTVI80K.Text = item.TVI.ToString();
                        break;
                    case "Black90": case "Black_90":
                        txtTVI90K.Text = item.TVI.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ClearPrintStandardTVI()
        {
            txtTVI10CMY.Text = "";
            txtTVI20CMY.Text = "";
            txtTVI25CMY.Text = "";
            txtTVI30CMY.Text = "";
            txtTVI40CMY.Text = "";
            txtTVI50CMY.Text = "";
            txtTVI60CMY.Text = "";
            txtTVI70CMY.Text = "";
            txtTVI75CMY.Text = "";
            txtTVI80CMY.Text = "";
            txtTVI90CMY.Text = "";
            txtTVI10K.Text = "";
            txtTVI20K.Text = "";
            txtTVI25K.Text = "";
            txtTVI30K.Text = "";
            txtTVI40K.Text = "";
            txtTVI50K.Text = "";
            txtTVI60K.Text = "";
            txtTVI70K.Text = "";
            txtTVI75K.Text = "";
            txtTVI80K.Text = "";
            txtTVI90K.Text = "";
        }

        private void BindPrintStandardDensity(List<Entity.CPSync1D.ProjectTargetDensity> ProjectTargetDensity)
        {
            foreach (var item in ProjectTargetDensity)
            {
                switch (item.PatchType.Type.ToString())
                {
                    case "Cyan":
                        txtDensityC.Text = item.Target.ToString();
                        break;
                    case "Magenta":
                        txtDensityM.Text = item.Target.ToString();
                        break;
                    case "Yellow":
                        txtDensityY.Text = item.Target.ToString();
                        break;
                    case "Black":
                        txtDensityK.Text = item.Target.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ClearPrintStandardDensity()
        {
            txtDensityC.Text = "";
            txtDensityM.Text = "";
            txtDensityY.Text = "";
            txtDensityK.Text = "";
        }

        private List<Entity.CPSync1D.ProjectTargetLAB> GetTargetLAB()
        {
            List<Entity.CPSync1D.ProjectTargetLAB> targetLAB = new List<Entity.CPSync1D.ProjectTargetLAB>();
            Entity.CPSync1D.ProjectTargetLAB itemLAB = new Entity.CPSync1D.ProjectTargetLAB();
            foreach (ListViewDataItem item in lvLAB.Items)
            {
                itemLAB = new Entity.CPSync1D.ProjectTargetLAB();
                TextBox txtPatchTargetL = item.FindControl("txtTargetL") as TextBox;
                if (txtPatchTargetL != null)
                    itemLAB.TargetL = Convert.ToDecimal((string.IsNullOrEmpty(txtPatchTargetL.Text.Trim()) ? "0" : txtPatchTargetL.Text.Trim()));

                TextBox txtPatchTargetA = item.FindControl("txtTargeta") as TextBox;
                if (txtPatchTargetA != null)
                    itemLAB.TargetA = Convert.ToDecimal((string.IsNullOrEmpty(txtPatchTargetA.Text.Trim()) ? "0" : txtPatchTargetA.Text.Trim()));

                TextBox txtPatchTargetB = item.FindControl("txtTargetb") as TextBox;
                if (txtPatchTargetB != null)
                    itemLAB.TargetB = Convert.ToDecimal((string.IsNullOrEmpty(txtPatchTargetB.Text.Trim()) ? "0" : txtPatchTargetB.Text.Trim()));

                Label lblPatchTypeID = item.FindControl("lblPatchTypeID") as Label;
                Label lblPatchType = item.FindControl("lblPatchType") as Label;
                if (lblPatchTypeID != null)
                    itemLAB.PatchType = new PatchType() { PatchTypeId = Convert.ToInt16(lblPatchTypeID.Text), Type = lblPatchType.Text };

                if (itemLAB.PatchType.PatchTypeId > 9 && itemLAB.TargetL == 0 && itemLAB.TargetA == 0 && itemLAB.TargetB == 0)
                {
                    //---Don't add if its Gray patches with no values
                }
                else
                {
                    targetLAB.Add(itemLAB);
                }
            }
            return targetLAB;
        }

        private List<Entity.CPSync1D.PrintStandardTVI> GetPrintStandardTVIList()
        {
            List<Entity.CPSync1D.PrintStandardTVI> printStandardTVIList = new List<Entity.CPSync1D.PrintStandardTVI>();

            #region ---For Cyan Color
            //--older codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_Quarter_Tone,
                    Type = CPPatchType.Cyan_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_Half_Tone,
                    Type = CPPatchType.Cyan_Half_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_Three_Quarter_Tone,
                    Type = CPPatchType.Cyan_Three_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            //--newer Codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_10,
                    Type = CPPatchType.Cyan_10.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI10CMY.Text.Trim()) ? "0" : txtTVI10CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_20,
                    Type = CPPatchType.Cyan_20.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI20CMY.Text.Trim()) ? "0" : txtTVI20CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_25,
                    Type = CPPatchType.Cyan_25.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_30,
                    Type = CPPatchType.Cyan_30.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI30CMY.Text.Trim()) ? "0" : txtTVI30CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_40,
                    Type = CPPatchType.Cyan_40.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI40CMY.Text.Trim()) ? "0" : txtTVI40CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_50,
                    Type = CPPatchType.Cyan_50.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_60,
                    Type = CPPatchType.Cyan_60.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI60CMY.Text.Trim()) ? "0" : txtTVI60CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_70,
                    Type = CPPatchType.Cyan_70.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI70CMY.Text.Trim()) ? "0" : txtTVI70CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_75,
                    Type = CPPatchType.Cyan_75.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_80,
                    Type = CPPatchType.Cyan_80.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI80CMY.Text.Trim()) ? "0" : txtTVI80CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_90,
                    Type = CPPatchType.Cyan_90.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI90CMY.Text.Trim()) ? "0" : txtTVI90CMY.Text.Trim()))
            });
            #endregion

            #region ---For Magenta Color
            //--older codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_Quarter_Tone,
                    Type = CPPatchType.Magenta_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_Half_Tone,
                    Type = CPPatchType.Magenta_Half_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_Three_Quarter_Tone,
                    Type = CPPatchType.Magenta_Three_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            //---newer codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_10,
                    Type = CPPatchType.Magenta_10.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI10CMY.Text.Trim()) ? "0" : txtTVI10CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_20,
                    Type = CPPatchType.Magenta_20.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI20CMY.Text.Trim()) ? "0" : txtTVI20CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_25,
                    Type = CPPatchType.Magenta_25.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_30,
                    Type = CPPatchType.Magenta_30.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI30CMY.Text.Trim()) ? "0" : txtTVI30CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_40,
                    Type = CPPatchType.Magenta_40.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI40CMY.Text.Trim()) ? "0" : txtTVI40CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_50,
                    Type = CPPatchType.Magenta_50.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_60,
                    Type = CPPatchType.Magenta_60.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI60CMY.Text.Trim()) ? "0" : txtTVI60CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_70,
                    Type = CPPatchType.Magenta_70.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI70CMY.Text.Trim()) ? "0" : txtTVI70CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_75,
                    Type = CPPatchType.Magenta_75.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_80,
                    Type = CPPatchType.Magenta_80.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI80CMY.Text.Trim()) ? "0" : txtTVI80CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_90,
                    Type = CPPatchType.Magenta_90.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI90CMY.Text.Trim()) ? "0" : txtTVI90CMY.Text.Trim()))
            });
            #endregion

            #region ---For Yellow Color
            //--older codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_Quarter_Tone,
                    Type = CPPatchType.Yellow_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_Half_Tone,
                    Type = CPPatchType.Yellow_Half_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_Three_Quarter_Tone,
                    Type = CPPatchType.Yellow_Three_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            //--newer codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_10,
                    Type = CPPatchType.Yellow_10.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI10CMY.Text.Trim()) ? "0" : txtTVI10CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_20,
                    Type = CPPatchType.Yellow_20.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI20CMY.Text.Trim()) ? "0" : txtTVI20CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_25,
                    Type = CPPatchType.Yellow_25.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_30,
                    Type = CPPatchType.Yellow_30.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI30CMY.Text.Trim()) ? "0" : txtTVI30CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_40,
                    Type = CPPatchType.Yellow_40.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI40CMY.Text.Trim()) ? "0" : txtTVI40CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_50,
                    Type = CPPatchType.Yellow_50.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_60,
                    Type = CPPatchType.Yellow_60.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI60CMY.Text.Trim()) ? "0" : txtTVI60CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_70,
                    Type = CPPatchType.Yellow_70.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI70CMY.Text.Trim()) ? "0" : txtTVI70CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_75,
                    Type = CPPatchType.Yellow_75.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_80,
                    Type = CPPatchType.Yellow_80.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI80CMY.Text.Trim()) ? "0" : txtTVI80CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_90,
                    Type = CPPatchType.Yellow_90.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI90CMY.Text.Trim()) ? "0" : txtTVI90CMY.Text.Trim()))
            });
            #endregion

            #region ---For Black Color
            //--older codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_Quarter_Tone,
                    Type = CPPatchType.Black_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25K.Text.Trim()) ? "0" : txtTVI25K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_Half_Tone,
                    Type = CPPatchType.Black_Half_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50K.Text.Trim()) ? "0" : txtTVI50K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_Three_Quarter_Tone,
                    Type = CPPatchType.Black_Three_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75K.Text.Trim()) ? "0" : txtTVI75K.Text.Trim()))
            });
            //--newer codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_10,
                    Type = CPPatchType.Black_10.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI10K.Text.Trim()) ? "0" : txtTVI10K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_20,
                    Type = CPPatchType.Black_20.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI20K.Text.Trim()) ? "0" : txtTVI20K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_25,
                    Type = CPPatchType.Black_25.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25K.Text.Trim()) ? "0" : txtTVI25K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_30,
                    Type = CPPatchType.Black_30.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI30K.Text.Trim()) ? "0" : txtTVI30K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_40,
                    Type = CPPatchType.Black_40.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI40K.Text.Trim()) ? "0" : txtTVI40K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_50,
                    Type = CPPatchType.Black_50.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50K.Text.Trim()) ? "0" : txtTVI50K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_60,
                    Type = CPPatchType.Black_60.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI60K.Text.Trim()) ? "0" : txtTVI60K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_70,
                    Type = CPPatchType.Black_70.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI70K.Text.Trim()) ? "0" : txtTVI70K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_75,
                    Type = CPPatchType.Black_75.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75K.Text.Trim()) ? "0" : txtTVI75K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_80,
                    Type = CPPatchType.Black_80.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI80K.Text.Trim()) ? "0" : txtTVI80K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_90,
                    Type = CPPatchType.Black_90.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI90K.Text.Trim()) ? "0" : txtTVI90K.Text.Trim()))
            });
            #endregion

            printStandardTVIList.RemoveAll(p => p.TVI == 0); //Remove all entries which TVI is not set or 0
            return printStandardTVIList;
        }

        private List<Entity.CPSync1D.ProjectTargetDensity> GetProjectTargetDensityList()
        {
            List<Entity.CPSync1D.ProjectTargetDensity> ProjectTargetDensityList = new List<Entity.CPSync1D.ProjectTargetDensity>();

            #region ---For CMYK Colors
            ProjectTargetDensityList.Add(new Entity.CPSync1D.ProjectTargetDensity()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan,
                    Type = CPPatchType.Cyan.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtDensityC.Text.Trim()) ? "0" : txtDensityC.Text.Trim())),
            });
            ProjectTargetDensityList.Add(new Entity.CPSync1D.ProjectTargetDensity()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta,
                    Type = CPPatchType.Magenta.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtDensityM.Text.Trim()) ? "0" : txtDensityM.Text.Trim())),
            });
            ProjectTargetDensityList.Add(new Entity.CPSync1D.ProjectTargetDensity()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow,
                    Type = CPPatchType.Yellow.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtDensityY.Text.Trim()) ? "0" : txtDensityY.Text.Trim())),
            });
            ProjectTargetDensityList.Add(new Entity.CPSync1D.ProjectTargetDensity()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black,
                    Type = CPPatchType.Black.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtDensityK.Text.Trim()) ? "0" : txtDensityK.Text.Trim())),
            });
            #endregion

            return ProjectTargetDensityList;
        }

        private void BindDummyPrintStandardLAB()
        {
            _presenterlookupTVILABDensity.GetDummyPrintStandardLAB();
            if (!IsErrorOccured)
            {
                lvLAB.DataSource = PrintStandardLAB;
                lvLAB.DataBind();
            }
        }

        #endregion


        #region IViewLookupTVILABDensity Members
        public short PrintStandardTypeID { get; set; }
        public Dictionary<int, string> PrintStandardTypeList { get; set; }
        public List<Entity.CPSync1D.PrintStandardTVI> PrintStandardTVI { get; set; }
        public List<Entity.CPSync1D.ProjectTargetLAB> PrintStandardLAB { get; set; }
        public List<Entity.CPSync1D.ProjectTargetDensity> PrintStandardDensity { get; set; }
        public short ProjectTypeID { get; set; }
        #endregion

    }
}