﻿namespace Etiquetas_CS
{
    partial class fMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnObtain = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintList = new System.Windows.Forms.Button();
            this.cboPrinters = new EspackFormControls.EspackComboBox();
            this.txtCode = new EspackFormControls.EspackTextBox();
            this.vsLabels = new VSGrid.CtlVSGrid();
            this.vsGroups = new VSGrid.CtlVSGrid();
            this.vsParameters = new VSGrid.CtlVSGrid();
            ((System.ComponentModel.ISupportInitialize)(this.vsLabels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vsGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vsParameters)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(425, 161);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(425, 190);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(425, 219);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnObtain
            // 
            this.btnObtain.Location = new System.Drawing.Point(258, 66);
            this.btnObtain.Name = "btnObtain";
            this.btnObtain.Size = new System.Drawing.Size(75, 23);
            this.btnObtain.TabIndex = 12;
            this.btnObtain.Text = "Obtain";
            this.btnObtain.UseVisualStyleBackColor = true;
            this.btnObtain.Click += new System.EventHandler(this.btnObtain_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(258, 95);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnPrintList
            // 
            this.btnPrintList.Location = new System.Drawing.Point(258, 124);
            this.btnPrintList.Name = "btnPrintList";
            this.btnPrintList.Size = new System.Drawing.Size(75, 23);
            this.btnPrintList.TabIndex = 14;
            this.btnPrintList.Text = "Print List";
            this.btnPrintList.UseVisualStyleBackColor = true;
            // 
            // cboPrinters
            // 
            this.cboPrinters.Add = false;
            this.cboPrinters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPrinters.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPrinters.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPrinters.BackColor = System.Drawing.Color.White;
            this.cboPrinters.Caption = "Printer";
            this.cboPrinters.DBField = null;
            this.cboPrinters.DBFieldType = null;
            this.cboPrinters.DefaultValue = null;
            this.cboPrinters.Del = false;
            this.cboPrinters.DependingRS = null;
            this.cboPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPrinters.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cboPrinters.ForeColor = System.Drawing.Color.Black;
            this.cboPrinters.FormattingEnabled = true;
            this.cboPrinters.Location = new System.Drawing.Point(506, 25);
            this.cboPrinters.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.cboPrinters.Name = "cboPrinters";
            this.cboPrinters.Order = 0;
            this.cboPrinters.ParentConn = null;
            this.cboPrinters.ParentDA = null;
            this.cboPrinters.PK = false;
            this.cboPrinters.Search = false;
            this.cboPrinters.Size = new System.Drawing.Size(240, 24);
            this.cboPrinters.Status = CommonTools.EnumStatus.ADDNEW;
            this.cboPrinters.TabIndex = 5;
            this.cboPrinters.TBDescription = null;
            this.cboPrinters.Upp = false;
            this.cboPrinters.Value = "";
            // 
            // txtCode
            // 
            this.txtCode.Add = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.Caption = "Code";
            this.txtCode.DBField = null;
            this.txtCode.DBFieldType = null;
            this.txtCode.DefaultValue = null;
            this.txtCode.Del = false;
            this.txtCode.DependingRS = null;
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Location = new System.Drawing.Point(12, 25);
            this.txtCode.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Order = 0;
            this.txtCode.ParentConn = null;
            this.txtCode.ParentDA = null;
            this.txtCode.PK = false;
            this.txtCode.Search = false;
            this.txtCode.Size = new System.Drawing.Size(240, 24);
            this.txtCode.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtCode.TabIndex = 3;
            this.txtCode.Upp = false;
            this.txtCode.Value = "";
            // 
            // vsLabels
            // 
            this.vsLabels.Add = false;
            this.vsLabels.AllowDelete = true;
            this.vsLabels.AllowInsert = true;
            this.vsLabels.AllowUpdate = false;
            this.vsLabels.AllowUserToAddRows = false;
            this.vsLabels.AllowUserToDeleteRows = false;
            this.vsLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vsLabels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.vsLabels.Caption = "";
            this.vsLabels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vsLabels.Conn = null;
            this.vsLabels.DBField = null;
            this.vsLabels.DBFieldType = null;
            this.vsLabels.DBTable = null;
            this.vsLabels.DefaultValue = null;
            this.vsLabels.Del = false;
            this.vsLabels.DependingRS = null;
            this.vsLabels.EspackControlParent = null;
            this.vsLabels.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.vsLabels.Location = new System.Drawing.Point(12, 270);
            this.vsLabels.MsgStatusLabel = null;
            this.vsLabels.Name = "vsLabels";
            this.vsLabels.NumPages = 0;
            this.vsLabels.Order = 0;
            this.vsLabels.Page = 0;
            this.vsLabels.Paginate = false;
            this.vsLabels.ParentConn = null;
            this.vsLabels.ParentDA = null;
            this.vsLabels.PK = false;
            this.vsLabels.ReadOnly = true;
            this.vsLabels.RowHeadersVisible = false;
            this.vsLabels.Search = false;
            this.vsLabels.Size = new System.Drawing.Size(734, 440);
            this.vsLabels.SQL = null;
            this.vsLabels.sSPAdd = "";
            this.vsLabels.sSPDel = "";
            this.vsLabels.sSPUpp = "";
            this.vsLabels.Status = CommonTools.EnumStatus.SEARCH;
            this.vsLabels.TabIndex = 9;
            this.vsLabels.Upp = false;
            this.vsLabels.Value = null;
            // 
            // vsGroups
            // 
            this.vsGroups.Add = false;
            this.vsGroups.AllowDelete = true;
            this.vsGroups.AllowInsert = true;
            this.vsGroups.AllowUpdate = false;
            this.vsGroups.AllowUserToAddRows = false;
            this.vsGroups.AllowUserToDeleteRows = false;
            this.vsGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vsGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.vsGroups.Caption = "Groups";
            this.vsGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vsGroups.Conn = null;
            this.vsGroups.DBField = null;
            this.vsGroups.DBFieldType = null;
            this.vsGroups.DBTable = null;
            this.vsGroups.DefaultValue = null;
            this.vsGroups.Del = false;
            this.vsGroups.DependingRS = null;
            this.vsGroups.EspackControlParent = null;
            this.vsGroups.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.vsGroups.Location = new System.Drawing.Point(506, 66);
            this.vsGroups.MsgStatusLabel = null;
            this.vsGroups.Name = "vsGroups";
            this.vsGroups.NumPages = 0;
            this.vsGroups.Order = 0;
            this.vsGroups.Page = 0;
            this.vsGroups.Paginate = false;
            this.vsGroups.ParentConn = null;
            this.vsGroups.ParentDA = null;
            this.vsGroups.PK = false;
            this.vsGroups.ReadOnly = true;
            this.vsGroups.RowHeadersVisible = false;
            this.vsGroups.Search = false;
            this.vsGroups.Size = new System.Drawing.Size(240, 176);
            this.vsGroups.SQL = null;
            this.vsGroups.sSPAdd = "";
            this.vsGroups.sSPDel = "";
            this.vsGroups.sSPUpp = "";
            this.vsGroups.Status = CommonTools.EnumStatus.SEARCH;
            this.vsGroups.TabIndex = 8;
            this.vsGroups.Upp = false;
            this.vsGroups.Value = null;
            // 
            // vsParameters
            // 
            this.vsParameters.Add = false;
            this.vsParameters.AllowDelete = true;
            this.vsParameters.AllowInsert = true;
            this.vsParameters.AllowUpdate = false;
            this.vsParameters.AllowUserToAddRows = false;
            this.vsParameters.AllowUserToDeleteRows = false;
            this.vsParameters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.vsParameters.Caption = "Parameters";
            this.vsParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vsParameters.Conn = null;
            this.vsParameters.DBField = null;
            this.vsParameters.DBFieldType = null;
            this.vsParameters.DBTable = null;
            this.vsParameters.DefaultValue = null;
            this.vsParameters.Del = false;
            this.vsParameters.DependingRS = null;
            this.vsParameters.EspackControlParent = null;
            this.vsParameters.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.vsParameters.Location = new System.Drawing.Point(12, 66);
            this.vsParameters.MsgStatusLabel = null;
            this.vsParameters.Name = "vsParameters";
            this.vsParameters.NumPages = 0;
            this.vsParameters.Order = 0;
            this.vsParameters.Page = 0;
            this.vsParameters.Paginate = false;
            this.vsParameters.ParentConn = null;
            this.vsParameters.ParentDA = null;
            this.vsParameters.PK = false;
            this.vsParameters.RowHeadersVisible = false;
            this.vsParameters.Search = false;
            this.vsParameters.Size = new System.Drawing.Size(240, 176);
            this.vsParameters.SQL = null;
            this.vsParameters.sSPAdd = "";
            this.vsParameters.sSPDel = "";
            this.vsParameters.sSPUpp = "";
            this.vsParameters.Status = CommonTools.EnumStatus.SEARCH;
            this.vsParameters.TabIndex = 7;
            this.vsParameters.Upp = false;
            this.vsParameters.Value = null;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 722);
            this.Controls.Add(this.btnPrintList);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnObtain);
            this.Controls.Add(this.vsLabels);
            this.Controls.Add(this.vsGroups);
            this.Controls.Add(this.vsParameters);
            this.Controls.Add(this.cboPrinters);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "fMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.vsLabels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vsGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vsParameters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private EspackFormControls.EspackTextBox txtCode;
        private EspackFormControls.EspackComboBox cboPrinters;
        private VSGrid.CtlVSGrid vsParameters;
        private VSGrid.CtlVSGrid vsGroups;
        private VSGrid.CtlVSGrid vsLabels;
        private System.Windows.Forms.Button btnObtain;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintList;
    }
}

