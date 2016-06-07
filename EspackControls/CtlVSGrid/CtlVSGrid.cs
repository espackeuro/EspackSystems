using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
//using Microsoft.Data.Schema.ScriptDom;
//using Microsoft.Data.Schema.ScriptDom.Sql;
using System.IO;
using AccesoDatosNet;
using CommonTools;
using EspackControls;
using EspackFormControls;

namespace VSGrid
{
    
    public class CtlVSGrid : DataGridView, EspackFormControl
    {
        public EspackControlTypeEnum EspackControlType { get; set; }
        //private string mDBTable;
        private DA mDA = new DA();
        private EnumStatus mStatus;
        //private string mWhereString;
        private bool mPaginate;
        private int mPageSize;
        private ToolStrip mNavigationBar;
        private EnumStatus mPreviousParentStatus;
//Properties
        public bool AllowInsert { get; set; }
        public bool AllowUpdate { get; set; }
        public bool AllowDelete { get; set; }
        public int NumPages { get; set; }
        public EspackControl EspackControlParent { get; set; }
        //public Dictionary<string,Control> VSPrimaryKey { get; set; }
        public object Value { get; set; }
        public string DBField { get; set; }
        public bool Add { get; set; }
        public bool Upp { get; set; }
        public bool Del { get; set; }
        public int Order { get; set; }
        public bool PK { get; set; }
        public bool Search { get; set; }
        public object DefaultValue { get; set; }
        public Type DBFieldType { get; set; }
        //EspackFormControl properties
        public EspackLabel CaptionLabel { get; set; }
        public string Caption
        {
            get
            {
                if (CaptionLabel != null)
                {
                    return CaptionLabel.Caption;
                }
                else return null;
            }
            set
            {
                if (CaptionLabel.Parent == null && Parent != null)
                {
                    Parent.Controls.Add(CaptionLabel);
                }
                CaptionLabel.Caption = value;
                //CaptionLabel.Location = new Point(Location.X - CaptionLabel.PreferredWidth - 6, Location.Y);
                CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight - 5);
            }
        }
        public DA ParentDA { get; set; }
        public cAccesoDatosNet ParentConn { get; set; }
        public StaticRS DependingRS { get; set; }

        //Properties with SPs but in string format, we will use this when assigning a SP by its name
        public string sSPAdd
        {
            set
            {
                if (value != "") mDA.sSPAdd = value;
                AllowInsert = true;
            }
            get
            {
                return mDA.sSPAdd;
            }
        }

        public string sSPUpp
        {
            set
            {
                if (value != "")
                {
                    mDA.sSPUpp = value;
                    AllowUpdate = true;
                }
            }
            get
            {
                return mDA.sSPUpp;
            }
        }

        public string sSPDel
        {
            set
            {
                if (value != "") mDA.sSPDel = value;
                AllowDelete = true;
            }
            get
            {
                return mDA.sSPDel;
            }
        }

        public cAccesoDatosNet Conn
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
        public string SQL
        {
            get
            {
                if (mDA.SelectRS != null) 
                {
                    return mDA.SQL;
                }
                else return null;
            }
            set
            {
                if (value != null)
                {
                    mDA.SQL = value;
                }
            }
        }

        //public StatusBar CtlVSStatusBar { get; set; }
        public ToolStripStatusLabel MsgStatusLabel { get; set; }

        public IEnumerable<DataGridViewColumn> ColumnsSPAdd
        {
            get
            {
                return Columns.Cast<DataGridViewColumn>().Where(x => ((CtlVSColumn)x).Add);
            }
        }
        public IEnumerable<DataGridViewColumn> ColumnsSPUpp
        {
            get
            {
                return Columns.Cast<DataGridViewColumn>().Where(x => ((CtlVSColumn)x).Upp);
            }
        }
        public IEnumerable<DataGridViewColumn> ColumnsSPDel
        {
            get
            {
                return Columns.Cast<DataGridViewColumn>().Where(x => ((CtlVSColumn)x).Del);
            }
        }
        public IEnumerable<DataGridViewColumn> ColumnsExternalKeys
        {
            get
            {
                return Columns.Cast<DataGridViewColumn>().Where(x => ((CtlVSColumn)x).LinkedControl != null);
            }
        }

        public List<DataGridViewRow> ToList()
        {
            return Rows.OfType<DataGridViewRow>().ToList();
        }

        public SqlQuery CtlQuery { get; set; }
        public int Page
        {
            get;
            set;
        }

