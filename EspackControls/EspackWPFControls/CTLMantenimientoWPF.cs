using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommonTools;
using EspackControls;
using EspackWPFControls;
using AccesoDatosNet;
using System.Windows.Controls.Primitives;

namespace EspackWPFControls
{
    //class CTLMantenimientoWPF
    //public partial class CTLMantenimientoWPF : ToolBarTray
    //{
    //    //private variables
    //    private EnumStatus mStatus;
    //    private List<EspackControl> mListItems = new List<EspackControl>();
    //    private DA mDA;
    //    private StatusBar mDefaultStatusStrip = null;

    //    //Events
    //    public event EventHandler<CTLMEventArgs> BeforeButtonClick; //launched when one of the buttons is pressed but before the action
    //    public event EventHandler<CTLMEventArgs> AfterButtonClick; //lauched after the action when one of the buttons is pressed

    //    //Properties
    //    public cAccesoDatosNet Conn //the connection object
    //    {
    //        get
    //        {
    //            return mDA.Conn;
    //        }
    //        set
    //        {
    //            mDA.Conn = value;
    //        }
    //    }
    //    //? i dont remember what is this for
    //    public TextBlock MsgStatusInfoLabel { get; set; }
    //    public TextBlock MsgStatusLabel { get; set; }

    //    //Properties with SPs but in string format, we will use this when assigning a SP by its name, it will assign the properties to the Insert, Update and Delete commands of the mDA connection
    //    public string sSPAdd //Insert
    //    {
    //        set
    //        {
    //            if (value != "") mDA.sSPAdd = value;
    //        }
    //        get
    //        {
    //            return mDA.sSPAdd;
    //        }
    //    }

    //    public string sSPUpp//Update
    //    {
    //        set
    //        {
    //            if (value != "") mDA.sSPUpp = value;
    //        }
    //        get
    //        {
    //            return mDA.sSPUpp;
    //        }
    //    }

    //    public string sSPDel//Delete
    //    {
    //        set
    //        {
    //            if (value != "") mDA.sSPDel = value;
    //        }
    //        get
    //        {
    //            return mDA.sSPDel;
    //        }
    //    }
    //    //Property which holds the table name.
    //    public string DBTable { set; get; } //property to assign table name

    //    //Items List property, it holds all the Form items besides with extra ones needed 
    //    public List<EspackControl> CTLMItems
    //    {
    //        get
    //        {
    //            return mListItems;
    //        }
    //    }
    //    //ItemsPK List, it holds the PK items of the Items list
    //    public List<EspackControl> ItemsPK
    //    {
    //        get
    //        {
    //            return mListItems.Where(x => x.PK == true).ToList();
    //        }
    //    }
    //    //Uncomment when finished
    //    //public List<EspackControl> VsGrids
    //    //{
    //    //    get
    //    //    {
    //    //        return mListItems.Where(x => x is CtlVSGrid).ToList();
    //    //    }
    //    //}

    //    //ItemsDBField list of items with DBField
    //    public List<EspackControl> ItemsDBField
    //    {
    //        get
    //        {
    //            return mListItems.Where(x => x.DBField != "" && x.DBField != null).ToList();
    //        }
    //    }
    //    public List<EspackControl> ItemsOrdered
    //    {
    //        get
    //        {
    //            return mListItems.Where(x => x.Order != 0).OrderBy(o => Math.Abs(o.Order)).ToList();
    //        }
    //    }
    //    public List<EspackControl> ItemsFormControl
    //    {
    //        get
    //        {
    //            return mListItems.Where(x => !(x is EspackString)).ToList();
    //        }
    //    }


    //    //public List<VSGrid.CtlVSGrid> VSGrids;

    //    private int mRsPosition;
    //    public int RSPosition
    //    {
    //        get
    //        {
    //            return mRsPosition;
    //        }
    //        set
    //        {
    //            if (value == -1)
    //            {
    //                mRsPosition = value;
    //                return;
    //            }
    //            try
    //            {
    //                if (mDA.SelectRS.DS != null)
    //                {
    //                    if (value <= mDA.SelectRS.RecordCount)
    //                    {
    //                        mDA.SelectRS.Move(value);
    //                        ShowRSValues();
    //                        mRsPosition = value;
    //                    }
    //                    else
    //                    {
    //                        throw new Exception("Wrong position value :(.");
    //                    }

