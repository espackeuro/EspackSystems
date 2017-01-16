using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using AccesoDatosNet;
using CommonTools;
using VSGrid;
using EspackControls;
using EspackFormControls;

namespace CTLMantenimientoNet
{
    public partial class CTLMantenimientoNet : ToolStrip
    {
        //private variables
        private EnumStatus mStatus;
        private List<EspackControl> mListItems = new List<EspackControl>();
        private DA mDA;
        private StatusStrip mDefaultStatusStrip = null;

        //Events
        public event EventHandler<CTLMEventArgs> BeforeButtonClick; //launched when one of the buttons is pressed but before the action
        public event EventHandler<CTLMEventArgs> AfterButtonClick; //lauched after the action when one of the buttons is pressed
        protected virtual void OnBeforeButtonClick(CTLMEventArgs e)
        {
            EventHandler<CTLMEventArgs> handler = BeforeButtonClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAfterButtonClick(CTLMEventArgs e)
        {
            EventHandler<CTLMEventArgs> handler = AfterButtonClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        //Properties
        public cAccesoDatosNet Conn //the connection object
        {
            get
            {
                return mDA.Conn;
            }
            set
            {
                mDA.Conn = value;
            }
        }
        //? i dont remember what is this for
        public ToolStripStatusLabel MsgStatusInfoLabel { get; set; }
        public ToolStripStatusLabel MsgStatusLabel { get; set; }
        public ToolStripProgressBar StatusBarProgress { get; set; }
        //Properties with SPs but in string format, we will use this when assigning a SP_Controls by its name, it will assign the properties to the Insert, Update and Delete commands of the mDA connection
        public string sSPAdd //Insert
        {
            set
            {
                if (value != "") mDA.sSPAdd = value;
            }
            get
            {
                return mDA.sSPAdd;
            }
        }

        public string sSPUpp//Update
        {
            set
            {
                if (value != "") mDA.sSPUpp = value;
            }
            get
            {
                return mDA.sSPUpp;
            }
        }

        public string sSPDel//Delete
        {
            set
            {
                if (value != "") mDA.sSPDel = value;
            }
            get
            {
                return mDA.sSPDel;
            }
        }

        public SP SPAdd
        {
            get
            {
                return mDA.InsertCommand;
            }
        }
        public SP SPUpp
        {
            get
            {
                return mDA.UpdateCommand;
            }
        }
        public SP SPDel
        {
            get
            {
                return mDA.DeleteCommand;
            }
        }
        //Property which holds the table name.
        public string DBTable { set; get; } //property to assign table name

        //Items List property, it holds all the Form items besides with extra ones needed 
        public List<EspackControl> CTLMItems
        {
            get
            {
                return mListItems;
            }
        }
        //ItemsPK List, it holds the PK items of the Items list
        public List<EspackControl> ItemsPK
        {
            get
            {
                return mListItems.Where(x => x.PK == true).ToList();
            }
        }

        public List<EspackControl> VsGrids
        {
            get
            {
                return mListItems.Where(x => x is CtlVSGrid).ToList();
            }
        }

        //ItemsDBField list of items with DBField
        public List<EspackControl> ItemsDBField
        {
            get
            {
                return mListItems.Where(x => x.DBField != "" && x.DBField != null).ToList();
            }
        }
        public List<EspackControl> ItemsOrdered
        {
            get
            {
                return mListItems.Where(x => x.Order != 0).OrderBy(o => Math.Abs(o.Order)).ToList();
            }
        }
        public List<EspackControl> ItemsFormControl
        {
            get
            {
                return mListItems.Where(x => !(x is EspackString)).ToList();
            }
        }

        public int RSPosition
        {
            get
            {
                return mDA.SelectRS.DS != null ? mDA.SelectRS.Index : 0;
            }
        }

        //public List<VSGrid.CtlVSGrid> VSGrids;

        //private int mRsPosition;
        //public int RSPosition { get; private set; } = -1;
        public void setRSPosition(int position)
        {
            mDA.SelectRS.Move(position, false);
            ShowRSValues();
        }
        //Query property, it constructs the SQL Query using the Items and the DBTable properties

        public string Query
        {
            get
            {
                string lSQL = "";
                List<string> lQueryFields = new List<string>();
                List<string> lWhereFields = new List<string>();
                List<string> lOrderFields = new List<string>();

                foreach (EspackControl Item in CTLMItems)
                {
                    string lDelimiter = CT.IsNumericType(Item.DBFieldType) ? "" : "'";
                    if (Item.DBField != "" && Item.DBField!= null)
                    {
                        lQueryFields.Add(Item.DBField);
                    }
                    if ((Item.PK || Item.Search) && Item.DBField != "")
                    {
                        string lValue = "@" + Item.DBField;
                        lWhereFields.Add("isnull("+Item.DBField + ",'') like  '%'+convert(varchar(max),isnull(" + lValue + ",''))+'%'");
                    }
                }
                foreach (EspackControl lItem in ItemsOrdered)
                {
                    lOrderFields.Add(lItem.DBField + (lItem.Order > 0 ? "" : " DESC"));
                }
                //construnt the select clause
                lSQL = "Select " + string.Join(", ", lQueryFields) + " from " + DBTable;
                //add the where clause
                string lWhere = string.Join(" and ", lWhereFields);
                lWhere = lWhere != "" ? " Where " + lWhere : "";
                lSQL = lSQL + lWhere;
                //add the order clause
                string lOrder = string.Join(", ", lOrderFields);
                lOrder = lOrder != "" ? " Order by " + lOrder : "";
                lSQL = lSQL + lOrder;

                return lSQL;
            }
        }

        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                //foreach (ToolStripItem item in Items)
                //{
                //    if (item.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
                //    {
                //        item.Enabled = value;
                //    }
                //}
                base.Enabled = value;
            }
        }
        //Status property, it enables or disables the buttons for each status



