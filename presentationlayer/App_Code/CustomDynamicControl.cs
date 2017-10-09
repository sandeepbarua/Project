using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.App_Code
{
    public class CustomDynamicControl
    {
       public int  DynamicControlID { get; set; }
        public int DocumentTypeID { get; set; }
        public string labelName { get; set; }
        public string ControlName { get; set; }
        public string ControlType { get; set; }
        public int ControlTypeID { get; set; }
        public int Order_No { get; set; }

        public string DropDown { get; set; }

    }
}