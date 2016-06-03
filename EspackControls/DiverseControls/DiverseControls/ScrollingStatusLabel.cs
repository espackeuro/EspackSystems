using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;
namespace DiverseControls
{
    public class ScrollingStatusLabel : ToolStripStatusLabel
    {
        private string _initialText;
        private int _count=0;
        private System.Timers.Timer _timer = new System.Timers.Timer();
        public double Interval
        {
            get
            {
                return _timer.Interval;
            }
            set
            {
                _timer.Interval = value;
            }
        }
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                _initialText = value;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var _textSize = e.Graphics.MeasureString(Text, Font);
            if (_textSize.Width > Width)
            {
                _timer.AutoReset = true;
                _timer.Enabled = true;
            }
        }
        public ScrollingStatusLabel() : base()
        {
            _timer.Enabled = false;
            _timer.Elapsed += _timer_Elapsed;
        }
        public ScrollingStatusLabel(string text) : base(text)
        {
            _initialText = text;
            _timer.Enabled = false;
            _timer.Elapsed += _timer_Elapsed;
            TextAlign = ContentAlignment.MiddleLeft;
        }
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Enabled = false;
            var _t = "               ~ "+_initialText;
            base.Text = _t.Substring(_count, _t.Length - _count) + _t.Substring(0, _count);
            _count = (_count + 1) % _t.Length;
            //Enabled = true;
        }
    }
}
