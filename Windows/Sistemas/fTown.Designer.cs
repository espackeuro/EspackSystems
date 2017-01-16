namespace Sistemas
{
    partial class fTown
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
            this.txtExternalIP = new EspackFormControls.EspackTextBox();
            this.txtMask = new EspackFormControls.EspackTextBox();
            this.txtSubNet = new EspackFormControls.EspackTextBox();
            this.txtCOD3Name = new EspackFormControls.EspackTextBox();
            this.cboCOD3 = new EspackFormControls.EspackComboBox();
            this.CTLM = new CTLMantenimientoNet.CTLMantenimientoNet();
            this.SuspendLayout();
            // 
            // txtExternalIP
            // 
            this.txtExternalIP.Add = false;
            this.txtExternalIP.BackColor = System.Drawing.Color.White;
            this.txtExternalIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExternalIP.Caption = "External IP";
            this.txtExternalIP.DBField = null;
            this.txtExternalIP.DBFieldType = null;
            this.txtExternalIP.DefaultValue = null;
            this.txtExternalIP.Del = false;
            this.txtExternalIP.DependingRS = null;
            this.txtExternalIP.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtExternalIP.ForeColor = System.Drawing.Color.Black;
            this.txtExternalIP.Location = new System.Drawing.Point(58, 200);
            this.txtExternalIP.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtExternalIP.Name = "txtExternalIP";
            this.txtExternalIP.Order = 0;
            this.txtExternalIP.ParentConn = null;
            this.txtExternalIP.ParentDA = null;
            this.txtExternalIP.PK = false;
            this.txtExternalIP.Search = false;
            this.txtExternalIP.Size = new System.Drawing.Size(352, 17);
            this.txtExternalIP.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtExternalIP.TabIndex = 15;
            this.txtExternalIP.Upp = false;
            this.txtExternalIP.Value = "";
            // 
            // txtMask
            // 
            this.txtMask.Add = false;
            this.txtMask.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtMask.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMask.Caption = "Mask";
            this.txtMask.DBField = null;
            this.txtMask.DBFieldType = null;
            this.txtMask.DefaultValue = null;
            this.txtMask.Del = false;
            this.txtMask.DependingRS = null;
            this.txtMask.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMask.ForeColor = System.Drawing.Color.Black;
            this.txtMask.Location = new System.Drawing.Point(58, 157);
            this.txtMask.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            //this.txtMask.Multiline = true;
            this.txtMask.Name = "txtMask";
            this.txtMask.Order = 0;
            this.txtMask.ParentConn = null;
            this.txtMask.ParentDA = null;
            this.txtMask.PK = false;
            this.txtMask.ReadOnly = true;
            this.txtMask.Search = false;
            this.txtMask.Size = new System.Drawing.Size(130, 24);
            this.txtMask.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtMask.TabIndex = 10;
            this.txtMask.Upp = false;
            this.txtMask.Value = "";
            // 
            // txtSubNet
            // 
            this.txtSubNet.Add = false;
            this.txtSubNet.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSubNet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSubNet.Caption = "SubNet";
            this.txtSubNet.DBField = null;
            this.txtSubNet.DBFieldType = null;
            this.txtSubNet.DefaultValue = null;
            this.txtSubNet.Del = false;
            this.txtSubNet.DependingRS = null;
            this.txtSubNet.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSubNet.ForeColor = System.Drawing.Color.Black;
            this.txtSubNet.Location = new System.Drawing.Point(58, 114);
            this.txtSubNet.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            //this.txtSubNet.Multiline = true;
            this.txtSubNet.Name = "txtSubNet";
            this.txtSubNet.Order = 0;
            this.txtSubNet.ParentConn = null;
            this.txtSubNet.ParentDA = null;
            this.txtSubNet.PK = false;
            this.txtSubNet.ReadOnly = true;
            this.txtSubNet.Search = false;
            this.txtSubNet.Size = new System.Drawing.Size(130, 24);
            this.txtSubNet.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtSubNet.TabIndex = 7;
            this.txtSubNet.Upp = false;
            this.txtSubNet.Value = "";
            // 
            // txtCOD3Name
            // 
            this.txtCOD3Name.Add = false;
            this.txtCOD3Name.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCOD3Name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCOD3Name.Caption = "";
            this.txtCOD3Name.DBField = null;
            this.txtCOD3Name.DBFieldType = null;
            this.txtCOD3Name.DefaultValue = null;
            this.txtCOD3Name.Del = false;
            this.txtCOD3Name.DependingRS = null;
            this.txtCOD3Name.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCOD3Name.ForeColor = System.Drawing.Color.Black;
            this.txtCOD3Name.Location = new System.Drawing.Point(206, 72);
            this.txtCOD3Name.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            //this.txtCOD3Name.Multiline = true;
            this.txtCOD3Name.Name = "txtCOD3Name";
            this.txtCOD3Name.Order = 0;
            this.txtCOD3Name.ParentConn = null;
            this.txtCOD3Name.ParentDA = null;
            this.txtCOD3Name.PK = false;
            this.txtCOD3Name.ReadOnly = true;
            this.txtCOD3Name.Search = false;
            this.txtCOD3Name.Size = new System.Drawing.Size(204, 24);
            this.txtCOD3Name.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtCOD3Name.TabIndex = 5;
            this.txtCOD3Name.Upp = false;
            this.txtCOD3Name.Value = "";
            // 
            // cboCOD3
            // 
            this.cboCOD3.Add = false;
            this.cboCOD3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCOD3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCOD3.BackColor = System.Drawing.Color.White;
            this.cboCOD3.Caption = "COD3";
            this.cboCOD3.DBField = null;
            this.cboCOD3.DBFieldType = null;
            this.cboCOD3.DefaultValue = null;
            this.cboCOD3.Del = false;
            this.cboCOD3.DependingRS = null;
            this.cboCOD3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCOD3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboCOD3.ForeColor = System.Drawing.Color.Black;
            this.cboCOD3.FormattingEnabled = true;
            this.cboCOD3.Location = new System.Drawing.Point(58, 71);
            this.cboCOD3.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboCOD3.Name = "cboCOD3";
            this.cboCOD3.Order = 0;
            this.cboCOD3.ParentConn = null;
            this.cboCOD3.ParentDA = null;
            this.cboCOD3.PK = false;
            this.cboCOD3.Search = false;
            this.cboCOD3.Size = new System.Drawing.Size(130, 17);
            this.cboCOD3.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboCOD3.TabIndex = 3;
            this.cboCOD3.TBDescription = null;
            this.cboCOD3.Upp = false;
            this.cboCOD3.Value = "";
            // 
            // CTLM
            // 
            this.CTLM.Clear = false;
            this.CTLM.Conn = null;
            this.CTLM.DBTable = null;
            this.CTLM.Dock = System.Windows.Forms.DockStyle.None;
            this.CTLM.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.CTLM.Location = new System.Drawing.Point(9, 9);
            this.CTLM.MsgStatusInfoLabel = null;
            this.CTLM.MsgStatusLabel = null;
            this.CTLM.Name = "CTLM";
            this.CTLM.ReQuery = false;
            
            this.CTLM.Size = new System.Drawing.Size(290, 29);
            this.CTLM.sSPAdd = "";
            this.CTLM.sSPDel = "";
            this.CTLM.sSPUpp = "";

            this.CTLM.StatusBarProgress = null;
            this.CTLM.TabIndex = 0;
            this.CTLM.Text = "CTLM";
            // 
            // fTown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 275);
            this.Controls.Add(this.txtExternalIP);
            this.Controls.Add(this.txtMask);
            this.Controls.Add(this.txtSubNet);
            this.Controls.Add(this.txtCOD3Name);
            this.Controls.Add(this.cboCOD3);
            this.Controls.Add(this.CTLM);
            this.Name = "fTown";
            this.Text = "Towns";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private EspackFormControls.EspackComboBox cboCOD3;
        private EspackFormControls.EspackTextBox txtCOD3Name;
        private EspackFormControls.EspackTextBox txtSubNet;
        private EspackFormControls.EspackTextBox txtMask;
        private EspackFormControls.EspackTextBox txtExternalIP;
    }
}