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

namespace PresentationLayer.Template
{
    public partial class DataPoint : System.Web.UI.Page
    {
        
       
        int DynamicControlID; int orderby;
        BlDocType blDocType = new BlDocType();
        int countforRadioButton;
        int countforsequence;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserCustomer") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CustID"]))
                {

                    CustomerID.Value = Request.QueryString["CustID"];
                }
                if (!string.IsNullOrEmpty(Request.QueryString["DocId"]))
                {
                    //blDocType.ddlBindDocumentTypeById(ddlEditDocumentType, DummyCompanyID);
                    BindDocumentType();
                    ddlDocumentType.SelectedIndex = ddlDocumentType.Items.IndexOf(ddlDocumentType.Items.FindByValue(Request.QueryString["DocID"]));
                    BindgridView();
                    if (ddlDocumentType.SelectedIndex >= 0)
                    {
                        btnorderno.Visible = true;
                    }
                    if (Convert.ToString(Request.QueryString["RedirectAction"]) == "AddDataPoint")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModalWindow();", true);
                    }

                }
                else
                {
                    BindDocumentType();
                }
               
            }
        }


        private void BindDocumentType()
        {
            ddlDocumentType.Items.Clear();
            DataTable dtddlCustomerName;
            ListItem li = new ListItem();
            li.Text = "-----Select------";
            li.Value = "-1";
            ddlDocumentType.Items.Add(li);
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(new BLConsumeApi().getAllDocumentTypeByCustomerId(Convert.ToInt32(CustomerID.Value)));
            dtddlCustomerName = dt;
            for (int i = 0; i < dtddlCustomerName.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtddlCustomerName.Rows[i]["DocumentTypeID"].ToString();
                li.Text = dtddlCustomerName.Rows[i]["DocumentTypeName"].ToString();
                ddlDocumentType.Items.Add(li);
            }
            ddlDocumentType.SelectedIndex = 0;
            ddlDocumentType.Enabled = true;           
        }

        protected void ddlDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDocumentType.SelectedIndex >= 0)
            {
                //lblDocumentType.Text = ddlDocumentType.SelectedItem.Text;
                //btnadd.Visible = true;
                btnorderno.Visible = true;
                BindgridView();
            }
        }

        private void BindgridView()
        {
            BlDynamicControl dyn = new BlDynamicControl();
            //ReptUse.DataSource = dyn.getDynamicControls();
            var documentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
            var dynamicControlList = new BLConsumeApi().dynamicControlList(documentTypeId);
            if(dynamicControlList!=null)
            {
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
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            HiddenField hd1 = (HiddenField)item.FindControl("HfLabelName");
            HiddenField hd2 = (HiddenField)item.FindControl("hfDropDownValue");
            Response.Redirect("EditDocumentControl.aspx?DynamicId=" + hd.Value + "&Label=" + hd1.Value+ "&drop="+ hd2.Value+"&docName="+ ddlDocumentType.SelectedItem.ToString()+"&Id="+ ddlDocumentType.SelectedValue+ "&CustID=" + Request.QueryString["CustID"], false);
        }
        
        protected void lbDelete_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            BlDynamicControl setData = new BlDynamicControl();
            MlDynamicControl Doc = new MlDynamicControl();
            Doc.DynamicControlID = Convert.ToInt32(hd.Value);
            setData.DeleteUserDetails(Doc);
            BindgridView();
        }
        

        protected void btnAddControl_Click(object sender, EventArgs e)
        {
            if (ddlDocumentType.SelectedIndex > 0)
            {
                Response.Redirect("AddControl.aspx?DocumentId=" + ddlDocumentType.SelectedValue+"&DocName=" + ddlDocumentType.SelectedItem.ToString(), false);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string stringtxtDynamicControlID = String.Format("{0}", Request.Form["hiddenDynamicControlID"]);
            //var btn = (Button)sender;
            //var item = (ContentPlaceHolder)btn.NamingContainer;         
            //HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            //string stringtxtDynamicControlID = hd.Value;

            MlDynamicControl Doc = new MlDynamicControl();
            Doc.DynamicControlID = Convert.ToInt32(stringtxtDynamicControlID);
            //Doc.ControlType = Convert.ToString(ddlEditControlType.SelectedItem);
            //Doc.DocumentTypeID = Convert.ToInt32(ddlEditDocumentType.SelectedValue);
           // Doc.DropDownValues = Convert.ToString(txtEditDropDown.Text);
            //String labelName = Convert.ToString(txtEditLabelName.Text);
            //Doc.labelName = labelName.Replace("[']", " ");
            //Doc.labelName = labelName.Replace("\"", "").Replace("'", " ");

            BlDynamicControl setData = new BlDynamicControl();
            setData.updateDynamicControl(Doc);
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
                DropDownDiv.Visible = true;
                RadioDiv1.Visible = false;
                RadioDiv2.Visible = false;

                //lblDropDownValue.Visible = true;
                //txtDropDownValue.Visible = true;
                //lblSelectValue.Visible = true;
                //lblAddChoiceTwo.Visible = false;
                //lbltxtAddChoiceOne.Visible = false;
                //txtAddChoiceOne.Visible = false;
                //txtAddChoiceTwo.Visible = false;
                //RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                //RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                //choiceOne.Enabled = false;
                //choiceTwo.Enabled = false;
            }
            else if (id == 2) {
                DropDownDiv.Visible = false;
                RadioDiv1.Visible = false;
                RadioDiv2.Visible = false;
                //lblDropDownValue.Visible = true;
                //txtDropDownValue.Visible = true;
                //lblAddChoiceTwo.Visible = false;
                //lbltxtAddChoiceOne.Visible = false;
                //txtAddChoiceOne.Visible = false;
                //txtAddChoiceTwo.Visible = false;
                //RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                //RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                //choiceOne.Enabled = false;
                //choiceTwo.Enabled = false;

            }
            else if (id == 4)
            {
                DropDownDiv.Visible = false;
                RadioDiv1.Visible = true; 
                RadioDiv2.Visible = true;
                //lblAddChoiceTwo.Visible = true;
                //lbltxtAddChoiceOne.Visible = true;
                //txtAddChoiceOne.Visible = true;
                //txtAddChoiceTwo.Visible = true;
                //txtDropDownValue.Visible = true;
                //lblDropDownValue.Visible = true;
                //RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                //RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                //choiceOne.Enabled = true;
                //choiceTwo.Enabled = true;

            }
            else if (id == 3)
            {
                //lblAddChoiceTwo.Visible = false;
                //lbltxtAddChoiceOne.Visible = false;
                //txtAddChoiceOne.Visible = false;
                //txtAddChoiceTwo.Visible = false;
                //lblDropDownValue.Visible = false;
                //txtDropDownValue.Visible = false;
                //RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                //RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                //choiceOne.Enabled = false;
                //choiceTwo.Enabled = false;

            }
            else if (id == 0)
            {
                DropDownDiv.Visible = false;
                RadioDiv1.Visible = false;
                RadioDiv2.Visible = false;
                //lblAddChoiceTwo.Visible = false;
                //lbltxtAddChoiceOne.Visible = false;
                //txtAddChoiceOne.Visible = false;
                //txtAddChoiceTwo.Visible = false;
                //lblDropDownValue.Visible = false;
                //txtDropDownValue.Visible = false;
                //RequiredFieldValidator choiceOne = (RequiredFieldValidator)UpdatePanel3.FindControl("RequiredFieldValidator8");
                //RequiredFieldValidator choiceTwo = (RequiredFieldValidator)UpdatePanel5.FindControl("RequiredFieldValidator9");
                //choiceOne.Enabled = false;
                //choiceTwo.Enabled = false;

            }
        }

        protected void ReptUse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField HfText = (HiddenField)e.Item.FindControl("HfLabelName");
            LinkButton ib = (LinkButton)e.Item.FindControl("lbDelete");
            LinkButton ledit = (LinkButton)e.Item.FindControl("btnEdit");
            TextBox txOrder = (TextBox)e.Item.FindControl("txtOrderNo");



            if (ib != null)
            {
                if (HfText.Value == "Start Page")
                {
                    ib.Visible = false;
                    ledit.Visible = false;
                    txOrder.Enabled = false;
                }
                else if (HfText.Value == "End Page")
                {
                    ib.Visible = false;
                    ledit.Visible = false;
                    txOrder.Enabled = false;
                }
                else if (HfText.Value == "Location")
                {
                    ib.Visible = false;
                    ledit.Visible = false;
                    txOrder.Enabled = false;
                }
                else
                {
                    ib.Visible = true;
                }
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
                    HiddenField DynamicControlID = ((HiddenField)item.FindControl("HiddenField1"));
                    TextBox orderby = ((TextBox)item.FindControl("txtOrderNo"));

                    DataRow rw10 = dtlog.NewRow();
                    rw10["DynamicControlID"] = DynamicControlID.Value;
                    rw10["orderby"] = orderby.Text;
                    dtlog.Rows.Add(rw10);                   
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

                                return false;
                            }
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
            else
            {

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
            RequiredFieldValidator choiceOne = (RequiredFieldValidator)FindControl("RequiredFieldValidator8");
            RequiredFieldValidator choiceTwo = (RequiredFieldValidator)FindControl("RequiredFieldValidator9");
            choiceOne.Enabled = false;
            choiceTwo.Enabled = false;
            txtAddLabelName.Text = "";
            txtDropDownValue.Text = "";
            txtAddChoiceOne.Text = "";
            txtAddChoiceTwo.Text = "";
            ddlAddControlType.ClearSelection();
            BindgridView();
        }

        protected void BtnAddTemplate_Click(object sender, EventArgs e)
        {

            MlDynamicControl Doc = new MlDynamicControl();
            //Doc.labelName = Convert.ToString(txtAddLabelName.Text);
            Doc.DocumentTypeID = Convert.ToInt32(Request.QueryString["DocumentId"]);
            string newLabelName = Doc.labelName;
            newLabelName = newLabelName.Replace(" ", String.Empty);
            var varcountforRadioButton = new BLConsumeApi().CountForRadioButtonDynamicControl(Doc.DocumentTypeID);

            foreach (var items in varcountforRadioButton) { countforRadioButton = items.Column1; }
            List<string> validValues = new List<string>() { "Start Page", "End Page", "Location" };
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
                //string finalControlName = countforRadioButton + 1 + table + txtAddChoiceOne.Text + "_" + txtAddChoiceTwo.Text; ;
                //Doc.ControlName = Convert.ToString(finalControlName);
            }
            MlDynamicControl control = new MlDynamicControl();

            control.ControlName = Doc.ControlName;
            control.ControlType = Doc.ControlType;
            control.labelName = Doc.labelName;
            control.OrderBy = countforsequence - 3 + 1;
            control.IsActive = true;
            control.DocumentTypeID = Convert.ToInt32(Request.QueryString["DocumentId"]);
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
            //Response.Redirect(prevPage);
        }

        protected void btnCacel_Click(object sender, EventArgs e)
        {
            
        }

        protected void BtnCreateuser_Click(object sender, EventArgs e)
        {
          
        }

        protected void btmCreate_Click(object sender, EventArgs e)
        {
            
            MlDynamicControl Doc = new MlDynamicControl();
            Doc.labelName = Convert.ToString(txtAddLabelName.Text);
            Doc.DocumentTypeID = Convert.ToInt32(ddlDocumentType.SelectedValue);
            string newLabelName = Doc.labelName;
            newLabelName = newLabelName.Replace(" ", String.Empty);
            var varcountforRadioButton = new BLConsumeApi().CountForRadioButtonDynamicControl(Doc.DocumentTypeID);
            foreach (var items in varcountforRadioButton) { countforRadioButton = items.Column1; }
            List<string> validValues = new List<string>() { "Start Page", "End Page", "Location" };
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
                string finalControlName = countforRadioButton + 1 + table + txtAddChoiceOne.Text + "_" + txtAddChoiceTwo.Text; ;
                Doc.ControlName = Convert.ToString(finalControlName);
            }
            MlDynamicControl control = new MlDynamicControl();

            control.ControlName = Doc.ControlName;
            control.ControlType = Doc.ControlType;
            control.labelName = Doc.labelName;
            control.OrderBy = countforsequence - 3 + 1;
            control.IsActive = true;
            control.DocumentTypeID = Convert.ToInt32(ddlDocumentType.SelectedValue);
            control.DropDownValue = "Incomplete,Missing,Illegible";
            int id = Convert.ToInt32(ddlAddControlType.SelectedValue);
            if (id == 1)
            {
                control.DropDownValue = txtDropDownValue.Text;
            }

            BLConsumeApi insertOperation = new BLConsumeApi();
            insertOperation.insertIntoDynamicControl(control);

            txtAddLabelName.Text = "";
            txtDropDownValue.Text = "";
            lblDropDownValue.Visible = true;
            txtDropDownValue.Visible = false;
            txtAddChoiceOne.Text = "";
            txtAddChoiceTwo.Text = "";
            ddlAddControlType.ClearSelection();
            //RequiredFieldValidator DropDownValue = (RequiredFieldValidator)FindControl("RequiredFieldValidator10");
            //DropDownValue.Enabled = false;
            lblAddChoiceTwo.Visible = false;
            lbltxtAddChoiceOne.Visible = false;
            txtAddChoiceOne.Visible = false;
            txtAddChoiceTwo.Visible = false;
            lblSelectValue.Visible = false;
            //RequiredFieldValidator choiceOne = (RequiredFieldValidator)FindControl("RequiredFieldValidator8");
            //RequiredFieldValidator choiceTwo = (RequiredFieldValidator)FindControl("RequiredFieldValidator9");
            //choiceOne.Enabled = false;
            //choiceTwo.Enabled = false;

            BindgridView();
        }
    }
}