    //                }
    //                else
    //                {
    //                    throw new Exception("Search is empty :(.");
    //                }
    //            }
    //            catch (Exception e)
    //            {
    //                throw new Exception("Error: " + e.Message);
    //            }
    //        }
    //    }
    //    //Query property, it constructs the SQL Query using the Items and the DBTable properties

    //    public string Query
    //    {
    //        get
    //        {
    //            string lSQL = "";
    //            List<string> lQueryFields = new List<string>();
    //            List<string> lWhereFields = new List<string>();
    //            List<string> lOrderFields = new List<string>();

    //            foreach (EspackControl Item in CTLMItems)
    //            {
    //                string lDelimiter = CT.IsNumericType(Item.DBFieldType) ? "" : "'";
    //                if (Item.DBField != "" && Item.DBField != null)
    //                {
    //                    lQueryFields.Add(Item.DBField);
    //                }
    //                if ((Item.PK || Item.Search) && Item.DBField != "")
    //                {
    //                    string lValue = "@" + Item.DBField;
    //                    lWhereFields.Add("isnull(" + Item.DBField + ",'') like  '%'+convert(varchar(max),isnull(" + lValue + ",''))+'%'");
    //                }
    //            }
    //            foreach (EspackControl lItem in ItemsOrdered)
    //            {
    //                lOrderFields.Add(lItem.DBField + (lItem.Order > 0 ? "" : " DESC"));
    //            }
    //            //construnt the select clause
    //            lSQL = "Select " + string.Join(", ", lQueryFields) + " from " + DBTable;
    //            //add the where clause
    //            string lWhere = string.Join(" and ", lWhereFields);
    //            lWhere = lWhere != "" ? " Where " + lWhere : "";
    //            lSQL = lSQL + lWhere;
    //            //add the order clause
    //            string lOrder = string.Join(", ", lOrderFields);
    //            lOrder = lOrder != "" ? " Order by " + lOrder : "";
    //            lSQL = lSQL + lOrder;

    //            return lSQL;
    //        }
    //    }

    //    public new bool Enabled
    //    {
    //        get
    //        {
    //            return base.Enabled;
    //        }
    //        set
    //        {
    //            Enabled = value;
    //            foreach (ToolStripItem item in Items)
    //            {
    //                if (item.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
    //                {
    //                    item.Enabled = value;
    //                }
    //            }
    //            base.Enabled = value;
    //        }
    //    }
    //    //Status property, it enables or disables the buttons for each status


