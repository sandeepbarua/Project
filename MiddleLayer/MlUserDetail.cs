using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public partial class MlUserDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public string RoleName { get; set; }
        public int UserDetailsID { get; set; }
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
