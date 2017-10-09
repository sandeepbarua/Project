using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using PresentationLayer.Model;
using System.Threading;
using System.Web.UI.HtmlControls;

namespace PresentationLayer.Admin.Template
{
    public partial class AddTemplate : System.Web.UI.Page
    {
        DQFEntities db = new DQFEntities();
        int DummyCompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DummyCompany"]);
        int countforRadioButton;
        int countforsequence;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserTemplateEngine") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
        }
        protected void BtnAddTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                BLConsumeApi objApi = new BLConsumeApi();
                List<MIDocumentType> obj = objApi.Sp_CheckIsTempalteExist(txtTemplateName.Text.Trim());
                if (obj!=null)
                {
                    if (obj.Count > 0)
                    {
                        lbExistmsg.Visible = true;
                    }
                    else
                    {
                        lbExistmsg.Visible = false;
                        InsertTemplate();
                    }
                }
                else
                {
                    lbExistmsg.Visible = false;
                    InsertTemplate();
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void InsertTemplate()
        {
            BLConsumeApi BLConsumeApi = new BLConsumeApi();
            MIDocumentType Doc = new MIDocumentType();
            Doc.DocumentTypeAlias = Convert.ToString(txtTemplateName.Text);
            Doc.CustomerID = DummyCompanyID;
            Doc.DocumentDescription = Convert.ToString(txtDocumentDescrition.Text);
            Doc.UserID = Convert.ToInt32(Session["UserId"]);
            BlDocType setData = new BlDocType();
            setData.setDocumentType(Doc);
            setData.setDynamicControl("Start Page", "txtStartPage", "TextBox", "Incomplete,Missing,Illegible");
            setData.setDynamicControl("End Page", "txtEndPage", "TextBox", "Incomplete,Missing,Illegible");
            setData.setDynamicControl("Location", "txtLocationName", "DropDown", "Incomplete,Missing,Illegible");
            txtDocumentDescrition.Text = "";
            txtTemplateName.Text = "";
            Response.Redirect("TemplateDocumnetType.aspx", false);
        }
        protected void Btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TemplateDocumnetType.aspx", false);
        }
    }
}