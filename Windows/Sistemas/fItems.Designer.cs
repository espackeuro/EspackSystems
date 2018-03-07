namespace Sistemas
{
    partial class fItems
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
            this.txtDesType = new EspackFormControls.EspackTextBox();
            this.cboType = new EspackFormControls.EspackComboBox();
            this.txtDescription = new EspackFormControls.EspackTextBox();
            this.txtName = new EspackFormControls.EspackTextBox();
            this.txtCode = new EspackFormControls.EspackTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDesCod3 = new EspackFormControls.EspackTextBox();
            this.cboCOD3 = new EspackFormControls.EspackComboBox();
            this.listCOD3 = new EspackFormControls.EspackCheckedListBox();
            this.cboZone = new EspackFormControls.EspackComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstFlags = new EspackFormControls.EspackCheckedListBox();
            this.txtCM = new EspackFormControls.EspackTextBox();
            this.txtInvoiceDate = new EspackFormControls.EspackDateTimePicker();
            this.txtInvoice = new EspackFormControls.EspackTextBox();
            this.txtSerial = new EspackFormControls.EspackTextBox();
            this.CTLM = new CTLMantenimientoNet.CTLMantenimientoNet();
            this.VS = new VSGrid.CtlVSGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VS)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.txtDesType);
            this.groupBox1.Controls.Add(this.cboType);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtCode);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(16, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(528, 152);
            this.groupBox1.TabIndex = 137;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "What it is";
            // 
            // txtDesType
            // 
            this.txtDesType.Add = false;
            this.txtDesType.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDesType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDesType.Caption = "";
            this.txtDesType.DBField = null;
            this.txtDesType.DBFieldType = null;
            this.txtDesType.DefaultValue = null;
            this.txtDesType.Del = false;
            this.txtDesType.DependingRS = null;
            this.txtDesType.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDesType.ForeColor = System.Drawing.Color.Gray;
            this.txtDesType.Location = new System.Drawing.Point(162, 116);
            this.txtDesType.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDesType.Multiline = true;
            this.txtDesType.Name = "txtDesType";
            this.txtDesType.Order = 0;
            this.txtDesType.ParentConn = null;
            this.txtDesType.ParentDA = null;
            this.txtDesType.PK = false;
            this.txtDesType.ReadOnly = true;
            this.txtDesType.Search = false;
            this.txtDesType.Size = new System.Drawing.Size(343, 24);
            this.txtDesType.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDesType.TabIndex = 5;
            this.txtDesType.TabStop = false;
            this.txtDesType.Upp = false;
            this.txtDesType.Value = "";
            // 
            // cboType
            // 
            this.cboType.Add = false;
            this.cboType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboType.BackColor = System.Drawing.Color.White;
            this.cboType.Caption = "Type";
            this.cboType.DBField = null;
            this.cboType.DBFieldType = null;
            this.cboType.DefaultValue = null;
            this.cboType.Del = false;
            this.cboType.DependingRS = null;
            this.cboType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboType.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboType.ForeColor = System.Drawing.Color.Black;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(23, 116);
            this.cboType.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboType.Name = "cboType";
            this.cboType.Order = 0;
            this.cboType.ParentConn = null;
            this.cboType.ParentDA = null;
            this.cboType.PK = false;
            this.cboType.Search = false;
            this.cboType.Size = new System.Drawing.Size(130, 24);
            this.cboType.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboType.TabIndex = 4;
            this.cboType.TBDescription = null;
            this.cboType.Upp = false;
            this.cboType.Value = "";
            // 
            // txtDescription
            // 
            this.txtDescription.Add = false;
            this.txtDescription.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Caption = "Item Description";
            this.txtDescription.DBField = null;
            this.txtDescription.DBFieldType = null;
            this.txtDescription.DefaultValue = null;
            this.txtDescription.Del = false;
            this.txtDescription.DependingRS = null;
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDescription.ForeColor = System.Drawing.Color.Gray;
            this.txtDescription.Location = new System.Drawing.Point(23, 73);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Order = 0;
            this.txtDescription.ParentConn = null;
            this.txtDescription.ParentDA = null;
            this.txtDescription.PK = false;
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Search = false;
            this.txtDescription.Size = new System.Drawing.Size(482, 24);
            this.txtDescription.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDescription.TabIndex = 3;
            this.txtDescription.Upp = false;
            this.txtDescription.Value = "";
            // 
            // txtName
            // 
            this.txtName.Add = false;
            this.txtName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Caption = "Item Name";
            this.txtName.DBField = null;
            this.txtName.DBFieldType = null;
            this.txtName.DefaultValue = null;
            this.txtName.Del = false;
            this.txtName.DependingRS = null;
            this.txtName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtName.ForeColor = System.Drawing.Color.Gray;
            this.txtName.Location = new System.Drawing.Point(162, 30);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Order = 0;
            this.txtName.ParentConn = null;
            this.txtName.ParentDA = null;
            this.txtName.PK = false;
            this.txtName.ReadOnly = true;
            this.txtName.Search = false;
            this.txtName.Size = new System.Drawing.Size(343, 24);
            this.txtName.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtName.TabIndex = 2;
            this.txtName.Upp = false;
            this.txtName.Value = "";
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
            this.txtCode.Location = new System.Drawing.Point(23, 30);
            this.txtCode.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Order = 0;
            this.txtCode.ParentConn = null;
            this.txtCode.ParentDA = null;
            this.txtCode.PK = false;
            this.txtCode.ReadOnly = true;
            this.txtCode.Search = false;
            this.txtCode.Size = new System.Drawing.Size(130, 24);
            this.txtCode.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtCode.TabIndex = 1;
            this.txtCode.Upp = false;
            this.txtCode.Value = "";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Controls.Add(this.txtDesCod3);
            this.groupBox2.Controls.Add(this.cboCOD3);
            this.groupBox2.Controls.Add(this.listCOD3);
            this.groupBox2.Controls.Add(this.cboZone);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(16, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(528, 228);
            this.groupBox2.TabIndex = 147;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Where is it";
            // 
            // txtDesCod3
            // 
            this.txtDesCod3.Add = false;
            this.txtDesCod3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDesCod3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDesCod3.Caption = "";
            this.txtDesCod3.DBField = null;
            this.txtDesCod3.DBFieldType = null;
            this.txtDesCod3.DefaultValue = null;
            this.txtDesCod3.Del = false;
            this.txtDesCod3.DependingRS = null;
            this.txtDesCod3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDesCod3.ForeColor = System.Drawing.Color.Gray;
            this.txtDesCod3.Location = new System.Drawing.Point(167, 37);
            this.txtDesCod3.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDesCod3.Multiline = true;
            this.txtDesCod3.Name = "txtDesCod3";
            this.txtDesCod3.Order = 0;
            this.txtDesCod3.ParentConn = null;
            this.txtDesCod3.ParentDA = null;
            this.txtDesCod3.PK = false;
            this.txtDesCod3.ReadOnly = true;
            this.txtDesCod3.Search = false;
            this.txtDesCod3.Size = new System.Drawing.Size(338, 24);
            this.txtDesCod3.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDesCod3.TabIndex = 116;
            this.txtDesCod3.TabStop = false;
            this.txtDesCod3.Upp = false;
            this.txtDesCod3.Value = "";
            // 
            // cboCOD3
            // 
            this.cboCOD3.Add = false;
            this.cboCOD3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCOD3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCOD3.BackColor = System.Drawing.Color.White;
            this.cboCOD3.Caption = "Main COD3";
            this.cboCOD3.DBField = null;
            this.cboCOD3.DBFieldType = null;
            this.cboCOD3.DefaultValue = null;
            this.cboCOD3.Del = false;
            this.cboCOD3.DependingRS = null;
            this.cboCOD3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCOD3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboCOD3.ForeColor = System.Drawing.Color.Black;
            this.cboCOD3.FormattingEnabled = true;
            this.cboCOD3.Location = new System.Drawing.Point(23, 37);
            this.cboCOD3.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboCOD3.Name = "cboCOD3";
            this.cboCOD3.Order = 0;
            this.cboCOD3.ParentConn = null;
            this.cboCOD3.ParentDA = null;
            this.cboCOD3.PK = false;
            this.cboCOD3.Search = true;
            this.cboCOD3.Size = new System.Drawing.Size(138, 24);
            this.cboCOD3.Status = CommonTools.EnumStatus.SEARCH;
            this.cboCOD3.TabIndex = 6;
            this.cboCOD3.TBDescription = null;
            this.cboCOD3.Upp = true;
            this.cboCOD3.Value = "";
            // 
            // listCOD3
            // 
            this.listCOD3.Add = false;
            this.listCOD3.BackColor = System.Drawing.Color.White;
            this.listCOD3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listCOD3.Caption = "COD3";
            this.listCOD3.CheckOnClick = true;
            this.listCOD3.ColumnWidth = 165;
            this.listCOD3.DBField = null;
            this.listCOD3.DBFieldType = null;
            this.listCOD3.DefaultValue = null;
            this.listCOD3.Del = false;
            this.listCOD3.DependingRS = null;
            this.listCOD3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.listCOD3.ForeColor = System.Drawing.Color.Black;
            this.listCOD3.FormattingEnabled = true;
            this.listCOD3.Location = new System.Drawing.Point(23, 80);
            this.listCOD3.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.listCOD3.MultiColumn = true;
            this.listCOD3.Name = "listCOD3";
            this.listCOD3.Order = 0;
            this.listCOD3.ParentConn = null;
            this.listCOD3.ParentDA = null;
            this.listCOD3.PK = false;
            this.listCOD3.Search = false;
            this.listCOD3.Size = new System.Drawing.Size(482, 95);
            this.listCOD3.Status = CommonTools.EnumStatus.ADDNEW;
            this.listCOD3.TabIndex = 7;
            this.listCOD3.Upp = false;
            this.listCOD3.Value = "";
            // 
            // cboZone
            // 
            this.cboZone.Add = false;
            this.cboZone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboZone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboZone.BackColor = System.Drawing.Color.White;
            this.cboZone.Caption = "Zone";
            this.cboZone.DBField = null;
            this.cboZone.DBFieldType = null;
            this.cboZone.DefaultValue = null;
            this.cboZone.Del = false;
            this.cboZone.DependingRS = null;
            this.cboZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboZone.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboZone.ForeColor = System.Drawing.Color.Black;
            this.cboZone.FormattingEnabled = true;
            this.cboZone.Location = new System.Drawing.Point(23, 194);
            this.cboZone.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboZone.Name = "cboZone";
            this.cboZone.Order = 0;
            this.cboZone.ParentConn = null;
            this.cboZone.ParentDA = null;
            this.cboZone.PK = false;
            this.cboZone.Search = false;
            this.cboZone.Size = new System.Drawing.Size(482, 24);
            this.cboZone.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboZone.TabIndex = 8;
            this.cboZone.TBDescription = null;
            this.cboZone.Upp = false;
            this.cboZone.Value = "";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox3.Controls.Add(this.lstFlags);
            this.groupBox3.Controls.Add(this.txtCM);
            this.groupBox3.Controls.Add(this.txtInvoiceDate);
            this.groupBox3.Controls.Add(this.txtInvoice);
            this.groupBox3.Controls.Add(this.txtSerial);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(16, 440);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(528, 179);
            this.groupBox3.TabIndex = 153;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bureaucracy";
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
            this.lstFlags.Location = new System.Drawing.Point(23, 118);
            this.lstFlags.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstFlags.Name = "lstFlags";
            this.lstFlags.Order = 0;
            this.lstFlags.ParentConn = null;
            this.lstFlags.ParentDA = null;
            this.lstFlags.PK = false;
            this.lstFlags.Search = false;
            this.lstFlags.Size = new System.Drawing.Size(482, 38);
            this.lstFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstFlags.TabIndex = 13;
            this.lstFlags.Upp = false;
            this.lstFlags.Value = "";
            // 
            // txtCM
            // 
            this.txtCM.Add = false;
            this.txtCM.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtCM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCM.Caption = "CM";
            this.txtCM.DBField = null;
            this.txtCM.DBFieldType = null;
            this.txtCM.DefaultValue = null;
            this.txtCM.Del = false;
            this.txtCM.DependingRS = null;
            this.txtCM.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCM.ForeColor = System.Drawing.Color.Gray;
            this.txtCM.Location = new System.Drawing.Point(234, 32);
            this.txtCM.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.txtCM.Multiline = true;
            this.txtCM.Name = "txtCM";
            this.txtCM.Order = 0;
            this.txtCM.ParentConn = null;
            this.txtCM.ParentDA = null;
            this.txtCM.PK = false;
            this.txtCM.ReadOnly = true;
            this.txtCM.Search = false;
            this.txtCM.Size = new System.Drawing.Size(271, 24);
            this.txtCM.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtCM.TabIndex = 10;
            this.txtCM.Upp = false;
            this.txtCM.Value = "";
            // 
            // txtInvoiceDate
            // 
            this.txtInvoiceDate.Add = false;
            this.txtInvoiceDate.BackColor = System.Drawing.Color.White;
            this.txtInvoiceDate.BorderColor = System.Drawing.Color.White;
            this.txtInvoiceDate.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.txtInvoiceDate.Caption = "Invoice Date";
            this.txtInvoiceDate.CustomFormat = "dd/MM/yyyy H:mm";
            this.txtInvoiceDate.DBField = null;
            this.txtInvoiceDate.DBFieldType = null;
            this.txtInvoiceDate.DefaultValue = null;
            this.txtInvoiceDate.Del = false;
            this.txtInvoiceDate.DependingRS = null;
            this.txtInvoiceDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtInvoiceDate.ForeColor = System.Drawing.Color.Black;
            this.txtInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtInvoiceDate.Location = new System.Drawing.Point(234, 75);
            this.txtInvoiceDate.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtInvoiceDate.Name = "txtInvoiceDate";
            this.txtInvoiceDate.Nullable = false;
            this.txtInvoiceDate.Order = 0;
            this.txtInvoiceDate.ParentConn = null;
            this.txtInvoiceDate.ParentDA = null;
            this.txtInvoiceDate.PK = false;
            this.txtInvoiceDate.Search = false;
            this.txtInvoiceDate.Size = new System.Drawing.Size(271, 24);
            this.txtInvoiceDate.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtInvoiceDate.TabIndex = 12;
            this.txtInvoiceDate.Upp = false;
            this.txtInvoiceDate.Value = new System.DateTime(2015, 10, 26, 13, 15, 22, 168);
            // 
            // txtInvoice
            // 
            this.txtInvoice.Add = false;
            this.txtInvoice.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtInvoice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInvoice.Caption = "Invoice";
            this.txtInvoice.DBField = null;
            this.txtInvoice.DBFieldType = null;
            this.txtInvoice.DefaultValue = null;
            this.txtInvoice.Del = false;
            this.txtInvoice.DependingRS = null;
            this.txtInvoice.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtInvoice.ForeColor = System.Drawing.Color.Gray;
            this.txtInvoice.Location = new System.Drawing.Point(23, 74);
            this.txtInvoice.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.txtInvoice.Multiline = true;
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Order = 0;
            this.txtInvoice.ParentConn = null;
            this.txtInvoice.ParentDA = null;
            this.txtInvoice.PK = false;
            this.txtInvoice.ReadOnly = true;
            this.txtInvoice.Search = false;
            this.txtInvoice.Size = new System.Drawing.Size(170, 24);
            this.txtInvoice.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtInvoice.TabIndex = 11;
            this.txtInvoice.Upp = false;
            this.txtInvoice.Value = "";
            // 
            // txtSerial
            // 
            this.txtSerial.Add = false;
            this.txtSerial.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtSerial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSerial.Caption = "Serial";
            this.txtSerial.DBField = null;
            this.txtSerial.DBFieldType = null;
            this.txtSerial.DefaultValue = null;
            this.txtSerial.Del = false;
            this.txtSerial.DependingRS = null;
            this.txtSerial.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSerial.ForeColor = System.Drawing.Color.Gray;
            this.txtSerial.Location = new System.Drawing.Point(23, 32);
            this.txtSerial.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.txtSerial.Multiline = true;
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Order = 0;
            this.txtSerial.ParentConn = null;
            this.txtSerial.ParentDA = null;
            this.txtSerial.PK = false;
            this.txtSerial.ReadOnly = true;
            this.txtSerial.Search = false;
            this.txtSerial.Size = new System.Drawing.Size(170, 24);
            this.txtSerial.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtSerial.TabIndex = 9;
            this.txtSerial.Upp = false;
            this.txtSerial.Value = "";
            // 
            // CTLM
            // 
            this.CTLM.Clear = false;
            this.CTLM.Conn = null;
            this.CTLM.DBTable = null;
            this.CTLM.Dock = System.Windows.Forms.DockStyle.None;
            this.CTLM.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.CTLM.Location = new System.Drawing.Point(16, 13);
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
            // VS
            // 
            this.VS.Add = false;
            this.VS.AllowDelete = false;
            this.VS.AllowInsert = false;
            this.VS.AllowUpdate = false;
            this.VS.AllowUserToAddRows = false;
            this.VS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.VS.Caption = "Details";
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
            this.VS.Location = new System.Drawing.Point(606, 45);
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
            this.VS.Size = new System.Drawing.Size(608, 594);
            this.VS.SQL = null;
            this.VS.sSPAdd = "";
            this.VS.sSPDel = "";
            this.VS.sSPUpp = "";
            this.VS.Status = CommonTools.EnumStatus.SEARCH;
            this.VS.TabIndex = 50;
            this.VS.Upp = false;
            this.VS.Value = null;
            // 
            // fItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.VS);
            this.Controls.Add(this.CTLM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "fItems";
            this.ShowIcon = false;
            this.Text = "Items";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private VSGrid.CtlVSGrid VS;
        private System.Windows.Forms.GroupBox groupBox1;
        private EspackFormControls.EspackTextBox txtDesType;
        private EspackFormControls.EspackComboBox cboType;
        private EspackFormControls.EspackTextBox txtDescription;
        private EspackFormControls.EspackTextBox txtName;
        private EspackFormControls.EspackTextBox txtCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private EspackFormControls.EspackTextBox txtDesCod3;
        private EspackFormControls.EspackComboBox cboCOD3;
        private EspackFormControls.EspackCheckedListBox listCOD3;
        private EspackFormControls.EspackComboBox cboZone;
        private System.Windows.Forms.GroupBox groupBox3;
        private EspackFormControls.EspackCheckedListBox lstFlags;
        private EspackFormControls.EspackTextBox txtCM;
        private EspackFormControls.EspackDateTimePicker txtInvoiceDate;
        private EspackFormControls.EspackTextBox txtInvoice;
        private EspackFormControls.EspackTextBox txtSerial;
    }
}