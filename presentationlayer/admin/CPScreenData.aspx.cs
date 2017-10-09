using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;

namespace PresentationLayer.Admin
{
    public partial class CPScreenData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReptUse.DataSource = new BlCPScreenData().getUserDetails();
            ReptUse.DataBind();
        }
    }
}