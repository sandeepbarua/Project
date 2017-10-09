using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    [Serializable]
    public class MlRptRobotProcess
    {
        public int RobotActivityID { get; set; }
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string TotalProcess { get; set; }

        public int CPScreenDataID { get; set; }

        public int FaxID { get; set; }
        public string ReceiveDate { get; set; }

        public string CompanyName { get; set; }
        public string TotalNumberofPages { get; set; }
        public string Comment { get; set; }

        public string LabellingText { get; set; }

        public string User { get; set; }
    }
}