        public EnumStatus Status
        {

            get
            {
                return mStatus;
            }
        }
        // Clear property, if is true, after executing a procedure correctly, the form is cleared
        public bool Clear { set; get; }
        // Requery property, if its true, after executing a procedure correctly, the record is requeried
        public bool ReQuery { set; get; }
        //statusBar property, with the key shortcuts and the info panel
        public StatusStrip CTLStatusBar
        {
            get
            {
                return mDefaultStatusStrip;
            }
        }
        //public Form Parent { get; set; }

        //Constructor
        public CTLMantenimientoNet()
        {
            InitializeComponent();
            this.ItemClicked += CTLM_Click; // new System.EventHandler(CTLM_Click);
            this.AfterButtonClick += DefaultEventButtonClick;
            this.BeforeButtonClick += DefaultEventButtonClick;
            SetStatus(EnumStatus.SEARCH);
            mDA = new DA();
        }

        public void SetStatus(EnumStatus value)
        {
            mStatus = value;
            switch (value)
            {
                case EnumStatus.ADDNEW:
                    {
                        btnAdd.Enabled = false;
                        btnUpp.Enabled = false;
                        btnDel.Enabled = false;
                        btnSearch.Enabled = false;
                        btnFirst.Enabled = false;
                        btnPrev.Enabled = false;
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                        btnCancel.Enabled = true;
                        btnOk.Enabled = true;
                        break;
                    }
                case EnumStatus.EDIT:
                    {
                        btnAdd.Enabled = false;
                        btnUpp.Enabled = false;
                        btnDel.Enabled = false;
                        btnSearch.Enabled = true;
                        btnFirst.Enabled = false;
                        btnPrev.Enabled = false;
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                        btnCancel.Enabled = true;
                        btnOk.Enabled = true;
                        break;
                    }
                case EnumStatus.DELETE:
                    {
                        btnAdd.Enabled = false;
                        btnUpp.Enabled = false;
                        btnDel.Enabled = false;
                        btnSearch.Enabled = true;
                        btnFirst.Enabled = false;
                        btnPrev.Enabled = false;
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                        btnCancel.Enabled = true;
                        btnOk.Enabled = true;
                        break;
                    }
                case EnumStatus.SEARCH:
                    {
                        btnAdd.Enabled = true;
                        btnUpp.Enabled = false;
                        btnDel.Enabled = false;
                        btnSearch.Enabled = true;
                        btnFirst.Enabled = false;
                        btnPrev.Enabled = false;
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                        btnCancel.Enabled = true;
                        btnOk.Enabled = true;
                        break;
                    }
                case EnumStatus.NAVIGATE:
                    {
                        btnAdd.Enabled = false;
                        btnUpp.Enabled = true;
                        btnDel.Enabled = true;
                        btnSearch.Enabled = true;
                        btnFirst.Enabled = true;
                        btnPrev.Enabled = true;
                        btnNext.Enabled = true;
                        btnLast.Enabled = true;
                        btnCancel.Enabled = true;
                        btnOk.Enabled = true;
                        break;
                    }
                case EnumStatus.ADDGRIDLINE:
                case EnumStatus.EDITGRIDLINE:
                    {
                        btnAdd.Enabled = false;
                        btnUpp.Enabled = false;
                        btnDel.Enabled = false;
                        btnSearch.Enabled = false;
                        btnFirst.Enabled = false;
                        btnPrev.Enabled = false;
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                        btnCancel.Enabled = true;
                        btnOk.Enabled = false;
                        break;
                    }

            }
            ItemsFormControl.ForEach(i => i.Status = value);
            if (CTLStatusBar != null)
            {
                if (MsgStatusLabel != null)
                {
                    MsgStatusLabel.Text = value.ToString();
                }
            }
        }
        

