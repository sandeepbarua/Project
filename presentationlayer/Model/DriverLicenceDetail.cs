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
    
    public partial class DriverLicenceDetail
    {
        public int DriverLicenceId { get; set; }
        public string Restriction { get; set; }
        public string Endorsement { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> TempCPScreenDataID { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }
}