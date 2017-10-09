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
    
    public partial class DocumentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocumentType()
        {
            this.DynamicControls = new HashSet<DynamicControl>();
        }
    
        public int DocumentTypeID { get; set; }
        public string DocumentTypeName { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string DocumentDescription { get; set; }
        public int UserID { get; set; }
        public System.DateTime DateOfCreation { get; set; }
        public System.DateTime DateOfModification { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string DocumentTypeAlias { get; set; }
    
        public virtual UserDetail UserDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DynamicControl> DynamicControls { get; set; }
    }
}
