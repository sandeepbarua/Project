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
    public partial class CreateRule : System.Web.UI.Page
    {
        string docid; string qustionid;
        int DocumentId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClsCommon comman = new ClsCommon();
            if (!IsPostBack)
            {

                docid = Request.QueryString[0];
                qustionid = Request.QueryString[2];
                if (int.TryParse(Request.QueryString[0], out DocumentId))
                {
                    comman.ddlDynamicControl(ddlAddauditrule, DocumentId);
                }
                comman.ddlRuletype(ddlruletype);


            }
            if (int.TryParse(Request.QueryString[0], out DocumentId))
            {
                comman.ddlDynamicControl(ddlEditSelectCompany, DocumentId);
            }

            comman.ddlRuletype(ddlEditDocumentType);

            Reptrule.DataSource = comman.getAuditRule(docid, qustionid);
            Reptrule.DataBind();
        }

       
        protected void BtnCreateRule_Click(object sender, EventArgs e)
        {
            try
            {
                int dynamiccontrolid = Convert.ToInt32(ddlAddauditrule.SelectedValue);
                int auditqustionid = Convert.ToInt32(Request.QueryString[2]);
                string ruleexpression = txtAuditQuestion.Text;
                string ruletype = ddlruletype.SelectedValue;
                BLConsumeApi objApi = new BLConsumeApi();
                objApi.sp_InsertAuditRuleClassificationData(auditqustionid, dynamiccontrolid, ruleexpression, ruletype, ddlAddauditrule.SelectedItem.Text.Trim());
                //ClsCommon common = new ClsCommon();
                //int msg = common.insertAuditRuleClassificationnd(auditqustionid,dynamiccontrolid , ruleexpression, ruletype, ddlAddauditrule.SelectedItem.Text.Trim());
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

                // Auditrule();
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
        }
        protected void OnDelete_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            int id = Convert.ToInt32(hd.Value);
            ClsCommon common = new ClsCommon();
            common.DeleteAuditRuleClassification(id);
           // Auditrule();
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


        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            MlAuditQuestionRule Doc = new MlAuditQuestionRule();
            Doc.DynamicControlId = Convert.ToInt32(ddlEditSelectCompany.SelectedValue);
            Doc.RuleType = ddlEditDocumentType.SelectedItem.Text;
            Doc.RuleExpression = txtEditAuditQuestion.Text.Trim();
            Doc.AuditRuleClassificationID = Convert.ToInt32(txtDynamicCtrID.Text.Trim());


            BlAuditQuestionRule setData = new BlAuditQuestionRule();
            setData.updateAuditRuleClassificationnd(Doc.AuditRuleClassificationID, Doc.RuleExpression, Doc.RuleType, ddlEditSelectCompany.SelectedItem.Text.Trim());


        }

        protected void lnkEdit_Click1(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");


            Response.Redirect("EditRule.aspx?AuditRuleClassificationID=" + hd.Value);
        }
    }
}