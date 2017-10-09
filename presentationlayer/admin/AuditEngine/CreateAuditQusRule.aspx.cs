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
            BindTable();
        }
        public void BindTable()
        {
            ClsCommon comman = new ClsCommon();
            if (Request.QueryString[0] != null && Request.QueryString[2] != null)
            {
                docid = Request.QueryString[0];
                qustionid = Request.QueryString[2];
                Reptrule.DataSource = comman.getAuditRule(docid, qustionid);
                Reptrule.DataBind();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            HiddenField hd2 = (HiddenField)item.FindControl("HiddenField4");
            HiddenField hd3 = (HiddenField)item.FindControl("HiddenField3");
            HiddenField hd4 = (HiddenField)item.FindControl("HiddenField5");

            
            Session["Rule"] = hd3.Value;
            Response.Redirect("EditRule.aspx?DocumentTypeID=" + Request.QueryString[0] + "&AuditQuestion=" + hd4.Value + "&AuditRuleClassificationID=" + hd.Value+ "&DynamicControlId=" + hd2.Value,false);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            Response.Redirect("EditRule.aspx?AuditRuleClassificationID=" + hd.Value);            
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddAuditQusRule.aspx?DocumentTypeID=" + Request.QueryString[0] + "& CustID=" + Request.QueryString[1] + "& AuditQuestionID=" + Request.QueryString[2]);
        }

        protected void OnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (LinkButton)sender;
                var item = (RepeaterItem)btn.NamingContainer;
                HiddenField hd = (HiddenField)item.FindControl("HiddenField2");
                int id = Convert.ToInt32(hd.Value);
                ClsCommon common = new ClsCommon();
                common.DeleteAuditRuleClassification(id);
                BindTable();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }           
        }
    }
}