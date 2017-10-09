using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public class MlFaxIdDumo
    {
        public int TempFAXIDDumpID { get; set; }
        public int FaxID { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfModification { get; set; }

    }
}
