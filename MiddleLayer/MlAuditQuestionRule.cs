using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class MlAuditQuestionRule
    {
        public int AuditRuleClassificationID { get; set; }
        public int AuditQuestionId { get; set; }
        public int DynamicControlId { get; set; }
        public string RuleExpression { get; set; }
        public string  RuleType { get; set; }

    }
}
