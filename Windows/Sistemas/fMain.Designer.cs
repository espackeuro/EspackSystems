namespace Sistemas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuNetwork = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTowns = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuZones = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItems = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dHCPControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aliasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnMaster = new System.Windows.Forms.ToolStripButton();
            this.dNSControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNetwork,
            this.actionsToolStripMenuItem,
            this.tasksToolStripMenuItem,
            this.controlToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(9, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(520, 25);
            this.menuStrip1.Stretch = false;
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuNetwork
            // 
            this.mnuNetwork.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTowns,
            this.mnuZones,
            this.mnuItems});
            this.mnuNetwork.Name = "mnuNetwork";
            this.mnuNetwork.Size = new System.Drawing.Size(60, 19);
            this.mnuNetwork.Text = "Masters";
            // 
            // mnuTowns
            // 
            this.mnuTowns.Name = "mnuTowns";
            this.mnuTowns.Size = new System.Drawing.Size(156, 22);
            this.mnuTowns.Text = "Towns";
            this.mnuTowns.Click += new System.EventHandler(this.mnuTowns_Click);
            // 
            // mnuZones
            // 
            this.mnuZones.Name = "mnuZones";
            this.mnuZones.Size = new System.Drawing.Size(156, 22);
            this.mnuZones.Text = "Network Zones";
            this.mnuZones.Click += new System.EventHandler(this.mnuZones_Click);
            // 
            // mnuItems
            // 
            this.mnuItems.Name = "mnuItems";
            this.mnuItems.Size = new System.Drawing.Size(156, 22);
            this.mnuItems.Text = "Inventory Items";
            this.mnuItems.Click += new System.EventHandler(this.mnuItems_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dHCPControlToolStripMenuItem,
            this.dNSControlToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 19);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // dHCPControlToolStripMenuItem
            // 
            this.dHCPControlToolStripMenuItem.Name = "dHCPControlToolStripMenuItem";
            this.dHCPControlToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dHCPControlToolStripMenuItem.Text = "DHCP Control";
            this.dHCPControlToolStripMenuItem.Click += new System.EventHandler(this.dHCPControlToolStripMenuItem_Click);
            // 
            // tasksToolStripMenuItem
            // 
            this.tasksToolStripMenuItem.Name = "tasksToolStripMenuItem";
            this.tasksToolStripMenuItem.Size = new System.Drawing.Size(47, 19);
            this.tasksToolStripMenuItem.Text = "Tasks";
            this.tasksToolStripMenuItem.Click += new System.EventHandler(this.tasksToolStripMenuItem_Click);
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flagsToolStripMenuItem,
            this.servicesToolStripMenuItem1,
            this.usersToolStripMenuItem1,
            this.aliasToolStripMenuItem,
            this.securityProfilesToolStripMenuItem});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(59, 19);
            this.controlToolStripMenuItem.Text = "Control";
            // 
            // flagsToolStripMenuItem
            // 
            this.flagsToolStripMenuItem.Name = "flagsToolStripMenuItem";
            this.flagsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.flagsToolStripMenuItem.Text = "Flags";
            this.flagsToolStripMenuItem.Click += new System.EventHandler(this.flagsToolStripMenuItem_Click);
            // 
            // servicesToolStripMenuItem1
            // 
            this.servicesToolStripMenuItem1.Name = "servicesToolStripMenuItem1";
            this.servicesToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.servicesToolStripMenuItem1.Text = "Services";
            this.servicesToolStripMenuItem1.Click += new System.EventHandler(this.servicesToolStripMenuItem1_Click);
            // 
            // usersToolStripMenuItem1
            // 
            this.usersToolStripMenuItem1.Name = "usersToolStripMenuItem1";
            this.usersToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.usersToolStripMenuItem1.Text = "Users";
            this.usersToolStripMenuItem1.Click += new System.EventHandler(this.usersToolStripMenuItem1_Click);
            // 
            // aliasToolStripMenuItem
            // 
            this.aliasToolStripMenuItem.Name = "aliasToolStripMenuItem";
            this.aliasToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.aliasToolStripMenuItem.Text = "Alias";
            this.aliasToolStripMenuItem.Click += new System.EventHandler(this.aliasToolStripMenuItem_Click);
            // 
            // securityProfilesToolStripMenuItem
            // 
            this.securityProfilesToolStripMenuItem.Name = "securityProfilesToolStripMenuItem";
            this.securityProfilesToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.securityProfilesToolStripMenuItem.Text = "Security Profiles";
            this.securityProfilesToolStripMenuItem.Click += new System.EventHandler(this.securityProfilesToolStripMenuItem_Click);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1256, 39);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(1256, 39);
            this.toolStripContainer1.TabIndex = 6;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMaster});
            this.toolStrip1.Location = new System.Drawing.Point(1227, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(29, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnMaster
            // 
            this.btnMaster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMaster.Image = global::Sistemas.Properties.Resources.lock_24;
            this.btnMaster.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnMaster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMaster.Name = "btnMaster";
            this.btnMaster.Size = new System.Drawing.Size(26, 28);
            this.btnMaster.Text = "btnMaster";
            this.btnMaster.ToolTipText = "Master status";
            this.btnMaster.Click += new System.EventHandler(this.btnMaster_Click);
            // 
            // dNSControlToolStripMenuItem
            // 
            this.dNSControlToolStripMenuItem.Name = "dNSControlToolStripMenuItem";
            this.dNSControlToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dNSControlToolStripMenuItem.Text = "DNS Control";
            this.dNSControlToolStripMenuItem.Click += new System.EventHandler(this.dNSControlToolStripMenuItem_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 721);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "fMain";
            this.Text = "Sistemas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuNetwork;
        private System.Windows.Forms.ToolStripMenuItem mnuTowns;
        private System.Windows.Forms.ToolStripMenuItem mnuZones;
        private System.Windows.Forms.ToolStripMenuItem mnuItems;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dHCPControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnMaster;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flagsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aliasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityProfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dNSControlToolStripMenuItem;
    }
}