    //    public EnumStatus Status
    //    {
    //        set
    //        {
    //            mStatus = value;
    //            switch (value)
    //            {
    //                case EnumStatus.ADDNEW:
    //                    {
    //                        btnAdd.Enabled = false;
    //                        btnUpp.Enabled = false;
    //                        btnDel.Enabled = false;
    //                        btnSearch.Enabled = false;
    //                        btnFirst.Enabled = false;
    //                        btnPrev.Enabled = false;
    //                        btnNext.Enabled = false;
    //                        btnLast.Enabled = false;
    //                        btnCancel.Enabled = true;
    //                        btnOk.Enabled = true;
    //                        break;
    //                    }
    //                case EnumStatus.EDIT:
    //                    {
    //                        btnAdd.Enabled = false;
    //                        btnUpp.Enabled = false;
    //                        btnDel.Enabled = false;
    //                        btnSearch.Enabled = true;
    //                        btnFirst.Enabled = false;
    //                        btnPrev.Enabled = false;
    //                        btnNext.Enabled = false;
    //                        btnLast.Enabled = false;
    //                        btnCancel.Enabled = true;
    //                        btnOk.Enabled = true;
    //                        break;
    //                    }
    //                case EnumStatus.DELETE:
    //                    {
    //                        btnAdd.Enabled = false;
    //                        btnUpp.Enabled = false;
    //                        btnDel.Enabled = false;
    //                        btnSearch.Enabled = true;
    //                        btnFirst.Enabled = false;
    //                        btnPrev.Enabled = false;
    //                        btnNext.Enabled = false;
    //                        btnLast.Enabled = false;
    //                        btnCancel.Enabled = true;
    //                        btnOk.Enabled = true;
    //                        break;
    //                    }
    //                case EnumStatus.SEARCH:
    //                    {
    //                        btnAdd.Enabled = true;
    //                        btnUpp.Enabled = false;
    //                        btnDel.Enabled = false;
    //                        btnSearch.Enabled = true;
    //                        btnFirst.Enabled = false;
    //                        btnPrev.Enabled = false;
    //                        btnNext.Enabled = false;
    //                        btnLast.Enabled = false;
    //                        btnCancel.Enabled = true;
    //                        btnOk.Enabled = true;
    //                        break;
    //                    }
    //                case EnumStatus.NAVIGATE:
    //                    {
    //                        btnAdd.Enabled = false;
    //                        btnUpp.Enabled = true;
    //                        btnDel.Enabled = true;
    //                        btnSearch.Enabled = true;
    //                        btnFirst.Enabled = true;
    //                        btnPrev.Enabled = true;
    //                        btnNext.Enabled = true;
    //                        btnLast.Enabled = true;
    //                        btnCancel.Enabled = true;
    //                        btnOk.Enabled = true;
    //                        break;
    //                    }
    //                case EnumStatus.ADDGRIDLINE:
    //                case EnumStatus.EDITGRIDLINE:
    //                    {
    //                        btnAdd.Enabled = false;
    //                        btnUpp.Enabled = false;
    //                        btnDel.Enabled = false;
    //                        btnSearch.Enabled = false;
    //                        btnFirst.Enabled = false;
    //                        btnPrev.Enabled = false;
    //                        btnNext.Enabled = false;
    //                        btnLast.Enabled = false;
    //                        btnCancel.Enabled = true;
    //                        btnOk.Enabled = false;
    //                        break;
    //                    }

    //            }
    //            foreach (EspackControl lItem in ItemsFormControl)
    //            {
    //                lItem.Status = value;
    //            }
    //            if (CTLStatusBar != null)
    //            {
    //                if (MsgStatusLabel != null)
    //                {
    //                    MsgStatusLabel.Text = value.ToString();
    //                }
    //            }
    //        }
    //        get
    //        {
    //            return mStatus;
    //        }
    //    }
    //    // Clear property, if is true, after executing a procedure correctly, the form is cleared
    //    public bool Clear { set; get; }

    //    //statusBar property, with the key shortcuts and the info panel
    //    public StatusStrip CTLStatusBar
    //    {
    //        get
    //        {
    //            return mDefaultStatusStrip;
    //        }
    //    }
    //    //public Form Parent { get; set; }

    //    //Constructor
    //    public CTLMantenimientoWPF()
    //    {
    //        InitializeComponent();
    //        this.ItemClicked += CTLM_Click; // new System.EventHandler(CTLM_Click);
    //        this.AfterButtonClick += DefaultEventButtonClick;
    //        this.BeforeButtonClick += DefaultEventButtonClick;
    //        Status = EnumStatus.SEARCH;
    //        mDA = new DA();
    //        RSPosition = -1;

    //    }


    //    //Methods

    //    public void Start() //this method should be called always after the CTLM definition, it should be the last method to be called. 
    //    //1st it provides the SQL query to the DataAdapter and assigns the parameters to its SelectRS object
    //    //2nd it assigns the Type to each Item added.
    //    {
    //        KeyEnabler();
    //        mDA.SQL = Query;
    //        foreach (EspackControl Item in CTLMItems)
    //        {
    //            if (Item.PK || Item.Search)
    //            {
    //                mDA.SelectRS.AddControlParameter("@" + Item.DBField, Item);
    //            }
    //            if (Item is EspackComboBox)
    //            {
    //                Item.ClearEspackControl();
    //            }
    //        }
    //        foreach (EspackControl Item in ItemsDBField)
    //        {
    //            if (Item.DBFieldType != null)
    //                Item.DBFieldType = mDA.Schema[Item.DBField].DataType;
    //        }
    //        foreach (EspackControl VS in VsGrids)
    //        {
    //            ((CtlVSGrid)VS).MsgStatusLabel = MsgStatusInfoLabel;
    //            ((CtlVSGrid)VS).Start();
    //        }

