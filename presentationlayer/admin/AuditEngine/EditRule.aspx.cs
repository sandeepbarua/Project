using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DataAccessLayer;
using PresentationLayer.Model;
using MiddleLayer;
using System.Data;

namespace PresentationLayer.Admin
{
    public partial class EditRule : System.Web.UI.Page
    {
        int DocumentId; string qustionid; int AuditRuleClassificationID;
        BLConsumeApi objApi = new BLConsumeApi();
        ClsCommon clsCommonObj = new ClsCommon();
        int DummyCompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DummyCompany"]);
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
                if (Request.QueryString["DocumentTypeID"] != null && int.TryParse(Request.QueryString["DocumentTypeID"], out DocumentId))
                {
                    BindgridView(DummyCompanyID, DocumentId);
                }
                if (Session["Rule"] != null)
                {
                    txtEditAuditQuestion.Text = Convert.ToString(Session["Rule"]).Trim();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["AuditQuestion"]))
                {
                    lblAuditQuestion.Text = Request.QueryString["AuditQuestion"].Trim();
                }
            }
        }
        private void BindgridView(int CustomerId, int DocumentId)
        {
            List<MlAuditQuestion> lstOfAuditQuestion = objApi.Sp_GetAuditQuestion(CustomerId, DocumentId);
            ViewState["vsUser"] = lstOfAuditQuestion;
            clsCommonObj.ddlDynamicControl(ddlDataField, DocumentId);
        }
        protected void rb_Logic_CheckedChanged(object sender, EventArgs e)
        {
            dtFuctionList = BlAuditQuestion.ddlFunctionList(ddlRuleType, rb_Logic.Text);
        }

        protected void rb_calculate_CheckedChanged(object sender, EventArgs e)
        {
            dtFuctionList = BlAuditQuestion.ddlFunctionList(ddlRuleType, rb_Logic.Text);
        }
        protected void btnEditRule_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["AuditRuleClassificationID"] != null && int.TryParse(Request.QueryString["AuditRuleClassificationID"], out AuditRuleClassificationID))
            {
                objApi.Sp_UpdateAuditRuleClassification(txtEditAuditQuestion.Text.Trim(), ddlRuleType.SelectedItem.ToString(), ddlDataField.SelectedItem.ToString(), AuditRuleClassificationID);
                Response.Redirect("CreateAuditQusRule.aspx?DocumentTypeID=" + Request.QueryString["DocumentTypeID"] + "&CustID=" + DummyCompanyID + "&AuditQuestionID=" + Session["AuditQuestionID"], false);
            }
        }
        
        protected void btnCacel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAuditQusRule.aspx?DocumentTypeID=" + Request.QueryString["DocumentTypeID"] + "&CustID=" + DummyCompanyID + "&AuditQuestionID=" + Session["AuditQuestionID"], false);
        }
        protected void ddlRuleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList myDropdown = sender as DropDownList;
            if (myDropdown.SelectedIndex >= 0)
            {
                string SeletedField = ddlRuleType.SelectedItem.Text.Trim();
                DataRow[] row = dtFuctionList.Select("FuntionName='" + SeletedField + "'");
                if (row.Length > 0)
                {
                    txtEditAuditQuestion.Text = Convert.ToString(row[0]["Expression"]);
                }
                else
                {
                    txtEditAuditQuestion.Text = string.Empty;
                }            
            }

        }
    } 
}