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
using Android.Telephony;

namespace CommonAndroidTools
{
    public static class cDeviceInfo
    {
        public static String getDeviceID(Context p_context)
        {
            String m_deviceID = null;
            TelephonyManager m_telephonyManager = null;
            m_telephonyManager = (TelephonyManager)p_context.GetSystemService(Context.TelephonyService);
            m_deviceID = m_telephonyManager.DeviceId;

            if (m_deviceID == null || "00000000000000".Equals(m_deviceID))
            {
                m_deviceID = "AAAAAAA";
            }

            return m_deviceID;
        }
        public static string Serial
        {
            get
            {
                return "16221522505390";//Android.OS.Build.Serial;
            }

        }
        public static string getDeviceInfo(string p_seperator)
        {
            string m_data = "";
            Java.Lang.StringBuilder m_builder = new Java.Lang.StringBuilder();
            m_builder.Append("RELEASE " + Android.OS.Build.VERSION.Release + p_seperator);
            m_builder.Append("DEVICE " + Android.OS.Build.Device + p_seperator);
            m_builder.Append("MODEL " + Android.OS.Build.Model + p_seperator);
            m_builder.Append("PRODUCT " + Android.OS.Build.Product + p_seperator);
            m_builder.Append("BRAND " + Android.OS.Build.Brand + p_seperator);
            m_builder.Append("DISPLAY " + Android.OS.Build.Display + p_seperator);
            // TODO : Android.OS.Build.CPU_ABI is deprecated
            m_builder.Append("CPU_ABI " + Android.OS.Build.CpuAbi + p_seperator);
            // TODO : Android.OS.Build.CPU_ABI2 is deprecated
            m_builder.Append("CPU_ABI2 " + Android.OS.Build.CpuAbi2 + p_seperator);
            m_builder.Append("UNKNOWN " + Android.OS.Build.Unknown + p_seperator);
            m_builder.Append("HARDWARE " + Android.OS.Build.Hardware + p_seperator);
            m_builder.Append("ID " + Android.OS.Build.Id + p_seperator);
            m_builder.Append("MANUFACTURER " + Android.OS.Build.Manufacturer + p_seperator);
            m_builder.Append("SERIAL " + Android.OS.Build.Serial + p_seperator);
            m_builder.Append("USER " + Android.OS.Build.User + p_seperator);
            m_builder.Append("HOST " + Android.OS.Build.Host + p_seperator);
            m_data = m_builder.ToString();
            return m_data;
        }
    }
}