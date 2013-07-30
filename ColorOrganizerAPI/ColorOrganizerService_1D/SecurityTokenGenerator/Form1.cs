using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Service.Common.Business;

namespace SecurityTokenGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillGroupName();
        }

        private void FillGroupName()
        {
            cmbGroupName.DataSource = typeof (ServiceGroup).GetEnumValues();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if(cmbGroupName.SelectedValue.ToString().Equals("NONE",StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show("Please select service group");
                return;
            }
            txtGeneratedToken.Text = UserBusiness.GenerateSecurityToken((ServiceGroup) Enum.Parse(typeof (ServiceGroup), cmbGroupName.SelectedValue.ToString()));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
