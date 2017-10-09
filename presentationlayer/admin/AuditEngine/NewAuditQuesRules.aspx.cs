using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using PresentationLayer.Model;
using PresentationLayer.App_Code;
using System.Data;
using System.Threading;

namespace PresentationLayer.AdminNew
{
    public partial class NewAuditQuesRules : System.Web.UI.Page
    {
        DQFEntities db = new DQFEntities();
        ClsCommon clsCommonObj = new ClsCommon();
        string DynamicControlLabel = string.Empty;
        int DynamicControlId = 0;
        int DocumentId = 0;
        //int DocumentId = 0;
        private DataTable dtFuctionList
        {
            get
            {
                if (ViewState["functionList"] == null)
                {
                    return new DataTable();
                }
                return (DataTable)ViewState["functionList"]; ;
            }
            set
            {
                ViewState["functionList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DocumentId = Convert.ToInt32(Request.QueryString["CustomerID"]);
                //clsCommonObj.ddlGetDocTypeByCustId(ddlDocumentType, DocumentId);
                BindDocumentType();
                if (!String.IsNullOrEmpty(Request.QueryString["DocumentTypeID"]))
                {
                    ddlDocumentType.SelectedIndex = ddlDocumentType.Items.IndexOf(ddlDocumentType.Items.FindByValue(Request.QueryString["DocumentTypeID"]));
                }
                //bindAuditQuestion();
                if (!String.IsNullOrEmpty(Request.QueryString["QuestionID"]))
                {
                    //ddlAuditQuestion.SelectedIndex = ddlAuditQuestion.Items.IndexOf(ddlAuditQuestion.Items.FindByValue(Request.QueryString["QuestionID"]));
                }
            }
            else
            {
                if (Session["DynamicControlId"] != null)
                {
                    DynamicControlId = Convert.ToInt32(Session["DynamicControlId"]);
                }
                if (Session["DynamicControlLabel"] != null)
                {
                    DynamicControlLabel = Convert.ToString(Session["DynamicControlLabel"]);
                }
            }

        }
        protected void rptDynamicControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddl_Parameters");
                if (int.TryParse(Request.QueryString["DocumentTypeID"], out DocumentId))
                { 
                    clsCommonObj.ddlDynamicControl(ddl, DocumentId);
                 }             
            }
        }
        protected void rptDynamicControl_ItemCommand(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.GetType() == typeof(DropDownList))
            {

            }
        }
        private void BindDocumentType()
        {
            var CustomerId = Convert.ToInt32(Request.QueryString["CustomerID"]);
            var documentTypeList = db.DocumentTypes.Where(x => x.CustomerID == CustomerId && x.IsActive == true).ToList();
            ddlDocumentType.DataSource = documentTypeList;
            ddlDocumentType.DataTextField = "DocumentTypeName";
            ddlDocumentType.DataValueField = "DocumentTypeID";
            ddlDocumentType.DataBind();
            ddlDocumentType.Items.Insert(0, new ListItem("Select DocumentType", "-1"));
            ddlDocumentType.SelectedIndex = 0;
            ddlDocumentType.Enabled = true;
        }
        //private void bindAuditQuestion()
        //{
        //    var DocumentId = Convert.ToInt32(Request.QueryString["DocumentTypeID"]);
        //    var CustomerId = Convert.ToInt32(Request.QueryString["CustomerID"]);
        //    var AuditQuestionsList = db.AuditQuestions.Where(x => x.CustomerID == CustomerId && x.IsActive == true && x.DocumentID == DocumentId).ToList();
        //    ddlAuditQuestion.DataSource = AuditQuestionsList;
        //    ddlAuditQuestion.DataTextField = "AuditQuestion1";
        //    ddlAuditQuestion.DataValueField = "AuditQuestionsID";
        //    ddlAuditQuestion.DataBind();
        //    ddlAuditQuestion.Items.Insert(0, new ListItem("Select Audit Question", "-1"));
        //    ddlAuditQuestion.SelectedIndex = 0;
        //    ddlAuditQuestion.Enabled = true;
        //}

        protected void Rb_Logical_CheckedChanged(object sender, EventArgs e)
        {
            ddlSelectFunction.Enabled = true;
            //dtFuctionList = BlAuditQuestion.ddlFuctionName(ddlSelectFunction, "Logical");
        }

        protected void Rb_Calculation_CheckedChanged(object sender, EventArgs e)
        {
            ddlSelectFunction.Enabled = true;
            //dtFuctionList = BlAuditQuestion.ddlFuctionName(ddlSelectFunction, "Calculation");
        }

        protected void ddlSelectFunction_SelectedIndexChanged(object sender, EventArgs e)
        {

            // var docId = ddlSelectFunction.SelectedValue;
            DataRow[] row = dtFuctionList.Select("Id='" + ddlSelectFunction.SelectedValue + "'");
            int? noOfParameter = Convert.ToInt32(row[0]["NoOfParameters"]);

            DataRow[] LabelRows= dtFuctionList.Select("FuntionName='" + ddlSelectFunction.SelectedItem.ToString() + "'");
            string documentName = ddlDocumentType.SelectedItem.Text;
            DataTable result = new DataTable();
            result.Columns.Add("ParameterName", typeof(string));
            if (noOfParameter.HasValue)
            {
                foreach (DataRow dr in LabelRows)
                {
                    result.Rows.Add(Convert.ToString(dr["LabelName"]));
                }
                //for (int i = 1; i <= noOfParameter; i++)
                //{
                //    result.Rows.Add($"{documentName}_Parameter_{i}");
                //}
            }
            rptDynamicControl.DataSource = result;
            rptDynamicControl.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OpenModalWindow();", true);
        }

        protected void ddlDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDocumentType.SelectedIndex >= 0)
            {
               // bindAuditQuestion();
            }
        }
        protected void BtnCreateRule_Click(object sender, EventArgs e)
        {
            try
            {
                int AuditQuentionID = Convert.ToInt32(1);//ddlAuditQuestion.SelectedValue
                string RuleExpression = txtAuditQuestion.Text.Replace("'", "''");
                string RuleType = ddlSelectFunction.SelectedItem.ToString();
                //int msg = clsCommonObj.insertAuditRuleClassificationnd(AuditQuentionID, DynamicControlId, RuleExpression, RuleType, DynamicControlLabel);
                BLConsumeApi objApi = new BLConsumeApi();
                objApi.sp_InsertAuditRuleClassificationData(AuditQuentionID, DynamicControlId, RuleExpression, RuleType, DynamicControlLabel);
                //if (msg == 1)
                //{
                //    string message = "Audit Rule Added Successfully";
                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //    sb.Append("<script type = 'text/javascript'>");
                //    sb.Append("window.onload=function(){");
                //    sb.Append("alert('");
                //    sb.Append(message);
                //    sb.Append("')};");
                //    sb.Append("</script>");
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                //}
                Response.Redirect("../Admin/CreateAuditQusRule.aspx?DocumentTypeID=" + Request.QueryString["DocumentTypeID"] + "&CustID=" + Request.QueryString["CustomerID"] + "&AuditQuestionID=" + AuditQuentionID, false);                
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {               
                throw new Exception("Create rule failed, Exception Details:"+ex.ToString(), ex);
            }
            finally
            {
            }
        }
        protected void myDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            DropDownList myDropdown = sender as DropDownList;

            //DynamicControlLabel = Convert.ToString(myDropdown.SelectedItem);
            //DynamicControlId = Convert.ToInt32(myDropdown.SelectedValue);
            Session["DynamicControlLabel"]= Convert.ToString(myDropdown.SelectedItem);
            Session["DynamicControlId"] = Convert.ToInt32(myDropdown.SelectedValue);
            if (ddlSelectFunction.SelectedItem.Text.Trim() == "DateFormat")
                txtAuditQuestion.Text = "isdate(dynamiccontrolvaluetext) = 1 OR (dynamiccontrolvaluetext) like '[0-1][0-9]/[0-3][0-9]/[0-9][0-9]'";
            else if (ddlSelectFunction.SelectedItem.Text.Trim() == "SplitDataApprove")
                txtAuditQuestion.Text = "CDL,Non-CDL";
            else if (ddlSelectFunction.SelectedItem.Text.Trim() == "SplitData")
                txtAuditQuestion.Text = "$Missing,$Illegible,$Incomplete,$Mismatch";
            else if (ddlSelectFunction.SelectedItem.Text.Trim() == "NotNullOrEmpty")
                txtAuditQuestion.Text = "LEN(dynamiccontrolvaluetext)!=0";
            else if (ddlSelectFunction.SelectedItem.Text.Trim() == "DateWithYearComparison")
                txtAuditQuestion.Text = "(DATEDIFF(year,GETDATE(), dateadd(year,1,DynamicControlValueText)))>0";
            else
                txtAuditQuestion.Text = string.Empty;

        }
    }   
}