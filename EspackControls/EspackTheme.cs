using System;
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

    public static class EspackTheme
    {
        //form
        public static Color DefaultFormBackground = Color.FromKnownColor(KnownColor.Control);
        //control
        public static Color EnabledControlBackground = Color.White;//FromKnownColor(KnownColor.AntiqueWhite);
        public static Color EnabledControlForeground = Color.Black;
        public static Color DisabledControlBackground = Color.LightGray;
        public static Color DisabledControlForeground = Color.DarkGray;
        public static BorderStyle DefaultBorderStyle = BorderStyle.None;
        public static Font DefaultControlFont = new Font("Tahoma", 10, FontStyle.Regular, GraphicsUnit.Point);

        //label
        public static Font DefaultLabelFont = new Font("Tahoma", 8, FontStyle.Bold, GraphicsUnit.Point);

        //group
        public static Color DefaultGroupColor = Color.FromKnownColor(KnownColor.ControlLightLight);

        public static void changeControlFormat(Control _control)
        {
            if (_control is EspackFormControl)
            {
                ((EspackFormControl)_control).CaptionLabel.Font = DefaultLabelFont;
                var _m = new Padding();
                _m = _control.Margin;
                _m.Top += ((EspackFormControl)_control).CaptionLabel.Height;
                _control.Margin = _m;
            }
            if (_control is EspackTextBox)
            {
                ((EspackTextBox)_control).Font = DefaultControlFont;
                if (((EspackTextBox)_control).Enabled == true)
                {
                    ((EspackTextBox)_control).BackColor = EnabledControlBackground;
                    ((EspackTextBox)_control).ForeColor = EnabledControlForeground;
                }
                else
                {
                    ((EspackTextBox)_control).BackColor = DisabledControlBackground;
                    ((EspackTextBox)_control).ForeColor = DisabledControlForeground;
                }
                ((TextBox)_control).BorderStyle = DefaultBorderStyle;
                //((TextBox)_control).Height  = ((TextBox)_control).PreferredHeight;

            }

        }
    }

}