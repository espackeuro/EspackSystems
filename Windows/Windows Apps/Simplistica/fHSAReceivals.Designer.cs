namespace Simplistica
{
    partial class fHSAReceivals
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fHSAReceivals));
            this.CTLM = new CTLMantenimientoNet.CTLMantenimientoNet();
            this.txtReceivalCode = new EspackFormControls.EspackTextBox();
            this.txtContainer = new EspackFormControls.EspackTextBox();
            this.cboService = new EspackFormControls.EspackComboBox();
            this.txtDescService = new EspackFormControls.EspackTextBox();
            this.lstFlags = new EspackFormControls.EspackCheckedListBox();
            this.txtArrivalDate = new EspackFormControls.EspackTextBox();
            this.txtDate = new EspackFormControls.EspackDateTimePicker();
            this.txtLocation = new EspackFormControls.EspackTextBox();
            this.VS = new VSGrid.CtlVSGrid();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnRobotProcess = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportReceival = new System.Windows.Forms.ToolStripButton();
            this.txtPackingSlip = new EspackFormControls.EspackTextBox();
            this.tmrRobot = new System.Windows.Forms.Timer(this.components);
            this.pctRobotStatus = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.VS)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctRobotStatus)).BeginInit();
            this.SuspendLayout();
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
            this.CTLM.TabIndex = 1;
            this.CTLM.Text = "ctlMantenimientoNet1";
            // 
            // txtReceivalCode
            // 
            this.txtReceivalCode.Add = false;
            this.txtReceivalCode.BackColor = System.Drawing.Color.White;
            this.txtReceivalCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReceivalCode.Caption = "Receival Code";
            this.txtReceivalCode.DBField = null;
            this.txtReceivalCode.DBFieldType = null;
            this.txtReceivalCode.DefaultValue = null;
            this.txtReceivalCode.Del = false;
            this.txtReceivalCode.DependingRS = null;
            this.txtReceivalCode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtReceivalCode.ForeColor = System.Drawing.Color.Black;
            this.txtReceivalCode.Location = new System.Drawing.Point(450, 54);
            this.txtReceivalCode.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtReceivalCode.Name = "txtReceivalCode";
            this.txtReceivalCode.Order = 0;
            this.txtReceivalCode.ParentConn = null;
            this.txtReceivalCode.ParentDA = null;
            this.txtReceivalCode.PK = false;
            this.txtReceivalCode.Search = false;
            this.txtReceivalCode.Size = new System.Drawing.Size(100, 17);
            this.txtReceivalCode.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtReceivalCode.TabIndex = 22;
            this.txtReceivalCode.Upp = false;
            this.txtReceivalCode.Value = "";
            // 
            // txtContainer
            // 
            this.txtContainer.Add = false;
            this.txtContainer.BackColor = System.Drawing.Color.White;
            this.txtContainer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContainer.Caption = "Container";
            this.txtContainer.DBField = null;
            this.txtContainer.DBFieldType = null;
            this.txtContainer.DefaultValue = null;
            this.txtContainer.Del = false;
            this.txtContainer.DependingRS = null;
            this.txtContainer.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtContainer.ForeColor = System.Drawing.Color.Black;
            this.txtContainer.Location = new System.Drawing.Point(12, 97);
            this.txtContainer.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtContainer.Name = "txtContainer";
            this.txtContainer.Order = 0;
            this.txtContainer.ParentConn = null;
            this.txtContainer.ParentDA = null;
            this.txtContainer.PK = false;
            this.txtContainer.Search = false;
            this.txtContainer.Size = new System.Drawing.Size(157, 17);
            this.txtContainer.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtContainer.TabIndex = 24;
            this.txtContainer.Upp = false;
            this.txtContainer.Value = "";
            // 
            // cboService
            // 
            this.cboService.Add = false;
            this.cboService.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboService.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboService.BackColor = System.Drawing.Color.White;
            this.cboService.Caption = "Service";
            this.cboService.DBField = null;
            this.cboService.DBFieldType = null;
            this.cboService.DefaultValue = null;
            this.cboService.Del = false;
            this.cboService.DependingRS = null;
            this.cboService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboService.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboService.ForeColor = System.Drawing.Color.Black;
            this.cboService.FormattingEnabled = true;
            this.cboService.Location = new System.Drawing.Point(12, 54);
            this.cboService.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboService.Name = "cboService";
            this.cboService.Order = 0;
            this.cboService.ParentConn = null;
            this.cboService.ParentDA = null;
            this.cboService.PK = false;
            this.cboService.Search = false;
            this.cboService.Size = new System.Drawing.Size(110, 24);
            this.cboService.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboService.TabIndex = 30;
            this.cboService.TBDescription = null;
            this.cboService.Upp = false;
            this.cboService.Value = "";
            // 
            // txtDescService
            // 
            this.txtDescService.Add = false;
            this.txtDescService.BackColor = System.Drawing.Color.White;
            this.txtDescService.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescService.Caption = "";
            this.txtDescService.DBField = null;
            this.txtDescService.DBFieldType = null;
            this.txtDescService.DefaultValue = null;
            this.txtDescService.Del = false;
            this.txtDescService.DependingRS = null;
            this.txtDescService.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDescService.ForeColor = System.Drawing.Color.Black;
            this.txtDescService.Location = new System.Drawing.Point(128, 54);
            this.txtDescService.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDescService.Name = "txtDescService";
            this.txtDescService.Order = 0;
            this.txtDescService.ParentConn = null;
            this.txtDescService.ParentDA = null;
            this.txtDescService.PK = false;
            this.txtDescService.Search = false;
            this.txtDescService.Size = new System.Drawing.Size(316, 17);
            this.txtDescService.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDescService.TabIndex = 32;
            this.txtDescService.Upp = false;
            this.txtDescService.Value = "";
            // 
            // lstFlags
            // 
            this.lstFlags.Add = false;
            this.lstFlags.BackColor = System.Drawing.Color.White;
            this.lstFlags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstFlags.Caption = "";
            this.lstFlags.CheckOnClick = true;
            this.lstFlags.DBField = null;
            this.lstFlags.DBFieldType = null;
            this.lstFlags.DefaultValue = null;
            this.lstFlags.Del = false;
            this.lstFlags.DependingRS = null;
            this.lstFlags.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstFlags.ForeColor = System.Drawing.Color.Black;
            this.lstFlags.FormattingEnabled = true;
            this.lstFlags.Location = new System.Drawing.Point(9, 126);
            this.lstFlags.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstFlags.Name = "lstFlags";
            this.lstFlags.Order = 0;
            this.lstFlags.ParentConn = null;
            this.lstFlags.ParentDA = null;
            this.lstFlags.PK = false;
            this.lstFlags.Search = false;
            this.lstFlags.Size = new System.Drawing.Size(690, 57);
            this.lstFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstFlags.TabIndex = 35;
            this.lstFlags.Upp = false;
            this.lstFlags.Value = "";
            // 
            // txtArrivalDate
            // 
            this.txtArrivalDate.Add = false;
            this.txtArrivalDate.BackColor = System.Drawing.Color.White;
            this.txtArrivalDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtArrivalDate.Caption = "Arrival Date";
            this.txtArrivalDate.DBField = null;
            this.txtArrivalDate.DBFieldType = null;
            this.txtArrivalDate.DefaultValue = null;
            this.txtArrivalDate.Del = false;
            this.txtArrivalDate.DependingRS = null;
            this.txtArrivalDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtArrivalDate.ForeColor = System.Drawing.Color.Black;
            this.txtArrivalDate.Location = new System.Drawing.Point(556, 97);
            this.txtArrivalDate.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtArrivalDate.Name = "txtArrivalDate";
            this.txtArrivalDate.Order = 0;
            this.txtArrivalDate.ParentConn = null;
            this.txtArrivalDate.ParentDA = null;
            this.txtArrivalDate.PK = false;
            this.txtArrivalDate.Search = false;
            this.txtArrivalDate.Size = new System.Drawing.Size(143, 17);
            this.txtArrivalDate.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtArrivalDate.TabIndex = 42;
            this.txtArrivalDate.Upp = false;
            this.txtArrivalDate.Value = "";
            // 
            // txtDate
            // 
            this.txtDate.Add = false;
            this.txtDate.BackColor = System.Drawing.Color.White;
            this.txtDate.BorderColor = System.Drawing.Color.White;
            this.txtDate.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.txtDate.Caption = "Date";
            this.txtDate.CustomFormat = "dd/MM/yyyy H:mm";
            this.txtDate.DBField = null;
            this.txtDate.DBFieldType = null;
            this.txtDate.DefaultValue = null;
            this.txtDate.Del = false;
            this.txtDate.DependingRS = null;
            this.txtDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDate.ForeColor = System.Drawing.Color.Black;
            this.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDate.Location = new System.Drawing.Point(556, 51);
            this.txtDate.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDate.Name = "txtDate";
            this.txtDate.Nullable = true;
            this.txtDate.Order = 0;
            this.txtDate.ParentConn = null;
            this.txtDate.ParentDA = null;
            this.txtDate.PK = false;
            this.txtDate.Search = false;
            this.txtDate.ShowCheckBox = true;
            this.txtDate.Size = new System.Drawing.Size(143, 24);
            this.txtDate.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDate.TabIndex = 44;
            this.txtDate.Upp = false;
            this.txtDate.Value = new System.DateTime(2016, 6, 22, 13, 36, 41, 91);
            // 
            // txtLocation
            // 
            this.txtLocation.Add = false;
            this.txtLocation.BackColor = System.Drawing.Color.White;
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocation.Caption = "Location";
            this.txtLocation.DBField = null;
            this.txtLocation.DBFieldType = null;
            this.txtLocation.DefaultValue = null;
            this.txtLocation.Del = false;
            this.txtLocation.DependingRS = null;
            this.txtLocation.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtLocation.ForeColor = System.Drawing.Color.Black;
            this.txtLocation.Location = new System.Drawing.Point(393, 97);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Order = 0;
            this.txtLocation.ParentConn = null;
            this.txtLocation.ParentDA = null;
            this.txtLocation.PK = false;
            this.txtLocation.Search = false;
            this.txtLocation.Size = new System.Drawing.Size(157, 17);
            this.txtLocation.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtLocation.TabIndex = 46;
            this.txtLocation.Upp = false;
            this.txtLocation.Value = "";
            // 
            // VS
            // 
            this.VS.Add = false;
            this.VS.AllowDelete = true;
            this.VS.AllowInsert = true;
            this.VS.AllowUpdate = false;
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
            this.VS.Location = new System.Drawing.Point(9, 189);
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
            this.VS.Size = new System.Drawing.Size(690, 308);
            this.VS.SQL = null;
            this.VS.sSPAdd = "";
            this.VS.sSPDel = "";
            this.VS.sSPUpp = "";
            this.VS.Status = CommonTools.EnumStatus.SEARCH;
            this.VS.TabIndex = 47;
            this.VS.Upp = false;
            this.VS.Value = null;
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRobotProcess,
            this.toolStripSeparator1,
            this.btnExportReceival});
            this.toolStrip.Location = new System.Drawing.Point(507, 9);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(194, 25);
            this.toolStrip.TabIndex = 56;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnRobotProcess
            // 
            this.btnRobotProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRobotProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnRobotProcess.Image")));
            this.btnRobotProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRobotProcess.Name = "btnRobotProcess";
            this.btnRobotProcess.Size = new System.Drawing.Size(86, 22);
            this.btnRobotProcess.Text = "Robot Process";
            this.btnRobotProcess.ToolTipText = "Launch CMMS Robot Process";
            this.btnRobotProcess.Click += new System.EventHandler(this.btnRobotProcess_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportReceival
            // 
            this.btnExportReceival.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExportReceival.Image = ((System.Drawing.Image)(resources.GetObject("btnExportReceival.Image")));
            this.btnExportReceival.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportReceival.Name = "btnExportReceival";
            this.btnExportReceival.Size = new System.Drawing.Size(90, 22);
            this.btnExportReceival.Text = "Export Receival";
            this.btnExportReceival.ToolTipText = "Export to LOGISTICA Receivals";
            // 
            // txtPackingSlip
            // 
            this.txtPackingSlip.Add = false;
            this.txtPackingSlip.BackColor = System.Drawing.Color.White;
            this.txtPackingSlip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPackingSlip.Caption = "Packing Slip";
            this.txtPackingSlip.DBField = null;
            this.txtPackingSlip.DBFieldType = null;
            this.txtPackingSlip.DefaultValue = null;
            this.txtPackingSlip.Del = false;
            this.txtPackingSlip.DependingRS = null;
            this.txtPackingSlip.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPackingSlip.ForeColor = System.Drawing.Color.Black;
            this.txtPackingSlip.Location = new System.Drawing.Point(175, 97);
            this.txtPackingSlip.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtPackingSlip.Name = "txtPackingSlip";
            this.txtPackingSlip.Order = 0;
            this.txtPackingSlip.ParentConn = null;
            this.txtPackingSlip.ParentDA = null;
            this.txtPackingSlip.PK = false;
            this.txtPackingSlip.Search = false;
            this.txtPackingSlip.Size = new System.Drawing.Size(157, 17);
            this.txtPackingSlip.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtPackingSlip.TabIndex = 66;
            this.txtPackingSlip.Upp = false;
            this.txtPackingSlip.Value = "";
            // 
            // tmrRobot
            // 
            this.tmrRobot.Interval = 10000;
            this.tmrRobot.Tick += new System.EventHandler(this.tmrRobot_Tick);
            // 
            // pctRobotStatus
            // 
            this.pctRobotStatus.Location = new System.Drawing.Point(474, 5);
            this.pctRobotStatus.Name = "pctRobotStatus";
            this.pctRobotStatus.Size = new System.Drawing.Size(30, 30);
            this.pctRobotStatus.TabIndex = 70;
            this.pctRobotStatus.TabStop = false;
            // 
            // fHSAReceivals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 518);
            this.Controls.Add(this.pctRobotStatus);
            this.Controls.Add(this.txtPackingSlip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.VS);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.txtArrivalDate);
            this.Controls.Add(this.lstFlags);
            this.Controls.Add(this.txtDescService);
            this.Controls.Add(this.cboService);
            this.Controls.Add(this.txtContainer);
            this.Controls.Add(this.txtReceivalCode);
            this.Controls.Add(this.CTLM);
            this.Name = "fHSAReceivals";
            this.ShowIcon = false;
            this.Text = "HSA Receivals";
            ((System.ComponentModel.ISupportInitialize)(this.VS)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctRobotStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private EspackFormControls.EspackTextBox txtReceivalCode;
        private EspackFormControls.EspackTextBox txtContainer;
        private EspackFormControls.EspackComboBox cboService;
        private EspackFormControls.EspackTextBox txtDescService;
        private EspackFormControls.EspackCheckedListBox lstFlags;
        private EspackFormControls.EspackTextBox txtArrivalDate;
        private EspackFormControls.EspackDateTimePicker txtDate;
        private EspackFormControls.EspackTextBox txtLocation;
        private VSGrid.CtlVSGrid VS;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnRobotProcess;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportReceival;
        private EspackFormControls.EspackTextBox txtPackingSlip;
        private System.Windows.Forms.Timer tmrRobot;
        private System.Windows.Forms.PictureBox pctRobotStatus;
    }
}