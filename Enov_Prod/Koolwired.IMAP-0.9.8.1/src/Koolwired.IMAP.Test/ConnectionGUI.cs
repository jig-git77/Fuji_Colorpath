using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Koolwired.Imap.Test
{
    public partial class ConnectionGUI : Form
    {
        #region Private Variables
        bool _status = false;
        #endregion

        #region Public Properties
        public string StandardHost
        {
            get { return tbxStdHost.Text; }
        }
        public string StandardUser
        {
            get { return tbxStdUser.Text; }
        }
        public string StandardPass
        {
            get { return tbxStdPass.Text; }
        }
        public string SslHost
        {
            get { return tbxSslHost.Text; }
        }
        public string SslUser
        {
            get { return tbxSslUser.Text; }
        }
        public string SslPass
        {
            get { return tbxSslPass.Text; }
        }
        public bool Status
        {
            get { return _status; }
        }
        #endregion

        public ConnectionGUI()
        {
            InitializeComponent();
            DialogResult b = this.ShowDialog();
            if (b == DialogResult.OK)
                _status = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxStdHost.Text = string.Empty;
            tbxStdUser.Text = string.Empty;
            tbxStdPass.Text = string.Empty;
            tbxSslHost.Text = string.Empty;
            tbxSslUser.Text = string.Empty;
            tbxSslPass.Text = string.Empty;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}