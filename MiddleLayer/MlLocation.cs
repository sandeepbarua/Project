using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class MlLocation
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string fadvId { get; set; }
        public string CompanyName { get; set;}
        
        public string Fadv_LocationID { get; set; }
        public int CustomerId { get; set; }

        public int CompanyID { get; set; }
        public int FADV_CustomerID { get; set; }

           
    }

    public class MlLocationForExcel
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }      
        public string CompanyName { get; set; }
        public string Fadv_LocationID { get; set; }
  


    }
}
