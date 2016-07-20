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
        public static ButtonBorderStyle DefaultButtonBorderStyle = ButtonBorderStyle.None;
        public static Font DefaultControlFont = new Font("Tahoma", 10, FontStyle.Regular, GraphicsUnit.Point);
        public static Padding DefaultMargin = new Padding(3, 16, 3, 3);
        public static int DefaultControlHeight = 24;

        //label
        public static Font DefaultLabelFont = new Font("Tahoma", 8, FontStyle.Bold, GraphicsUnit.Point);

        //group
        public static Color DefaultGroupColor = Color.FromKnownColor(KnownColor.ControlLightLight);

        public static void changeControlFormat(Control _control)
        {
            if (_control is EspackFormControl)
            {
                ((EspackFormControl)_control).CaptionLabel.Font = DefaultLabelFont;
                //_control.Margin = DefaultMargin;
            }
            //EspackTextBox
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
                ((EspackTextBox)_control).Margin = DefaultMargin;
                //((TextBox)_control).Multiline = true;
                ((TextBox)_control).Height  = DefaultControlHeight;
            }
            //EspackMaskedMaskedTextBox
            if (_control is EspackMaskedTextBox)
            {
                ((EspackMaskedTextBox)_control).Font = DefaultControlFont;
                if (((EspackMaskedTextBox)_control).Enabled == true)
                {
                    ((EspackMaskedTextBox)_control).BackColor = EnabledControlBackground;
                    ((EspackMaskedTextBox)_control).ForeColor = EnabledControlForeground;
                }
                else
                {
                    ((EspackMaskedTextBox)_control).BackColor = DisabledControlBackground;
                    ((EspackMaskedTextBox)_control).ForeColor = DisabledControlForeground;
                }
                ((MaskedTextBox)_control).BorderStyle = DefaultBorderStyle;
                //((MaskedTextBox)_control).Height  = ((MaskedTextBox)_control).PreferredHeight;
            }
            //DateTimePicker
            if (_control is EspackDateTimePicker)
            {
                ((EspackDateTimePicker)_control).Font = DefaultControlFont;
                if (((EspackDateTimePicker)_control).Enabled == true)
                {
                    ((EspackDateTimePicker)_control).BackColor = EnabledControlBackground;
                    ((EspackDateTimePicker)_control).ForeColor = EnabledControlForeground;
                    ((EspackDateTimePicker)_control).BorderColor = EnabledControlBackground;
                }
                else
                {
                    ((EspackDateTimePicker)_control).BackColor = DisabledControlBackground;
                    ((EspackDateTimePicker)_control).ForeColor = DisabledControlForeground;
                    ((EspackDateTimePicker)_control).BorderColor = DisabledControlBackground;
                    ((EspackDateTimePicker)_control).BorderStyle = ButtonBorderStyle.Solid;
                }
                
                //((DateTimePicker)_control).Height  = ((DateTimePicker)_control).PreferredHeight;
            }
            if (_control is EspackComboBox)
            {
                ((EspackComboBox)_control).Font = DefaultControlFont;
                if (((EspackComboBox)_control).Enabled == true)
                {
                    ((EspackComboBox)_control).BackColor = EnabledControlBackground;
                    ((EspackComboBox)_control).ForeColor = EnabledControlForeground;
                }
                else
                {
                    ((EspackComboBox)_control).BackColor = DisabledControlBackground;
                    ((EspackComboBox)_control).ForeColor = DisabledControlForeground;

                }
                
                //((ComboBox)_control).Height  = ((ComboBox)_control).PreferredHeight;
            }
            //Checked ListBox
            //EspackTextBox
            if (_control is EspackCheckedListBox)
            {
                ((EspackCheckedListBox)_control).Font = DefaultControlFont;
                if (((EspackCheckedListBox)_control).Enabled == true)
                {
                    ((EspackCheckedListBox)_control).BackColor = EnabledControlBackground;
                    ((EspackCheckedListBox)_control).ForeColor = EnabledControlForeground;
                }
                else
                {
                    ((EspackCheckedListBox)_control).BackColor = DisabledControlBackground;
                    ((EspackCheckedListBox)_control).ForeColor = DisabledControlForeground;
                }
                ((CheckedListBox)_control).BorderStyle = DefaultBorderStyle;
                //((CheckedListBox)_control).Height  = ((CheckedListBox)_control).PreferredHeight;
            }


        }
    }
}
