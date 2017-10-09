using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
  public  class MlField
    {
        public int FieldNameID { get; set; }
        public int DocumentTypeID { get; set; }
        public string FieldName { get; set; }
        public string FieldLabel { get; set; }
    }
}
