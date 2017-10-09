using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public  class TypeOfEquipmentDetails
    {
        public int TypeOfEquipmentDetailId { get; set; }
        public string TypeOfEquipment { get; set; }
        public string Miles { get; set; }
        public string DrivingFrom { get; set; }
        public string DrivingTo { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> TempCPScreenDataID { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}