    //    }
    //    //KeyEnabler, adds the parent_keydown method to the parent form keydown events
    //    public void KeyEnabler()
    //    {
    //        this.Parent.KeyDown += Parent_KeyDown;
    //    }

    //    void Parent_KeyDown(object sender, KeyEventArgs e)
    //    {
    //        switch (e.KeyCode)
    //        {
    //            case Keys.F8:
    //                if (btnSearch.Enabled)
    //                {
    //                    Button_Click("btnSearch");
    //                    Button_Click("btnOk");
    //                }
    //                break;
    //            case Keys.F2:
    //                if (btnOk.Enabled)
    //                {
    //                    Button_Click("btnOk");
    //                }
    //                break;
    //            case Keys.F7:
    //                if (btnUpp.Enabled)
    //                {
    //                    Button_Click("btnUpp");
    //                }
    //                break;
    //            case Keys.F10:
    //                if (btnAdd.Enabled)
    //                {
    //                    Button_Click("btnAdd");
    //                }
    //                break;
    //            case Keys.Escape:
    //                Button_Click("btnCancel");
    //                break;
    //        }
    //    }

    //    // Methods
    //    //Default event handler for CTLM
    //    private void DefaultEventButtonClick(object source, CTLMEventArgs e)
    //    {
    //        return;
    //    }

    //    //AddControl: adds a Control to the Control collection
    //    public void AddItem(object pDataContainer, string pDBField = "", bool pAdd = false, bool pUpp = false, bool pDel = false, int pOrder = 0, bool pPK = false,
    //        bool pSearch = false, object pDefValue = null, string pSPAddParamName = "", string pSPUppParamName = "", string pSPDelParamName = "")
    //    {
    //        EspackControl lControl;
    //        if (pDataContainer is string)
    //        {
    //            lControl = new EspackString() { Value = pDataContainer };
    //            lControl.EspackControlType = EspackControlTypeEnum.STATIC;
    //        }
    //        else
    //        {
    //            lControl = (EspackControl)pDataContainer;
    //            if (lControl is Control)
    //                ((Control)lControl).TabIndex = CTLMItems.Count;
    //        }

    //        lControl.DBField = pDBField;
    //        if (pDBField != "")
    //        {
    //            lControl.Add = pAdd;
    //            lControl.Upp = pUpp;
    //            lControl.Del = pDel;
    //            lControl.Order = pOrder;
    //            lControl.PK = pPK;
    //            lControl.Search = pSearch;
    //            lControl.EspackControlType |= EspackControlTypeEnum.CTLM;
    //        }

    //        lControl.DefaultValue = pDefValue;
    //        if (lControl is EspackFormControl)
    //        {
    //            ((EspackFormControl)lControl).ParentDA = mDA;
    //            ((EspackFormControl)lControl).ParentConn = Conn;
    //        }
    //        mListItems.Add(lControl);
    //        if (lControl is Control && !(lControl is CtlVSGrid))
    //            ((Control)lControl).Enabled = (pAdd && Status == EnumStatus.ADDNEW) || (pUpp && Status == EnumStatus.EDIT) || (pDel && Status == EnumStatus.DELETE) || (pSearch && Status == EnumStatus.SEARCH);
    //        if (pAdd)
    //        {
    //            mDA.InsertCommand.AddControlParameter("@" + (pSPAddParamName == "" ? pDBField : pSPAddParamName), lControl);
    //        }
    //        if (pUpp)
    //        {
    //            mDA.UpdateCommand.AddControlParameter("@" + (pSPUppParamName == "" ? pDBField : pSPUppParamName), lControl);
    //        }
    //        if (pDel)
    //        {
    //            mDA.DeleteCommand.AddControlParameter("@" + (pSPDelParamName == "" ? pDBField : pSPDelParamName), lControl);
    //        }
    //    }
    //    //adds the status strip to the parent form.
    //    public void AddDefaultStatusStrip()
    //    {
    //        mDefaultStatusStrip = new StatusStrip();
    //        //SizeType lSize = new SizeType(118, 17);
    //        int lLabelWidth = 75;
    //        mDefaultStatusStrip.Items.Add(new ToolStripStatusLabel("F2: OK") { Width = lLabelWidth, AutoSize = false });
    //        mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
    //        mDefaultStatusStrip.Items.Add(new ToolStripStatusLabel("F7: Edit") { Width = lLabelWidth, AutoSize = false });
    //        mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
    //        mDefaultStatusStrip.Items.Add(new ToolStripStatusLabel("F8: Search") { Width = lLabelWidth, AutoSize = false });
    //        mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
    //        mDefaultStatusStrip.Items.Add(new ToolStripStatusLabel("F10: New") { Width = lLabelWidth, AutoSize = false });
    //        mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
    //        mDefaultStatusStrip.Items.Add(new ToolStripStatusLabel("ESC: Clear") { Width = lLabelWidth, AutoSize = false });
    //        mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
    //        MsgStatusInfoLabel = (new ToolStripStatusLabel("") { AutoSize = true });
    //        mDefaultStatusStrip.Items.Add(MsgStatusInfoLabel);
    //        mDefaultStatusStrip.Items.Add(new ToolStripSeparator());
    //        MsgStatusLabel = (new ToolStripStatusLabel(string.Empty));
    //        //MsgStatusLabel.Padding= new Padding((int)(this.Size.Width - 10), 0, 0, 0);
    //        MsgStatusLabel.Alignment = ToolStripItemAlignment.Right;
    //        MsgStatusLabel.Spring = true;
    //        mDefaultStatusStrip.Items.Add(MsgStatusLabel);
    //        this.FindForm().Controls.Add(mDefaultStatusStrip);
    //    }

