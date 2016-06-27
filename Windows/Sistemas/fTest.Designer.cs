namespace Sistemas
{
    partial class fTest
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
            this.txtIdreg = new EspackFormControls.EspackTextBox();
            this.txtCampo1 = new EspackFormControls.EspackTextBox();
            this.txtCampo2 = new EspackFormControls.EspackTextBox();
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
            // txtIdreg
            // 
            this.txtIdreg.Add = false;
            this.txtIdreg.BackColor = System.Drawing.Color.White;
            this.txtIdreg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIdreg.Caption = "IdReg";
            this.txtIdreg.DBField = null;
            this.txtIdreg.DBFieldType = null;
            this.txtIdreg.DefaultValue = null;
            this.txtIdreg.Del = false;
            this.txtIdreg.DependingRS = null;
            this.txtIdreg.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtIdreg.ForeColor = System.Drawing.Color.Black;
            this.txtIdreg.Location = new System.Drawing.Point(22, 73);
            this.txtIdreg.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtIdreg.Multiline = true;
            this.txtIdreg.Name = "txtIdreg";
            this.txtIdreg.Order = 0;
            this.txtIdreg.ParentConn = null;
            this.txtIdreg.ParentDA = null;
            this.txtIdreg.PK = false;
            this.txtIdreg.Search = false;
            this.txtIdreg.Size = new System.Drawing.Size(100, 24);
            this.txtIdreg.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtIdreg.TabIndex = 1;
            this.txtIdreg.Upp = false;
            this.txtIdreg.Value = "";
            // 
            // txtCampo1
            // 
            this.txtCampo1.Add = false;
            this.txtCampo1.BackColor = System.Drawing.Color.White;
            this.txtCampo1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCampo1.Caption = "Campo 1";
            this.txtCampo1.DBField = null;
            this.txtCampo1.DBFieldType = null;
            this.txtCampo1.DefaultValue = null;
            this.txtCampo1.Del = false;
            this.txtCampo1.DependingRS = null;
            this.txtCampo1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCampo1.ForeColor = System.Drawing.Color.Black;
            this.txtCampo1.Location = new System.Drawing.Point(22, 130);
            this.txtCampo1.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtCampo1.Multiline = true;
            this.txtCampo1.Name = "txtCampo1";
            this.txtCampo1.Order = 0;
            this.txtCampo1.ParentConn = null;
            this.txtCampo1.ParentDA = null;
            this.txtCampo1.PK = false;
            this.txtCampo1.Search = false;
            this.txtCampo1.Size = new System.Drawing.Size(281, 24);
            this.txtCampo1.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtCampo1.TabIndex = 3;
            this.txtCampo1.Upp = false;
            this.txtCampo1.Value = "";
            // 
            // txtCampo2
            // 
            this.txtCampo2.Add = false;
            this.txtCampo2.BackColor = System.Drawing.Color.White;
            this.txtCampo2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCampo2.Caption = "Campo 2";
            this.txtCampo2.DBField = null;
            this.txtCampo2.DBFieldType = null;
            this.txtCampo2.DefaultValue = null;
            this.txtCampo2.Del = false;
            this.txtCampo2.DependingRS = null;
            this.txtCampo2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCampo2.ForeColor = System.Drawing.Color.Black;
            this.txtCampo2.Location = new System.Drawing.Point(22, 173);
            this.txtCampo2.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.txtCampo2.Multiline = true;
            this.txtCampo2.Name = "txtCampo2";
            this.txtCampo2.Order = 0;
            this.txtCampo2.ParentConn = null;
            this.txtCampo2.ParentDA = null;
            this.txtCampo2.PK = false;
            this.txtCampo2.Search = false;
            this.txtCampo2.Size = new System.Drawing.Size(281, 24);
            this.txtCampo2.Status = CommonTools.EnumStatus.ADDNEW;
            this.txtCampo2.TabIndex = 5;
            this.txtCampo2.Upp = false;
            this.txtCampo2.Value = "";
            // 
            // fTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 347);
            this.Controls.Add(this.txtCampo2);
            this.Controls.Add(this.txtCampo1);
            this.Controls.Add(this.txtIdreg);
            this.Controls.Add(this.CTLM);
            this.Name = "fTest";
            this.Text = "fTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet CTLM;
        private EspackFormControls.EspackTextBox txtIdreg;
        private EspackFormControls.EspackTextBox txtCampo1;
        private EspackFormControls.EspackTextBox txtCampo2;
    }
}