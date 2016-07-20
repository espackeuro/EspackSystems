using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CommonToolsWin
{
    public static class cSettings
    {
        private static string callingAssembly { get; set; }
        private static IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        public static string SettingsFileName
        {
            get
            {
                return string.Format("{0}.xml", callingAssembly != null ? callingAssembly : Assembly.GetCallingAssembly().GetName().Name);
            }
        }
        public static string readSetting(string settingName)
        {
            callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
            string _result = "";
            //IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            if (isoStore.FileExists(cSettings.SettingsFileName))
            {
                using (var isoStream = new IsolatedStorageFileStream(SettingsFileName, FileMode.Open, isoStore))
                {
                    XDocument _x = XDocument.Load(isoStream);
                    try
                    {
                        _result = _x.Descendants(settingName).First().Value;
                    } catch
                    {
                        _result = "";
                    }
                    
                    //using (var reader = XmlReader.Create(isoStream))
                    //{
                    //    reader.ReadToFollowing(settingName);
                    //    _result = reader.ReadElementContentAsString();
                    //}
                }
            }
            return _result;
        }
        public static void writeSetting(string settingName, string settingValue)
        {
            callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
            XDocument _x;
            if (!isoStore.FileExists(cSettings.SettingsFileName))
            {
                string _xml = string.Format(
                    @"<?xml version='1.0' encoding='utf - 8'?>
<!--{0} preferences file-->
<preferences>
</preferences>", callingAssembly.ToUpper());
                _x = XDocument.Parse(_xml);
            }
            else
                //IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
                using (var isoStream = new IsolatedStorageFileStream(SettingsFileName, FileMode.OpenOrCreate, isoStore))
                    _x = XDocument.Load(isoStream);
            try
            {
                _x.Descendants(settingName).First().Value = settingValue;
            }
            catch
            {
                _x.Descendants("preferences").FirstOrDefault().Add(new XElement(settingName, settingValue));
            }

            using (var isoStream = new IsolatedStorageFileStream(SettingsFileName, FileMode.Create, isoStore))
                _x.Save(isoStream);
        }
        public static bool SettingFileNameExists
        {
            get
            {
                callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
                return isoStore.FileExists(cSettings.SettingsFileName);
            }
        }
    }
}
