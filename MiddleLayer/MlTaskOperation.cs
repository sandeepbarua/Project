using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public partial class MlTaskOperation
    {
        public int TaskOperationId { get; set; }
        public Nullable<int> FaxId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
    }
}
