using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;

namespace EspackClasses
{
    enum BarcodeTypes { CODE39, CODE128 }

    public interface printerItem : ICloneable
    {
        float x { get; set; }
        float y { get; set; }
        Size Size { get; }
        string textData { get; set; }
        cLabel Parent { get; set; }
        float Height { get; set; }
        float Width { get; }
        string align { get; set; }
        //new printerItem Clone();
    }

    public class printerBarcode : printerItem
    {

        public float x { get; set; }
        public float y { get; set; }
        public Size Size
        {
            get
            {
                return new Size(Convert.ToInt32(Width * Parent.dpm), Convert.ToInt32(Height * Parent.dpm));
            }
        }
        public string textData { get; set; }
        public cLabel Parent { get; set; }
        public bool HumanReadable { get; set; }
        public float Height { get; set; }
        public float Width { get
            {
                return (11 * textData.Length + 35) * xDimension;
            }

        }
        public string align { get; set; }
        public float WidthMult { get; set; }
        public float xDimension { get; set; }
        public bool Rotated { get; set; }
        //constructor
        public printerBarcode(cLabel pParent)
        {
            Parent = pParent;
        }
        object ICloneable.Clone()
        {
            return Clone() ;
        }
        public printerBarcode Clone()
        {
            return (printerBarcode)this.MemberwiseClone();
        }
    }
    public class printerLine: printerItem
    {
        public float x { get; set; }
        public float y { get; set; }
        public float textSize { get; set; }
        public string align { get; set; }
        public string style { get; set; }
        public string textData { get; set; }
        public float charSize { get; set; }
        public bool Rotated { get; set; }
        public cLabel Parent { get; set; }
        public Size Size
        {
            get
            {
                return new Size(Convert.ToInt32(Width * Parent.dpm), Convert.ToInt32(Height * Parent.dpm));
            }
        }
        public float Height { get; set; }
        public float Width { get; set; }

        //constructor
        public printerLine(cLabel pParent)
        {
            Parent = pParent;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
            //return new printerLine() {
            //    x = x,
            //    y = y,
            //    textSize = textSize,
            //    align = align,
            //    style = style,
            //    textData = textData,
            //    charSize = charSize
            //};
        }
        public printerLine Clone()
        {
            return (printerLine)this.MemberwiseClone();
        }
    }
    public abstract class cLabel
    {
        public float width { get; set; }
        public float height { get; set; }
        public Size Size { get; set; }
        protected float gap { get; set; }
        public int dpi { get; set; }
        public float dpm { get; set; }
        protected int qty { get; set; }
        protected virtual List<string> labelHeader { get; } 
        protected virtual List<string> labelFooter { get; } 
        protected List<printerItem> labelBody { get; set; } = new List<printerItem>();
        public override string ToString()
        {
            //return base.ToString();
            return string.Join("", labelHeader) + string.Join("", labelBody.Select(x => renderLine(x))) + string.Join("", labelFooter);
        }

        public void Clear()
        {
            labelBody.Clear();
        }

        public string ToString(Dictionary<string,string> pParameters, int pQty=1)
        {

            List<printerItem> _replacedList = new List<printerItem>();
            //labelBody.ForEach(l => _replacedList.Add(new printerLine()
            //{
            //    x = l.x,
            //    y = l.y,
            //    textSize = l.textSize,
            //    align = l.align,
            //    style = l.style,
            //    textData = l.textData,
            //    charSize = l.charSize
            //}));
            _replacedList = labelBody.Select(x => (printerItem)x.Clone()).ToList();
            pParameters.ToList().ForEach(p =>
            {
                _replacedList.Where(x => x.textData.IndexOf("[")!=-1).ToList().ForEach(x =>
                {
                    x.textData = x.textData.Replace("["+p.Key.ToUpper()+"]", p.Value);
                });
            });
            qty = pQty;
            return string.Join("", labelHeader) + string.Join("", _replacedList.Select(x => renderLine(x))) + string.Join("", labelFooter);
        }

