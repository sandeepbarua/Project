using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public partial class MlException_FormData
    {
        public int FormDataID { get; set; }
        public int CPScreenDataID { get; set; }
        public int DynamicControlID { get; set; }
        public string DynamicControlValueText { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}
