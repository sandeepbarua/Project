using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class MIDoctumentType
    {
               public int DocumentTypeID { get; set; }
        public string DocumentTypeName { get; set; }
        public int CustomerID { get; set; }

        public string DocumentDescription { get; set; }
        public string UserName { get; set; }

        public int UserID { get; set; }
        public string CompanyName { get; set; }


        public int UserDetailsID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
