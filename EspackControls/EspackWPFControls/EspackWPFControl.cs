using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatosNet;
using EspackControls;
//using System.Windows;
using System.Windows.Controls;
//using System.Drawing;
using CommonTools;
using System.Windows;
using System.Windows.Media;

namespace EspackWPFControls
{
    public interface EspackWPFControl : EspackControl
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

    public class EspackLabel : Label
    {
        EspackWPFControl ParentControl { get; set; }
        public string Caption
        {
            get
            {
                return Content.ToString();
            }
            set
            {
                Content = value;
                if (ParentControl != null)
                {
                    Canvas.SetTop(this,60);
                    Canvas.SetLeft(this, ParentControl.Location.Y);
                    //Margin = new Thickness(60, ParentControl.Location.Y);//ParentControl.Location.X - PreferredWidth - 6
                }
            }
        }
        public Point Location
        {
            get
            {
                return new Point(Margin.Left, Margin.Top);
            }
            set
            {
                var _pos = new Thickness(value.X, value.Y, 0, 0);
                Margin = _pos;
            }
        }
        public EspackLabel(string pCaption, EspackWPFControl pParentControl)
        {
            ParentControl = pParentControl;
            Caption = pCaption;
            Margin = new System.Windows.Thickness(0, 0, 0, 0);
        }



    }

    public class EspackWPFTextBox : TextBox, EspackWPFControl
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
        public Point Location
        {
            get
            {
                return new Point(Margin.Left, Margin.Top);
            }
            set
            {
                var _pos = new Thickness(value.X, value.Y, 0, 0);
                Margin = _pos;
            }
        }
        //public new Thickness Margin
        //{
        //    get
        //    {
        //        return Margin;
        //    }
        //    set
        //    {
        //        Margin = value;
        //        //if (CaptionLabel!= null)
        //          //  CaptionLabel.Margin = new Thickness(Location.X, Location.Y - CaptionLabel.ActualHeight,0,0);
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
                IsEnabled = (Add && Status == EnumStatus.ADDNEW) || (Upp && Status == EnumStatus.EDIT && !PK) || (Del && Status == EnumStatus.DELETE) || (Search && Status == EnumStatus.SEARCH);
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
                    ((Grid)Parent).Children.Add(CaptionLabel);
                }
                CaptionLabel.Caption = value;
                //CaptionLabel.Location = new Point(Location.X - CaptionLabel.PreferredWidth - 6, Location.Y);
                CaptionLabel.Location = new Point(Location.X, Location.Y - CaptionLabel.DesiredSize.Height);
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


        public EspackWPFTextBox()
            : base()
        {
            CaptionLabel = new EspackLabel("", this);// { AutoSize = true };
            //var _m = new Padding();
            //_m = base.Margin;
            //_m.Top = 16;
            //base.Margin = _m;
            //Margin = _m;
            //EspackTheme.changeControlFormat(this);
            //LayoutUpdated += EspackTextBox_LayoutUpdated;
            
        }

        event EventHandler IsValuable.TextChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            CaptionLabel.Location = new Point(Location.X, Location.Y - ActualHeight - 2);
            base.OnRender(drawingContext);
        }

        //private void EspackTextBox_LayoutUpdated(object sender, EventArgs e)
        //{
        //    CaptionLabel.Location = new Point(Location.X,Location.Y - CaptionLabel.ActualHeight);
        //    //base.LayoutUpdated(object, e);
        //}

        ~EspackWPFTextBox()
        {
            if (CaptionLabel != null)
                //CaptionLabel.Dispose();
                CaptionLabel = null;
        }
        //protected override void OnVisualParentChanged(DependencyObject oldParent)
        //{
        //    if (Parent != null)
        //    {
        //        Parent.Controls.Add(CaptionLabel);
        //        base.OnParentChange(e);
        //    }
        //}


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
}
/*

        public interface EspackFormControl : EspackControl
    {
        EspackLabel CaptionLabel { get; set; }
        string Caption { get; set; }
        DA ParentDA { get; set; }
        cAccesoDatosNet ParentConn { get; set; }
        StaticRS DependingRS { get; set; }
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
        EspackControl ParentControl { get; set; }
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
        public EspackLabel(string pCaption, EspackControl pParentControl)
        {
            ParentControl = pParentControl;
            Caption = pCaption;
            Margin = new Padding(0, 0, 0, 0);
        }
    }


    */
