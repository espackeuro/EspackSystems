namespace Sistemas
{
    partial class fProfiles
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescription = new EspackFormControls.EspackTextBox();
            this.lstCOD3 = new EspackFormControls.EspackCheckedListBox();
            this.lstFlags = new EspackFormControls.EspackCheckedListBox();
            this.txtProfileCode = new EspackFormControls.EspackTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstServiceDefaultFlags = new EspackFormControls.EspackCheckedListBox();
            this.VS = new VSGrid.CtlVSGrid();
            this.CTLM = new CTLMantenimientoNet.CTLMantenimientoNet();
            this.ProfileCode = new VSGrid.CtlVSTextBoxColumn();
            this.Service = new VSGrid.CtlVSTextBoxColumn();
            this.DefaultFlags = new VSGrid.CtlVSTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VS)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.lstCOD3);
            this.groupBox1.Controls.Add(this.lstFlags);
            this.groupBox1.Controls.Add(this.txtProfileCode);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 184);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profile Header";
            // 
            // txtDescription
            // 
            this.txtDescription.Add = false;
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Caption = "Description";
            this.txtDescription.DBField = null;
            this.txtDescription.DBFieldType = null;
            this.txtDescription.DefaultValue = null;
            this.txtDescription.Del = false;
            this.txtDescription.DependingRS = null;
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(240, 33);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Order = 0;
            this.txtDescription.ParentConn = null;
            this.txtDescription.ParentDA = null;
            this.txtDescription.PK = false;
            this.txtDescription.Search = false;
            this.txtDescription.Size = new System.Drawing.Size(345, 22);
            this.txtDescription.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDescription.TabIndex = 29;
            this.txtDescription.Upp = false;
            this.txtDescription.Value = "";
            // 
            // lstCOD3
            // 
            this.lstCOD3.Add = false;
            this.lstCOD3.BackColor = System.Drawing.Color.White;
            this.lstCOD3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstCOD3.Caption = "COD3";
            this.lstCOD3.CheckOnClick = true;
            this.lstCOD3.ColumnWidth = 270;
            this.lstCOD3.DBField = null;
            this.lstCOD3.DBFieldType = null;
            this.lstCOD3.DefaultValue = null;
            this.lstCOD3.Del = false;
            this.lstCOD3.DependingRS = null;
            this.lstCOD3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstCOD3.ForeColor = System.Drawing.Color.Black;
            this.lstCOD3.FormattingEnabled = true;
            this.lstCOD3.Location = new System.Drawing.Point(6, 75);
            this.lstCOD3.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstCOD3.MultiColumn = true;
            this.lstCOD3.Name = "lstCOD3";
            this.lstCOD3.Order = 0;
            this.lstCOD3.ParentConn = null;
            this.lstCOD3.ParentDA = null;
            this.lstCOD3.PK = false;
            this.lstCOD3.Search = false;
            this.lstCOD3.Size = new System.Drawing.Size(400, 95);
            this.lstCOD3.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstCOD3.TabIndex = 26;
            this.lstCOD3.Upp = false;
            this.lstCOD3.Value = "";
            // 
            // lstFlags
            // 
            this.lstFlags.Add = false;
            this.lstFlags.BackColor = System.Drawing.Color.White;
            this.lstFlags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstFlags.Caption = "Flags";
            this.lstFlags.CheckOnClick = true;
            this.lstFlags.ColumnWidth = 180;
            this.lstFlags.DBField = null;
            this.lstFlags.DBFieldType = null;
            this.lstFlags.DefaultValue = null;
            this.lstFlags.Del = false;
            this.lstFlags.DependingRS = null;
            this.lstFlags.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstFlags.ForeColor = System.Drawing.Color.Black;
            this.lstFlags.FormattingEnabled = true;
            this.lstFlags.Location = new System.Drawing.Point(412, 75);
            this.lstFlags.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstFlags.MultiColumn = true;
            this.lstFlags.Name = "lstFlags";
            this.lstFlags.Order = 0;
            this.lstFlags.ParentConn = null;
            this.lstFlags.ParentDA = null;
            this.lstFlags.PK = false;
            this.lstFlags.Search = false;
            this.lstFlags.Size = new System.Drawing.Size(174, 95);
            this.lstFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstFlags.TabIndex = 25;
            this.lstFlags.Upp = false;
            this.lstFlags.Value = "";
            // 
            // txtProfileCode
            // 
            this.txtProfileCode.Add = false;
            this.txtProfileCode.BackColor = System.Drawing.Color.White;
            this.txtProfileCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProfileCode.Caption = "Profile Code";
            this.txtProfileCode.DBField = null;
            this.txtProfileCode.DBFieldType = null;
            this.txtProfileCode.DefaultValue = null;
            this.txtProfileCode.Del = false;
            this.txtProfileCode.DependingRS = null;
            this.txtProfileCode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtProfileCode.ForeColor = System.Drawing.Color.Black;
            this.txtProfileCode.Location = new System.Drawing.Point(6, 32);
            this.txtProfileCode.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtProfileCode.Multiline = true;
            this.txtProfileCode.Name = "txtProfileCode";
            this.txtProfileCode.Order = 0;
            this.txtProfileCode.ParentConn = null;
            this.txtProfileCode.ParentDA = null;
            this.txtProfileCode.PK = false;
            this.txtProfileCode.Search = false;
            this.txtProfileCode.Size = new System.Drawing.Size(225, 24);
            this.txtProfileCode.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtProfileCode.TabIndex = 0;
            this.txtProfileCode.Upp = false;
            this.txtProfileCode.Value = "";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Controls.Add(this.lstServiceDefaultFlags);
            this.groupBox2.Controls.Add(this.VS);
            this.groupBox2.Location = new System.Drawing.Point(9, 231);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(595, 387);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // lstServiceDefaultFlags
            // 
            this.lstServiceDefaultFlags.Add = false;
            this.lstServiceDefaultFlags.BackColor = System.Drawing.Color.White;
            this.lstServiceDefaultFlags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstServiceDefaultFlags.Caption = "Service Default Flags";
            this.lstServiceDefaultFlags.CheckOnClick = true;
            this.lstServiceDefaultFlags.ColumnWidth = 180;
            this.lstServiceDefaultFlags.DBField = null;
            this.lstServiceDefaultFlags.DBFieldType = null;
            this.lstServiceDefaultFlags.DefaultValue = null;
            this.lstServiceDefaultFlags.Del = false;
            this.lstServiceDefaultFlags.DependingRS = null;
            this.lstServiceDefaultFlags.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstServiceDefaultFlags.ForeColor = System.Drawing.Color.Black;
            this.lstServiceDefaultFlags.FormattingEnabled = true;
            this.lstServiceDefaultFlags.Location = new System.Drawing.Point(9, 24);
            this.lstServiceDefaultFlags.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstServiceDefaultFlags.MultiColumn = true;
            this.lstServiceDefaultFlags.Name = "lstServiceDefaultFlags";
            this.lstServiceDefaultFlags.Order = 0;
            this.lstServiceDefaultFlags.ParentConn = null;
            this.lstServiceDefaultFlags.ParentDA = null;
            this.lstServiceDefaultFlags.PK = false;
            this.lstServiceDefaultFlags.Search = false;
            this.lstServiceDefaultFlags.Size = new System.Drawing.Size(579, 76);
            this.lstServiceDefaultFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstServiceDefaultFlags.TabIndex = 30;
            this.lstServiceDefaultFlags.Upp = false;
            this.lstServiceDefaultFlags.Value = "";
            // 
            // VS
            // 
            this.VS.Add = false;
            this.VS.AllowDelete = true;
            this.VS.AllowInsert = true;
            this.VS.AllowUpdate = false;
            this.VS.AllowUserToAddRows = false;
            this.VS.Caption = "";
            this.VS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProfileCode,
            this.Service,
            this.DefaultFlags});
            this.VS.Conn = null;
            this.VS.DBField = null;
            this.VS.DBFieldType = null;
            this.VS.DBTable = null;
            this.VS.DefaultValue = null;
            this.VS.Del = false;
            this.VS.DependingRS = null;
            this.VS.EspackControlParent = null;
            this.VS.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.VS.Location = new System.Drawing.Point(10, 106);
            this.VS.MsgStatusLabel = null;
            this.VS.Name = "VS";
            this.VS.NumPages = 0;
            this.VS.Order = 0;
            this.VS.Page = 0;
            this.VS.Paginate = false;
            this.VS.ParentConn = null;
            this.VS.ParentDA = null;
            this.VS.PK = false;
            this.VS.RowHeadersVisible = false;
            this.VS.Search = false;
            this.VS.Size = new System.Drawing.Size(579, 264);
            this.VS.SQL = null;
            this.VS.sSPAdd = "";
            this.VS.sSPDel = "";
            this.VS.sSPUpp = "";
            this.VS.Status = CommonTools.EnumStatus.SEARCH;
            this.VS.TabIndex = 3;
            this.VS.Upp = false;
            this.VS.Value = null;
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

            this.CTLM.TabIndex = 1;
            this.CTLM.Text = "CTLM";
            // 
            // ProfileCode
            // 
            this.ProfileCode.Add = true;
            this.ProfileCode.Alignment = null;
            this.ProfileCode.Attr = null;
            this.ProfileCode.AutoCompleteCustomSource = null;
            this.ProfileCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.ProfileCode.AutoCompleteQuery = null;
            this.ProfileCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.ProfileCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProfileCode.Caption = null;
            this.ProfileCode.CaptionLabel = null;
            this.ProfileCode.ChangedCols = null;
            this.ProfileCode.Colnumber = 0;
            this.ProfileCode.DBField = "ProfileCode";
            this.ProfileCode.DBFieldType = null;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.ProfileCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.ProfileCode.DefaultValue = null;
            this.ProfileCode.Del = true;
            this.ProfileCode.HeaderText = "ProfileCode";
            this.ProfileCode.IsFlag = false;
            this.ProfileCode.LinkedControl = this.txtProfileCode;
            this.ProfileCode.Location = new System.Drawing.Point(0, 0);
            this.ProfileCode.Locked = true;
            this.ProfileCode.Name = "ProfileCode";
            this.ProfileCode.Order = 0;
            this.ProfileCode.Parent = null;
            this.ProfileCode.PK = false;
            this.ProfileCode.ReadOnly = true;
            this.ProfileCode.RowColor = null;
            this.ProfileCode.Search = false;
            this.ProfileCode.Sortable = false;
            this.ProfileCode.SPAddParamName = "@ProfileCode";
            this.ProfileCode.SPDelParamName = "@ProfileCode";
            this.ProfileCode.SPUppParamName = "@ProfileCode";
            this.ProfileCode.Status = CommonTools.EnumStatus.ADDNEW;
            this.ProfileCode.Upp = true;
            this.ProfileCode.Value = "";
            this.ProfileCode.Visible = false;
            // 
            // Service
            // 
            this.Service.Add = true;
            this.Service.Alignment = null;
            this.Service.Attr = null;
            this.Service.AutoCompleteCustomSource = null;
            this.Service.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.Service.AutoCompleteQuery = "Select ServiceCode from Services order by ServiceCode";
            this.Service.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Service.Caption = null;
            this.Service.CaptionLabel = null;
            this.Service.ChangedCols = null;
            this.Service.Colnumber = 0;
            this.Service.DBField = "Service";
            this.Service.DBFieldType = null;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.Service.DefaultCellStyle = dataGridViewCellStyle2;
            this.Service.DefaultValue = null;
            this.Service.Del = true;
            this.Service.HeaderText = "Service";
            this.Service.IsFlag = false;
            this.Service.LinkedControl = null;
            this.Service.Location = new System.Drawing.Point(0, 0);
            this.Service.Locked = true;
            this.Service.Name = "Service";
            this.Service.Order = 0;
            this.Service.Parent = null;
            this.Service.PK = false;
            this.Service.ReadOnly = true;
            this.Service.RowColor = null;
            this.Service.Search = false;
            this.Service.Sortable = false;
            this.Service.SPAddParamName = "@Service";
            this.Service.SPDelParamName = "@Service";
            this.Service.SPUppParamName = "@Service";
            this.Service.Status = CommonTools.EnumStatus.ADDNEW;
            this.Service.Upp = true;
            this.Service.Value = "";
            // 
            // DefaultFlags
            // 
            this.DefaultFlags.Add = true;
            this.DefaultFlags.Alignment = null;
            this.DefaultFlags.Attr = null;
            this.DefaultFlags.AutoCompleteCustomSource = null;
            this.DefaultFlags.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.DefaultFlags.AutoCompleteQuery = null;
            this.DefaultFlags.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.DefaultFlags.Caption = null;
            this.DefaultFlags.CaptionLabel = null;
            this.DefaultFlags.ChangedCols = null;
            this.DefaultFlags.Colnumber = 0;
            this.DefaultFlags.DBField = "DefaultFlags";
            this.DefaultFlags.DBFieldType = null;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.DefaultFlags.DefaultCellStyle = dataGridViewCellStyle3;
            this.DefaultFlags.DefaultValue = null;
            this.DefaultFlags.Del = false;
            this.DefaultFlags.HeaderText = "Default Flags";
            this.DefaultFlags.IsFlag = false;
            this.DefaultFlags.LinkedControl = null;
            this.DefaultFlags.Location = new System.Drawing.Point(0, 0);
            this.DefaultFlags.Locked = true;
            this.DefaultFlags.Name = "DefaultFlags";
            this.DefaultFlags.Order = 0;
            this.DefaultFlags.Parent = null;
            this.DefaultFlags.PK = false;
            this.DefaultFlags.ReadOnly = true;
            this.DefaultFlags.RowColor = null;
            this.DefaultFlags.Search = false;
            this.DefaultFlags.Sortable = false;
            this.DefaultFlags.SPAddParamName = "@DefaultFlags";
            this.DefaultFlags.SPDelParamName = "";
            this.DefaultFlags.SPUppParamName = "@DefaultFlags";
            this.DefaultFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.DefaultFlags.Upp = true;
            this.DefaultFlags.Value = "";
            this.DefaultFlags.Width = 250;
            // 
            // fProfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 650);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CTLM);
            this.Name = "fProfiles";
            this.Text = "Profiles";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private System.Windows.Forms.GroupBox groupBox1;
        private EspackFormControls.EspackTextBox txtProfileCode;
        private EspackFormControls.EspackCheckedListBox lstCOD3;
        private EspackFormControls.EspackCheckedListBox lstFlags;
        private VSGrid.CtlVSGrid VS;
        private EspackFormControls.EspackTextBox txtDescription;
        private System.Windows.Forms.GroupBox groupBox2;
        private EspackFormControls.EspackCheckedListBox lstServiceDefaultFlags;
        private VSGrid.CtlVSTextBoxColumn ProfileCode;
        private VSGrid.CtlVSTextBoxColumn Service;
        private VSGrid.CtlVSTextBoxColumn DefaultFlags;
    }
}