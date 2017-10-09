using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using PresentationLayer.Model;
using PresentationLayer.App_Code;
using MiddleLayer;
using System.Web.UI.HtmlControls;

namespace PresentationLayer.Admin.Template
{
    public partial class AddControl : System.Web.UI.Page
    {
        int DummyCompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DummyCompany"]);
        //DQFEntities db = new DQFEntities();
        static string prevPage = String.Empty;
        int countforRadioButton;
        int countforsequence;
        BlDocType blDocType = new BlDocType();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserTemplateEngine") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                lblDocumentType.Text = Request.QueryString["DocName"];
            }
        }
        protected void BtnAddControl_Click(object sender, EventArgs e)
        {
            MlDynamicControl Doc = new MlDynamicControl();
            Doc.labelName = Convert.ToString(txtAddLabelName.Text);
            Doc.DocumentTypeID = Convert.ToInt32(Request.QueryString["DocumentId"]);
            string newLabelName = Doc.labelName;
            newLabelName = newLabelName.Replace(" ", String.Empty);
            var varcountforRadioButton = new BLConsumeApi().CountForRadioButtonDynamicControl(Doc.DocumentTypeID);
            foreach (var items in varcountforRadioButton) { countforRadioButton = items.Column1; }

            //int countforRadioButton = db.DynamicControls.Count(a => a.ControlName.Contains("rb") && a.DocumentTypeID == (int)Doc.DocumentTypeID);
            List<string> validValues = new List<string>() { "Start Page", "End Page", "Location" };

       
            var varcountforsequence = new BLConsumeApi().sspcountforsequenceDynamicControl(Doc.DocumentTypeID);
            foreach (var items in varcountforsequence) { countforsequence = items.Column1; }

           // int countforsequence = db.DynamicControls.Count(a => a.DocumentTypeID == (int)Doc.DocumentTypeID && a.IsActive == true);
            Doc.ControlType = Convert.ToString(ddlAddControlType.SelectedItem);
            string newControlType = Doc.ControlType.Trim();
            if (newControlType.Equals("TextBox"))
            {
                string txtBox = "txt";
                string finalControlName = txtBox + newLabelName;
                Doc.ControlName = Convert.ToString(finalControlName);
            }
            if (newControlType.Equals("CheckBox"))
            {
                string chkBox = "chk";
                string finalControlName = chkBox + newLabelName;
                Doc.ControlName = Convert.ToString(finalControlName);
            }
            if (newControlType.Equals("Calender"))
            {
                string calenders = "cl";
                string finalControlName = calenders + newLabelName;
                Doc.ControlName = Convert.ToString(finalControlName);
            }
            if (newControlType.Equals("RadioButton"))
            {

                string table = "rb_";
                string finalControlName = countforRadioButton + 1 + table + txtAddChoiceOne.Text + "_" + txtAddChoiceTwo.Text; ;
                Doc.ControlName = Convert.ToString(finalControlName);
            }
            MlDynamicControl control = new MlDynamicControl();

            control.ControlName = Doc.ControlName;
            control.ControlType = Doc.ControlType;
            control.labelName = Doc.labelName;
            control.OrderBy = countforsequence - 3 + 1;
            control.IsActive = true;
            control.DocumentTypeID = Convert.ToInt32(Request.QueryString["DocumentId"]);
            control.DropDownValue = "Incomplete,Missing,Illegible";
            int id = Convert.ToInt32(ddlAddControlType.SelectedValue);
            if (id == 1)
            {
                control.DropDownValue = txtDropDownValue.Text;
            }
            // db.DynamicControls.Add(control);
            //db.SaveChanges();
            BLConsumeApi insertOperation = new BLConsumeApi();
            insertOperation.insertIntoDynamicControl(control);

            Response.Redirect(prevPage);
        }
        protected void ddlAddControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlAddControlType.SelectedValue);
            if (id == 1)
            {
                tbl.Visible = true;
                tbl1.Visible = false;               
                txtDropDownValue.Visible = true;               
                RequiredFieldValidator1.Enabled = true;
                RequiredFieldValidator2.Enabled = false;
                RequiredFieldValidator5.Enabled = false;
            }
            else if (id == 2)
            {
                tbl.Visible = false;
                tbl1.Visible = false;                
                RequiredFieldValidator1.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                RequiredFieldValidator5.Enabled = false;
            }
            else if (id == 4)
            {
                tbl.Visible = false;
                tbl1.Visible = true;               
                RequiredFieldValidator1.Enabled = true;
                RequiredFieldValidator2.Enabled = true;
                RequiredFieldValidator5.Enabled = true;
            }
            else
            {
                tbl.Visible = false;
                tbl1.Visible = false;        
                RequiredFieldValidator1.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                RequiredFieldValidator5.Enabled = false;
            }
        }
        protected void btnCacel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TemplateDynamicControl.aspx?DocId="+ Request.QueryString["DocumentId"], false);
        }
    }
}