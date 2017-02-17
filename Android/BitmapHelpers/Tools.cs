using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CommonAndroidTools
{
    public static class Tools
    {
        public static EditText FocusedEditText(this View v)
        {
            if (v is EditText && v.IsFocused)
                return (EditText)v;
            else
                if (v is ViewGroup)
            {
                for (var i = 0; i < ((ViewGroup)v).ChildCount; i++)
                {
                    var c = (((ViewGroup)v).GetChildAt(i).FocusedEditText());
                    if (c != null)
                        return c;
                }
            }
            return null;
        }

        public static List<EditText> EditTextList(this View v)
        {
            var l = new List<EditText>();
            if (v is EditText)
            {
                l.Add((EditText)v);
                return l;
            }
            else
                if (v is ViewGroup)
            {
                for (var i = 0; i < ((ViewGroup)v).ChildCount; i++)
                {
                    var lc = (((ViewGroup)v).GetChildAt(i).EditTextList());
                    if (lc != null)
                        l.AddRange(lc);
                }
                return l;
            }
            else
                return null;
        }

    }
}