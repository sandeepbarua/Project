using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class MIAuditQuestionFunction
    {
        public int id { get; set; }
        public string FuntionName { get; set; }
        public string FunctionType { get; set; }
        public int NoOfParameters { get; set; }
        public string Expression { get; set; }
        public string LabelName { get; set; }
    }
}
