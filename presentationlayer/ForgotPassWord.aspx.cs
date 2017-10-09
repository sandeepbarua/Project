using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using RestSharp;
using System.Net;
using System.Configuration;

namespace PresentationLayer
{
    
    public partial class ForgotPassWord : System.Web.UI.Page
    {
        string UserId = ConfigurationManager.AppSettings["UserId"].ToString();
        string Password = ConfigurationManager.AppSettings["Password"].ToString();
        string grantType = ConfigurationManager.AppSettings["GrantType"].ToString();

        string ClientExecuteQueryUrl = ConfigurationManager.AppSettings["ClientExecuteQueryUrl"].ToString();

        string ClientTokenUrl = ConfigurationManager.AppSettings["ClienTokenUrl"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["authorization"] = "bearer " + generateToken(grantType, UserId, Password);
            if (!IsPostBack)
            {  }
        }
        public string generateToken(string grant_type, string username, string password)
        {

            var client = new RestClient(ClientTokenUrl);
            var request = new RestRequest(Method.POST);
            //ServicePointManager.Expect100Continue = false;
            //ServicePointManager.MaxServicePointIdleTime = 0;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=" + grant_type + "&username=" + username + "&password=" + password + "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                var clients = new RestClient(ClientTokenUrl);
                var requests = new RestRequest(Method.POST);
                //ServicePointManager.Expect100Continue = false;
                //ServicePointManager.MaxServicePointIdleTime = 0;
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("content-type", "application/x-www-form-urlencoded");
                requests.AddParameter("application/x-www-form-urlencoded", "grant_type=" + grant_type + "&username=" + username + "&password=" + password + "", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                int ii = responses.Content.IndexOf(":");
                int lastindexs = responses.Content.IndexOf(",", ii);
                string de = responses.Content.Substring(ii + 2, (lastindexs - 3) - ii);
                return de;
            }
            int i = response.Content.IndexOf(":");
            int lastindex = response.Content.IndexOf(",", i);
            string d = response.Content.Substring(i + 2, (lastindex - 3) - i);
            return d;
        }
        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            CkeckEmailPassword();
        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
        public void CkeckEmailPassword()
        {
            try
            {
                DataSet ds = new DataSet();
                MlUserDetails clsuser = new MlUserDetails();
                BlUserDetail user = new BlUserDetail();
                var Emailid = new BLConsumeApi().GetEmailIdandPassword(txtAgentID.Text);

               // ds = user.GetEmailIdandPassword(txtAgentID.Text);
                if (Emailid!=null)
                {
                   // lblMessage.Text = "<font color='green'>We have found your password and have sent you an email with your account information.</font>";

                    string fname = Emailid[0].UserFirstName.ToString();// ds.Tables[0].Rows[0]["UserFirstName"].ToString();
                    string lname = Emailid[0].UserLastName.ToString(); //ds.Tables[0].Rows[0]["UserLastName"].ToString();
                    string Email = Emailid[0].EmailId.ToString(); //ds.Tables[0].Rows[0]["EmailId"].ToString();

                    var value = new BLConsumeApi().GetAuthenticationEmail("ForgotPassword");
                    if (value[0].HeaderName.ToString() == "ForgotPassword")
                    {
                        string time = DateTime.Now.ToShortDateString();
                       var emailvalue= new BLConsumeApi().GetEmailvalues(value[0].HeaderName);
                        BlUserDetail.SendEmail(fname,lname,Email,emailvalue[0].BccEmailAddress,emailvalue[0].EmailBody,emailvalue[0].EmailSubject,emailvalue[0].EmailNotificationPath,emailvalue[0].SMTPHost,emailvalue[0].SmtpPort,emailvalue[0].SMTPCredentialFromAddress,emailvalue[0].SMTPCedentialFromPassword,time);
                       // businessLogic.Email.SendEmailforForGotPassword(user, fname, lname, username, password, txtEmail.Text, clsmessage[2].Subject, clsmessage[2].BccEmailAddresses, clsmessage[2].Body);
                    }


                    txtAgentID.Text = "";
                    string message = "Email with link to reset your password has been sent to your email address.Please click on the link in the email to reset your password.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    lblMessage.Text = "";

                }
                else
                {
                    //lblMessage.Text = "Sorry, we were not able to find any record in our database. Please check email address and try again.";
                    string message = "Sorry, we were not able to find any record in our database. Please check email address and try again.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    
                }
                txtAgentID.Text = "";
            }
            catch (Exception ex)
            {
                //logWrite.LogError(ex);
                //clsManageMessageTemplete meassage = new clsManageMessageTemplete();
                //List<clsMessageTemplate> clsmessage = new List<clsMessageTemplate>();
                //clsmessage = meassage.GetMessageList();
                //logWrite.SendEmailforLog(ex, clsmessage[3].Subject, clsmessage[3].BccEmailAddresses, clsmessage[3].Body);
            }
            finally
            {
            }
        }
        
    }
}