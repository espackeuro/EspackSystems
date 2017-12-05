using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
//using System.Windows.Forms;
//using System.Drawing;
using System.Data;
//using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Net;
using System.Xml.Linq;
using System.Security;
//using ADODB;
namespace CommonTools
{
    public enum EnumStatus { ADDNEW, EDIT, DELETE, SEARCH, NAVIGATE, EDITGRIDLINE, ADDGRIDLINE }

    public class EspackParamArray
    {
        public string AppName { get; set; }
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Cod3 { get; set; }
        public string DataBase { get; set; }
    }

    public static class CT
    {


        public static SqlQuery SimpleParseSQL(string pSQL)
        {
            SqlQuery result = new SqlQuery();
            result.SelectFields = new Dictionary<string, SqlItem>();
            result.WhereFields = new Dictionary<string, SqlItem>();
            result.OrderFields = new Dictionary<string, SqlItem>();
            List<string> lAlias=new List<string>();
            string lTable = pSQL.ToUpper().IndexOf(" WHERE")!=-1 ? pSQL.Substring(pSQL.ToUpper().IndexOf(" FROM") + 6, pSQL.ToUpper().IndexOf(" WHERE") - (pSQL.ToUpper().IndexOf(" FROM") + 6)).Trim():pSQL.Substring(pSQL.ToUpper().IndexOf(" FROM") + 6);
            if (lTable.IndexOf(' ')!=-1) {
                result.Tablename.DBItemName=lTable.Substring(0,lTable.IndexOf(' ')-1);
                result.Tablename.Alias = lTable.Replace(result.Tablename.DBItemName, "").Replace(" AS ", "").Replace(" ", "");
            }
            else
            {
                result.Tablename.DBItemName = lTable;
                result.Tablename.Alias = "";
            }
            result.Tablename.Type = "TABLE";
            //now the select fields
            string lSelectFields = pSQL.Substring(7, pSQL.ToUpper().IndexOf(" FROM")-7);
            Regex lRegEx=new Regex("TOP\\(\\d+\\)",RegexOptions.IgnoreCase);
            lSelectFields=lRegEx.Replace(lSelectFields,"");
            lSelectFields = lSelectFields.Replace(" ,",",").Replace(", ",",").Replace(" =","=").Replace("= ","=").Trim();//remove spaces after and before comma and =
            result.SelectString = lSelectFields;
            foreach (string lField in lSelectFields.Split(','))
            {
                if (lField.IndexOf('=') != -1 || lField.ToUpper().IndexOf(" AS ") != -1)
                {
                    if (lField.ToUpper().IndexOf(" AS ") != -1)
                    {
                        result.SelectFields.Add(lField.Substring(lField.ToUpper().IndexOf("AS ") + 3).Trim().Replace("[", "").Replace("]", ""), new SqlItem()
                        {
                            DBItemName=lField.Substring(0,lField.ToUpper().IndexOf(" AS")).Trim(),
                            Alias=lField.Substring(lField.ToUpper().IndexOf("AS ")+3).Trim().Replace("[","").Replace("]",""),
                            Type="FIELD"
                        });
                    }
                    else
                    {
                        result.SelectFields.Add(lField.Substring(0, lField.ToUpper().IndexOf("=")).Trim().Replace("[", "").Replace("]", ""), new SqlItem()
                        {
                            Alias = lField.Substring(0, lField.ToUpper().IndexOf("=")).Trim(),
                            DBItemName = lField.Substring(lField.ToUpper().IndexOf("=") + 1).Trim(),
                            Type = "FIELD"
                        });
                    }
                } else {
                    result.SelectFields.Add(lField.Trim().Replace("[", "").Replace("]", ""), new SqlItem()
                        {
                            Alias = lField.Trim(),
                            DBItemName = lField.Trim(),
                            Type = "FIELD"
                        });
                }
            }
            foreach (KeyValuePair<string,SqlItem> kvp in result.SelectFields) {
                lAlias.Add(kvp.Value.Alias);
            }
            result.AliasString=string.Join(",",lAlias);
            //lets get the order string
            string lOrderString = (pSQL.ToUpper().IndexOf(" ORDER BY") != -1) ? pSQL.Substring(pSQL.ToUpper().IndexOf(" ORDER BY")+10):"";
            result.OrderString = lOrderString;
            //now the where Fields
            string lWhereFields = "";
            if (pSQL.ToUpper().IndexOf(" WHERE ") != -1)
            {
                lWhereFields = (lOrderString != "") ? pSQL.Substring(pSQL.ToUpper().IndexOf(" WHERE ") + 7, pSQL.ToUpper().IndexOf(" ORDER BY") - (pSQL.ToUpper().IndexOf(" WHERE ") + 7)) : pSQL.Substring(pSQL.ToUpper().IndexOf(" WHERE ") + 7);
            }
                
            result.WhereString = lWhereFields;
            Regex pattern=new Regex("(.*?)(=|like|>)(.*?)( |\\Z)(and|or|\\Z)");
            
            MatchCollection matches=pattern.Matches(lWhereFields);
            //int lOrder=0;
            foreach (Match lMatch in matches)
            {
                result.WhereFields.Add(lMatch.Groups[1].ToString().Replace("[", "").Replace("]", ""), new SqlItem()
                {
                    Alias = "",
                    DBItemName = lMatch.Groups[1].ToString(),
                    Condition = lMatch.Groups[2].ToString(),
                    Value = lMatch.Groups[3].ToString(),
                    //Order = lOrder++,
                    Type = "FIELD"
                });
            }

            return result;
        }
        public static EspackParamArray LoadVars(string[] args)
        {
            EspackParamArray lParam=new EspackParamArray();
            foreach (string arg in args)
            {
                if (arg.IndexOf('=') != -1)
                {
                    string lsParam = arg.Split('=')[0];
                    string lValue = arg.Split('=')[1];
                    switch (lsParam)
                    {
                        case "/app":
                            lParam.AppName = lValue;
                            break;
                        case "/srv":
                            lParam.Server = lValue;
                            break;
                        case "/usr":
                            lParam.User = lValue;
                            break;
                        case "/pwd":
                            lParam.Password = lValue;
                            break;
                        case "/loc":
                            lParam.Cod3 = lValue;
                            break;
                        case "/db":
                            lParam.DataBase = lValue;
                            break;
                    }
                }
            }
            return lParam;
        }

