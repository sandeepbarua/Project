using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PresentationLayer
{
    public partial class Roboactivityshow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindRepeater();
            }
        }
        private void BindRepeater()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select [RoboActivitiesID],[StartTime],[EndTime],[Activity],DATEDIFF(day,[StartTime],[EndTime]) AS DiffDate  from [dbo].[RoboActivity]", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptCustomers.DataSource = dt;
                        rptCustomers.DataBind();
                    }
                }
            }
        }

        protected void rptCustomers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}