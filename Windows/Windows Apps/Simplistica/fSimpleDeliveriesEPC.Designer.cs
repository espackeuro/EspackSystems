namespace Simplistica
{
    partial class fSimpleDeliveriesEPC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fSimpleDeliveriesEPC));
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnSalida = new System.Windows.Forms.ToolStripButton();
            this.CTLM = new CTLMantenimientoNet.CTLMantenimientoNet();
            this.VS = new VSGrid.CtlVSGrid();
            this.txtDeliveryN = new EspackFormControls.EspackTextBox();
            this.cboShift = new EspackFormControls.EspackComboBox();
            this.txtUser = new EspackFormControls.EspackTextBox();
            this.cboDock = new EspackFormControls.EspackComboBox();
            this.cboDestination = new EspackFormControls.EspackComboBox();
            this.txtPlate = new EspackFormControls.EspackTextBox();
            this.dateCheckPoint = new EspackFormControls.EspackDateTimePicker();
            this.dateEPC = new EspackFormControls.EspackDateTimePicker();
            this.dateStart = new EspackFormControls.EspackDateTimePicker();
            this.dateEnd = new EspackFormControls.EspackDateTimePicker();
            this.lstFlags = new EspackFormControls.EspackCheckedListBox();
            this.cboService = new EspackFormControls.EspackComboBox();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VS)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(809, 652);
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSalida});
            this.toolStrip.Location = new System.Drawing.Point(299, 9);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(76, 31);
            this.toolStrip.TabIndex = 57;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnSalida
            // 
            this.btnSalida.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSalida.Image = ((System.Drawing.Image)(resources.GetObject("btnSalida.Image")));
            this.btnSalida.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalida.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalida.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalida.Name = "btnSalida";
            this.btnSalida.Size = new System.Drawing.Size(64, 28);
            this.btnSalida.Text = "Close";
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
            this.CTLM.TabIndex = 56;
            this.CTLM.Text = "ctlMantenimientoNet1";
            // 
            // VS
            // 
            this.VS.Add = false;
            this.VS.AllowDelete = false;
            this.VS.AllowInsert = false;
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
            this.VS.Location = new System.Drawing.Point(9, 236);
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
            this.VS.Size = new System.Drawing.Size(676, 308);
            this.VS.SQL = null;
            this.VS.sSPAdd = "";
            this.VS.sSPDel = "";
            this.VS.sSPUpp = "";
            this.VS.Status = CommonTools.EnumStatus.SEARCH;
            this.VS.TabIndex = 58;
            this.VS.Upp = false;
            this.VS.Value = null;
            // 
            // txtDeliveryN
            // 
            this.txtDeliveryN.Add = false;
            this.txtDeliveryN.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDeliveryN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeliveryN.Caption = "Delivery Number";
            this.txtDeliveryN.DBField = null;
            this.txtDeliveryN.DBFieldType = null;
            this.txtDeliveryN.DefaultValue = null;
            this.txtDeliveryN.Del = false;
            this.txtDeliveryN.DependingRS = null;
            this.txtDeliveryN.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDeliveryN.ForeColor = System.Drawing.Color.Gray;
            this.txtDeliveryN.Location = new System.Drawing.Point(9, 56);
            this.txtDeliveryN.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDeliveryN.Name = "txtDeliveryN";
            this.txtDeliveryN.Order = 0;
            this.txtDeliveryN.ParentConn = null;
            this.txtDeliveryN.ParentDA = null;
            this.txtDeliveryN.PK = false;
            this.txtDeliveryN.ReadOnly = true;
            this.txtDeliveryN.Search = false;
            this.txtDeliveryN.Size = new System.Drawing.Size(130, 17);
            this.txtDeliveryN.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDeliveryN.TabIndex = 59;
            this.txtDeliveryN.Upp = false;
            this.txtDeliveryN.Value = "";
            // 
            // cboShift
            // 
            this.cboShift.Add = false;
            this.cboShift.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboShift.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboShift.BackColor = System.Drawing.Color.White;
            this.cboShift.Caption = "Shift";
            this.cboShift.DBField = null;
            this.cboShift.DBFieldType = null;
            this.cboShift.DefaultValue = null;
            this.cboShift.Del = false;
            this.cboShift.DependingRS = null;
            this.cboShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboShift.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboShift.ForeColor = System.Drawing.Color.Black;
            this.cboShift.FormattingEnabled = true;
            this.cboShift.Items.AddRange(new object[] {
            "Morning",
            "Afternoon",
            "Night"});
            this.cboShift.Location = new System.Drawing.Point(9, 92);
            this.cboShift.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboShift.Name = "cboShift";
            this.cboShift.Order = 0;
            this.cboShift.ParentConn = null;
            this.cboShift.ParentDA = null;
            this.cboShift.PK = false;
            this.cboShift.Search = false;
            this.cboShift.Size = new System.Drawing.Size(130, 24);
            this.cboShift.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboShift.TabIndex = 61;
            this.cboShift.TBDescription = null;
            this.cboShift.Upp = false;
            this.cboShift.Value = "";
            // 
            // txtUser
            // 
            this.txtUser.Add = false;
            this.txtUser.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.Caption = "User";
            this.txtUser.DBField = null;
            this.txtUser.DBFieldType = null;
            this.txtUser.DefaultValue = null;
            this.txtUser.Del = false;
            this.txtUser.DependingRS = null;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtUser.ForeColor = System.Drawing.Color.Gray;
            this.txtUser.Location = new System.Drawing.Point(419, 56);
            this.txtUser.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtUser.Name = "txtUser";
            this.txtUser.Order = 0;
            this.txtUser.ParentConn = null;
            this.txtUser.ParentDA = null;
            this.txtUser.PK = false;
            this.txtUser.ReadOnly = true;
            this.txtUser.Search = false;
            this.txtUser.Size = new System.Drawing.Size(130, 17);
            this.txtUser.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtUser.TabIndex = 63;
            this.txtUser.Upp = false;
            this.txtUser.Value = "";
            // 
            // cboDock
            // 
            this.cboDock.Add = false;
            this.cboDock.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDock.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDock.BackColor = System.Drawing.Color.White;
            this.cboDock.Caption = "Dock";
            this.cboDock.DBField = null;
            this.cboDock.DBFieldType = null;
            this.cboDock.DefaultValue = null;
            this.cboDock.Del = false;
            this.cboDock.DependingRS = null;
            this.cboDock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDock.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboDock.ForeColor = System.Drawing.Color.Black;
            this.cboDock.FormattingEnabled = true;
            this.cboDock.Location = new System.Drawing.Point(146, 92);
            this.cboDock.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboDock.Name = "cboDock";
            this.cboDock.Order = 0;
            this.cboDock.ParentConn = null;
            this.cboDock.ParentDA = null;
            this.cboDock.PK = false;
            this.cboDock.Search = false;
            this.cboDock.Size = new System.Drawing.Size(130, 24);
            this.cboDock.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboDock.TabIndex = 69;
            this.cboDock.TBDescription = null;
            this.cboDock.Upp = false;
            this.cboDock.Value = "";
            // 
            // cboDestination
            // 
            this.cboDestination.Add = false;
            this.cboDestination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDestination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDestination.BackColor = System.Drawing.Color.White;
            this.cboDestination.Caption = "Destination";
            this.cboDestination.DBField = null;
            this.cboDestination.DBFieldType = null;
            this.cboDestination.DefaultValue = null;
            this.cboDestination.Del = false;
            this.cboDestination.DependingRS = null;
            this.cboDestination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDestination.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboDestination.ForeColor = System.Drawing.Color.Black;
            this.cboDestination.FormattingEnabled = true;
            this.cboDestination.Location = new System.Drawing.Point(283, 92);
            this.cboDestination.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboDestination.Name = "cboDestination";
            this.cboDestination.Order = 0;
            this.cboDestination.ParentConn = null;
            this.cboDestination.ParentDA = null;
            this.cboDestination.PK = false;
            this.cboDestination.Search = false;
            this.cboDestination.Size = new System.Drawing.Size(130, 24);
            this.cboDestination.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboDestination.TabIndex = 71;
            this.cboDestination.TBDescription = null;
            this.cboDestination.Upp = false;
            this.cboDestination.Value = "";
            // 
            // txtPlate
            // 
            this.txtPlate.Add = false;
            this.txtPlate.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtPlate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlate.Caption = "Plate";
            this.txtPlate.DBField = null;
            this.txtPlate.DBFieldType = null;
            this.txtPlate.DefaultValue = null;
            this.txtPlate.Del = false;
            this.txtPlate.DependingRS = null;
            this.txtPlate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPlate.ForeColor = System.Drawing.Color.Gray;
            this.txtPlate.Location = new System.Drawing.Point(283, 56);
            this.txtPlate.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtPlate.Name = "txtPlate";
            this.txtPlate.Order = 0;
            this.txtPlate.ParentConn = null;
            this.txtPlate.ParentDA = null;
            this.txtPlate.PK = false;
            this.txtPlate.ReadOnly = true;
            this.txtPlate.Search = false;
            this.txtPlate.Size = new System.Drawing.Size(130, 17);
            this.txtPlate.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtPlate.TabIndex = 73;
            this.txtPlate.Upp = false;
            this.txtPlate.Value = "";
            // 
            // dateCheckPoint
            // 
            this.dateCheckPoint.Add = false;
            this.dateCheckPoint.BackColor = System.Drawing.Color.White;
            this.dateCheckPoint.BorderColor = System.Drawing.Color.White;
            this.dateCheckPoint.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.dateCheckPoint.Caption = "CheckPoint Date";
            this.dateCheckPoint.Checked = false;
            this.dateCheckPoint.CustomFormat = " ";
            this.dateCheckPoint.DBField = null;
            this.dateCheckPoint.DBFieldType = null;
            this.dateCheckPoint.DefaultValue = null;
            this.dateCheckPoint.Del = false;
            this.dateCheckPoint.DependingRS = null;
            this.dateCheckPoint.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateCheckPoint.ForeColor = System.Drawing.Color.Black;
            this.dateCheckPoint.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateCheckPoint.Location = new System.Drawing.Point(555, 56);
            this.dateCheckPoint.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.dateCheckPoint.Name = "dateCheckPoint";
            this.dateCheckPoint.Nullable = false;
            this.dateCheckPoint.Order = 0;
            this.dateCheckPoint.ParentConn = null;
            this.dateCheckPoint.ParentDA = null;
            this.dateCheckPoint.PK = false;
            this.dateCheckPoint.Search = false;
            this.dateCheckPoint.Size = new System.Drawing.Size(130, 24);
            this.dateCheckPoint.Status = CommonTools.EnumStatus.ADDNEW;
            this.dateCheckPoint.TabIndex = 75;
            this.dateCheckPoint.Upp = false;
            this.dateCheckPoint.Value = null;
            // 
            // dateEPC
            // 
            this.dateEPC.Add = false;
            this.dateEPC.BackColor = System.Drawing.Color.White;
            this.dateEPC.BorderColor = System.Drawing.Color.White;
            this.dateEPC.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.dateEPC.Caption = "EPC Entry Date";
            this.dateEPC.Checked = false;
            this.dateEPC.CustomFormat = " ";
            this.dateEPC.DBField = null;
            this.dateEPC.DBFieldType = null;
            this.dateEPC.DefaultValue = null;
            this.dateEPC.Del = false;
            this.dateEPC.DependingRS = null;
            this.dateEPC.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateEPC.ForeColor = System.Drawing.Color.Black;
            this.dateEPC.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateEPC.Location = new System.Drawing.Point(555, 99);
            this.dateEPC.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.dateEPC.Name = "dateEPC";
            this.dateEPC.Nullable = false;
            this.dateEPC.Order = 0;
            this.dateEPC.ParentConn = null;
            this.dateEPC.ParentDA = null;
            this.dateEPC.PK = false;
            this.dateEPC.Search = false;
            this.dateEPC.Size = new System.Drawing.Size(130, 24);
            this.dateEPC.Status = CommonTools.EnumStatus.ADDNEW;
            this.dateEPC.TabIndex = 77;
            this.dateEPC.Upp = false;
            this.dateEPC.Value = null;
            // 
            // dateStart
            // 
            this.dateStart.Add = false;
            this.dateStart.BackColor = System.Drawing.Color.White;
            this.dateStart.BorderColor = System.Drawing.Color.White;
            this.dateStart.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.dateStart.Caption = "Load Start";
            this.dateStart.Checked = false;
            this.dateStart.CustomFormat = " ";
            this.dateStart.DBField = null;
            this.dateStart.DBFieldType = null;
            this.dateStart.DefaultValue = null;
            this.dateStart.Del = false;
            this.dateStart.DependingRS = null;
            this.dateStart.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateStart.ForeColor = System.Drawing.Color.Black;
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateStart.Location = new System.Drawing.Point(555, 142);
            this.dateStart.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.dateStart.Name = "dateStart";
            this.dateStart.Nullable = false;
            this.dateStart.Order = 0;
            this.dateStart.ParentConn = null;
            this.dateStart.ParentDA = null;
            this.dateStart.PK = false;
            this.dateStart.Search = false;
            this.dateStart.Size = new System.Drawing.Size(130, 24);
            this.dateStart.Status = CommonTools.EnumStatus.ADDNEW;
            this.dateStart.TabIndex = 79;
            this.dateStart.Upp = false;
            this.dateStart.Value = null;
            // 
            // dateEnd
            // 
            this.dateEnd.Add = false;
            this.dateEnd.BackColor = System.Drawing.Color.White;
            this.dateEnd.BorderColor = System.Drawing.Color.White;
            this.dateEnd.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.dateEnd.Caption = "Load End";
            this.dateEnd.Checked = false;
            this.dateEnd.CustomFormat = " ";
            this.dateEnd.DBField = null;
            this.dateEnd.DBFieldType = null;
            this.dateEnd.DefaultValue = null;
            this.dateEnd.Del = false;
            this.dateEnd.DependingRS = null;
            this.dateEnd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateEnd.ForeColor = System.Drawing.Color.Black;
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateEnd.Location = new System.Drawing.Point(555, 185);
            this.dateEnd.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Nullable = false;
            this.dateEnd.Order = 0;
            this.dateEnd.ParentConn = null;
            this.dateEnd.ParentDA = null;
            this.dateEnd.PK = false;
            this.dateEnd.Search = false;
            this.dateEnd.Size = new System.Drawing.Size(130, 24);
            this.dateEnd.Status = CommonTools.EnumStatus.ADDNEW;
            this.dateEnd.TabIndex = 81;
            this.dateEnd.Upp = false;
            this.dateEnd.Value = null;
            // 
            // lstFlags
            // 
            this.lstFlags.Add = false;
            this.lstFlags.BackColor = System.Drawing.Color.White;
            this.lstFlags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstFlags.Caption = "Flags";
            this.lstFlags.CheckOnClick = true;
            this.lstFlags.DBField = null;
            this.lstFlags.DBFieldType = null;
            this.lstFlags.DefaultValue = null;
            this.lstFlags.Del = false;
            this.lstFlags.DependingRS = null;
            this.lstFlags.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstFlags.ForeColor = System.Drawing.Color.Black;
            this.lstFlags.FormattingEnabled = true;
            this.lstFlags.Location = new System.Drawing.Point(9, 152);
            this.lstFlags.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstFlags.Name = "lstFlags";
            this.lstFlags.Order = 0;
            this.lstFlags.ParentConn = null;
            this.lstFlags.ParentDA = null;
            this.lstFlags.PK = false;
            this.lstFlags.Search = false;
            this.lstFlags.Size = new System.Drawing.Size(540, 57);
            this.lstFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstFlags.TabIndex = 84;
            this.lstFlags.Upp = false;
            this.lstFlags.Value = "";
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
            this.cboService.Location = new System.Drawing.Point(145, 49);
            this.cboService.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboService.Name = "cboService";
            this.cboService.Order = 0;
            this.cboService.ParentConn = null;
            this.cboService.ParentDA = null;
            this.cboService.PK = false;
            this.cboService.Search = false;
            this.cboService.Size = new System.Drawing.Size(130, 24);
            this.cboService.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboService.TabIndex = 97;
            this.cboService.TBDescription = null;
            this.cboService.Upp = false;
            this.cboService.Value = "";
            // 
            // fSimpleDeliveriesEPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(704, 586);
            this.Controls.Add(this.cboService);
            this.Controls.Add(this.lstFlags);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.dateStart);
            this.Controls.Add(this.dateEPC);
            this.Controls.Add(this.dateCheckPoint);
            this.Controls.Add(this.txtPlate);
            this.Controls.Add(this.cboDestination);
            this.Controls.Add(this.cboDock);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.cboShift);
            this.Controls.Add(this.txtDeliveryN);
            this.Controls.Add(this.VS);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.CTLM);
            this.Name = "fSimpleDeliveriesEPC";
            this.Text = "Simple Deliveries EPC";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnSalida;
        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private VSGrid.CtlVSGrid VS;
        private EspackFormControls.EspackTextBox txtDeliveryN;
        private EspackFormControls.EspackComboBox cboShift;
        private EspackFormControls.EspackTextBox txtUser;
        private EspackFormControls.EspackComboBox cboDock;
        private EspackFormControls.EspackComboBox cboDestination;
        private EspackFormControls.EspackTextBox txtPlate;
        private EspackFormControls.EspackDateTimePicker dateCheckPoint;
        private EspackFormControls.EspackDateTimePicker dateEPC;
        private EspackFormControls.EspackDateTimePicker dateStart;
        private EspackFormControls.EspackDateTimePicker dateEnd;
        private EspackFormControls.EspackCheckedListBox lstFlags;
        private EspackFormControls.EspackComboBox cboService;
    }
}