using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using PresentationLayer.Model;
using PresentationLayer.App_Code;
using System.Data;
using System.IO;
using FADVCustomLibrary;


namespace PresentationLayer.Admin
{
    public partial class CopyCompany : System.Web.UI.Page
    {
        int DummyCompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DummyCompany"]);
        int intCustomerID;
        BlCustomerDetails cust = new BlCustomerDetails();
        DQFEntities db = new DQFEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserCustomer") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)
            {
                ListtoDataTableConverter converter = new ListtoDataTableConverter();
               
                DataTable dt = converter.ToDataTable(new BLConsumeApi().getAllDocumentTypeByCustomerId(DummyCompanyID));
                GridView1.DataSource = dt; //db.DocumentTypes.Where(x => x.IsActive == true && x.DocumentTypeName != null && x.CustomerID == DummyCompanyID).ToList();
                GridView1.DataBind();
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            
                try
                {
                BLConsumeApi bLConsumeApi = new BLConsumeApi();
                string CustomerName = txtCustomerName.Text;
                string CustomerID = txtCustomerID.Text;               
                string DocumentName = string.Empty;
                string DocumentCode = string.Empty;
                string SourceType = string.Empty;
                string strValue = string.Empty;
               
                foreach (GridViewRow row in GridView1.Rows)
                {
                    try
                    {
                        strValue = ((HiddenField)row.FindControl("DocID")).Value; //usp_CopyCompany2
                        CheckBox chk = (CheckBox)row.FindControl("chkRow");
                        TextBox txtDocumentName = (TextBox)row.FindControl("txtDocumentName");
                        TextBox txtDocumentCode = (TextBox)row.FindControl("txtDocumentCode");
                        DropDownList ddlSourceType = (DropDownList)row.FindControl("ddlSourceType");
                        ClsCommon common = new ClsCommon();
                        if (chk != null && chk.Checked)
                        {
                            DocumentName = txtDocumentName.Text;
                            DocumentCode = txtDocumentCode.Text;
                            SourceType = ddlSourceType.SelectedItem.ToString();
                            bLConsumeApi.usp_CopyCompany2(DummyCompanyID, CustomerName, Convert.ToInt32(CustomerID), Convert.ToInt32(strValue), DocumentName, SourceType, DocumentCode);
                           
                        }
                        else
                        {
                            DocumentCode = string.Empty;
                            DocumentName = string.Empty;
                            SourceType = string.Empty;
                            strValue = string.Empty;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                    
                }
                InsertBulkLocation();

                Response.Redirect("../Admin/Customers.aspx");

            }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            
        }
        public void InsertBulkLocation()
        {
            string tokenId = Convert.ToString(Session["authorization"]).Replace("bearer ","");
            string CurrentDateTime = Convert.ToString(System.DateTime.Now).Replace("/", "").Replace(".", "").Replace(":", "");
            Session["authorization"] = tokenId;
            string filename = Path.GetFileName(FileUpload1.FileName);
            FileUpload1.SaveAs(Server.MapPath("~/ExcelLoad/") +txtCustomerName.Text.Trim()+ CurrentDateTime + filename);
            UploadExcelLocation(Path.GetFileName(filename), Server.MapPath("~/ExcelLoad"), "Location", "Sheet1$");
        }
        private void UploadExcelLocation(string FileName, string FilePath, string DbTableName, string SheetName)
        {
            DQFBuldLoadRequest req = new DQFBuldLoadRequest();
            req.FileName = FileName;
            req.FileLocation = FilePath;
            req.SheetName = SheetName;
            req.Token = Convert.ToString(Session["authorization"]);
            req.TableName = DbTableName;
            BulkLoad(req);
        }
        public static void BulkLoad(DQFBuldLoadRequest req)
        {

            try
            {
                DQFController obj = new DQFController();
                DataTable DT = obj.ExecuteBulkLoad(req);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //Reset();
        }

        //private bool valid()
        //{

        //    if (ddlDocumentType.SelectedIndex == 0 && txtNewCustName.Text.Trim() == "" && txtNewCustId.Text.Trim() == "" && txtDocumentName.Text.Trim() == "" && ddlSourceType.SelectedIndex == 0)
        //    {
        //        lblMessage.Text = "Fill all information";
        //        lblMessage.Visible = true;
        //        return false;
        //    }            
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}