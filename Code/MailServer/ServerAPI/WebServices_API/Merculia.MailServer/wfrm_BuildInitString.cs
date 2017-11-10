using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Merculia.MailServer
{
    public class wfrm_BuildInitString : wfrm_BuildInitString_base
    {
        private Label label1;

        private Label label2;

        private Label label3;

        private TextBox m_pWebServicesUrl;

        private TextBox m_pUserName;

        private TextBox m_pPassword;

        private Container components = null;

        public override string InitString
        {
            get
            {
                return string.Concat(new string[]
                {
                    "url=",
                    this.m_pWebServicesUrl.Text,
                    "\r\nusername=",
                    this.m_pUserName.Text,
                    "\r\npassword=",
                    this.m_pPassword.Text
                });
            }
            set
            {
                string[] array = value.Replace("\r\n", "\n").Split(new char[]
                {
                    '\n'
                });
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string text = array2[i];
                    if (text.ToLower().IndexOf("url=") > -1)
                    {
                        this.m_pWebServicesUrl.Text = text.Substring(4);
                    }
                    else if (text.ToLower().IndexOf("username=") > -1)
                    {
                        this.m_pUserName.Text = text.Substring(9);
                    }
                    else if (text.ToLower().IndexOf("password=") > -1)
                    {
                        this.m_pPassword.Text = text.Substring(9);
                    }
                }
            }
        }

        public wfrm_BuildInitString()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.m_pWebServicesUrl = new TextBox();
            this.label1 = new Label();
            this.m_pUserName = new TextBox();
            this.label2 = new Label();
            this.m_pPassword = new TextBox();
            this.label3 = new Label();
            base.SuspendLayout();
            this.m_pWebServicesUrl.Anchor = AnchorStyles.Left;
            this.m_pWebServicesUrl.Location = new Point(8, 24);
            this.m_pWebServicesUrl.Name = "m_pWebServicesUrl";
            this.m_pWebServicesUrl.Size = new Size(352, 20);
            this.m_pWebServicesUrl.TabIndex = 0;
            this.m_pWebServicesUrl.Text = "";
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(104, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "WebServices URL:";
            this.m_pUserName.Location = new Point(80, 56);
            this.m_pUserName.Name = "m_pUserName";
            this.m_pUserName.TabIndex = 2;
            this.m_pUserName.Text = "";
            this.label2.Location = new Point(8, 56);
            this.label2.Name = "label2";
            this.label2.Size = new Size(72, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "User Name:";
            this.m_pPassword.Location = new Point(256, 56);
            this.m_pPassword.Name = "m_pPassword";
            this.m_pPassword.PasswordChar = '*';
            this.m_pPassword.Size = new Size(104, 20);
            this.m_pPassword.TabIndex = 4;
            this.m_pPassword.Text = "";
            this.label3.Location = new Point(192, 56);
            this.label3.Name = "label3";
            this.label3.Size = new Size(64, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password:";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(376, 85);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.m_pPassword);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.m_pUserName);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.m_pWebServicesUrl);
            base.Name = "wfrm_BuildInitString";
            this.Text = "wfrm_BuildInitString";
            base.ResumeLayout(false);
        }
    }
}