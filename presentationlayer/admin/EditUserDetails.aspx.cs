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
    public partial class EditUserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserFName"] != null)
                {
                    txtEditFirstName.Text = Session["UserFName"].ToString();
                }
                if (Session["UserLName"] != null)
                {
                    txtEditLastName.Text = Session["UserLName"].ToString();
                }
                if (Session["RoleName"] != null)
                {
                    ddlEditRole.Items.FindByText(Session["RoleName"].ToString()).Selected = true;
                }
                if (Session["Emailid"] != null)
                {
                    txtEditEmail.Text = Session["Emailid"].ToString();
                }

            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            MlLogin userEntryUpdate = new MlLogin();
            userEntryUpdate.UserFirstName = txtEditFirstName.Text;
            userEntryUpdate.UserLastName = txtEditLastName.Text;
            userEntryUpdate.UserDetailsID = Convert.ToInt32(Session["ID"].ToString());
            //userEntryUpdate.FADVUserID = Convert.ToInt32(txtEditFadvID.Text);
            userEntryUpdate.RoleName = ddlEditRole.SelectedItem.ToString();
            userEntryUpdate.EmailId = txtEditEmail.Text;
            BLConsumeApi checkForSameEmail = new BLConsumeApi();
            BlUserDetail updateData = new BlUserDetail();
            //if (userEntryUpdate.EmailId == checkForSameEmail.getemailidexit(userEntryUpdate.UserDetailsID))
            //{
            //    updateData.updateUserDetails(userEntryUpdate);
            //   // lblMessageError.Visible = false;
            //   // bindGrid();
            //}
            //else if (checkForSameEmail.getExistingemailid(userEntryUpdate.UserDetailsID, userEntryUpdate.EmailId) >= 1)
            //{
            //   // lblMessageError.Visible = true;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabelError();", true);
            //    txtEditEmail.Text = "";
            //    txtEditEmail.Text = checkForSameEmail.getemailidexit(userEntryUpdate.UserDetailsID);
            //}

            // lblMessageError.Visible = false;
            updateData.updateUserDetails(userEntryUpdate);
            // bindGrid();

            Response.Redirect("UserDetail.aspx");


        }

        protected void btncancal_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserDetail.aspx");
        }
    }
}