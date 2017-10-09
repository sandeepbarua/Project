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
using System.Web.UI.HtmlControls;

namespace PresentationLayer.Admin
{
    public partial class EditDocumentControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserCustomer") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)
            {
                if (Request.QueryString["docName"] != null)
                {
                    ddlDocumentType.Text = Request.QueryString["docName"];
                }
                if (Request.QueryString["Label"] != null)
                {
                    txtEditLabelName.Text = Request.QueryString["Label"];
                }
                if (Request.QueryString["drop"] != null)
                {
                    txtEditDropDown.Text = Request.QueryString["drop"];
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int txtDynamicControlID;
            if (int.TryParse(Request.QueryString["DynamicId"], out txtDynamicControlID))
            {
                MlDynamicControl Doc = new MlDynamicControl();
                Doc.DynamicControlID = txtDynamicControlID;
                Doc.DropDownValues = Convert.ToString(txtEditDropDown.Text.Trim());
                String labelName = Convert.ToString(txtEditLabelName.Text.Trim());
                Doc.labelName = labelName.Replace("\"", "").Replace("'", " ");
                BlDynamicControl setData = new BlDynamicControl();
                setData.updateDynamicControl(Doc);
                Response.Redirect("DataPoint.aspx?DocId=" + Request.QueryString["Id"]+ "&CustID=" + Request.QueryString["CustID"], false);
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DataPoint.aspx?DocId=" + Request.QueryString["Id"] + "&CustID=" + Request.QueryString["CustID"], false);
        }
    }

}