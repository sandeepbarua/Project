using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public partial class MlTrafficConvictionsDetails
    {
        public int TrafficConvictionsDetailID { get; set; }
        public string Location { get; set; }
        public string VehicleType { get; set; }
        public string DateOfConviction { get; set; }
        public string Charge { get; set; }
        public string Penalty { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> TempCPScreenDataID { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}
