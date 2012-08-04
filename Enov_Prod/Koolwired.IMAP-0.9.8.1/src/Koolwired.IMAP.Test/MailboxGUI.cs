using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Koolwired.Imap.Test
{
    public partial class MailboxGUI : Form
    {
        public string Host
        {
            get { return tbxHost.Text; }
        }
        public string User
        {
            get { return tbxUser.Text; }
        }
        public string Pass
        {
            get { return tbxPass.Text; }
        }
        public int Port
        {
            get
            {
                int x = 0;
                if (!int.TryParse(tbxPort.Text, out x))
                    if (chbSsl.Checked)
                        return 993;
                    else
                        return 143;
                return x;
            }
        }
        public bool SSL
        {
            get { return chbSsl.Checked; }
        }
                
        public MailboxGUI()
        {
            InitializeComponent();
            ShowDialog();
        }

        private void chbSsl_CheckedChanged(object sender, EventArgs e)
        {
            if (chbSsl.CheckState == CheckState.Checked)
                tbxPort.Text = "993";
            else
                tbxPort.Text = "143";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}