namespace Sistemas
{
    partial class fDNS
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
            this.btnProcess = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtPassword = new EspackFormControls.EspackTextBox();
            this.txtUser = new EspackFormControls.EspackTextBox();
            this.txtServerName = new EspackFormControls.EspackTextBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(98, 149);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(133, 23);
            this.btnProcess.TabIndex = 17;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMsg});
            this.statusStrip.Location = new System.Drawing.Point(0, 239);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(284, 22);
            this.statusStrip.TabIndex = 16;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblMsg
            // 
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(42, 17);
            this.lblMsg.Text = "Status:";
            // 
            // txtPassword
            // 
            this.txtPassword.Add = false;
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Caption = "Password";
            this.txtPassword.DBField = null;
            this.txtPassword.DBFieldType = null;
            this.txtPassword.DefaultValue = null;
            this.txtPassword.Del = false;
            this.txtPassword.DependingRS = null;
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(44, 114);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Order = 0;
            this.txtPassword.ParentConn = null;
            this.txtPassword.ParentDA = null;
            this.txtPassword.PasswordChar = '·';
            this.txtPassword.PK = false;
            this.txtPassword.Search = true;
            this.txtPassword.Size = new System.Drawing.Size(188, 20);
            this.txtPassword.Status = CommonTools.EnumStatus.SEARCH;
            this.txtPassword.TabIndex = 15;
            this.txtPassword.Upp = true;
            this.txtPassword.Value = "";
            // 
            // txtUser
            // 
            this.txtUser.Add = false;
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.Caption = "User";
            this.txtUser.DBField = null;
            this.txtUser.DBFieldType = null;
            this.txtUser.DefaultValue = null;
            this.txtUser.Del = false;
            this.txtUser.DependingRS = null;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtUser.ForeColor = System.Drawing.Color.Black;
            this.txtUser.Location = new System.Drawing.Point(44, 79);
            this.txtUser.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtUser.Multiline = true;
            this.txtUser.Name = "txtUser";
            this.txtUser.Order = 0;
            this.txtUser.ParentConn = null;
            this.txtUser.ParentDA = null;
            this.txtUser.PK = false;
            this.txtUser.Search = true;
            this.txtUser.Size = new System.Drawing.Size(188, 20);
            this.txtUser.Status = CommonTools.EnumStatus.SEARCH;
            this.txtUser.TabIndex = 14;
            this.txtUser.Upp = true;
            this.txtUser.Value = "";
            // 
            // txtServerName
            // 
            this.txtServerName.Add = false;
            this.txtServerName.BackColor = System.Drawing.Color.White;
            this.txtServerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtServerName.Caption = "Server Name";
            this.txtServerName.DBField = null;
            this.txtServerName.DBFieldType = null;
            this.txtServerName.DefaultValue = null;
            this.txtServerName.Del = false;
            this.txtServerName.DependingRS = null;
            this.txtServerName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtServerName.ForeColor = System.Drawing.Color.Black;
            this.txtServerName.Location = new System.Drawing.Point(44, 44);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtServerName.Multiline = true;
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Order = 0;
            this.txtServerName.ParentConn = null;
            this.txtServerName.ParentDA = null;
            this.txtServerName.PK = false;
            this.txtServerName.Search = true;
            this.txtServerName.Size = new System.Drawing.Size(188, 20);
            this.txtServerName.Status = CommonTools.EnumStatus.SEARCH;
            this.txtServerName.TabIndex = 13;
            this.txtServerName.Upp = true;
            this.txtServerName.Value = "";
            // 
            // fDNS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtServerName);
            this.Name = "fDNS";
            this.ShowIcon = false;
            this.Text = "DNS Control";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblMsg;
        private EspackFormControls.EspackTextBox txtPassword;
        private EspackFormControls.EspackTextBox txtUser;
        private EspackFormControls.EspackTextBox txtServerName;
    }
}