using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neodynamic.SDK.Printing;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //var tLabel = new ThermalLabel(UnitType.Mm, 76, 51);
            //tLabel.GapLength = 3;
            //Define a ThermalLabel object and set unit to inch and label size
            ThermalLabel tLabel = new ThermalLabel(UnitType.Inch, 4, 3);
            tLabel.GapLength = 0.2;

            //Define a TextItem object
            TextItem txtItem = new TextItem(0.2, 0.2, 2.5, 0.5, "Thermal Label Test");

            //Define a BarcodeItem object
            BarcodeItem bcItem = new BarcodeItem(0.2, 1, 2, 1, BarcodeSymbology.Code128, "ABC 12345");
            //Set bars height to .75inch
            bcItem.BarHeight = 0.75;
            //Set bars width to 0.0104inch
            bcItem.BarWidth = 0.0104;

            //Add items to ThermalLabel object...
            tLabel.Items.Add(txtItem);
            tLabel.Items.Add(bcItem);

            //Create a PrintJob object
            using (PrintJob pj = new PrintJob())
            {
                //Create PrinterSettings object
                PrinterSettings myPrinter = new PrinterSettings();
                myPrinter.Communication.CommunicationType = CommunicationType.PrinterDriver;
                myPrinter.Dpi = 203;
                myPrinter.ProgrammingLanguage = ProgrammingLanguage.ZPL;
                myPrinter.PrinterName = "ZDesigner GT800 (EPL)";

                //Set PrinterSettings to PrintJob
                pj.PrinterSettings = myPrinter;
                //Print ThermalLabel object...
                pj.Print(tLabel);
            }

        }
    }
}
