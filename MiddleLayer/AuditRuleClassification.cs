using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class AuditRuleClassification
    {
        public int AuditRuleClassificationId { get; set; }
        public int AuditQuestionId { get; set; }
        public int DynamicControlId { get; set; }
        public string RuleExpression { get; set; }
        public string DynamicControlLabel { get; set; }
        public string RuleType { get; set; }
        public string AuditQuestion { get; set; }
        public string labelName { get; set; }
    }
}
