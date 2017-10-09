using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PresentationLayer.Model;
using System.Web.Security;
using FADVCustomLibrary;
using System.Data;

namespace PresentationLayer.Admin
{
    public partial class Admin2 : System.Web.UI.MasterPage
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
            if (Session["RoleName"].ToString() == "Template Manager")
            {
                MANAGEUser.Style.Add("display", "none");
                Template.Style.Add("display", "display");
                LOCATION.Style.Add("display", "none");
                DOCUMENT.Style.Add("display", "none");
                // DataEntry.Style.Add("display", "none");
                // Audit.Style.Add("display", "none");
                EXCEPTION.Style.Add("display", "none");
                REPORTS.Style.Add("display", "none");
                clone.Style.Add("display", "none");


            }
            if (Session["RoleName"].ToString() == "Audit Manager")
            {
                MANAGEUser.Style.Add("display", "none");
                Template.Style.Add("display", "none");
                LOCATION.Style.Add("display", "display");
                DOCUMENT.Style.Add("display", "none");
                // DataEntry.Style.Add("display", "none");
                // Audit.Style.Add("display", "none");
                EXCEPTION.Style.Add("display", "none");
                REPORTS.Style.Add("display", "none");
                clone.Style.Add("display", "none");

            }
            if (Session["RoleName"].ToString() == "Client Manager")
            {
                MANAGEUser.Style.Add("display", "none");
                Template.Style.Add("display", "none");
                LOCATION.Style.Add("display", "none");
                DOCUMENT.Style.Add("display", "display");
                // DataEntry.Style.Add("display", "none");
                // Audit.Style.Add("display", "none");
                EXCEPTION.Style.Add("display", "none");
                REPORTS.Style.Add("display", "none");
                clone.Style.Add("display", "none");

            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                //int LoginDetailsId = Convert.ToInt32(Session["LoginDetailsId"]);
                //LoginDetail loginDetail = db.LoginDetails.Where(x => x.LoginDetailsId == LoginDetailsId).Single();
                //loginDetail.DateOfLogOut = DateTime.Now;
                //db.SaveChanges();
                if (Convert.ToString(Session["LoginDetailsId"]) != null)
                {
                    DQFController ctrl = new DQFController();
                    DQFModelRequest request = new DQFModelRequest();

                    List<OperationParameter> oprList = new List<OperationParameter>();
                    oprList.Add(new OperationParameter("@LoginDetailsId", Convert.ToString(Session["LoginDetailsId"])));

                    request.OperationName = "upsUpdateLogout_LoginDetails";
                    request.OperationType = "update";

                    request.ParameterList = new List<OperationParameter>();
                    request.ParameterList = oprList;
                    request.Token = Convert.ToString(Session["authorization"]).Replace("bearer ", "");

                    var DT = ctrl.CallRestApi(request);

                    if(DT!=null)
                    {
                        if(DT.Rows.Count>0)
                        {
                            if(DT.Columns.Count==3)
                            {
                                if(Convert.ToString(DT.Rows[0][0])=="500" && Convert.ToString(DT.Rows[0][0]) == "Internal Server Error")
                                {
                                    //Error Logging
                                    
                                }
                            }
                        }
                    }


                }


                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Redirect("~/default.aspx");
            }
            catch (Exception ex)
            {
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Redirect("~/default.aspx", false);
                logger.Error(ex.Message);
            }
        }
    }
}