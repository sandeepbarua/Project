using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public  class MlUserDetails
    {
        public int UserDetailsID { get; set; }
        public string HeaderName { get; set; }
        public string BccEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string SMTPHost { get; set; }
        public string SmtpPort { get; set; }
        public string SMTPCredentialFromAddress { get; set; }
        public string SMTPCedentialFromPassword { get; set; }
        public string EmailNotificationPath { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int FADVUserID { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public int LoginAttempt { get; set; }
        public Nullable<bool> LocktheAccount { get; set; }
        public System.DateTime DateOfCreation { get; set; }
        public System.DateTime DateOfModification { get; set; }
        public string EmailId { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
}
