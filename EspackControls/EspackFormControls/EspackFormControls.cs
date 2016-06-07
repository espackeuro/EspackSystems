﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using EspackControls;
using CommonTools;
using AccesoDatosNet;
using System.Data;
using System.ComponentModel;

namespace EspackFormControls
{
    

    public interface EspackFormControl : EspackControl
    {
        EspackLabel CaptionLabel { get; set; }
        string Caption { get; set; }
        DA ParentDA { get; set; }
        cAccesoDatosNet ParentConn { get; set; }
        StaticRS DependingRS { get; set; }
        Point Location { get; set; }
        
        //List<StaticRS> ExternalControls;//list of possible external controls, the key is the parameter name and the object is the control
        //List<EspackControl> DependingControls { get; set; } //list of those controls which have me as external control
        //void AddRS(string pFieldName, EspackControl pControl);

    }

    //public class EspackQuery : EspackControl
    //{
    //    EspackControlTypeEnum EspackControlType { get; set; }
    //    EnumStatus Status { get; set; }
    //    Point Location { get; set; }
    //    object Value { get; set; }
    //    string DBField { get; set; }
    //    bool Add { get; set; }
    //    bool Upp { get; set; }
    //    bool Del { get; set; }
    //    int Order { get; set; }
    //    bool PK { get; set; }
    //    bool Search { get; set; }
    //    object DefaultValue { get; set; }
    //    Type DBFieldType { get; set; }
    //    void UpdateEspackControl(EspackControlTypeEnum pUpdateType);
    //    void ClearEspackControl();



    //    public string Query { get; set; }


    //}

