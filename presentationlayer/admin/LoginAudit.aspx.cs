using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using PresentationLayer.Model;
using PresentationLayer.App_Code;
using System.Data;

namespace PresentationLayer.Admin
{
    public partial class LoginAudit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HtmlGenericControl tabContact = Master.FindControl("AccessReport") as HtmlGenericControl;
                tabContact.Attributes.Add("class", "active");

                cls_LoginAudit logaudit = new cls_LoginAudit();


                rptUser.DataSource = logaudit.getLoginAuditReport(Convert.ToInt32(Session["UserId"]));
                rptUser.DataBind();
            }
        }



        protected void btnGetData_Click(object sender, EventArgs e)
        {
            cls_LoginAudit logaudit = new cls_LoginAudit();
            string strdate = txtStartTime.Text.ToString();
            string enddate = txtEndTime.Text.ToString();

            rptUser.DataSource = logaudit.getLoginAuditReportDateWise(Convert.ToInt32(Session["UserId"]), strdate, enddate);
            rptUser.DataBind();
        }
    }
}