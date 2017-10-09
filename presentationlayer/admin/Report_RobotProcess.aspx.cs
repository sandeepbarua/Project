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
    public partial class Report_RobotProcess : System.Web.UI.Page
    {

        BlCustomerDetails cust = new BlCustomerDetails();
        DQFEntities db = new DQFEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HtmlGenericControl tabContact = Master.FindControl("AccessReport") as HtmlGenericControl;
                tabContact.Attributes.Add("class", "active");

                cls_LoginAudit logaudit = new cls_LoginAudit();


                rptUser.DataSource = logaudit.getRebotProcessingReport();
                rptUser.DataBind();
            }
        }


        //protected void btnrobo_Click(object sender, EventArgs e)
        //{
        //    LinkButton btn = (LinkButton)(sender);
        //    string processName = btn.CommandArgument;
        //    int rec = Convert.ToInt32(processName);
        //    BindgridViewProcess(rec);
        //    pnlRobot.Visible = false;
        //    pnlRobotProcess.Visible = true;
        //    // Response.Redirect("RoboAction.aspx");
        //    // do what you need here
        //}


        protected void btnGetData_Click(object sender, EventArgs e)
        {

            cls_LoginAudit logaudit = new cls_LoginAudit();
            string strdate = txtStartTime.Text.ToString();
            string enddate = txtEndTime.Text.ToString();

            rptUser.DataSource = logaudit.getRoboProcessingReportDateWise(strdate, enddate);
            rptUser.DataBind();


        }


    }
}