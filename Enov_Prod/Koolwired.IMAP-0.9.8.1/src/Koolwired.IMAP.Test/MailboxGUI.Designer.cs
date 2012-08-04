namespace Koolwired.Imap.Test
{
    partial class MailboxGUI
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
            this.tbxHost = new System.Windows.Forms.TextBox();
            this.tbxUser = new System.Windows.Forms.TextBox();
            this.tbxPass = new System.Windows.Forms.TextBox();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.chbSsl = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxHost
            // 
            this.tbxHost.Location = new System.Drawing.Point(63, 23);
            this.tbxHost.Name = "tbxHost";
            this.tbxHost.Size = new System.Drawing.Size(198, 20);
            this.tbxHost.TabIndex = 0;
            this.tbxHost.Text = "imap.gmail.com";
            // 
            // tbxUser
            // 
            this.tbxUser.Location = new System.Drawing.Point(63, 49);
            this.tbxUser.Name = "tbxUser";
            this.tbxUser.Size = new System.Drawing.Size(100, 20);
            this.tbxUser.TabIndex = 1;
            this.tbxUser.Text = "email@gmail.com";
            // 
            // tbxPass
            // 
            this.tbxPass.Location = new System.Drawing.Point(63, 75);
            this.tbxPass.Name = "tbxPass";
            this.tbxPass.Size = new System.Drawing.Size(100, 20);
            this.tbxPass.TabIndex = 2;
            this.tbxPass.UseSystemPasswordChar = true;
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(63, 124);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(40, 20);
            this.tbxPort.TabIndex = 3;
            this.tbxPort.Text = "993";
            // 
            // chbSsl
            // 
            this.chbSsl.AutoSize = true;
            this.chbSsl.Checked = true;
            this.chbSsl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSsl.Location = new System.Drawing.Point(31, 101);
            this.chbSsl.Name = "chbSsl";
            this.chbSsl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbSsl.Size = new System.Drawing.Size(46, 17);
            this.chbSsl.TabIndex = 4;
            this.chbSsl.Text = "SSL";
            this.chbSsl.UseVisualStyleBackColor = true;
            this.chbSsl.CheckedChanged += new System.EventHandler(this.chbSsl_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Host";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(186, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Continue";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MailboxGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 187);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chbSsl);
            this.Controls.Add(this.tbxPort);
            this.Controls.Add(this.tbxPass);
            this.Controls.Add(this.tbxUser);
            this.Controls.Add(this.tbxHost);
            this.Name = "MailboxGUI";
            this.Text = "MailboxGUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxHost;
        private System.Windows.Forms.TextBox tbxUser;
        private System.Windows.Forms.TextBox tbxPass;
        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.CheckBox chbSsl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}