        public bool Paginate
        {
            get
            {
                return mPaginate;
            }
            set
            {
                if (value)
                {
                    if (mNavigationBar != null)
                    {
                        return;
                    }
                    else
                    {
                        Page = 1;
                        mPaginate = true;
                        
                        mNavigationBar = new ToolStrip() { 
                            Location= new Point(this.Location.X,this.Location.Y+this.Size.Height-25),
                            Size=new Size(this.Size.Width,RowTemplate.Height), //the height of a row
                            Dock=DockStyle.None
                        };
                        this.Size = new Size(this.Size.Width, this.Size.Height - RowTemplate.Height);
                        this.Parent.FindForm().Controls.Add(mNavigationBar);
                        mNavigationBar.Items.Add(new ToolStripButton()
                        {
                            Name = "btnFirst",
                            Image =(System.Drawing.Image) Properties.Resources.first16
                        });
                        mNavigationBar.Items.Add(new ToolStripButton()
                        {
                            Name = "btnPrev",
                            Image = (System.Drawing.Image)Properties.Resources.prev16
                        });
                        mNavigationBar.Items.Add(new ToolStripLabel()
                        {
                            Name = "Counter",
                            Text = "_/_",
                        });
                        mNavigationBar.Items.Add(new ToolStripButton()
                        {
                            Name = "btnNext",
                            Image = (System.Drawing.Image)Properties.Resources.next16
                        });
                        mNavigationBar.Items.Add(new ToolStripButton()
                        {
                            Name = "btnLast",
                            Image = (System.Drawing.Image)Properties.Resources.last16
                        });
                        mNavigationBar.ItemClicked += mNavigationBar_ItemClicked;
                        mPageSize = this.Size.Height / this.RowTemplate.Height - 3;
                    }
                }
                else
                {
                    if (mNavigationBar != null)
                    {
                        mNavigationBar.Dispose();
                        mNavigationBar = null;
                        this.Size = new Size(this.Size.Width, this.Size.Height + RowTemplate.Height);
                    }
                    mPaginate = false;
                }
            }
        }


        public EnumStatus Status
        {
            set
            {
                foreach (CtlVSColumn Col in Columns)
                {
                    Col.Status = value;
                }
                mStatus = value;
                switch (value)
                {
                    case EnumStatus.ADDNEW:
                        {
                            break;
                        }
                    case EnumStatus.EDIT:
                        {
                            DataRow newRow = mDA.Table.NewRow();
                            mDA.Table.Rows.Add(newRow);
                            break;
                        }
                    case EnumStatus.DELETE:
                        {
                            break;
                        }
                    case EnumStatus.SEARCH:
                        {
                            RowEditedBool = false;
                            foreach (CtlVSColumn Col in Columns)
                            {
                                Col.Locked = true;
                            }
                            break;
                        }
                    case EnumStatus.NAVIGATE:
                        {
                            foreach (CtlVSColumn Col in Columns)
                            {
                                Col.Locked = true;
                            }
                            break;
                        }
                }
                if ((Status == EnumStatus.ADDNEW || Status == EnumStatus.EDIT) && RowCount>0)
                {
                    if (AllowUpdate)
                    {
                        foreach (CtlVSColumn lCol in Columns)
                        {
                            lCol.Locked = !lCol.Upp;
                        }
                    }
                    foreach (DataGridViewCell lCell in Rows[RowCount - 1].Cells)
                    {
                        lCell.ReadOnly = !((CtlVSColumn)Columns[lCell.ColumnIndex]).Add;
                        if (lCell.ReadOnly)
                        {
                            lCell.Style.BackColor = Colors.CELLLOCKEDBACKCOLOR;
                            lCell.Style.ForeColor = Colors.CELLLOCKEDFORECOLOR;
                        }
                        else
                        {
                            lCell.Style.BackColor = Colors.CELLBACKCOLOR;
                            lCell.Style.ForeColor = Colors.CELLFORECOLOR;
                        }
                    }
                }
            }
            get
            {
                return mStatus;
            }
        }



