using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    using System;

public partial class MlPreviousResidenceDetail
{
    public int PreviousResidenceDetailID { get; set; }
    public string PreviousResidenceStreetAddress { get; set; }
    public string PreviousResidenceCity { get; set; }
    public string PreviousResidenceState { get; set; }
    public string PreviousResidenceZipcode { get; set; }
    public string PreviousResidenceDuration { get; set; }
    public Nullable<int> UserId { get; set; }
    public Nullable<int> DocumentTypeId { get; set; }
    public Nullable<int> TempCPScreenDataID { get; set; }
    public Nullable<int> CountForFaxId { get; set; }
}
}
