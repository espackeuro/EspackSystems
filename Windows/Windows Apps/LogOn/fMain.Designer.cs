using System;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.gbCredentials = new System.Windows.Forms.GroupBox();
            this.gbChangePassword = new System.Windows.Forms.GroupBox();
            this.btnCancelChange = new System.Windows.Forms.Button();
            this.btnOKChange = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.txtNewPINConfirm = new EspackFormControls.EspackMaskedTextBox();
            this.txtNewPIN = new EspackFormControls.EspackMaskedTextBox();
            this.txtNewPasswordConfirm = new EspackFormControls.EspackTextBox();
            this.txtNewPassword = new EspackFormControls.EspackTextBox();
            this.gbCred = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtPassword = new EspackFormControls.EspackTextBox();
            this.txtUser = new EspackFormControls.EspackTextBox();
            this.gbApps = new System.Windows.Forms.GroupBox();
            this.tlpApps = new System.Windows.Forms.TableLayoutPanel();
            this.gbDebug = new System.Windows.Forms.GroupBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.gbCredentials.SuspendLayout();
            this.gbChangePassword.SuspendLayout();
            this.gbCred.SuspendLayout();
            this.gbApps.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCredentials
            // 
            this.gbCredentials.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCredentials.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gbCredentials.Controls.Add(this.chkDebug);
            this.gbCredentials.Controls.Add(this.gbChangePassword);
            this.gbCredentials.Controls.Add(this.gbCred);
            this.gbCredentials.Location = new System.Drawing.Point(12, 12);
            this.gbCredentials.Name = "gbCredentials";
            this.gbCredentials.Size = new System.Drawing.Size(1686, 55);
            this.gbCredentials.TabIndex = 0;
            this.gbCredentials.TabStop = false;
            // 
            // gbChangePassword
            // 
            this.gbChangePassword.Controls.Add(this.btnCancelChange);
            this.gbChangePassword.Controls.Add(this.btnOKChange);
            this.gbChangePassword.Controls.Add(this.btnChange);
            this.gbChangePassword.Controls.Add(this.txtNewPINConfirm);
            this.gbChangePassword.Controls.Add(this.txtNewPIN);
            this.gbChangePassword.Controls.Add(this.txtNewPasswordConfirm);
            this.gbChangePassword.Controls.Add(this.txtNewPassword);
            this.gbChangePassword.Location = new System.Drawing.Point(319, 8);
            this.gbChangePassword.Name = "gbChangePassword";
            this.gbChangePassword.Size = new System.Drawing.Size(693, 41);
            this.gbChangePassword.TabIndex = 35;
            this.gbChangePassword.TabStop = false;
            // 
            // btnCancelChange
            // 
            this.btnCancelChange.Location = new System.Drawing.Point(604, 8);
            this.btnCancelChange.Name = "btnCancelChange";
            this.btnCancelChange.Size = new System.Drawing.Size(75, 27);
            this.btnCancelChange.TabIndex = 41;
            this.btnCancelChange.Text = "Cancel";
            this.btnCancelChange.UseVisualStyleBackColor = true;
            this.btnCancelChange.Visible = false;
            this.btnCancelChange.Click += new System.EventHandler(this.btnCancelChange_Click);
            // 
            // btnOKChange
            // 
            this.btnOKChange.Location = new System.Drawing.Point(523, 8);
            this.btnOKChange.Name = "btnOKChange";
            this.btnOKChange.Size = new System.Drawing.Size(75, 27);
            this.btnOKChange.TabIndex = 40;
            this.btnOKChange.Text = "Ok";
            this.btnOKChange.UseVisualStyleBackColor = true;
            this.btnOKChange.Visible = false;
            this.btnOKChange.Click += new System.EventHandler(this.btnOKChange_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(6, 8);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 27);
            this.btnChange.TabIndex = 39;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Visible = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtNewPINConfirm
            // 
            this.txtNewPINConfirm.Add = false;
            this.txtNewPINConfirm.BackColor = System.Drawing.Color.White;
            this.txtNewPINConfirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPINConfirm.Caption = "Confirm";
            this.txtNewPINConfirm.DBField = null;
            this.txtNewPINConfirm.DBFieldType = null;
            this.txtNewPINConfirm.DefaultValue = null;
            this.txtNewPINConfirm.Del = false;
            this.txtNewPINConfirm.DependingRS = null;
            this.txtNewPINConfirm.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNewPINConfirm.ForeColor = System.Drawing.Color.Black;
            this.txtNewPINConfirm.Location = new System.Drawing.Point(417, 13);
            this.txtNewPINConfirm.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtNewPINConfirm.Mask = "9999";
            this.txtNewPINConfirm.Name = "txtNewPINConfirm";
            this.txtNewPINConfirm.Order = 0;
            this.txtNewPINConfirm.ParentConn = null;
            this.txtNewPINConfirm.ParentDA = null;
            this.txtNewPINConfirm.PasswordChar = '*';
            this.txtNewPINConfirm.PK = false;
            this.txtNewPINConfirm.PromptChar = ' ';
            this.txtNewPINConfirm.Search = false;
            this.txtNewPINConfirm.Size = new System.Drawing.Size(100, 17);
            this.txtNewPINConfirm.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtNewPINConfirm.TabIndex = 38;
            this.txtNewPINConfirm.Upp = false;
            this.txtNewPINConfirm.UseSystemPasswordChar = true;
            this.txtNewPINConfirm.Value = "";
            this.txtNewPINConfirm.Visible = false;
            // 
            // txtNewPIN
            // 
            this.txtNewPIN.Add = false;
            this.txtNewPIN.BackColor = System.Drawing.Color.White;
            this.txtNewPIN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPIN.Caption = "PIN";
            this.txtNewPIN.DBField = null;
            this.txtNewPIN.DBFieldType = null;
            this.txtNewPIN.DefaultValue = null;
            this.txtNewPIN.Del = false;
            this.txtNewPIN.DependingRS = null;
            this.txtNewPIN.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNewPIN.ForeColor = System.Drawing.Color.Black;
            this.txtNewPIN.Location = new System.Drawing.Point(311, 13);
            this.txtNewPIN.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtNewPIN.Mask = "9999";
            this.txtNewPIN.Name = "txtNewPIN";
            this.txtNewPIN.Order = 0;
            this.txtNewPIN.ParentConn = null;
            this.txtNewPIN.ParentDA = null;
            this.txtNewPIN.PasswordChar = '*';
            this.txtNewPIN.PK = false;
            this.txtNewPIN.PromptChar = ' ';
            this.txtNewPIN.Search = false;
            this.txtNewPIN.Size = new System.Drawing.Size(100, 17);
            this.txtNewPIN.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtNewPIN.TabIndex = 37;
            this.txtNewPIN.Upp = false;
            this.txtNewPIN.UseSystemPasswordChar = true;
            this.txtNewPIN.Value = "";
            this.txtNewPIN.Visible = false;
            // 
            // txtNewPasswordConfirm
            // 
            this.txtNewPasswordConfirm.Add = false;
            this.txtNewPasswordConfirm.BackColor = System.Drawing.Color.White;
            this.txtNewPasswordConfirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPasswordConfirm.Caption = "Confirm";
            this.txtNewPasswordConfirm.DBField = null;
            this.txtNewPasswordConfirm.DBFieldType = null;
            this.txtNewPasswordConfirm.DefaultValue = null;
            this.txtNewPasswordConfirm.Del = false;
            this.txtNewPasswordConfirm.DependingRS = null;
            this.txtNewPasswordConfirm.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNewPasswordConfirm.ForeColor = System.Drawing.Color.Black;
            this.txtNewPasswordConfirm.Location = new System.Drawing.Point(193, 13);
            this.txtNewPasswordConfirm.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtNewPasswordConfirm.Multiline = true;
            this.txtNewPasswordConfirm.Name = "txtNewPasswordConfirm";
            this.txtNewPasswordConfirm.Order = 0;
            this.txtNewPasswordConfirm.ParentConn = null;
            this.txtNewPasswordConfirm.ParentDA = null;
            this.txtNewPasswordConfirm.PK = false;
            this.txtNewPasswordConfirm.Search = false;
            this.txtNewPasswordConfirm.Size = new System.Drawing.Size(100, 17);
            this.txtNewPasswordConfirm.Status = CommonTools.EnumStatus.EDIT;
            this.txtNewPasswordConfirm.TabIndex = 36;
            this.txtNewPasswordConfirm.Upp = true;
            this.txtNewPasswordConfirm.UseSystemPasswordChar = true;
            this.txtNewPasswordConfirm.Value = "";
            this.txtNewPasswordConfirm.Visible = false;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Add = false;
            this.txtNewPassword.BackColor = System.Drawing.Color.White;
            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPassword.Caption = "New Password";
            this.txtNewPassword.DBField = null;
            this.txtNewPassword.DBFieldType = null;
            this.txtNewPassword.DefaultValue = null;
            this.txtNewPassword.Del = false;
            this.txtNewPassword.DependingRS = null;
            this.txtNewPassword.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNewPassword.ForeColor = System.Drawing.Color.Black;
            this.txtNewPassword.Location = new System.Drawing.Point(87, 13);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtNewPassword.Multiline = true;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Order = 0;
            this.txtNewPassword.ParentConn = null;
            this.txtNewPassword.ParentDA = null;
            this.txtNewPassword.PK = false;
            this.txtNewPassword.Search = false;
            this.txtNewPassword.Size = new System.Drawing.Size(100, 17);
            this.txtNewPassword.Status = CommonTools.EnumStatus.EDIT;
            this.txtNewPassword.TabIndex = 35;
            this.txtNewPassword.Upp = true;
            this.txtNewPassword.UseSystemPasswordChar = true;
            this.txtNewPassword.Value = "";
            this.txtNewPassword.Visible = false;
            // 
            // gbCred
            // 
            this.gbCred.BackColor = System.Drawing.Color.Transparent;
            this.gbCred.Controls.Add(this.btnOk);
            this.gbCred.Controls.Add(this.txtPassword);
            this.gbCred.Controls.Add(this.txtUser);
            this.gbCred.Location = new System.Drawing.Point(6, 8);
            this.gbCred.Margin = new System.Windows.Forms.Padding(0);
            this.gbCred.Name = "gbCred";
            this.gbCred.Padding = new System.Windows.Forms.Padding(0);
            this.gbCred.Size = new System.Drawing.Size(310, 41);
            this.gbCred.TabIndex = 34;
            this.gbCred.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(226, 8);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 27);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            this.txtPassword.Location = new System.Drawing.Point(120, 14);
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
            this.txtUser.Location = new System.Drawing.Point(6, 14);
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
            this.tlpApps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1687F));
            this.tlpApps.Location = new System.Drawing.Point(0, 0);
            this.tlpApps.Name = "tlpApps";
            this.tlpApps.RowCount = 1;
            this.tlpApps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 533F));
            this.tlpApps.Size = new System.Drawing.Size(1687, 533);
            this.tlpApps.TabIndex = 2;
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
            this.gbDebug.Text = "Debug";
            // 
            // chkDebug
            // 
            this.chkDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDebug.AutoSize = true;
            this.chkDebug.Location = new System.Drawing.Point(1600, 26);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(58, 17);
            this.chkDebug.TabIndex = 36;
            this.chkDebug.Text = "Debug";
            this.chkDebug.UseVisualStyleBackColor = true;
            this.chkDebug.CheckedChanged += new System.EventHandler(this.chkDebug_CheckedChanged);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1710, 783);
            this.Controls.Add(this.gbDebug);
            this.Controls.Add(this.gbApps);
            this.Controls.Add(this.gbCredentials);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fMain";
            this.Text = "LogOn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.gbCredentials.ResumeLayout(false);
            this.gbCredentials.PerformLayout();
            this.gbChangePassword.ResumeLayout(false);
            this.gbChangePassword.PerformLayout();
            this.gbCred.ResumeLayout(false);
            this.gbCred.PerformLayout();
            this.gbApps.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCredentials;
        private System.Windows.Forms.GroupBox gbApps;
        private LogOnObjects.cAppBot cAppBot1;
        private System.Windows.Forms.TableLayoutPanel tlpApps;
        private System.Windows.Forms.TextBox txtDebug1;
        private System.Windows.Forms.GroupBox gbDebug;
        private System.Windows.Forms.GroupBox gbChangePassword;
        private System.Windows.Forms.Button btnCancelChange;
        private System.Windows.Forms.Button btnOKChange;
        private System.Windows.Forms.Button btnChange;
        private EspackFormControls.EspackMaskedTextBox txtNewPINConfirm;
        private EspackFormControls.EspackMaskedTextBox txtNewPIN;
        private EspackFormControls.EspackTextBox txtNewPasswordConfirm;
        private EspackFormControls.EspackTextBox txtNewPassword;
        private System.Windows.Forms.GroupBox gbCred;
        private System.Windows.Forms.Button btnOk;
        private EspackFormControls.EspackTextBox txtPassword;
        private EspackFormControls.EspackTextBox txtUser;
        private System.Windows.Forms.CheckBox chkDebug;
    }
}