    //    //ShowRSValues shows the actual values of actual record of mDA.SelectRS  
    //    public void ShowRSValues()
    //    {
    //        foreach (EspackControl lItem in CTLMItems)
    //        {
    //            lItem.UpdateEspackControl();
    //        }
    //    }

    //    //ClearValues Cleans the form and fills it with the default values 
    //    public void ClearValues()
    //    {
    //        foreach (EspackControl lItem in ItemsFormControl)
    //        {
    //            lItem.ClearEspackControl();
    //        }
    //        if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "";
    //        mRsPosition = 0;
    //    }

    //    //What happens when you click OK
    //    private void Click_OK()
    //    {
    //        switch (Status)
    //        {
    //            case EnumStatus.SEARCH:
    //                {
    //                    mDA.SelectRS.Open();
    //                    if (mDA.SelectRS.EOF)
    //                    {
    //                        MessageBox.Show("No data found.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //                        Status = EnumStatus.SEARCH;
    //                        ClearValues();
    //                        return;
    //                    }
    //                    ShowRSValues();
    //                    Status = EnumStatus.NAVIGATE;
    //                    if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = mDA.SelectRS.RecordCount.ToString() + " Records returned by search.";
    //                    break;
    //                }
    //            case EnumStatus.ADDNEW:
    //                {
    //                    mDA.InsertCommand.Execute();
    //                    if (mDA.InsertCommand.LastMsg.Substring(0, 2) != "OK")
    //                    {
    //                        MessageBox.Show("ERROR:" + mDA.InsertCommand.LastMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //                        if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "ERROR:" + mDA.InsertCommand.LastMsg;
    //                        return;
    //                    }
    //                    if (Clear)
    //                    {
    //                        ClearValues();
    //                    }
    //                    else
    //                    {
    //                        if (ItemsPK.Count == 1 && mDA.InsertCommand.LastMsg.Length > 2) //if the msg is longer that 2 for example "OK/12345" it assigns the right side to the first key control
    //                        {
    //                            ItemsPK[0].Value = mDA.InsertCommand.LastMsg.Substring(3);
    //                        }
    //                    }
    //                    if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "Record added successfully :D!";
    //                    Status = VsGrids.Count != 0 ? EnumStatus.EDIT : EnumStatus.SEARCH;
    //                    break;
    //                }
    //            case EnumStatus.EDIT:
    //                {
    //                    mDA.UpdateCommand.Execute();
    //                    if (mDA.UpdateCommand.LastMsg.Substring(0, 2) != "OK")
    //                    {
    //                        MessageBox.Show("ERROR:" + mDA.UpdateCommand.LastMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //                        if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "ERROR:" + mDA.UpdateCommand.LastMsg;
    //                        return;
    //                    }
    //                    if (Clear)
    //                    {
    //                        ClearValues();
    //                    }
    //                    Status = EnumStatus.SEARCH;
    //                    if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "Record updated successfully ;D!";
    //                    break;
    //                }
    //            case EnumStatus.DELETE:
    //                {
    //                    mDA.DeleteCommand.Execute();
    //                    if (mDA.DeleteCommand.LastMsg.Substring(0, 2) != "OK")
    //                    {
    //                        MessageBox.Show("ERROR:" + mDA.DeleteCommand.LastMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //                        StatusMsg("ERROR:" + mDA.DeleteCommand.LastMsg);
    //                        return;
    //                    }
    //                    ClearValues();
    //                    Status = EnumStatus.SEARCH;
    //                    if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = "Record deleted successfully X(!";
    //                    break;
    //                }
    //        }
    //    }

