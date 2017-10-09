using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using System.Web.UI.HtmlControls;

namespace PresentationLayer.Admin.Template
{
    public partial class EditTemplate : System.Web.UI.Page
    {
        int DummyCompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DummyCompany"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserTemplateEngine") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)//DocName
            {
                txtTemplateName.Text = Request.QueryString["DocName"];
                txtDocumentDescrition.Text = Request.QueryString["Desc"];
            }
        }

        protected void OnEdit_Click(object sender, EventArgs e)//Sp_CheckIsTempalteExist
        {
            try
            {
                BLConsumeApi objApi = new BLConsumeApi();
                List<MIDocumentType> obj = objApi.Sp_CheckIsTempalteExist(txtTemplateName.Text.Trim());
                if (obj != null)
                {
                    if (obj.Count > 0 && txtTemplateName.Text.Trim() != Request.QueryString["DocName"].Trim())
                    {
                        lbExistmsg.Visible = true;
                    }
                    else
                    {
                        lbExistmsg.Visible = false;
                        updateTemplate();
                    }
                }
                else
                {
                    lbExistmsg.Visible = false;
                    updateTemplate();
                }
               
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void updateTemplate()
        {
            MIDoctumentType updateData = new MIDoctumentType();
            updateData.DocumentTypeID = Convert.ToInt32(Request.QueryString["DocId"]);
            updateData.DocumentTypeName = txtTemplateName.Text;
            updateData.DocumentDescription = txtDocumentDescrition.Text;
            updateData.UserID = Convert.ToInt32(Session["UserId"]);
            updateData.CustomerID = DummyCompanyID;
            BlDocType update = new BlDocType();
            update.updateDocumentType(updateData);
            Response.Redirect("TemplateDocumnetType.aspx", false);
        }
        protected void OnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TemplateDocumnetType.aspx", false);
        }
    }
}