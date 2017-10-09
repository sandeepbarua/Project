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

namespace PresentationLayer.Admin.AuditEngine
{
    public partial class EditAuditQuestion : System.Web.UI.Page
    {
        int DummyCompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DummyCompany"]);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Request.QueryString["AuditQuestionsID"] !=null && Request.QueryString["AuditQuestion"] != null) 
                {
                    txtEditAuditQuestion.Text = Convert.ToString(Request.QueryString["AuditQuestion"]).Trim();
                }
                if(Request.QueryString["DocName"] != null)
                {
                    lblDocumentType.Text = Request.QueryString["DocName"].Trim();
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BLConsumeApi objApi = new BLConsumeApi();
                string stringtxtDynamicControlID = Request.QueryString["AuditQuestionsID"];
                int DocumentId = Convert.ToInt32(Request.QueryString["DocID"]);
                int QuestionId = Convert.ToInt32(Request.QueryString["AuditQuestionsID"]);
                objApi.Sp_UpdateAuditQuestion(txtEditAuditQuestion.Text, DummyCompanyID, DocumentId, QuestionId);
                Response.Redirect("TamplateAuditQuestion.aspx?DocumentId="+Request.QueryString["DocID"], false);
            }
            catch(Exception ex)
            {
                ex.ToString();
            }

        }
        
         protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("TamplateAuditQuestion.aspx?DocumentId=" + Request.QueryString["DocID"], false);
        }

        }
}