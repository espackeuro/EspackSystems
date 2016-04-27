using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistemas
{
    public partial class fTasks : Form
    {
        public fTasks()
        {
            InitializeComponent();
            //CTLM definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pAddCabTareas";
            CTLM.sSPUpp = "pUppCabTareas";
            CTLM.sSPDel = "pDelCabTareas";

            //ctlm fields
            CTLM.AddItem(txtIdTarea, "idTarea", false, true, true, 1, true, true);
            CTLM.AddItem(txtIT, "ITWorker", true, true, false, 0, false, true);
            CTLM.AddItem(txtPerson, "Person", true, true, false, 0, false, true);
            CTLM.AddItem(cboPlaceAdvise, "PlaceAdvise", true, true, false, 0, false, true);
            CTLM.AddItem(txtPlaceAdvise, "DesPlaceAdvise");
            CTLM.AddItem(cboPlaceAffected, "PlaceAffected", true, true, false, 0, false, true);
            CTLM.AddItem(txtPlaceAffected, "DesPlaceAffected");
            CTLM.AddItem(cboService, "ServiceAffected", true, true, false, 0, false, true);
            CTLM.AddItem(cboMatter, "Matter", true, true, false, 0, false, true);
            //CTLM.AddItem(txt, "DesPlaceAffected");
            CTLM.AddItem(dateStart, "StartTime", true, true, false, 0, false, true);
            CTLM.AddItem(dateEnd, "EndTime", true, true, false, 0, false, true);
            CTLM.AddItem(txtKm, "Km", true, true, false, 0, false, true);
            CTLM.AddItem(txtNotes, "Notes", true, true, false, 0, false, true);
            CTLM.AddItem(chkFlags, "flags", true, true, false, 0, false, true);

            //combo definitions
            cboPlaceAdvise.Source("Select cod3,Nombre from vSedes", txtPlaceAdvise);
            cboPlaceAffected.Source("Select cod3,Nombre from vSedes", txtPlaceAffected);
            cboPlaceAffected.SelectedValueChanged += delegate
            {
                cboService.Source("Select codigoservicio from vServiciosSedes where cod3 = '" + cboPlaceAffected.Value + "'");
            };

            //cboService.Source("");// Select codigoservicio from vServiciosSedes where cod3 = '" + txtPlaceAffected.Text + "'
            cboMatter.Source("Select Codigo from materias");


            //Defaults
            cboPlaceAdviseDef.Source("Select cod3 from vSedes", Values.gDatos);
            cboPlaceAffectedDef.Source("Select cod3 from vSedes", Values.gDatos);
            cboPlaceAffectedDef.SelectedValueChanged += delegate
            {
                cboServiceDef.Source("Select codigoservicio from vServiciosSedes where cod3 = '" + cboPlaceAffectedDef.Value + "'", Values.gDatos);
            };

            //cboService.Source("");// Select codigoservicio from vServiciosSedes where cod3 = '" + txtPlaceAffected.Text + "'
            cboMatterDef.Source("Select Codigo from materias", Values.gDatos);

            chkFlags.Source("Select codigo,DescFlagEng from flags where Tabla='CabTareas'");
            CTLM.AddDefaultStatusStrip();
            CTLM.DBTable = "vCabTareas";


            CTLM.Start();

            CTLM.AfterButtonClick += CTLM_AfterButtonClick;

        }

        private void CTLM_AfterButtonClick(object sender, CTLMantenimientoNet.CTLMEventArgs e)
        {
            switch (e.ButtonClick)
            {
                case "btnAdd":
                    cboPlaceAdvise.Value = cboPlaceAdviseDef.Value;
                    cboPlaceAffected.Value = cboPlaceAffectedDef.Value;
                    cboService.Value = cboServiceDef.Value;
                    cboMatter.Value = cboMatterDef.Value;
                    dateStart.Value = CalDateDef.SelectionStart;
                    dateEnd.Value = CalDateDef.SelectionEnd;
                    break;
            }

        }
    }
    
}
