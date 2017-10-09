using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public  class PreviousEmploymentDetail
    {
        public int PreviousEmploymentDetailId { get; set; }
        public string EmployerName { get; set; }
        public string EmploymentStartDate { get; set; }
        public string EmploymentEndDate { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> TempCPScreenDataID { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}
