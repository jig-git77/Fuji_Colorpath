using System;
using System.Windows.Forms;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace CredentialEncryption
{
    public partial class frmCredentialEncryption : Form
    {
        public frmCredentialEncryption()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            txtPassword.Text = txtSecurityKey.Text = txtResultString.Text = string.Empty;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtSecurityKey.Text))
            {
                lblError.Text = "Password or Security key can not be empty";
                lblError.Visible = true;
                return;
            }
            var miscBusiness = BusinessFactory<IMiscBusiness>.CreateObject();
            var result = miscBusiness.EncryptPassword(txtPassword.Text.Trim(), txtSecurityKey.Text.Trim());
            if(result == "Wrong Input")
            {
                lblError.Text = "Error while generating encryption string";
                lblError.Visible = true;
            }
            else
            {
                lblError.Visible = false;
                txtResultString.Text = result;
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtSecurityKey.Text))
            {
                lblError.Text = "Encrypted string or Security key can not be empty";
                lblError.Visible = true;
                return;
            }
            var miscBusiness = BusinessFactory<IMiscBusiness>.CreateObject();
            var result = miscBusiness.DecryptPassword(txtPassword.Text.Trim(), txtSecurityKey.Text.Trim());
            if (result == "Wrong Input")
            {
                lblError.Text = "Error while generating descryption string";
                lblError.Visible = true;
            }
            else
            {
                lblError.Visible = false;
                txtResultString.Text = result;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, txtResultString.Text.Trim());
        }
    }
}
