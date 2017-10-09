using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Data;
using System.Web.UI.HtmlControls;

namespace PresentationLayer.Admin
{
    public partial class ExceptionData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserExceptionData") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            visiblityPage();
        }

        public void visiblityPage()
        {
            if (!IsPostBack)
            {
                int role = Convert.ToInt32(Session["RoleId"]);
                //role = 3;
                bindgrid();
            }
        }

        public void bindgrid()
        {

            //cls_LoginAudit data = new cls_LoginAudit();
            rptProducts.DataSource = new BLConsumeQCApi().getExceptionDataDetails();
            rptProducts.DataBind();

        }

        protected void rptProducts_OnItemDatabound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    //Label userid = (Label)e.Item.FindControl("lblOrderID");
                    Label lblProductID = (Label)e.Item.FindControl("lblProductID");
                    HiddenField doctypeid = (HiddenField)e.Item.FindControl("hdndocid");
                    HiddenField userid = (HiddenField)e.Item.FindControl("hdnuserid");
                    string DocID = doctypeid.Value;
                    string UserId = userid.Value;
                    DataList dlProductSales = (DataList)e.Item.FindControl("dlProductSales");
                    ImageButton lbtnView = (ImageButton)e.Item.FindControl("lbtnView");
                    // Label lblMsg = (Label)e.Item.FindControl("lblMsg");

                    if (lblProductID.Text.Trim() != null)
                    {
                        int faxid = Convert.ToInt32(lblProductID.Text.ToString());
                        cls_LoginAudit data = new cls_LoginAudit();
                        dlProductSales.DataSource = data.getExceptionDataDetails2(faxid);
                        dlProductSales.DataBind();

                        // lbtnView.Attributes.Add("OnClick", "window.top.location.href='ExceptionFaxIDData.aspx?FAXID=" + lblProductID.Text.ToString() + " & DocTID="+DocID+" & UserTID="+UserId+"';return false;");

                    }
                }

            }
            catch (Exception oEx)
            {
                // Response.Write(oEx.Message);
            }
        }


        protected void dlProductSales_OnItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item)
            {
                Label lblOrderID = (Label)e.Item.FindControl("lblOrderID");
                LinkButton lbtnOrderView = (LinkButton)e.Item.FindControl("lbtnOrderView");

                if (lblOrderID.Text.Trim() != null)
                {
                    lbtnOrderView.Attributes.Add("OnClick", "window.location.href='FaxID_Data.aspx?OrderID=" + lblOrderID.Text.Trim() + "';return false;");
                }
            }
        }



        protected void lbtnView_Click(object sender, ImageClickEventArgs e)
        {
            // RepeaterItem item = (sender as ImageButton).Parent as RepeaterItem;
            ImageButton btn = (ImageButton)sender;
            Label Faxid = (Label)btn.FindControl("lblProductID");
            HiddenField Userid = (HiddenField)btn.FindControl("hdnuserid");
            string UId = Userid.Value;
            // Session["UId"] = UId;
            //Session["FAXID"] = Faxid.Text;
            string[] userIds = Userid.Value.Split(',');
            string url = "ExceptionQueue.aspx?FaxId=" + Faxid.Text + "&UserId=" + userIds[0];
            Response.Redirect(url);

        }

        //protected void (object sender, ImageClickEventArgs e)
        //{

        //    RepeaterItem item = (sender as ImageButton).Parent as RepeaterItem;

        //                //string DId = Docid.Value;
        //    //string UId = Userid.Value;
        //    Session["FAXID"] = Faxid.Text;
        //    //Session["DId"] = DId;
        //    //Session["UId"] = UId;
        //    Response.Redirect("FaxID_Data.aspx");
        //}

    }
}