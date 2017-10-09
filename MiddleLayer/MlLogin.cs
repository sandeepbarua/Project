using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public class MlLogin
    {
        public int UserDetailsID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int FADVUserID { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }
        public int LoginAttempt { get; set; }
        public string LocktheAccount { get; set; }
        public string EmailId { get; set; }
        public string DateOfCreation { get; set; }
        public DateTime DateOfModification { get; set; }

        public int Column1 { get; set; }

    }
}
