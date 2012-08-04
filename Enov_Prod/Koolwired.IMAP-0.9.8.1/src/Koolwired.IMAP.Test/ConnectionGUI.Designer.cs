namespace Koolwired.Imap.Test
{
    partial class ConnectionGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcServers = new System.Windows.Forms.TabControl();
            this.tabStandard = new System.Windows.Forms.TabPage();
            this.tbxStdUser = new System.Windows.Forms.TextBox();
            this.tbxStdHost = new System.Windows.Forms.TextBox();
            this.lblStdPass = new System.Windows.Forms.Label();
            this.lblStdUser = new System.Windows.Forms.Label();
            this.lblStdHost = new System.Windows.Forms.Label();
            this.tabSSL = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.tbxStdPass = new System.Windows.Forms.TextBox();
            this.tbxSslPass = new System.Windows.Forms.TextBox();
            this.tbxSslUser = new System.Windows.Forms.TextBox();
            this.tbxSslHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tcServers.SuspendLayout();
            this.tabStandard.SuspendLayout();
            this.tabSSL.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcServers
            // 
            this.tcServers.Controls.Add(this.tabStandard);
            this.tcServers.Controls.Add(this.tabSSL);
            this.tcServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcServers.Location = new System.Drawing.Point(0, 0);
            this.tcServers.Name = "tcServers";
            this.tcServers.SelectedIndex = 0;
            this.tcServers.Size = new System.Drawing.Size(315, 184);
            this.tcServers.TabIndex = 0;
            // 
            // tabStandard
            // 
            this.tabStandard.Controls.Add(this.tbxStdPass);
            this.tabStandard.Controls.Add(this.tbxStdUser);
            this.tabStandard.Controls.Add(this.tbxStdHost);
            this.tabStandard.Controls.Add(this.lblStdPass);
            this.tabStandard.Controls.Add(this.lblStdUser);
            this.tabStandard.Controls.Add(this.lblStdHost);
            this.tabStandard.Location = new System.Drawing.Point(4, 22);
            this.tabStandard.Name = "tabStandard";
            this.tabStandard.Padding = new System.Windows.Forms.Padding(3);
            this.tabStandard.Size = new System.Drawing.Size(307, 158);
            this.tabStandard.TabIndex = 0;
            this.tabStandard.Text = "Standard";
            this.tabStandard.UseVisualStyleBackColor = true;
            // 
            // tbxStdUser
            // 
            this.tbxStdUser.Location = new System.Drawing.Point(86, 53);
            this.tbxStdUser.Name = "tbxStdUser";
            this.tbxStdUser.Size = new System.Drawing.Size(100, 20);
            this.tbxStdUser.TabIndex = 4;
            // 
            // tbxStdHost
            // 
            this.tbxStdHost.Location = new System.Drawing.Point(86, 24);
            this.tbxStdHost.Name = "tbxStdHost";
            this.tbxStdHost.Size = new System.Drawing.Size(213, 20);
            this.tbxStdHost.TabIndex = 3;
            // 
            // lblStdPass
            // 
            this.lblStdPass.AutoSize = true;
            this.lblStdPass.Location = new System.Drawing.Point(27, 89);
            this.lblStdPass.Name = "lblStdPass";
            this.lblStdPass.Size = new System.Drawing.Size(53, 13);
            this.lblStdPass.TabIndex = 2;
            this.lblStdPass.Text = "Password";
            // 
            // lblStdUser
            // 
            this.lblStdUser.AutoSize = true;
            this.lblStdUser.Location = new System.Drawing.Point(25, 56);
            this.lblStdUser.Name = "lblStdUser";
            this.lblStdUser.Size = new System.Drawing.Size(55, 13);
            this.lblStdUser.TabIndex = 1;
            this.lblStdUser.Text = "Username";
            // 
            // lblStdHost
            // 
            this.lblStdHost.AutoSize = true;
            this.lblStdHost.Location = new System.Drawing.Point(51, 27);
            this.lblStdHost.Name = "lblStdHost";
            this.lblStdHost.Size = new System.Drawing.Size(29, 13);
            this.lblStdHost.TabIndex = 0;
            this.lblStdHost.Text = "Host";
            // 
            // tabSSL
            // 
            this.tabSSL.Controls.Add(this.tbxSslPass);
            this.tabSSL.Controls.Add(this.tbxSslUser);
            this.tabSSL.Controls.Add(this.tbxSslHost);
            this.tabSSL.Controls.Add(this.label1);
            this.tabSSL.Controls.Add(this.label2);
            this.tabSSL.Controls.Add(this.label3);
            this.tabSSL.Location = new System.Drawing.Point(4, 22);
            this.tabSSL.Name = "tabSSL";
            this.tabSSL.Padding = new System.Windows.Forms.Padding(3);
            this.tabSSL.Size = new System.Drawing.Size(307, 158);
            this.tabSSL.TabIndex = 1;
            this.tabSSL.Text = "SSL";
            this.tabSSL.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnContinue);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 40);
            this.panel1.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(155, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(236, 10);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 0;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // tbxStdPass
            // 
            this.tbxStdPass.Location = new System.Drawing.Point(86, 86);
            this.tbxStdPass.Name = "tbxStdPass";
            this.tbxStdPass.Size = new System.Drawing.Size(100, 20);
            this.tbxStdPass.TabIndex = 5;
            // 
            // tbxSslPass
            // 
            this.tbxSslPass.Location = new System.Drawing.Point(86, 86);
            this.tbxSslPass.Name = "tbxSslPass";
            this.tbxSslPass.Size = new System.Drawing.Size(100, 20);
            this.tbxSslPass.TabIndex = 11;
            this.tbxSslPass.Text = "password";
            this.tbxSslPass.UseSystemPasswordChar = true;
            // 
            // tbxSslUser
            // 
            this.tbxSslUser.Location = new System.Drawing.Point(86, 53);
            this.tbxSslUser.Name = "tbxSslUser";
            this.tbxSslUser.Size = new System.Drawing.Size(100, 20);
            this.tbxSslUser.TabIndex = 10;
            this.tbxSslUser.Text = "email@gmail.com";
            // 
            // tbxSslHost
            // 
            this.tbxSslHost.Location = new System.Drawing.Point(86, 24);
            this.tbxSslHost.Name = "tbxSslHost";
            this.tbxSslHost.Size = new System.Drawing.Size(213, 20);
            this.tbxSslHost.TabIndex = 9;
            this.tbxSslHost.Text = "imap.gmail.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Host";
            // 
            // ConnectionGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 184);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tcServers);
            this.Name = "ConnectionGUI";
            this.Text = "ConnectionGUI";
            this.tcServers.ResumeLayout(false);
            this.tabStandard.ResumeLayout(false);
            this.tabStandard.PerformLayout();
            this.tabSSL.ResumeLayout(false);
            this.tabSSL.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcServers;
        private System.Windows.Forms.TabPage tabStandard;
        private System.Windows.Forms.TabPage tabSSL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.TextBox tbxStdUser;
        private System.Windows.Forms.TextBox tbxStdHost;
        private System.Windows.Forms.Label lblStdPass;
        private System.Windows.Forms.Label lblStdUser;
        private System.Windows.Forms.Label lblStdHost;
        private System.Windows.Forms.TextBox tbxStdPass;
        private System.Windows.Forms.TextBox tbxSslPass;
        private System.Windows.Forms.TextBox tbxSslUser;
        private System.Windows.Forms.TextBox tbxSslHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}