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
    
    public partial class Document_Table_Relation
    {
        public decimal Id { get; set; }
        public Nullable<int> DocumentId { get; set; }
        public string Ref_tblname { get; set; }
        public string Ref_tblnameColumn { get; set; }
        public string Ref_tblnameColumnType { get; set; }
        public Nullable<bool> active { get; set; }
        public string AuditQuestion { get; set; }
        public Nullable<int> AuditQuestionOrder { get; set; }
        public string Remarks { get; set; }
    }
}
