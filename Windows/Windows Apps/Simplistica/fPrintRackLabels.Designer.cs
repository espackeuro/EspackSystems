namespace simplistica
{
    partial class fPrintRackLabels
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
            this.cboCOD3 = new EspackFormControls.EspackComboBox();
            this.txtAISLE = new EspackFormControls.EspackTextBox();
            this.btnPRINT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboCOD3
            // 
            this.cboCOD3.Add = false;
            this.cboCOD3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCOD3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCOD3.BackColor = System.Drawing.Color.White;
            this.cboCOD3.Caption = "COD3";
            this.cboCOD3.DBField = null;
            this.cboCOD3.DBFieldType = null;
            this.cboCOD3.DefaultValue = null;
            this.cboCOD3.Del = false;
            this.cboCOD3.DependingRS = null;
            this.cboCOD3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCOD3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboCOD3.ForeColor = System.Drawing.Color.Black;
            this.cboCOD3.FormattingEnabled = true;
            this.cboCOD3.Location = new System.Drawing.Point(27, 43);
            this.cboCOD3.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboCOD3.Name = "cboCOD3";
            this.cboCOD3.Order = 0;
            this.cboCOD3.ParentConn = null;
            this.cboCOD3.ParentDA = null;
            this.cboCOD3.PK = false;
            this.cboCOD3.Search = false;
            this.cboCOD3.Size = new System.Drawing.Size(143, 24);
            this.cboCOD3.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboCOD3.TabIndex = 0;
            this.cboCOD3.TBDescription = null;
            this.cboCOD3.Upp = false;
            this.cboCOD3.Value = "";
            // 
            // txtAISLE
            // 
            this.txtAISLE.Add = false;
            this.txtAISLE.BackColor = System.Drawing.Color.White;
            this.txtAISLE.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAISLE.Caption = "AISLE";
            this.txtAISLE.DBField = null;
            this.txtAISLE.DBFieldType = null;
            this.txtAISLE.DefaultValue = null;
            this.txtAISLE.Del = false;
            this.txtAISLE.DependingRS = null;
            this.txtAISLE.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtAISLE.ForeColor = System.Drawing.Color.Black;
            this.txtAISLE.Location = new System.Drawing.Point(199, 43);
            this.txtAISLE.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtAISLE.Multiline = true;
            this.txtAISLE.Name = "txtAISLE";
            this.txtAISLE.Order = 0;
            this.txtAISLE.ParentConn = null;
            this.txtAISLE.ParentDA = null;
            this.txtAISLE.PK = false;
            this.txtAISLE.Search = false;
            this.txtAISLE.Size = new System.Drawing.Size(144, 24);
            this.txtAISLE.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtAISLE.TabIndex = 9;
            this.txtAISLE.Upp = false;
            this.txtAISLE.Value = "";
            // 
            // btnPRINT
            // 
            this.btnPRINT.Location = new System.Drawing.Point(386, 126);
            this.btnPRINT.Name = "btnPRINT";
            this.btnPRINT.Size = new System.Drawing.Size(117, 28);
            this.btnPRINT.TabIndex = 10;
            this.btnPRINT.Text = "PRINT";
            this.btnPRINT.UseVisualStyleBackColor = true;
            // 
            // fPrintRackLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 189);
            this.Controls.Add(this.btnPRINT);
            this.Controls.Add(this.txtAISLE);
            this.Controls.Add(this.cboCOD3);
            this.Name = "fPrintRackLabels";
            this.Text = "Rack LAbels";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EspackFormControls.EspackComboBox cboCOD3;
        private EspackFormControls.EspackTextBox txtAISLE;
        private System.Windows.Forms.Button btnPRINT;
    }
}