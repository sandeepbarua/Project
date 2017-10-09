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
    public partial class UserDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // HtmlGenericControl tabContact = Master.FindControl("AccessUser") as HtmlGenericControl;
            // tabContact.Attributes.Add("class", "active");
            HtmlGenericControl tabContact = Master.FindControl("AccessUserManageUser") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)
            {
                bindGrid();
            }

        }
        private void bindGrid()
        {
            ReptUse.DataSource = new BlUserDetail().getUserDetails();
            ReptUse.DataBind();
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            HiddenField hd1 = (HiddenField)item.FindControl("HiddenField2");
            HiddenField hd2 = (HiddenField)item.FindControl("HiddenField3");
            HiddenField hd3 = (HiddenField)item.FindControl("HiddenField4");
            HiddenField hd4 = (HiddenField)item.FindControl("HiddenField5");
            LinkButton btzn = (LinkButton)item.FindControl("btnEdit");
            MlLogin editdetails = new MlLogin();
            editdetails.UserDetailsID = Convert.ToInt32(hd.Value);
            editdetails.UserFirstName = Convert.ToString(hd1.Value);
            editdetails.UserLastName = Convert.ToString(hd2.Value);
            editdetails.EmailId = Convert.ToString(hd3.Value);
            editdetails.RoleName = Convert.ToString(hd4.Value);
            Session["ID"] = editdetails.UserDetailsID;
            Session["UserFName"] = editdetails.UserFirstName;
            Session["UserLName"] = editdetails.UserLastName;
            Session["Emailid"] = editdetails.EmailId;
            Session["RoleName"] = editdetails.RoleName;
            Response.Redirect("EditUserDetails.aspx");
        }
        protected void OnDelete_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            LinkButton btzn = (LinkButton)item.FindControl("OnDelete");
            MlLogin userEntryToDeelete = new MlLogin();
            userEntryToDeelete.UserDetailsID = Convert.ToInt16(hd.Value);
            BlUserDetail deteData = new BlUserDetail();
            deteData.DeleteUserDetails(userEntryToDeelete);


            bindGrid();

        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddUserDeatils.aspx");
        }
        protected void onLocked_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;

            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");

            Label lblText = (Label)item.FindControl("la");
            ImageButton btns = (ImageButton)item.FindControl("onLocked");
            MlLogin userDetail = new MlLogin();
            userDetail.LocktheAccount = Convert.ToString(lblText.Text);
            userDetail.UserDetailsID = Convert.ToInt32(hd.Value);
            BlUserDetail LockUnlockAccount = new BlUserDetail();
            LockUnlockAccount.updateLockUserDetails(userDetail);
            LockUnlockAccount.updateLockUserDetailsToZero(userDetail);
            bindGrid();
        }
        protected void ReptUse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trID");
            HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("serial");
            HtmlTableCell td1 = (HtmlTableCell)e.Item.FindControl("UserFirstName");
            HtmlTableCell td2 = (HtmlTableCell)e.Item.FindControl("UserLastName");
            HtmlTableCell td3 = (HtmlTableCell)e.Item.FindControl("FADVUserID");
            HtmlTableCell td4 = (HtmlTableCell)e.Item.FindControl("RoleName");
            HtmlTableCell td5 = (HtmlTableCell)e.Item.FindControl("LoginAttempt");
            HtmlTableCell td6 = (HtmlTableCell)e.Item.FindControl("email");
            HtmlTableCell td7 = (HtmlTableCell)e.Item.FindControl("actions");

            HiddenField lbl = (HiddenField)e.Item.FindControl("hndUserID") as HiddenField;
            //double unitprice = double.Parse(strUnitPrice);
            string status;
            status = (lbl.Value);
            if (status == "InActive")
            {
                tr.Attributes.Add("style", "background-color:#8B0000;color:#CD5C5C;");

                td.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                td1.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                td2.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                //  td3.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                td4.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                //td5.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                td6.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                td7.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                // td5.Attributes.Add("style", "background-color:#8B0000;color:#FF0000;");
            }
            else
            {

                //td5.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
            }



            string lockFileName = "lock.png";
            string unlockFileName = "unlock.png";


            Label lblText = (Label)e.Item.FindControl("la");

            if (lblText.Text == "InActive")
            {
                ImageButton img = e.Item.FindControl("onLocked") as ImageButton;
                img.ImageUrl = "Images/" + lockFileName;
                //  tr.Attributes.Add("style", "background-color:#8B0000;color:#CD5C5C;");

                //  td.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                //  td1.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                //  td2.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                ////  td3.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                //  td4.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                // // td5.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                //  td6.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");
                //  td7.Attributes.Add("style", "background-color:#8B0000;color:#FFFFFF;");

            }
            else
            {
                ImageButton img = e.Item.FindControl("onLocked") as ImageButton;
                img.ImageUrl = "Images/" + unlockFileName;
            }



            // ....



        }


        protected void showmodal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

    }
}