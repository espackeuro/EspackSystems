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
    public enum AggregateOperations { COUNT, MIN, MAX, SUM, AVERAGE, NONE }
    public interface CtlVSColumn : EspackControl
    {
        string Attr { get; set; }
        bool IsFlag { get; set; }
        bool Locked { get; set; }
        bool Print { get; set; }
        int Colnumber { get; set; }
        int MaxWidth { get; }
        string SPAddParamName { get; set; }
        string SPUppParamName { get; set; }
        string SPDelParamName { get; set; }
        string Query { get; set; }
        int Width { get; set; }
        string Alignment { get; set; }
        bool Sortable { get; set; }
        string RowColor { get; set; }
        List<CtlVSColumn> ChangedCols { get; set; }
        EspackFormControl LinkedControl { get; set; }
        string Name { get; set; }
        AutoCompleteMode AutoCompleteMode { get; set; }
        AutoCompleteSource AutoCompleteSource { get; set; }
        AutoCompleteStringCollection AutoCompleteCustomSource { get; set; }
        string AutoCompleteQuery { get; set; }
        cAccesoDatosNet Conn { get; }
        CtlVSGrid Parent { get; set; }
        List<DataGridViewCell> Cells { get; }
        bool IsNumeric { get; }
        AggregateOperations Aggregate { get; set; }
        float AggregateValue { get; }
        //object Value { get; set; }
    }

    public class CtlVSTextBoxColumn : DataGridViewColumn, CtlVSColumn
    {
        private string aQuery;
        private bool lLocked;
        public EspackControlTypeEnum EspackControlType { get; set; }
        // non used stuff, must be declared to accomplisht with EspackFormControl interface
        public EspackLabel CaptionLabel { get; set; }
        private EnumStatus _status;
        public bool Print { get; set; }
        public event EventHandler TextChanged;

        public EnumStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                if (value== EnumStatus.ADDNEW || value==EnumStatus.EDIT)
                {
                    if (aQuery != "" && Conn != null)
                    {
                        AutoCompleteCustomSource = new AutoCompleteStringCollection();
                        DynamicRS _RS = new DynamicRS(aQuery, Conn);
                        _RS.Open();
                        while (!_RS.EOF)
                        {
                            AutoCompleteCustomSource.Add(_RS[0].ToString());
                            _RS.MoveNext();
                        }
                        _RS.Close();
                        _RS = null;
                    }
                }
            }
        }
        public CtlVSGrid Parent { get; set; }
        public string Caption { get; set; }
        public int Order { get; set; }
        public bool PK { get; set; }
        public bool Search { get; set; }
        public object DefaultValue { get; set; }
        public Type DBFieldType { get; set; }
        public Point Location { get; set; }
        public void UpdateEspackControl() { }
        public void ClearEspackControl() { }
        // end non unsed stuff
        public AutoCompleteMode AutoCompleteMode { get; set; }
        public AutoCompleteSource AutoCompleteSource { get; set; }
        public AutoCompleteStringCollection AutoCompleteCustomSource { get; set; }
        public string AutoCompleteQuery {
            get
            {
                return aQuery;
            }
            set
            {
                aQuery = value;
                //if (value!="" && value!= null)
                //{
                //    AutoCompleteCustomSource = new AutoCompleteStringCollection();
                //    DynamicRS _RS = new DynamicRS(aQuery, Conn);
                //    _RS.Open();
                //    while (!_RS.EOF)
                //    {
                //        AutoCompleteCustomSource.Add(_RS[0].ToString());
                //        _RS.MoveNext();
                //    }
                //    _RS.Close();
                //    _RS = null;
                //}
            }
        }
        public cAccesoDatosNet Conn
        {
            get
            {
                if (Parent != null)
                    return Parent.Conn;
                else
                    return null;
            }
        }
        public string DBField { get; set; }
        //public bool    Hidden { get; set; } 
        public string Attr { get; set; }
        public bool Add { get; set; }
        public bool Upp { get; set; }
        public bool Del { get; set; }
        public bool IsFlag { get; set; }
        public bool Locked
        {
            get
            {
                return lLocked;
            }
            set
            {
                lLocked = value;
                ReadOnly = value;
                if (!value)
                {
                    DefaultCellStyle.BackColor = Colors.CELLBACKCOLOR;
                    DefaultCellStyle.ForeColor = Colors.CELLFORECOLOR;
                }
                else
                {
                    DefaultCellStyle.BackColor = Colors.CELLLOCKEDBACKCOLOR;
                    DefaultCellStyle.ForeColor = Colors.CELLLOCKEDFORECOLOR;
                }
            }

        }
        public int Colnumber { get; set; }
        public string SPAddParamName { get; set; }
        public string SPUppParamName { get; set; }
        public string SPDelParamName { get; set; }
        public string Query { get; set; }
        //public int     Width { get; set; }
        public string Alignment { get; set; }
        public bool Sortable { get; set; }
        public string RowColor { get; set; }
        public List<CtlVSColumn> ChangedCols { get; set; }
        public EspackFormControl LinkedControl { get; set; }

        public override object Clone()
        {
            CtlVSTextBoxColumn that = (CtlVSTextBoxColumn)base.Clone();
            that.Order = this.Order;
            that.PK = this.PK;
            that.Search = this.Search;
            that.DefaultValue = this.DefaultValue;
            that.DBFieldType = this.DBFieldType;
            that.Location = this.Location;
            that.AutoCompleteMode = this.AutoCompleteMode;
            that.AutoCompleteSource = this.AutoCompleteSource;
            that.AutoCompleteCustomSource = this.AutoCompleteCustomSource;
            that.AutoCompleteQuery = this.AutoCompleteQuery;
            that.DBField = this.DBField;
            that.Attr = this.Attr;
            that.Add = this.Add;
            that.Upp = this.Upp;
            that.Del = this.Del;
            that.IsFlag = this.IsFlag;
            that.Locked = this.Locked;
            that.SPAddParamName = this.SPAddParamName;
            that.SPUppParamName = this.SPUppParamName;
            that.SPDelParamName = this.SPDelParamName;
            that.Query = this.Query;
            that.Alignment = this.Alignment;
            that.Sortable = this.Sortable;
            that.RowColor = this.RowColor;
            that.LinkedControl = this.LinkedControl;
            that.Width = this.Width;
            return that;
        }

        public object Value
        {
            get
            {
                if (DataGridView != null)
                {
                    DataGridView lDG = DataGridView;
                    int lColNumber = Index;
                    if (lDG.CurrentCell != null)
                    {
                        int lRowNumber = lDG.CurrentCell.RowIndex;
                        if (lDG.Rows[lRowNumber].Cells[lColNumber] != null)
                            return lDG.Rows[lRowNumber].Cells[lColNumber].Value;
                    }
                }
                return "";
            }
            set
            {
                if (DataGridView != null)
                {
                    DataGridView lDG = DataGridView;
                    //int lColNumber = Index;
                    if (lDG.CurrentCell!= null)
                    {
                        //int lRowNumber = lDG.CurrentCell.RowIndex;
                        lDG.CurrentCell.Value = value;
                    }

                }
            }
        }

        public string Text
        {
            get
            {
                return Value.ToString();
            }

            set
            {
                Value=value;
            }
        }

        public CtlVSTextBoxColumn():
            base()
        {
            CellTemplate = new DataGridViewTextBoxCell();
            Aggregate = AggregateOperations.NONE;
        }
        public int MaxWidth
        {
            get
            {
                var _grid= Cells.OfType<DataGridViewCell>().Max(x => x.Value.ToString().Length);
                return _grid > HeaderCell.Value.ToString().Length ? _grid : HeaderCell.Value.ToString().Length;
            }
        }
        public List<DataGridViewCell> Cells
        {
            get
            {
                return DataGridView.Rows.OfType<DataGridViewRow>().Select(x => x.Cells[Index]).ToList();
            }
        }
        public bool IsNumeric
        {
            get
            {
                try
                {
                    Cells.ForEach(c => Single.Parse(c.Value.ToString()));
                } catch (FormatException)
                {
                    return false;
                }
                return true;
            }
        }
        public AggregateOperations Aggregate { get; set; }
        public float AggregateValue
        {
            get
            {
                switch (Aggregate)
                {
                    case AggregateOperations.COUNT:
                        return Cells.Count;
                    case AggregateOperations.AVERAGE:
                        if (IsNumeric)
                        {
                            return Cells.Select(x => Single.Parse(x.Value.ToString())).Average();
                        }
                        else
                            throw new InvalidDataException("Not numeric data.");
                    case AggregateOperations.MAX:
                        if (IsNumeric)
                        {
                            return Cells.Select(x => Single.Parse(x.Value.ToString())).Max();
                        }
                        else
                            throw new InvalidDataException("Not numeric data.");
                    case AggregateOperations.MIN:
                        if (IsNumeric)
                        {
                            return Cells.Select(x => Single.Parse(x.Value.ToString())).Min();
                        }
                        else
                            throw new InvalidDataException("Not numeric data.");
                    case AggregateOperations.SUM:
                        if (IsNumeric)
                        {
                            return Cells.Select(x => Single.Parse(x.Value.ToString())).Sum();
                        }
                        else
                            throw new InvalidDataException("Not numeric data.");
                    default:
                        return 0;
                }
            }
        }
    }




    public class CtlVSComboColumn : DataGridViewComboBoxColumn, CtlVSColumn
    {
        private bool lLocked;
        private string _query;
        public EspackControlTypeEnum EspackControlType { get; set; }
        // non used stuff, must be declared to accomplisht with EspackFormControl interface
        //public EspackLabel CaptionLabel { get; set; }
        private EnumStatus _status;
        public bool Print { get; set; }
        public event EventHandler TextChanged;

        public EnumStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                //if (value == EnumStatus.ADDNEW || value == EnumStatus.EDIT)
                //{
                //    if (Query != "" && Conn != null)
                //    {
                //        var _RS = new DynamicRS(Query, Conn);
                //        DataSource = _RS.DataObject;
                //        DisplayMember = _RS.Fields[0];
                //        ValueMember = DisplayMember;
                //    }
                //}
            }
        }
        public CtlVSGrid Parent { get; set; }
        public string Caption { get; set; }
        public int Order { get; set; }
        public bool PK { get; set; }
        public bool Search { get; set; }
        public object DefaultValue { get; set; }
        public Type DBFieldType { get; set; }
        public Point Location { get; set; }
        public void UpdateEspackControl() { }
        public void ClearEspackControl() { }
        // end non unsed stuff

        public AutoCompleteMode AutoCompleteMode { get; set; }
        public AutoCompleteSource AutoCompleteSource { get; set; }
        public AutoCompleteStringCollection AutoCompleteCustomSource { get; set; }
        public string AutoCompleteQuery { get; set; }

        public string DBField { get; set; }
        //public bool    Hidden { get; set; } 
        public string Attr { get; set; }
        public bool Add { get; set; }
        public bool Upp { get; set; }
        public bool Del { get; set; }
        public bool IsFlag { get; set; }
        public bool Locked
        {
            get
            {
                return lLocked;
            }
            set
            {
                lLocked = value;
                ReadOnly = value;
                if (!value)
                {
                    DefaultCellStyle.BackColor = Colors.CELLBACKCOLOR;
                    DefaultCellStyle.ForeColor = Colors.CELLFORECOLOR;
                }
                else
                {
                    DefaultCellStyle.BackColor = Colors.CELLLOCKEDBACKCOLOR;
                    DefaultCellStyle.ForeColor = Colors.CELLLOCKEDFORECOLOR;
                }
            }

        }
        public int Colnumber { get; set; }
        public string SPAddParamName { get; set; }
        public string SPUppParamName { get; set; }
        public string SPDelParamName { get; set; }
        public cAccesoDatosNet Conn
        {
            get
            {
                if (Parent != null)
                    return Parent.Conn;
                else
                    return null;
            }
        }
        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
                if (_query != "" && Conn != null)
                {
                    var _RS = new DynamicRS(_query, Conn);
                    DataSource = _RS.DataObject;
                    DisplayMember = _RS.Fields[0];
                    ValueMember = DisplayMember;
                }
            }
        }
        //public int Width { get; set; }
        public string Alignment { get; set; }
        public bool Sortable { get; set; }
        public string RowColor { get; set; }
        public List<CtlVSColumn> ChangedCols { get; set; }
        public EspackFormControl LinkedControl { get; set; }

        public override object Clone()
        {
            CtlVSComboColumn that = (CtlVSComboColumn)base.Clone();
            that.Order = this.Order;
            that.PK = this.PK;
            that.Search = this.Search;
            that.DefaultValue = this.DefaultValue;
            that.DBFieldType = this.DBFieldType;
            that.Location = this.Location;
            that.AutoCompleteMode = this.AutoCompleteMode;
            that.AutoCompleteSource = this.AutoCompleteSource;
            that.AutoCompleteCustomSource = this.AutoCompleteCustomSource;
            that.AutoCompleteQuery = this.AutoCompleteQuery;
            that.DBField = this.DBField;
            that.Attr = this.Attr;
            that.Add = this.Add;
            that.Upp = this.Upp;
            that.Del = this.Del;
            that.IsFlag = this.IsFlag;
            that.Locked = this.Locked;
            that.SPAddParamName = this.SPAddParamName;
            that.SPUppParamName = this.SPUppParamName;
            that.SPDelParamName = this.SPDelParamName;
            that.Query = this.Query;
            that.Alignment = this.Alignment;
            that.Sortable = this.Sortable;
            that.RowColor = this.RowColor;
            that.LinkedControl = this.LinkedControl;
            that.Width = this.Width;
            return that;
        }
        public object Value
        {
            get
            {
                if (DataGridView != null)
                {
                    if (DataGridView != null)
                    {
                        DataGridView lDG = DataGridView;
                        int lColNumber = Index;
                        if (lDG.CurrentCell != null)
                        {
                            int lRowNumber = lDG.CurrentCell.RowIndex;
                            if (lDG.Rows[lRowNumber].Cells[lColNumber] != null)
                                return lDG.Rows[lRowNumber].Cells[lColNumber].Value;
                        }
                    }
                    return "";
                }
                return "";
            }
            set
            {
                if (DataGridView != null)
                {
                    DataGridView lDG = DataGridView;
                    if (lDG.CurrentCell != null)
                    {
                        lDG.CurrentCell.Value = value;
                    }

                }
            }
        }

        public string Text
        {
            get
            {
                return Value.ToString();
            }

            set
            {
                Value = value;
            }
        }

        public CtlVSComboColumn():
            base()
        {
            CellTemplate = new DataGridViewComboBoxCell();
            Aggregate = AggregateOperations.NONE;
        }
        public int MaxWidth
        {
            get
            {
                var _grid = Cells.OfType<DataGridViewCell>().Max(x => x.Value.ToString().Length);
                return _grid > HeaderCell.Value.ToString().Length ? _grid : HeaderCell.Value.ToString().Length;
            }
        }
        public List<DataGridViewCell> Cells
        {
            get
            {
                return DataGridView.Rows.OfType<DataGridViewRow>().Select(x => x.Cells[Index]).ToList();
            }
        }
        public bool IsNumeric
        {
            get
            {
                try
                {
                    Cells.ForEach(c => Single.Parse(c.Value.ToString()));
                }
                catch (FormatException)
                {
                    return false;
                }
                return true;
            }
        }
        public AggregateOperations Aggregate { get; set; }
        public float AggregateValue
        {
            get
            {
                switch (Aggregate)
                {
                    case AggregateOperations.COUNT:
                        return Cells.Count;
                    case AggregateOperations.AVERAGE:
                        if (IsNumeric)
                        {
                            return Cells.Select(x => Single.Parse(x.Value.ToString())).Average();
                        }
                        else
                            throw new InvalidDataException("Not numeric data.");
                    case AggregateOperations.MAX:
                        if (IsNumeric)
                        {
                            return Cells.Select(x => Single.Parse(x.Value.ToString())).Max();
                        }
                        else
                            throw new InvalidDataException("Not numeric data.");
                    case AggregateOperations.MIN:
                        if (IsNumeric)
                        {
                            return Cells.Select(x => Single.Parse(x.Value.ToString())).Min();
                        }
                        else
                            throw new InvalidDataException("Not numeric data.");
                    case AggregateOperations.SUM:
                        if (IsNumeric)
                        {
                            return Cells.Select(x => Single.Parse(x.Value.ToString())).Sum();
                        }
                        else
                            throw new InvalidDataException("Not numeric data.");
                    default:
                        return 0;
                }
            }
        }
    }
}
