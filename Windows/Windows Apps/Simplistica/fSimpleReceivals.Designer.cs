namespace Simplistica
{
    partial class fSimpleReceivals
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fSimpleReceivals));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnReceived = new System.Windows.Forms.ToolStripButton();
            this.btnLabelCMs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lstFlags = new EspackFormControls.EspackCheckedListBox();
            this.VS = new VSGrid.CtlVSGrid();
            this.txtDesServicio = new EspackFormControls.EspackTextBox();
            this.cboServicio = new EspackFormControls.EspackComboBox();
            this.txtNotes = new EspackFormControls.EspackTextBox();
            this.txtSuppDoc = new EspackFormControls.EspackTextBox();
            this.txtFecha = new EspackFormControls.EspackDateTimePicker();
            this.txtEntrada = new EspackFormControls.EspackTextBox();
            this.CTLM = new CTLMantenimientoNet.CTLMantenimientoNet();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VS)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnReceived,
            this.btnLabelCMs,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(393, 13);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(142, 25);
            this.toolStrip1.TabIndex = 30;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnReceived
            // 
            this.btnReceived.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnReceived.Image = ((System.Drawing.Image)(resources.GetObject("btnReceived.Image")));
            this.btnReceived.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReceived.Name = "btnReceived";
            this.btnReceived.Size = new System.Drawing.Size(58, 22);
            this.btnReceived.Text = "Received";
            this.btnReceived.Click += new System.EventHandler(this.btnReceived_Click);
            // 
            // btnLabelCMs
            // 
            this.btnLabelCMs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLabelCMs.Image = ((System.Drawing.Image)(resources.GetObject("btnLabelCMs.Image")));
            this.btnLabelCMs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLabelCMs.Name = "btnLabelCMs";
            this.btnLabelCMs.Size = new System.Drawing.Size(66, 22);
            this.btnLabelCMs.Text = "Label CMs";
            this.btnLabelCMs.Click += new System.EventHandler(this.btnLabelCMs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            this.lstFlags.Location = new System.Drawing.Point(11, 241);
            this.lstFlags.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstFlags.Name = "lstFlags";
            this.lstFlags.Order = 0;
            this.lstFlags.ParentConn = null;
            this.lstFlags.ParentDA = null;
            this.lstFlags.PK = false;
            this.lstFlags.Search = false;
            this.lstFlags.Size = new System.Drawing.Size(540, 57);
            this.lstFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstFlags.TabIndex = 31;
            this.lstFlags.Upp = false;
            this.lstFlags.Value = "";
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
            this.VS.Location = new System.Drawing.Point(15, 304);
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
            this.VS.Size = new System.Drawing.Size(536, 308);
            this.VS.SQL = null;
            this.VS.sSPAdd = "";
            this.VS.sSPDel = "";
            this.VS.sSPUpp = "";
            this.VS.Status = CommonTools.EnumStatus.SEARCH;
            this.VS.TabIndex = 23;
            this.VS.Upp = false;
            this.VS.Value = null;
            // 
            // txtDesServicio
            // 
            this.txtDesServicio.Add = false;
            this.txtDesServicio.BackColor = System.Drawing.Color.White;
            this.txtDesServicio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDesServicio.Caption = "";
            this.txtDesServicio.DBField = null;
            this.txtDesServicio.DBFieldType = null;
            this.txtDesServicio.DefaultValue = null;
            this.txtDesServicio.Del = false;
            this.txtDesServicio.DependingRS = null;
            this.txtDesServicio.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDesServicio.ForeColor = System.Drawing.Color.Black;
            this.txtDesServicio.Location = new System.Drawing.Point(168, 156);
            this.txtDesServicio.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDesServicio.Multiline = true;
            this.txtDesServicio.Name = "txtDesServicio";
            this.txtDesServicio.Order = 0;
            this.txtDesServicio.ParentConn = null;
            this.txtDesServicio.ParentDA = null;
            this.txtDesServicio.PK = false;
            this.txtDesServicio.Search = false;
            this.txtDesServicio.Size = new System.Drawing.Size(382, 23);
            this.txtDesServicio.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDesServicio.TabIndex = 15;
            this.txtDesServicio.Upp = false;
            this.txtDesServicio.Value = "";
            // 
            // cboServicio
            // 
            this.cboServicio.Add = false;
            this.cboServicio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboServicio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboServicio.BackColor = System.Drawing.Color.White;
            this.cboServicio.Caption = "Service";
            this.cboServicio.DBField = null;
            this.cboServicio.DBFieldType = null;
            this.cboServicio.DefaultValue = null;
            this.cboServicio.Del = false;
            this.cboServicio.DependingRS = null;
            this.cboServicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboServicio.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboServicio.ForeColor = System.Drawing.Color.Black;
            this.cboServicio.FormattingEnabled = true;
            this.cboServicio.Location = new System.Drawing.Point(11, 153);
            this.cboServicio.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboServicio.Name = "cboServicio";
            this.cboServicio.Order = 0;
            this.cboServicio.ParentConn = null;
            this.cboServicio.ParentDA = null;
            this.cboServicio.PK = false;
            this.cboServicio.Search = false;
            this.cboServicio.Size = new System.Drawing.Size(130, 24);
            this.cboServicio.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboServicio.TabIndex = 13;
            this.cboServicio.TBDescription = null;
            this.cboServicio.Upp = false;
            this.cboServicio.Value = "";
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
            this.txtNotes.Location = new System.Drawing.Point(168, 198);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Order = 0;
            this.txtNotes.ParentConn = null;
            this.txtNotes.ParentDA = null;
            this.txtNotes.PK = false;
            this.txtNotes.Search = false;
            this.txtNotes.Size = new System.Drawing.Size(383, 17);
            this.txtNotes.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtNotes.TabIndex = 11;
            this.txtNotes.Upp = false;
            this.txtNotes.Value = "";
            // 
            // txtSuppDoc
            // 
            this.txtSuppDoc.Add = false;
            this.txtSuppDoc.BackColor = System.Drawing.Color.White;
            this.txtSuppDoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSuppDoc.Caption = "Supplier Doc";
            this.txtSuppDoc.DBField = null;
            this.txtSuppDoc.DBFieldType = null;
            this.txtSuppDoc.DefaultValue = null;
            this.txtSuppDoc.Del = false;
            this.txtSuppDoc.DependingRS = null;
            this.txtSuppDoc.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSuppDoc.ForeColor = System.Drawing.Color.Black;
            this.txtSuppDoc.Location = new System.Drawing.Point(12, 198);
            this.txtSuppDoc.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtSuppDoc.Name = "txtSuppDoc";
            this.txtSuppDoc.Order = 0;
            this.txtSuppDoc.ParentConn = null;
            this.txtSuppDoc.ParentDA = null;
            this.txtSuppDoc.PK = false;
            this.txtSuppDoc.Search = false;
            this.txtSuppDoc.Size = new System.Drawing.Size(121, 17);
            this.txtSuppDoc.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtSuppDoc.TabIndex = 7;
            this.txtSuppDoc.Upp = false;
            this.txtSuppDoc.Value = "";
            // 
            // txtFecha
            // 
            this.txtFecha.Add = false;
            this.txtFecha.BackColor = System.Drawing.Color.White;
            this.txtFecha.BorderColor = System.Drawing.Color.White;
            this.txtFecha.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.txtFecha.Caption = "Receival Date";
            this.txtFecha.CustomFormat = "dd/MM/yyyy H:mm";
            this.txtFecha.DBField = null;
            this.txtFecha.DBFieldType = null;
            this.txtFecha.DefaultValue = null;
            this.txtFecha.Del = false;
            this.txtFecha.DependingRS = null;
            this.txtFecha.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFecha.ForeColor = System.Drawing.Color.Black;
            this.txtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFecha.Location = new System.Drawing.Point(388, 113);
            this.txtFecha.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Order = 0;
            this.txtFecha.ParentConn = null;
            this.txtFecha.ParentDA = null;
            this.txtFecha.PK = false;
            this.txtFecha.Search = false;
            this.txtFecha.Size = new System.Drawing.Size(163, 24);
            this.txtFecha.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtFecha.TabIndex = 5;
            this.txtFecha.Upp = false;
            this.txtFecha.Value = new System.DateTime(2016, 6, 22, 13, 36, 41, 91);
            // 
            // txtEntrada
            // 
            this.txtEntrada.Add = false;
            this.txtEntrada.BackColor = System.Drawing.Color.White;
            this.txtEntrada.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEntrada.Caption = "Receival Number";
            this.txtEntrada.DBField = null;
            this.txtEntrada.DBFieldType = null;
            this.txtEntrada.DefaultValue = null;
            this.txtEntrada.Del = false;
            this.txtEntrada.DependingRS = null;
            this.txtEntrada.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEntrada.ForeColor = System.Drawing.Color.Black;
            this.txtEntrada.Location = new System.Drawing.Point(11, 110);
            this.txtEntrada.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtEntrada.Multiline = true;
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Order = 0;
            this.txtEntrada.ParentConn = null;
            this.txtEntrada.ParentDA = null;
            this.txtEntrada.PK = false;
            this.txtEntrada.Search = false;
            this.txtEntrada.Size = new System.Drawing.Size(121, 24);
            this.txtEntrada.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtEntrada.TabIndex = 1;
            this.txtEntrada.Upp = false;
            this.txtEntrada.Value = "";
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
            this.CTLM.ReQuery = false;
            
            this.CTLM.Size = new System.Drawing.Size(290, 29);
            this.CTLM.sSPAdd = "";
            this.CTLM.sSPDel = "";
            this.CTLM.sSPUpp = "";
            this.CTLM.StatusBarProgress = null;
            this.CTLM.TabIndex = 0;
            this.CTLM.Text = "ctlMantenimientoNet1";
            // 
            // fSimpleReceivals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 650);
            this.Controls.Add(this.lstFlags);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.VS);
            this.Controls.Add(this.txtDesServicio);
            this.Controls.Add(this.cboServicio);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtSuppDoc);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.txtEntrada);
            this.Controls.Add(this.CTLM);
            this.Name = "fSimpleReceivals";
            this.ShowIcon = false;
            this.Text = "Receivals";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private EspackFormControls.EspackTextBox txtEntrada;
        private EspackFormControls.EspackDateTimePicker txtFecha;
        private EspackFormControls.EspackTextBox txtSuppDoc;
        private EspackFormControls.EspackTextBox txtNotes;
        private EspackFormControls.EspackComboBox cboServicio;
        private EspackFormControls.EspackTextBox txtDesServicio;
        private VSGrid.CtlVSGrid VS;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnLabelCMs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnReceived;
        private EspackFormControls.EspackCheckedListBox lstFlags;
    }
}