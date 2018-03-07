namespace Sistemas
{
    partial class fFlags
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
            this.CTLM = new CTLMantenimientoNet.CTLMantenimientoNet();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboTable = new EspackFormControls.EspackComboBox();
            this.txtCode = new EspackFormControls.EspackTextBox();
            this.txtLetter = new EspackFormControls.EspackTextBox();
            this.txtIdReg = new EspackFormControls.EspackTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDesFlagEng = new EspackFormControls.EspackTextBox();
            this.txtDescFlag = new EspackFormControls.EspackTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstServices = new EspackFormControls.EspackCheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CTLM
            // 
            this.CTLM.Clear = false;
            this.CTLM.Conn = null;
            this.CTLM.DBTable = null;
            this.CTLM.Dock = System.Windows.Forms.DockStyle.None;
            this.CTLM.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.CTLM.Location = new System.Drawing.Point(0, 0);
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
            this.CTLM.Text = "ctlMantenimientoNet1";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.cboTable);
            this.groupBox1.Controls.Add(this.txtCode);
            this.groupBox1.Controls.Add(this.txtLetter);
            this.groupBox1.Controls.Add(this.txtIdReg);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(703, 73);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Id";
            // 
            // cboTable
            // 
            this.cboTable.Add = false;
            this.cboTable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTable.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTable.BackColor = System.Drawing.Color.White;
            this.cboTable.Caption = "Table";
            this.cboTable.DBField = null;
            this.cboTable.DBFieldType = null;
            this.cboTable.DefaultValue = null;
            this.cboTable.Del = false;
            this.cboTable.DependingRS = null;
            this.cboTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTable.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboTable.ForeColor = System.Drawing.Color.Black;
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(177, 32);
            this.cboTable.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboTable.Name = "cboTable";
            this.cboTable.Order = 0;
            this.cboTable.ParentConn = null;
            this.cboTable.ParentDA = null;
            this.cboTable.PK = false;
            this.cboTable.Search = false;
            this.cboTable.Size = new System.Drawing.Size(250, 24);
            this.cboTable.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboTable.TabIndex = 11;
            this.cboTable.TBDescription = null;
            this.cboTable.Upp = false;
            this.cboTable.Value = "";
            // 
            // txtCode
            // 
            this.txtCode.Add = false;
            this.txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.Caption = "Code";
            this.txtCode.DBField = null;
            this.txtCode.DBFieldType = null;
            this.txtCode.DefaultValue = null;
            this.txtCode.Del = false;
            this.txtCode.DependingRS = null;
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCode.ForeColor = System.Drawing.Color.Gray;
            this.txtCode.Location = new System.Drawing.Point(564, 32);
            this.txtCode.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Order = 0;
            this.txtCode.ParentConn = null;
            this.txtCode.ParentDA = null;
            this.txtCode.PK = false;
            this.txtCode.ReadOnly = true;
            this.txtCode.Search = false;
            this.txtCode.Size = new System.Drawing.Size(125, 24);
            this.txtCode.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtCode.TabIndex = 15;
            this.txtCode.Upp = false;
            this.txtCode.Value = "";
            // 
            // txtLetter
            // 
            this.txtLetter.Add = false;
            this.txtLetter.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtLetter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLetter.Caption = "Letter";
            this.txtLetter.DBField = null;
            this.txtLetter.DBFieldType = null;
            this.txtLetter.DefaultValue = null;
            this.txtLetter.Del = false;
            this.txtLetter.DependingRS = null;
            this.txtLetter.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtLetter.ForeColor = System.Drawing.Color.Gray;
            this.txtLetter.Location = new System.Drawing.Point(464, 32);
            this.txtLetter.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtLetter.Multiline = true;
            this.txtLetter.Name = "txtLetter";
            this.txtLetter.Order = 0;
            this.txtLetter.ParentConn = null;
            this.txtLetter.ParentDA = null;
            this.txtLetter.PK = false;
            this.txtLetter.ReadOnly = true;
            this.txtLetter.Search = false;
            this.txtLetter.Size = new System.Drawing.Size(55, 24);
            this.txtLetter.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtLetter.TabIndex = 13;
            this.txtLetter.Upp = false;
            this.txtLetter.Value = "";
            // 
            // txtIdReg
            // 
            this.txtIdReg.Add = false;
            this.txtIdReg.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtIdReg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIdReg.Caption = "IdReg";
            this.txtIdReg.DBField = null;
            this.txtIdReg.DBFieldType = null;
            this.txtIdReg.DefaultValue = null;
            this.txtIdReg.Del = false;
            this.txtIdReg.DependingRS = null;
            this.txtIdReg.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtIdReg.ForeColor = System.Drawing.Color.Gray;
            this.txtIdReg.Location = new System.Drawing.Point(17, 32);
            this.txtIdReg.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtIdReg.Multiline = true;
            this.txtIdReg.Name = "txtIdReg";
            this.txtIdReg.Order = 0;
            this.txtIdReg.ParentConn = null;
            this.txtIdReg.ParentDA = null;
            this.txtIdReg.PK = false;
            this.txtIdReg.ReadOnly = true;
            this.txtIdReg.Search = false;
            this.txtIdReg.Size = new System.Drawing.Size(125, 24);
            this.txtIdReg.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtIdReg.TabIndex = 9;
            this.txtIdReg.Upp = false;
            this.txtIdReg.Value = "";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Controls.Add(this.txtDesFlagEng);
            this.groupBox2.Controls.Add(this.txtDescFlag);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(13, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(703, 71);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Description";
            // 
            // txtDesFlagEng
            // 
            this.txtDesFlagEng.Add = false;
            this.txtDesFlagEng.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDesFlagEng.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDesFlagEng.Caption = "DescFlagEng";
            this.txtDesFlagEng.DBField = null;
            this.txtDesFlagEng.DBFieldType = null;
            this.txtDesFlagEng.DefaultValue = null;
            this.txtDesFlagEng.Del = false;
            this.txtDesFlagEng.DependingRS = null;
            this.txtDesFlagEng.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDesFlagEng.ForeColor = System.Drawing.Color.Gray;
            this.txtDesFlagEng.Location = new System.Drawing.Point(499, 32);
            this.txtDesFlagEng.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDesFlagEng.Multiline = true;
            this.txtDesFlagEng.Name = "txtDesFlagEng";
            this.txtDesFlagEng.Order = 0;
            this.txtDesFlagEng.ParentConn = null;
            this.txtDesFlagEng.ParentDA = null;
            this.txtDesFlagEng.PK = false;
            this.txtDesFlagEng.ReadOnly = true;
            this.txtDesFlagEng.Search = false;
            this.txtDesFlagEng.Size = new System.Drawing.Size(190, 24);
            this.txtDesFlagEng.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDesFlagEng.TabIndex = 2;
            this.txtDesFlagEng.Upp = false;
            this.txtDesFlagEng.Value = "";
            // 
            // txtDescFlag
            // 
            this.txtDescFlag.Add = false;
            this.txtDescFlag.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDescFlag.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescFlag.Caption = "DescFlag";
            this.txtDescFlag.DBField = null;
            this.txtDescFlag.DBFieldType = null;
            this.txtDescFlag.DefaultValue = null;
            this.txtDescFlag.Del = false;
            this.txtDescFlag.DependingRS = null;
            this.txtDescFlag.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDescFlag.ForeColor = System.Drawing.Color.Gray;
            this.txtDescFlag.Location = new System.Drawing.Point(17, 32);
            this.txtDescFlag.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDescFlag.Multiline = true;
            this.txtDescFlag.Name = "txtDescFlag";
            this.txtDescFlag.Order = 0;
            this.txtDescFlag.ParentConn = null;
            this.txtDescFlag.ParentDA = null;
            this.txtDescFlag.PK = false;
            this.txtDescFlag.ReadOnly = true;
            this.txtDescFlag.Search = false;
            this.txtDescFlag.Size = new System.Drawing.Size(190, 24);
            this.txtDescFlag.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDescFlag.TabIndex = 0;
            this.txtDescFlag.Upp = false;
            this.txtDescFlag.Value = "";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox3.Controls.Add(this.lstServices);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(12, 197);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(703, 326);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Services";
            // 
            // lstServices
            // 
            this.lstServices.Add = false;
            this.lstServices.BackColor = System.Drawing.Color.White;
            this.lstServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstServices.Caption = "";
            this.lstServices.CheckOnClick = true;
            this.lstServices.ColumnWidth = 225;
            this.lstServices.DBField = null;
            this.lstServices.DBFieldType = null;
            this.lstServices.DefaultValue = null;
            this.lstServices.Del = false;
            this.lstServices.DependingRS = null;
            this.lstServices.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstServices.ForeColor = System.Drawing.Color.Black;
            this.lstServices.FormattingEnabled = true;
            this.lstServices.Location = new System.Drawing.Point(14, 21);
            this.lstServices.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstServices.MultiColumn = true;
            this.lstServices.Name = "lstServices";
            this.lstServices.Order = 0;
            this.lstServices.ParentConn = null;
            this.lstServices.ParentDA = null;
            this.lstServices.PK = false;
            this.lstServices.Search = false;
            this.lstServices.Size = new System.Drawing.Size(675, 285);
            this.lstServices.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstServices.TabIndex = 0;
            this.lstServices.Upp = false;
            this.lstServices.Value = "";
            // 
            // fFlags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 586);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.CTLM);
            this.Controls.Add(this.groupBox1);
            this.Name = "fFlags";
            this.ShowIcon = false;
            this.Text = "Flags";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private System.Windows.Forms.GroupBox groupBox1;
        private EspackFormControls.EspackComboBox cboTable;
        private EspackFormControls.EspackTextBox txtIdReg;
        private EspackFormControls.EspackTextBox txtCode;
        private EspackFormControls.EspackTextBox txtLetter;
        private System.Windows.Forms.GroupBox groupBox2;
        private EspackFormControls.EspackTextBox txtDesFlagEng;
        private EspackFormControls.EspackTextBox txtDescFlag;
        private System.Windows.Forms.GroupBox groupBox3;
        private EspackFormControls.EspackCheckedListBox lstServices;
    }
}