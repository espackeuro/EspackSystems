namespace Simplistica
{
    partial class fProductionOrders
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
            this.txtNumero = new EspackFormControls.NumericTextBox();
            this.txtFecha = new EspackFormControls.EspackDateTimePicker();
            this.txtDesServicio = new EspackFormControls.EspackTextBox();
            this.cboServicio = new EspackFormControls.EspackComboBox();
            this.txtDesRoute = new EspackFormControls.EspackTextBox();
            this.cboRuta = new EspackFormControls.EspackComboBox();
            this.VS = new VSGrid.CtlVSGrid();
            this.txtExpedicion = new EspackFormControls.NumericTextBox();
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
            // txtNumero
            // 
            this.txtNumero.Add = false;
            this.txtNumero.AllowSpace = false;
            this.txtNumero.BackColor = System.Drawing.Color.White;
            this.txtNumero.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumero.Caption = "Order Number";
            this.txtNumero.DBField = null;
            this.txtNumero.DBFieldType = null;
            this.txtNumero.DefaultValue = null;
            this.txtNumero.Del = false;
            this.txtNumero.DependingRS = null;
            this.txtNumero.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNumero.ForeColor = System.Drawing.Color.Black;
            this.txtNumero.Length = 0;
            this.txtNumero.Location = new System.Drawing.Point(13, 67);
            this.txtNumero.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtNumero.Mask = false;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Order = 0;
            this.txtNumero.ParentConn = null;
            this.txtNumero.ParentDA = null;
            this.txtNumero.PK = false;
            this.txtNumero.Precision = 0;
            this.txtNumero.Search = false;
            this.txtNumero.Size = new System.Drawing.Size(145, 17);
            this.txtNumero.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtNumero.TabIndex = 1;
            this.txtNumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNumero.ThousandsGroup = false;
            this.txtNumero.Upp = false;
            this.txtNumero.Value = null;
            // 
            // txtFecha
            // 
            this.txtFecha.Add = false;
            this.txtFecha.BackColor = System.Drawing.Color.White;
            this.txtFecha.BorderColor = System.Drawing.Color.White;
            this.txtFecha.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.txtFecha.Caption = "Date";
            this.txtFecha.Checked = false;
            this.txtFecha.CustomFormat = " ";
            this.txtFecha.DBField = null;
            this.txtFecha.DBFieldType = null;
            this.txtFecha.DefaultValue = null;
            this.txtFecha.Del = false;
            this.txtFecha.DependingRS = null;
            this.txtFecha.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFecha.ForeColor = System.Drawing.Color.Black;
            this.txtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFecha.Location = new System.Drawing.Point(13, 103);
            this.txtFecha.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Nullable = false;
            this.txtFecha.Order = 0;
            this.txtFecha.ParentConn = null;
            this.txtFecha.ParentDA = null;
            this.txtFecha.PK = false;
            this.txtFecha.Search = false;
            this.txtFecha.Size = new System.Drawing.Size(145, 24);
            this.txtFecha.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtFecha.TabIndex = 3;
            this.txtFecha.Upp = false;
            this.txtFecha.Value = null;
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
            this.txtDesServicio.Location = new System.Drawing.Point(309, 67);
            this.txtDesServicio.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDesServicio.Name = "txtDesServicio";
            this.txtDesServicio.Order = 0;
            this.txtDesServicio.ParentConn = null;
            this.txtDesServicio.ParentDA = null;
            this.txtDesServicio.PK = false;
            this.txtDesServicio.Search = false;
            this.txtDesServicio.Size = new System.Drawing.Size(297, 17);
            this.txtDesServicio.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDesServicio.TabIndex = 23;
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
            this.cboServicio.Location = new System.Drawing.Point(173, 64);
            this.cboServicio.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboServicio.Name = "cboServicio";
            this.cboServicio.Order = 0;
            this.cboServicio.ParentConn = null;
            this.cboServicio.ParentDA = null;
            this.cboServicio.PK = false;
            this.cboServicio.Search = false;
            this.cboServicio.Size = new System.Drawing.Size(130, 24);
            this.cboServicio.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboServicio.TabIndex = 22;
            this.cboServicio.TBDescription = null;
            this.cboServicio.Upp = false;
            this.cboServicio.Value = "";
            // 
            // txtDesRoute
            // 
            this.txtDesRoute.Add = false;
            this.txtDesRoute.BackColor = System.Drawing.Color.White;
            this.txtDesRoute.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDesRoute.Caption = "";
            this.txtDesRoute.DBField = null;
            this.txtDesRoute.DBFieldType = null;
            this.txtDesRoute.DefaultValue = null;
            this.txtDesRoute.Del = false;
            this.txtDesRoute.DependingRS = null;
            this.txtDesRoute.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDesRoute.ForeColor = System.Drawing.Color.Black;
            this.txtDesRoute.Location = new System.Drawing.Point(309, 110);
            this.txtDesRoute.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDesRoute.Name = "txtDesRoute";
            this.txtDesRoute.Order = 0;
            this.txtDesRoute.ParentConn = null;
            this.txtDesRoute.ParentDA = null;
            this.txtDesRoute.PK = false;
            this.txtDesRoute.Search = false;
            this.txtDesRoute.Size = new System.Drawing.Size(143, 17);
            this.txtDesRoute.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDesRoute.TabIndex = 31;
            this.txtDesRoute.Upp = false;
            this.txtDesRoute.Value = "";
            // 
            // cboRuta
            // 
            this.cboRuta.Add = false;
            this.cboRuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboRuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboRuta.BackColor = System.Drawing.Color.White;
            this.cboRuta.Caption = "Route";
            this.cboRuta.DBField = null;
            this.cboRuta.DBFieldType = null;
            this.cboRuta.DefaultValue = null;
            this.cboRuta.Del = false;
            this.cboRuta.DependingRS = null;
            this.cboRuta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboRuta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboRuta.ForeColor = System.Drawing.Color.Black;
            this.cboRuta.FormattingEnabled = true;
            this.cboRuta.Location = new System.Drawing.Point(173, 106);
            this.cboRuta.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboRuta.Name = "cboRuta";
            this.cboRuta.Order = 0;
            this.cboRuta.ParentConn = null;
            this.cboRuta.ParentDA = null;
            this.cboRuta.PK = false;
            this.cboRuta.Search = false;
            this.cboRuta.Size = new System.Drawing.Size(130, 24);
            this.cboRuta.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboRuta.TabIndex = 30;
            this.cboRuta.TBDescription = null;
            this.cboRuta.Upp = false;
            this.cboRuta.Value = "";
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
            this.VS.Location = new System.Drawing.Point(13, 151);
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
            this.VS.Size = new System.Drawing.Size(593, 308);
            this.VS.SQL = null;
            this.VS.sSPAdd = "";
            this.VS.sSPDel = "";
            this.VS.sSPUpp = "";
            this.VS.Status = CommonTools.EnumStatus.SEARCH;
            this.VS.TabIndex = 32;
            this.VS.Upp = false;
            this.VS.Value = null;
            // 
            // txtExpedicion
            // 
            this.txtExpedicion.Add = false;
            this.txtExpedicion.AllowSpace = false;
            this.txtExpedicion.BackColor = System.Drawing.Color.White;
            this.txtExpedicion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExpedicion.Caption = "Expedition";
            this.txtExpedicion.DBField = null;
            this.txtExpedicion.DBFieldType = null;
            this.txtExpedicion.DefaultValue = null;
            this.txtExpedicion.Del = false;
            this.txtExpedicion.DependingRS = null;
            this.txtExpedicion.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtExpedicion.ForeColor = System.Drawing.Color.Black;
            this.txtExpedicion.Length = 0;
            this.txtExpedicion.Location = new System.Drawing.Point(461, 109);
            this.txtExpedicion.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtExpedicion.Mask = false;
            this.txtExpedicion.Name = "txtExpedicion";
            this.txtExpedicion.Order = 0;
            this.txtExpedicion.ParentConn = null;
            this.txtExpedicion.ParentDA = null;
            this.txtExpedicion.PK = false;
            this.txtExpedicion.Precision = 0;
            this.txtExpedicion.Search = false;
            this.txtExpedicion.Size = new System.Drawing.Size(145, 17);
            this.txtExpedicion.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtExpedicion.TabIndex = 41;
            this.txtExpedicion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExpedicion.ThousandsGroup = false;
            this.txtExpedicion.Upp = false;
            this.txtExpedicion.Value = null;
            // 
            // fProductionOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 500);
            this.Controls.Add(this.txtExpedicion);
            this.Controls.Add(this.VS);
            this.Controls.Add(this.txtDesRoute);
            this.Controls.Add(this.cboRuta);
            this.Controls.Add(this.txtDesServicio);
            this.Controls.Add(this.cboServicio);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.CTLM);
            this.Name = "fProductionOrders";
            this.Text = "fProductionOrders";
            ((System.ComponentModel.ISupportInitialize)(this.VS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private EspackFormControls.NumericTextBox txtNumero;
        private EspackFormControls.EspackDateTimePicker txtFecha;
        private EspackFormControls.EspackTextBox txtDesServicio;
        private EspackFormControls.EspackComboBox cboServicio;
        private EspackFormControls.EspackTextBox txtDesRoute;
        private EspackFormControls.EspackComboBox cboRuta;
        private VSGrid.CtlVSGrid VS;
        private EspackFormControls.NumericTextBox txtExpedicion;
    }
}