        public virtual void addLine(float x, float y, float textSize, string align, string style, string textData, float charSize = 0, float barcodeHeight=0, float barcodeWidthMult=0, bool humanReadable=false, bool rotated=false)
        {
            if (textData.Length > 4 && textData.Substring(0, 4) == "[BC]")
            {
                labelBody.Add(new printerBarcode(this)
                {
                    x = x,
                    y = y,
                    Height = barcodeHeight,
                    align = align,
                    WidthMult = barcodeWidthMult,
                    xDimension = Convert.ToInt32(Math.Ceiling(barcodeWidthMult * 2)),
                    textData = textData.Replace("[BC]", ""),
                    HumanReadable = humanReadable,
                    Rotated = rotated
                });
            }
            else
            {
                labelBody.Add(new printerLine(this)
                {
                    x = x,
                    y = y,
                    textSize = textSize,
                    align = align,
                    style = style,
                    textData = textData,
                    charSize = charSize,
                    Rotated = rotated
                });
            }
        }
        public virtual string renderLine(printerItem p)
        {
            return "";
        }
        public cLabel(float pwidth, float pheight, float pgap, int pdpi)
        {
            width = pwidth;
            height = pheight;
            Size = new Size(Convert.ToInt32(pwidth * dpm), Convert.ToInt32(pheight * dpm));
            gap = pgap;
            dpi = pdpi;
            dpm = (dpi / 25.4F);
        }
    };
 
    public class ZPLLabel: cLabel
    {
        public ZPLLabel(float pwidth, float pheight, float pgap, int pdpi) : base(pwidth, pheight, pgap, pdpi)
        {
        }

        protected override List<string> labelHeader
        {
            get
            {
                return (new List<string>() { "^XA^CWX,E:TT0003M_.FNT^XZ", "^XA", "^CI28", "^LH0,0" });
            }
        }

        protected override List<string> labelFooter
        {
            get
            {
                return (new List<string>() { string.Format("^PQ{0},0,0,N", qty), "^XZ" });
            }
        }


