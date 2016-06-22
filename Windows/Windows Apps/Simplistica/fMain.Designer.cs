namespace Simplistica
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.receivalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleReceivalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.receivalsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // receivalsToolStripMenuItem
            // 
            this.receivalsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simpleReceivalsToolStripMenuItem});
            this.receivalsToolStripMenuItem.Name = "receivalsToolStripMenuItem";
            this.receivalsToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.receivalsToolStripMenuItem.Text = "Receivals";
            // 
            // simpleReceivalsToolStripMenuItem
            // 
            this.simpleReceivalsToolStripMenuItem.Name = "simpleReceivalsToolStripMenuItem";
            this.simpleReceivalsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.simpleReceivalsToolStripMenuItem.Text = "Simple Receivals";
            this.simpleReceivalsToolStripMenuItem.Click += new System.EventHandler(this.simpleReceivalsToolStripMenuItem_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fMain";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem receivalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleReceivalsToolStripMenuItem;
    }
}

