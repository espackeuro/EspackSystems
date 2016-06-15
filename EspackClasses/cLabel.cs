using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;

namespace EspackClasses
{
    public class printerLine: ICloneable
    {
        public int x;
        public int y;
        public float textSize;
        public string align;
        public string style;
        public string textData;
        public int charSize;

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
        protected float gap { get; set; }
        protected int dpi { get; set; }
        protected float dpm { get; set; }
        protected int qty { get; set; }
        protected virtual List<string> labelHeader { get; } 
        protected virtual List<string> labelFooter { get; } 
        protected List<printerLine> labelBody { get; set; } = new List<printerLine>();
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

            List<printerLine> _replacedList = new List<printerLine>();
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
            _replacedList = labelBody.Select(x => x.Clone()).ToList();
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

        public virtual void addLine(int x, int y, float textSize, string align, string style, string textData, int charSize = 0)
        {
            labelBody.Add(new printerLine()
            {
                x = x,
                y = y,
                textSize = textSize,
                align = align,
                style = style,
                textData = textData,
                charSize = charSize
            });
        }
        public virtual string renderLine(printerLine p)
        {
            return "";
        }
    };
 
    public class ZPLLabel: cLabel
    {
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
        public ZPLLabel(float pwidth, float pheight, float pgap, int pdpi)
        {
            width = pwidth;
            height = pheight;
            gap = pgap;
            dpi = pdpi;
            dpm=(dpi/25.4F);
        }

        public override string renderLine(printerLine p)
        {
            List<string> _result= new List<string>();
            int _xdpm = Convert.ToInt32(p.x * dpm);
            int _ydpm = Convert.ToInt32(p.y * dpm);
            float _charSize_mm = p.charSize * 0.3527777777778F;
            
            if (p.textSize == 0)
            {
                Font _font = new Font("Swis721 BT", p.charSize);
                p.textSize = TextMeasurer.MeasureString(p.textData, _font).Width*1.7F;
             }
                
            //textSize = textSize == 0F ? ): textSize;
            float _width = p.textSize * dpm;// = 2.1F * (textSize - 1);
            int _charWidthDPM = _charSize_mm == 0 ? Convert.ToInt32(p.textSize / p.textData.Length * dpm * 1.9) : Convert.ToInt32(_charSize_mm * 1.9F * dpm);//(textSize / textData.Length) < ((textSize-1)*2+10) ? (textSize / textData.Length): ((textSize - 1) * 2 + 10);
            switch (p.align)
            {
                case "C":
                    _xdpm = (_xdpm - Convert.ToInt32(_width / 2F));
                    _result.Add(string.Format("^FT{0},{1},0", _xdpm, _ydpm));
                    break;
                case "D":
                    _result.Add(string.Format("^FT{0},{1},1", _xdpm, _ydpm));//(_charWidth*1.2).ToString()
                    //_width = _xdpm;
                    break;
                case "I":
                    _result.Add(string.Format("^FT{0},{1},0", _xdpm, _ydpm));
                    break;
            }
            
            if (p.style.IndexOf("M") != -1)
                p.textData = p.textData.ToUpper();
            //labelBody.Add(string.Format("^FB{0},1,0,{1},0",Convert.ToInt32(_width),_alignChar));
            //labelBody.Add(string.Format("^FO{0},{1}",_xdpm,_ydpm));//(_charWidth*1.2).ToString()
            _result.Add("^PA1,1,1,1");
            _result.Add(string.Format("^AX,{0},{1}", _charWidthDPM, _charWidthDPM));
            _result.Add(string.Format("^FH^FD{0}^FS", p.textData));
            return string.Join("\n",_result);
        }
        
    }

    public static class delimiterLabel
    {
        public static void delim(cLabel pLabel,string pCode, string pValue)
        {
            pLabel.Clear();
            var _x = Convert.ToInt32(pLabel.width / 2);
            var _width = pLabel.width - 10;
            var i = 10;
            pLabel.addLine(_x, i += 5, _width - 20, "C", "", pCode);
            pLabel.addLine(_x, i += 3, _width, "C", "", "######################");
            pLabel.addLine(_x, i += 3, _width, "C", "", "######################");
            pLabel.addLine(_x, i += 3, _width, "C", "", "######################");
            pValue.Split('|').ToList().ForEach(x =>
            {
            pLabel.addLine(5, i += 6, 0, "I", "", x.Length > 40 ? x.Substring(0, 40) : x,9);
            });
            pLabel.addLine(_x, i += 6, _width, "C", "", "######################");
        }
            
    }

    public static class TextMeasurer 
    {
        private static readonly Image _fakeImage=new Bitmap(1, 1);
        private static readonly Graphics _graphics= Graphics.FromImage(_fakeImage);

        public static SizeF MeasureString(string text, Font font)
        {
            _graphics.PageUnit = GraphicsUnit.Millimeter;
            return _graphics.MeasureString(text, font, int.MaxValue);
        }
    }
}
