//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PresentationLayer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CMS_CPScreenData_Temp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_CPScreenData_Temp()
        {
            this.DynamicControlValues = new HashSet<DynamicControlValue>();
            this.TempTaskAssignments = new HashSet<TempTaskAssignment>();
        }
    
        public int TempCPScreenDataID { get; set; }
        public int FaxID { get; set; }
        public Nullable<System.DateTime> ReceiveDate { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string SourceFile { get; set; }
        public Nullable<int> TotalNumberOfPages { get; set; }
        public Nullable<int> NoOfDataEntries { get; set; }
        public Nullable<bool> IsDataEntriesInProgress { get; set; }
        public Nullable<System.DateTime> DateofCreation { get; set; }
        public Nullable<System.DateTime> DateOfModification { get; set; }
        public string Comment { get; set; }
        public Nullable<int> CountAssignedUser { get; set; }
        public Nullable<int> RoboActivitiesID { get; set; }
    
        public virtual CustomerDetail CustomerDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DynamicControlValue> DynamicControlValues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TempTaskAssignment> TempTaskAssignments { get; set; }
    }
}