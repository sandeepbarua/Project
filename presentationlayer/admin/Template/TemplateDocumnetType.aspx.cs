using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using System.Web.UI.HtmlControls;
using System.Data;
using PresentationLayer.Model;
using System.Threading;

namespace PresentationLayer.Template
{
    public partial class TemplateDocumnetType : System.Web.UI.Page
    {
      
        int DummyCompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DummyCompany"]);
        int countforRadioButton;
        int countforsequence;
        protected void Page_Load(object sender, EventArgs e)
        {
            // HtmlGenericControl tabContact = Master.FindControl("AccessTamplateDocumentType") as HtmlGenericControl;
            //tabContact.Attributes.Add("class", "box_active");
            HtmlGenericControl tabContact = Master.FindControl("AccessUserTemplateEngine") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)
            {
                bindGrid();
            }
        }
        private void bindGrid()
        {
            ReptUse.DataSource = new BlDocType().getDocumentTypeById(DummyCompanyID);
            ReptUse.DataBind();
        }
        protected void lbDocument_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (LinkButton)sender;
                var item = (RepeaterItem)btn.NamingContainer;
                HiddenField hd = (HiddenField)item.FindControl("HiddenField1");                             
                Response.Redirect("TemplateDynamicControl.aspx?DocId=" + hd.Value, false);
            }
            catch (ThreadAbortException)
            {

            }
            catch { }

        }
        protected void lbAddDataPoint_Click(object sender, EventArgs e)
        {
            try
            {
                //var btn = (LinkButton)sender;
                //var item = (RepeaterItem)btn.NamingContainer;
                //HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
                //MIDocumentType mIDocumentType = new MIDocumentType();
                //mIDocumentType.DocumentTypeName = Convert.ToString(hd.Value);
                //Session["DocumentName"] = mIDocumentType.DocumentTypeName;
                //HiddenField hd1 = (HiddenField)item.FindControl("HiddenField2");
                //Response.Redirect("AddControl.aspx?DocumentId=" + hd.Value+"&DocName="+hd1.Value, false);
                var btn = (LinkButton)sender;
                var item = (RepeaterItem)btn.NamingContainer;
                HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
                Response.Redirect("TemplateDynamicControl.aspx?DocId=" + hd.Value, false);
            }
            catch (ThreadAbortException)
            {

            }
            catch { }

        }
        protected void BtnCreateuser_Click(object sender, EventArgs e)
        {
            try
            {
                BLConsumeApi BLConsumeApi = new BLConsumeApi();
                MIDocumentType Doc = new MIDocumentType();
                Doc.DocumentTypeAlias = Convert.ToString(txtDocumentTypeName.Text);
                Doc.CustomerID = DummyCompanyID;
                Doc.DocumentDescription = Convert.ToString(txtDocumentDescrition.Text);
                Doc.UserID = Convert.ToInt32(Session["UserId"]);
                             BlDocType setData = new BlDocType();
                setData.setDocumentType(Doc);
                setData.setDynamicControl("Start Page", "txtStartPage", "TextBox", "Incomplete,Missing,Illegible");
                setData.setDynamicControl("End Page", "txtEndPage", "TextBox", "Incomplete,Missing,Illegible");
                setData.setDynamicControl("Location", "txtLocationName", "DropDown", "Incomplete,Missing,Illegible");
                txtDocumentDescrition.Text = "";
                txtDocumentTypeName.Text = "";
                bindGrid();

            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (LinkButton)sender;
                var item = (RepeaterItem)btn.NamingContainer;
                HiddenField hd = (HiddenField)item.FindControl("HiddenField2");
                HiddenField hd1 = (HiddenField)item.FindControl("HiddenField3");
                HiddenField hd2 = (HiddenField)item.FindControl("HiddenField1");
                Response.Redirect("TemplateDynamicControl.aspx?DocName=" + hd.Value + "&Desc=" + hd1.Value+"&DocId="+ hd2.Value, false);
            }
            catch (ThreadAbortException)
            {

            }
            catch { }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (LinkButton)sender;
                var item = (RepeaterItem)btn.NamingContainer;
                HiddenField hd = (HiddenField)item.FindControl("HiddenField2");
                HiddenField hd1 = (HiddenField)item.FindControl("HiddenField3");
                HiddenField hd2 = (HiddenField)item.FindControl("HiddenField1");
                string description = hd1.Value.Replace(Environment.NewLine, "");
                Response.Redirect("EditTemplate.aspx?DocName=" + hd.Value + "&Desc=" + description + "&DocId=" + hd2.Value, false);

            }
            catch (ThreadAbortException)
            {

            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
        protected void OnDelete_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            MIDoctumentType deleteData = new MIDoctumentType();
            deleteData.DocumentTypeID = Convert.ToInt32(hd.Value);
            BlDocType del = new BlDocType();
            del.ReferenceDeleteDocumentType(deleteData);
            del.DeleteDocumentType(deleteData);
            bindRepeater(DummyCompanyID);
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTemplate.aspx");
        }
        protected void ReptUse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void bindRepeater(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    bindGrid();
                }
                else
                {
                    ReptUse.DataSource = new BlDocType().getDocumentTypeById(id);
                    ReptUse.DataBind();
                }
            }
            catch { }
        }

    }
}