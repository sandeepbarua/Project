using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using PresentationLayer.Model;
using MiddleLayer;

namespace PresentationLayer.Admin
{
    public partial class CreateAuditQusRule : System.Web.UI.Page
    {
        string docid; string qustionid;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClsCommon comman = new ClsCommon();
            docid = Request.QueryString[0];
            qustionid = Request.QueryString[2];
            Reptrule.DataSource = comman.getAuditRule(docid, qustionid);
            Reptrule.DataBind();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //Response.Redirect("EditRule.aspx");

            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");


            Response.Redirect("EditRule.aspx?DocumentTypeID=" + Request.QueryString[0] + "& CustID=" + Request.QueryString[1] + "& AuditQuestionID=" + Request.QueryString[2] +"&AuditRuleClassificationID=" + hd.Value);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");


            Response.Redirect("EditRule.aspx?AuditRuleClassificationID=" + hd.Value);

            //try
            //{
            //    int dynamiccontrolid = Convert.ToInt32(ddlAddauditrule.SelectedValue);
            //    int auditqustionid = Convert.ToInt32(Request.QueryString[2]);
            //    string ruleexpression = txtAuditQuestion.Text;
            //    string ruletype = ddlruletype.SelectedValue;
            //    ClsCommon common = new ClsCommon();
            //    int msg = common.insertAuditRuleClassificationnd(auditqustionid, dynamiccontrolid, ruleexpression, ruletype);
            //    if (msg == 1)
            //    {
            //        string message = "Audit Rule Added Successfully";
            //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //        sb.Append("<script type = 'text/javascript'>");
            //        sb.Append("window.onload=function(){");
            //        sb.Append("alert('");
            //        sb.Append(message);
            //        sb.Append("')};");
            //        sb.Append("</script>");
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

            //    }

            //    // Auditrule();
            //}
            //catch (Exception Ex)
            //{
            //    Ex.ToString();
            //}




            //var btn = (ImageButton)sender;
            //var item = (RepeaterItem)btn.NamingContainer;
            //HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            //BlAuditQuestionRule setData = new BlAuditQuestionRule();
            //MlAuditQuestionRule Doc = new MlAuditQuestionRule();
            //Doc.AuditRuleClassificationID = Convert.ToInt32(hd.Value);
            //List<MlAuditQuestionRule> lstOfAuditQuestion = new List<MlAuditQuestionRule>();
            //lstOfAuditQuestion = setData.getAuditQuestionRule(Doc);


            ////txtEditAuditQuestion.Text = lstOfAuditQuestion[0].AuditQuestion;
            ////var customerList = db.CustomerDetails.ToList();

            //ddlEditSelectCompany.SelectedValue = lstOfAuditQuestion[0].DynamicControlId.ToString();

            //ddlEditDocumentType.SelectedItem.Text  = lstOfAuditQuestion[0].RuleType;

            //txtEditAuditQuestion.Text = lstOfAuditQuestion[0].RuleExpression;


            //ddlEditSelectCompany.DataSource = customerList;
            //ddlEditSelectCompany.DataTextField = "CompanyName";
            //ddlEditSelectCompany.DataValueField = "CompanyID";
            //ddlEditSelectCompany.DataBind();
            //ddlEditSelectCompany.SelectedIndex = ddlEditSelectCompany.Items.IndexOf(ddlEditSelectCompany.Items.FindByValue(Convert.ToString(lstOfAuditQuestion[0].CustomerID)));





        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            //PostBackUrl = "AddAuditQusRule.aspx"

            Response.Redirect("AddAuditQusRule.aspx?DocumentTypeID=" + Request.QueryString[0] + "& CustID=" + Request.QueryString[1] + "& AuditQuestionID=" + Request.QueryString[2]);
        }

        protected void OnDelete_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            int id = Convert.ToInt32(hd.Value);
            ClsCommon common = new ClsCommon();
            common.DeleteAuditRuleClassification(id);

            Response.Redirect("CreateAuditQusRule.aspx?DocumentTypeID=" + Request.QueryString[0] + "& CustID=" + Request.QueryString[1] + "& AuditQuestionID=" + Request.QueryString[2]);
            // Auditrule();
        }
    }
}