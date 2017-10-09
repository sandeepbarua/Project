using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
   public  class MIDocumentType
    {
        public int DocumentTypeID { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentTypeAlias { get; set; }
        public int CustomerID { get; set; }

        public string DocumentDescription { get; set; }
        public string DisplayDescription
        {
            get
            {
                if (!string.IsNullOrEmpty(DocumentDescription))
                {
                    if (DocumentDescription.Length > 100)
                    {
                        return DocumentDescription.Substring(0, 100);
                    }
                    else
                    {
                        return DocumentDescription;
                    }
                }
                else
                {
                    return DocumentDescription;
                }
            }
        }
        public string UserName { get; set; }

        public int UserID { get; set; }
        public string CompanyName { get; set; }

        //public int UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string DateOfCreation { get; set; }
        public string DateDisplayCreation
        {
            get
            {
                DateTime dt = new DateTime();
                if (DateTime.TryParse(DateOfCreation, out dt))
                {
                    return dt.ToString("MM-dd-yyyy");
                }
                else
                {
                    return Date;
                }
            }
        }
        public string DateOfModification { get; set; }
        public string DateDisplayModify
        {
            get
            {
                DateTime dt = new DateTime();
                if (DateTime.TryParse(DateOfModification, out dt))
                {
                    return dt.ToString("MM-dd-yyyy");
                }
                else
                {
                    return Date;
                }
            }
        }

        public string FAXID { get; set; }
        public string DocumentName { get; set; }
        public string ClientName { get; set; }

        public string TotalError { get; set; }
        public String Date { get; set; }
        public string DateDisplayText { get {
                DateTime dt = new DateTime();
                if (DateTime.TryParse(Date, out dt))
                    {
                    return dt.ToString("MM-dd-yyyy");
                }
                else
                {
                    return Date;
                }
            }
        }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TotalNumberOfPages { get; set; }
        public string SourceType { get; set; }
        public string DocumentCode { get; set; }
    }
}
