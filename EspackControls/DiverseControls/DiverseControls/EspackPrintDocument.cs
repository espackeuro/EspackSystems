using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseControls
{
    public interface PrintableThing
    {
        object Item { get; set; }
        Font Font { get; set; }
        Brush Brush { get; set; }
        Graphics Graphics { get; set; }
        float Height { get;  }
        float Width { get;  }
        void Draw(float x, float y);
    }

    public class PrintableText : PrintableThing
    {
        private string _item;
        public object Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value.ToString();
            }
        }
        public Font Font { get; set; }
        public Brush Brush { get; set; }
        public Graphics Graphics { get; set; }
        public float Height
        {
            get
            {
                if (Graphics != null)
                    return Graphics.MeasureString(_item, Font).Height;
                else return 0;
            }
        }
        public float Width
        {
            get
            {
                if (Graphics != null)
                    return Graphics.MeasureString(_item, Font).Width;
                else return 0;
            }
        }
        public PrintableText(string pText, Font pFont)
        {
            _item = pText;
            Font = pFont;
            Brush = new SolidBrush(Color.Black);
        }
        public void Draw(float x, float y)
        {
            if ( Graphics!= null)
            {
                Graphics.DrawString(_item, Font, Brush, x, y);
            }
        }
    }

    public class PrintableLine
    {
        public List<PrintableThing> Things { get; set; } = new List<PrintableThing>();
        public void Add(PrintableThing pThing)
        {
            Things.Add(pThing);
        }
        public PrintableThing this[int index]
        {
            get
            {
                return Things[index];
            }
        }
        public bool Empty { get; set; } = true;
        private Graphics _g;
        public Graphics Graphics
        {
            get
            {
                return _g;
            }
            set
            {
                _g = value;
                Things.ForEach(x => x.Graphics = value);
            }
        }
        public float Height
        {
            get
            {
                return Things.Max(x => x.Height);
            }
        }
        public float Width
        {
            get
            {
                return Things.Sum(x => x.Width);
            }
        }
    }

    public class EspackPrintDocument:PrintDocument
    {
        public float CurrentX { get; set; }
        public float CurrentY { get; set; }
        public PrintableLine CurrentLine
        {
            get
            {
                return Lines[CurrentLineIndex];
            }
        }
        public int CurrentLineIndex { get; set; }
        public List<PrintableLine> Lines { get; set; } = new List<PrintableLine>();
        public Font CurrentFont { get; set; }
        public Brush CurrentBrush { get; set; }

        public float XMin
        {
            get
            {
                return DefaultPageSettings.PrintableArea.Left;
            }
        }
        public float XMax
        {
            get
            {
                return DefaultPageSettings.PrintableArea.Right;
            }
        }

        public int YMin
        {
            get
            {
                return Convert.ToInt32(DefaultPageSettings.PrintableArea.Top);
            }
        }

        public int YMax
        {
            get
            {
                return Convert.ToInt32(DefaultPageSettings.PrintableArea.Bottom);
            }
        }
        public EspackPrintDocument() : base()
        {
            CurrentX = XMin;
            CurrentY = YMin;
            
            Lines.Add(new PrintableLine());
            CurrentLineIndex = 0;
        }
        public void NewLine()
        {
            var _line = new PrintableLine();
            _line.Add(new PrintableText(" ",CurrentFont));
            Lines.Add(_line);
            CurrentLineIndex = Lines.Count - 1;
        }
        public void Add(PrintableThing pThing)
        {
            if (CurrentLine.Empty)
            {
                CurrentLine.Things.RemoveAt(0);
                CurrentLine.Empty = false;
            }
                
            CurrentLine.Add(pThing);
            CurrentFont = pThing.Font;

        }
        public void Add(string pText)
        {
            var _thing = new PrintableText(pText, CurrentFont);
            Add(_thing);
        }
        public void Add(string pText, Font pFont)
        {
            var _thing = new PrintableText(pText, pFont);
            Add(_thing);
            CurrentFont = pFont;
        }
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            var g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            float _x = XMin;
            float _y = YMin;
            Lines.ForEach(l => 
            {
                l.Graphics = g;
                l.Things.ForEach(t =>
                {
                    t.Draw(_x, _y);
                    _x += t.Width;
                });
                _y += l.Height;
            });
            base.OnPrintPage(e);
        }
        
    }
}
