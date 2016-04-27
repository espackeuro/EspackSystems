namespace Sistemas
{
    partial class fAlias
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
            this.txtDomain = new EspackFormControls.EspackTextBox();
            this.txtLocalPart = new EspackFormControls.EspackTextBox();
            this.txtAddress = new EspackFormControls.EspackTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstCOD3 = new EspackFormControls.EspackCheckedListBox();
            this.lstFlags = new EspackFormControls.EspackCheckedListBox();
            this.VS = new VSGrid.CtlVSGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VS)).BeginInit();
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
            this.CTLM.RSPosition = -1;
            this.CTLM.Size = new System.Drawing.Size(290, 29);
            this.CTLM.sSPAdd = "";
            this.CTLM.sSPDel = "";
            this.CTLM.sSPUpp = "";
            this.CTLM.Status = CommonTools.EnumStatus.SEARCH;
            this.CTLM.TabIndex = 0;
            this.CTLM.Text = "CTLM";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.txtDomain);
            this.groupBox1.Controls.Add(this.txtLocalPart);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(13, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 110);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Header";
            // 
            // txtDomain
            // 
            this.txtDomain.Add = false;
            this.txtDomain.BackColor = System.Drawing.Color.White;
            this.txtDomain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDomain.Caption = "Domain";
            this.txtDomain.DBField = null;
            this.txtDomain.DBFieldType = null;
            this.txtDomain.DefaultValue = null;
            this.txtDomain.Del = false;
            this.txtDomain.DependingRS = null;
            this.txtDomain.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDomain.ForeColor = System.Drawing.Color.Black;
            this.txtDomain.Location = new System.Drawing.Point(343, 75);
            this.txtDomain.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDomain.Multiline = true;
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Order = 0;
            this.txtDomain.ParentConn = null;
            this.txtDomain.ParentDA = null;
            this.txtDomain.PK = false;
            this.txtDomain.Search = false;
            this.txtDomain.Size = new System.Drawing.Size(225, 24);
            this.txtDomain.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDomain.TabIndex = 18;
            this.txtDomain.Upp = false;
            this.txtDomain.Value = "";
            // 
            // txtLocalPart
            // 
            this.txtLocalPart.Add = false;
            this.txtLocalPart.BackColor = System.Drawing.Color.White;
            this.txtLocalPart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocalPart.Caption = "LocalPart";
            this.txtLocalPart.DBField = null;
            this.txtLocalPart.DBFieldType = null;
            this.txtLocalPart.DefaultValue = null;
            this.txtLocalPart.Del = false;
            this.txtLocalPart.DependingRS = null;
            this.txtLocalPart.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtLocalPart.ForeColor = System.Drawing.Color.Black;
            this.txtLocalPart.Location = new System.Drawing.Point(28, 75);
            this.txtLocalPart.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtLocalPart.Multiline = true;
            this.txtLocalPart.Name = "txtLocalPart";
            this.txtLocalPart.Order = 0;
            this.txtLocalPart.ParentConn = null;
            this.txtLocalPart.ParentDA = null;
            this.txtLocalPart.PK = false;
            this.txtLocalPart.Search = false;
            this.txtLocalPart.Size = new System.Drawing.Size(225, 24);
            this.txtLocalPart.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtLocalPart.TabIndex = 16;
            this.txtLocalPart.Upp = false;
            this.txtLocalPart.Value = "";
            // 
            // txtAddress
            // 
            this.txtAddress.Add = false;
            this.txtAddress.BackColor = System.Drawing.Color.White;
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.Caption = "Address";
            this.txtAddress.DBField = null;
            this.txtAddress.DBFieldType = null;
            this.txtAddress.DefaultValue = null;
            this.txtAddress.Del = false;
            this.txtAddress.DependingRS = null;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtAddress.ForeColor = System.Drawing.Color.Black;
            this.txtAddress.Location = new System.Drawing.Point(28, 32);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Order = 0;
            this.txtAddress.ParentConn = null;
            this.txtAddress.ParentDA = null;
            this.txtAddress.PK = false;
            this.txtAddress.Search = false;
            this.txtAddress.Size = new System.Drawing.Size(540, 24);
            this.txtAddress.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtAddress.TabIndex = 0;
            this.txtAddress.Upp = false;
            this.txtAddress.Value = "";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Controls.Add(this.lstCOD3);
            this.groupBox2.Controls.Add(this.lstFlags);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(13, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(592, 233);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Various";
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
            this.lstCOD3.Location = new System.Drawing.Point(28, 127);
            this.lstCOD3.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstCOD3.MultiColumn = true;
            this.lstCOD3.Name = "lstCOD3";
            this.lstCOD3.Order = 0;
            this.lstCOD3.ParentConn = null;
            this.lstCOD3.ParentDA = null;
            this.lstCOD3.PK = false;
            this.lstCOD3.Search = false;
            this.lstCOD3.Size = new System.Drawing.Size(540, 95);
            this.lstCOD3.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstCOD3.TabIndex = 2;
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
            this.lstFlags.Location = new System.Drawing.Point(28, 32);
            this.lstFlags.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstFlags.MultiColumn = true;
            this.lstFlags.Name = "lstFlags";
            this.lstFlags.Order = 0;
            this.lstFlags.ParentConn = null;
            this.lstFlags.ParentDA = null;
            this.lstFlags.PK = false;
            this.lstFlags.Search = false;
            this.lstFlags.Size = new System.Drawing.Size(540, 76);
            this.lstFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstFlags.TabIndex = 0;
            this.lstFlags.Upp = false;
            this.lstFlags.Value = "";
            // 
            // VS
            // 
            this.VS.Add = false;
            this.VS.AllowDelete = true;
            this.VS.AllowInsert = true;
            this.VS.AllowUpdate = true;
            this.VS.AllowUserToAddRows = false;
            this.VS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.VS.Caption = "";
            this.VS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VS.Conn = null;
            this.VS.DBField = null;
            this.VS.DBFieldType = null;
            this.VS.DBTable = null;
            this.VS.DefaultValue = null;
            this.VS.Del = false;
            this.VS.DependingRS = null;
            this.VS.EspackControlParent = null;
            this.VS.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.VS.Location = new System.Drawing.Point(12, 392);
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
            this.VS.Size = new System.Drawing.Size(592, 198);
            this.VS.SQL = null;
            this.VS.sSPAdd = "";
            this.VS.sSPDel = "";
            this.VS.sSPUpp = "";
            this.VS.Status = CommonTools.EnumStatus.SEARCH;
            this.VS.TabIndex = 3;
            this.VS.Upp = false;
            this.VS.Value = null;
            // 
            // fAlias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 650);
            this.Controls.Add(this.VS);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CTLM);
            this.KeyPreview = true;
            this.Name = "fAlias";
            this.Text = "Aliases";
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
        private EspackFormControls.EspackTextBox txtAddress;
        private System.Windows.Forms.GroupBox groupBox2;
        private EspackFormControls.EspackTextBox txtDomain;
        private EspackFormControls.EspackTextBox txtLocalPart;
        private EspackFormControls.EspackCheckedListBox lstFlags;
        private VSGrid.CtlVSGrid VS;
        private EspackFormControls.EspackCheckedListBox lstCOD3;
    }
}