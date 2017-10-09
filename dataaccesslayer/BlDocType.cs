using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Reflection;

namespace DataAccessLayer
{
    
    public class BlDocType
    {

        public void ddlBindDocumentTypeById(DropDownList ddlDocType, int companyId)
        {
            ListItem li = new ListItem();
            li.Text = "<--Select Document Type-->";
            li.Value = "0";
            List<MIDocumentType> lstUserDetails = new List<MIDocumentType>();
            List<MIDocumentType> reader = new BLConsumeApi().getAllDocumentTypeByCustomerID(companyId);
            if (reader == null)
            {
            }
            else
            {
                foreach (var r in reader)
                {
                    li = new ListItem();
                    li.Value = Convert.ToString(r.DocumentTypeID);
                    li.Text = r.DocumentTypeName;
                    ddlDocType.Items.Add(li);
                }
            }
        }
        public static bool ddlCustomerAddName(DropDownList ddlCustomerAddName, int CustomerID)
        {
            ddlCustomerAddName.Items.Clear();
            DataTable dtddlCustomerAddName;
            ListItem li = new ListItem();
            ddlCustomerAddName.Items.Add(li);
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(new BLConsumeApi().bindDropDowngetCustomerDetailsByID(CustomerID));
            dtddlCustomerAddName = dt;
            for (int i = 0; i < dtddlCustomerAddName.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtddlCustomerAddName.Rows[i]["CompanyID"].ToString();
                li.Text = dtddlCustomerAddName.Rows[i]["CompanyName"].ToString();
                ddlCustomerAddName.Items.Add(li);
                return true;
            }
            return true;
        }
        public List<MIDocumentType> getDocumentTypeById(int companyId)
        {
            List<MIDocumentType> lstUserDetails = new List<MIDocumentType>();
            List<MIDocumentType> reader = new BLConsumeApi().getAllDocumentTypeByCustomerID(companyId);
            if (reader == null)
            {
            }
            else { 
            foreach (var r in reader)
            {
                MIDocumentType setData = new MIDocumentType();
                setData.DocumentTypeID = r.DocumentTypeID;
                setData.DocumentTypeName = r.DocumentTypeName;
                setData.DocumentTypeAlias = r.DocumentTypeAlias;
                setData.CustomerID = r.CustomerID;
                setData.DocumentDescription = r.DocumentDescription;
                setData.UserID = r.UserID;
                setData.UserFirstName = r.UserFirstName;
                setData.UserLastName = r.UserLastName;
                setData.UserName = setData.UserFirstName + " " + setData.UserLastName;
                setData.CompanyName = r.CompanyName;
                setData.DateOfCreation = ChageDateFormat(r.DateOfCreation);
                setData.DateOfModification = ChageDateFormat(r.DateOfModification);
                    setData.SourceType = r.SourceType;
                    setData.DocumentCode = r.DocumentCode;
                lstUserDetails.Add(setData);
            }
            }
            return lstUserDetails;
        }
        public static string ChageDateFormat(string strDate)
        {            
            DateTime date = new DateTime();
            string NewDate = string.Empty;
            try
            {
                if (DateTime.TryParse(strDate, out date))
                {
                    NewDate = date.ToString("dd/MM/yyyy HH:mm tt");
                }
                return NewDate;
            }
            catch
            {
                return strDate;
            }
        }
        public List<MIDocumentType> getDocumentType()
        {
            List<MIDocumentType> lstUserDetails = new List<MIDocumentType>();
            List<MIDocumentType> reader = new BLConsumeApi().getAllDocumentType();
            foreach (var r in reader)
            {
                MIDocumentType setData = new MIDocumentType();
                setData.DocumentTypeID = r.DocumentTypeID;
                setData.DocumentTypeName = r.DocumentTypeName;
                setData.DocumentTypeAlias = r.DocumentTypeAlias;
                setData.CustomerID = r.CustomerID;
                setData.DocumentDescription = r.DocumentDescription;
                setData.UserID = r.UserID;
                setData.UserFirstName = r.UserFirstName;
                setData.UserLastName = r.UserLastName;
                setData.UserName = setData.UserFirstName + " " + setData.UserLastName;
                setData.CompanyName = r.CompanyName;
                lstUserDetails.Add(setData);
            }
            return lstUserDetails;
        }
        public void setDynamicControl(string labelName, string controlName, string controlType, string DropDownValue)
        {
            

            List<MIDocumentType> reader = new BLConsumeApi().getTopDocumentTypeByCustomerID();
            foreach (var r in reader)
            {
                MIDocumentType setData = new MIDocumentType();
                setData.DocumentTypeID = r.DocumentTypeID;
                BLConsumeApi consume = new BLConsumeApi();
                consume.setDocumentDynamicDetails(setData.DocumentTypeID, labelName, controlName, controlType, DropDownValue);
            }

        }
        public bool setDocumentType(MIDocumentType Doc)
        {
            BLConsumeApi consume = new BLConsumeApi();
            consume.setDocumentTypeDetails(Doc.DocumentTypeAlias, Doc.CustomerID, Doc.DocumentDescription, Doc.UserID);
            return true;
        }
        public bool updateDocumentType(MIDoctumentType setData)
        {
            BLConsumeApi api = new BLConsumeApi();
            api.updateDocumentTypeDetails(setData.DocumentTypeName, setData.CustomerID, setData.DocumentDescription, setData.UserID, setData.DocumentTypeID);
            return true;
        }
        public bool DeleteDocumentType(MIDoctumentType deldata)
        {
            BLConsumeApi api = new BLConsumeApi();
            api.deleteDocumentTypeDetails(deldata.DocumentTypeID);
            return true;
        }
        public bool ReferenceDeleteDocumentType(MIDoctumentType deldata)
        {
            BLConsumeApi api = new BLConsumeApi();
            api.ReferenceDeleteDocumentType(deldata.DocumentTypeID);
            return true;

        }
        

    }
}