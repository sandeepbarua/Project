using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using System.Web.UI.HtmlControls;

namespace PresentationLayer.Admin
{
    public partial class Locations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessLocations") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "active");
            if (!IsPostBack)
            {

                BlLocation.ddlCustomerName(ddlCompanyName);
                BlLocation.ddlCustomerName(ddlEditCompany);
                bindGrid();
            }


        }
        private void bindGrid()
        {
            ReptUse.DataSource = new BlLocation().getLocationDetails();
            ReptUse.DataBind();
        }


        protected void BtnCreateuser_Click(object sender, EventArgs e)
        {
            BlLocation Location = new BlLocation();
            MlLocation Location1 = new MlLocation();

            Location1.LocationName = txtAddLocation.Text.Trim();
            Location1.Fadv_LocationID = Convert.ToString(txtAddFadvLocationID.Text.Trim());
            Location1.CustomerId = Convert.ToInt32(ddlAddCompanyName.SelectedValue);
            Location.addLocationDetails(Location1);

            txtAddFadvLocationID.Text = "";
            txtAddLocation.Text = "";
            int id = Convert.ToInt32(ddlCompanyName.SelectedValue);
            bindRepeater(id);

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string stringtxtLocationID = String.Format("{0}", Request.Form["hiddenLocationID"]);
            MlLocation mlLocation = new MlLocation();
            mlLocation.LocationID = Convert.ToInt32(stringtxtLocationID);

            mlLocation.LocationName = txtEditLocationName.Text.Trim();
            mlLocation.Fadv_LocationID =txtEditLocationFadvID.Text.Trim();
            mlLocation.CustomerId = Convert.ToInt32(ddlEditCompany.SelectedValue);
            BlLocation blLocation = new BlLocation();
            blLocation.updateLocation(mlLocation);
            int id = Convert.ToInt32(ddlCompanyName.SelectedValue);
            bindRepeater(id);
            

        }

        protected void Image2_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {

        }

        protected void btnadd_Click1(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void OnDelete_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            MlLocation mlLocation = new MlLocation();
            mlLocation.LocationID = Convert.ToInt32(hd.Value);
            BlLocation blLocation = new BlLocation();
            blLocation.DeleteLocation(mlLocation);
            int id = Convert.ToInt32(ddlCompanyName.SelectedValue);
            bindRepeater(id);
           // btnadd.Visible = false;
        }

        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlCompanyName.SelectedValue);
            bindRepeater(id);
        
        }
        private void bindRepeater(int id=0) {
            if (id == 0)
            {

                ddlAddCompanyName.ClearSelection();
                bindGrid();
                btnadd.Visible = false;

            }
            else
            {
                ReptUse.DataSource = new BlLocation().getLocationDetailsByCustomer(id);
                ReptUse.DataBind();
                btnadd.Visible = true;
                //if (BlLocation.ddlCustomerAddName(ddlAddCompanyName, id))
                //{
                //    ddlAddCompanyName.SelectedIndex = 1;
                //}
                //else
                //{

                //}
            }
        }
    }
}