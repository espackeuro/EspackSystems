using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspackClasses
{
    public abstract class cLabel
    {
        protected float width { get; set; }
        protected float height { get; set; }
        protected float gap { get; set; }
        protected int dpi { get; set; }
        protected float dpm { get; set; }
        protected List<string> labelHeader { get; set; } = new List<string>();
        protected List<string> labelFooter { get; set; } = new List<string>();
        protected List<string> labelBody { get; set; } = new List<string>();
        public override string ToString()
        {
            //return base.ToString();
            return string.Join("", labelHeader) + string.Join("", labelBody) + string.Join("", labelFooter);
        }
        public virtual void addLine(int x, int y, float textSize, string align, string style, string textData, float charWidth = 0, int mul = 1, int mulv = 1, bool overrideSize = false)
        {

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
        public override void addLine(int x, int y, float textSize, string align, string style, string textData, float charWidth=0,int mul = 1, int mulv = 1, bool overrideSize = false)
        {
            int _xdpm = Convert.ToInt32(x * dpm);
            int _ydpm = Convert.ToInt32(y * dpm);
            textSize = textSize == 0F ? charWidth * textData.Length: textSize;
            float _width = textSize * dpm;// = 2.1F * (textSize - 1);
            int _charWidthDPM = charWidth == 0 ? Convert.ToInt32(textSize / textData.Length * dpm * 1.9) : Convert.ToInt32(charWidth * 1.9F * dpm);//(textSize / textData.Length) < ((textSize-1)*2+10) ? (textSize / textData.Length): ((textSize - 1) * 2 + 10);
            switch (align)
            {
                case "C":
                    //_width = width - 2 * Math.Abs(width / 2 - _xdpm);
                    _xdpm = (_xdpm - Convert.ToInt32(_width / 2F));
                    labelBody.Add(string.Format("^FT{0},{1},0", _xdpm, _ydpm));
                    break;
                case "D":
                    labelBody.Add(string.Format("^FT{0},{1},1", _xdpm, _ydpm));//(_charWidth*1.2).ToString()
                    //_width = _xdpm;
                    break;
                case "I":
                    labelBody.Add(string.Format("^FT{0},{1},0", _xdpm, _ydpm));
                    break;
            }
            
            if (style.IndexOf("M") != -1)
                textData = textData.ToUpper();
            //labelBody.Add(string.Format("^FB{0},1,0,{1},0",Convert.ToInt32(_width),_alignChar));
            //labelBody.Add(string.Format("^FO{0},{1}",_xdpm,_ydpm));//(_charWidth*1.2).ToString()
            labelBody.Add(string.Format("^AX,{0},{1}", _charWidthDPM, _charWidthDPM));
            labelBody.Add(string.Format("^FH^FD{0}^FS", textData));
        }
    }



}
