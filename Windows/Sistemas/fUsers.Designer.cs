namespace Sistemas
{
    partial class fUsers
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstEmailAliases = new EspackFormControls.EspackCheckedListBox();
            this.txtEmail = new EspackFormControls.EspackTextBox();
            this.txtPIN = new EspackFormControls.NumericTextBox();
            this.txtQuota = new EspackFormControls.NumericTextBox();
            this.cboDomain = new EspackFormControls.EspackComboBox();
            this.txtPasswordEXP = new EspackFormControls.EspackDateTimePicker();
            this.lstFlags = new EspackFormControls.EspackCheckedListBox();
            this.txtPWD = new EspackFormControls.EspackTextBox();
            this.txtDesCod3 = new EspackFormControls.EspackTextBox();
            this.cboCOD3 = new EspackFormControls.EspackComboBox();
            this.listCOD3 = new EspackFormControls.EspackCheckedListBox();
            this.cboZone = new EspackFormControls.EspackComboBox();
            this.txtUserNumber = new EspackFormControls.NumericTextBox();
            this.txtSurname2 = new EspackFormControls.EspackTextBox();
            this.txtSurname1 = new EspackFormControls.EspackTextBox();
            this.txtName = new EspackFormControls.EspackTextBox();
            this.txtUserCode = new EspackFormControls.EspackTextBox();
            this.CTLM = new CTLMantenimientoNet.CTLMantenimientoNet();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.txtUserNumber);
            this.groupBox1.Controls.Add(this.txtSurname2);
            this.groupBox1.Controls.Add(this.txtSurname1);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtUserCode);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(16, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(528, 110);
            this.groupBox1.TabIndex = 137;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Who is";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Controls.Add(this.txtDesCod3);
            this.groupBox2.Controls.Add(this.cboCOD3);
            this.groupBox2.Controls.Add(this.listCOD3);
            this.groupBox2.Controls.Add(this.cboZone);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(16, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(528, 228);
            this.groupBox2.TabIndex = 147;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Where";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox3.Controls.Add(this.txtEmail);
            this.groupBox3.Controls.Add(this.txtPIN);
            this.groupBox3.Controls.Add(this.txtQuota);
            this.groupBox3.Controls.Add(this.cboDomain);
            this.groupBox3.Controls.Add(this.txtPasswordEXP);
            this.groupBox3.Controls.Add(this.lstFlags);
            this.groupBox3.Controls.Add(this.txtPWD);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(16, 395);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(528, 244);
            this.groupBox3.TabIndex = 153;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Systems data";
            // 
            // lstEmailAliases
            // 
            this.lstEmailAliases.Add = false;
            this.lstEmailAliases.BackColor = System.Drawing.Color.White;
            this.lstEmailAliases.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstEmailAliases.Caption = "";
            this.lstEmailAliases.CheckOnClick = true;
            this.lstEmailAliases.DBField = null;
            this.lstEmailAliases.DBFieldType = null;
            this.lstEmailAliases.DefaultValue = null;
            this.lstEmailAliases.Del = false;
            this.lstEmailAliases.DependingRS = null;
            this.lstEmailAliases.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstEmailAliases.ForeColor = System.Drawing.Color.Black;
            this.lstEmailAliases.FormattingEnabled = true;
            this.lstEmailAliases.Location = new System.Drawing.Point(550, 48);
            this.lstEmailAliases.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstEmailAliases.Name = "lstEmailAliases";
            this.lstEmailAliases.Order = 0;
            this.lstEmailAliases.ParentConn = null;
            this.lstEmailAliases.ParentDA = null;
            this.lstEmailAliases.PK = false;
            this.lstEmailAliases.Search = false;
            this.lstEmailAliases.Size = new System.Drawing.Size(292, 589);
            this.lstEmailAliases.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstEmailAliases.TabIndex = 15;
            this.lstEmailAliases.Upp = false;
            this.lstEmailAliases.Value = "";
            // 
            // txtEmail
            // 
            this.txtEmail.Add = false;
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Caption = "Email Address";
            this.txtEmail.DBField = null;
            this.txtEmail.DBFieldType = null;
            this.txtEmail.DefaultValue = null;
            this.txtEmail.Del = false;
            this.txtEmail.DependingRS = null;
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEmail.ForeColor = System.Drawing.Color.Black;
            this.txtEmail.Location = new System.Drawing.Point(206, 72);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtEmail.Multiline = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Order = 0;
            this.txtEmail.ParentConn = null;
            this.txtEmail.ParentDA = null;
            this.txtEmail.PK = false;
            this.txtEmail.Search = false;
            this.txtEmail.Size = new System.Drawing.Size(303, 24);
            this.txtEmail.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtEmail.TabIndex = 12;
            this.txtEmail.Upp = false;
            this.txtEmail.Value = "";
            // 
            // txtPIN
            // 
            this.txtPIN.Add = false;
            this.txtPIN.AllowSpace = false;
            this.txtPIN.BackColor = System.Drawing.Color.White;
            this.txtPIN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPIN.Caption = "PIN";
            this.txtPIN.DBField = null;
            this.txtPIN.DBFieldType = null;
            this.txtPIN.DefaultValue = null;
            this.txtPIN.Del = false;
            this.txtPIN.DependingRS = null;
            this.txtPIN.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPIN.ForeColor = System.Drawing.Color.Black;
            this.txtPIN.Length = 0;
            this.txtPIN.Location = new System.Drawing.Point(327, 32);
            this.txtPIN.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtPIN.Mask = false;
            this.txtPIN.Multiline = true;
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.Order = 0;
            this.txtPIN.ParentConn = null;
            this.txtPIN.ParentDA = null;
            this.txtPIN.PK = false;
            this.txtPIN.Precision = 0;
            this.txtPIN.Search = false;
            this.txtPIN.Size = new System.Drawing.Size(51, 24);
            this.txtPIN.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtPIN.TabIndex = 10;
            this.txtPIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPIN.ThousandsGroup = false;
            this.txtPIN.Upp = false;
            this.txtPIN.Value = null;
            // 
            // txtQuota
            // 
            this.txtQuota.Add = false;
            this.txtQuota.AllowSpace = false;
            this.txtQuota.BackColor = System.Drawing.Color.White;
            this.txtQuota.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuota.Caption = "Email Quota";
            this.txtQuota.DBField = null;
            this.txtQuota.DBFieldType = null;
            this.txtQuota.DefaultValue = null;
            this.txtQuota.Del = false;
            this.txtQuota.DependingRS = null;
            this.txtQuota.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtQuota.ForeColor = System.Drawing.Color.Black;
            this.txtQuota.Length = 0;
            this.txtQuota.Location = new System.Drawing.Point(384, 32);
            this.txtQuota.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtQuota.Mask = false;
            this.txtQuota.Multiline = true;
            this.txtQuota.Name = "txtQuota";
            this.txtQuota.Order = 0;
            this.txtQuota.ParentConn = null;
            this.txtQuota.ParentDA = null;
            this.txtQuota.PK = false;
            this.txtQuota.Precision = 0;
            this.txtQuota.Search = false;
            this.txtQuota.Size = new System.Drawing.Size(125, 24);
            this.txtQuota.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtQuota.TabIndex = 13;
            this.txtQuota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuota.ThousandsGroup = false;
            this.txtQuota.Upp = false;
            this.txtQuota.Value = null;
            // 
            // cboDomain
            // 
            this.cboDomain.Add = false;
            this.cboDomain.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDomain.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDomain.BackColor = System.Drawing.Color.White;
            this.cboDomain.Caption = "Domain";
            this.cboDomain.DBField = null;
            this.cboDomain.DBFieldType = null;
            this.cboDomain.DefaultValue = null;
            this.cboDomain.Del = false;
            this.cboDomain.DependingRS = null;
            this.cboDomain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDomain.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboDomain.ForeColor = System.Drawing.Color.Black;
            this.cboDomain.FormattingEnabled = true;
            this.cboDomain.Location = new System.Drawing.Point(23, 72);
            this.cboDomain.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboDomain.Name = "cboDomain";
            this.cboDomain.Order = 0;
            this.cboDomain.ParentConn = null;
            this.cboDomain.ParentDA = null;
            this.cboDomain.PK = false;
            this.cboDomain.Search = false;
            this.cboDomain.Size = new System.Drawing.Size(170, 24);
            this.cboDomain.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboDomain.TabIndex = 11;
            this.cboDomain.TBDescription = null;
            this.cboDomain.Upp = false;
            this.cboDomain.Value = "";
            // 
            // txtPasswordEXP
            // 
            this.txtPasswordEXP.Add = false;
            this.txtPasswordEXP.BackColor = System.Drawing.Color.White;
            this.txtPasswordEXP.BorderColor = System.Drawing.Color.White;
            this.txtPasswordEXP.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.txtPasswordEXP.Caption = "Password Expiration";
            this.txtPasswordEXP.CustomFormat = "";
            this.txtPasswordEXP.DBField = null;
            this.txtPasswordEXP.DBFieldType = null;
            this.txtPasswordEXP.DefaultValue = null;
            this.txtPasswordEXP.Del = false;
            this.txtPasswordEXP.DependingRS = null;
            this.txtPasswordEXP.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPasswordEXP.ForeColor = System.Drawing.Color.Black;
            this.txtPasswordEXP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtPasswordEXP.Location = new System.Drawing.Point(167, 32);
            this.txtPasswordEXP.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtPasswordEXP.Name = "txtPasswordEXP";
            this.txtPasswordEXP.Nullable = true;
            this.txtPasswordEXP.Order = 0;
            this.txtPasswordEXP.ParentConn = null;
            this.txtPasswordEXP.ParentDA = null;
            this.txtPasswordEXP.PK = false;
            this.txtPasswordEXP.Search = false;
            this.txtPasswordEXP.ShowCheckBox = true;
            this.txtPasswordEXP.Size = new System.Drawing.Size(154, 24);
            this.txtPasswordEXP.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtPasswordEXP.TabIndex = 19;
            this.txtPasswordEXP.Upp = false;
            this.txtPasswordEXP.Value = null;
            // 
            // lstFlags
            // 
            this.lstFlags.Add = false;
            this.lstFlags.BackColor = System.Drawing.Color.White;
            this.lstFlags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstFlags.Caption = "Flags";
            this.lstFlags.CheckOnClick = true;
            this.lstFlags.ColumnWidth = 150;
            this.lstFlags.DBField = null;
            this.lstFlags.DBFieldType = null;
            this.lstFlags.DefaultValue = null;
            this.lstFlags.Del = false;
            this.lstFlags.DependingRS = null;
            this.lstFlags.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstFlags.ForeColor = System.Drawing.Color.Black;
            this.lstFlags.FormattingEnabled = true;
            this.lstFlags.Location = new System.Drawing.Point(23, 120);
            this.lstFlags.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstFlags.MultiColumn = true;
            this.lstFlags.Name = "lstFlags";
            this.lstFlags.Order = 0;
            this.lstFlags.ParentConn = null;
            this.lstFlags.ParentDA = null;
            this.lstFlags.PK = false;
            this.lstFlags.Search = false;
            this.lstFlags.Size = new System.Drawing.Size(482, 114);
            this.lstFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstFlags.TabIndex = 14;
            this.lstFlags.Upp = false;
            this.lstFlags.Value = "";
            // 
            // txtPWD
            // 
            this.txtPWD.Add = false;
            this.txtPWD.BackColor = System.Drawing.Color.White;
            this.txtPWD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPWD.Caption = "Password";
            this.txtPWD.DBField = null;
            this.txtPWD.DBFieldType = null;
            this.txtPWD.DefaultValue = null;
            this.txtPWD.Del = false;
            this.txtPWD.DependingRS = null;
            this.txtPWD.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPWD.ForeColor = System.Drawing.Color.Black;
            this.txtPWD.Location = new System.Drawing.Point(23, 32);
            this.txtPWD.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.txtPWD.Multiline = true;
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.Order = 0;
            this.txtPWD.ParentConn = null;
            this.txtPWD.ParentDA = null;
            this.txtPWD.PK = false;
            this.txtPWD.Search = false;
            this.txtPWD.Size = new System.Drawing.Size(130, 24);
            this.txtPWD.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtPWD.TabIndex = 9;
            this.txtPWD.Upp = false;
            this.txtPWD.Value = "";
            // 
            // txtDesCod3
            // 
            this.txtDesCod3.Add = false;
            this.txtDesCod3.BackColor = System.Drawing.Color.White;
            this.txtDesCod3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDesCod3.Caption = "";
            this.txtDesCod3.DBField = null;
            this.txtDesCod3.DBFieldType = null;
            this.txtDesCod3.DefaultValue = null;
            this.txtDesCod3.Del = false;
            this.txtDesCod3.DependingRS = null;
            this.txtDesCod3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDesCod3.ForeColor = System.Drawing.Color.Black;
            this.txtDesCod3.Location = new System.Drawing.Point(167, 37);
            this.txtDesCod3.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDesCod3.Multiline = true;
            this.txtDesCod3.Name = "txtDesCod3";
            this.txtDesCod3.Order = 0;
            this.txtDesCod3.ParentConn = null;
            this.txtDesCod3.ParentDA = null;
            this.txtDesCod3.PK = false;
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
            // txtUserNumber
            // 
            this.txtUserNumber.Add = false;
            this.txtUserNumber.AllowSpace = false;
            this.txtUserNumber.BackColor = System.Drawing.Color.White;
            this.txtUserNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserNumber.Caption = "UserNumber";
            this.txtUserNumber.DBField = null;
            this.txtUserNumber.DBFieldType = null;
            this.txtUserNumber.DefaultValue = null;
            this.txtUserNumber.Del = false;
            this.txtUserNumber.DependingRS = null;
            this.txtUserNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtUserNumber.ForeColor = System.Drawing.Color.Black;
            this.txtUserNumber.Length = 0;
            this.txtUserNumber.Location = new System.Drawing.Point(405, 29);
            this.txtUserNumber.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtUserNumber.Mask = false;
            this.txtUserNumber.Multiline = true;
            this.txtUserNumber.Name = "txtUserNumber";
            this.txtUserNumber.Order = 0;
            this.txtUserNumber.ParentConn = null;
            this.txtUserNumber.ParentDA = null;
            this.txtUserNumber.PK = false;
            this.txtUserNumber.Precision = 0;
            this.txtUserNumber.Search = false;
            this.txtUserNumber.Size = new System.Drawing.Size(100, 24);
            this.txtUserNumber.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtUserNumber.TabIndex = 2;
            this.txtUserNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUserNumber.ThousandsGroup = false;
            this.txtUserNumber.Upp = false;
            this.txtUserNumber.Value = null;
            // 
            // txtSurname2
            // 
            this.txtSurname2.Add = false;
            this.txtSurname2.BackColor = System.Drawing.Color.White;
            this.txtSurname2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSurname2.Caption = "Surname 2";
            this.txtSurname2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSurname2.DBField = null;
            this.txtSurname2.DBFieldType = null;
            this.txtSurname2.DefaultValue = null;
            this.txtSurname2.Del = false;
            this.txtSurname2.DependingRS = null;
            this.txtSurname2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSurname2.ForeColor = System.Drawing.Color.Black;
            this.txtSurname2.Location = new System.Drawing.Point(351, 74);
            this.txtSurname2.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtSurname2.Multiline = true;
            this.txtSurname2.Name = "txtSurname2";
            this.txtSurname2.Order = 0;
            this.txtSurname2.ParentConn = null;
            this.txtSurname2.ParentDA = null;
            this.txtSurname2.PK = false;
            this.txtSurname2.Search = false;
            this.txtSurname2.Size = new System.Drawing.Size(157, 24);
            this.txtSurname2.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtSurname2.TabIndex = 5;
            this.txtSurname2.Upp = false;
            this.txtSurname2.Value = "";
            // 
            // txtSurname1
            // 
            this.txtSurname1.Add = false;
            this.txtSurname1.BackColor = System.Drawing.Color.White;
            this.txtSurname1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSurname1.Caption = "Surname 1";
            this.txtSurname1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSurname1.DBField = null;
            this.txtSurname1.DBFieldType = null;
            this.txtSurname1.DefaultValue = null;
            this.txtSurname1.Del = false;
            this.txtSurname1.DependingRS = null;
            this.txtSurname1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSurname1.ForeColor = System.Drawing.Color.Black;
            this.txtSurname1.Location = new System.Drawing.Point(188, 74);
            this.txtSurname1.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtSurname1.Multiline = true;
            this.txtSurname1.Name = "txtSurname1";
            this.txtSurname1.Order = 0;
            this.txtSurname1.ParentConn = null;
            this.txtSurname1.ParentDA = null;
            this.txtSurname1.PK = false;
            this.txtSurname1.Search = false;
            this.txtSurname1.Size = new System.Drawing.Size(157, 24);
            this.txtSurname1.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtSurname1.TabIndex = 4;
            this.txtSurname1.Upp = false;
            this.txtSurname1.Value = "";
            // 
            // txtName
            // 
            this.txtName.Add = false;
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Caption = "Name";
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.DBField = null;
            this.txtName.DBFieldType = null;
            this.txtName.DefaultValue = null;
            this.txtName.Del = false;
            this.txtName.DependingRS = null;
            this.txtName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(23, 74);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Order = 0;
            this.txtName.ParentConn = null;
            this.txtName.ParentDA = null;
            this.txtName.PK = false;
            this.txtName.Search = false;
            this.txtName.Size = new System.Drawing.Size(157, 24);
            this.txtName.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtName.TabIndex = 3;
            this.txtName.Upp = false;
            this.txtName.Value = "";
            // 
            // txtUserCode
            // 
            this.txtUserCode.Add = false;
            this.txtUserCode.BackColor = System.Drawing.Color.White;
            this.txtUserCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserCode.Caption = "UserCode";
            this.txtUserCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtUserCode.DBField = null;
            this.txtUserCode.DBFieldType = null;
            this.txtUserCode.DefaultValue = null;
            this.txtUserCode.Del = false;
            this.txtUserCode.DependingRS = null;
            this.txtUserCode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtUserCode.ForeColor = System.Drawing.Color.Black;
            this.txtUserCode.Location = new System.Drawing.Point(23, 29);
            this.txtUserCode.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtUserCode.Multiline = true;
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.Order = 0;
            this.txtUserCode.ParentConn = null;
            this.txtUserCode.ParentDA = null;
            this.txtUserCode.PK = false;
            this.txtUserCode.Search = false;
            this.txtUserCode.Size = new System.Drawing.Size(130, 24);
            this.txtUserCode.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtUserCode.TabIndex = 1;
            this.txtUserCode.Upp = false;
            this.txtUserCode.Value = "";
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
            this.CTLM.RSPosition = -1;
            this.CTLM.Size = new System.Drawing.Size(290, 29);
            this.CTLM.sSPAdd = "";
            this.CTLM.sSPDel = "";
            this.CTLM.sSPUpp = "";
            this.CTLM.Status = CommonTools.EnumStatus.SEARCH;
            this.CTLM.StatusBarProgress = null;
            this.CTLM.TabIndex = 0;
            this.CTLM.Text = "ctlMantenimientoNet1";
            // 
            // fUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.lstEmailAliases);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CTLM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "fUsers";
            this.Text = "Users";
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
        private EspackFormControls.EspackTextBox txtName;
        private EspackFormControls.EspackTextBox txtUserCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private EspackFormControls.EspackTextBox txtDesCod3;
        private EspackFormControls.EspackComboBox cboCOD3;
        private EspackFormControls.EspackCheckedListBox listCOD3;
        private EspackFormControls.EspackComboBox cboZone;
        private System.Windows.Forms.GroupBox groupBox3;
        private EspackFormControls.EspackCheckedListBox lstFlags;
        private EspackFormControls.EspackTextBox txtPWD;
        private EspackFormControls.EspackTextBox txtSurname2;
        private EspackFormControls.EspackTextBox txtSurname1;
        private EspackFormControls.EspackDateTimePicker txtPasswordEXP;
        private EspackFormControls.NumericTextBox txtUserNumber;
        private EspackFormControls.NumericTextBox txtPIN;
        private EspackFormControls.NumericTextBox txtQuota;
        private EspackFormControls.EspackComboBox cboDomain;
        private EspackFormControls.EspackTextBox txtEmail;
        private EspackFormControls.EspackCheckedListBox lstEmailAliases;
    }
}