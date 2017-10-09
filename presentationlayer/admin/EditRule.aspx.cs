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
    public partial class EditRule : System.Web.UI.Page
    {
        string docid; string qustionid;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClsCommon comman = new ClsCommon();
            if (!IsPostBack)
            {

                docid = Request.QueryString[0];
                qustionid = Request.QueryString[2];
                //comman.ddlDynamicControl(ddlAddauditrule, docid);
                //comman.ddlRuletype(ddlruletype);


                //var btn = (ImageButton)sender;
                //var item = (RepeaterItem)btn.NamingContainer;
                //HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
                BlAuditQuestionRule setData = new BlAuditQuestionRule();
                MlAuditQuestionRule Doc = new MlAuditQuestionRule();
                //Doc.AuditRuleClassificationID = Convert.ToInt32(hd.Value);
                Doc.AuditRuleClassificationID = Convert.ToInt32(Request.QueryString["AuditRuleClassificationID"].ToString());
                List<MlAuditQuestionRule> lstOfAuditQuestion = new List<MlAuditQuestionRule>();
                lstOfAuditQuestion = setData.getAuditQuestionRule(Doc);


                //txtEditAuditQuestion.Text = lstOfAuditQuestion[0].AuditQuestion;
                //var customerList = db.CustomerDetails.ToList();

                comman.ddlDynamicControl(ddlEditSelectCompany, docid);
                comman.ddlRuletype(ddlEditDocumentType);

                ddlEditSelectCompany.SelectedValue = lstOfAuditQuestion[0].DynamicControlId.ToString();

                ddlEditDocumentType.SelectedValue = lstOfAuditQuestion[0].RuleType;

                txtEditAuditQuestion.Text = lstOfAuditQuestion[0].RuleExpression;



            }




        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //string stringtxtDynamicControlID = String.Format("{0}", Request.Form["hiddenDynamicControlID"]);

            if(valid())

            {
                MlAuditQuestionRule Doc = new MlAuditQuestionRule();
                Doc.DynamicControlId = Convert.ToInt32(ddlEditSelectCompany.SelectedValue);
                Doc.RuleType = ddlEditDocumentType.SelectedItem.Text;
                Doc.RuleExpression = txtEditAuditQuestion.Text.Trim();
                Doc.AuditRuleClassificationID = Convert.ToInt32(Request.QueryString["AuditRuleClassificationID"].ToString());


                BlAuditQuestionRule setData = new BlAuditQuestionRule();
                int res = setData.updateAuditRuleClassificationnd(Doc.AuditRuleClassificationID, Doc.DynamicControlId, Doc.RuleExpression, Doc.RuleType, ddlEditSelectCompany.SelectedItem.Text.Trim());

                //MlAuditQuestion Doc1 = new MlAuditQuestion();
                //Doc1.CustomerID = Convert.ToInt32(ddlCustomer.SelectedValue);
                //Doc1.DocumentTypeID = Convert.ToInt32(ddlDocumentType.SelectedValue);

                //BindgridView(Doc1);


                if (res == 1)
                {
                    string message = "Audit Rule Updated Successfully";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

                }

                Response.Redirect("CreateAuditQusRule.aspx?DocumentTypeID=" + Request.QueryString[0] + "& CustID=" + Request.QueryString[1] + "& AuditQuestionID=" + Request.QueryString[2]);

            }

           

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAuditQusRule.aspx?DocumentTypeID=" + Request.QueryString[0] + "& CustID=" + Request.QueryString[1] + "& AuditQuestionID=" + Request.QueryString[2]);



        }

        private bool valid()
        {
            
            if (ddlEditSelectCompany.SelectedItem.Text.Trim() == "" || ddlEditSelectCompany.SelectedIndex == 0 || ddlEditDocumentType.SelectedItem.Text.Trim() == "" || ddlEditDocumentType.SelectedIndex == 0 )
            {
                lblMessage.Text = "Fill all information";
                lblMessage.Visible = true;
                
                return false;
            }
            else
            {

                return true;

            }
        }

        protected void ddlEditDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEditDocumentType.SelectedItem.Text.Trim() == "DateFormat")
                txtEditAuditQuestion.Text = "isdate(dynamiccontrolvaluetext) = 1 OR (dynamiccontrolvaluetext) like '[0-1][0-9]/[0-3][0-9]/[0-9][0-9]'";
            else if (ddlEditDocumentType.SelectedItem.Text.Trim() == "SplitDataApprove")
                txtEditAuditQuestion.Text = "CDL,Non-CDL";
            else if (ddlEditDocumentType.SelectedItem.Text.Trim() == "SplitData")
                txtEditAuditQuestion.Text = "$Missing,$Illegible,$Incomplete,$Mismatch";
            else if (ddlEditDocumentType.SelectedItem.Text.Trim() == "NotNullOrEmpty")
                txtEditAuditQuestion.Text = "LEN(dynamiccontrolvaluetext)!=0";
            else if (ddlEditDocumentType.SelectedItem.Text.Trim() == "DateWithYearComparison")
                txtEditAuditQuestion.Text = "(DATEDIFF(year,GETDATE(), dateadd(year,1,DynamicControlValueText)))>0";
            else
                txtEditAuditQuestion.Text = string.Empty;




        }
    }
}