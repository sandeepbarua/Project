using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public    class MlDynamicControl
    {
        public int DynamicControlID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentTypeName { get; set; }
        public string labelName { get; set; }
        public string ControlName { get; set; }
        public string ControlType { get; set; }
        public int ControlTypeID { get; set; }
        public string CompanyName { get; set; }

        public string DropDownValues { get; set; }

        public int Column1 { get; set; }

        public int? OrderBy { get; set; }

        public bool IsActive { get; set; }

        public string DropDownValue { get; set; }
    }
}
