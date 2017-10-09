using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public  class DriverLicenceDetail
    {
        public int DriverLicenceId { get; set; }
        public string Restriction { get; set; }
        public string Endorsement { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> TempCPScreenDataID { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}
