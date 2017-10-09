using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class MlCPScreenData
    {
        public int CPScreenDataID { get; set; }
        public int FaxID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int RoboActivityID { get; set; }
        public int UserId { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int CMS_CPScreenDocumentdTypeId { get; set; }
        public string SourceFile { get; set; }
        public string DateofCreation { get; set; }
        public string DateOfModification { get; set; }
        public string Comment { get; set; }
        public string Labelling { get; set; }
        public bool IsLabellingDone { get; set; }

        public string stringProcessingStarted { get; set; }
        public string stringProcessingEnd { get; set; }

        public DateTime ProcessingStarted { get; set; }
        public DateTime ProcessingEnd { get; set; }

        public string TotalProcessingStarted { get; set; }


        public string Processed { get; set; }
    }
}
