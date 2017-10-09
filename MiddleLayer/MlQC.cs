using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class MlQC
    {

        public int CPScreenDataID { get; set; }
        public int FaxID { get; set; }
        public System.DateTime ReceiveDate { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string SourceFile { get; set; }
        public Nullable<int> TotalNumberOfPages { get; set; }
        public Nullable<System.DateTime> DateofCreation { get; set; }
        public Nullable<System.DateTime> DateOfModification { get; set; }
        public string Comment { get; set; }
        public Nullable<int> RoboActivityID { get; set; }
        public string CMS_CPScreenDocumentdTypeId { get; set; }
        public string Labelling { get; set; }
        public string UserId { get; set; }
        public Nullable<bool> IsLabellingDone { get; set; }
        public Nullable<System.DateTime> ProcessingStarted { get; set; }
        public Nullable<System.DateTime> ProcessingEnd { get; set; }
        public Nullable<bool> PassbyQA { get; set; }

        public string HrsMM { get; set; }
}
}
