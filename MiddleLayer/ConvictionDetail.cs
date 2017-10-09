using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public  class ConvictionDetail
    {
        public int ConvictionDetailsId { get; set; }
        public string DateOfConviction { get; set; }
        public string Offense { get; set; }
        public string Location { get; set; }
        public string TypeOfVehicleOperated { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> TempCPScreenDataID { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}
