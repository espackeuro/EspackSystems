namespace Simplistica
{
    partial class fSimpleReferences
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
            this.txtReference = new EspackFormControls.EspackTextBox();
            this.txtDescription = new EspackFormControls.EspackTextBox();
            this.txtFase4 = new EspackFormControls.NumericTextBox();
            this.txtPrice = new EspackFormControls.NumericTextBox();
            this.txtPeso = new EspackFormControls.NumericTextBox();
            this.txtNotes = new EspackFormControls.EspackTextBox();
            this.cboServicio = new EspackFormControls.EspackComboBox();
            this.txtDesServicio = new EspackFormControls.EspackTextBox();
            this.lstFlags = new EspackFormControls.EspackCheckedListBox();
            this.txtMin = new EspackFormControls.NumericTextBox();
            this.txtMax = new EspackFormControls.NumericTextBox();
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
            this.CTLM.TabIndex = 0;
            this.CTLM.Text = "ctlMantenimientoNet1";
            // 
            // txtReference
            // 
            this.txtReference.Add = false;
            this.txtReference.BackColor = System.Drawing.Color.White;
            this.txtReference.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReference.Caption = "Reference";
            this.txtReference.DBField = null;
            this.txtReference.DBFieldType = null;
            this.txtReference.DefaultValue = null;
            this.txtReference.Del = false;
            this.txtReference.DependingRS = null;
            this.txtReference.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtReference.ForeColor = System.Drawing.Color.Black;
            this.txtReference.Location = new System.Drawing.Point(12, 73);
            this.txtReference.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtReference.Name = "txtReference";
            this.txtReference.Order = 0;
            this.txtReference.ParentConn = null;
            this.txtReference.ParentDA = null;
            this.txtReference.PK = true;
            this.txtReference.Search = false;
            this.txtReference.Size = new System.Drawing.Size(287, 17);
            this.txtReference.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtReference.TabIndex = 1;
            this.txtReference.Upp = false;
            this.txtReference.Value = "";
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
            this.txtDescription.Location = new System.Drawing.Point(13, 154);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Order = 0;
            this.txtDescription.ParentConn = null;
            this.txtDescription.ParentDA = null;
            this.txtDescription.PK = false;
            this.txtDescription.Search = false;
            this.txtDescription.Size = new System.Drawing.Size(433, 17);
            this.txtDescription.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDescription.TabIndex = 3;
            this.txtDescription.Upp = false;
            this.txtDescription.Value = "";
            // 
            // txtFase4
            // 
            this.txtFase4.Add = false;
            this.txtFase4.AllowSpace = false;
            this.txtFase4.BackColor = System.Drawing.Color.White;
            this.txtFase4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFase4.Caption = "Qty per pack";
            this.txtFase4.DBField = null;
            this.txtFase4.DBFieldType = null;
            this.txtFase4.DefaultValue = null;
            this.txtFase4.Del = false;
            this.txtFase4.DependingRS = null;
            this.txtFase4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFase4.ForeColor = System.Drawing.Color.Black;
            this.txtFase4.Length = 0;
            this.txtFase4.Location = new System.Drawing.Point(13, 190);
            this.txtFase4.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtFase4.Mask = false;
            this.txtFase4.Name = "txtFase4";
            this.txtFase4.Order = 0;
            this.txtFase4.ParentConn = null;
            this.txtFase4.ParentDA = null;
            this.txtFase4.PK = false;
            this.txtFase4.Precision = 0;
            this.txtFase4.Search = false;
            this.txtFase4.Size = new System.Drawing.Size(100, 17);
            this.txtFase4.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtFase4.TabIndex = 5;
            this.txtFase4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFase4.ThousandsGroup = false;
            this.txtFase4.Upp = false;
            this.txtFase4.Value = null;
            // 
            // txtPrice
            // 
            this.txtPrice.Add = false;
            this.txtPrice.AllowSpace = false;
            this.txtPrice.BackColor = System.Drawing.Color.White;
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrice.Caption = "Price";
            this.txtPrice.DBField = null;
            this.txtPrice.DBFieldType = null;
            this.txtPrice.DefaultValue = null;
            this.txtPrice.Del = false;
            this.txtPrice.DependingRS = null;
            this.txtPrice.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPrice.ForeColor = System.Drawing.Color.Black;
            this.txtPrice.Length = 0;
            this.txtPrice.Location = new System.Drawing.Point(120, 190);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtPrice.Mask = false;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Order = 0;
            this.txtPrice.ParentConn = null;
            this.txtPrice.ParentDA = null;
            this.txtPrice.PK = false;
            this.txtPrice.Precision = 0;
            this.txtPrice.Search = false;
            this.txtPrice.Size = new System.Drawing.Size(100, 17);
            this.txtPrice.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtPrice.TabIndex = 9;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.ThousandsGroup = false;
            this.txtPrice.Upp = false;
            this.txtPrice.Value = null;
            // 
            // txtPeso
            // 
            this.txtPeso.Add = false;
            this.txtPeso.AllowSpace = false;
            this.txtPeso.BackColor = System.Drawing.Color.White;
            this.txtPeso.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPeso.Caption = "Weight";
            this.txtPeso.DBField = null;
            this.txtPeso.DBFieldType = null;
            this.txtPeso.DefaultValue = null;
            this.txtPeso.Del = false;
            this.txtPeso.DependingRS = null;
            this.txtPeso.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPeso.ForeColor = System.Drawing.Color.Black;
            this.txtPeso.Length = 0;
            this.txtPeso.Location = new System.Drawing.Point(227, 190);
            this.txtPeso.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtPeso.Mask = false;
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.Order = 0;
            this.txtPeso.ParentConn = null;
            this.txtPeso.ParentDA = null;
            this.txtPeso.PK = false;
            this.txtPeso.Precision = 0;
            this.txtPeso.Search = false;
            this.txtPeso.Size = new System.Drawing.Size(100, 17);
            this.txtPeso.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtPeso.TabIndex = 11;
            this.txtPeso.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPeso.ThousandsGroup = false;
            this.txtPeso.Upp = false;
            this.txtPeso.Value = null;
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
            this.txtNotes.Location = new System.Drawing.Point(13, 262);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Order = 0;
            this.txtNotes.ParentConn = null;
            this.txtNotes.ParentDA = null;
            this.txtNotes.PK = false;
            this.txtNotes.Search = false;
            this.txtNotes.Size = new System.Drawing.Size(433, 64);
            this.txtNotes.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtNotes.TabIndex = 13;
            this.txtNotes.Upp = false;
            this.txtNotes.Value = "";
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
            this.cboServicio.Location = new System.Drawing.Point(12, 109);
            this.cboServicio.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboServicio.Name = "cboServicio";
            this.cboServicio.Order = 0;
            this.cboServicio.ParentConn = null;
            this.cboServicio.ParentDA = null;
            this.cboServicio.PK = false;
            this.cboServicio.Search = false;
            this.cboServicio.Size = new System.Drawing.Size(130, 24);
            this.cboServicio.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboServicio.TabIndex = 17;
            this.cboServicio.TBDescription = null;
            this.cboServicio.Upp = false;
            this.cboServicio.Value = "";
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
            this.txtDesServicio.Location = new System.Drawing.Point(149, 115);
            this.txtDesServicio.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDesServicio.Name = "txtDesServicio";
            this.txtDesServicio.Order = 0;
            this.txtDesServicio.ParentConn = null;
            this.txtDesServicio.ParentDA = null;
            this.txtDesServicio.PK = false;
            this.txtDesServicio.Search = false;
            this.txtDesServicio.Size = new System.Drawing.Size(297, 17);
            this.txtDesServicio.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDesServicio.TabIndex = 19;
            this.txtDesServicio.Upp = false;
            this.txtDesServicio.Value = "";
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
            this.lstFlags.Location = new System.Drawing.Point(12, 345);
            this.lstFlags.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lstFlags.Name = "lstFlags";
            this.lstFlags.Order = 0;
            this.lstFlags.ParentConn = null;
            this.lstFlags.ParentDA = null;
            this.lstFlags.PK = false;
            this.lstFlags.Search = false;
            this.lstFlags.Size = new System.Drawing.Size(433, 76);
            this.lstFlags.Status = CommonTools.EnumStatus.ADDNEW;
            this.lstFlags.TabIndex = 21;
            this.lstFlags.Upp = false;
            this.lstFlags.Value = "";
            // 
            // txtMin
            // 
            this.txtMin.Add = false;
            this.txtMin.AllowSpace = false;
            this.txtMin.BackColor = System.Drawing.Color.White;
            this.txtMin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMin.Caption = "Min";
            this.txtMin.DBField = null;
            this.txtMin.DBFieldType = null;
            this.txtMin.DefaultValue = null;
            this.txtMin.Del = false;
            this.txtMin.DependingRS = null;
            this.txtMin.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMin.ForeColor = System.Drawing.Color.Black;
            this.txtMin.Length = 0;
            this.txtMin.Location = new System.Drawing.Point(12, 226);
            this.txtMin.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtMin.Mask = false;
            this.txtMin.Name = "txtMin";
            this.txtMin.Order = 0;
            this.txtMin.ParentConn = null;
            this.txtMin.ParentDA = null;
            this.txtMin.PK = false;
            this.txtMin.Precision = 0;
            this.txtMin.Search = false;
            this.txtMin.Size = new System.Drawing.Size(100, 17);
            this.txtMin.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtMin.TabIndex = 23;
            this.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMin.ThousandsGroup = false;
            this.txtMin.Upp = false;
            this.txtMin.Value = null;
            // 
            // txtMax
            // 
            this.txtMax.Add = false;
            this.txtMax.AllowSpace = false;
            this.txtMax.BackColor = System.Drawing.Color.White;
            this.txtMax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMax.Caption = "Max";
            this.txtMax.DBField = null;
            this.txtMax.DBFieldType = null;
            this.txtMax.DefaultValue = null;
            this.txtMax.Del = false;
            this.txtMax.DependingRS = null;
            this.txtMax.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMax.ForeColor = System.Drawing.Color.Black;
            this.txtMax.Length = 0;
            this.txtMax.Location = new System.Drawing.Point(120, 226);
            this.txtMax.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtMax.Mask = false;
            this.txtMax.Name = "txtMax";
            this.txtMax.Order = 0;
            this.txtMax.ParentConn = null;
            this.txtMax.ParentDA = null;
            this.txtMax.PK = false;
            this.txtMax.Precision = 0;
            this.txtMax.Search = false;
            this.txtMax.Size = new System.Drawing.Size(100, 17);
            this.txtMax.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtMax.TabIndex = 26;
            this.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMax.ThousandsGroup = false;
            this.txtMax.Upp = false;
            this.txtMax.Value = null;
            // 
            // fSimpleReferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 463);
            this.Controls.Add(this.txtMax);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.lstFlags);
            this.Controls.Add(this.txtDesServicio);
            this.Controls.Add(this.cboServicio);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtPeso);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtFase4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.CTLM);
            this.Name = "fSimpleReferences";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "References";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private EspackFormControls.EspackTextBox txtReference;
        private EspackFormControls.EspackTextBox txtDescription;
        private EspackFormControls.NumericTextBox txtFase4;
        private EspackFormControls.NumericTextBox txtPrice;
        private EspackFormControls.NumericTextBox txtPeso;
        private EspackFormControls.EspackTextBox txtNotes;
        private EspackFormControls.EspackComboBox cboServicio;
        private EspackFormControls.EspackTextBox txtDesServicio;
        private EspackFormControls.EspackCheckedListBox lstFlags;
        private EspackFormControls.NumericTextBox txtMin;
        private EspackFormControls.NumericTextBox txtMax;
    }
}