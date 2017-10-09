using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    [Serializable]
    public class MlAuditQuestion
    {
        public int AuditQuestionsID { get; set; }
        public string AuditQuestion { get; set; }
        public int DocumentID { get; set; }
        public string DocumentTypeName { get; set; }
        public int CustomerID { get; set; }

        public string DocumentDescription { get; set; }
        public string UserName { get; set; }

        public int UserID { get; set; }
        public string CompanyName { get; set; }

       
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}
