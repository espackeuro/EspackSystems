using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Linq;
using System.Globalization;
//using ADODB;
namespace CommonTools
{
    public enum EnumStatus { ADDNEW, EDIT, DELETE, SEARCH, NAVIGATE, EDITGRIDLINE, ADDGRIDLINE }

    public struct EspackParamArray
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
                    if (lField.ToUpper().IndexOf("AS") != -1)
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

        #region Gets the build date and time (by reading the COFF header)

        // http://msdn.microsoft.com/en-us/library/ms680313

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

        public static string ToASCII(string s)
        {
            return String.Join("",
                 s.Normalize(NormalizationForm.FormD)
                .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        }

        public static DialogResult InputBox(string title, string promptText, ref string value, bool Password = false)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            if (Password)
            {
                textBox.PasswordChar = '·';
            }

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        #endregion
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





}
