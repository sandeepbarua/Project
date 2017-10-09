using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer
{
    public class DataEntryClass
    {
    }
    //public class DynamicControlResponse
    //{
    //    public int DynamicControlID { get; set; }
    //    public int DocumentTypeID { get; set; }
    //    public string labelName { get; set; }
    //    public string ControlName { get; set; }
    //    public string ControlType { get; set; }
    //    public string DynamicControlValueText { get; set; }
    //    public int DyanamicControlValueID { get; set; }
    //    public string DropDownValue { get; set; }
    //    public int? OrderBy { get; set; }
    //}
    //public class DynamicControlResponseList : List<DynamicControlResponse>
    //{

    //}

    //public class DynamicControlWithValueResponse
    //{
    //    public int DynamicControlValudId { get; set; }
    //    public string DynamicControlValueText { get; set; }
    //    public string ButtonId { get; set; }
    //}

    //public class DynamicControlWithControlTypeResponse : DynamicControlResponse
    //{
    //    public string ControlTypeId { get; set; }
    //    public string DocumentTypeName { get; set; }
    //}

    public class DropDownValue
    {
        public string Text { get; set; }
        public int DyanamicControlID { get; set; }
        public int DynamicControlValueId { get; set; }
    }

    public class UpdateControlClass
    {
        public string Text { get; set; }
        public int DyanamicControlID { get; set; }
        public int DynamicControlValueId { get; set; }
    }

    //public class DocumentTypeDetails
    //{
    //    public int? DocumentTypeId { get; set; }
    //    public int? CountForFaxId { get; set; }

    //}

    public class DocumentTypeReviewList
    {
        public int? DocumentTypeId { get; set; }
        public bool IsReview = false;
        public int? CountForFaxId { get; set; }

    }

    public class DropDownValueTextTable
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }


}