    public class EspackLabel : Label
    {
        EspackFormControl ParentControl { get; set; }
        public string Caption
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
                if (ParentControl != null)
                {
                    Location = new Point(60, ParentControl.Location.Y);//ParentControl.Location.X - PreferredWidth - 6
                }
            }
        }


        public EspackLabel(string pCaption, EspackFormControl pParentControl)
        {
            ParentControl = pParentControl;
            Caption = pCaption;
            Margin = new Padding(0, 0, 0, 0);
        }
    }

    public class EspackTextBox : TextBox, EspackFormControl
    {
        public EspackControlTypeEnum EspackControlType { get; set; }
        public EspackLabel CaptionLabel { get; set; }
        public cAccesoDatosNet ParentConn { get; set; }
        private EnumStatus mStatus;
        private StaticRS mDependingRS;
        //private Padding _margin;
        //private Size _size;

        //public new Size Size
        //{
        //    get
        //    {
        //        return _size;
        //    }
        //    set
        //    {
        //        _size = value;
        //        base.Height = value.Height-CaptionLabel.Height;
        //        base.Width = value.Width;
        //    }
        //}
        //public new Point Location
        //{
        //    get
        //    {
        //        if (CaptionLabel != null)
        //        {
        //            var _l = new Point();
        //            _l.X = base.Location.X;
        //            _l.Y = base.Location.Y - CaptionLabel.Height;
        //            return _l;
        //        }
        //        else return base.Location;
        //    }
        //    set
        //    {
        //        var gap = 0;
        //        if (CaptionLabel != null)
        //        {
        //            CaptionLabel.Location = value;
        //            gap = CaptionLabel.Height;
        //        }

        //        var _l = new Point();
        //        _l.X = value.X;
        //        _l.Y = value.Y + gap;
        //        base.Location = _l;

        //    }
        //}
        ////[DefaultValue(typeof(Padding), "3, 3, 3, 3")]
        //public new Padding Margin
        //{
        //    get
        //    {
        //        return _margin;
        //    }
        //    set
        //    {

        //        var gap = 0;
        //        if (CaptionLabel != null)
        //            gap = CaptionLabel.Height;
        //        var _m = new Padding();
        //        _margin = value;
        //        _m = _margin;
        //        _m.Top += gap;
        //        BaseMargin = _m;
        //    }
        //}

        //[Category("Layout")]
        //[DefaultValue(typeof(Padding), "3, 16, 3, 3")]
        //public Padding BaseMargin
        //{
        //    get
        //    {
        //        return base.Margin;
        //    }
        //    set
        //    {
        //        base.Margin = value;
        //    }
        //}

        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                if (CaptionLabel != null)
                    CaptionLabel.Visible = value;
            }
        }

        public EnumStatus Status
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
                Enabled = (Add && Status == EnumStatus.ADDNEW) || (Upp && Status == EnumStatus.EDIT && !PK) || (Del && Status == EnumStatus.DELETE) || (Search && Status == EnumStatus.SEARCH);
            }
        }

        public object Value
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value.ToString();
            }
        }

        public string DBField { get; set; }
        public bool Add { get; set; }
        public bool Upp { get; set; }
        public bool Del { get; set; }
        public int Order { get; set; }
        public bool PK { get; set; }
        public bool Search { get; set; }
        public object DefaultValue { get; set; }
        public Type DBFieldType { get; set; }
        public DA ParentDA { get; set; }
        public StaticRS DependingRS
        {
            get
            {
                return mDependingRS;
            }
            set
            {
                mDependingRS = value;
                if (value!=null)
                    mDependingRS.AfterExecution += mDependingRS_AfterExecution;
            }
        }

        void mDependingRS_AfterExecution(object sender, EventArgs e)
        {
            Text=mDependingRS[DBField].ToString();
        }
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
                CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight);
            }
        }

        //List<StaticRS> ExternalControls;//list of possible external controls, the key is the parameter name and the object is the control
        //List<EspackControl> DependingControls; //list of those controls which have me as external control
        

        //void AddExternalControl(string pParameterName, StaticRS pRS)
        //{
        //    EspackControlType &= EspackControlTypeEnum.DEPENDANT;
        //    ExternalControls.Add(pParameterName, pRS);
        //    foreach (ControlParameter lControl in pRS.ControlParameters)
        //    {
        //        if (lControl.LinkedControl is EspackFormControl)
        //        {
        //            ((EspackFormControl)lControl.LinkedControl).DependingControls.Add(this);
        //        }
        //    }
        //}
        

        public EspackTextBox()
            : base()
        {
            CaptionLabel = new EspackLabel("", this) { AutoSize = true };
            //var _m = new Padding();
            //_m = base.Margin;
            //_m.Top = 16;
            //base.Margin = _m;
            //Margin = _m;
            EspackTheme.changeControlFormat(this);
        }

        ~EspackTextBox()
        {
            if (CaptionLabel!= null)
                CaptionLabel.Dispose();
            CaptionLabel = null;
        }
        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent != null)
            {
                Parent.Controls.Add(CaptionLabel);
                base.OnParentChanged(e);
            }
        }

        protected override void OnMove(EventArgs e)
        {
            CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight);
            //CaptionLabel.Location = new Point(Location.X - CaptionLabel.PreferredWidth - 6, Location.Y);
            base.OnMove(e);
        }

        public void UpdateEspackControl()
        {
            if ((EspackControlType & EspackControlTypeEnum.CTLM) == EspackControlTypeEnum.CTLM)
            {
                Text = ParentDA.SelectRS[DBField.ToString()].ToString();
            }
        }

        //public void OnTextChanged(EventArgs e)
        //{
        //    foreach (EspackControl lControl in DependingControls)
        //    {
        //        lControl.UpdateEspackControl(EspackControlTypeEnum.DEPENDANT);
        //    }
        //}

        public void ClearEspackControl()
        {
            Text = "";
        }
    }

    public class EspackMaskedTextBox : MaskedTextBox, EspackFormControl
    {
        public EspackControlTypeEnum EspackControlType { get; set; }
        public EspackLabel CaptionLabel { get; set; }
        public cAccesoDatosNet ParentConn { get; set; }
        private EnumStatus mStatus;
        private StaticRS mDependingRS;

        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                if (CaptionLabel != null)
                    CaptionLabel.Visible = value;
            }
        }

        public EnumStatus Status
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
                Enabled = (Add && Status == EnumStatus.ADDNEW) || (Upp && Status == EnumStatus.EDIT && !PK) || (Del && Status == EnumStatus.DELETE) || (Search && Status == EnumStatus.SEARCH);
            }
        }

        public object Value
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value.ToString();
            }
        }

        public string DBField { get; set; }
        public bool Add { get; set; }
        public bool Upp { get; set; }
        public bool Del { get; set; }
        public int Order { get; set; }
        public bool PK { get; set; }
        public bool Search { get; set; }
        public object DefaultValue { get; set; }
        public Type DBFieldType { get; set; }
        public DA ParentDA { get; set; }
        public StaticRS DependingRS
        {
            get
            {
                return mDependingRS;
            }
            set
            {
                mDependingRS = value;
                if (value != null)
                    mDependingRS.AfterExecution += mDependingRS_AfterExecution;
            }
        }

        void mDependingRS_AfterExecution(object sender, EventArgs e)
        {
            Text = mDependingRS[DBField].ToString();
        }
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
                CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight);
            }
        }
        //List<StaticRS> ExternalControls;//list of possible external controls, the key is the parameter name and the object is the control
        //List<EspackControl> DependingControls; //list of those controls which have me as external control


        //void AddExternalControl(string pParameterName, StaticRS pRS)
        //{
        //    EspackControlType &= EspackControlTypeEnum.DEPENDANT;
        //    ExternalControls.Add(pParameterName, pRS);
        //    foreach (ControlParameter lControl in pRS.ControlParameters)
        //    {
        //        if (lControl.LinkedControl is EspackFormControl)
        //        {
        //            ((EspackFormControl)lControl.LinkedControl).DependingControls.Add(this);
        //        }
        //    }
        //}


        public EspackMaskedTextBox()
            : base()
        {
            CaptionLabel = new EspackLabel("", this) { AutoSize = true };
            var _m = new Padding();
            _m = base.Margin;
            _m.Top = 16;
            base.Margin = _m;
            EspackTheme.changeControlFormat(this);
        }

        ~EspackMaskedTextBox()
        {
            CaptionLabel.Dispose();
            CaptionLabel = null;
        }
        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent != null)
            {
                Parent.Controls.Add(CaptionLabel);
                base.OnParentChanged(e);
            }
        }

        protected override void OnMove(EventArgs e)
        {
            CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight);
            //CaptionLabel.Location = new Point(Location.X - CaptionLabel.PreferredWidth - 6, Location.Y);
            base.OnMove(e);
        }

        public void UpdateEspackControl()
        {
            if ((EspackControlType & EspackControlTypeEnum.CTLM) == EspackControlTypeEnum.CTLM)
            {
                Text = ParentDA.SelectRS[DBField.ToString()].ToString();
            }
        }

        //public void OnTextChanged(EventArgs e)
        //{
        //    foreach (EspackControl lControl in DependingControls)
        //    {
        //        lControl.UpdateEspackControl(EspackControlTypeEnum.DEPENDANT);
        //    }
        //}

        public void ClearEspackControl()
        {
            Text = "";
        }
    }

    public class NumericTextBox : EspackTextBox
    {

        bool allowSpace = false;
        public NumericTextBox()
            : base()
        {
            this.TextAlign = HorizontalAlignment.Right;
        }

        //Restricts the entry of characters to digits (including hex), the negative sign,
        //the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();
            if (Length == 0 || Text.Length < Length + (Precision == 0 ? 0 : Precision + 1))
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    // Digits are OK
                }
                else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
                 keyInput.Equals(negativeSign))
                {
                    // Decimal separator is OK
                }
                else if (e.KeyChar == '\b')
                {
                    // Backspace key is OK
                }
                //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
                //    {
                //     // Let the edit control handle control and alt key combinations
                //    }
                else if (this.allowSpace && e.KeyChar == ' ')
                {

                }
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        public int IntValue
        {
            get
            {
                return Int32.Parse(this.Text);
            }
        }

        public decimal DecimalValue
        {
            get
            {
                return Decimal.Parse(this.Text);
            }
        }

        public new object Value
        {
            get
            {
                if (Text == "")
                {
                    return null;
                }
                else
                    if (Precision != 0)
                    {
                        return double.Parse(this.Text == "." ? "0.0" : this.Text);
                    }
                    else
                    {
                        return Int32.Parse(this.Text == "." ? "0.0" : this.Text);
                    }

            }
            set
            {
                Text = Value == null ? "" : Value.ToString();
            }
        }

        public int Precision { get; set; }
        public int Length { get; set; }
        public bool Mask { get; set; }
        public bool ThousandsGroup { set; get; }
        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }

        //protected override void OnTextChanged(System.EventArgs e)
        //{
        //    if (ThousandsGroup) {
        //        Text = decimal.Parse(Text).ToString("N");
        //    } 
        //}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
        public void ClearControl()
        {
            Value=0;
        }

    }

    public class EspackDateTimePicker : DateTimePicker, EspackFormControl
    {
        public EspackControlTypeEnum EspackControlType { get; set; }
        public EspackLabel CaptionLabel { get; set; }
        public cAccesoDatosNet ParentConn { get; set; }
        public StaticRS DependingRS { get; set; }
        private EnumStatus mStatus;
        public EnumStatus Status
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
                Enabled = (Add && Status == EnumStatus.ADDNEW) || (Upp && Status == EnumStatus.EDIT && !PK) || (Del && Status == EnumStatus.DELETE) || (Search && Status == EnumStatus.SEARCH);
            }
        }

        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                if (CaptionLabel != null)
                    CaptionLabel.Visible = value;
            }
        }

        public DA ParentDA { get; set; }
        public new object Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = (DateTime)value;
            }
        }

        public string DBField { get; set; }
        public bool Add { get; set; }
        public bool Upp { get; set; }
        public bool Del { get; set; }
        public int Order { get; set; }
        public bool PK { get; set; }
        public bool Search { get; set; }
        public object DefaultValue { get; set; }
        public Type DBFieldType { get; set; }

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
                CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight);
            }
        }

        public EspackDateTimePicker()
        {
            CaptionLabel = new EspackLabel("", this) { AutoSize = true };
            Format = DateTimePickerFormat.Custom;
            CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern‌ + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern‌;
            Size = new Size(130, 20);
            var _m = new Padding();
            _m = base.Margin;
            _m.Top = 16;
            base.Margin = _m;
            EspackTheme.changeControlFormat(this);
        }

        public void UpdateEspackControl()
        {
            Text = ParentDA.SelectRS[DBField.ToString()].ToString();
        }
        public void ClearEspackControl()
        {
            base.Value = DefaultValue == null ? DateTime.Today : (DateTime)DefaultValue; 
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent != null && CaptionLabel != null)
            {
                Parent.Controls.Add(CaptionLabel);
                base.OnParentChanged(e);
            }
        }

        protected override void OnMove(EventArgs e)
        {
            //CaptionLabel.Location = new Point(Location.X - CaptionLabel.PreferredWidth - 6, Location.Y);
            CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight);
            base.OnMove(e);
        }
        // to add BorderStyle

        private Color _borderColor = Color.Black;
        private ButtonBorderStyle _borderStyle = ButtonBorderStyle.Solid;
        private static int WM_PAINT = 0x000F;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                Graphics g = Graphics.FromHwnd(Handle);
                Rectangle bounds = new Rectangle(0, 0, Width, Height);
                ControlPaint.DrawBorder(g, bounds, _borderColor, _borderStyle);
            }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Invalidate(); // causes control to be redrawn
            }
        }

        [Category("Appearance")]
        public ButtonBorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                _borderStyle = value;
                Invalidate();
            }
        }

    }

    public class EspackString : EspackFormControl
    {
        public EspackControlTypeEnum EspackControlType { get; set; }
        private string theString { get; set; }
        public EnumStatus Status { get; set; }
        public StaticRS DependingRS { get; set; }
        public cAccesoDatosNet ParentConn { get; set; }
        public Point Location { get; set; }
        public object Value
        {
            get
            {
                return theString;
            }
            set
            {
                theString = value.ToString();
            }
        }

        public string DBField { get; set; }
        public bool Add { get; set; }
        public bool Upp { get; set; }
        public bool Del { get; set; }
        public int Order { get; set; }
        public bool PK { get; set; }
        public bool Search { get; set; }
        public object DefaultValue { get; set; }
        public Type DBFieldType { get; set; }

        public event EventHandler TextChanged;

        public void UpdateEspackControl()
        {
            theString = ParentDA.SelectRS[DBField.ToString()].ToString();
        }
        public void ClearEspackControl()
        {
            theString = DefaultValue != null ? "" : DefaultValue.ToString();
        }
        public EspackLabel CaptionLabel { get; set; }
        public DA ParentDA { get; set; }
        public string Caption { get; set; }

        public string Text
        {
            get
            {
                return theString;
            }

            set
            {
                theString = value; ;
            }
        }
    }


    public class EspackCheckedListBox : CheckedListBox, EspackFormControl
    {
        public EspackControlTypeEnum EspackControlType { get; set; }
        public EspackLabel CaptionLabel { get; set; }
        public StaticRS DependingRS { get; set; }
        public cAccesoDatosNet ParentConn { get; set; }
        private EnumStatus mStatus;
        private DynamicRS _RS;
        private string _SQL;
        private bool noChange = false;
        public event EventHandler<EventArgs> Changed;
        public event EventHandler AfterItemCheck;

        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                if (CaptionLabel != null)
                    CaptionLabel.Visible = value;
            }
        }

        //Default event handler for CTLM
        private void DefaultEventChanged(object source, EventArgs e)
        {
            return;
        }

        public EnumStatus Status
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
                Enabled = (Add && Status == EnumStatus.ADDNEW) || (Upp && Status == EnumStatus.EDIT && !PK) || (Del && Status == EnumStatus.DELETE) || (Search && Status == EnumStatus.SEARCH);
            }
        }

        public DA ParentDA { get; set; }
        public object Value
        {
            get
            {
                string _result = ListJoin;

                return _result=="" ? "": "|"+_result+"|";
                //return base.Text;
            }
            set
            {
                if (value != null)
                {
                    base.Text = value.ToString();
                    //UpdateEspackControl();
                }
            }
        }
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
                CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight);
            }
        }

        public string DBField { get; set; }
        public bool Add { get; set; }
        public bool Upp { get; set; }
        public bool Del { get; set; }
        public int Order { get; set; }
        public bool PK { get; set; }
        public bool Search { get; set; }
        public object DefaultValue { get; set; }
        public Type DBFieldType { get; set; }

        //own properties
        //public DynamicRS DataSource { get; set; }
        //public string DisplayMember { get; set; }
        //public string ValueMember { get; set; }



        public EspackCheckedListBox()
            :base()
        {
            CaptionLabel = new EspackLabel("", this) { AutoSize = true };
            CheckOnClick = true;
            //Format = DateTimePickerFormat.Custom;
            //CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern‌ + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern‌;
            var _m = new Padding();
            _m = base.Margin;
            _m.Top = 16;
            base.Margin = _m;
            EspackTheme.changeControlFormat(this);
            Changed += DefaultEventChanged;
            AfterItemCheck += delegate
             {
                 if (noChange == false)
                 {
                     EventArgs _ev = new EventArgs();
                     Changed(this, _ev);
                 }
             };
        }
        ~EspackCheckedListBox()
        {
            CaptionLabel.Dispose();
            CaptionLabel = null;
        }

        public void Source(string pSQL, cAccesoDatosNet pConn)
        {
            noChange = true;
            _SQL = pSQL;
            _SQL = pSQL;
            _RS = new DynamicRS(_SQL, pConn);
            DataSource = _RS.DataObject;
            DisplayMember = _RS.Fields[1];
            if (_RS.FieldCount > 1)
                ValueMember = _RS.Fields[0];
            SelectedItem = null;
            noChange = false;
        }
        //public void Load()
        //{
        //    while (!_RS)
        //}

        public void Source(string pSql)
        {
            Source(pSql, ParentConn);
        }
        protected override void OnItemCheck(ItemCheckEventArgs e)
        {
            base.OnItemCheck(e);

            EventHandler handler = AfterItemCheck;
            if (handler != null)
            {
                Delegate[] invocationList = AfterItemCheck.GetInvocationList();
                foreach (var receiver in invocationList)
                {
                    AfterItemCheck -= (EventHandler)receiver;
                }

                SetItemCheckState(e.Index, e.NewValue);

                foreach (var receiver in invocationList)
                {
                    AfterItemCheck += (EventHandler)receiver;
                }
            }
            OnAfterItemCheck(EventArgs.Empty);
        }

        public void OnAfterItemCheck(EventArgs e)
        {
            EventHandler handler = AfterItemCheck;
            if (handler != null)
                handler(this, e);
        }

        //protected override void OnItemCheck(ItemCheckEventArgs e)
        //{
        //    base.OnItemCheck(e);
        //    EventHandler handler = AfterItemCheck;
        //    if (handler != null)
        //        SetItemCheckState(e.Index, e.NewValue);
        //    if (noChange == false)
        //    {
        //        EventArgs _ev = new EventArgs();
        //        Changed(this, _ev);
        //    }
        //}


        public void UpdateEspackControl()
        {
            var _old = Value;
            noChange = true;
            ClearSelected();
            if (ParentDA != null)
                Text = ParentDA.SelectRS[DBField.ToString()].ToString();
            for (var i = 0; i < Items.Count; i++)
            {
                SetItemChecked(i, false);
                foreach (var item in Text.Split('|'))
                {
                    var r = ((DataRowView)Items[i]).Row;
                    var _l = r[ValueMember].ToString();
                    if (_l==item)
                    {
                        SetItemChecked(i, true);
                        break;
                    }
                }
            }
            if (Value!=_old)
            {
                EventArgs _ev = new EventArgs();
                Changed(this,_ev);
            }
            noChange = false;
        }

        public string keyItem(int _index)
        {
            var r = ((DataRowView)Items[_index]).Row;
            return r[ValueMember].ToString();
        }
        public int indexItem(string key)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var r = ((DataRowView)Items[i]).Row;
                var _l = r[ValueMember].ToString();
                if (_l == key)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool itemStatus(string key)
        {
            return GetItemChecked(indexItem(key));
        }
        public bool itemStatus(int index)
        {
            return GetItemChecked(index);
        }

        public void CheckItem(string key)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var r = ((DataRowView)Items[i]).Row;
                var _l = r[ValueMember].ToString();
                if (_l == key)
                {
                    SetItemChecked(i, true);
                    break;
                }
            }
        }

        public void UnCheckItem(string key)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var r = ((DataRowView)Items[i]).Row;
                var _l = r[ValueMember].ToString();
                if (_l == key)
                {
                    SetItemChecked(i, false);
                    break;
                }
            }
        }

        public void ClearEspackControl()
        {
            noChange = true;
            for (var i = 0; i < Items.Count; i++)
            {
                SetItemChecked(i, false);
            }
            noChange = false;
            EventArgs _ev = new EventArgs();
            Changed(this, _ev);
        }
        public List<string> CheckedValues
        {
            get
            {
                IEnumerable<string> l = (from DataRowView item in CheckedItems select item.Row[ValueMember].ToString());
                return l.ToList<string>();
            }
        }
        private string ListJoin
        {
            get
            {
                return string.Join("|", CheckedValues);
            }
            
        }
        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent != null && CaptionLabel != null)
            {
                Parent.Controls.Add(CaptionLabel);
                base.OnParentChanged(e);
            }
        }

        protected override void OnMove(EventArgs e)
        {
            //CaptionLabel.Location = new Point(Location.X - CaptionLabel.PreferredWidth - 6, Location.Y);
            CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight);
            base.OnMove(e);
        }


    }

    public class EspackComboBox : ComboBox, EspackFormControl
    {
        public EspackControlTypeEnum EspackControlType { get; set; }
        public EspackLabel CaptionLabel { get; set; }
        public StaticRS DependingRS { get; set; }
        public cAccesoDatosNet ParentConn { get; set; }
        private EnumStatus mStatus;
        private DynamicRS _RS;
        private string _SQL;
        public EspackTextBox TBDescription { get; set; }

        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                if (CaptionLabel != null)
                    CaptionLabel.Visible = value;
            }
        }

        private Size _size;

        //public new Size Size
        //{
        //    get
        //    {
        //        return _size;
        //    }
        //    set
        //    {
        //        _size = value;
        //        base.Height = value.Height - CaptionLabel.Height;
        //        base.Width = value.Width;
        //    }
        //}
        //public new Point Location
        //{
        //    get
        //    {
        //        if (CaptionLabel != null)
        //        {
        //            var _l = new Point();
        //            _l.X = base.Location.X;
        //            _l.Y = base.Location.Y - CaptionLabel.Height;
        //            return _l;
        //        }
        //        else return base.Location;
        //    }
        //    set
        //    {
        //        var gap = 0;
        //        if (CaptionLabel != null)
        //        {
        //            CaptionLabel.Location = value;
        //            gap = CaptionLabel.Height;
        //        }

        //        var _l = new Point();
        //        _l.X = value.X;
        //        _l.Y = value.Y + gap;
        //        base.Location = _l;

        //    }
        //}
        //[DefaultValue(typeof(Padding), "3, 3, 3, 3")]
        //public new Padding Margin
        //{
        //    get
        //    {
        //        var gap = 0;
        //        if (CaptionLabel != null)
        //            gap = CaptionLabel.Height;
        //        else
        //            gap = 10;
        //        var _m = new Padding();
        //        _m = base.Margin;
        //        //Text = base.Margin.Top.ToString();
        //        _m.Top -= gap;
        //        return _m;
        //    }
        //    set
        //    {
        //        var gap = 0;
        //        if (CaptionLabel != null)
        //            gap = CaptionLabel.Height;
        //        var _m = new Padding();
        //        _m = value;
        //        _m.Top += gap;
        //        base.Margin = _m;
        //    }
        //}
        public EnumStatus Status
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
                Enabled = (Add && Status == EnumStatus.ADDNEW) || (Upp && Status == EnumStatus.EDIT && !PK) || (Del && Status == EnumStatus.DELETE) || (Search && Status == EnumStatus.SEARCH);
            }
        }

        public DA ParentDA { get; set; }
        public object Value
        {
            get
            {
                return Text == "System.Data.DataRowView" ? "" : Text;
            }
            set
            {
                if (value!= null)
                {
                    Text = value.ToString();
                } 
            }
        }

        public string DBField { get; set; }
        public bool Add { get; set; }
        public bool Upp { get; set; }
        public bool Del { get; set; }
        public int Order { get; set; }
        public bool PK { get; set; }
        public bool Search { get; set; }
        public object DefaultValue { get; set; }
        public Type DBFieldType { get; set; }

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
                CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight);
            }
        }

        public EspackComboBox()
        {
            CaptionLabel = new EspackLabel("", this) { AutoSize = true };
            Text = "";
            //Format = DateTimePickerFormat.Custom;
            //CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern‌ + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern‌;
            Size = new Size(130, 20);
            var _m = new Padding();
            _m = base.Margin;
            _m.Top = 16;
            base.Margin = _m;
            AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteSource = AutoCompleteSource.ListItems;
            this.SelectedValueChanged += delegate
            {
                if (TBDescription!= null)
                {
                    if (ValueMember!= null && SelectedValue!=null)
                    {
                        TBDescription.Text = SelectedValue.ToString();
                    }
                    else
                    {
                        TBDescription.Text = "";
                    }
                }
            };
            this.FlatStyle = FlatStyle.Flat;
            EspackTheme.changeControlFormat(this);
        }

        public void Source(string pSQL, cAccesoDatosNet pConn)
        {
            
            _SQL = pSQL;
            _RS = new DynamicRS(_SQL, pConn);
            DataSource = _RS.DataObject;
            DisplayMember = _RS.Fields[0];
            if (_RS.FieldCount > 1)
                ValueMember = _RS.Fields[1];
            SelectedItem = null;
            //Text = "...";
            //Value = "";
        }

        public void Source(string pSql)
        {
            Source(pSql, ParentConn);
        }
        public void Source(string pSql, EspackTextBox pTB)
        {
            Source(pSql, ParentConn);
            TBDescription = pTB;
        }
        public void UpdateEspackControl()
        {
            Text = ParentDA.SelectRS[DBField.ToString()].ToString();
        }
        public void ClearEspackControl()
        {
            base.SelectedItem =null ;
            if (TBDescription!= null)
            {
                TBDescription.Text = "";
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent != null && CaptionLabel != null)
            {
                Parent.Controls.Add(CaptionLabel);
                base.OnParentChanged(e);
            }
        }

        protected override void OnMove(EventArgs e)
        {
            //CaptionLabel.Location = new Point(Location.X - CaptionLabel.PreferredWidth - 6, Location.Y);
            CaptionLabel.Location = new Point(base.Location.X, base.Location.Y - CaptionLabel.PreferredHeight);
            base.OnMove(e);
        }

        ////to be able to change borderstyle

        //private Color _borderColor = Color.Black;
        //private ButtonBorderStyle _borderStyle = ButtonBorderStyle.Solid;
        //private static int WM_PAINT = 0x000F;

        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);

        //    if (m.Msg == WM_PAINT)
        //    {
        //        Graphics g = Graphics.FromHwnd(Handle);
        //        Rectangle bounds = new Rectangle(0, 0, Width, Height);
        //        ControlPaint.DrawBorder(g, bounds, _borderColor, _borderStyle);
        //    }
        //}

        //[Category("Appearance")]
        //public Color BorderColor
        //{
        //    get { return _borderColor; }
        //    set
        //    {
        //        _borderColor = value;
        //        Invalidate(); // causes control to be redrawn
        //    }
        //}

        //[Category("Appearance")]
        //public ButtonBorderStyle BorderStyle
        //{
        //    get { return _borderStyle; }
        //    set
        //    {
        //        _borderStyle = value;
        //        Invalidate();
        //    }
        //}


    }

}
