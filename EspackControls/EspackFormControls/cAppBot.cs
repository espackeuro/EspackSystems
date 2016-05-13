using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesoDatosNet;
using System.Drawing;
using static System.Windows.Forms.ListViewItem;
using System.Collections;

namespace EspackFormControls
{
    public class cAppBot: Control
    {
        public string Code { get; set; }
        private string Description { get; set; }
        private string DataBase { get; set; }
        private cServer DBServer { get; set; }
        private cServer ShareServer { get; set; }
        private string ExeName { get; set; }
        private cAccesoDatosNet Conn { get; set; }
        public GroupBox grpApp { get; set; }
        public PictureBox pctApp { get; set; }
        public ProgressBar prgApp { get; set; }
        public Label lblDescriptionApp { get; set; }


        public new Size Size
        {
            get
            {
                if (grpApp != null)
                    return grpApp.Size;
                else
                    return new Size(0, 0);
            }
            set
            {
                if (grpApp != null)
                    grpApp.Size = value;

            }
        }

        public static int GROUP_HEIGHT = 150;
        public static int GROUP_WIDTH = 150;

        public static int PROGRESS_PADDING = 10;
        public static int PROGRESS_HEIGHT= 40;
        public static int PROGRESS_WIDTH = GROUP_WIDTH- (PROGRESS_PADDING*2);

        public static int DESCRIPTION_PADDING = PROGRESS_PADDING;
        public static int DESCRIPTION_HEIGHT = 40;
        public static int DESCRIPTION_WIDTH = GROUP_WIDTH - (DESCRIPTION_PADDING * 2);

        public static int PICTURE_PADDING = PROGRESS_PADDING;
        public static int PICTURE_HEIGHT = GROUP_HEIGHT - DESCRIPTION_PADDING - DESCRIPTION_HEIGHT- PICTURE_PADDING * 2;
        public static int PICTURE_WIDTH = GROUP_WIDTH - PICTURE_PADDING * 2;

        public cAppBot(string pCode,string pDescription,string pDatabase, string pExeName, string ServiceZone, cServer pDBServer, cServer pShareServer)
            : this()
        {
            Code = pCode;
            Description = pDescription;
            DataBase = pDatabase;
            ExeName = pExeName;

            //
            DBServer = pDBServer;
            ShareServer = pShareServer;
        }

        public cAppBot()
        {
            
            grpApp = new GroupBox() { Size = new Size(GROUP_WIDTH, GROUP_HEIGHT), Location= new Point(0,0) };
            pctApp = new PictureBox();
            prgApp = new ProgressBar() { Size =new Size(PROGRESS_WIDTH, PROGRESS_HEIGHT), Location=new Point(PROGRESS_PADDING, (GROUP_HEIGHT/2)-(PROGRESS_HEIGHT/2)) };
            lblDescriptionApp = new Label() { Size = new Size(DESCRIPTION_WIDTH, DESCRIPTION_HEIGHT), Location = new Point(DESCRIPTION_PADDING, GROUP_HEIGHT - DESCRIPTION_HEIGHT - DESCRIPTION_PADDING) };
            pctApp = new PictureBox() { Size = new Size(PICTURE_WIDTH, PICTURE_HEIGHT), Location = new Point(PICTURE_PADDING, PICTURE_PADDING) };
#if DEBUG
            pctApp.BorderStyle = BorderStyle.FixedSingle;
            lblDescriptionApp.BorderStyle = BorderStyle.FixedSingle;
#endif
            pctApp.Image = Properties.Resources.Prototype_80;
            pctApp.SizeMode = PictureBoxSizeMode.CenterImage;

            grpApp.Controls.Add(prgApp);
            grpApp.Controls.Add(pctApp);
            grpApp.Controls.Add(lblDescriptionApp);
            MaximumSize = Size;
            MinimumSize = Size;
            this.Controls.Add(grpApp);
            
        }

        protected override void OnResize(EventArgs e)
        {
            this.Size = new Size(GROUP_WIDTH, GROUP_HEIGHT);
        }
        //protected override void OnParentChanged(EventArgs e)
        //{
        //    if (Parent != null)
        //    {
        //        Parent.Controls.Add(grpApp);
        //        base.OnParentChanged(e);
        //    }
        //}
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }

    // Class cAppList
    public class cAppList: IEnumerable<cAppBot>
    {
        private List<cAppBot> AppList { get; set; } = new List<cAppBot>();
        public cAppBot this[string pCode]
        {
            get
            {
                return AppList.FirstOrDefault(x => x.Code == pCode);
            }
            
        }
        public cAppBot this[int pKey]
        {
            get
            {
                return AppList[pKey];
            }

        }
        public int Count
        {
            get
            {
                return AppList.Count;
            }
        }
        public void Add(cAppBot pApp)
        {
            AppList.Add(pApp);
        }

        public IEnumerator<cAppBot> GetEnumerator()
        {
            return ((IEnumerable<cAppBot>)AppList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<cAppBot>)AppList).GetEnumerator();
        }
    }

}
