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
using System.Web.UI.WebControls.Adapters;

namespace PresentationLayer
{
    public partial class UserLogin : System.Web.UI.Page
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
        #endregion
        private const string ERROR_CLASS = "validation-error";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            GetIPAddress();

            Session["authorization"] = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
        }
        public string generateToken(string grant_type, string username, string password)
        {
            var client = new RestClient("http://localhost:49427/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=" + grant_type + "&username=" + username + "&password=" + password + "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
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
            int LoginDetailsId =Convert.ToInt32(loginDetails.LoginDetailsId);
            Session["LoginDetailsId"] = LoginDetailsId;
        }
      

        private void FormAuthentication(int roleresult,int userIdresult)
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
            else
            {
                return;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LoginDetail loginDetailss = new LoginDetail();
                string pass = Utilities.Encrypt(txtPassword.Text);
                //UserDetail userDetails = db.UserDetails.Where(x => x.EmailId == txtAgentID.Text && x.Password == pass).SingleOrDefault();
                var mlUserDetail = new BLConsumeApi().SelectUserDetailLogin(txtAgentID.Text,pass);
               
                foreach (var item in mlUserDetail) {
                    isLock = (bool)item.LocktheAccount;
                    newUserDetailsID = item.UserDetailsID;
                    LoginAttempt = item.LoginAttempt;
                    newIsActive = (bool)item.IsActive;
                    roleid = item.RoleId;
                }
                if (mlUserDetail == null)
                {
                    lblErrorMessage.Text = "Invalid UserId or Password";
                    lblErrorMessage.Visible = true;

                    mlUserDetail = new BLConsumeApi().SelectUserDetailLogin(txtAgentID.Text, pass);
                    EventType = "Login Failed";

                    
                        AddToLoginDetails(newUserDetailsID);
                      
                    
                    
                    if (mlUserDetail != null)
                    {
                        if (isLock == true)
                        {
                            lblErrorMessage.Text = "Your account is locked.";
                            lblErrorMessage.Visible = true;
                            EventType = "Login Failed";
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
                                EventType = "Login Failed";
                                AddToLoginDetails(newUserDetailsID);
                            }
                            //db.SaveChanges();
                        }

                    }
                }
                else
                {
                    if (isLock == true)
                    {
                        lblErrorMessage.Text = "Your account is locked.";
                        lblErrorMessage.Visible = true;
                        EventType = "Login Failed";
                        AddToLoginDetails(newUserDetailsID);

                        return;
                    }
                    else
                    {
                        if (newIsActive == true)
                        {
                            EventType = "Login Successfull";
                            AddToLoginDetails(newUserDetailsID);
                            BLConsumeApi bl = new BLConsumeApi();
                            bl.updateLoginattempt(newUserDetailsID);
                            FormAuthentication(roleid,newUserDetailsID);
                        }
                        else
                        {
                            lblErrorMessage.Text = "Your account is deleted";
                            lblErrorMessage.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}