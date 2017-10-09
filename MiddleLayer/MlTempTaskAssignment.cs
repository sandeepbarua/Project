using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class MlTempTaskAssignment
    {
        public int TempTaskAssignmentID { get; set; }
        public int FaxID { get; set; }

        public int UserID { get; set; }

        public int ProcessStatus { get; set; }

        public Nullable<int> NoOfPagesCompleted { get; set; }
        public DateTime DateOfCreation { get; set; }

        public DateTime DateOfModification { get; set; }
        public int TempCPScreenDataID { get; set; }
        public bool isProcessed { get; set; }

    }
}
