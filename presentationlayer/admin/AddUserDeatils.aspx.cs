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
    public partial class AddUserDeatils : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserManageUser") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)
            {

            }
        }

        protected void BtnCreateuser_Click(object sender, EventArgs e)
        {
            MlLogin Detail = new MlLogin();
            Detail.UserFirstName = Convert.ToString(txtFirstName.Text);
            Detail.UserLastName = Convert.ToString(txtLastName.Text);
            Detail.RoleId = Convert.ToInt32(dllSelectRole.SelectedValue);
            Detail.RoleName = Convert.ToString(dllSelectRole.SelectedItem.ToString());

            //Detail.FADVUserID = Convert.ToInt32(txtFadvId.Text);
            Random rand = new Random();
            Detail.FADVUserID = rand.Next(100000, 1000000);
            Detail.EmailId = Convert.ToString(txtEmailId.Text);
            //Detail.Password = Convert.ToString(TxtPassword.Text);
            BlUserDetail setData = new BlUserDetail();
            DataTable DtemailExit = new DataTable();
            //DtemailExit = setData.getemailidexit(txtEmailId.Text);

            if (DtemailExit.Rows.Count > 0) // Means Emaiul Id is already present
            {
                string message = "Enter Email Id is already Exist!";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
            else if (DtemailExit.Rows.Count == 0)
            {
                setData.setUserDetails(Detail);
                Response.Redirect("UserDetail.aspx");
            }
            txtFirstName.Text = "";
            txtLastName.Text = "";
            dllSelectRole.ClearSelection();
            // txtEditEmail.Text = "";

            // bindGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void Btnbtncanal_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserDetail.aspx");
        }
    }
}