        public string Query
        {
            get
            {
                string lSQL = "";
                List<string> lQueryFields = new List<string>();
                List<string> lWhereFields = new List<string>();
                List<string> lOrderFields = new List<string>();

                foreach (CtlVSColumn Item in Columns)
                {
                    string lDelimiter = CT.IsNumericType(Item.DBFieldType) ? "" : "'";
                    if (Item.DBField != "")
                    {
                        lQueryFields.Add("["+Item.Name + "]=" + Item.DBField);

                    }
                    if (Item.LinkedControl != null)
                    {
                        string lValue = "@" + Item.Name;
                        lWhereFields.Add(Item.DBField + "=" + lValue);
                    }
                }
                foreach (CtlVSColumn lItem in Columns)
                {
                    if (lItem.Sortable)
                    {
                        lOrderFields.Add(lItem.DBField);
                    }
                    
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

        public string DBTable { get; set; }
        private int RowEdited;
        private bool RowEditedBool=false;
//Constructors
        public CtlVSGrid()
        {
            AllowDelete = false;
            AllowInsert = false;
            AllowUpdate = false;
            RowHeadersVisible = false;
            //VSPrimaryKey = new Dictionary<string,Control>();
            GridColor = SystemColors.ButtonFace;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //EditMode = DataGridViewEditMode.EditOnEnter;
            CellBeginEdit += VSCellBeginEdit;
            CellEndEdit+=VSCellEndEdit;
            Resize += CtlVSGrid_Resize;
            KeyDown += CtlVSGrid_KeyDown;
            SelectionChanged += CtlVSGrid_SelectionChanged;
            EditingControlShowing += CtlVSGrid_EditingControlShowing;
            Status = EnumStatus.SEARCH;
            AllowUserToAddRows = false;
            //CellValidating += CtlVSGrid_CellValidating;
            //EditingControlShowing += CtlVSGrid_EditingControlShowing;
            //DataError += CtlVSGrid_DataError;
            CaptionLabel = new EspackLabel("", this) { AutoSize = true };
            EspackTheme.changeControlFormat(this);
        }

        private void CtlVSGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var _Col = Columns[this.CurrentCellAddress.X];
            if (((CtlVSColumn)_Col).AutoCompleteMode != AutoCompleteMode.None)
            {
               if (_Col is CtlVSTextBoxColumn)
                {
                    TextBox _box = (TextBox)e.Control;
                    
                    if (_box != null)
                    {
                        _box.AutoCompleteMode = ((CtlVSColumn)_Col).AutoCompleteMode;
                        _box.AutoCompleteSource = ((CtlVSColumn)_Col).AutoCompleteSource;
                        _box.AutoCompleteCustomSource = ((CtlVSColumn)_Col).AutoCompleteCustomSource;
                    }
                } else if (_Col is CtlVSComboColumn)
                {
                    ComboBox _box = (ComboBox)e.Control;
                    if (_box != null)
                    {
                        _box.AutoCompleteMode = ((CtlVSColumn)_Col).AutoCompleteMode;
                        _box.AutoCompleteSource = ((CtlVSColumn)_Col).AutoCompleteSource;
                        _box.AutoCompleteCustomSource = ((CtlVSColumn)_Col).AutoCompleteCustomSource;
                    }
                }
            }
        }

        ~ CtlVSGrid()
        {
            if (CaptionLabel != null)
                CaptionLabel.Dispose();
            CaptionLabel = null;
        }

        //private void CtlVSGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //{
        //    CancelEdit();
        //}

        //private void CtlVSGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    if (Columns[(this.CurrentCellAddress.X)] is CtlVSComboColumn)
        //    {
        //        ComboBox cb = e.Control as ComboBox;
        //        if (cb != null)
        //        {
        //            cb.DropDownStyle = ComboBoxStyle.DropDown;
        //        }
        //    }
        //}

        //private void CtlVSGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //{
        //    if (Columns[e.ColumnIndex] is CtlVSComboColumn)
        //    {
        //        if (!((CtlVSComboColumn)Columns[e.ColumnIndex]).Items.Contains(e.FormattedValue))
        //        {
        //            //((CtlVSComboColumn)Columns[e.ColumnIndex]).Items.Add(e.FormattedValue);
        //            //CancelEdit();
        //        }
        //    }
        //}


        //Events
        void CtlVSGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (RowEditedBool && RowEdited != CurrentCell.RowIndex && Rows.Count>RowEdited)
            {
                CurrentCell = Rows[RowEdited].Cells[CurrentCell.ColumnIndex];
            }
        }

        private void CtlVSGrid_Resize(object sender, EventArgs e) //resizes the control to multiples of row height
        {
            if (this.Size.Height % RowTemplate.Height != 0)
            {
                if (this.Size.Height % RowTemplate.Height > RowTemplate.Height / 2)
                {
                    this.Size = new Size(this.Size.Width, RowTemplate.Height * ((int)(this.Size.Height / RowTemplate.Height) + 1));
                }
                else
                {
                    this.Size = new Size(this.Size.Width, RowTemplate.Height * (int)(this.Size.Height / RowTemplate.Height));
                }
            }
        }

        private void VSCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (EspackControlParent != null && !RowEditedBool)
            {
                mPreviousParentStatus = EspackControlParent.Status;
                if (CurrentCell.RowIndex == Rows.Count - 1)
                {
                    EspackControlParent.Status = EnumStatus.ADDGRIDLINE;
                }
                else
                {
                    EspackControlParent.Status = EnumStatus.EDITGRIDLINE;
                }
            }
        }

