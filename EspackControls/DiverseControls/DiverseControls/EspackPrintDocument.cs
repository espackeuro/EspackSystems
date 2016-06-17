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
                    return Graphics.MeasureString(_item.Replace(' ', '@'), Font).Height;
                else return 0;
            }
        }
        public float Width
        {
            get
            {
                if (Graphics != null)
                    return Graphics.MeasureString(_item.Replace(' ', '@') + '@', Font).Width;
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
        //public float x { get; set; }
        //public float y { get; set; }

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
                if (Things.Count != 0)
                    return Things.Max(x => x.Height);
                else return 0;
            }
        }
        public float Width
        {
            get
            {
                if (Things.Count != 0)
                    return Things.Sum(x => x.Width);
                else return 0;
            }
        }
        public bool Banding { get; set; } = false;
        public int LineNumber { get; set; }
    }

    public class PrintableLineList
    {
        private Graphics _g;
        public int LastPrintedLine { get; set; } = 0;
        public List<PrintableLine> Lines { get; set; } = new List<PrintableLine>();
        public Graphics Graphics
        {
            get
            {
                return _g;
            }
            set
            {
                _g = value;
                Lines.ForEach(x => x.Graphics = value);
            }
        }
        public float MaxHeight
        {
            get
            {
                if (Lines.Count != 0)
                    return Lines.Select(x => x.Height).Max();
                else
                    return 0;
            }
        }
        public float MaxWidth
        {
            get
            {
                if (Lines.Count != 0)
                    return Lines.Select(x => x.Width).Max();
                else
                    return 0;
            }
        }

    }

    public class EspackPrintDocument:PrintDocument
    {
        public float CurrentX { get; set; }
        public float CurrentY { get; set; }
        public PrintableLineList LineList { get; set; } = new PrintableLineList();
        public PrintableLine CurrentLine
        {
            get
            {
                return LineList.Lines[CurrentLineIndex];
            }
        }
        public int CurrentLineIndex { get; set; }
        
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

            LineList.Lines.Add(new PrintableLine());
            CurrentLineIndex = 0;
        }
        public void NewLine(bool pBanding = false)
        {
            var _line = new PrintableLine() { Banding = pBanding, LineNumber= LineList.Lines.Count };
            _line.Add(new PrintableText(" ", CurrentFont));
            LineList.Lines.Add(_line);
            CurrentLineIndex = LineList.Lines.Count - 1;
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

            //LineList.Lines[0].x = _x;
            //LineList.Lines[0].y = _y;
            //var query = LineList.Lines.TakeWhile(x => x.y <= YMax );

            //query.ToList().ForEach(x =>
            //{
            //    _y+=200;
            //    LineList.Lines[LineList.LastPrintedLine++].y = _y;
            //});


            // RAFA, no ha habido manera... ya harás tu las pruebas con el TakeWhile a ver si lo consigues tú.
            while (_y <= YMax && LineList.LastPrintedLine < LineList.Lines.Count-1 )
            {
                var l = LineList.Lines[LineList.LastPrintedLine];
                l.Graphics = g;
                if (l.Banding && (l.LineNumber % 2 == 0))
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), XMin, _y, l.Width, l.Height);
                l.Things.ForEach(t =>
                {
                    t.Draw(_x, _y);
                    _x += t.Width;
                });

                _y += l.Height;
                _x = XMin;
                LineList.LastPrintedLine++;
            }

            e.HasMorePages = (_y > YMax && LineList.LastPrintedLine < LineList.Lines.Count - 1);

            base.OnPrintPage(e);
        }
        
    }
}
