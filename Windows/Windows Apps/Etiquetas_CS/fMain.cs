﻿
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using RawPrinterHelper;
using AccesoDatosNet;
using CommonTools;
using System.Linq;
using System.Collections.Generic;
using VSGrid;
using System.Data;
using System.Data.SqlClient;
using EspackClasses;
using System.Collections.Specialized;
using System.Drawing.Printing;

namespace Etiquetas_CS
{
    public partial class fMain : Form
    {
        public string SQLSelect { get; set; }
        public string SQLView { get; set; }
        public string SQLWhere { get; set; }
        public string SQLGroup { get; set; }
        public string SQLOrder { get; set; }
        public string SQLQty { get; set; }
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
        public string SQLParameterString { get; set; }
        private int labelWidth;
        private int labelHeight;

        public fMain(string[] args)
        {
            InitializeComponent();
            txtCode.Enabled = true;
            cboPrinters.Enabled = true;

            var espackArgs = CT.LoadVars(args);
            //Values.gDatos.DataBase = "Sistemas";//espackArgs.DataBase;
            //Values.gDatos.Server = "192.168.200.7";//espackArgs.Server;
            //Values.gDatos.User = "sa";//espackArgs.User;
            //Values.gDatos.Password = "5380"; //espackArgs.Password;

            Values.gDatos.DataBase = espackArgs.DataBase;
            Values.gDatos.Server = espackArgs.Server;
            Values.gDatos.User = espackArgs.User;
            Values.gDatos.Password = espackArgs.Password;
            try
            {
                Values.gDatos.Connect();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error connecting to database: " + e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            Values.gDatos.Close();

            vsParameters.AddColumn("Parameter", pLocked: true);
            vsParameters.AddColumn("Value", pWidth: 150, pLocked: false);

            //((CtlVSColumn)vsParameters.Columns[1]).Locked = false;
            //vsParameters.Status = EnumStatus.ADDNEW;
            txtCode.Validating += TxtCode_Validating;
            vsLabels.RowsAdded += VsLabels_RowsAdded;
            vsGroups.SelectionChanged += VsGroups_SelectionChanged;
            vsLabels.DoubleClick += VsLabels_DoubleClick;
        }

        private void VsLabels_DoubleClick(object sender, EventArgs e)
        {
            var box = vsLabels.CurrentCell.Value;
        }

        private void VsGroups_SelectionChanged(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void VsLabels_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            vsLabels.Rows.OfType<DataGridViewRow>().Where(x => x.Index >= e.RowIndex && x.Index < e.RowIndex + e.RowCount).ToList().ForEach(r =>
               {
                   if (r.Cells[r.Cells.Count-1].Value.ToString() == "S")
                       r.DefaultCellStyle.BackColor = Color.Red;
               });
        }

        private void TxtCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            vsGroups.SelectionChanged -= VsGroups_SelectionChanged;
            using (var _RS = new DynamicRS("select *,NoDelim=dbo.checkflag(flags,'NODELIM') from etiquetas where codigo='" + txtCode.Text + "'", Values.gDatos))
            {
                _RS.Open();
                if (_RS.RecordCount==0)
                {
                    MessageBox.Show("Unknown label code.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCode.Text = "";
                    e.Cancel = true;
                    return;
                }
                SQLSelect = _RS["Campos_SELECT"].ToString();
                SQLView = _RS["Vista"].ToString();
                SQLWhere = _RS["Campos_WHERE"].ToString();
                SQLQty = _RS["Campo_QTY"].ToString();
                SQLGroup = _RS["Campo_GROUP"].ToString();
                SQLOrder = _RS["Campos_ORDER"].ToString();
                labelHeight = Convert.ToInt32(_RS["Alto"]);
                labelWidth = Convert.ToInt32(_RS["Ancho"]);
                if (SQLGroup != "")
                {
                    vsGroups.ClearEspackControl();
                    vsGroups.Columns.Clear();
                    vsGroups.AddColumn(SQLGroup);
                    SQLSelect += "|" + SQLGroup;
                }
                // Params GRID
                vsParameters.ClearEspackControl();
                Parameters.Clear();
                vsLabels.ClearEspackControl();
                vsLabels.Columns.Clear();
                SQLWhere.Split('|').ToList().ForEach(x =>
                {
                    //var _row = (DataGridViewRow) vsParameters.Rows[0].Clone();
                    //_row.Cells[0].Value = x;
                    //_row.Cells[1].Value = "";
                    var _param = x.Split('=');
                    if (_param.Count() > 1)
                    {
                        if (_param[1] == "ASK")
                        {
                            Parameters.Add(_param[0], "");
                            vsParameters.Rows.Add(_param[0], "");
                            //vsParameters.Rows[vsParameters.RowCount-1].Cells[1].ReadOnly = false;
                        }
                        else
                        {
                            Parameters.Add(_param[0], _param[1]);
                        }
                    }
                    else
                    {
                        Parameters.Add(_param[0], "NOTHING");
                    }

                });
                //
                if (vsParameters.RowCount != 0)
                {
                    vsParameters.CurrentCell = vsParameters.Rows[0].Cells[1];
                    vsParameters.BeginEdit(true);
                }

                cboPrinters.Source("select Codigo from datosEmpresa where descripcion like '%" + txtCode.Text + "%' order by cmp_integer", Values.gDatos);

            }
            vsGroups.SelectionChanged += VsGroups_SelectionChanged;


        }


        private void button1_Click(object sender, System.EventArgs e)
        {
            // Allow the user to select a file.
            OpenFileDialog ofd = new OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                // Allow the user to select a printer.
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();
                if (DialogResult.OK == pd.ShowDialog(this))
                {
                    // Print the file to the printer.
                    cRawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, ofd.FileName);
                }
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            string s = "hola"; // device-dependent string, need a FormFeed?
            //var label = new ZPLLabel(76, 51, 3, 204);
            //label.addLine(10,10,80,"I","",s);
            // Allow the user to select a printer.
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            if (DialogResult.OK == pd.ShowDialog(this))
            {
                for (var i = 0; i < 5; i += 5)
                {
                    var label = new ZPLLabel(76, 51, 3, 203);
                    label.addLine(35, 10, 20, "C", "", s);
                    label.addLine(35, 20, 40, "C", "", "Cocacola");
                    cRawPrinterHelper.SendUTF8StringToPrinter(pd.PrinterSettings.PrinterName, label.ToString());
                }
                // Send a printer-specific to the printer.

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        public static class Values
        {
            public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        }

        private void btnObtain_Click(object sender, EventArgs e)
        {
            vsGroups.SelectionChanged -= VsGroups_SelectionChanged;
            vsGroups.ClearEspackControl();
            vsParameters.ToList().ForEach(z =>
            {
                Parameters[z.Cells[0].Value.ToString()] = z.Cells[1].Value.ToString();
            });


            var s = Parameters.Where(x => x.Value == "").ToDictionary(a => a.Key, a => a.Value);
            //Dictionary<string, string>)
            if (s.Count != 0)
            {
                MessageBox.Show("Parameter " + s.First().Key + " must be entered.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };

            var _Sql = "SELECT " + SQLSelect.Replace("|", ",") + " FROM " + SQLView;
            SQLParameterString = "";
            Parameters.ToList().ForEach(x =>
            {
                Parameters[x.Key] = x.Value;
                SQLParameterString += x.Key + (x.Value != "NOTHING" ? "='" + x.Value + "'" : "") + " and ";
            });
            if (SQLParameterString != "")
                SQLParameterString = " WHERE " + SQLParameterString.Substring(0, SQLParameterString.Length - 5);

            _Sql += SQLParameterString;
            if (SQLOrder != "")
                _Sql += " ORDER BY " + SQLOrder.Replace("|", ",");

            using (var _RS = new DynamicRS(_Sql, Values.gDatos))
            {
                _RS.Open();
                if (_RS.EOF)
                {
                    MessageBox.Show("No rows returned.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                ShowDetails();
                GenerateGroups(_RS.ToList());
                if (_RS.RecordCount != vsLabels.RowCount)
                {
                    GenerateNewLabels(_RS.ToList());
                    ShowDetails();
                }

            }
            vsGroups.SelectionChanged += VsGroups_SelectionChanged;
        }
        private void ShowDetails()
        {
            vsLabels.ClearEspackControl();
            vsLabels.Conn = Values.gDatos;

            string _group = "";
            if (vsGroups.CurrentCell != null)
                _group = (vsGroups.CurrentCell.Value.ToString() != "" ? " and grupo='" + vsGroups.CurrentCell.Value + "'" : "");
            List<string> _SelectFields = new List<string>();
            _SelectFields.Add("IDREG");
            _SelectFields.AddRange(SQLSelect.Split('|'));
            _SelectFields.Add("QTY");
            _SelectFields.Add("PRINTED");
            vsLabels.ColumnCount = _SelectFields.Count;
            vsLabels.Columns.Cast<DataGridViewColumn>().ToList().ForEach(x => x.Name = _SelectFields[x.Index].ToUpper());
            //Application.DoEvents();
            var _Sql = string.Format("SELECT IDREG,DATA=datos,QTY,PRINTED=impreso FROM etiquetas_detalle WHERE codigo='{0}' and parametros='{1}'{2} order by idreg", txtCode.Text, SQLParameterString.Replace("'", "#"), _group);
            using (var _RS = new DynamicRS(_Sql, Values.gDatos))
            {
                _RS.Open();
                _RS.ToList().ForEach(x =>
                {
                    List<string> row= new List<string>();
                    row.Add( x["IDREG"].ToString());
                    row.AddRange(x["DATA"].ToString().Split('|'));
                    row.Add(x["QTY"].ToString());
                    row.Add(x["PRINTED"].ToString());
                    vsLabels.Rows.Add(row.ToArray());
                    //Application.DoEvents();
                    //vsLabels.Rows.Add(x["IDREG"].ToString(), x["DATA"].ToString(), x["QTY"].ToString(), x["PRINTED"]);
                });
                
            };
            //vsLabels.UpdateEspackControl();
        } 

        private void GenerateGroups(List<DataRow> r)
        {
            vsGroups.SelectionChanged -= VsGroups_SelectionChanged;
            vsGroups.ClearEspackControl();
            vsGroups.Rows.Add("");
            var l = r.GroupBy(p => p[SQLGroup].ToString());
            l.ToList().ForEach(x =>
            {
                vsGroups.Rows.Add(x.Key);
            });
            vsGroups.SelectionChanged += VsGroups_SelectionChanged;

        }
        private void GenerateNewLabels(List<DataRow> r)
        {
            var l = r.GroupBy(p => p[SQLGroup].ToString());
            r.ForEach(x =>
            {
                int _qty = SQLQty != "" ? Convert.ToInt32(x[SQLQty]):1;
                var _SP = new SP(Values.gDatos, "pAddEtiquetasDetalle");
                _SP.AddParameterValue("codigo", txtCode.Text.ToUpper());
                var _split = SQLSelect.Split('|');
                string _dataString = "";
                _split.ToList().ForEach(s => _dataString += x[s] + "|");
                _dataString = _dataString.Substring(0, _dataString.Length - 1);
                _SP.AddParameterValue("parametros", SQLParameterString.Replace("'","#"));
                _SP.AddParameterValue("datos", _dataString);
                _SP.AddParameterValue("qty", _qty);
                _SP.AddParameterValue("Grupo", SQLGroup != "" ? x[SQLGroup] : "");
                _SP.Execute();
                if (_SP.LastMsg.Substring(0, 2) != "OK")
                {
                    CT.MsgError("Error: " + _SP.LastMsg);
                    return;
                }
                Application.DoEvents();
            });
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cboPrinters.Value.ToString() == "")
            {
                CT.MsgError("Select a printer first.");
                return;
            }
            string _printerType = "";
            string _printerAddress = "";
            using (var _RS = new DynamicRS(string.Format("select descripcion,cmp_varchar from datosEmpresa where codigo='{0}'",cboPrinters.Value),Values.gDatos))
            {
                _RS.Open();
                _printerAddress = _RS["cmp_varchar"].ToString();
                _printerType = _RS["descripcion"].ToString().Split('|')[0];
            }
            cLabel _label;
            if (_printerType=="ZPL")
            {
                _label = new ZPLLabel(labelHeight,labelWidth,3,204);
            } else
            {
                throw new NotImplementedException();
            }
            using (var _RS = new DynamicRS(string.Format("Select * from campos where codigo='{0}'", txtCode.Text),Values.gDatos))
            {
                _RS.Open();
                _RS.ToList().ForEach(z =>
                {
                    _label.addLine(Convert.ToInt32(z["Col"]), Convert.ToInt32(z["Fila"]), Convert.ToSingle(CT.Qnuln(z["TamTexto"])),z["Orientacion"].ToString(),z["Estilo"].ToString(),z["Texto"].ToString(),Convert.ToInt32(z["charSize"]));
                });
            }
            Dictionary<string, string> _parameters = new Dictionary<string, string>();
            SQLSelect.Split('|').ToList().ForEach(x => _parameters.Add(x, ""));
            vsLabels.ToList().Where(line => line.Cells["PRINTED"].Value.ToString()=="N").ToList().ForEach(line =>
            {
                _parameters.ToList().ForEach(p => _parameters[p.Key] = line.Cells[p.Key].Value.ToString());
                cRawPrinterHelper.SendUTF8StringToPrinter(_printerAddress, _label.ToString(_parameters),Convert.ToInt32(line.Cells["QTY"].Value));
            });
            
        }

        public class PrintPage : PrintDocument
        {
            private Font PrinterFont { get; set; }
            private string TextToPrint { get; set; }
            static int curChar;

            public PrintPage() : base()
            {
                PrinterFont = new Font("Times New Roman", 10);
                TextToPrint = string.Empty;
            }

            public PrintPage(String pStr) : base()
            {
                PrinterFont = new Font("Times New Roman", 10);
                TextToPrint = pStr;
            }

            protected override void OnPrintPage(System.Drawing.Printing.PrintPageEventArgs e)
            {
                // Run base code
                base.OnPrintPage(e);

                //Declare local variables needed

                int printHeight;
                int printWidth;
                int leftMargin;
                int rightMargin;
                Int32 lines;
                Int32 chars;

                //Set print area size and margins
                {
                    printHeight = base.DefaultPageSettings.PaperSize.Height - base.DefaultPageSettings.Margins.Top - base.DefaultPageSettings.Margins.Bottom;
                    printWidth = base.DefaultPageSettings.PaperSize.Width - base.DefaultPageSettings.Margins.Left - base.DefaultPageSettings.Margins.Right;
                    leftMargin = base.DefaultPageSettings.Margins.Left;  //X
                    rightMargin = base.DefaultPageSettings.Margins.Top;  //Y
                }

                //Check if the user selected to print in Landscape mode
                //if they did then we need to swap height/width parameters
                if (base.DefaultPageSettings.Landscape)
                {
                    int tmp;
                    tmp = printHeight;
                    printHeight = printWidth;
                    printWidth = tmp;
                }

                //Now we need to determine the total number of lines
                //we're going to be printing
                Int32 numLines = (int)printHeight / PrinterFont.Height;

                //Create a rectangle printing are for our document
                RectangleF printArea = new RectangleF(leftMargin, rightMargin, printWidth, printHeight);

                //Use the StringFormat class for the text layout of our document
                StringFormat format = new StringFormat(StringFormatFlags.LineLimit);

                //Fit as many characters as we can into the print area     
                e.Graphics.MeasureString(TextToPrint.Substring(curChar), PrinterFont, new SizeF(printWidth, printHeight), format, out chars, out lines);

                //Print the page
                e.Graphics.DrawString(TextToPrint.Substring(curChar), PrinterFont, Brushes.Black, printArea, format);

                //Increase current char count
                curChar += chars;

                //Detemine if there is more text to print, if
                //there is the tell the printer there is more coming
                if (curChar < TextToPrint.Length)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                    curChar = 0;
                }

            }

            internal void BeginPrint()
            {
                throw new NotImplementedException();
            }
        }

        private void btnPrintList_Click(object sender, EventArgs e)
        {
            using (var _printIt = new PrintPage("Pollos nights"))
            {
                _printIt.Print();
            }
                
        }
    
    }
}
