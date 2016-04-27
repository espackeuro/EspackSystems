namespace CTLMantenimientoNet
{
    partial class CTLMantenimientoNet 
    {
        private void InitializeComponent()
        {
            //this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = Properties.Resources.ResourceManager; //new System.ComponentModel.ComponentResourceManager(typeof(CTLMantenimientoNet));
            //this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.btnAdd = new System.Windows.Forms.ToolStripButton()
            {
                DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                Enabled = false,
                Image = Properties.Resources.add24,
                Name = "btnAdd",
                Size = new System.Drawing.Size(26, 26),
                Text = "Add a new element."
            };
            this.btnUpp = new System.Windows.Forms.ToolStripButton()
            {
                DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                Enabled = false,
                Image = Properties.Resources.edit24,
                Name = "btnUpp",
                Size = new System.Drawing.Size(26, 26),
                Text = "Edit current element."
            };
            this.btnDel = new System.Windows.Forms.ToolStripButton()
            {
                DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                Enabled = false,
                Image = Properties.Resources.del24,
                Name = "btnDel",
                Size = new System.Drawing.Size(26, 26),
                Text = "Delete current element."
            };
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripButton()
            {
                DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                Enabled = false,
                Image = Properties.Resources.search24,
                Name = "btnSearch",
                Size = new System.Drawing.Size(26, 26),
                Text = "Search."
            };
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFirst = new System.Windows.Forms.ToolStripButton()
            {
                DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                Enabled = false,
                Image = Properties.Resources.first24,
                Name = "btnFirst",
                Size = new System.Drawing.Size(26, 26),
                Text = "First Element."
            };
            this.btnPrev = new System.Windows.Forms.ToolStripButton()
            {
                DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                Enabled = false,
                Image = Properties.Resources.prev24,
                Name = "btnPrev",
                Size = new System.Drawing.Size(26, 26),
                Text = "Previous Element."
            };
            this.btnNext = new System.Windows.Forms.ToolStripButton()
            {
                DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                Enabled = false,
                Image = Properties.Resources.next24,
                Name = "btnNext",
                Size = new System.Drawing.Size(26, 26),
                Text = "Next Element."
            };
            this.btnLast = new System.Windows.Forms.ToolStripButton()
            {
                DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                Enabled = false,
                Image = Properties.Resources.last24,
                Name = "btnLast",
                Size = new System.Drawing.Size(26, 26),
                Text = "Last Element."
            };
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancel = new System.Windows.Forms.ToolStripButton()
            {
                DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                Enabled = false,
                Image = Properties.Resources.cancel24,
                Name = "btnCancel",
                Size = new System.Drawing.Size(26, 26),
                Text = "Clear / Cancel."
            };
            this.btnOk = new System.Windows.Forms.ToolStripButton()
            {
                DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                Enabled = false,
                Image = Properties.Resources.ok24,
                Name = "btnOk",
                Size = new System.Drawing.Size(26, 26),
                Text = "Confirm action."
            };
            this.Dock = System.Windows.Forms.DockStyle.None;
            this.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnUpp,
            this.btnDel,
            this.toolStripSeparator3,
            this.btnSearch,
            this.toolStripSeparator1,
            this.btnFirst,
            this.btnPrev,
            this.btnNext,
            this.btnLast,
            this.toolStripSeparator2,
            this.btnCancel,
            this.btnOk});
        }
        //private System.ComponentModel.IContainer components = null;
        //private System.Windows.Forms.ImageList imgList;
        //private System.Windows.Forms.ToolStripContainer tsContainer;
        //private System.Windows.Forms.ToolStrip CTLStrip;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnUpp;
        private System.Windows.Forms.ToolStripButton btnDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPrev;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnOk;
        private System.Windows.Forms.ToolStripButton btnCancel;
    }
}
