using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class MlDocumentRelationTemp
    {
        public int FaxId { get; set; }
        public DateTime DataEntryStartTime { get; set; }
        public DateTime DataEntryEndTime { get; set; }
        public int TotalNumberOfPages { get; set; }

        public int TotalError { get; set; }

        public int UserId { get; set; }

        public int DocumentTypeId { get; set; }
        public int CountForFaxId { get; set; }
        public bool IsReview { get; set; }
    }
}
