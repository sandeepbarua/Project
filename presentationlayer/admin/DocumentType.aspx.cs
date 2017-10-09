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

namespace PresentationLayer.Admin
{
    public partial class DocumentType1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                //BlDocType.ddlCustomerName(ddlCompanyName)
                ClsCommon.ddlCustomerID(ddlCompanyName); 
                //ClsCommon.ddlCustomerID(ddlAddCompany);
                ClsCommon.ddlCustomerID(ddlEditSelectCompany);
                bindGrid();
            }
        }
        private void bindGrid()
        {
            ReptUse.DataSource = new BlDocType().getDocumentType();
            ReptUse.DataBind();
        }
        protected void BtnCreateuser_Click(object sender, EventArgs e)
        {
            MIDocumentType Doc = new MIDocumentType();
            Doc.DocumentTypeAlias = Convert.ToString(txtDocumentTypeName.Text);
            Doc.CustomerID = Convert.ToInt32(ddlAddCompany.SelectedValue);
            Doc.DocumentDescription = Convert.ToString(txtDocumentDescrition.Text);
            
            Doc.UserID = 3;
            //Doc.UserID = Convert.ToInt32(Session["UserId"]);
            BlDocType setData = new BlDocType();
            setData.setDocumentType(Doc);
            setData.setDynamicControl("Start Page","txtStartPage","TextBox", "Incomplete,Missing,Illegible");
            setData.setDynamicControl("End Page", "txtEndPage", "TextBox", "Incomplete,Missing,Illegible");
            setData.setDynamicControl("Location", "txtLocationName", "DropDown", "Incomplete,Missing,Illegible");
            txtDocumentDescrition.Text = "";
            txtDocumentTypeName.Text = "";


            // GridView1.DataSource = new BlUserDetails().getUserDetails();
            //  GridView1.DataBind();

            int id = Convert.ToInt32(ddlCompanyName.SelectedValue);
            bindRepeater(id);
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string stringtxtDocumentTypeID = String.Format("{0}", Request.Form["hiddenDocumentTypeID"]);

            MIDoctumentType updateData = new MIDoctumentType();

            updateData.DocumentTypeID = Convert.ToInt32(stringtxtDocumentTypeID); 
            updateData.DocumentTypeName = txtEditDocumentTypeName.Text;
            updateData.CustomerID = Convert.ToInt32(ddlEditSelectCompany.SelectedValue);
            updateData.DocumentDescription = txtEditDocumentDescription.Text;
            updateData.UserID =Convert.ToInt32( Session["UserId"]);
            BlDocType update = new BlDocType();
            update.updateDocumentType(updateData);
            int id = Convert.ToInt32(ddlCompanyName.SelectedValue);
            bindRepeater(id);
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }
        protected void OnDelete_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            MIDoctumentType deleteData = new MIDoctumentType();
            deleteData.DocumentTypeID = Convert.ToInt32(hd.Value);
            BlDocType del = new BlDocType();
            del.ReferenceDeleteDocumentType(deleteData);
            del.DeleteDocumentType(deleteData);
            int id = Convert.ToInt32(ddlCompanyName.SelectedValue);
            bindRepeater(id);
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {

        }
     
        protected void ReptUse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }

        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlCompanyName.SelectedValue);
            bindRepeater(id);
        }
        private void bindRepeater(int id = 0)
        {
            if (id == 0)
            {

                ddlAddCompany.ClearSelection();
                bindGrid();
                btnadd.Visible = false;

            }
            else
            {
                ReptUse.DataSource = new BlDocType().getDocumentTypeById(id);
                ReptUse.DataBind();
                btnadd.Visible = true;
                if (BlDocType.ddlCustomerAddName(ddlAddCompany, id))
                {
                    ddlAddCompany.SelectedIndex = 1;
                }
                else
                {

                }
            }
        }
    }
}