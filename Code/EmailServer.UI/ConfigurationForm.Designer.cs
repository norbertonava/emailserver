namespace EmailServer.UI
{
    partial class ConfigurationForm
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
            this.lblFetch = new System.Windows.Forms.Label();
            this.numSeconds = new System.Windows.Forms.NumericUpDown();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.lblMail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPOP3Address = new System.Windows.Forms.TextBox();
            this.lblPOP3Address = new System.Windows.Forms.Label();
            this.numPOP3Port = new System.Windows.Forms.NumericUpDown();
            this.lblPOP3Port = new System.Windows.Forms.Label();
            this.numSMTPPort = new System.Windows.Forms.NumericUpDown();
            this.lblPortSMTP = new System.Windows.Forms.Label();
            this.txtSMTPAddress = new System.Windows.Forms.TextBox();
            this.lblSMTPAddress = new System.Windows.Forms.Label();
            this.chkPOP3UseSSL = new System.Windows.Forms.CheckBox();
            this.chkSMTPUseSSL = new System.Windows.Forms.CheckBox();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBadResponseMailSubject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBadResponseMailBody = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPOP3Port)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSMTPPort)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFetch
            // 
            this.lblFetch.AutoSize = true;
            this.lblFetch.Location = new System.Drawing.Point(24, 19);
            this.lblFetch.Name = "lblFetch";
            this.lblFetch.Size = new System.Drawing.Size(66, 13);
            this.lblFetch.TabIndex = 0;
            this.lblFetch.Text = "Fetch every:";
            // 
            // numSeconds
            // 
            this.numSeconds.Location = new System.Drawing.Point(118, 19);
            this.numSeconds.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numSeconds.Name = "numSeconds";
            this.numSeconds.Size = new System.Drawing.Size(120, 20);
            this.numSeconds.TabIndex = 1;
            this.numSeconds.ValueChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Location = new System.Drawing.Point(244, 19);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(47, 13);
            this.lblSeconds.TabIndex = 2;
            this.lblSeconds.Text = "seconds";
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new System.Drawing.Point(24, 63);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(78, 13);
            this.lblMail.TabIndex = 3;
            this.lblMail.Text = "E-mail address:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(118, 56);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(191, 20);
            this.txtEmail.TabIndex = 2;
            this.txtEmail.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(118, 101);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(191, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(25, 104);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(86, 13);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "E-mail password:";
            // 
            // txtPOP3Address
            // 
            this.txtPOP3Address.Location = new System.Drawing.Point(118, 146);
            this.txtPOP3Address.Name = "txtPOP3Address";
            this.txtPOP3Address.Size = new System.Drawing.Size(191, 20);
            this.txtPOP3Address.TabIndex = 5;
            this.txtPOP3Address.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // lblPOP3Address
            // 
            this.lblPOP3Address.AutoSize = true;
            this.lblPOP3Address.Location = new System.Drawing.Point(25, 149);
            this.lblPOP3Address.Name = "lblPOP3Address";
            this.lblPOP3Address.Size = new System.Drawing.Size(78, 13);
            this.lblPOP3Address.TabIndex = 9;
            this.lblPOP3Address.Text = "POP3 address:";
            // 
            // numPOP3Port
            // 
            this.numPOP3Port.Location = new System.Drawing.Point(434, 146);
            this.numPOP3Port.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numPOP3Port.Name = "numPOP3Port";
            this.numPOP3Port.Size = new System.Drawing.Size(120, 20);
            this.numPOP3Port.TabIndex = 6;
            this.numPOP3Port.ValueChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // lblPOP3Port
            // 
            this.lblPOP3Port.AutoSize = true;
            this.lblPOP3Port.Location = new System.Drawing.Point(355, 149);
            this.lblPOP3Port.Name = "lblPOP3Port";
            this.lblPOP3Port.Size = new System.Drawing.Size(29, 13);
            this.lblPOP3Port.TabIndex = 11;
            this.lblPOP3Port.Text = "Port:";
            // 
            // numSMTPPort
            // 
            this.numSMTPPort.Location = new System.Drawing.Point(434, 183);
            this.numSMTPPort.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numSMTPPort.Name = "numSMTPPort";
            this.numSMTPPort.Size = new System.Drawing.Size(120, 20);
            this.numSMTPPort.TabIndex = 9;
            this.numSMTPPort.ValueChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // lblPortSMTP
            // 
            this.lblPortSMTP.AutoSize = true;
            this.lblPortSMTP.Location = new System.Drawing.Point(355, 186);
            this.lblPortSMTP.Name = "lblPortSMTP";
            this.lblPortSMTP.Size = new System.Drawing.Size(29, 13);
            this.lblPortSMTP.TabIndex = 15;
            this.lblPortSMTP.Text = "Port:";
            // 
            // txtSMTPAddress
            // 
            this.txtSMTPAddress.Location = new System.Drawing.Point(118, 183);
            this.txtSMTPAddress.Name = "txtSMTPAddress";
            this.txtSMTPAddress.Size = new System.Drawing.Size(191, 20);
            this.txtSMTPAddress.TabIndex = 8;
            this.txtSMTPAddress.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // lblSMTPAddress
            // 
            this.lblSMTPAddress.AutoSize = true;
            this.lblSMTPAddress.Location = new System.Drawing.Point(25, 186);
            this.lblSMTPAddress.Name = "lblSMTPAddress";
            this.lblSMTPAddress.Size = new System.Drawing.Size(80, 13);
            this.lblSMTPAddress.TabIndex = 13;
            this.lblSMTPAddress.Text = "SMTP address:";
            // 
            // chkPOP3UseSSL
            // 
            this.chkPOP3UseSSL.AutoSize = true;
            this.chkPOP3UseSSL.Location = new System.Drawing.Point(593, 147);
            this.chkPOP3UseSSL.Name = "chkPOP3UseSSL";
            this.chkPOP3UseSSL.Size = new System.Drawing.Size(68, 17);
            this.chkPOP3UseSSL.TabIndex = 7;
            this.chkPOP3UseSSL.Text = "Use SSL";
            this.chkPOP3UseSSL.UseVisualStyleBackColor = true;
            this.chkPOP3UseSSL.CheckedChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // chkSMTPUseSSL
            // 
            this.chkSMTPUseSSL.AutoSize = true;
            this.chkSMTPUseSSL.Location = new System.Drawing.Point(593, 184);
            this.chkSMTPUseSSL.Name = "chkSMTPUseSSL";
            this.chkSMTPUseSSL.Size = new System.Drawing.Size(68, 17);
            this.chkSMTPUseSSL.TabIndex = 10;
            this.chkSMTPUseSSL.Text = "Use SSL";
            this.chkSMTPUseSSL.UseVisualStyleBackColor = true;
            this.chkSMTPUseSSL.CheckedChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(434, 60);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(191, 20);
            this.txtDisplayName.TabIndex = 3;
            this.txtDisplayName.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Display name:";
            // 
            // txtBadResponseMailSubject
            // 
            this.txtBadResponseMailSubject.Location = new System.Drawing.Point(247, 237);
            this.txtBadResponseMailSubject.Name = "txtBadResponseMailSubject";
            this.txtBadResponseMailSubject.Size = new System.Drawing.Size(307, 20);
            this.txtBadResponseMailSubject.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Unrecognized phone number mail subject:";
            // 
            // txtBadResponseMailBody
            // 
            this.txtBadResponseMailBody.Location = new System.Drawing.Point(247, 277);
            this.txtBadResponseMailBody.Multiline = true;
            this.txtBadResponseMailBody.Name = "txtBadResponseMailBody";
            this.txtBadResponseMailBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBadResponseMailBody.Size = new System.Drawing.Size(307, 100);
            this.txtBadResponseMailBody.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Unrecognized phone number mail body:";
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 396);
            this.Controls.Add(this.txtBadResponseMailBody);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBadResponseMailSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDisplayName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSMTPUseSSL);
            this.Controls.Add(this.chkPOP3UseSSL);
            this.Controls.Add(this.numSMTPPort);
            this.Controls.Add(this.lblPortSMTP);
            this.Controls.Add(this.txtSMTPAddress);
            this.Controls.Add(this.lblSMTPAddress);
            this.Controls.Add(this.numPOP3Port);
            this.Controls.Add(this.lblPOP3Port);
            this.Controls.Add(this.txtPOP3Address);
            this.Controls.Add(this.lblPOP3Address);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblMail);
            this.Controls.Add(this.lblSeconds);
            this.Controls.Add(this.numSeconds);
            this.Controls.Add(this.lblFetch);
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this.Activated += new System.EventHandler(this.ConfigurationForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPOP3Port)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSMTPPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFetch;
        private System.Windows.Forms.NumericUpDown numSeconds;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPOP3Address;
        private System.Windows.Forms.Label lblPOP3Address;
        private System.Windows.Forms.NumericUpDown numPOP3Port;
        private System.Windows.Forms.Label lblPOP3Port;
        private System.Windows.Forms.NumericUpDown numSMTPPort;
        private System.Windows.Forms.Label lblPortSMTP;
        private System.Windows.Forms.TextBox txtSMTPAddress;
        private System.Windows.Forms.Label lblSMTPAddress;
        private System.Windows.Forms.CheckBox chkPOP3UseSSL;
        private System.Windows.Forms.CheckBox chkSMTPUseSSL;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBadResponseMailSubject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBadResponseMailBody;
        private System.Windows.Forms.Label label3;
    }
}