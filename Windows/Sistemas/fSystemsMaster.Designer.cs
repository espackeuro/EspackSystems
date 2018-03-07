namespace Sistemas
{
    partial class fSystemsMaster
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
            this.ctlMantenimientoNet1 = new CTLMantenimientoNet.CTLMantenimientoNet();
            this.SuspendLayout();
            // 
            // ctlMantenimientoNet1
            // 
            this.ctlMantenimientoNet1.Clear = false;
            this.ctlMantenimientoNet1.Conn = null;
            this.ctlMantenimientoNet1.DBTable = null;
            this.ctlMantenimientoNet1.Dock = System.Windows.Forms.DockStyle.None;
            this.ctlMantenimientoNet1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.ctlMantenimientoNet1.Location = new System.Drawing.Point(13, 13);
            this.ctlMantenimientoNet1.MsgStatusInfoLabel = null;
            this.ctlMantenimientoNet1.MsgStatusLabel = null;
            this.ctlMantenimientoNet1.Name = "ctlMantenimientoNet1";
            this.ctlMantenimientoNet1.ReQuery = false;
            this.ctlMantenimientoNet1.Size = new System.Drawing.Size(290, 29);
            this.ctlMantenimientoNet1.sSPAdd = "";
            this.ctlMantenimientoNet1.sSPDel = "";
            this.ctlMantenimientoNet1.sSPUpp = "";
            this.ctlMantenimientoNet1.StatusBarProgress = null;
            this.ctlMantenimientoNet1.TabIndex = 0;
            this.ctlMantenimientoNet1.Text = "ctlMantenimientoNet1";
            // 
            // fSystemsMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 579);
            this.Controls.Add(this.ctlMantenimientoNet1);
            this.Name = "fSystemsMaster";
            this.Text = "fSystemsMaster";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CTLMantenimientoNet.CTLMantenimientoNet ctlMantenimientoNet1;
    }
}