    //    public void StatusMsg(string lMsg)
    //    {
    //        if (CTLStatusBar != null)
    //        {
    //            if (MsgStatusInfoLabel != null) MsgStatusInfoLabel.Text = lMsg;
    //        }
    //    }

    //    private void Button_Click(string pButtonName) //lauched when clicked any button
    //    {
    //        try
    //        {
    //            CTLMEventArgs lCTLMEventArgs = new CTLMEventArgs(pButtonName);
    //            BeforeButtonClick(this, lCTLMEventArgs); //launched BeforeButtonClick Event
    //            if (lCTLMEventArgs.Cancel) { return; }
    //            switch (pButtonName)
    //            {
    //                case "btnAdd":
    //                    Status = EnumStatus.ADDNEW;
    //                    break;
    //                case "btnUpp":
    //                    Status = EnumStatus.EDIT;
    //                    break;
    //                case "btnDel":
    //                    if (MessageBox.Show("This will delete the actual record. Are you sure?", "WARNING", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
    //                    {
    //                        Status = EnumStatus.DELETE;
    //                        Click_OK();
    //                    }
    //                    break;
    //                case "btnSearch":
    //                    Status = EnumStatus.SEARCH;
    //                    break;
    //                case "btnCancel":
    //                    ClearValues();
    //                    Status = EnumStatus.SEARCH;
    //                    break;
    //                case "btnOk":
    //                    Click_OK();
    //                    break;
    //                case "btnNext":
    //                    if (mDA.SelectRS.State == RSState.Open)
    //                    {
    //                        mDA.SelectRS.MoveNext();
    //                        mRsPosition++;
    //                        if (mDA.SelectRS.EOF)
    //                        {
    //                            MessageBox.Show("Actual record is the last one.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //                            return;
    //                        }
    //                        ShowRSValues();
    //                    }
    //                    break;
    //                case "btnPrev":
    //                    if (mDA.SelectRS.State == RSState.Open)
    //                    {
    //                        mDA.SelectRS.MovePrevious();
    //                        mRsPosition--;
    //                        if (mDA.SelectRS.BOF)
    //                        {
    //                            MessageBox.Show("Actual record is the first one.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //                            return;
    //                        }
    //                        ShowRSValues();
    //                    }
    //                    break;
    //                case "btnFirst":
    //                    if (mDA.SelectRS.State == RSState.Open)
    //                    {
    //                        mDA.SelectRS.MoveFirst();
    //                        mRsPosition = 1;
    //                        if (mDA.SelectRS.BOF)
    //                        {
    //                            MessageBox.Show("Actual record is the first one.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //                            return;
    //                        }
    //                        ShowRSValues();
    //                    }
    //                    break;
    //                case "btnLast":
    //                    if (mDA.SelectRS.State == RSState.Open)
    //                    {
    //                        mDA.SelectRS.MoveLast();
    //                        mRsPosition = mDA.SelectRS.RecordCount;
    //                        if (mDA.SelectRS.EOF)
    //                        {
    //                            MessageBox.Show("Actual record is the last one.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //                            return;
    //                        }
    //                        ShowRSValues();
    //                    }
    //                    break;
    //            }
    //            AfterButtonClick(this, new CTLMEventArgs(pButtonName)); //Launched AfterButtonClick Event
    //        }
    //        catch (Exception e)
    //        {
    //            MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //        }
    //    }


    //    private void CTLM_Click(object sender, ToolStripItemClickedEventArgs e)  //the Click Event
    //    {
    //        Button_Click(e.ClickedItem.Name);
    //    }

    //}

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
