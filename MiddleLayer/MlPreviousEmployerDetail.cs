using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    using System;
    using System.Collections.Generic;
    public partial class MlPreviousEmployerDetail
    {
        public int PreviousEmployerDetailId { get; set; }
        public string PreviousEmployerName { get; set; }
        public string PreviousEmployerStreetAddress { get; set; }
        public string PreviousEmployerCity { get; set; }
        public string PreviousEmployerState { get; set; }
        public string PreviousEmployerZipcode { get; set; }
        public string PreviousEmploymentStartDate { get; set; }
        public string PreviousEmploymentEndDate { get; set; }
        public string ReasonForLeavingPreviousEmployments { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> TempCPScreenDataID { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}
