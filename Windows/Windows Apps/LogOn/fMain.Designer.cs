namespace LogOn
{
    partial class fMain
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
            this.gbCredentials = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbApps = new System.Windows.Forms.GroupBox();
            this.tlpApps = new System.Windows.Forms.TableLayoutPanel();
            this.txtPassword = new EspackFormControls.EspackTextBox();
            this.txtUser = new EspackFormControls.EspackTextBox();
            this.gbDebug = new System.Windows.Forms.GroupBox();
            this.gbCredentials.SuspendLayout();
            this.gbApps.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCredentials
            // 
            this.gbCredentials.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCredentials.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gbCredentials.Controls.Add(this.btnOk);
            this.gbCredentials.Controls.Add(this.txtPassword);
            this.gbCredentials.Controls.Add(this.txtUser);
            this.gbCredentials.Location = new System.Drawing.Point(12, 12);
            this.gbCredentials.Name = "gbCredentials";
            this.gbCredentials.Size = new System.Drawing.Size(1686, 55);
            this.gbCredentials.TabIndex = 0;
            this.gbCredentials.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(293, 19);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gbApps
            // 
            this.gbApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbApps.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gbApps.Controls.Add(this.tlpApps);
            this.gbApps.Location = new System.Drawing.Point(12, 74);
            this.gbApps.Name = "gbApps";
            this.gbApps.Size = new System.Drawing.Size(1686, 539);
            this.gbApps.TabIndex = 1;
            this.gbApps.TabStop = false;
            // 
            // tlpApps
            // 
            this.tlpApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpApps.AutoScroll = true;
            this.tlpApps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpApps.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tlpApps.ColumnCount = 1;
            this.tlpApps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1229F));
            this.tlpApps.Location = new System.Drawing.Point(0, 0);
            this.tlpApps.Name = "tlpApps";
            this.tlpApps.RowCount = 1;
            this.tlpApps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 528F));
            this.tlpApps.Size = new System.Drawing.Size(1687, 533);
            this.tlpApps.TabIndex = 2;
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
            this.txtPassword.Location = new System.Drawing.Point(145, 22);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Order = 0;
            this.txtPassword.ParentConn = null;
            this.txtPassword.ParentDA = null;
            this.txtPassword.PK = false;
            this.txtPassword.Search = false;
            this.txtPassword.Size = new System.Drawing.Size(100, 17);
            this.txtPassword.Status = CommonTools.EnumStatus.EDIT;
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Upp = true;
            this.txtPassword.UseSystemPasswordChar = true;
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
            this.txtUser.Location = new System.Drawing.Point(11, 22);
            this.txtUser.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtUser.Multiline = true;
            this.txtUser.Name = "txtUser";
            this.txtUser.Order = 0;
            this.txtUser.ParentConn = null;
            this.txtUser.ParentDA = null;
            this.txtUser.PK = false;
            this.txtUser.Search = false;
            this.txtUser.Size = new System.Drawing.Size(108, 17);
            this.txtUser.Status = CommonTools.EnumStatus.EDIT;
            this.txtUser.TabIndex = 0;
            this.txtUser.Upp = true;
            this.txtUser.Value = "";
            this.txtUser.WordWrap = false;
            // 
            // gbDebug
            // 
            this.gbDebug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDebug.Location = new System.Drawing.Point(12, 619);
            this.gbDebug.Name = "gbDebug";
            this.gbDebug.Size = new System.Drawing.Size(1683, 132);
            this.gbDebug.TabIndex = 3;
            this.gbDebug.TabStop = false;
            this.gbDebug.Text = "groupBox1";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1710, 783);
            this.Controls.Add(this.gbDebug);
            this.Controls.Add(this.gbApps);
            this.Controls.Add(this.gbCredentials);
            this.Name = "fMain";
            this.Text = "LogOn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.gbCredentials.ResumeLayout(false);
            this.gbCredentials.PerformLayout();
            this.gbApps.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCredentials;
        private EspackFormControls.EspackTextBox txtPassword;
        private EspackFormControls.EspackTextBox txtUser;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox gbApps;
        private LogOn.cAppBot cAppBot1;
        private System.Windows.Forms.TableLayoutPanel tlpApps;
        private System.Windows.Forms.TextBox txtDebug1;
        private System.Windows.Forms.GroupBox gbDebug;
    }
}

