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
    public partial class showdata : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand(@"     
select[CPScreenDataID],[FaxID],[ReceiveDate],[CustomerID],[CompanyName],[SourceFile],[DateofCreation],[Comment],[CMS_CPScreenDocumentdTypeId],[Labelling],DATEDIFF(day,[ProcessingStarted],[ProcessingEnd]) AS DiffDate  from dbo.CMS_CPScreenData INNER JOIN dbo.CustomerDetails on dbo.CMS_CPScreenData.CustomerID = dbo.CustomerDetails.CompanyID", con))
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