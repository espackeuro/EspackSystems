using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using CommonTools;
using System.Threading.Tasks;

namespace EspackControls
{
    [Flags] public enum EspackControlTypeEnum
    {
        CTLM = 0x1,
        STATIC = 0x2,
        DEPENDANT = 0x4
    }

    public interface EspackControl:IsValuable
    {
        EspackControlTypeEnum EspackControlType { get; set; }
        EnumStatus Status { get; set; }
        new object Value { get; set; }
        string Name { get; set; }
        string DBField { get; set; }
        bool Add { get; set; }
        bool Upp { get; set; }
        bool Del { get; set; }
        int Order { get; set; }
        bool PK { get; set; }
        bool Search { get; set; }
        object DefaultValue { get; set; }
        Type DBFieldType { get; set; }
        void UpdateEspackControl();
        void ClearEspackControl();
    }


    


}