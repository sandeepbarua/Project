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
    public partial class AddAuditQusRule : System.Web.UI.Page
    {
        int docid;
        string qustionid;
        protected void Page_Load(object sender, EventArgs e)
        {

            ClsCommon comman = new ClsCommon();
            if (!IsPostBack)
            {
               
                if(int.TryParse(Request.QueryString[0],out docid))
                {
                    comman.ddlDynamicControl(ddlAddauditrule, docid);
                    comman.ddlRuletype(ddlruletype);
                }
               
                //qustionid = Request.QueryString[2];
                //comman.ddlDynamicControl(ddlAddauditrule, docid);
                //comman.ddlRuletype(ddlruletype);


                //var btn = (ImageButton)sender;
                //var item = (RepeaterItem)btn.NamingContainer;
                //HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
                BlAuditQuestionRule setData = new BlAuditQuestionRule();
                MlAuditQuestionRule Doc = new MlAuditQuestionRule();
                //Doc.AuditRuleClassificationID = Convert.ToInt32(hd.Value);
                ////Doc.AuditRuleClassificationID = Convert.ToInt32(Request.QueryString["AuditRuleClassificationID"].ToString());
                ////List<MlAuditQuestionRule> lstOfAuditQuestion = new List<MlAuditQuestionRule>();
                ////lstOfAuditQuestion = setData.getAuditQuestionRule(Doc);


                //txtEditAuditQuestion.Text = lstOfAuditQuestion[0].AuditQuestion;
                //var customerList = db.CustomerDetails.ToList();

                

                //ddlEditSelectCompany.SelectedValue = lstOfAuditQuestion[0].DynamicControlId.ToString();

                //ddlEditDocumentType.SelectedValue = lstOfAuditQuestion[0].RuleType;

                //txtEditAuditQuestion.Text = lstOfAuditQuestion[0].RuleExpression;



            }

        }

        protected void BtnCreateRule_Click(object sender, EventArgs e)
        {
            
                try
                {
                    int dynamiccontrolid = Convert.ToInt32(ddlAddauditrule.SelectedValue);
                    int auditqustionid = Convert.ToInt32(Request.QueryString[2]);
                    string ruleexpression = txtAuditQuestion.Text;
                    string ruletype = ddlruletype.SelectedValue;
                    ClsCommon common = new ClsCommon();
                    BLConsumeApi objApi = new BLConsumeApi();
                    objApi.sp_InsertAuditRuleClassificationData(auditqustionid, dynamiccontrolid, ruleexpression, ruletype, ddlAddauditrule.SelectedItem.Text.Trim());
                    //int msg = common.insertAuditRuleClassificationnd(auditqustionid, dynamiccontrolid, ruleexpression, ruletype, ddlAddauditrule.SelectedItem.Text.Trim());
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

                    Response.Redirect("CreateAuditQusRule.aspx?DocumentTypeID=" + Request.QueryString[0] + "& CustID=" + Request.QueryString[1] + "& AuditQuestionID=" + Request.QueryString[2]);

                    // Auditrule();
                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }


            
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAuditQusRule.aspx?DocumentTypeID=" + Request.QueryString[0] + "& CustID=" + Request.QueryString[1] + "& AuditQuestionID=" + Request.QueryString[2]);
            
        }

       

        protected void ddlruletype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlruletype.SelectedItem.Text.Trim() == "DateFormat")
                txtAuditQuestion.Text = "isdate(dynamiccontrolvaluetext) = 1 OR (dynamiccontrolvaluetext) like '[0-1][0-9]/[0-3][0-9]/[0-9][0-9]'";
            else if (ddlruletype.SelectedItem.Text.Trim() == "SplitDataApprove")
                txtAuditQuestion.Text = "CDL,Non-CDL";
            else if (ddlruletype.SelectedItem.Text.Trim() == "SplitData")
                txtAuditQuestion.Text = "$Missing,$Illegible,$Incomplete,$Mismatch";
            else if (ddlruletype.SelectedItem.Text.Trim() == "NotNullOrEmpty")
                txtAuditQuestion.Text = "LEN(dynamiccontrolvaluetext)!=0";
            else if (ddlruletype.SelectedItem.Text.Trim() == "DateWithYearComparison")
                txtAuditQuestion.Text = "(DATEDIFF(year,GETDATE(), dateadd(year,1,DynamicControlValueText)))>0";
            else
                txtAuditQuestion.Text = string.Empty;




        }
    }
}