        public static string QNul(string value)
        {
            return value == null ? "" : value;
        }

#pragma warning disable 169
#pragma warning disable 649


        struct _IMAGE_FILE_HEADER
        {
            public ushort Machine;
            public ushort NumberOfSections;
            public uint TimeDateStamp;
            public uint PointerToSymbolTable;
            public uint NumberOfSymbols;
            public ushort SizeOfOptionalHeader;
            public ushort Characteristics;
        };


        public static DateTime GetBuildDateTime(Assembly assembly)
        {
            
            if (File.Exists(assembly.Location))
            {
                var buffer = new byte[Math.Max(Marshal.SizeOf(typeof(_IMAGE_FILE_HEADER)), 4)];
                using (var fileStream = new FileStream(assembly.Location, FileMode.Open, FileAccess.Read))
                {
                    fileStream.Position = 0x3C;
                    fileStream.Read(buffer, 0, 4);
                    fileStream.Position = BitConverter.ToUInt32(buffer, 0); // COFF header offset
                    fileStream.Read(buffer, 0, 4); // "PE\0\0"
                    fileStream.Read(buffer, 0, buffer.Length);
                }
                var pinnedBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                try
                {
                    var coffHeader = (_IMAGE_FILE_HEADER)Marshal.PtrToStructure(pinnedBuffer.AddrOfPinnedObject(), typeof(_IMAGE_FILE_HEADER));

                    return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1) + new TimeSpan(coffHeader.TimeDateStamp * TimeSpan.TicksPerSecond));
                }
                finally
                {
                    pinnedBuffer.Free();
                }
            }
            return new DateTime();
        }
        public static void DirectoryCopy(this DirectoryInfo dir, string destDirName)
        {
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: ");
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                subdir.DirectoryCopy(temppath);
            }

        }

        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        public static int ToInt(this object s)
        {
            return Convert.ToInt32(s.ToString());
        }

        public static bool IsNumericType(this DbType o)
        {
            switch (o)
            {
                case DbType.Byte:
                case DbType.Currency:
                case DbType.Decimal:
                case DbType.Double:
                case DbType.Int16:
                case DbType.Int32:
                case DbType.Int64:
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                case DbType.VarNumeric:
                case DbType.SByte:
                    return true;
                default:
                    return false;


            }
        }


        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsNumericType(Type pType)
        {
            switch (Type.GetTypeCode(pType))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsTextType(this SqlDbType columnType)
        {
            return columnType == SqlDbType.Char
                || columnType == SqlDbType.NChar
                || columnType == SqlDbType.NText
                || columnType == SqlDbType.NVarChar
                || columnType == SqlDbType.Text
                || columnType == SqlDbType.VarChar;
        }
        public static bool IsNumericType(this SqlDbType columnType)
        {
            return columnType == SqlDbType.TinyInt
                    || columnType == SqlDbType.BigInt
                    || columnType == SqlDbType.Decimal
                    || columnType == SqlDbType.Int
                    || columnType == SqlDbType.SmallInt;
        }
        public static bool IsBoolean(SqlDbType columnType)
        {
            return columnType == SqlDbType.Bit;
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static string ToASCII(string s)
        {
            return String.Join("",
                 s.Normalize(NormalizationForm.FormD)
                .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        }

        public static SecureString ToSecureString(this string source)
        {

            if (string.IsNullOrWhiteSpace(source))
                return null;
            else
            {
                SecureString result = new SecureString();
                foreach (char c in source.ToCharArray())
                    result.AppendChar(c);
                return result;
            }
        }
        public static string ToUnsecureString(this SecureString securePassword)
        {
            if (securePassword == null)
                throw new ArgumentNullException("securePassword");

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        public static string PadCenter(this string pString, int totalWidth,char paddingChar)
        {
            pString = "**********************" + pString + "*******************************";
            var _sb = new StringBuilder("",totalWidth);
            return _sb.AppendFormat("{0:C}", pString).ToString();
        }

        public static string Qnul(object t)
        {
            if (t is DBNull)
                return "";
            else
                return t.ToString();
        }
        public static object Qnuln(object t)
        {
            if (t is DBNull)
                return 0;
            else
                return t;
        }

    }

    public struct SqlItem 
    {
        public string Type;
        public string Alias;
        public string DBItemName;
        public string Order;
        public string Condition;
        public string Value;
    }

    public struct SqlQuery
    {
        public Dictionary<string,SqlItem> SelectFields;
        public SqlItem Tablename;
        public Dictionary<string, SqlItem> WhereFields;
        public Dictionary<string, SqlItem> OrderFields;
        public string WhereString;
        public string SelectString;
        public string AliasString;
        public string OrderString;
    }

    public class DirectoryItem
    {
        
        public Uri BaseUri;

        public string AbsolutePath
        {
            get
            {
                return string.Format("{0}/{1}", BaseUri, Name);
            }
        }
        public Uri Uri
        {
            get
            {
                return new UriBuilder(AbsolutePath).Uri;
            }
        }
        public cServer Server; 
        public DateTime DateCreated;
        public bool IsDirectory;
        public string Name;
        public List<DirectoryItem> Items;
    }
    // Class cServer -> there are two types: DATABASE and SHARE
    public class cServer
    {
        public bool Resolve { get; set; } = true;
        private string _hostName;
        public string HostName
        {
            get
            {
                return _hostName;
            }
            set
            {
                _hostName = value;
                if (Resolve && value!="")
                {
                    IPAddress _serverIP;
                    if (!IPAddress.TryParse(value, out _serverIP))
                    {

                        try
                        {
                            var result = Dns.GetHostEntry(value);
                            _hostName = result.HostName;
                            IP = result.AddressList[0];
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error trying {0}: {1}", HostName, ex.Message));
                        }
                    }
                }
            }
        }
        public IPAddress IP { get; set; }
        public ServerTypes Type { get; set; }
        public string COD3 { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public XElement xServer
        {
            get
            {
                var _root = new XElement("server");
                _root.Add(new XElement("HostName", HostName));
                _root.Add(new XElement("IP", IP));
                _root.Add(new XElement("Type", Type));
                _root.Add(new XElement("COD3", COD3));
                _root.Add(new XElement("User", User));
                _root.Add(new XElement("Password", Password));
                return _root;
            }
            set
            {
                HostName = value.Element("HostName").Value;
                Type = (ServerTypes)Enum.Parse(typeof(ServerTypes), value.Element("Type").Value);
                COD3 = value.Element("COD3").Value;
                User = value.Element("User").Value;
                Password = value.Element("Password").Value;
            }
        }
        public string Server
        {
            get
            {
                return HostName;
            }
            set
            {
                IPAddress _serverIP;
                _hostName = value;
                if (Resolve)
                {
                    if (!IPAddress.TryParse(value, out _serverIP))
                    {

                        try
                        {
                            var result = Dns.GetHostEntry(value);
                            HostName = result.HostName;
                            IP = result.AddressList[0];
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error trying {0}: {1}", HostName, ex.Message));
                        }
                    }
                    else
                    {
                        IP = _serverIP;
                    }
                }
            }
        }
        public cServer(string ServerName)
        {
            Server = ServerName;
        }

        public cServer(XElement pxServer)
        {
            xServer = pxServer;
        }
        public cServer()
        {
            HostName = "";
            IPAddress _IP;
            if (IPAddress.TryParse("0.0.0.0", out _IP))
                IP = _IP;

        }
    }
    public enum ServerTypes { SHARE, DATABASE }
    // Class cServerList
    public class cServerList
    {
        public List<cServer> ServerList { get; set; } = new List<cServer>();
        public ServerTypes ListType { get; set; }

        public cServer this[string COD3]
        {
            get
            {
                return ServerList.FirstOrDefault(x => x.COD3 == COD3);
            }
        }

        public cServerList(ServerTypes pServerType)
        {
            ListType = pServerType;
        }

        public void Add(cServer pServer)
        {
            ServerList.Add(pServer);
        }
    }

    public struct EspackCredentials
    {
        public string User { get; set; }
        public SecureString Password { get; set; }
    }

    public interface IsValuable
    {
        event EventHandler TextChanged;
        string Text { get; set; }
        object Value { get; set; }
    }


}
