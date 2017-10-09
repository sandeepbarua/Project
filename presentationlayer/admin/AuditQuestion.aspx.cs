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


namespace PresentationLayer.Admin
{
    public partial class AuditQuestion : System.Web.UI.Page
    {

        BlCustomerDetails cust = new BlCustomerDetails();
        DQFEntities db = new DQFEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessAuditQuestion") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "active");
            if (!IsPostBack)
            {
                BlAuditQuestion.ddlCustomerID(ddlEditSelectCompany);
                BlAuditQuestion.ddlGetDocType(ddlEditDocumentType);

                BlAuditQuestion.ddlCustomerID(ddlCustomer);
                BlAuditQuestion.ddlGetDocType(ddlDocumentType);


                //BindgridView();
                BindCustomer();
                // BindgridView();
            }

        }

        private void BindCustomer()
        {
            //var customerList = db.CustomerDetails.ToList();
            //ddlCustomer.DataSource = customerList;
            //ddlCustomer.DataTextField = "CompanyName";
            //ddlCustomer.DataValueField = "CompanyID";
            //ddlCustomer.DataBind();
            //ddlCustomer.Items.Insert(0, new ListItem("Select Customer", "-1"));
            //ddlCustomer.SelectedIndex = 0;

            List<MlCustomerDetails> lstOfCust = new List<MlCustomerDetails>();

            lstOfCust = cust.getCustomerDetails();
            ddlAddCompany.DataSource = lstOfCust;
            ddlAddCompany.DataTextField = "CompanyName";
            ddlAddCompany.DataValueField = "CompanyID";
            ddlAddCompany.DataBind();
            ddlAddCompany.Items.Insert(0, new ListItem("Select Customer", "-1"));
            ddlAddCompany.SelectedIndex = 0;
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var customerId = Convert.ToInt32(ddlCustomer.SelectedValue);
            var documentTypeList = db.DocumentTypes.Where(x => x.CustomerID == customerId && x.IsActive == true).ToList();
            ddlDocumentType.DataSource = documentTypeList;
            ddlDocumentType.DataTextField = "DocumentTypeName";
            ddlDocumentType.DataValueField = "DocumentTypeID";
            ddlDocumentType.DataBind();
            ddlDocumentType.Items.Insert(0, new ListItem("Select Document Type", "-1"));
            ddlDocumentType.SelectedIndex = 0;
            ddlDocumentType.Enabled = true;
            //  dvDocumentType.Visible = true;
            //btnadd.Visible = false;
            MlAuditQuestion Doc = new MlAuditQuestion();
            Doc.CustomerID = Convert.ToInt32(ddlCustomer.SelectedValue);
            BindgridView(Doc);
        }


