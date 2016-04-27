namespace Sistemas
{
    partial class fDHCP
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
            this.cboCOD3 = new EspackFormControls.EspackComboBox();
            this.txtServerName = new EspackFormControls.EspackTextBox();
            this.txtUser = new EspackFormControls.EspackTextBox();
            this.txtPassword = new EspackFormControls.EspackTextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnProcess = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboCOD3
            // 
            this.cboCOD3.Add = false;
            this.cboCOD3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCOD3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCOD3.Caption = "COD3";
            this.cboCOD3.DBField = null;
            this.cboCOD3.DBFieldType = null;
            this.cboCOD3.DefaultValue = null;
            this.cboCOD3.Del = false;
            this.cboCOD3.DependingRS = null;
            this.cboCOD3.FormattingEnabled = true;
            this.cboCOD3.Location = new System.Drawing.Point(44, 53);
            this.cboCOD3.Name = "cboCOD3";
            this.cboCOD3.Order = 0;
            this.cboCOD3.ParentConn = null;
            this.cboCOD3.ParentDA = null;
            this.cboCOD3.PK = false;
            this.cboCOD3.Search = true;
            this.cboCOD3.Size = new System.Drawing.Size(188, 21);
            this.cboCOD3.Status = CommonTools.EnumStatus.SEARCH;
            this.cboCOD3.TabIndex = 0;
            this.cboCOD3.TBDescription = null;
            this.cboCOD3.Upp = true;
            this.cboCOD3.Value = "";
            this.cboCOD3.SelectedIndexChanged += new System.EventHandler(this.cboCOD3_SelectedIndexChanged);
            // 
            // txtServerName
            // 
            this.txtServerName.Add = false;
            this.txtServerName.Caption = "Server Name";
            this.txtServerName.DBField = null;
            this.txtServerName.DBFieldType = null;
            this.txtServerName.DefaultValue = null;
            this.txtServerName.Del = false;
            this.txtServerName.DependingRS = null;
            this.txtServerName.Location = new System.Drawing.Point(44, 89);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Order = 0;
            this.txtServerName.ParentConn = null;
            this.txtServerName.ParentDA = null;
            this.txtServerName.PK = false;
            this.txtServerName.Search = true;
            this.txtServerName.Size = new System.Drawing.Size(188, 20);
            this.txtServerName.Status = CommonTools.EnumStatus.SEARCH;
            this.txtServerName.TabIndex = 2;
            this.txtServerName.Upp = true;
            this.txtServerName.Value = "";
            // 
            // txtUser
            // 
            this.txtUser.Add = false;
            this.txtUser.Caption = "User";
            this.txtUser.DBField = null;
            this.txtUser.DBFieldType = null;
            this.txtUser.DefaultValue = null;
            this.txtUser.Del = false;
            this.txtUser.DependingRS = null;
            this.txtUser.Location = new System.Drawing.Point(44, 124);
            this.txtUser.Name = "txtUser";
            this.txtUser.Order = 0;
            this.txtUser.ParentConn = null;
            this.txtUser.ParentDA = null;
            this.txtUser.PK = false;
            this.txtUser.Search = true;
            this.txtUser.Size = new System.Drawing.Size(188, 20);
            this.txtUser.Status = CommonTools.EnumStatus.SEARCH;
            this.txtUser.TabIndex = 4;
            this.txtUser.Upp = true;
            this.txtUser.Value = "";
            // 
            // txtPassword
            // 
            this.txtPassword.Add = false;
            this.txtPassword.Caption = "Password";
            this.txtPassword.DBField = null;
            this.txtPassword.DBFieldType = null;
            this.txtPassword.DefaultValue = null;
            this.txtPassword.Del = false;
            this.txtPassword.DependingRS = null;
            this.txtPassword.Location = new System.Drawing.Point(44, 159);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Order = 0;
            this.txtPassword.ParentConn = null;
            this.txtPassword.ParentDA = null;
            this.txtPassword.PasswordChar = '·';
            this.txtPassword.PK = false;
            this.txtPassword.Search = true;
            this.txtPassword.Size = new System.Drawing.Size(188, 20);
            this.txtPassword.Status = CommonTools.EnumStatus.SEARCH;
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Upp = true;
            this.txtPassword.Value = "";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMsg});
            this.statusStrip.Location = new System.Drawing.Point(0, 239);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(284, 22);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblMsg
            // 
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(42, 17);
            this.lblMsg.Text = "Status:";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(98, 194);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(133, 23);
            this.btnProcess.TabIndex = 9;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // fDHCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.cboCOD3);
            this.Name = "fDHCP";
            this.Text = "DHCP Control";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EspackFormControls.EspackComboBox cboCOD3;
        private EspackFormControls.EspackTextBox txtServerName;
        private EspackFormControls.EspackTextBox txtUser;
        private EspackFormControls.EspackTextBox txtPassword;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblMsg;
        private System.Windows.Forms.Button btnProcess;
    }
}