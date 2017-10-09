using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer
{
    public class LoginAudit
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string EmailId { get; set; }
        public string DateOfLogin { get; set; }
        public string DateOfLogOut { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Activity { get; set; }
        public string TotalNumberOfPages { get; set; }
        public string Comment { get; set; }
        public string Labelling { get; set; }


        public string FormDataID { get; set; }
        public string FaxID { get; set; }
        public string DynamicControlValueText { get; set; }
        public string UserDetailsID { get; set; }
        public string DocumentTypeName { get; set; }
        public string labelName { get; set; }

    }
}
