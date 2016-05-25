using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etiquetas_CS
{
    public class ZPLLabel
    {
        private float width;
        private float height;
        private float gap;
        private int dpi;
        private float dpm;
        private List<string> labelHeader = new List<string>();
        private List<string> labelFooter = new List<string>();
        private List<string> labelBody= new List<string>();
        public override string ToString()
        {
            //return base.ToString();
            return string.Join("",labelHeader)+string.Join("",labelBody)+string.Join("",labelFooter);
        }
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
        public void addLine(int x, int y, float textSize, string align, string style, string textData, int mul = 1, int mulv = 1, bool overrideSize = false)
        {
            int _xdpm = Convert.ToInt32(x * dpm);
            int _ydpm = Convert.ToInt32(y * dpm);
            float _width = textSize * dpm;// = 2.1F * (textSize - 1);
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
            int _charWidth =Convert.ToInt32(textSize / textData.Length * dpm *1.9) ;//(textSize / textData.Length) < ((textSize-1)*2+10) ? (textSize / textData.Length): ((textSize - 1) * 2 + 10);
            if (style.IndexOf("M") != -1)
                textData = textData.ToUpper();
            //labelBody.Add(string.Format("^FB{0},1,0,{1},0",Convert.ToInt32(_width),_alignChar));
            //labelBody.Add(string.Format("^FO{0},{1}",_xdpm,_ydpm));//(_charWidth*1.2).ToString()
            labelBody.Add(string.Format("^AX,{0},{1}", _charWidth, _charWidth));
            labelBody.Add(string.Format("^FH^FD{0}^FS", textData));
        }
    }



}
