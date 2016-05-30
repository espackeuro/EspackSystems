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
        protected float width { get; set; }
        protected float height { get; set; }
        protected float gap { get; set; }
        protected int dpi { get; set; }
        protected float dpm { get; set; }
        protected List<string> labelHeader { get; set; } = new List<string>();
        protected List<string> labelFooter { get; set; } = new List<string>();
        protected List<printerLine> labelBody { get; set; } = new List<printerLine>();
        public override string ToString()
        {
            //return base.ToString();
            return string.Join("", labelHeader) + string.Join("", labelBody.Select(x => renderLine(x))) + string.Join("", labelFooter);
        }
        public string ToString(Dictionary<string,string> pParameters)
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


        public ZPLLabel(float pwidth, float pheight, float pgap, int pdpi)
        {
            width = pwidth;
            height = pheight;
            gap = pgap;
            dpi = pdpi;
            dpm=(dpi/25.4F);
            labelHeader.Add("^XA^CWX,E:TT0003M_.FNT^XZ");
            labelHeader.Add("^XA");
            labelHeader.Add("^CI28");
            labelHeader.Add("^LH0,0");
            labelFooter.Add("^XZ");
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
            _result.Add(string.Format("^AX,{0},{1}", _charWidthDPM, _charWidthDPM));
            _result.Add(string.Format("^FH^FD{0}^FS", p.textData));

            return string.Join("\n",_result);
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