        private void VSCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < Columns.Count - 1)
            {
                do
                {
                    //if (Rows[e.RowIndex].Cells[CurrentCell.ColumnIndex + 1].Visible == true)
                    //{
                    //    CurrentCell = Rows[e.RowIndex].Cells[CurrentCell.ColumnIndex + 1];
                    //}
                    SendKeys.Send("{RIGHT}");
                }
                while (CurrentCell.ReadOnly && CurrentCell.ColumnIndex < Columns.Count - 1);
            }
            RowEdited = e.RowIndex;
            RowEditedBool = true;
        }

        //private void VSCellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (CurrentCell.ReadOnly && e.ColumnIndex < Columns.Count - 1)
        //    {
        //        SendKeys.Send("{RIGHT}");
        //    }
        //}

        void mNavigationBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "btnNext":
                    if (Page < NumPages)
                    {
                        Page++;
                        Navigate();
                    }
                    break;
                case "btnPrev":
                    if (Page > 1)
                    {
                        Page--;
                        Navigate();
                    }
                    break;
                case "btnFirst":
                    Page = 1;
                    Navigate();
                    break;
                case "btnLast":
                    Page = NumPages;
                    Navigate();
                    break;
            }
            mNavigationBar.Items["Counter"].Text = Page.ToString() + "/"+NumPages.ToString();
            return;
        }

        private void CtlVSGrid_KeyDown(object sender, KeyEventArgs e)
        {
            SP lCommand;
            string lMsg = "";

            if (Status != EnumStatus.ADDGRIDLINE && Status != EnumStatus.EDITGRIDLINE && Status != EnumStatus.EDIT && Status != EnumStatus.ADDNEW)
            {
                CancelEdit();
                return;
            }
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        //do
                        //{
                        //    CurrentCell = Rows[CurrentCell.RowIndex].Cells[CurrentCell.ColumnIndex + 1];
                        //    //SendKeys.Send("{RIGHT}");
                        //}
                        //while (CurrentCell.ReadOnly && CurrentCell.ColumnIndex < Columns.Count - 1);
                        //SendKeys.Send("{RIGHT}");
                        BeginEdit(true);
                        e.Handled = true;
                        break;
                    }
                case Keys.Delete:
                case Keys.Insert:
                    {
                        if (e.KeyCode == Keys.Delete)
                        {
                            if (MessageBox.Show("This will delete the actual line. Are you sure?", "WARNING", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                lCommand = mDA.DeleteCommand;
                                lMsg = "Line deleted OK";
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (CurrentCell.RowIndex == Rows.Count - 1)
                            {
                                lCommand = mDA.InsertCommand;
                                lMsg = "Line inserted OK";
                            }
                            else
                            {
                                lCommand = mDA.UpdateCommand;
                                lMsg = "Line updated OK";
                            }
                        }
                        lCommand.Execute();
                        if (lCommand.LastMsg != "OK")
                        {
                            MessageBox.Show("ERROR:" + lCommand.LastMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            lMsg="ERROR:" + lCommand.LastMsg;
                            if (lCommand == mDA.UpdateCommand) //si es un update que ha dado error, reconsultamos el grid para eliminar los valores erróneos
                            {
                                RowEditedBool = false;
                                var x = CurrentCell.ColumnIndex;
                                var y = CurrentCell.RowIndex;
                                UpdateEspackControl();
                                CurrentCell = Rows[y].Cells[x];
                            }
                                
                            break;
                        }
                        StatusMsg(lMsg);
                        RowEditedBool = false;
                        UpdateEspackControl();
                        if (EspackControlParent != null && !lCommand.Equals(mDA.DeleteCommand))
                        {
                            EspackControlParent.Status = mPreviousParentStatus;
                        }
                        break;
                    }
            }
        }
        protected override void OnMove(EventArgs e)
        {
            CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight - 5);
            //CaptionLabel.Location = new Point(Location.X - CaptionLabel.PreferredWidth - 6, Location.Y);
            base.OnMove(e);
        }
        //methods
        public void Start()
        {
            this.AutoGenerateColumns = false;
            if (SQL != null)
            {
                CtlQuery = CT.SimpleParseSQL(SQL);
                mDA.FillSchema();
                Columns.Clear();
                foreach (DataColumn Col in mDA.Schema)
                {
                    this.Columns.Add(new CtlVSTextBoxColumn()
                    {
                        Name = CtlQuery.SelectFields[Col.ColumnName].DBItemName,
                        DBField = CtlQuery.SelectFields[Col.ColumnName].DBItemName,
                        HeaderText = Col.ColumnName,
                        CellTemplate = new DataGridViewTextBoxCell(),
                        DataPropertyName = Col.ColumnName,
                        DBFieldType = Col.DataType,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    });
                }
            }
            else
            {
                SQL = Query;
                foreach (CtlVSColumn Col in Columns) 
                {
                    if (Col.LinkedControl != null)
                    {
                        mDA.AddParameter(Col.Name, Col.LinkedControl);
                        if (Col.Add) mDA.InsertCommand.AddControlParameter(Col.SPAddParamName, Col.LinkedControl);
                        if (Col.Upp) mDA.UpdateCommand.AddControlParameter(Col.SPUppParamName, Col.LinkedControl);
                        if (Col.Del) mDA.DeleteCommand.AddControlParameter(Col.SPDelParamName, Col.LinkedControl);
                    }
                    else
                    {
                        if (Col.Add) mDA.InsertCommand.AddControlParameter(Col.SPAddParamName, Col);
                        if (Col.Upp) mDA.UpdateCommand.AddControlParameter(Col.SPUppParamName, Col);
                        if (Col.Del) mDA.DeleteCommand.AddControlParameter(Col.SPDelParamName, Col);
                    }
                }
                CtlQuery = CT.SimpleParseSQL(Query);
                mDA.FillSchema();
                foreach (DataColumn Col in mDA.Schema)
                {
                    ((CtlVSColumn)this.Columns[Col.ColumnName]).DBFieldType = Col.DataType;
                }
                
            }

            this.Refresh();
        }

        public void AddColumn(string pName, string pDBFieldName = "", string pSPAdd = "", string pSPUpp = "", string pSPDel = "", bool pSortable = false,
            bool pIsFlag=false, bool pLocked=false, string pQuery = "", int pWidth = 0, string pAlignment = "",  string pAttr="",AutoCompleteMode aMode=AutoCompleteMode.None,AutoCompleteSource aSource=AutoCompleteSource.None,string aQuery="")
        {
            if (pQuery=="")
            {
                var _Col = new CtlVSTextBoxColumn()
                {
                    Name = pName,
                    HeaderText = pName,
                    //Colnumber = pColnumber,
                    DBField = pDBFieldName == "" ? pName : pDBFieldName,
                    DataPropertyName = pName,
                    Parent = this,
                    Attr = pAttr,
                    Add = pSPAdd != "",
                    Upp = pSPUpp != "",
                    Del = pSPDel != "",
                    SPAddParamName = pSPAdd,
                    SPDelParamName = pSPDel,
                    SPUppParamName = pSPUpp,
                    IsFlag = pIsFlag,
                    Locked = pLocked,
                    ReadOnly = pLocked,
                    Query = pQuery,
                    Width = pWidth,
                    Alignment = pAlignment,
                    Sortable = pSortable,
                    LinkedControl = null,
                    AutoSizeMode = pWidth == 0 ? DataGridViewAutoSizeColumnMode.DisplayedCells : DataGridViewAutoSizeColumnMode.None,
                    AutoCompleteMode = aMode,
                    AutoCompleteSource = aSource,
                    AutoCompleteQuery = aQuery
               
                };
                Columns.Add(_Col);
            } else
            {
                var _Col = new CtlVSComboColumn()
                {
                    Name = pName,
                    HeaderText = pName,
                    //Colnumber = pColnumber,
                    DBField = pDBFieldName == "" ? pName : pDBFieldName,
                    DataPropertyName = pName,
                    Parent = this,
                    CellTemplate = new DataGridViewComboBoxCell(),
                    Attr = pAttr,
                    Add = pSPAdd != "",
                    Upp = pSPUpp != "",
                    Del = pSPDel != "",
                    SPAddParamName = pSPAdd,
                    SPDelParamName = pSPDel,
                    SPUppParamName = pSPUpp,
                    IsFlag = pIsFlag,
                    Locked = pLocked,
                    ReadOnly = pLocked,
                    Query = pQuery,
                    Width = pWidth,
                    Alignment = pAlignment,
                    Sortable = pSortable,
                    LinkedControl = null,
                    AutoSizeMode = pWidth == 0 ? DataGridViewAutoSizeColumnMode.DisplayedCells : DataGridViewAutoSizeColumnMode.None,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                    AutoComplete = true,
                    AutoCompleteMode = aMode,
                    AutoCompleteSource = aSource,
                    AutoCompleteQuery = aQuery,
                };
                Columns.Add(_Col);
            }
        }

        public void AddColumn(string pName, EspackFormControl pLinkedControl, string pSPAdd = "", string pSPUpp = "", string pSPDel = "", AutoCompleteMode aMode = AutoCompleteMode.None, AutoCompleteSource aSource = AutoCompleteSource.None, string aQuery="")
        {
            var _Col = new CtlVSTextBoxColumn()
            {
                Name = pName,
                HeaderText = pName,
                //Colnumber = pColnumber,
                DBField = pName,
                DataPropertyName = pName,
                CellTemplate = new DataGridViewTextBoxCell(),
                Attr = "",
                Add = pSPAdd != "",
                Upp = pSPUpp != "",
                Del = pSPDel != "",
                SPAddParamName = pSPAdd,
                SPDelParamName = pSPDel,
                SPUppParamName = pSPUpp,
                IsFlag = false,
                Locked = true,
                ReadOnly = true,
                Query = "",
                Width = 0,
                Alignment = "",
                Sortable = false,
                LinkedControl = pLinkedControl,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                AutoCompleteMode = aMode,
                AutoCompleteSource = aSource,
                AutoCompleteQuery = aQuery
            };
            Columns.Add(_Col);
        }

        public void StatusMsg(string lMsg)
        {
            if (MsgStatusLabel != null) MsgStatusLabel.Text = lMsg;
        }
        //public void AddPK(string pDBParameter,pDBFieldName)
        //{
        //    VSPrimaryKey.Add(pDBParameter,pPK);
        //    mDA.AddParameter(pDBParameter,pPK);
        //}
        public void UpdateEspackControl()
        {
            string lSql;
            int mNumRecords=0;
            Page = 1;
            //mDA.SelectRS=new DynamicRS();
            mDA.SelectRS.DS.Dispose();
            DataSource = null;
            if (Paginate)
            {
                mDA.Open((Page - 1) * mPageSize, mPageSize);
                lSql = "SELECT NumRecords=count(*) FROM " + CtlQuery.Tablename.DBItemName + " WHERE " + CtlQuery.WhereString;
                DynamicRS lRS = (DynamicRS)new DynamicRS(lSql,Conn);
                foreach (CtlVSColumn lColumn in ColumnsExternalKeys)
                {
                    lRS.AddControlParameter(lColumn.Name,lColumn.LinkedControl);
                }
                lRS.Open();
                mNumRecords = Convert.ToInt32(lRS["NumRecords"]);
                NumPages = mNumRecords / mPageSize + ((mNumRecords % mPageSize) == 0 ? 0 : 1);
                mNavigationBar.Enabled = NumPages>1;
                mNavigationBar.Items["Counter"].Text = "1/" + NumPages.ToString();
            }
            else
            {
                mDA.Open();
            }
            DataSource = mDA.Table;
            Refresh();
            Status = mStatus;
        }

        public void Navigate()
        {
            mDA.Open((Page - 1) * mPageSize, mPageSize);
            this.DataSource = mDA.Table;
            this.Refresh();
        }

        public void ClearEspackControl()
        {
            RowEditedBool = false;
            this.DataSource=null;
            this.RowCount = 0;
        }

    }

    public static class Colors
    {
        public static Color CELLLOCKEDBACKCOLOR = Color.LightGray;
        public static Color CELLLOCKEDFORECOLOR = Color.Black;
        public static Color CELLBACKCOLOR = Color.Beige;
        public static Color CELLFORECOLOR = Color.Black;
    }


}