        //Methods

        public void Start() //this method should be called always after the CTLM definition, it should be the last method to be called. 
        //1st it provides the SQL query to the DataAdapter and assigns the parameters to its SelectRS object
        //2nd it assigns the Type to each Item added.
        {
            KeyEnabler();
            mDA.SQL = Query;
            foreach (EspackControl Item in CTLMItems)
            {
                if (Item.PK || Item.Search)
                {
                    mDA.SelectRS.AddControlParameter("@" + Item.DBField, Item);
                }
                if (Item is EspackComboBox)
                {
                    Item.ClearEspackControl();
                }
            }
            foreach (EspackControl Item in ItemsDBField)
            {
                if (Item.DBFieldType!=null)
                    Item.DBFieldType = mDA.Schema[Item.DBField].DataType;
            }
            foreach (EspackControl VS in VsGrids)
            {
                ((CtlVSGrid)VS).MsgStatusLabel = MsgStatusInfoLabel;
                ((CtlVSGrid)VS).Start();
            }

        }
        //KeyEnabler, adds the parent_keydown method to the parent form keydown events
        public void KeyEnabler()
        {
            this.Parent.KeyDown += Parent_KeyDown;
        }
        //eventhandler
        private void Parent_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F8:
                    if (btnSearch.Enabled)
                    {
                        Button_Click("btnSearch");
                        Button_Click("btnOk");
                    }
                    break;
                case Keys.F2:
                    if (btnOk.Enabled)
                    {
                        Button_Click("btnOk");
                    }
                    break;
                case Keys.F7:
                    if (btnUpp.Enabled)
                    {
                        Button_Click("btnUpp");
                    }
                    break;
                case Keys.F10:
                    if (btnAdd.Enabled)
                    {
                        Button_Click("btnAdd");
                    }
                    break;
                case Keys.Escape:
                    Button_Click("btnCancel");
                    break;
            }
        }

        // Methods
        //Default event handler for CTLM
        private void DefaultEventButtonClick(object source, CTLMEventArgs e)
        {
            return;
        }

        //AddControl: adds a Control to the Control collection
        public void AddItem(object pDataContainer, string pDBField = "", bool pAdd = false, bool pUpp = false, bool pDel = false, int pOrder = 0, bool pPK = false,
            bool pSearch = false, object pDefValue = null, string pSPAddParamName = "", string pSPUppParamName = "", string pSPDelParamName = "")
        {
            EspackControl lControl;
            if (pDataContainer is string)
            {
                lControl = new EspackString() { Value = pDataContainer };
                lControl.EspackControlType = EspackControlTypeEnum.STATIC;
            }
            else
            {
                lControl = (EspackControl)pDataContainer;
                if (lControl is Control)
                    ((Control)lControl).TabIndex = CTLMItems.Count;
            }

            lControl.DBField = pDBField;
            if (pDBField != "")
            {
                lControl.Add = pAdd;
                lControl.Upp = pUpp;
                lControl.Del = pDel;
                lControl.Order = pOrder;
                lControl.PK = pPK;
                lControl.Search = pSearch;
                lControl.EspackControlType |= EspackControlTypeEnum.CTLM;
            }

            lControl.DefaultValue = pDefValue;
            if (lControl is EspackFormControl)
            {
                ((EspackFormControl)lControl).ParentDA = mDA;
                ((EspackFormControl)lControl).ParentConn = Conn;
            }
            mListItems.Add(lControl);
            if (lControl is Control && !(lControl is CtlVSGrid))
                ((Control)lControl).Enabled = (pAdd && Status == EnumStatus.ADDNEW) || (pUpp && Status == EnumStatus.EDIT) || (pDel && Status == EnumStatus.DELETE) || (pSearch && Status == EnumStatus.SEARCH);
            if (pAdd)
            {
                mDA.InsertCommand.AddControlParameter("@" + (pSPAddParamName == "" ? pDBField : pSPAddParamName), lControl);
            }
            if (pUpp)
            {
                mDA.UpdateCommand.AddControlParameter("@" + (pSPUppParamName == "" ? pDBField : pSPUppParamName), lControl);
            }
            if (pDel)
            {
                mDA.DeleteCommand.AddControlParameter("@" + (pSPDelParamName == "" ? pDBField : pSPDelParamName), lControl);
            }
        }
        //adds the status strip to the parent form.
        public void AddDefaultStatusStrip()
        {
            mDefaultStatusStrip = new StatusStrip();
            //SizeType lSize = new SizeType(118, 17);
            int lLabelWidth = 75;
            mDefaultStatusStrip.Items.Add(new ToolStripStatusLabel("F2: OK") { Width = lLabelWidth, AutoSize = false });
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
            mDefaultStatusStrip.Items.Add(new ToolStripStatusLabel("F7: Edit") { Width = lLabelWidth, AutoSize = false });
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
            mDefaultStatusStrip.Items.Add(new ToolStripStatusLabel("F8: Search") { Width = lLabelWidth, AutoSize = false });
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
            mDefaultStatusStrip.Items.Add(new ToolStripStatusLabel("F10: New") { Width = lLabelWidth, AutoSize = false });
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
            mDefaultStatusStrip.Items.Add(new ToolStripStatusLabel("ESC: Clear") { Width = lLabelWidth, AutoSize = false });
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
            MsgStatusInfoLabel = (new ToolStripStatusLabel("") { AutoSize = true });
            mDefaultStatusStrip.Items.Add(MsgStatusInfoLabel);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
            StatusBarProgress = new ToolStripProgressBar() { Size= new Size(100,mDefaultStatusStrip.Height), Visible=false};
            mDefaultStatusStrip.Items.Add(StatusBarProgress);
            mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
            MsgStatusLabel = (new ToolStripStatusLabel(string.Empty));
            //MsgStatusLabel.Padding= new Padding((int)(this.Size.Width - 10), 0, 0, 0);
            MsgStatusLabel.Alignment = ToolStripItemAlignment.Right;
            MsgStatusLabel.Spring = true;
            mDefaultStatusStrip.Items.Add(MsgStatusLabel);
            this.FindForm().Controls.Add(mDefaultStatusStrip);
            this.FindForm().KeyPreview = true;
        }
        public void StatusBarProgressMarqueeStart()
        {
            StatusBarProgress.Style = ProgressBarStyle.Marquee;
            StatusBarProgress.Value = 0;
            StatusBarProgress.Minimum = 0;
            StatusBarProgress.Maximum = 50;
            StatusBarProgress.MarqueeAnimationSpeed = 50;
            StatusBarProgress.Visible = true;
        }
        public void StatusBarProgressStop()
        {
            StatusBarProgress.Style = ProgressBarStyle.Continuous;
            StatusBarProgress.MarqueeAnimationSpeed = 0;
            StatusBarProgress.Visible = false;
        }
        //ShowRSValues shows the actual values of actual record of mDA.SelectRS  
        delegate Task ShowRSValuesDelegate();
        delegate Task ClearValuesDelegate(bool PK = false);
        //public async Task ShowRSValues()
        //{
        //    foreach (EspackControl lItem in CTLMItems)
        //    {
        //        await lItem.UpdateEspackControl();
        //    }
        //}
        //public async Task ShowRSValues()
        //{
        //    try
        //    {
        //        foreach (EspackControl lItem in CTLMItems)
        //        {
        //            if (lItem is Control)
        //            {
        //                if (((Control)lItem).InvokeRequired)
        //                {
        //                    ShowRSValuesDelegate a = new ShowRSValuesDelegate(ShowRSValues);
        //                    this.Invoke(a);
        //                }
        //                else
        //                {
        //                    await lItem.UpdateEspackControl();
        //                }
        //            }
        //            else
        //            {
        //                await lItem.UpdateEspackControl();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //ClearValues Cleans the form and fills it with the default values 
        public void ShowRSValues()
        {
            foreach (EspackControl lItem in CTLMItems)
                try
                {
                    lItem.UpdateEspackControl();
                } catch (Exception ex)
                {
                    MessageBox.Show(string.Format("ERROR in Item {0}: {1}", lItem.Name ,ex.Message), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
        public void ClearValues(bool PK = false)
        {
            //setRSPosition(0);
            if (PK)
                CTLMItems.Where(r => r.PK==false).ToList().ForEach(i => i.ClearEspackControl());
            else
                CTLMItems.ForEach(i => i.ClearEspackControl());
            if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "";
            
        }

        //What happens when you click OK
        private void Click_OK(string pButtonName = "btnOk")
        {
            switch (Status)
            {
                case EnumStatus.SEARCH:
                    {
                        mDA.SelectRS.Open();
                        if (mDA.SelectRS.EOF)
                        {
                            MessageBox.Show("No data found.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            OnAfterButtonClick(new CTLMEventArgs(pButtonName));
                            SetStatus(EnumStatus.SEARCH);
                            ClearValues();
                            return;
                        }
                        try
                        {
                            ShowRSValues();
                        } catch (Exception ex)
                        {
                            Console.Write(ex.Message);
                        }
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName));
                        SetStatus(EnumStatus.NAVIGATE);
                        if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = mDA.SelectRS.RecordCount.ToString() + " Records returned by search.";
                        break;
                    }
                case EnumStatus.ADDNEW:
                    {
                        if (mDA.Conn.State == ConnectionState.Open)
                            mDA.Conn.Close();
                        mDA.InsertCommand.Execute();
                        if (mDA.InsertCommand.LastMsg.Substring(0, 2) != "OK")
                        {
                            MessageBox.Show("ERROR:" + mDA.InsertCommand.LastMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "ERROR:" + mDA.InsertCommand.LastMsg;
                            return;
                        }
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName));
                        if (Clear)
                        {
                            ClearValues();
                        }
                        else
                        {
                            if (ItemsPK.Count == 1 && mDA.InsertCommand.LastMsg.Length > 2) //if the msg is longer that 2 for example "OK/12345" it assigns the right side to the first key control
                            {
                                ItemsPK[0].Value = mDA.InsertCommand.LastMsg.Substring(3);
                            }
                        }
                        if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "Record added successfully :D!";
                        if (ReQuery)
                        {
                            ClearValues(true);
                            SetStatus(EnumStatus.SEARCH);
                            Click_OK(pButtonName); ;
                        }

                        if (VsGrids.Count != 0)
                        {
                            SetStatus(EnumStatus.EDIT);
                            ((Control)VsGrids[0]).Focus();
                        }
                        else
                            SetStatus(EnumStatus.SEARCH);
                        break;
                    }
                case EnumStatus.EDIT:
                    {
                        if (mDA.Conn.State == ConnectionState.Open)
                            mDA.Conn.Close();
                        mDA.UpdateCommand.Execute();
                        if (mDA.UpdateCommand.LastMsg.Substring(0, 2) != "OK")
                        {
                            MessageBox.Show("ERROR:" + mDA.UpdateCommand.LastMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "ERROR:" + mDA.UpdateCommand.LastMsg;
                            return;
                        }
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName));
                        if (Clear)
                        {
                            ClearValues();
                        }
                        if (ReQuery)
                        {
                            ClearValues(true);
                            SetStatus(EnumStatus.SEARCH);
                            Click_OK(pButtonName); ;
                        }
                        SetStatus(EnumStatus.SEARCH);
                        if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "Record updated successfully ;D!";
                        break;
                    }
                case EnumStatus.DELETE:
                    {
                        if (mDA.Conn.State == ConnectionState.Open)
                            mDA.Conn.Close();
                        mDA.DeleteCommand.Execute();
                        if (mDA.DeleteCommand.LastMsg.Substring(0, 2) != "OK")
                        {
                            MessageBox.Show("ERROR:" + mDA.DeleteCommand.LastMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            StatusMsg("ERROR:" + mDA.DeleteCommand.LastMsg);
                            return;
                        }
                        ClearValues();
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName));

                        SetStatus(EnumStatus.SEARCH);
                        if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "Record deleted successfully X(!";
                        break;
                    }
            }
        }

        public void StatusMsg(string lMsg)
        {
            if (CTLStatusBar != null)
            {
                if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = lMsg;
            }
        }

        private void Button_Click(string pButtonName) //lauched when clicked any button
        {
            try
            {
                CTLMEventArgs lCTLMEventArgs = new CTLMEventArgs(pButtonName);
                OnBeforeButtonClick(lCTLMEventArgs); //launched BeforeButtonClick Event
                if (lCTLMEventArgs.Cancel) { return; }
                switch (pButtonName)
                {
                    case "btnAdd":
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName)); //Launched AfterButtonClick Event
                        SetStatus( EnumStatus.ADDNEW);
                        break;
                    case "btnUpp":
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName)); //Launched AfterButtonClick Event
                        SetStatus( EnumStatus.EDIT);
                        break;
                    case "btnDel":
                        if (MessageBox.Show("This will delete the actual record. Are you sure?", "WARNING", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            SetStatus( EnumStatus.DELETE);
                            Click_OK(pButtonName);
                        }
                        break;
                    case "btnSearch":
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName));
                        SetStatus( EnumStatus.SEARCH);
                        break;
                    case "btnCancel":
                        ClearValues();
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName));
                        SetStatus( EnumStatus.SEARCH);
                        break;
                    case "btnOk":
                        Click_OK(pButtonName);
                        break;
                    case "btnNext":
                        if (mDA.SelectRS.State == RSState.Open)
                        {
                            try
                            {
                                setRSPosition(RSPosition + 1);
                            } catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName));
                        break;
                    case "btnPrev":
                        if (mDA.SelectRS.State == RSState.Open)
                        {
                            try
                            {
                                setRSPosition(RSPosition - 1);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName));
                        break;
                    case "btnFirst":
                        if (mDA.SelectRS.State == RSState.Open)
                        {
                            try
                            {
                                setRSPosition(0);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName));
                        break;
                    case "btnLast":
                        if (mDA.SelectRS.State == RSState.Open)
                        {
                            try
                            {
                                setRSPosition(mDA.SelectRS.RecordCount - 1);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        OnAfterButtonClick(new CTLMEventArgs(pButtonName));
                        break;
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //eventhandler 
        private void CTLM_Click(object sender, ToolStripItemClickedEventArgs e)  //the Click Event
        {
            Button_Click(e.ClickedItem.Name);
        }

    }

    //Events for the control
    public class CTLMEventArgs : EventArgs
    {
        private string mButtonClick;
        public string ButtonClick
        {
            get
            {
                return mButtonClick;
            }
        }
        public bool Cancel { get; set; }
        public CTLMEventArgs(string pButtonClick)
        {
            mButtonClick = pButtonClick;
            Cancel = false;
        }
    }
}
