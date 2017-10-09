using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public class MlCustomerDetails
    {
        public int CompanyID { get; set; }
        public int FADV_CustomerID { get; set; }
        public int LocationID { get; set; }
        public string CompanyName { get; set; }
        public string LocationName { get; set; }
        public string DateofCreation { get; set; }
        public string DateDisplayCreation
        {
            get
            {
                DateTime dt = new DateTime();
                if (DateTime.TryParse(DateofCreation, out dt))
                {
                    return dt.ToString("MM-dd-yyyy");
                }
                else
                {
                    return DateofCreation;
                }
            }
        }
        public string DateOfModification { get; set; }
    }
}
