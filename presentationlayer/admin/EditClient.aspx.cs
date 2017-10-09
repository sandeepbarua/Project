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
using System.Net;

namespace PresentationLayer.Admin
{
    public partial class EditClient : System.Web.UI.Page
    {
        int DummyCompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DummyCompany"]);
        int intCustomerID;
        BlCustomerDetails cust = new BlCustomerDetails();
        DQFEntities db = new DQFEntities();
        BLConsumeApi objAPI = new BLConsumeApi();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserCustomer") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["FADV_ID"]))
                {
                    txtCustomerID.Text = Request.QueryString["FADV_ID"].Trim();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["ClientName"]))
                {
                    txtCustomerName.Text = Request.QueryString["ClientName"].Trim();
                }
                if (int.TryParse(Request.QueryString["CompanyID"].Trim(), out intCustomerID))
                {
                    CustomerID.Value = Convert.ToString(intCustomerID);
                }
                bindGrid();
                ListtoDataTableConverter converter = new ListtoDataTableConverter();               
                DataTable dt = converter.ToDataTable(new BLConsumeApi().getAllDocumentTypeByCustomerId(DummyCompanyID));
               
            }
        }
       

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                
                    BlLocation objLocation = new BlLocation();


                    string filename = "Location1.xls";
                    System.IO.StringWriter tw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                    DataGrid dgGrid = new DataGrid();
                    dgGrid.DataSource = objLocation.getLocationDetailsByCustomer(Convert.ToInt32(CustomerID.Value));
                    dgGrid.DataBind();

                    //Get the HTML for the control.
                    dgGrid.RenderControl(hw);
                    //Write the HTML back to the browser.
                    //Response.ContentType = application/vnd.ms-excel;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                    this.EnableViewState = false;
                    Response.Write(tw.ToString());
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                                                                               //  HttpContext.Current.Response.End();
                
            }
            catch (Exception ex)
            {

            }

        }
       
        protected void ReptUse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
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
            Session["authorization"] = tokenId;            
            string filename = Path.GetFileName(FileUpload1.FileName);
          
            if (FileUpload1.HasFile)
            {
                objAPI.Sp_DeleteLocationByCustID(Convert.ToInt32(CustomerID.Value));
                FileUpload1.SaveAs(Server.MapPath("~/ExcelLoad") + filename);
                UploadExcelLocation(Path.GetFileName(filename), Server.MapPath("~/ExcelLoad"), "Location", "Sheet1$");
            }
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
        protected void btnCustomizeRule_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField DocumentId = (HiddenField)item.FindControl("HiddenField1");
            HiddenField DocumentName = (HiddenField)item.FindControl("HiddenField2");
            Response.Redirect("CustomizeRule.aspx?DocumentId=" + DocumentId.Value + "&CustID=" + CustomerID.Value+ "&DocumentTypeName=" + DocumentName.Value, false);
        }
        protected void OnDelete_Click(object sender, EventArgs e)
        {
        }
        protected void lbAddDataPoint_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField DocumentId = (HiddenField)item.FindControl("HiddenField1");
            Response.Redirect("DataPoint.aspx?DocID="+ DocumentId.Value+"&CustID=" + CustomerID.Value, false);          
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Customers.aspx", false);
        }
        private void bindGrid()
        {

            ReptUse.DataSource = new BlDocType().getDocumentTypeById(Convert.ToInt32(CustomerID.Value));
            ReptUse.DataBind();
        }


    }
}