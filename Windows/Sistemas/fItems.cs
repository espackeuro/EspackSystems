using System;
using System.Windows.Forms;
using AccesoDatosNet;
using CommonTools;


namespace Sistemas
{
    public partial class fItems : Form
    {
        private string _prevStatus;
        public fItems()
        {
            InitializeComponent();
            
            //CTLM definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pAddItemsCab";
            CTLM.sSPUpp = "pUppItemsCab";
            CTLM.sSPDel = "pDelItemsCab";
            CTLM.AddItem(txtCode, "Code", true, true, true, 1, true, true);
            CTLM.AddItem(txtName, "Name", true, true, false, 0, false, true);
            CTLM.AddItem(txtDescription, "Description", true, true, false, 0, false, true);
            CTLM.AddItem(cboCOD3, "MainCOD3", true, true, false, 1,false, true);
            CTLM.AddItem(txtDesCod3, "DesCOD3");
            CTLM.AddItem(listCOD3, "COD3", true, true, false, 1, false, true);
            CTLM.AddItem(cboType,"Type",true,true,false,0,false,true);
            CTLM.AddItem(txtDesType, "DesType");
            CTLM.AddItem(cboZone, "Zone", true, true, false, 1, false, true);
            CTLM.AddItem(txtSerial, "Serial", true, true, false, 0, false, true);
            CTLM.AddItem(txtInvoice, "Invoice", true, true, false, 0, false, true);
            CTLM.AddItem(txtInvoiceDate, "InvoiceDate", true, true, false, 0, false, false);
            CTLM.AddItem(txtCM, "CM", true, false, false, 0, false, true);
            CTLM.AddItem(lstFlags, "Flags", true, true, false, 0, false, false);

            CTLM.AddDefaultStatusStrip();
            CTLM.DBTable = "vItemsCab";
            
            cboCOD3.Source("select n.COD3,g.Descripcion from NetworkSedes n inner join general..sedes g on g.cod3=n.COD3 order by n.Cod3", txtDesCod3);
            listCOD3.Source("select n.COD3,g.Descripcion from NetworkSedes n inner join general..sedes g on g.cod3=n.COD3 order by n.Cod3");
            cboType.Source("Select Code,Description from ItemTypes order by Code",txtDesType);
            cboZone.Source("Select Code from Zones order by Code");
            lstFlags.Source("Select codigo,DescFlagEng from flags where Tabla='ItemsCab'");
            VS.Conn = Values.gDatos;
            VS.AddColumn("Code", txtCode,"@Code","@Code","@Code");
            VS.AddColumn("Line","Line","@Line", "@Line", "@Line", true,false,true, pWidth:100);
            VS.AddColumn("Type", "Type", "@Type", "", "",true,false,false,"Select Code from ItemTypes order by Code",100);
            VS.AddColumn("Description", "Description", "@Description", "@Description",pWidth : 100);
            VS.AddColumn("Value1", "Value1", "@Value1", "@Value1", "",pWidth : 100);
            VS.AddColumn("Value2", "Value2", "@Value2", "@Value2", "", pWidth: 200);
            VS.DBTable = "ItemsDet";
            VS.sSPAdd = "pAddItemsDet";
            VS.sSPUpp = "pUppItemsDet";
            VS.sSPDel = "pDelItemsDet";
            CTLM.AddItem(VS);
            CTLM.Start();
            _prevStatus = listCOD3.Text;
            cboCOD3.SelectedValueChanged += delegate
            {
                if (cboCOD3.SelectedValue != null)
                    listCOD3.CheckItem(cboCOD3.Text);
            };
            
            listCOD3.ItemCheck += delegate (object sender, ItemCheckEventArgs e)
            {
                if ((e.NewValue==CheckState.Unchecked) && (listCOD3.keyItem(e.Index)==cboCOD3.Text) && (CTLM.Status==EnumStatus.EDIT || CTLM.Status==EnumStatus.ADDNEW))
                {
                    MessageBox.Show("Cannot remove Main COD3 from COD3 list", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.NewValue = CheckState.Checked;
                }
            };
            //cboCOD3.SelectedValue = "";
            //CTLM.AfterButtonClick += delegate (object source, CTLMEventArgs e)
            //{
            //    if (e.ButtonClick == "btnCancel")
            //    {
            //        txtCOD3Name.Text = "";
            //    }
            //};
            //KeyDown += delegate(object sender, KeyEventArgs e)
            // {
            //     MessageBox.Show("Patata");
            // };
        }

  
    }
}
