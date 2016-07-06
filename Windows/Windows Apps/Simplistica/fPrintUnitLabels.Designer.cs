namespace Simplistica
{
    partial class fPrintUnitLabels
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
            this.txtQty = new EspackFormControls.NumericTextBox();
            this.cboService = new EspackFormControls.EspackComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtDesService = new EspackFormControls.EspackTextBox();
            this.SuspendLayout();
            // 
            // txtQty
            // 
            this.txtQty.Add = false;
            this.txtQty.AllowSpace = false;
            this.txtQty.BackColor = System.Drawing.Color.White;
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQty.Caption = "Number of labels";
            this.txtQty.DBField = null;
            this.txtQty.DBFieldType = null;
            this.txtQty.DefaultValue = null;
            this.txtQty.Del = false;
            this.txtQty.DependingRS = null;
            this.txtQty.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtQty.ForeColor = System.Drawing.Color.Black;
            this.txtQty.Length = 0;
            this.txtQty.Location = new System.Drawing.Point(58, 113);
            this.txtQty.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtQty.Mask = false;
            this.txtQty.Multiline = true;
            this.txtQty.Name = "txtQty";
            this.txtQty.Order = 0;
            this.txtQty.ParentConn = null;
            this.txtQty.ParentDA = null;
            this.txtQty.PK = false;
            this.txtQty.Precision = 0;
            this.txtQty.Search = false;
            this.txtQty.Size = new System.Drawing.Size(160, 24);
            this.txtQty.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtQty.TabIndex = 0;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.ThousandsGroup = false;
            this.txtQty.Upp = false;
            this.txtQty.Value = null;
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
            this.cboService.Location = new System.Drawing.Point(58, 59);
            this.cboService.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboService.Name = "cboService";
            this.cboService.Order = 0;
            this.cboService.ParentConn = null;
            this.cboService.ParentDA = null;
            this.cboService.PK = false;
            this.cboService.Search = false;
            this.cboService.Size = new System.Drawing.Size(160, 24);
            this.cboService.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboService.TabIndex = 2;
            this.cboService.TBDescription = null;
            this.cboService.Upp = false;
            this.cboService.Value = "";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(407, 114);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtDesService
            // 
            this.txtDesService.Add = false;
            this.txtDesService.BackColor = System.Drawing.Color.White;
            this.txtDesService.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDesService.Caption = "";
            this.txtDesService.DBField = null;
            this.txtDesService.DBFieldType = null;
            this.txtDesService.DefaultValue = null;
            this.txtDesService.Del = false;
            this.txtDesService.DependingRS = null;
            this.txtDesService.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDesService.ForeColor = System.Drawing.Color.Black;
            this.txtDesService.Location = new System.Drawing.Point(225, 58);
            this.txtDesService.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtDesService.Multiline = true;
            this.txtDesService.Name = "txtDesService";
            this.txtDesService.Order = 0;
            this.txtDesService.ParentConn = null;
            this.txtDesService.ParentDA = null;
            this.txtDesService.PK = false;
            this.txtDesService.Search = false;
            this.txtDesService.Size = new System.Drawing.Size(257, 24);
            this.txtDesService.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtDesService.TabIndex = 5;
            this.txtDesService.Upp = false;
            this.txtDesService.Value = "";
            // 
            // fPrintUnitLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 189);
            this.Controls.Add(this.txtDesService);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.cboService);
            this.Controls.Add(this.txtQty);
            this.Name = "fPrintUnitLabels";
            this.Text = "fPrintUnitLabels";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private EspackFormControls.EspackComboBox cboService;
        private System.Windows.Forms.Button btnPrint;
        private EspackFormControls.EspackTextBox txtDesService;
        private EspackFormControls.NumericTextBox txtQty;
    }
}