        public override string renderLine(printerItem p)
        {
            if (p is printerLine)
            {
                var _p = ((printerLine)p);
                List<string> _result = new List<string>();
                int _xdpm = Convert.ToInt32(_p.x * dpm);
                int _ydpm = Convert.ToInt32(_p.y * dpm);


                if (_p.textSize == 0)
                {
                    Font _font = new Font("Swis721 BT", _p.charSize);
                    _p.textSize = TextMeasurer.MeasureString(_p.textData, _font, dpi).Width; //* 1.7F;
                }
                else
                {
                    for (float i = 1; i < 100; i += 0.1F)
                    {
                        Font _font = new Font("Swis721 BT", i);
                        if (TextMeasurer.MeasureString(_p.textData, _font, dpi).Width > _p.textSize)
                        {
                            _p.charSize = i;
                            break;
                        }
                    }
                }
                float _charSize_mm = _p.charSize * 0.3527777777778F;
                //textSize = textSize == 0F ? ): textSize;
                float _width = _p.textSize * dpm;// = 2.1F * (textSize - 1);
                int _charWidthDPM = _charSize_mm == 0 ? Convert.ToInt32(Math.Ceiling(_p.textSize / _p.textData.Length * dpm)) : Convert.ToInt32(_charSize_mm * dpm);//(textSize / textData.Length) < ((textSize-1)*2+10) ? (textSize / textData.Length): ((textSize - 1) * 2 + 10);
                switch (_p.align)
                {
                    case "C":
                        _xdpm = (_xdpm - Convert.ToInt32(_width / 2F));
                        _result.Add(string.Format("^FT{0},{1},0", _xdpm, _ydpm));
                        break;
                    case "D":
                    case "R":
                        _result.Add(string.Format("^FT{0},{1},1", _xdpm, _ydpm));//(_charWidth*1.2).ToString()
                                                                                 //_width = _xdpm;
                        break;
                    case "I":
                    case "L":
                        _result.Add(string.Format("^FT{0},{1},0", _xdpm, _ydpm));
                        break;
                }

                if (_p.style.IndexOf("M") != -1)
                    _p.textData = _p.textData.ToUpper();
                //labelBody.Add(string.Format("^FB{0},1,0,{1},0",Convert.ToInt32(_width),_alignChar));
                //labelBody.Add(string.Format("^FO{0},{1}",_xdpm,_ydpm));//(_charWidth*1.2).ToString()
                _result.Add("^PA1,1,1,1");
                _result.Add(string.Format("^AX{0},{1},{2}", _p.Rotated ? "R" : "", _charWidthDPM, _charWidthDPM));
                _result.Add(string.Format("^FH^FD{0}^FS", _p.textData));
                return string.Join("\n", _result);
            }
            else if (p is printerBarcode)
            {
                var _p = (printerBarcode)p;
                List<string> _result = new List<string>();
                int _xdpm = Convert.ToInt32(_p.x * dpm);
                int _ydpm = Convert.ToInt32(_p.y * dpm);
                int _heightDpm = Convert.ToInt32(_p.Height * dpm);
                switch (_p.align)
                {
                    case "C":
                        _xdpm = (_xdpm - Convert.ToInt32(_p.Width / 2F));
                        _xdpm = _xdpm < 0 ? 0 : _xdpm;
                        _result.Add(string.Format("^FO{0},{1}", _xdpm, _ydpm));
                        break;
                    case "D":
                        _xdpm = (_xdpm - Convert.ToInt32(_p.Width));
                        _result.Add(string.Format("^FO{0},{1}", _xdpm, _ydpm));//(_charWidth*1.2).ToString()
                                                                               //_width = _xdpm;
                        break;
                    case "I":
                        _result.Add(string.Format("^FO{0},{1}", _xdpm, _ydpm));
                        break;
                }
                _result.Add(string.Format("^BY{0},3,{1}", _p.xDimension, _heightDpm));
                _result.Add(string.Format("^BC{0},{1},{2},N,N,N", _p.Rotated ? "R" : "N", _heightDpm, _p.HumanReadable ? "Y" : "N"));
                _result.Add(string.Format("^FD{0}^FS", _p.textData));
                return string.Join("\n", _result);
            }
            else return "";
        }
        
    }

    public static class delimiterLabel
    {
        public static void delim(cLabel pLabel, string pCode, string pValue)
        {
            pLabel.Clear();
            var _x = Convert.ToInt32(pLabel.width / 2);
            var _width = pLabel.width - 3;
            var i = 2;
            pLabel.addLine(_x, i += 7, 0, "C", "", pCode, 12);
            pLabel.addLine(_x, i += 3, _width, "C", "", "######################");
            pValue.Split('|').ToList().ForEach(x =>
            {
                pLabel.addLine(_x, i += 2, 0, "C", "", "[BC]" + (x.Length > 40 ? x.Substring(0, 40) : x), 0, 5, 1);
                pLabel.addLine(_x, i += 9, 0, "C", "", x.Length > 40 ? x.Substring(0, 40) : x, 12);
            });
            pLabel.addLine(_x, i += 3, _width, "C", "", "######################");


            //pLabel.addLine(1, i += 6, 10, "I", "", "1234567890");
            //pLabel.addLine(1, i += 6, 20, "I", "", "1234567890");
            //pLabel.addLine(1, i += 6, 40, "I", "", "1234567890");
            //pLabel.addLine(1, i += 6, 60, "I", "", "1234567890");
            //pLabel.addLine(1, i += 6, 80, "I", "", "1234567890");

        }
            
    }

    public static class TextMeasurer 
    {
        private static readonly Bitmap _fakeImage=new Bitmap(1, 1);
        
        //private static readonly Graphics _graphics= Graphics.FromImage(_fakeImage);

        public static SizeF MeasureString(string text, Font font, int DPI=203)
        {
            _fakeImage.SetResolution(DPI, DPI);
            Graphics _graphics = Graphics.FromImage(_fakeImage);
            _graphics.PageUnit = GraphicsUnit.Millimeter;
            return _graphics.MeasureString(text, font, int.MaxValue);
        }
    }
}
