namespace Sistemas
{
    partial class fTasks
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboMatterDef = new EspackFormControls.EspackComboBox();
            this.cboServiceDef = new EspackFormControls.EspackComboBox();
            this.cboPlaceAffectedDef = new EspackFormControls.EspackComboBox();
            this.cboPlaceAdviseDef = new EspackFormControls.EspackComboBox();
            this.CalDateDef = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.CTLM = new CTLMantenimientoNet.CTLMantenimientoNet();
            this.txtIT = new EspackFormControls.EspackTextBox();
            this.chkFlags = new EspackFormControls.EspackCheckedListBox();
            this.txtNotes = new EspackFormControls.EspackTextBox();
            this.txtKm = new EspackFormControls.NumericTextBox();
            this.dateEnd = new EspackFormControls.EspackDateTimePicker();
            this.dateStart = new EspackFormControls.EspackDateTimePicker();
            this.cboMatter = new EspackFormControls.EspackComboBox();
            this.cboService = new EspackFormControls.EspackComboBox();
            this.txtPlaceAffected = new EspackFormControls.EspackTextBox();
            this.cboPlaceAffected = new EspackFormControls.EspackComboBox();
            this.txtPlaceAdvise = new EspackFormControls.EspackTextBox();
            this.cboPlaceAdvise = new EspackFormControls.EspackComboBox();
            this.txtPerson = new EspackFormControls.EspackTextBox();
            this.txtIdTarea = new EspackFormControls.EspackTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboMatterDef);
            this.groupBox1.Controls.Add(this.cboServiceDef);
            this.groupBox1.Controls.Add(this.cboPlaceAffectedDef);
            this.groupBox1.Controls.Add(this.cboPlaceAdviseDef);
            this.groupBox1.Controls.Add(this.CalDateDef);
            this.groupBox1.Location = new System.Drawing.Point(13, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 207);
            this.groupBox1.TabIndex = 112;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Defaults";
            // 
            // cboMatterDef
            // 
            this.cboMatterDef.Add = true;
            this.cboMatterDef.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboMatterDef.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMatterDef.BackColor = System.Drawing.Color.White;
            this.cboMatterDef.Caption = "Matter";
            this.cboMatterDef.DBField = null;
            this.cboMatterDef.DBFieldType = null;
            this.cboMatterDef.DefaultValue = null;
            this.cboMatterDef.Del = false;
            this.cboMatterDef.DependingRS = null;
            this.cboMatterDef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMatterDef.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboMatterDef.ForeColor = System.Drawing.Color.Black;
            this.cboMatterDef.FormattingEnabled = true;
            this.cboMatterDef.Location = new System.Drawing.Point(216, 157);
            this.cboMatterDef.Margin = new System.Windows.Forms.Padding(3, 163, 3, 3);
            this.cboMatterDef.Name = "cboMatterDef";
            this.cboMatterDef.Order = 0;
            this.cboMatterDef.ParentConn = null;
            this.cboMatterDef.ParentDA = null;
            this.cboMatterDef.PK = false;
            this.cboMatterDef.Search = false;
            this.cboMatterDef.Size = new System.Drawing.Size(130, 24);
            this.cboMatterDef.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboMatterDef.TabIndex = 120;
            this.cboMatterDef.TBDescription = null;
            this.cboMatterDef.Upp = false;
            this.cboMatterDef.Value = "";
            // 
            // cboServiceDef
            // 
            this.cboServiceDef.Add = true;
            this.cboServiceDef.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboServiceDef.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboServiceDef.BackColor = System.Drawing.Color.White;
            this.cboServiceDef.Caption = "Service";
            this.cboServiceDef.DBField = null;
            this.cboServiceDef.DBFieldType = null;
            this.cboServiceDef.DefaultValue = null;
            this.cboServiceDef.Del = false;
            this.cboServiceDef.DependingRS = null;
            this.cboServiceDef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboServiceDef.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboServiceDef.ForeColor = System.Drawing.Color.Black;
            this.cboServiceDef.FormattingEnabled = true;
            this.cboServiceDef.Location = new System.Drawing.Point(216, 116);
            this.cboServiceDef.Margin = new System.Windows.Forms.Padding(3, 163, 3, 3);
            this.cboServiceDef.Name = "cboServiceDef";
            this.cboServiceDef.Order = 0;
            this.cboServiceDef.ParentConn = null;
            this.cboServiceDef.ParentDA = null;
            this.cboServiceDef.PK = false;
            this.cboServiceDef.Search = false;
            this.cboServiceDef.Size = new System.Drawing.Size(130, 24);
            this.cboServiceDef.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboServiceDef.TabIndex = 119;
            this.cboServiceDef.TBDescription = null;
            this.cboServiceDef.Upp = false;
            this.cboServiceDef.Value = "";
            // 
            // cboPlaceAffectedDef
            // 
            this.cboPlaceAffectedDef.Add = true;
            this.cboPlaceAffectedDef.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPlaceAffectedDef.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPlaceAffectedDef.BackColor = System.Drawing.Color.White;
            this.cboPlaceAffectedDef.Caption = "Place Affected";
            this.cboPlaceAffectedDef.DBField = null;
            this.cboPlaceAffectedDef.DBFieldType = null;
            this.cboPlaceAffectedDef.DefaultValue = null;
            this.cboPlaceAffectedDef.Del = false;
            this.cboPlaceAffectedDef.DependingRS = null;
            this.cboPlaceAffectedDef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPlaceAffectedDef.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboPlaceAffectedDef.ForeColor = System.Drawing.Color.Black;
            this.cboPlaceAffectedDef.FormattingEnabled = true;
            this.cboPlaceAffectedDef.Location = new System.Drawing.Point(216, 75);
            this.cboPlaceAffectedDef.Margin = new System.Windows.Forms.Padding(3, 163, 3, 3);
            this.cboPlaceAffectedDef.Name = "cboPlaceAffectedDef";
            this.cboPlaceAffectedDef.Order = 0;
            this.cboPlaceAffectedDef.ParentConn = null;
            this.cboPlaceAffectedDef.ParentDA = null;
            this.cboPlaceAffectedDef.PK = false;
            this.cboPlaceAffectedDef.Search = false;
            this.cboPlaceAffectedDef.Size = new System.Drawing.Size(130, 24);
            this.cboPlaceAffectedDef.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboPlaceAffectedDef.TabIndex = 116;
            this.cboPlaceAffectedDef.TBDescription = null;
            this.cboPlaceAffectedDef.Upp = false;
            this.cboPlaceAffectedDef.Value = "";
            // 
            // cboPlaceAdviseDef
            // 
            this.cboPlaceAdviseDef.Add = true;
            this.cboPlaceAdviseDef.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPlaceAdviseDef.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPlaceAdviseDef.BackColor = System.Drawing.Color.White;
            this.cboPlaceAdviseDef.Caption = "Place Advise";
            this.cboPlaceAdviseDef.DBField = null;
            this.cboPlaceAdviseDef.DBFieldType = null;
            this.cboPlaceAdviseDef.DefaultValue = null;
            this.cboPlaceAdviseDef.Del = false;
            this.cboPlaceAdviseDef.DependingRS = null;
            this.cboPlaceAdviseDef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPlaceAdviseDef.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboPlaceAdviseDef.ForeColor = System.Drawing.Color.Black;
            this.cboPlaceAdviseDef.FormattingEnabled = true;
            this.cboPlaceAdviseDef.Location = new System.Drawing.Point(216, 38);
            this.cboPlaceAdviseDef.Margin = new System.Windows.Forms.Padding(3, 163, 3, 3);
            this.cboPlaceAdviseDef.Name = "cboPlaceAdviseDef";
            this.cboPlaceAdviseDef.Order = 0;
            this.cboPlaceAdviseDef.ParentConn = null;
            this.cboPlaceAdviseDef.ParentDA = null;
            this.cboPlaceAdviseDef.PK = false;
            this.cboPlaceAdviseDef.Search = false;
            this.cboPlaceAdviseDef.Size = new System.Drawing.Size(130, 24);
            this.cboPlaceAdviseDef.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboPlaceAdviseDef.TabIndex = 115;
            this.cboPlaceAdviseDef.TBDescription = null;
            this.cboPlaceAdviseDef.Upp = false;
            this.cboPlaceAdviseDef.Value = "";
            // 
            // CalDateDef
            // 
            this.CalDateDef.Location = new System.Drawing.Point(12, 25);
            this.CalDateDef.Name = "CalDateDef";
            this.CalDateDef.TabIndex = 112;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(954, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 115;
            this.label1.Text = "IT:";
            // 
            // CTLM
            // 
            this.CTLM.Clear = false;
            this.CTLM.Conn = null;
            this.CTLM.DBTable = null;
            this.CTLM.Dock = System.Windows.Forms.DockStyle.None;
            this.CTLM.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.CTLM.Location = new System.Drawing.Point(13, 13);
            this.CTLM.MsgStatusInfoLabel = null;
            this.CTLM.MsgStatusLabel = null;
            this.CTLM.Name = "CTLM";
            
            this.CTLM.Size = new System.Drawing.Size(290, 29);
            this.CTLM.sSPAdd = "";
            this.CTLM.sSPDel = "";
            this.CTLM.sSPUpp = "";

            this.CTLM.TabIndex = 0;
            this.CTLM.Text = "ctlMantenimientoNet1";
            // 
            // txtIT
            // 
            this.txtIT.Add = false;
            this.txtIT.BackColor = System.Drawing.Color.White;
            this.txtIT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIT.Caption = "";
            this.txtIT.DBField = null;
            this.txtIT.DBFieldType = null;
            this.txtIT.DefaultValue = null;
            this.txtIT.Del = false;
            this.txtIT.DependingRS = null;
            this.txtIT.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtIT.ForeColor = System.Drawing.Color.Black;
            this.txtIT.Location = new System.Drawing.Point(980, 13);
            this.txtIT.Margin = new System.Windows.Forms.Padding(3, 46, 3, 3);
            this.txtIT.Multiline = true;
            this.txtIT.Name = "txtIT";
            this.txtIT.Order = 0;
            this.txtIT.ParentConn = null;
            this.txtIT.ParentDA = null;
            this.txtIT.PK = false;
            this.txtIT.Search = false;
            this.txtIT.Size = new System.Drawing.Size(100, 30);
            this.txtIT.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtIT.TabIndex = 113;
            this.txtIT.Upp = false;
            this.txtIT.Value = "";
            // 
            // chkFlags
            // 
            this.chkFlags.Add = false;
            this.chkFlags.BackColor = System.Drawing.Color.White;
            this.chkFlags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkFlags.Caption = "Flags";
            this.chkFlags.CheckOnClick = true;
            this.chkFlags.DBField = null;
            this.chkFlags.DBFieldType = null;
            this.chkFlags.DefaultValue = null;
            this.chkFlags.Del = false;
            this.chkFlags.DependingRS = null;
            this.chkFlags.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chkFlags.ForeColor = System.Drawing.Color.Black;
            this.chkFlags.FormattingEnabled = true;
            this.chkFlags.Location = new System.Drawing.Point(839, 226);
            this.chkFlags.Margin = new System.Windows.Forms.Padding(3, 136, 3, 3);
            this.chkFlags.Name = "chkFlags";
            this.chkFlags.Order = 0;
            this.chkFlags.ParentConn = null;
            this.chkFlags.ParentDA = null;
            this.chkFlags.PK = false;
            this.chkFlags.Search = false;
            this.chkFlags.Size = new System.Drawing.Size(241, 19);
            this.chkFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.chkFlags.TabIndex = 109;
            this.chkFlags.Upp = false;
            this.chkFlags.Value = "";
            // 
            // txtNotes
            // 
            this.txtNotes.Add = false;
            this.txtNotes.BackColor = System.Drawing.Color.White;
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotes.Caption = "Notes";
            this.txtNotes.DBField = null;
            this.txtNotes.DBFieldType = null;
            this.txtNotes.DefaultValue = null;
            this.txtNotes.Del = false;
            this.txtNotes.DependingRS = null;
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNotes.ForeColor = System.Drawing.Color.Black;
            this.txtNotes.Location = new System.Drawing.Point(839, 146);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(3, 46, 3, 3);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Order = 0;
            this.txtNotes.ParentConn = null;
            this.txtNotes.ParentDA = null;
            this.txtNotes.PK = false;
            this.txtNotes.Search = false;
            this.txtNotes.Size = new System.Drawing.Size(241, 24);
            this.txtNotes.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtNotes.TabIndex = 107;
            this.txtNotes.Upp = false;
            this.txtNotes.Value = "";
            // 
            // txtKm
            // 
            this.txtKm.Add = false;
            this.txtKm.AllowSpace = false;
            this.txtKm.BackColor = System.Drawing.Color.White;
            this.txtKm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKm.Caption = "Km";
            this.txtKm.DBField = null;
            this.txtKm.DBFieldType = null;
            this.txtKm.DefaultValue = null;
            this.txtKm.Del = false;
            this.txtKm.DependingRS = null;
            this.txtKm.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtKm.ForeColor = System.Drawing.Color.Black;
            this.txtKm.Length = 0;
            this.txtKm.Location = new System.Drawing.Point(839, 108);
            this.txtKm.Margin = new System.Windows.Forms.Padding(3, 46, 3, 3);
            this.txtKm.Mask = false;
            this.txtKm.Multiline = true;
            this.txtKm.Name = "txtKm";
            this.txtKm.Order = 0;
            this.txtKm.ParentConn = null;
            this.txtKm.ParentDA = null;
            this.txtKm.PK = false;
            this.txtKm.Precision = 0;
            this.txtKm.Search = false;
            this.txtKm.Size = new System.Drawing.Size(105, 24);
            this.txtKm.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtKm.TabIndex = 105;
            this.txtKm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKm.ThousandsGroup = false;
            this.txtKm.Upp = false;
            this.txtKm.Value = null;
            // 
            // dateEnd
            // 
            this.dateEnd.Add = false;
            this.dateEnd.BackColor = System.Drawing.Color.White;
            this.dateEnd.BorderColor = System.Drawing.Color.White;
            this.dateEnd.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.dateEnd.Caption = "End Date";
            this.dateEnd.CustomFormat = "dd/MM/yyyy H:mm";
            this.dateEnd.DBField = null;
            this.dateEnd.DBFieldType = null;
            this.dateEnd.DefaultValue = null;
            this.dateEnd.Del = false;
            this.dateEnd.DependingRS = null;
            this.dateEnd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateEnd.ForeColor = System.Drawing.Color.Black;
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateEnd.Location = new System.Drawing.Point(688, 226);
            this.dateEnd.Margin = new System.Windows.Forms.Padding(3, 136, 3, 3);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Order = 0;
            this.dateEnd.ParentConn = null;
            this.dateEnd.ParentDA = null;
            this.dateEnd.PK = false;
            this.dateEnd.Search = false;
            this.dateEnd.Size = new System.Drawing.Size(130, 24);
            this.dateEnd.Status = CommonTools.EnumStatus.ADDNEW;
            this.dateEnd.TabIndex = 94;
            this.dateEnd.Upp = false;
            this.dateEnd.Value = new System.DateTime(2015, 12, 15, 15, 19, 34, 571);
            // 
            // dateStart
            // 
            this.dateStart.Add = false;
            this.dateStart.BackColor = System.Drawing.Color.White;
            this.dateStart.BorderColor = System.Drawing.Color.White;
            this.dateStart.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.dateStart.Caption = "Start Date";
            this.dateStart.CustomFormat = "dd/MM/yyyy H:mm";
            this.dateStart.DBField = null;
            this.dateStart.DBFieldType = null;
            this.dateStart.DefaultValue = null;
            this.dateStart.Del = false;
            this.dateStart.DependingRS = null;
            this.dateStart.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateStart.ForeColor = System.Drawing.Color.Black;
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateStart.Location = new System.Drawing.Point(688, 186);
            this.dateStart.Margin = new System.Windows.Forms.Padding(3, 136, 3, 3);
            this.dateStart.Name = "dateStart";
            this.dateStart.Order = 0;
            this.dateStart.ParentConn = null;
            this.dateStart.ParentDA = null;
            this.dateStart.PK = false;
            this.dateStart.Search = false;
            this.dateStart.Size = new System.Drawing.Size(130, 24);
            this.dateStart.Status = CommonTools.EnumStatus.ADDNEW;
            this.dateStart.TabIndex = 92;
            this.dateStart.Upp = false;
            this.dateStart.Value = new System.DateTime(2015, 12, 15, 15, 19, 8, 165);
            // 
            // cboMatter
            // 
            this.cboMatter.Add = false;
            this.cboMatter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboMatter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMatter.BackColor = System.Drawing.Color.White;
            this.cboMatter.Caption = "Matter";
            this.cboMatter.DBField = null;
            this.cboMatter.DBFieldType = null;
            this.cboMatter.DefaultValue = null;
            this.cboMatter.Del = false;
            this.cboMatter.DependingRS = null;
            this.cboMatter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMatter.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboMatter.ForeColor = System.Drawing.Color.Black;
            this.cboMatter.FormattingEnabled = true;
            this.cboMatter.Location = new System.Drawing.Point(688, 145);
            this.cboMatter.Margin = new System.Windows.Forms.Padding(3, 136, 3, 3);
            this.cboMatter.Name = "cboMatter";
            this.cboMatter.Order = 0;
            this.cboMatter.ParentConn = null;
            this.cboMatter.ParentDA = null;
            this.cboMatter.PK = false;
            this.cboMatter.Search = false;
            this.cboMatter.Size = new System.Drawing.Size(130, 24);
            this.cboMatter.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboMatter.TabIndex = 90;
            this.cboMatter.TBDescription = null;
            this.cboMatter.Upp = false;
            this.cboMatter.Value = "";
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
            this.cboService.Location = new System.Drawing.Point(688, 107);
            this.cboService.Margin = new System.Windows.Forms.Padding(3, 136, 3, 3);
            this.cboService.Name = "cboService";
            this.cboService.Order = 0;
            this.cboService.ParentConn = null;
            this.cboService.ParentDA = null;
            this.cboService.PK = false;
            this.cboService.Search = false;
            this.cboService.Size = new System.Drawing.Size(130, 24);
            this.cboService.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboService.TabIndex = 88;
            this.cboService.TBDescription = null;
            this.cboService.Upp = false;
            this.cboService.Value = "";
            // 
            // txtPlaceAffected
            // 
            this.txtPlaceAffected.Add = false;
            this.txtPlaceAffected.BackColor = System.Drawing.Color.White;
            this.txtPlaceAffected.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlaceAffected.Caption = "";
            this.txtPlaceAffected.DBField = null;
            this.txtPlaceAffected.DBFieldType = null;
            this.txtPlaceAffected.DefaultValue = null;
            this.txtPlaceAffected.Del = false;
            this.txtPlaceAffected.DependingRS = null;
            this.txtPlaceAffected.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPlaceAffected.ForeColor = System.Drawing.Color.Black;
            this.txtPlaceAffected.Location = new System.Drawing.Point(561, 226);
            this.txtPlaceAffected.Margin = new System.Windows.Forms.Padding(3, 46, 3, 3);
            this.txtPlaceAffected.Multiline = true;
            this.txtPlaceAffected.Name = "txtPlaceAffected";
            this.txtPlaceAffected.Order = 0;
            this.txtPlaceAffected.ParentConn = null;
            this.txtPlaceAffected.ParentDA = null;
            this.txtPlaceAffected.PK = false;
            this.txtPlaceAffected.Search = false;
            this.txtPlaceAffected.Size = new System.Drawing.Size(100, 24);
            this.txtPlaceAffected.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtPlaceAffected.TabIndex = 84;
            this.txtPlaceAffected.Upp = false;
            this.txtPlaceAffected.Value = "";
            // 
            // cboPlaceAffected
            // 
            this.cboPlaceAffected.Add = false;
            this.cboPlaceAffected.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPlaceAffected.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPlaceAffected.BackColor = System.Drawing.Color.White;
            this.cboPlaceAffected.Caption = "Place Affected";
            this.cboPlaceAffected.DBField = null;
            this.cboPlaceAffected.DBFieldType = null;
            this.cboPlaceAffected.DefaultValue = null;
            this.cboPlaceAffected.Del = false;
            this.cboPlaceAffected.DependingRS = null;
            this.cboPlaceAffected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPlaceAffected.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboPlaceAffected.ForeColor = System.Drawing.Color.Black;
            this.cboPlaceAffected.FormattingEnabled = true;
            this.cboPlaceAffected.Location = new System.Drawing.Point(425, 225);
            this.cboPlaceAffected.Margin = new System.Windows.Forms.Padding(3, 136, 3, 3);
            this.cboPlaceAffected.Name = "cboPlaceAffected";
            this.cboPlaceAffected.Order = 0;
            this.cboPlaceAffected.ParentConn = null;
            this.cboPlaceAffected.ParentDA = null;
            this.cboPlaceAffected.PK = false;
            this.cboPlaceAffected.Search = false;
            this.cboPlaceAffected.Size = new System.Drawing.Size(130, 24);
            this.cboPlaceAffected.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboPlaceAffected.TabIndex = 82;
            this.cboPlaceAffected.TBDescription = null;
            this.cboPlaceAffected.Upp = false;
            this.cboPlaceAffected.Value = "";
            // 
            // txtPlaceAdvise
            // 
            this.txtPlaceAdvise.Add = false;
            this.txtPlaceAdvise.BackColor = System.Drawing.Color.White;
            this.txtPlaceAdvise.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlaceAdvise.Caption = "";
            this.txtPlaceAdvise.DBField = null;
            this.txtPlaceAdvise.DBFieldType = null;
            this.txtPlaceAdvise.DefaultValue = null;
            this.txtPlaceAdvise.Del = false;
            this.txtPlaceAdvise.DependingRS = null;
            this.txtPlaceAdvise.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPlaceAdvise.ForeColor = System.Drawing.Color.Black;
            this.txtPlaceAdvise.Location = new System.Drawing.Point(561, 185);
            this.txtPlaceAdvise.Margin = new System.Windows.Forms.Padding(3, 46, 3, 3);
            this.txtPlaceAdvise.Multiline = true;
            this.txtPlaceAdvise.Name = "txtPlaceAdvise";
            this.txtPlaceAdvise.Order = 0;
            this.txtPlaceAdvise.ParentConn = null;
            this.txtPlaceAdvise.ParentDA = null;
            this.txtPlaceAdvise.PK = false;
            this.txtPlaceAdvise.Search = false;
            this.txtPlaceAdvise.Size = new System.Drawing.Size(100, 24);
            this.txtPlaceAdvise.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtPlaceAdvise.TabIndex = 73;
            this.txtPlaceAdvise.Upp = false;
            this.txtPlaceAdvise.Value = "";
            // 
            // cboPlaceAdvise
            // 
            this.cboPlaceAdvise.Add = false;
            this.cboPlaceAdvise.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPlaceAdvise.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPlaceAdvise.BackColor = System.Drawing.Color.White;
            this.cboPlaceAdvise.Caption = "Place Advise";
            this.cboPlaceAdvise.DBField = null;
            this.cboPlaceAdvise.DBFieldType = null;
            this.cboPlaceAdvise.DefaultValue = null;
            this.cboPlaceAdvise.Del = false;
            this.cboPlaceAdvise.DependingRS = null;
            this.cboPlaceAdvise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPlaceAdvise.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboPlaceAdvise.ForeColor = System.Drawing.Color.Black;
            this.cboPlaceAdvise.FormattingEnabled = true;
            this.cboPlaceAdvise.Location = new System.Drawing.Point(425, 185);
            this.cboPlaceAdvise.Margin = new System.Windows.Forms.Padding(3, 136, 3, 3);
            this.cboPlaceAdvise.Name = "cboPlaceAdvise";
            this.cboPlaceAdvise.Order = 0;
            this.cboPlaceAdvise.ParentConn = null;
            this.cboPlaceAdvise.ParentDA = null;
            this.cboPlaceAdvise.PK = false;
            this.cboPlaceAdvise.Search = false;
            this.cboPlaceAdvise.Size = new System.Drawing.Size(130, 24);
            this.cboPlaceAdvise.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboPlaceAdvise.TabIndex = 71;
            this.cboPlaceAdvise.TBDescription = null;
            this.cboPlaceAdvise.Upp = false;
            this.cboPlaceAdvise.Value = "";
            // 
            // txtPerson
            // 
            this.txtPerson.Add = false;
            this.txtPerson.BackColor = System.Drawing.Color.White;
            this.txtPerson.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPerson.Caption = "Person Name";
            this.txtPerson.DBField = null;
            this.txtPerson.DBFieldType = null;
            this.txtPerson.DefaultValue = null;
            this.txtPerson.Del = false;
            this.txtPerson.DependingRS = null;
            this.txtPerson.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPerson.ForeColor = System.Drawing.Color.Black;
            this.txtPerson.Location = new System.Drawing.Point(425, 146);
            this.txtPerson.Margin = new System.Windows.Forms.Padding(3, 46, 3, 3);
            this.txtPerson.Multiline = true;
            this.txtPerson.Name = "txtPerson";
            this.txtPerson.Order = 0;
            this.txtPerson.ParentConn = null;
            this.txtPerson.ParentDA = null;
            this.txtPerson.PK = false;
            this.txtPerson.Search = false;
            this.txtPerson.Size = new System.Drawing.Size(236, 24);
            this.txtPerson.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtPerson.TabIndex = 65;
            this.txtPerson.Upp = false;
            this.txtPerson.Value = "";
            // 
            // txtIdTarea
            // 
            this.txtIdTarea.Add = false;
            this.txtIdTarea.BackColor = System.Drawing.Color.White;
            this.txtIdTarea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIdTarea.Caption = "Task #";
            this.txtIdTarea.DBField = null;
            this.txtIdTarea.DBFieldType = null;
            this.txtIdTarea.DefaultValue = null;
            this.txtIdTarea.Del = false;
            this.txtIdTarea.DependingRS = null;
            this.txtIdTarea.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtIdTarea.ForeColor = System.Drawing.Color.Black;
            this.txtIdTarea.Location = new System.Drawing.Point(425, 107);
            this.txtIdTarea.Margin = new System.Windows.Forms.Padding(3, 46, 3, 3);
            this.txtIdTarea.Multiline = true;
            this.txtIdTarea.Name = "txtIdTarea";
            this.txtIdTarea.Order = 0;
            this.txtIdTarea.ParentConn = null;
            this.txtIdTarea.ParentDA = null;
            this.txtIdTarea.PK = false;
            this.txtIdTarea.Search = false;
            this.txtIdTarea.Size = new System.Drawing.Size(236, 24);
            this.txtIdTarea.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtIdTarea.TabIndex = 63;
            this.txtIdTarea.Upp = false;
            this.txtIdTarea.Value = "";
            // 
            // fTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 504);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIT);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkFlags);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtKm);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.dateStart);
            this.Controls.Add(this.cboMatter);
            this.Controls.Add(this.cboService);
            this.Controls.Add(this.txtPlaceAffected);
            this.Controls.Add(this.cboPlaceAffected);
            this.Controls.Add(this.txtPlaceAdvise);
            this.Controls.Add(this.cboPlaceAdvise);
            this.Controls.Add(this.txtPerson);
            this.Controls.Add(this.txtIdTarea);
            this.Controls.Add(this.CTLM);
            this.KeyPreview = true;
            this.Name = "fTasks";
            this.Text = "Tasks";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private EspackFormControls.EspackTextBox txtIdTarea;
        private EspackFormControls.EspackTextBox txtPerson;
        private EspackFormControls.EspackComboBox cboPlaceAdvise;
        private EspackFormControls.EspackTextBox txtPlaceAdvise;
        private EspackFormControls.EspackComboBox cboPlaceAffected;
        private EspackFormControls.EspackTextBox txtPlaceAffected;
        private EspackFormControls.EspackComboBox cboService;
        private EspackFormControls.EspackComboBox cboMatter;
        private EspackFormControls.EspackDateTimePicker dateStart;
        private EspackFormControls.EspackDateTimePicker dateEnd;
        private EspackFormControls.NumericTextBox txtKm;
        private EspackFormControls.EspackTextBox txtNotes;
        private EspackFormControls.EspackCheckedListBox chkFlags;
        private System.Windows.Forms.GroupBox groupBox1;
        private EspackFormControls.EspackComboBox cboMatterDef;
        private EspackFormControls.EspackComboBox cboServiceDef;
        private EspackFormControls.EspackComboBox cboPlaceAffectedDef;
        private EspackFormControls.EspackComboBox cboPlaceAdviseDef;
        private System.Windows.Forms.MonthCalendar CalDateDef;
        private EspackFormControls.EspackTextBox txtIT;
        private System.Windows.Forms.Label label1;
    }
}