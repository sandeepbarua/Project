using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using PresentationLayer.Model;
using System.Web.Security;
using PresentationLayer.App_Code;
using System.Net;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;

namespace PresentationLayer
{
    public partial class _default : System.Web.UI.Page
    {
        #region Grobal Variables
        DQFEntities db = new DQFEntities();
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(UserLogin));
        string str;
        string EventType;
        bool isLock;
        int newUserDetailsID;
        int LoginAttempt;
        bool newIsActive;
        int roleid;
        string RoleName;
        string UserId = ConfigurationManager.AppSettings["UserId"].ToString();
        string Password = ConfigurationManager.AppSettings["Password"].ToString();
        string grantType = ConfigurationManager.AppSettings["GrantType"].ToString();

        string ClientExecuteQueryUrl = ConfigurationManager.AppSettings["ClientExecuteQueryUrl"].ToString();

        string ClientTokenUrl = ConfigurationManager.AppSettings["ClienTokenUrl"].ToString();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                {
                    txtAgentID.Text = Request.Cookies["UserName"].Value;
                    txtPassword.Attributes["value"] = Request.Cookies["Password"].Value;
                    chkRemember.Checked = true;
                }
                else
                {
                    chkRemember.Checked = false;
                }
            }
            Session.Clear();
            GetIPAddress();

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
        protected void GetIPAddress()
        {
            str = string.Empty;


            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    string ipAddress = addr.ToString();
                    str += ipAddress;
                }
            }

        }

        private void NewAddToLiginDetail(int email)
        {


        }

        private void AddToLoginDetails(int UserId)
        {
            MlUserLoginDetail loginDetails = new MlUserLoginDetail();
            loginDetails.UserId = UserId;
            loginDetails.DateOfLogin = Convert.ToString(DateTime.Now);
            loginDetails.IPAddress = str;
            loginDetails.Attempts = 1;
            loginDetails.Event = EventType;
            new BLConsumeApi().insertLoginDetail(loginDetails);
            //db.LoginDetails.Add(loginDetails);
            //db.SaveChanges();
            int LoginDetailsId =Convert.ToInt32( loginDetails.LoginDetailsId);
            Session["LoginDetailsId"] = LoginDetailsId;
        }

        private void FormAuthentication(int roleresult, int userIdresult)
        {
            string strRole = string.Empty;
            Session["RoleId"] = Convert.ToString(roleresult);
            if (roleresult == 1)
            {
                strRole = "Admin";
            }
            else if (roleresult == 2)
            {
                strRole = "DataEntryOperator";
            }
            else if (roleresult == 3)
            {
                strRole = "QCManager";
            }
            else if (RoleName == "Template Manager")
            {
                strRole = "Template Manager";
            }
            else if (RoleName == "Audit Manager")
            {
                strRole = "Audit Manager";
            }
            else if (RoleName == "Client Manager")
            {
                strRole = "Client Manager";
            }


            else
            {
                return;
            }

            FormsAuthentication.SetAuthCookie(txtAgentID.Text, false);
            FormsAuthenticationTicket ticket1 =
         new FormsAuthenticationTicket(
              1,                                   // version
              txtAgentID.Text.Trim(),   // get username  from the form
              DateTime.Now,                        // issue time is now
              DateTime.Now.AddMinutes(120),         // expires in 10 minutes
              false,      // cookie is not persistent
             strRole                             // role assignment is stored
                                                 // in userData
              );
            System.Web.HttpCookie cookie1 = new System.Web.HttpCookie(
              FormsAuthentication.FormsCookieName,
              FormsAuthentication.Encrypt(ticket1));
            Response.Cookies.Add(cookie1);
            Session["UserId"] = Convert.ToString(userIdresult);
            if (roleresult == 2)
            {
                Response.Redirect("Users/DataEntryForm.aspx", false);
            }
            else if (roleresult == 3)
            {
                Response.Redirect("Admin/ExceptionData.aspx", false);

            }
            else if (roleresult == 1)
            {
                Response.Redirect("Admin/UserDetail.aspx", false);
            }
            else if (RoleName == "Template Manager")
            {
                Response.Redirect("Admin/Template/TemplateDocumnetType.aspx", false);
            }

            else
            {
                return;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                if (chkRemember.Checked == false)
                {
                    if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                    }

                }


                cookesRememberMe();
                Session["authorization"] = "bearer " + generateToken(grantType, UserId, Password);
                LoginDetail loginDetailss = new LoginDetail();
                string pass = Utilities.Encrypt(txtPassword.Text);
                //UserDetail userDetails = db.UserDetails.Where(x => x.EmailId == txtAgentID.Text && x.Password == pass).SingleOrDefault();
                var mlUserDetail = new BLConsumeApi().SelectUserDetailLogin(txtAgentID.Text, pass);
                if (mlUserDetail != null)
                {
                    foreach (var item in mlUserDetail)
                    {
                        isLock = (bool)item.LocktheAccount;
                        newUserDetailsID = item.UserDetailsID;
                        LoginAttempt = item.LoginAttempt;
                        newIsActive = (bool)item.IsActive;
                        roleid = item.RoleId;
                        RoleName = item.RoleName;
                        Session["RoleName"] = RoleName;
                    }
                }

                if (mlUserDetail == null)
                {
                    if (Page.IsValid == true)
                    {
                        var check = new BLConsumeApi().SelectUserDetailAttemts(txtAgentID.Text);
                        if (check == null)
                        {
                            var val1 = new CustomValidator()
                            {
                                ErrorMessage = "Invalid Email Id or Password",
                                Display = ValidatorDisplay.None,
                                IsValid = false,
                                ValidationGroup = "forgotpass"
                            };
                            val1.ServerValidate += (object source, ServerValidateEventArgs args) =>
                            { args.IsValid = false; };
                            Page.Validators.Add(val1);

                        }
                        else
                        {
                            foreach (var ch in check)
                            {
                                int att = ch.LoginAttempt;
                                int newatt = att + 1;
                                if (att >= 3)
                                {
                                    var val1 = new CustomValidator()
                                    {
                                        ErrorMessage = "Your account is locked",
                                        Display = ValidatorDisplay.None,
                                        IsValid = false,
                                        ValidationGroup = "forgotpass"
                                    };
                                    val1.ServerValidate += (object source, ServerValidateEventArgs args) =>
                                    { args.IsValid = false; };
                                    Page.Validators.Add(val1);
                                    new BLConsumeApi().LocktheAccount(txtAgentID.Text);
                                }
                                else
                                {
                                    var val = new CustomValidator()
                                    {

                                        ErrorMessage = "Invalid Email Id or Password",
                                        Display = ValidatorDisplay.None,
                                        IsValid = false,
                                        ValidationGroup = "forgotpass"
                                    };
                                    val.ServerValidate += (object source, ServerValidateEventArgs args) =>
                                    { args.IsValid = false; };
                                    Page.Validators.Add(val);
                                }

                                new BLConsumeApi().UpdateLockAttempts(newatt, txtAgentID.Text);

                            }
                        }

                    }


                    AddToLoginDetails(newUserDetailsID);



                    if (mlUserDetail != null)
                    {
                        if (isLock == true)
                        {
                            if (Page.IsValid == true)
                            {
                                var val1 = new CustomValidator()
                                {
                                    ErrorMessage = "Your account is locked",
                                    Display = ValidatorDisplay.None,
                                    IsValid = false,
                                    ValidationGroup = "forgotpass"
                                };
                                val1.ServerValidate += (object source, ServerValidateEventArgs args) =>
                                { args.IsValid = false; };
                                Page.Validators.Add(val1);
                            }
                            //lblErrorMessage.Text = ".";
                            //lblErrorMessage.Visible = true;
                            // EventType = "Login Failed";
                            AddToLoginDetails(newUserDetailsID);
                            return;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(LoginAttempt)))
                                LoginAttempt = 0;
                            LoginAttempt++;


                            if (LoginAttempt >= 3)
                            {
                                isLock = true;
                                // EventType = "Login Failed";
                                AddToLoginDetails(newUserDetailsID);
                            }
                            //db.SaveChanges();
                        }

                    }
                }
                else
                {
                    if (newIsActive == false)
                    {
                        var val2 = new CustomValidator()
                        {
                            ErrorMessage = "Your account is deleted",
                            Display = ValidatorDisplay.None,
                            IsValid = false,
                            ValidationGroup = "forgotpass"
                        };
                        val2.ServerValidate += (object source, ServerValidateEventArgs args) =>
                        { args.IsValid = false; };
                        Page.Validators.Add(val2);
                    }
                    else if (isLock == true)
                    {
                        if (Page.IsValid == true)
                        {
                            var val2 = new CustomValidator()
                            {
                                ErrorMessage = "Your account is locked",
                                Display = ValidatorDisplay.None,
                                IsValid = false,
                                ValidationGroup = "forgotpass"
                            };
                            val2.ServerValidate += (object source, ServerValidateEventArgs args) =>
                            { args.IsValid = false; };
                            Page.Validators.Add(val2);
                        }
                        EventType = "Login Failed";
                        AddToLoginDetails(newUserDetailsID);

                        return;
                    }
                    else
                    {
                        if (newIsActive == true)
                        {
                            // EventType = "Login Successfull";
                            AddToLoginDetails(newUserDetailsID);

                            BLConsumeApi bl = new BLConsumeApi();
                            bl.updateLoginattempt(newUserDetailsID);
                            FormAuthentication(roleid, newUserDetailsID);

                        }
                        else
                        {
                            if (Page.IsValid == true)
                            {
                                var val2 = new CustomValidator()
                                {
                                    ErrorMessage = "Your account is deleted",
                                    Display = ValidatorDisplay.None,
                                    IsValid = false,
                                    ValidationGroup = "forgotpass"
                                };
                                val2.ServerValidate += (object source, ServerValidateEventArgs args) =>
                                { args.IsValid = false; };
                                Page.Validators.Add(val2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        private void cookesRememberMe()
        {
            try
            {
                //save user name and password in cookies
                if (chkRemember.Checked == true)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                }

                Response.Cookies["UserName"].Value = txtAgentID.Text.Trim();
                Response.Cookies["Password"].Value = txtPassword.Text.Trim();
            }
            catch (Exception ex)
            {

            }
        }

    }
}