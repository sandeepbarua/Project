using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using MiddleLayer;
using System.Web.UI.HtmlControls;

namespace PresentationLayer.Admin
{
    public partial class UserLoginDetail : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserManageUser") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (Request.QueryString["id"] == null)
            {
                BLConsumeApi consume = new BLConsumeApi();
              
                List<MlUserLoginDetail> objLoginDetails = consume.getUserDetailszeroidwise();
                if (objLoginDetails!=null)
                {
                    if (objLoginDetails.Count > 0)
                    {
                        DetailsDiv.Visible = true;
                        ReptUse.DataSource = objLoginDetails;
                        ReptUse.DataBind();
                    }
                    else
                    {
                        DetailsDiv.Visible = false;
                    }
                }
                else
                {
                    DetailsDiv.Visible = false;
                }

            }
            else
            {
                var UserData = new BLConsumeApi().getUserDetailsUserIdWise(Convert.ToInt32(Request.QueryString["id"]));
               // var logindatapagewise = new BLConsumeApi().GetLoginDetailsPagewise(1, PageSize, 4, Convert.ToInt32(Request.QueryString["id"]));
                if (UserData!=null)
                {
                    lblname.Text = UserData[0].UserFirstName + " " + UserData[0].UserLastName;
                    lblemail.Text = UserData[0].EmailId;
                    lblrole.Text = UserData[0].RoleName;

                    if (Convert.ToInt32(UserData[0].LoginAttempt) == 0)
                    {
                        lblstatus.Text = "Active";

                    }
                    else
                    {
                        lblstatus.Text = "InActive"; 
                    }
                    lbldatecreation.Text = ClsCommon.getDateFormat(Convert.ToString(UserData[0].DateOfCreation));
                    lbldatemodification.Text = ClsCommon.getDateFormat(Convert.ToString(UserData[0].DateOfModification));
                   // lblnoofpage.Text = UserData[0].NoOfPagesCompleted;
                }
                else
                {
                   
                }
                BLConsumeApi consume = new BLConsumeApi();
                List<MlUserLoginDetail> objLoginDetails= consume.getUserDetailsidwise(Convert.ToInt32(Request.QueryString["id"]));
                if (objLoginDetails!=null)
                {
                    if (objLoginDetails.Count > 0)
                    {
                        DetailsDiv.Visible = true;
                        ReptUse.DataSource = objLoginDetails;
                        ReptUse.DataBind();
                    }
                    else
                    {
                        DetailsDiv.Visible = false;
                    }
                }
                else
                {
                    DetailsDiv.Visible = false;
                }
                List<MIDocumentType> objDocument= consume.GetDataEntry(Convert.ToInt32(Request.QueryString["id"]));
                if (objDocument!=null)
                {
                    if (objDocument.Count > 0)
                    {
                        test_wrapper.Visible = true;
                        reptdataentry.DataSource = objDocument;
                        reptdataentry.DataBind();
                    }
                    else
                    {
                        test_wrapper.Visible = false;
                    }
                }
                else
                {
                    test_wrapper.Visible = false;
                }
            }
        }

      
    }
}