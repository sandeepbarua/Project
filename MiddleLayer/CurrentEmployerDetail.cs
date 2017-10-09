using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public  class CurrentEmployerDetail
    {
        public int CurrentEmployerDetailID { get; set; }
        public string CurrentEmployerName { get; set; }
        public string CurrentEmploymentStartDate { get; set; }
        public string CurrentEmployerStreetAddress { get; set; }
        public string CurrentEmployerCity { get; set; }
        public string CurrentEmployerState { get; set; }
        public string CurrentEmployerZipcode { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> TempCPScreenDataID { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}
