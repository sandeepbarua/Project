using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public  class CurrentResidenceDetail
    {
        public int CurrentResidenceDetailID { get; set; }
        public string CurrentResidenceStreetAddress { get; set; }
        public string CurrentResidenceCity { get; set; }
        public string CurrentResidenceState { get; set; }
        public string CurrentResidenceZipcode { get; set; }
        public string CurrentResidenceDuration { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> TempCPScreenDataID { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}
