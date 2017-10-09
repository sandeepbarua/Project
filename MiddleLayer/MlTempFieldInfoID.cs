using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class MlTempFieldInfoID
    {
        public int TempFieldInfoID { get; set; }
        public int TempFAXIDDumpId { get; set; }
        public int FaxID { get; set; }
        public int DocumentTypeID { get; set; }
        public int FieldNameId { get; set; }
        public int UserID { get; set; }
        public string FieldValue { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfModification { get; set; }
        public bool IsMismatchFound { get; set; }
        public bool IsReviewed { get; set; }
    }
}
