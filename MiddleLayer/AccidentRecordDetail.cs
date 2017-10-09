using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public  class AccidentRecordDetail
    {
        public int AccidentRecordDetailId { get; set; }
        public string NatureOfAccident { get; set; }
        public string DateOfAccident { get; set; }
        public string Fatalities { get; set; }
        public string Injuries { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> TempCPScreenDataID { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}
