namespace Simplistica
{
    partial class fMainSimplistica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMainSimplistica));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.receivalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleReceivalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printRepairsUnitLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printRackLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.receivalsToolStripMenuItem,
            this.toolsToolStripMenuItem});
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
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.printRepairsUnitLabelsToolStripMenuItem,
            this.printRackLabelsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // printRepairsUnitLabelsToolStripMenuItem
            // 
            this.printRepairsUnitLabelsToolStripMenuItem.Name = "printRepairsUnitLabelsToolStripMenuItem";
            this.printRepairsUnitLabelsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.printRepairsUnitLabelsToolStripMenuItem.Text = "Print Repairs Unit Labels";
            this.printRepairsUnitLabelsToolStripMenuItem.Click += new System.EventHandler(this.printRepairsUnitLabelsToolStripMenuItem_Click);
            // 
            // printRackLabelsToolStripMenuItem
            // 
            this.printRackLabelsToolStripMenuItem.Name = "printRackLabelsToolStripMenuItem";
            this.printRackLabelsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.printRackLabelsToolStripMenuItem.Text = "Print Rack Labels";
            this.printRackLabelsToolStripMenuItem.Click += new System.EventHandler(this.printRackLabelsToolStripMenuItem_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fMain";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem receivalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleReceivalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printRepairsUnitLabelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printRackLabelsToolStripMenuItem;
    }
}

