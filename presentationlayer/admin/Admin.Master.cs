using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PresentationLayer.Model;
using System.Web.Security;

namespace PresentationLayer.Admin
{
    public partial class Admin1 : System.Web.UI.MasterPage
    {
        #region Grobal Variables
        DQFEntities db = new DQFEntities();
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(UserLogin));

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ExpiresAbsolute = DateTime.Now;
            Response.Expires = -1441;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Pragma", "no-store");
            Response.AddHeader("cache-control", "no-cache");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoServerCaching();
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                int LoginDetailsId = Convert.ToInt32(Session["LoginDetailsId"]);
                LoginDetail loginDetail = db.LoginDetails.Where(x => x.LoginDetailsId == LoginDetailsId).Single();
                loginDetail.DateOfLogOut = DateTime.Now;
                db.SaveChanges();
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Redirect("~/UserLogin.aspx");
            }
            catch (Exception ex)
            {
                Response.Redirect("~/UserLogin.aspx", false);
                logger.Error(ex.Message);
            }
        }
    }
}