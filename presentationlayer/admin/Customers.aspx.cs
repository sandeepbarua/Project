using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using System.Data;

namespace PresentationLayer.Admin
{
   
    public partial class Customers : System.Web.UI.Page
    {
        string aa; string bb;
        protected void Page_Load(object sender, EventArgs e)
        {            
            HtmlGenericControl tabContact = Master.FindControl("AccessUserCustomer") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)
            {
                BindGrid();
            }

        }
        private void BindGrid()
        {

            ReptUse.DataSource = new BlCustomerDetails().getCustomerDetails();
            ReptUse.DataBind();
        }
       

        protected void Image2_Click(object sender, ImageClickEventArgs e)
        {
            BlCustomerDetails getData = new BlCustomerDetails();
            BlCustomerDetails datas = new BlCustomerDetails();
            string fileName = "Customers.xls";
                DataTable dt = new DataTable();
            dt.Columns.Add("Serial No", typeof(Int32));
            dt.Columns.Add("Customer ID", typeof(string));
            dt.Columns.Add("Company Name", typeof(string));

            int CountSno = 1;
            foreach (var value in datas.getCustomerDetails())
            {
                var row = dt.NewRow();
                row["Serial No"] = CountSno;
                row["Customer ID"] = Convert.ToString(value.FADV_CustomerID);
                row["Company Name"] = Convert.ToString(value.CompanyName);


                dt.Rows.Add(row);
                CountSno++;


            }
            ClsCommon.CreateExcelFile(dt, fileName);
            
        }

        protected void BtnCreateuser_Click(object sender, EventArgs e)
        {
            BlCustomerDetails CustomerDetails = new BlCustomerDetails();
            MlCustomerDetails CustomerDetails1 = new MlCustomerDetails();
            CustomerDetails1.CompanyName = txtAddCompanyName.Text;
            CustomerDetails1.FADV_CustomerID = Convert.ToInt32(txtAddFadvLocationId.Text);

            DataTable DtemailExit = new DataTable();
            DataTable DtemailExitN = new DataTable();
            DtemailExit = CustomerDetails.getFadvFaxidexit(txtAddFadvLocationId.Text);
            DtemailExitN = CustomerDetails.getCompanyexit(txtAddCompanyName.Text);
            if (DtemailExit.Rows.Count > 0) // Means Emaiul Id is already present
            {
                string message = "Entered Customer Id  already Exist!";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

            }
            else if (DtemailExitN.Rows.Count > 0)
            {
                string message = "Entered Company Name already Exist!";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            }
            else
            {
                CustomerDetails.addCustomerDetails(CustomerDetails1);
                txtAddCompanyName.Text = "";
                txtAddFadvLocationId.Text = "";
            }
            BindGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string stringComapnyID = String.Format("{0}", Request.Form["hiddenCompanyID"]);
            BlCustomerDetails obj = new BlCustomerDetails();
            MlCustomerDetails obj1 = new MlCustomerDetails();
            obj1.CompanyID = Convert.ToInt32(stringComapnyID);
            obj1.CompanyName = txtEditCompanyName.Text;
            obj1.FADV_CustomerID = Convert.ToInt32(txtEditLocationFadvID.Text);


        
                obj.updateCustomerDetails(obj1);
            
            BindGrid();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField CompanyID = (HiddenField)item.FindControl("HiddenField1");
            HiddenField CompanyName = (HiddenField)item.FindControl("HiddenField2");
            HiddenField FADV_CustomerID = (HiddenField)item.FindControl("HiddenField3");
            Response.Redirect("EditClient.aspx?CompanyID=" + CompanyID.Value+"&ClientName="+ CompanyName.Value+"&FADV_ID="+ FADV_CustomerID.Value, false);

        }
        protected void lbView_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            BlCustomerDetails obj = new BlCustomerDetails();
            MlCustomerDetails obj1 = new MlCustomerDetails();
            obj1.CompanyName = Convert.ToString(hd.Value);
            Session["CompanyId"] = obj1.CompanyName;
            Response.Redirect("DocumentType.aspx?CompId=" + obj1.CompanyName);
        }
        protected void OnDelete_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            BlCustomerDetails obj = new BlCustomerDetails();
            MlCustomerDetails obj1 = new MlCustomerDetails();
            obj1.CompanyID = Convert.ToInt32(hd.Value);
            obj.DeleteCustomerDetails(obj1);
            BindGrid();
        }

        protected void close_Click(object sender, EventArgs e)
        {
            txtAddCompanyName.Text = "";
            txtAddFadvLocationId.Text = "";
            BindGrid();

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("CopyCompany.aspx");
        }
    }
}