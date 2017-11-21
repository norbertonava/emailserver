namespace EmailServer.UI
{
    partial class SafeListForm
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
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.safeListGrid = new System.Windows.Forms.DataGridView();
            this.lblMail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.lblToken = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.safeListGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(119, 23);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(179, 20);
            this.txtPhoneNumber.TabIndex = 5;
            this.txtPhoneNumber.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(13, 26);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(79, 13);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Phone number:";
            // 
            // safeListGrid
            // 
            this.safeListGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.safeListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.safeListGrid.Location = new System.Drawing.Point(12, 112);
            this.safeListGrid.Name = "safeListGrid";
            this.safeListGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.safeListGrid.Size = new System.Drawing.Size(634, 182);
            this.safeListGrid.TabIndex = 3;
            this.safeListGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.safeListGrid_DataBindingComplete);
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new System.Drawing.Point(13, 52);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(38, 13);
            this.lblMail.TabIndex = 6;
            this.lblMail.Text = "E-mail:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(119, 49);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(179, 20);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(119, 75);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(179, 20);
            this.txtToken.TabIndex = 9;
            this.txtToken.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // lblToken
            // 
            this.lblToken.AutoSize = true;
            this.lblToken.Location = new System.Drawing.Point(13, 78);
            this.lblToken.Name = "lblToken";
            this.lblToken.Size = new System.Drawing.Size(41, 13);
            this.lblToken.TabIndex = 8;
            this.lblToken.Text = "Token:";
            // 
            // SafeListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 306);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.lblToken);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblMail);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.safeListGrid);
            this.Name = "SafeListForm";
            this.Text = "Safe List";
            this.Activated += new System.EventHandler(this.SafeListForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SafeListForm_FormClosing);
            this.Load += new System.EventHandler(this.SafeListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.safeListGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.DataGridView safeListGrid;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label lblToken;
    }
}