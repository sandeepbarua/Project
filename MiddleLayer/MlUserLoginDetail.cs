using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public  class MlUserLoginDetail
    {
        public int? LoginDetailsId { get; set; }
        public int? UserId { get; set; }
        public string DateOfLogin { get; set; }
        public string DateDisplayLogin
        {
            get
            {
                DateTime dt = new DateTime();
                if (DateTime.TryParse(DateOfLogin, out dt))
                {
                    return dt.ToString("MM-dd-yyyy HH:mm tt");
                }
                else
                {
                    return DateOfLogin;
                }
            }
        }
        public string DateOfLogOut { get; set; }
        public string DateDisplayLogout
        {
            get
            {
                DateTime dt = new DateTime();
                if (DateTime.TryParse(DateOfLogOut, out dt))
                {
                    return dt.ToString("MM-dd-yyyy HH:mm tt");
                }
                else
                {
                    return DateOfLogOut;
                }
            }
        }
        public string IPAddress { get; set; }
        public string Event { get; set; }
        public string name { get; set; }
        public string NewDate { get; set; }
        public int? Attempts { get; set; }

        public string EmailId { get; set; }
    }
}
