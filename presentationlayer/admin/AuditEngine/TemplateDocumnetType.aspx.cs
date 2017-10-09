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

namespace PresentationLayer.Admin_New
{
    public partial class TemplateDocumnetType : System.Web.UI.Page
    {
        DQFEntities db = new DQFEntities();
        int DummyCompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DummyCompany"]);       
        int countforRadioButton;
        int countforsequence;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessTamplateDocumentType") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "active");
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
                MIDocumentType mIDocumentType = new MIDocumentType();
                mIDocumentType.DocumentTypeName = Convert.ToString(hd.Value);
                Session["DocumentName"] = mIDocumentType.DocumentTypeName;
                Response.Redirect("TemplateDynamicControl.aspx?DocID=" + mIDocumentType.DocumentTypeName,false);
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
                Doc.DocumentTypeName = Convert.ToString(txtDocumentTypeName.Text);
                Doc.CustomerID = DummyCompanyID;
                Doc.DocumentDescription = Convert.ToString(txtDocumentDescrition.Text);
                Doc.UserID = Convert.ToInt32(Session["UserId"]);
             
                txtDocumentDescrition.Text = "";
                txtDocumentTypeName.Text = "";
                
                MlDynamicControl mlDynamicControl = new MlDynamicControl();
                mlDynamicControl.labelName = Convert.ToString(txtAddLabelName.Text);
                
                string newLabelName = mlDynamicControl.labelName;
                newLabelName = newLabelName.Replace(" ", String.Empty);                
                var varcountforRadioButton = new BLConsumeApi().CountForRadioButtonDynamicControl(Doc.DocumentTypeID);
                foreach (var items in varcountforRadioButton) { countforRadioButton = items.Column1; }

                mlDynamicControl.ControlType = Convert.ToString(ddlAddControlType.SelectedItem);
                string newControlType = mlDynamicControl.ControlType.Trim();
                if (newControlType.Equals("TextBox"))
                {
                    string txtBox = "txt";
                    string finalControlName = txtBox + newLabelName;
                    mlDynamicControl.ControlName = Convert.ToString(finalControlName);
                }
                if (newControlType.Equals("CheckBox"))
                {
                    string chkBox = "chk";
                    string finalControlName = chkBox + newLabelName;
                    mlDynamicControl.ControlName = Convert.ToString(finalControlName);
                }
                if (newControlType.Equals("Calender"))
                {
                    string calenders = "cl";
                    string finalControlName = calenders + newLabelName;
                    mlDynamicControl.ControlName = Convert.ToString(finalControlName);
                }
                if (newControlType.Equals("RadioButton"))
                {

                    string table = "rb_";
                    string finalControlName = countforRadioButton + 1 + table + txtAddChoiceOne.Text + "_" + txtAddChoiceTwo.Text; ;
                    mlDynamicControl.ControlName = Convert.ToString(finalControlName);
                }
                PresentationLayer.Model.DynamicControl control = new Model.DynamicControl();

                control.ControlName = mlDynamicControl.ControlName;
                control.ControlType = mlDynamicControl.ControlType;
                control.labelName = mlDynamicControl.labelName;
                control.OrderBy = countforsequence - 3 + 1;
                control.IsActive = true;
                
                control.DropDownValue = "Incomplete,Missing,Illegible";
                int id = Convert.ToInt32(ddlAddControlType.SelectedValue);
                if (id == 1)
                {
                    control.DropDownValue = txtDropDownValue.Text;
                }
                
                BLConsumeApi.spinsertNewDocumentType(Doc.DocumentTypeName, Doc.DocumentDescription, DummyCompanyID, control.labelName, control.ControlName, control.ControlType, control.DropDownValue, 1, true, Doc.UserID);
                BlDocType setData = new BlDocType();
                
                setData.setDynamicControl("Start Page", "txtStartPage", "TextBox", "Incomplete,Missing,Illegible");
                setData.setDynamicControl("End Page", "txtEndPage", "TextBox", "Incomplete,Missing,Illegible");
                setData.setDynamicControl("Location", "txtLocationName", "DropDown", "Incomplete,Missing,Illegible");
               
                List<MIDocumentType> objDocs = BLConsumeApi.getTopDocumentTypeByCustomerID();
                mlDynamicControl.DocumentTypeID = objDocs.FirstOrDefault().DocumentTypeID;
                Response.Redirect("TemplateDynamicControl.aspx?DocID=" + mlDynamicControl.DocumentTypeID, false);
            }
            catch (ThreadAbortException)
            {

            }
            catch { }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string stringtxtDocumentTypeID = String.Format("{0}", Request.Form["hiddenDocumentTypeID"]);
                MIDoctumentType updateData = new MIDoctumentType();
                updateData.DocumentTypeID = Convert.ToInt32(stringtxtDocumentTypeID);
                updateData.DocumentTypeName = txtEditDocumentTypeName.Text;
                updateData.DocumentDescription = txtEditDocumentDescription.Text;
                updateData.UserID = Convert.ToInt32(Session["UserId"]);
                updateData.CustomerID = DummyCompanyID;
                BlDocType update = new BlDocType();
                update.updateDocumentType(updateData);
                bindRepeater(DummyCompanyID);
            }
            catch { }
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
            bindRepeater(DummyCompanyID);           
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {

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
        protected void ddlAddControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(ddlAddControlType.SelectedValue);
                if (id == 1)
                {
                    lblDropDownValue.Visible = true;
                    txtDropDownValue.Visible = true;
                    lblAddChoiceTwo.Visible = false;
                    lbltxtAddChoiceOne.Visible = false;
                    txtAddChoiceOne.Visible = false;
                    txtAddChoiceTwo.Visible = false;
                    RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                    RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                    choiceOne.Enabled = false;
                    choiceTwo.Enabled = false;
                }
                else if (id == 4)
                {
                    lblAddChoiceTwo.Visible = true;
                    lbltxtAddChoiceOne.Visible = true;
                    txtAddChoiceOne.Visible = true;
                    txtAddChoiceTwo.Visible = true;
                    txtDropDownValue.Visible = false;
                    lblDropDownValue.Visible = false;
                    RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                    RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                    choiceOne.Enabled = true;
                    choiceTwo.Enabled = true;

                }
                else if (id == 3)
                {
                    lblAddChoiceTwo.Visible = false;
                    lbltxtAddChoiceOne.Visible = false;
                    txtAddChoiceOne.Visible = false;
                    txtAddChoiceTwo.Visible = false;
                    lblDropDownValue.Visible = false;
                    txtDropDownValue.Visible = false;
                    RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                    RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                    choiceOne.Enabled = false;
                    choiceTwo.Enabled = false;

                }
                else if (id == 0)
                {
                    lblAddChoiceTwo.Visible = false;
                    lbltxtAddChoiceOne.Visible = false;
                    txtAddChoiceOne.Visible = false;
                    txtAddChoiceTwo.Visible = false;
                    lblDropDownValue.Visible = false;
                    txtDropDownValue.Visible = false;
                    RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                    RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                    choiceOne.Enabled = false;
                    choiceTwo.Enabled = false;

                }
            }
            catch { }
        }
    }
}