        protected void ddlAddCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            var customerId = Convert.ToInt32(ddlAddCompany.SelectedValue);
            var documentTypeList = db.DocumentTypes.Where(x => x.CustomerID == customerId).ToList();
            ddlAddDocumentType.DataSource = documentTypeList;
            ddlAddDocumentType.DataTextField = "DocumentTypeName";
            ddlAddDocumentType.DataValueField = "DocumentTypeID";
            ddlAddDocumentType.DataBind();
            ddlAddDocumentType.Items.Insert(0, new ListItem("Select Document Type", "-1"));
            ddlAddDocumentType.SelectedIndex = 0;
            ddlAddDocumentType.Enabled = true;
            //  dvDocumentType.Visible = true;
            //btnadd.Visible = false;
        }


        protected void ddlDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // lblDocumentType.Text = ddlDocumentType.SelectedItem.Text;
            MlAuditQuestion Doc = new MlAuditQuestion();
            Doc.CustomerID = Convert.ToInt32(ddlCustomer.SelectedValue);
            Doc.DocumentID = Convert.ToInt32(ddlDocumentType.SelectedValue);

            btnadd.Visible = true;
            BindgridView(Doc);
        }

        private void BindgridView(MlAuditQuestion Doc)
        {

            List<MlAuditQuestion> lstOfAuditQuestion = new List<MlAuditQuestion>();
            BlAuditQuestion Auditobj = new BlAuditQuestion();
            lstOfAuditQuestion = Auditobj.getAuditQuestion(Doc);
            ReptUse.DataSource = lstOfAuditQuestion;
            ReptUse.DataBind();
            ViewState["vsUser"] = lstOfAuditQuestion;

            List<MlCustomerDetails> lstOfCust = new List<MlCustomerDetails>();
            lstOfCust = cust.getCustomerDetails();
            ddlAddCompany.DataSource = lstOfCust;
            ddlAddCompany.DataTextField = "CompanyName";
            ddlAddCompany.DataValueField = "CompanyID";
            ddlAddCompany.DataBind();
            ddlAddCompany.Items.Insert(0, new ListItem("Select Customer", "-1"));
            ddlAddCompany.SelectedIndex = 0;


        }

        protected void btnadd_Click(object sender, EventArgs e)
        {



        }

        protected void Image2_Click(object sender, ImageClickEventArgs e)
        {
            BlAuditQuestion getData = new BlAuditQuestion();
            BlAuditQuestion datas = new BlAuditQuestion();
            string fileName = "AuditQuestion.xls";
            DataTable dt = new DataTable();
            dt.Columns.Add("SerialNo", typeof(Int32));
            dt.Columns.Add("AuditQuestion", typeof(string));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("DocumentTypeName", typeof(string));
            dt.Columns.Add("DocumentDescription", typeof(string));
            int CountSno = 1;
            foreach (var value in datas.getAuditQuestion())
            {
                var row = dt.NewRow();
                row["SerialNo"] = CountSno;
                row["AuditQuestion"] = Convert.ToString(value.AuditQuestion);
                row["CompanyName"] = Convert.ToString(value.CompanyName);
                row["DocumentTypeName"] = Convert.ToString(value.DocumentTypeName);
                row["DocumentDescription"] = Convert.ToString(value.DocumentDescription);

                dt.Rows.Add(row);
                CountSno++;


            }
            ClsCommon.CreateExcelFile(dt, fileName);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            BlAuditQuestion setData = new BlAuditQuestion();
            MlAuditQuestion Doc = new MlAuditQuestion();
            Doc.AuditQuestionsID = Convert.ToInt32(hd.Value);
            List<MlAuditQuestion> lstOfAuditQuestion = new List<MlAuditQuestion>();
            lstOfAuditQuestion = setData.getAuditQuestion(Doc);


            txtEditAuditQuestion.Text = lstOfAuditQuestion[0].AuditQuestion;
            var customerList = db.CustomerDetails.ToList();
            //ddlEditSelectCompany.DataSource = customerList;
            //ddlEditSelectCompany.DataTextField = "CompanyName";
            //ddlEditSelectCompany.DataValueField = "CompanyID";
            //ddlEditSelectCompany.DataBind();
            //ddlEditSelectCompany.SelectedIndex = ddlEditSelectCompany.Items.IndexOf(ddlEditSelectCompany.Items.FindByValue(Convert.ToString(lstOfAuditQuestion[0].CustomerID)));





        }

        protected void OnDelete_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            BlAuditQuestion setData = new BlAuditQuestion();
            MlAuditQuestion Doc = new MlAuditQuestion();
            Doc.AuditQuestionsID = Convert.ToInt32(hd.Value);
            setData.DeleteAuditQuestion(Doc);
            Doc.CustomerID = Convert.ToInt32(ddlCustomer.SelectedValue);
            Doc.DocumentID = Convert.ToInt32(ddlDocumentType.SelectedValue);

            BindgridView(Doc);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string stringtxtDynamicControlID = String.Format("{0}", Request.Form["hiddenDynamicControlID"]);

            MlAuditQuestion Doc = new MlAuditQuestion();
            Doc.AuditQuestion = Convert.ToString(txtEditAuditQuestion.Text);
            Doc.CustomerID = Convert.ToInt32(ddlEditSelectCompany.SelectedValue);
            Doc.DocumentID = Convert.ToInt32(ddlEditDocumentType.SelectedValue);
            Doc.UserID = Convert.ToInt32(Session["UserId"]);
            Doc.AuditQuestionsID = Convert.ToInt32(stringtxtDynamicControlID);
            BlAuditQuestion setData = new BlAuditQuestion();
            setData.updateAuditQuestion(Doc);
            MlAuditQuestion Doc1 = new MlAuditQuestion();
            Doc1.CustomerID = Convert.ToInt32(ddlCustomer.SelectedValue);
            Doc1.DocumentID = Convert.ToInt32(ddlDocumentType.SelectedValue);

            BindgridView(Doc1);

        }

        protected void BtnCreateuser_Click(object sender, EventArgs e)
        {
            MlAuditQuestion Doc = new MlAuditQuestion();
            Doc.AuditQuestion = Convert.ToString(txtAuditQuestion.Text);
            Doc.DocumentID = Convert.ToInt32(ddlAddDocumentType.SelectedValue);
            Doc.CustomerID = Convert.ToInt32(ddlAddCompany.SelectedValue);
            Doc.UserID = Convert.ToInt32(Session["UserId"]);
            BlAuditQuestion Aq = new BlAuditQuestion();
            Aq.setAuditQuestion(Doc);
            MlAuditQuestion Doc1 = new MlAuditQuestion();
            Doc1.CustomerID = Convert.ToInt32(ddlCustomer.SelectedValue);
            Doc1.DocumentID = Convert.ToInt32(ddlDocumentType.SelectedValue);

            BindgridView(Doc1);


        }

        protected void ddlEditSelectCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            var customerId = Convert.ToInt32(ddlEditSelectCompany.SelectedValue);
            var documentTypeList = db.DocumentTypes.Where(x => x.CustomerID == customerId).ToList();
            ddlEditDocumentType.DataSource = documentTypeList;
            ddlEditDocumentType.DataTextField = "DocumentTypeName";
            ddlEditDocumentType.DataValueField = "DocumentTypeID";
            ddlEditDocumentType.DataBind();
            // ddlEditDocumentType.Items.Insert(0, new ListItem("Select DocumentType", "-1"));
            // ddlEditDocumentType.SelectedIndex = 0;
            // ddlEditDocumentType.Enabled = true;
        }

        protected void lnkBtnCreateRule_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            HiddenField hd2 = (HiddenField)item.FindControl("HiddenField2");
            HiddenField hd3 = (HiddenField)item.FindControl("HiddenField3");

            Response.Redirect("CreateAuditQusRule.aspx?DocumentTypeID=" + hd2.Value + "& CustID=" + hd3.Value + "& AuditQuestionID=" + hd.Value); 
            ////BlAuditQuestion setData = new BlAuditQuestion();
            ////MlAuditQuestion Doc = new MlAuditQuestion();
            ////Doc.AuditQuestionID = Convert.ToInt32(hd.Value);
            ////List<MlAuditQuestion> lstOfAuditQuestion = new List<MlAuditQuestion>();
            ////lstOfAuditQuestion = setData.getAuditQuestion(Doc);


            ////txtEditAuditQuestion.Text = lstOfAuditQuestion[0].AuditQuestion;
            ////var customerList = db.CustomerDetails.ToList();
            //ddlEditSelectCompany.DataSource = customerList;
            //ddlEditSelectCompany.DataTextField = "CompanyName";
            //ddlEditSelectCompany.DataValueField = "CompanyID";
            //ddlEditSelectCompany.DataBind();
            //ddlEditSelectCompany.SelectedIndex = ddlEditSelectCompany.Items.IndexOf(ddlEditSelectCompany.Items.FindByValue(Convert.ToString(lstOfAuditQuestion[0].CustomerID)));





        }
    }
}