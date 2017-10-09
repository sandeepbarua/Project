using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class MlAuditRule
    {
        public int AuditRuleClassification { get; set; }
        public string AuditQuestion { get; set; }
        public string labelName { get; set; }
        public int DynamicControlId { get; set; }
        public string RuleExpression { get; set; }
        public string RuleType { get; set; }
    }
}
