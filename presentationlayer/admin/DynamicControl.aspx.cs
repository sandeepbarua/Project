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
    public partial class DynamicControl : System.Web.UI.Page
    {
        DQFEntities db = new DQFEntities();
        BlDocType blDocType = new BlDocType();
        int DynamicControlID; int orderby;
        int countforRadioButton;
        int countforsequence;
        int CustomerId;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessDynamicControl") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "active");
            if (!IsPostBack)
            {
                // ClsCommon.ddlGetDocType(ddlAddDocumentType);
                if (int.TryParse(Request.QueryString["CustomerId"], out CustomerId))
                {
                    blDocType.ddlBindDocumentTypeById(ddlEditDocumentType, CustomerId);
                }



                BindCustomer();

            }

        }

        private void BindCustomer()
        {
            ClsCommon.ddlCustomerID(ddlCustomer);
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var customerId = Convert.ToInt32(ddlCustomer.SelectedValue);
            BlDynamicControl.ddldocumenttypeByID(ddlDocumentType,customerId);
            ddlDocumentType.Enabled = true;
            //  dvDocumentType.Visible = true;
            btnadd.Visible = false;
        }

        protected void ddlDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDocumentType.Text = ddlDocumentType.SelectedItem.Text;
            btnadd.Visible = true;
            btnorderno.Visible = true;
            BindgridView();
        }

        private void BindgridView()
        {
            BlDynamicControl dyn = new BlDynamicControl();
            ReptUse.DataSource = dyn.getDynamicControls();
            var documentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
            //var dynamicControlList = db.DynamicControls.Where(x => x.DocumentTypeID == documentTypeId && x.IsActive == true).Select(x => new CustomDynamicControl
            //{ ControlName = x.ControlName, ControlType = x.ControlType, DocumentTypeID = x.DocumentTypeID, DynamicControlID = x.DynamicControlID, labelName = x.labelName}).ToList();

            //var dynamicControlList = db.DynamicControls.Where(x => x.DocumentTypeID == documentTypeId && x.IsActive == true).OrderBy(x => x.OrderBy).Select(x => new CustomDynamicControl
            //{ ControlName = x.ControlName, ControlType = x.ControlType, DocumentTypeID = x.DocumentTypeID, DynamicControlID = x.DynamicControlID, labelName = x.labelName, DropDown = x.DropDownValue, Order_No = (int?)x.OrderBy.Value ?? 0 }).ToList();

            var dynamicControlList = new BLConsumeApi().dynamicControlList(documentTypeId);

            foreach (var dynamic in dynamicControlList)
            {
                if (dynamic.ControlType == "TextBox")
                {
                    dynamic.ControlTypeID = 1;
                }
                else if (dynamic.ControlType == "CheckBox")
                {
                    dynamic.ControlTypeID = 2;
                }
                else if (dynamic.ControlName == "txtLocationName")
                {
                    dynamic.ControlTypeID = 3;
                }
                else if (dynamic.ControlName == "clCalender")
                {
                    dynamic.ControlTypeID = 4;
                }
                else if (dynamic.ControlName == "tbTable")
                {
                    dynamic.ControlTypeID = 5;
                }
            }
            

            ReptUse.DataSource = dynamicControlList;
            ReptUse.DataBind();

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {

        }

        protected void Image2_Click(object sender, ImageClickEventArgs e)
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
            BlDynamicControl setData = new BlDynamicControl();
            MlDynamicControl Doc = new MlDynamicControl();
            Doc.DynamicControlID = Convert.ToInt32(hd.Value);
            setData.DeleteUserDetails(Doc);
            BindgridView();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string stringtxtDynamicControlID = String.Format("{0}", Request.Form["hiddenDynamicControlID"]);

            //var item = (RepeaterItem)NamingContainer;
            //HiddenField hd = (HiddenField)FindControl("HiddenField1");
            //tring stringtxtDynamicControlID = hd.Value;

            MlDynamicControl Doc = new MlDynamicControl();
            Doc.DynamicControlID = Convert.ToInt32(stringtxtDynamicControlID);
            //Doc.ControlType = Convert.ToString(ddlEditControlType.SelectedItem);
            //Doc.DocumentTypeID = Convert.ToInt32(ddlEditDocumentType.SelectedValue);
            Doc.DropDownValues = Convert.ToString(txtEditDropDown.Text);
            String labelName = Convert.ToString(txtEditLabelName.Text);
            //Doc.labelName = labelName.Replace("[']", " ");
            Doc.labelName = labelName.Replace("\"", "").Replace("'", " ");

            BlDynamicControl setData = new BlDynamicControl();
            setData.updateDynamicControl(Doc);
            BindgridView();
            //string newControlType = Doc.ControlType.Trim();
            //if (newControlType.Equals("TextBox"))
            //{
            //    string txtBox = "txt";
            //    string finalControlName = txtBox + newLabelName;
            //    Doc.ControlName = Convert.ToString(finalControlName);
            //}
            //if (newControlType.Equals("CheckBox"))
            //{
            //    string chkBox = "chk";
            //    string finalControlName = chkBox + newLabelName;
            //    Doc.ControlName = Convert.ToString(finalControlName);
            //}
            //if (newControlType.Equals("Calender"))
            //{
            //    string calenders = "cl";
            //    string finalControlName = calenders + newLabelName;
            //    Doc.ControlName = Convert.ToString(finalControlName);
            //}
            //if (newControlType.Equals("Table"))
            //{
            //    string table = "tb";
            //    string finalControlName = table + newLabelName;
            //    Doc.ControlName = Convert.ToString(finalControlName);
            //}


        }

        protected void BtnCreateuser_Click(object sender, EventArgs e)
        {
            MlDynamicControl Doc = new MlDynamicControl();
            Doc.labelName = Convert.ToString(txtAddLabelName.Text);
            Doc.DocumentTypeID = Convert.ToInt32(ddlDocumentType.SelectedValue);
            string newLabelName = Doc.labelName;
            newLabelName = newLabelName.Replace(" ", String.Empty);

            //var customerList = db.CustomerDetails.Where(x => x.IsActive == true).ToList();
            //int countforRadioButton = db.DynamicControls.Count(a => a.ControlName.Contains("rb") && a.DocumentTypeID==(int)Doc.DocumentTypeID);

            var varcountforRadioButton = new BLConsumeApi().CountForRadioButtonDynamicControl(Doc.DocumentTypeID);
            foreach (var items in varcountforRadioButton) { countforRadioButton = items.Column1; }
            //var item = { "Start Page", "End Page", "Location" };
            List<string> validValues = new List<string>() { "Start Page", "End Page", "Location"};

            //var query = from a in db.DynamicControls
            //            where validValues.Contains(a.ControlName)
            //            select a;

            // int countforsequence = db.DynamicControls.Count(a => !validValues.Contains(a.ControlName)  && a.DocumentTypeID == (int)Doc.DocumentTypeID);
            //int countforsequence = db.DynamicControls.Count(a => a.DocumentTypeID == (int)Doc.DocumentTypeID && a.IsActive==true);
            var varcountforsequence = new BLConsumeApi().sspcountforsequenceDynamicControl(Doc.DocumentTypeID);
            foreach (var items in varcountforsequence) { countforsequence = items.Column1; }

            Doc.ControlType = Convert.ToString(ddlAddControlType.SelectedItem);
            string newControlType = Doc.ControlType.Trim();
            if (newControlType.Equals("TextBox"))
            {
                string txtBox = "txt";
                string finalControlName = txtBox + newLabelName;
                Doc.ControlName = Convert.ToString(finalControlName);
            }
            if (newControlType.Equals("CheckBox"))
            {
                string chkBox = "chk";
                string finalControlName = chkBox + newLabelName;
                Doc.ControlName = Convert.ToString(finalControlName);
            }
            if (newControlType.Equals("Calender"))
            {
                string calenders = "cl";
                string finalControlName = calenders + newLabelName;
                Doc.ControlName = Convert.ToString(finalControlName);
            }
            if (newControlType.Equals("RadioButton"))
            {
                string table = "rb_";
                string finalControlName =countforRadioButton+1 + table + txtAddChoiceOne.Text + "_" + txtAddChoiceTwo.Text; ;
                Doc.ControlName = Convert.ToString(finalControlName);
            }
            //BlDynamicControl setData = new BlDynamicControl();
            //setData.setDynamicControl(Doc);

            MlDynamicControl control = new MlDynamicControl();

            control.ControlName = Doc.ControlName;
            control.ControlType = Doc.ControlType;
            control.labelName = Doc.labelName;
            control.OrderBy = countforsequence -3 + 1 ;
            control.IsActive = true;
            control.DocumentTypeID = Convert.ToInt32(ddlDocumentType.SelectedValue);
            control.DropDownValue = "Incomplete,Missing,Illegible";
            int id = Convert.ToInt32(ddlAddControlType.SelectedValue);
            if (id == 1)
            {
                control.DropDownValue = txtDropDownValue.Text;
            }
            // db.DynamicControls.Add(control);
            //db.SaveChanges();
            BLConsumeApi insertOperation = new BLConsumeApi();
            insertOperation.insertIntoDynamicControl(control);
            txtAddLabelName.Text = "";
            txtDropDownValue.Text = "";
            lblDropDownValue.Visible = false;
            txtDropDownValue.Visible = false;
            txtAddChoiceOne.Text = "";
            txtAddChoiceTwo.Text = "";
            ddlAddControlType.ClearSelection();
            RequiredFieldValidator DropDownValue = (RequiredFieldValidator)UpdatePanel6.FindControl("RequiredFieldValidator10");
            DropDownValue.Enabled = false;
            lblAddChoiceTwo.Visible = false;
            lbltxtAddChoiceOne.Visible = false;
            txtAddChoiceOne.Visible = false;
            txtAddChoiceTwo.Visible = false;
            RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
            RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
            choiceOne.Enabled = false;
            choiceTwo.Enabled = false;

            BindgridView();


        }

        protected void ddlEditControlType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlAddControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlAddControlType.SelectedValue);
            if (id == 1)
            {
                lblDropDownValue.Visible = true;
                txtDropDownValue.Visible = true;
                lblAddChoiceTwo.Visible = false;
                lbltxtAddChoiceOne.Visible = false;
                txtAddChoiceOne.Visible = false;
                txtAddChoiceTwo.Visible = false;
                RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                choiceOne.Enabled = false;
                choiceTwo.Enabled = false;
            }
            else if (id == 4)
            {
                lblAddChoiceTwo.Visible = true;
                lbltxtAddChoiceOne.Visible = true;
                txtAddChoiceOne.Visible = true;
                txtAddChoiceTwo.Visible = true;
                txtDropDownValue.Visible = false;
                lblDropDownValue.Visible = false;
                RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                choiceOne.Enabled = true;
                choiceTwo.Enabled = true;

            }
            else if (id == 3)
            {
                lblAddChoiceTwo.Visible = false;
                lbltxtAddChoiceOne.Visible = false;
                txtAddChoiceOne.Visible = false;
                txtAddChoiceTwo.Visible = false;
                lblDropDownValue.Visible = false;
                txtDropDownValue.Visible = false;
                RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                choiceOne.Enabled = false;
                choiceTwo.Enabled = false;

            }
            else if (id == 0) {
                lblAddChoiceTwo.Visible = false;
                lbltxtAddChoiceOne.Visible = false;
                txtAddChoiceOne.Visible = false;
                txtAddChoiceTwo.Visible = false;
                lblDropDownValue.Visible = false;
                txtDropDownValue.Visible = false;
                RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                choiceOne.Enabled = false;
                choiceTwo.Enabled = false;

            }
        }

        protected void ReptUse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField HfText = (HiddenField)e.Item.FindControl("HfLabelName");
            ImageButton ib = (ImageButton)e.Item.FindControl("OnDelete");


            if (HfText.Value == "Start Page")
            {
                ib.Visible = false;
            }
            else if (HfText.Value == "End Page")
            {
                ib.Visible = false;
            }
            else if (HfText.Value == "Location")
            {
                ib.Visible = false;
            }
            else
            {
                ib.Visible = true;
            }
        }

        public void OrderBySet()
        {
            try
            {
                DataTable dtlog = new DataTable();
                dtlog.Columns.Add("DynamicControlID");
                dtlog.Columns.Add("OrderBy");

                foreach (RepeaterItem item in ReptUse.Items)
                {
                    //Repeater innerRepeater = (Repeater)item.FindControl("innerRepeater");


                    // foreach (RepeaterItem item1 in innerRepeater.Items)
                    // {

                    HiddenField DynamicControlID = ((HiddenField)item.FindControl("hndorderno"));
                    TextBox orderby = ((TextBox)item.FindControl("txtOrderNo"));

                    DataRow rw10 = dtlog.NewRow();
                    rw10["DynamicControlID"] = DynamicControlID.Value;
                    rw10["orderby"] = orderby.Text;

                    dtlog.Rows.Add(rw10);
                    //}
                }

                Session["OrderBy"] = dtlog;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public bool checkSequecing()
        {
            //errorid = 0;
            OrderBySet();
            
            if (Session["OrderBy"] != null)

            {
                BlDynamicControl orderbyset = new BlDynamicControl();



                System.Data.DataTable MyItem = (System.Data.DataTable)Session["OrderBy"];
                System.Data.DataTable CompareItem = (System.Data.DataTable)Session["OrderBy"];

                DataView item = MyItem.DefaultView;
                

                foreach (DataRow row1 in MyItem.Rows)
                {
                    int counterValue = 0;
                    foreach (DataRow row2 in CompareItem.Rows)
                    {

                        string rowValue = row1["OrderBy"].ToString();
                        string rowValue2 = row2["OrderBy"].ToString();

                        if (rowValue.Equals("0"))
                        {
                            break;
                        }
                        if (rowValue.Equals(rowValue2))
                        {
                            counterValue += 1;
                            if (counterValue > 1)
                            {

                                return false;                            }
                        }
                        else
                        {
                            
                        }
                    }
                }

            }

            return true;
        }

        protected void btnorderno_Click(object sender, EventArgs e)
        {
            OrderBySet();

            int str = 0;
            int seq = 0;

            if (!checkSequecing())
            {
                string message = "Entered should not be Duplicate";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                BindgridView();

            }
            else {

                if (Session["OrderBy"] != null)

                {
                    BlDynamicControl orderbyset = new BlDynamicControl();


                    System.Data.DataTable MyItem = (System.Data.DataTable)Session["OrderBy"];
                    DataView item = MyItem.DefaultView;
                    int Dir = 0;
                    foreach (DataRowView drv in item)
                    {

                        if (drv.Row["DynamicControlID"].ToString() != "")
                        {
                            DynamicControlID = Convert.ToInt32(drv.Row["DynamicControlID"].ToString());
                            if (drv.Row["DynamicControlID"].ToString() != "" || drv.Row["OrderBy"].ToString() != "")
                                orderby = Convert.ToInt32(drv.Row["OrderBy"].ToString());

                        }


                        str = orderbyset.UpdateOrderno(DynamicControlID, orderby);
                        ++Dir;

                    }
                }
                BindgridView();
            }
        }
        protected void close_Click(object sender, EventArgs e)
        {
            lblAddChoiceTwo.Visible = false;
            lbltxtAddChoiceOne.Visible = false;
            txtAddChoiceOne.Visible = false;
            txtAddChoiceTwo.Visible = false;
            lblDropDownValue.Visible = false;
            txtDropDownValue.Visible = false;
            RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
            RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
            choiceOne.Enabled = false;
            choiceTwo.Enabled = false;
            txtAddLabelName.Text = "";
            txtDropDownValue.Text = "";
            txtAddChoiceOne.Text = "";
            txtAddChoiceTwo.Text = "";
            ddlAddControlType.ClearSelection();

            BindgridView();

        }
    }
}