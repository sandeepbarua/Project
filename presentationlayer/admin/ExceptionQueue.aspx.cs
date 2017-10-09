using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PresentationLayer.Model;
using System.Web.UI.HtmlControls;
using PresentationLayer;
using System.Web.Security;
using PresentationLayer.App_Code;
using DataAccessLayer;
using System.Globalization;
using MiddleLayer;


namespace PresentationLayer.Admin
{
    public partial class ExceptionQueue : System.Web.UI.Page
    {
        #region Global Variables
        //DQFEntities db = new DQFEntities();
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(QCDataEntry));
        List<DropDownValue> dropDownValueList = new List<DropDownValue>();
        string[] dateFormats = {"dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd",
                   "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy"};
        List<string> dropDownValueTextTableList = new List<string>();
        List<UpdateControlClass> updateControlClassList = new List<UpdateControlClass>();

        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DropDownValueText"] == null)
            {
                //dropDownValueTextTableList = db.DropDownValueTexts.Select(x => x.DataText).ToList();
                List<StringDataMamber> res = new BLConsumeApi().sp_dropDownValueTextTableList();
                List<string> lstStr = new List<string>();
                for (int i = 0; i < res.Count; i++)
                {
                    lstStr.Add(res[i].DataText);
                }
                dropDownValueTextTableList = lstStr;
                Session["DropDownValueText"] = dropDownValueTextTableList;
            }
            else
            {
                dropDownValueTextTableList = Session["DropDownValueText"] as List<string>;
            }

            VerifyUser();
            if (string.IsNullOrEmpty(Request.QueryString["FaxId"]) || string.IsNullOrEmpty(Request.QueryString["UserId"]))
            {
                Response.Redirect("ExceptionData.aspx", false);
                return;
            }


            Session.Timeout = 120;
            try
            {

                PageLoadInitialisation();
                if (!IsPostBack)
                {

                    DefaultLoadForUserChange();
                    PageLoad();
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }

        private void VerifyUser()
        {
            if (Session["UserId"] == null)
                Response.Redirect("~/UserLogin.aspx", false);
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                Response.Redirect("~/UserLogin.aspx", false);
        }

        private void DefaultLoadForUserChange()
        {

            hfTaskOperationId.Value = "0";
            litFaxId1.Text = lblFaxId.Text;
            litFaxId2.Text = lblFaxId.Text;
            hfCountForFaxId.Value = "1";
            txtStartPage.Text = "1";
            //  ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);

            Session["DocumentTypeReviewList"] = null;
            Session["CurrentDocument"] = null;
            DefaultValue();

        }

        private void PageLoadInitialisation()
        {
            txtEndPage.CssClass = txtEndPage.CssClass.Replace("input-validation-error", "");
            if (!string.IsNullOrEmpty(hfTempCPScreenDataID.Value) && !string.IsNullOrEmpty(hfRepeaterBound.Value))
            {
                if (ddlDocumentType.Items.Count > 0)
                {
                    if (Convert.ToInt32(hfRepeaterBound.Value) > 0 && ddlDocumentType.SelectedIndex > 0)
                    {
                        int UserId = Convert.ToInt32(Request.QueryString["UserId"]);
                        int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);

                        foreach (RepeaterItem item in rptDynamicControl.Items)
                        {
                            string strDynamicControlValue = string.Empty;
                            string strDynamicControlID = ((HiddenField)item.FindControl("hfDynamicControlID")).Value;
                            string strControlName = ((HiddenField)item.FindControl("hfControlName")).Value;
                            string strControlType = ((HiddenField)item.FindControl("hfControlType")).Value;
                            string strDynamicControlValueId = ((HiddenField)item.FindControl("hfDyanamicControlValueID")).Value;

                            bool pageOperation = true;
                            DropDownValue ddlListValue = new DropDownValue();
                            UpdateControlClass ucc = new UpdateControlClass();
                            if (strControlName == "txtStartPage" || strControlName == "txtEndPage" || strControlName == "txtLocationName")
                            {
                                pageOperation = false;
                            }
                            if (pageOperation)
                            {
                                ddlListValue.DyanamicControlID = Convert.ToInt32(strDynamicControlID);
                                if (strControlType == "TextBox")
                                {
                                    if (((DropDownList)item.FindControl("tone")).SelectedValue != "-1")
                                    {
                                        strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        ddlListValue.Text = strDynamicControlValue;
                                        ucc.DyanamicControlID = Convert.ToInt32(strDynamicControlID);
                                        ucc.Text = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        if (!string.IsNullOrEmpty(strDynamicControlValueId))
                                        {
                                            ddlListValue.DynamicControlValueId = Convert.ToInt32(strDynamicControlValueId);
                                            ucc.DynamicControlValueId = Convert.ToInt32(strDynamicControlValueId);
                                        }
                                    }

                                }
                                else if (strControlType == "Calender")
                                {
                                    if (((DropDownList)item.FindControl("tone")).SelectedValue != "-1")
                                    {
                                        strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        ddlListValue.Text = strDynamicControlValue;
                                        ucc.DyanamicControlID = Convert.ToInt32(strDynamicControlID);
                                        ucc.Text = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        if (!string.IsNullOrEmpty(strDynamicControlValueId))
                                        {
                                            ddlListValue.DynamicControlValueId = Convert.ToInt32(strDynamicControlValueId);
                                            ucc.DynamicControlValueId = Convert.ToInt32(strDynamicControlValueId);
                                        }
                                    }

                                }
                                else if (strControlType == "CheckBox")
                                {
                                    if (((DropDownList)item.FindControl("tone")).SelectedValue != "-1")
                                    {
                                        strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        ddlListValue.Text = strDynamicControlValue;
                                        if (!string.IsNullOrEmpty(strDynamicControlValueId))
                                        {
                                            ddlListValue.DynamicControlValueId = Convert.ToInt32(strDynamicControlValueId);
                                        }
                                    }
                                }
                                else if (strControlType == "RadioButton")
                                {
                                    if (((DropDownList)item.FindControl("tone")).SelectedValue != "-1")
                                    {
                                        strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        ddlListValue.Text = strDynamicControlValue;
                                        ucc.DyanamicControlID = Convert.ToInt32(strDynamicControlID);
                                        ucc.Text = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        if (!string.IsNullOrEmpty(strDynamicControlValueId))
                                        {
                                            ddlListValue.DynamicControlValueId = Convert.ToInt32(strDynamicControlValueId);
                                        }
                                    }

                                }

                                dropDownValueList.Add(ddlListValue);
                            }
                            updateControlClassList.Add(ucc);
                        }

                        BindDynamicControlRepeaterByDocumentTypeId(Convert.ToInt32(ddlDocumentType.SelectedValue), Convert.ToInt32(hfCountForFaxId.Value));

                    }
                }
            }


        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedValue != "-1")
            {
                ddlDocumentType.Enabled = false;
                ddlLocation.Enabled = true;
            }
            else
            {
                ddlLocation.Enabled = false;
                ddlDocumentType.Enabled = false;
            }
            BindDocumentTypeDropDown(Convert.ToInt32(ddlCustomer.SelectedValue));
        }
        private void BindDocumentTypeDropDown(int CustomerId)
        {
            try
            {
                ManagerUserControl();
                BindLocationAndDocumentTypeByCustomerId(CustomerId);
                rptDynamicControl.DataSource = null;
                rptDynamicControl.DataSourceID = null;

                rptDynamicControl.DataBind();
                ddlLocation.CssClass = ddlLocation.CssClass.Replace("input-validation-error", "");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        protected void ddlDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAllDataByDocumentTypeId(Convert.ToInt32(ddlDocumentType.SelectedValue));
        }

        private void BindAllDataByDocumentTypeId(int DocumentTypeId, int CountForFaxId = 0)
        {
            try
            {
                hfDbOperation.Value = "Add";
                ddlLocation.CssClass = ddlLocation.CssClass.Replace("input-validation-error", "");
                txtStartPage.CssClass = txtStartPage.CssClass.Replace("input-validation-error", "");
                txtEndPage.CssClass = txtEndPage.CssClass.Replace("input-validation-error", "");
                //txtStartPage.Text = string.Empty;
                txtEndPage.Text = string.Empty;
                ddlLocation.SelectedIndex = 0;

                rptDynamicControl.DataSource = null;
                rptDynamicControl.DataSourceID = null;
                lblLocatoinError.Visible = false;
                rptDynamicControl.DataBind();

                ManagerUserControl();



                // dvRptDynamicTextBox.Controls.Clear();

                BindDynamicControlRepeaterByDocumentTypeId(DocumentTypeId, CountForFaxId);
                GetSubmittedTaskByOtherUser(DocumentTypeId, CountForFaxId);

                var result = Session["SubmittedTaskByOtherUser"] as List<DynamicControlResponse>;
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        MatchDataForBothUsers(result, CountForFaxId);
                        hfIsReviewed.Value = "1";
                    }
                }


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        protected void rptDynamicControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    HtmlGenericControl hc = (HtmlGenericControl)e.Item.FindControl("dvControl");
                    string strControlType = ((HiddenField)e.Item.FindControl("hfControlType")).Value;
                    string strControlName = ((HiddenField)e.Item.FindControl("hfControlName")).Value;
                    string strhfDynamicControlID = ((HiddenField)e.Item.FindControl("hfDynamicControlID")).Value;
                    int hfDynamicControlID = Convert.ToInt32(strhfDynamicControlID);
                    string dropDownText = string.Empty;
                    var ddlValueListData = dropDownValueList.Where(x => x.DyanamicControlID == hfDynamicControlID).SingleOrDefault();
                    if (strControlName == "txtLocationName" || strControlName == "txtStartPage" || strControlName == "txtEndPage" || strControlType == "Table")
                    {
                        ((Label)e.Item.FindControl("lblLabelName")).Visible = false;
                        ((DropDownList)e.Item.FindControl("tone")).Visible = false;
                    }
                    string strDynamicControlValue = ((HiddenField)e.Item.FindControl("hfDynamicControlValueText")).Value;
                    if (strControlName == "txtStartPage" || strControlName == "txtEndPage" || strControlType == "Table")
                    {
                        return;
                    }

                    string hfDropDownValue = ((HiddenField)e.Item.FindControl("hfDropDownValue")).Value;
                    var ddlList = new List<DropDownValue>();
                    if (!string.IsNullOrEmpty(hfDropDownValue))
                    {
                        ddlList = hfDropDownValue.Split(',').Select(x => new DropDownValue { Text = x }).ToList();
                    }

                    if (strControlType.Trim() == "TextBox")
                    {
                        TextBox txt = new TextBox();

                        txt.ID = strControlName;
                        txt.CssClass += "form-control ";
                        if (!string.IsNullOrEmpty(strDynamicControlValue))
                        {
                            txt.Text = strDynamicControlValue;
                            ddlValueListData = ddlList.Where(x => x.Text == strDynamicControlValue).SingleOrDefault();
                        }

                        if (ddlValueListData != null)
                        {
                            if (!string.IsNullOrEmpty(ddlValueListData.Text))
                            {
                                txt.Text = "";
                                txt.Enabled = false;
                            }
                        }
                        hc.Controls.Add(txt);

                    }

                    else if (strControlType == "CheckBox")
                    {
                        CheckBox chk = new CheckBox();
                        chk.ID = strControlName;
                        chk.Checked = false;
                        chk.CssClass += " form-control ";
                        if (!string.IsNullOrEmpty(strDynamicControlValue))
                        {
                            chk.Checked = strDynamicControlValue == "true" ? true : false;
                            ddlValueListData = ddlList.Where(x => x.Text == strDynamicControlValue).Select(x => new DropDownValue { Text = x.Text }).SingleOrDefault();
                        }


                        if (ddlValueListData != null)
                        {
                            if (!string.IsNullOrEmpty(ddlValueListData.Text))
                            {
                                chk.Checked = false;
                                chk.Enabled = false;
                            }
                        }
                        hc.Controls.Add(chk);
                    }
                    else if (strControlType == "Calender")
                    {
                        TextBox txt = new TextBox();
                        txt.ID = strControlName;
                        txt.CssClass += " form-control ";
                        if (!string.IsNullOrEmpty(strDynamicControlValue))
                        {

                            if (!dropDownValueTextTableList.Any(strDynamicControlValue.Contains))
                            {
                                DateTime temp;
                                if (DateTime.TryParse(strDynamicControlValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out temp))
                                {
                                    string converted = DateTime.ParseExact(strDynamicControlValue, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                    .ToString("yyyy-MM-dd");
                                    txt.Text = converted;
                                }
                            }
                            //    txt.Text = strDynamicControlValue;
                            ddlValueListData = ddlList.Where(x => x.Text == strDynamicControlValue).Select(x => new DropDownValue { Text = x.Text }).SingleOrDefault();
                        }
                        else
                        {
                            txt.Text = "MM/DD/YYYY";
                        }

                        txt.TextMode = TextBoxMode.Date;


                        if (ddlValueListData != null)
                        {
                            if (!string.IsNullOrEmpty(ddlValueListData.Text))
                            {
                                txt.Text = "";
                                txt.Enabled = false;
                            }
                        }
                        hc.Controls.Add(txt);
                    }
                    else if (strControlType == "RadioButton")
                    {
                        RadioButtonList rb = new RadioButtonList();
                        string strrbControlName = strControlName.Substring(strControlName.IndexOf("rb_"));
                        string strRadioValue = strrbControlName.Replace("rb_", "");
                        //string strRadioValue = strControlName.Replace("rb_", "");
                        var ddlRadioList = strRadioValue.Split('_').Select(x => new { Text = x }).ToList();
                        rb.ID = strControlName;
                        rb.DataSource = ddlRadioList;
                        rb.DataTextField = "Text";
                        rb.DataValueField = "Text";
                        rb.DataBind();
                        // rb.CssClass += " form-control ";
                        if (!string.IsNullOrEmpty(strDynamicControlValue))
                        {
                            rb.SelectedValue = strDynamicControlValue;
                            ddlValueListData = ddlList.Where(x => x.Text == strDynamicControlValue).Select(x => new DropDownValue { Text = x.Text }).SingleOrDefault();
                        }

                        if (ddlValueListData != null)
                        {
                            if (!string.IsNullOrEmpty(ddlValueListData.Text))
                            {
                                rb.SelectedValue = "-1";
                                rb.Enabled = false;
                            }
                        }

                        hc.Controls.Add(rb);
                    }


                    if (ddlList != null)
                    {
                        if (ddlList.Count > 0)
                        {
                            ((DropDownList)e.Item.FindControl("tone")).DataSource = ddlList;
                            ((DropDownList)e.Item.FindControl("tone")).DataTextField = "Text";
                            ((DropDownList)e.Item.FindControl("tone")).DataValueField = "Text";
                            ((DropDownList)e.Item.FindControl("tone")).DataBind();
                            ((DropDownList)e.Item.FindControl("tone")).Items.Insert(0, new ListItem("Select a Value", "-1"));
                            ((DropDownList)e.Item.FindControl("tone")).CssClass += " form-control ";
                        }
                        else
                        {
                            ((DropDownList)e.Item.FindControl("tone")).Visible = false;

                        }
                    }
                    else
                    {
                        ((DropDownList)e.Item.FindControl("tone")).Visible = false;

                    }
                    if (ddlValueListData != null)
                    {
                        if (!string.IsNullOrEmpty(ddlValueListData.Text))
                        {
                            ((DropDownList)e.Item.FindControl("tone")).SelectedValue = ddlValueListData.Text;
                        }
                    }

                    /*  HtmlGenericControl hcDropDown = (HtmlGenericControl)e.Item.FindControl("dvDropDown");
                      DropDownList ddlDropDown = new DropDownList();
                      string hfDropDownValue = ((HiddenField)e.Item.FindControl("hfDropDownValue")).Value;
                      if(!string.IsNullOrEmpty(hfDropDownValue))
                      {
                          var ddlList = hfDropDownValue.Split(',').Select(x => new DropDownValue { Text = x }).ToList();
                          ddlDropDown.DataSource = ddlList;
                          ddlDropDown.DataTextField = "Text";
                          ddlDropDown.DataValueField = "Text";
                          ddlDropDown.DataBind();
                          ddlDropDown.Items.Insert(0, new ListItem("Select a value", "-1"));
                          ddlDropDown.AutoPostBack = true;
                          ddlDropDown.SelectedIndexChanged += DdlDropDown_SelectedIndexChanged;
                          hcDropDown.Controls.Add(ddlDropDown);
                      }*/
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }

        private void DdlDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            //RepeaterItem item = ddl.Parent as RepeaterItem;
            var item = (RepeaterItem)ddl.NamingContainer;
            string strControlName = ((HiddenField)item.FindControl("hfControlName")).Value;
            TextBox tb = item.FindControl(strControlName) as TextBox;

            RepeaterItem gvRow = (RepeaterItem)(((Control)sender).NamingContainer);
            string strControlName1 = ((HiddenField)item.FindControl("hfControlName")).Value;
            TextBox tb1 = item.FindControl(strControlName1) as TextBox;
        }

        private void UserControlAddOperation()
        {
            // if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 51 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 60)
            var GetCertificateOfViolationDocID = new BLConsumeApi().GetCertificateOfViolationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.ConvictionDetails");
            if (GetCertificateOfViolationDocID != null)
            {
                foreach (var item in GetCertificateOfViolationDocID)
                {
                    if (Convert.ToInt32(ddlDocumentType.SelectedValue) == Convert.ToInt32(item.DocumentTypeID))
                    {
                        SaveConvictionDetailsData();
                        HtmlTable tbl = (HtmlTable)ConvictionDetails1.FindControl("tbl");
                        tbl.Controls.Clear();
                    }
                }
            }
            // if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 48 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 57)
            var GetDriversLicenseDocID = new BLConsumeApi().GetDriversLicenseDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.DriverLicenceDetail");
            if (GetDriversLicenseDocID != null)
            {
                foreach (var item in GetDriversLicenseDocID)
                {
                    if (Convert.ToInt32(ddlDocumentType.SelectedValue) == Convert.ToInt32(item.DocumentTypeID))
                    {
                        SaveDriverLicenceData();
                        HtmlTable tbl = (HtmlTable)TgDriverLicence1.FindControl("tbl");
                        tbl.Controls.Clear();
                    }
                }
            }
            // if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 49 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 58)
            var GetPreviousEmploymentDocID = new BLConsumeApi().GetPreviousEmploymentDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.PreviousEmploymentDetail");
            if (GetPreviousEmploymentDocID != null)
            {
                foreach (var item in GetPreviousEmploymentDocID)
                {
                    if (Convert.ToInt32(ddlDocumentType.SelectedValue) == Convert.ToInt32(item.DocumentTypeID))
                    {
                        SavePreviousEmployeementData();
                        HtmlTable tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
                        tbl.Controls.Clear();
                    }
                }
            }
            //if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
            //{
            //    SaveCurrentEmployerData();
            //    HtmlTable tbl = (HtmlTable)TgCurrentEmployer1.FindControl("tbl");
            //    tbl.Controls.Clear();
            //}
            // if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)

            var GetDriverApplicationDocID = new BLConsumeApi().GetDriverApplicationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.CurrentResidenceDetail");
            if (GetDriverApplicationDocID != null)
            {
                foreach (var item in GetDriverApplicationDocID)
                {
                    if (Convert.ToInt32(ddlDocumentType.SelectedValue) == Convert.ToInt32(item.DocumentTypeID))
                    {
                        SaveCurrentResidenceData();
                        HtmlTable tbl = (HtmlTable)TgCurrentResidence1.FindControl("tbl");
                        tbl.Controls.Clear();
                    }
                }
            }
            // if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)

            var GetDriverApplicationDocIDs = new BLConsumeApi().GetDriverApplicationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.PreviousResidenceDetail");
            if (GetDriverApplicationDocIDs != null)
            {
                foreach (var item in GetDriverApplicationDocIDs)
                {
                    if (Convert.ToInt32(ddlDocumentType.SelectedValue) == Convert.ToInt32(item.DocumentTypeID))
                    {
                        SavePreviousResidenceData();
                        HtmlTable tbl = (HtmlTable)TgPreviousResidence1.FindControl("tbl");
                        tbl.Controls.Clear();
                    }
                }
            }
            //if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
            //{
            //    SaveTypeOfEquipmentResidenceData();
            //    HtmlTable tbl = (HtmlTable)TgTypeOfEquipment1.FindControl("tbl");
            //    tbl.Controls.Clear();
            //}
            //if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
            var GetDriverApplicationDocI = new BLConsumeApi().GetDriverApplicationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.TrafficConvictionsDetail");
            if (GetDriverApplicationDocI != null)
            {
                foreach (var item in GetDriverApplicationDocI)
                {
                    if (Convert.ToInt32(ddlDocumentType.SelectedValue) == Convert.ToInt32(item.DocumentTypeID))
                    {
                        SaveTrafficConvictionsData();
                        HtmlTable tbl = (HtmlTable)TgTrafficConvictions1.FindControl("tbl");
                        tbl.Controls.Clear();
                    }
                }
            }
            //if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
            var GetDriverApplicationDoc = new BLConsumeApi().GetDriverApplicationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.CEDPreviousEmployerDetail");
            if (GetDriverApplicationDoc != null)
            {
                foreach (var item in GetDriverApplicationDoc)
                {
                    if (Convert.ToInt32(ddlDocumentType.SelectedValue) == Convert.ToInt32(item.DocumentTypeID))
                    {
                        SaveCEDPreviousEmployerData();
                        HtmlTable tbl = (HtmlTable)TgCEDPreviousEmployer1.FindControl("tbl");
                        tbl.Controls.Clear();
                    }
                }
            }
            // if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
            var GetDriverApplicationDo = new BLConsumeApi().GetDriverApplicationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.PreviousEmployerDetail");
            if (GetDriverApplicationDo != null)
            {
                foreach (var item in GetDriverApplicationDo)
                {
                    if (Convert.ToInt32(ddlDocumentType.SelectedValue) == Convert.ToInt32(item.DocumentTypeID))
                    {
                        SavePreviousEmployerData();
                        HtmlTable tbl = (HtmlTable)TgPreviousEmployer1.FindControl("tbl");
                        tbl.Controls.Clear();
                    }
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidationOperation())
                {
                    UserControlAddOperation();

                    UpdateOperation();

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private void ManageSession()
        {
            List<DocumentTypeReviewList> documentTypeReviewList = (List<DocumentTypeReviewList>)Session["DocumentTypeReviewList"];
            DocumentTypeReviewList doc = (DocumentTypeReviewList)Session["CurrentDocument"];
            documentTypeReviewList.Remove(doc);
            Session["DocumentTypeReviewList"] = documentTypeReviewList;
            if (documentTypeReviewList.Count == 0)
            {
                Session["DocumentTypeReviewList"] = null;
            }
        }

        private void PerformSubmitOperation()
        {
            try
            {
                int UserId = Convert.ToInt32(Request.QueryString["UserId"]);
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int FaxId = Convert.ToInt32(Request.QueryString["FaxId"]);
                //db.uspMoveDataFromExceptionToCPScreenTable(FaxId, UserId);
                new BLConsumeApi().uspMoveDataFromExceptionToCPScreenTable(FaxId, UserId);
                Response.Redirect("ExceptionData.aspx", false);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            PerformSubmitOperation();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            DefaultValue();
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            try
            {
                int LoginDetailsId = Convert.ToInt32(Session["LoginDetailsId"]);
                //LoginDetail loginDetail = db.LoginDetails.Where(x => x.LoginDetailsId == LoginDetailsId).Single();
                new BLConsumeApi().LoginDetailsLogOut(LoginDetailsId);
                //loginDetail.DateOfLogOut = DateTime.Now;
                //db.SaveChanges();
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Redirect("~/UserLogin.aspx", false);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/UserLogin.aspx", false);
                logger.Error(ex.Message);
            }
        }

        #endregion
        #region Methods

        bool IsAllDigits(string s)
        {
            return s.All(char.IsDigit);
        }


        private bool ValidationOperation()
        {
            int countError = 0;
            try
            {

                int UserId = Convert.ToInt32(Session["UserId"]);
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                DynamicControlValue dcv;
                bool isValidPage = true;
                bool pageOperation = true;
                foreach (RepeaterItem item in rptDynamicControl.Items)
                {
                    pageOperation = true;
                    string strDynamicControlValue = string.Empty;
                    string strDynamicControlID = ((HiddenField)item.FindControl("hfDynamicControlID")).Value;
                    string strControlName = ((HiddenField)item.FindControl("hfControlName")).Value;
                    string strControlType = ((HiddenField)item.FindControl("hfControlType")).Value;

                    if (strControlName == "txtStartPage" || strControlName == "txtEndPage" || strControlName == "txtLocationName")
                    {
                        pageOperation = false;
                    }
                    if (pageOperation)
                    {
                        if (strControlType == "TextBox")
                        {

                            if (((DropDownList)item.FindControl("tone")).SelectedValue == "-1")
                            {
                                if (string.IsNullOrEmpty(((TextBox)item.FindControl(strControlName)).Text))
                                {
                                    ((DropDownList)item.FindControl("tone")).CssClass += " input-validation-error";
                                    ((TextBox)item.FindControl(strControlName)).CssClass += " input-validation-error";
                                    isValidPage = false;
                                    countError++;
                                }
                                else
                                {
                                    ((DropDownList)item.FindControl("tone")).CssClass = ((DropDownList)item.FindControl("tone")).CssClass.Replace("input-validation-error", "");
                                    ((TextBox)item.FindControl(strControlName)).CssClass = ((TextBox)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                }

                                if (strControlName == "txtSSNNumber")
                                {
                                    if (!string.IsNullOrEmpty(((TextBox)item.FindControl(strControlName)).Text))
                                    {
                                        if (((TextBox)item.FindControl(strControlName)).Text.Length != 9)
                                        {
                                            ((TextBox)item.FindControl(strControlName)).CssClass += " input-validation-error";
                                            isValidPage = false;
                                            countError++;
                                        }
                                    }
                                    if (!IsAllDigits(((TextBox)item.FindControl(strControlName)).Text))
                                    {
                                        ((TextBox)item.FindControl(strControlName)).CssClass += " input-validation-error";
                                        isValidPage = false;
                                        countError++;
                                    }
                                }

                            }

                        }
                        else if (strControlType == "Calender")
                        {
                            //DateTime.TryParse(startDateTextBox.Text, out temp)
                            if (((DropDownList)item.FindControl("tone")).SelectedValue == "-1")
                            {
                                DateTime temp;

                                if (string.IsNullOrEmpty(((TextBox)item.FindControl(strControlName)).Text) || !DateTime.TryParse((((TextBox)item.FindControl(strControlName)).Text), out temp))
                                {
                                    ((DropDownList)item.FindControl("tone")).CssClass += " input-validation-error";
                                    ((TextBox)item.FindControl(strControlName)).CssClass += " input-validation-error";
                                    isValidPage = false;
                                    countError++;
                                }
                                else
                                {
                                    ((DropDownList)item.FindControl("tone")).CssClass = ((DropDownList)item.FindControl("tone")).CssClass.Replace("input-validation-error", "");
                                    ((TextBox)item.FindControl(strControlName)).CssClass = ((TextBox)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                }
                            }
                        }
                        else if (strControlType == "CheckBox")
                        {

                            if (((DropDownList)item.FindControl("tone")).SelectedValue == "-1")
                            {
                                if (!(((CheckBox)item.FindControl(strControlName)).Checked))
                                {
                                    ((DropDownList)item.FindControl("tone")).CssClass += " input-validation-error";
                                    ((CheckBox)item.FindControl(strControlName)).CssClass += " input-validation-error";
                                    isValidPage = false;
                                    countError++;
                                }
                                else
                                {
                                    ((DropDownList)item.FindControl("tone")).CssClass = ((DropDownList)item.FindControl("tone")).CssClass.Replace("input-validation-error", "");
                                    ((CheckBox)item.FindControl(strControlName)).CssClass = ((CheckBox)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                }
                            }
                        }
                        else if (strControlType == "RadioButton")
                        {

                            if (((DropDownList)item.FindControl("tone")).SelectedValue == "-1")
                            {
                                if ((((RadioButtonList)item.FindControl(strControlName)).SelectedIndex == -1))
                                {
                                    ((DropDownList)item.FindControl("tone")).CssClass += " input-validation-error";
                                    ((RadioButtonList)item.FindControl(strControlName)).CssClass += " input-validation-error";
                                    isValidPage = false;
                                    countError++;
                                }
                                else
                                {
                                    ((DropDownList)item.FindControl("tone")).CssClass = ((DropDownList)item.FindControl("tone")).CssClass.Replace("input-validation-error", "");
                                    ((RadioButtonList)item.FindControl(strControlName)).CssClass = ((RadioButtonList)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                }
                            }
                        }
                    }
                }
                if (ddlLocation.SelectedValue == "-1")
                {
                    ddlLocation.CssClass += " input-validation-error ";
                    isValidPage = false;
                    countError++;
                }
                else
                {
                    ddlLocation.CssClass = ddlLocation.CssClass.Replace("input-validation-error", "");
                }
                ddlLocation.CssClass += " form-control ";

                if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 51)
                {
                    if (!ValidateConvictionDetailsData())
                    {
                        countError++;
                    }
                }
                if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 48)
                {
                    if (!ValidateDriverLicenceData())
                    {
                        countError++;
                    }
                }
                if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 49)
                {

                    if (!ValidatePreviousEmployeementData())
                    {
                        countError++;
                    }
                }

                //if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
                //{

                //    if (!ValidateCurrentEmployerData())
                //    {
                //        countError++;
                //    }
                //}
                if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
                {

                    if (!ValidateCurrentResidenceData())
                    {
                        countError++;
                    }
                }
                if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
                {

                    if (!ValidatePreviousResidenceData())
                    {
                        countError++;
                    }
                }
                //if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
                //{

                //    if (!ValidateTypeOfEquipmentData())
                //    {
                //        countError++;
                //    }
                //}
                if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
                {

                    if (!ValidateTrafficConvictionsData())
                    {
                        countError++;
                    }
                }

                if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
                {


                    if (!ValidatePreviousEmployerData())
                    {
                        countError++;
                    }
                }

                if (Convert.ToInt32(ddlDocumentType.SelectedValue) == 50 || Convert.ToInt32(ddlDocumentType.SelectedValue) == 59)
                {

                    if (!ValidateCEDPreviousEmployerData())
                    {
                        countError++;
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                DefaultValue();
                PageLoad();
            }
            if (countError > 0)
                return false;
            else
                return true;
        }




        private void UpdateOperation()
        {
            try
            {
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);

                int StartPage = 0, EndPages = 0;
                int UserId = Convert.ToInt32(Request.QueryString["UserId"]);

                int i = -1;
                foreach (RepeaterItem item in rptDynamicControl.Items)
                {
                    i = i + 1;
                    string strDynamicControlValue = string.Empty;
                    string strDynamicControlID = ((HiddenField)item.FindControl("hfDynamicControlID")).Value;
                    string strDyanamicControlValueID = ((HiddenField)item.FindControl("hfDyanamicControlValueID")).Value;
                    string strControlName = ((HiddenField)item.FindControl("hfControlName")).Value;
                    string strControlType = ((HiddenField)item.FindControl("hfControlType")).Value;
                    bool confirmForDbOperationToDb = false;
                    bool pageOperation = true;
                    var clsData = updateControlClassList[i];
                    if (strControlName == "txtStartPage" || strControlName == "txtEndPage")
                    {
                        pageOperation = false;
                    }
                    if (pageOperation)
                    {
                        if (strControlType == "TextBox")
                        {
                            confirmForDbOperationToDb = true;

                            if (clsData.DyanamicControlID == 0)
                                strDynamicControlValue = ((TextBox)item.FindControl(strControlName)).Text;
                            else
                                strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;

                        }
                        else if (strControlType == "Calender")
                        {
                            confirmForDbOperationToDb = true;
                            if (clsData.DyanamicControlID == 0)
                            {
                                strDynamicControlValue = ((TextBox)item.FindControl(strControlName)).Text;
                                DateTime temp;
                                strDynamicControlValue = DateTime.ParseExact(strDynamicControlValue, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");

                            }
                            else
                                strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;


                        }
                        else if (strControlType == "CheckBox")
                        {
                            confirmForDbOperationToDb = true;

                            if (((DropDownList)item.FindControl("tone")).SelectedValue == "-1")
                                strDynamicControlValue = ((CheckBox)item.FindControl(strControlName)).Checked ? "true" : "false";
                            else
                                strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;

                            //strDynamicControlValue = ((CheckBox)item.FindControl(strControlName)).Checked ? "true" : "false";
                        }
                        else if (strControlType == "RadioButton")
                        {
                            confirmForDbOperationToDb = true;

                            if (clsData.DyanamicControlID == 0)
                                strDynamicControlValue = ((RadioButtonList)item.FindControl(strControlName)).SelectedValue;
                            else
                                strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;


                        }
                        if (confirmForDbOperationToDb)
                        {
                            int DynamicControlId = Convert.ToInt32(strDynamicControlID);
                            int DyanamicControlValueID = Convert.ToInt32(strDyanamicControlValueID);
                            //Exception_FormData dcv = dpfdatabase.Exception_FormData.Where(x => x.FormDataID == DyanamicControlValueID).SingleOrDefault();
                            var dcv = new BLConsumeApi().qcExceptionFormDataById(DyanamicControlValueID);
                            if (dcv != null)
                            {
                                foreach (var items in dcv)
                                {
                                    new BLConsumeApi().qcUpdateExceptionFormData(items.FormDataID, strDynamicControlValue);
                                }
                                //dcv.DynamicControlValueText = strDynamicControlValue;
                                //dpfdatabase.SaveChanges();
                            }
                        }
                    }
                }
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                //var dynamicControlIdForLocation = dpfdatabase.DynamicControls.Where(x => x.DocumentTypeID == DocumentTypeId && x.ControlName == "txtLocationName").SingleOrDefault();
                var dynamicControlIdForLocation = new BLConsumeApi().DynamicControlByDocumentTypeId(DocumentTypeId, "txtLocationName");
                if (dynamicControlIdForLocation != null)
                {
                    if (!string.IsNullOrEmpty(hfLocationId.Value))
                    {
                        int DyanamicControlValueID = Convert.ToInt32(hfLocationId.Value);
                        //Exception_FormData dcv = dpfdatabase.Exception_FormData.Where(x => x.FormDataID == DyanamicControlValueID).SingleOrDefault();
                        var dcv = new BLConsumeApi().qcExceptionFormDataById(DyanamicControlValueID);
                        if (dcv != null)
                        {
                            foreach (var items in dcv)
                            {
                                new BLConsumeApi().qcUpdateExceptionFormData(items.FormDataID, ddlLocation.SelectedValue);
                            }

                            //dcv.DynamicControlValueText = ddlLocation.SelectedValue;
                            //dpfdatabase.SaveChanges();
                        }
                    }
                }

                // var dynamicControlIdForStartPage = dpfdatabase.DynamicControls.Where(x => x.DocumentTypeID == DocumentTypeId && x.ControlName == "txtStartPage").SingleOrDefault();
                var dynamicControlIdForStartPage = new BLConsumeApi().DynamicControlByDocumentTypeId(DocumentTypeId, "txtStartPage");
                if (dynamicControlIdForStartPage != null)
                {
                    if (!string.IsNullOrEmpty(hfStartPageId.Value))
                    {
                        int DyanamicControlValueID = Convert.ToInt32(hfStartPageId.Value);
                        //Exception_FormData dcv = dpfdatabase.Exception_FormData.Where(x => x.FormDataID == DyanamicControlValueID).SingleOrDefault();
                        var dcv = new BLConsumeApi().qcExceptionFormDataById(DyanamicControlValueID);
                        if (dcv != null)
                        {
                            foreach (var items in dcv)
                            {
                                new BLConsumeApi().qcUpdateExceptionFormData(items.FormDataID, txtStartPage.Text);
                            }
                            //dcv.DynamicControlValueText = txtStartPage.Text;
                            //dpfdatabase.SaveChanges();
                        }
                    }
                }


                //var dynamicControlIdForEndPage = dpfdatabase.DynamicControls.Where(x => x.DocumentTypeID == DocumentTypeId && x.ControlName == "txtEndPage").SingleOrDefault();
                var dynamicControlIdForEndPage = new BLConsumeApi().DynamicControlByDocumentTypeId(DocumentTypeId, "txtEndPage");
                if (dynamicControlIdForEndPage != null)
                {
                    if (!string.IsNullOrEmpty(hfEndPageId.Value))
                    {
                        int DyanamicControlValueID = Convert.ToInt32(hfEndPageId.Value);
                        //Exception_FormData dcv = dpfdatabase.Exception_FormData.Where(x => x.FormDataID == DyanamicControlValueID).SingleOrDefault();
                        var dcv = new BLConsumeApi().qcExceptionFormDataById(DyanamicControlValueID);
                        if (dcv != null)
                        {
                            foreach (var items in dcv)
                            {
                                new BLConsumeApi().qcUpdateExceptionFormData(items.FormDataID, txtEndPage.Text);
                            }
                            //dcv.DynamicControlValueText = txtEndPage.Text;
                            //dpfdatabase.SaveChanges();
                        }
                    }
                }




                PerformFinalUpdateOperation(TempCPScreenDataID);


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                DefaultValue();
                PageLoad();
            }
        }

        private void PerformFinalUpdateOperation(int TempCPScreenDataID)
        {

            rptDynamicControl.DataSource = null;
            rptDynamicControl.DataSourceID = null;
            rptDynamicControl.DataBind();
            dropDownValueList = new List<DropDownValue>();

            //var CPScreenDataTempData = dpfdatabasenew.Exception_CPScreenData.Where(x => x.CPScreenDataID == TempCPScreenDataID).SingleOrDefault();
            var CPScreenDataTempData = new BLConsumeApi().sp_FindException_CPScreenDataByCPScreenDataID(TempCPScreenDataID);
            if (CPScreenDataTempData != null)
            {
                DefaultValue();

                if (Session["DocumentTypeReviewList"] != null)
                    ManageSession();

                if (Session["DocumentTypeReviewList"] == null)
                {

                    PerformSubmitOperation();

                }
                else
                {

                    PageLoad();
                }
            }
            else
            {
                PerformSubmitOperation();
            }


        }

        private bool MatchDataForBothUsers(List<DynamicControlResponse> result, int CountForFaxId)
        {
            txtEndPage.Enabled = false;
            ddlDocumentType.Enabled = false;
            ddlCustomer.Enabled = false;
            bool isAllMatched = true;
            int errorCount = 0;
            try
            {
                if (Session["SubmittedTaskByOtherUser"] != null)
                {
                    int UserId = Convert.ToInt32(Request.QueryString["UserId"]);
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    foreach (RepeaterItem item in rptDynamicControl.Items)
                    {

                        int pages = 0;
                        int dynamicControlID = 0;
                        bool showError = false;
                        string strDynamicControlValue = string.Empty;
                        string strDynamicControlID = ((HiddenField)item.FindControl("hfDynamicControlID")).Value;
                        dynamicControlID = Convert.ToInt32(strDynamicControlID);
                        string strDyanamicControlValueID = ((HiddenField)item.FindControl("hfDyanamicControlValueID")).Value;
                        string strControlName = ((HiddenField)item.FindControl("hfControlName")).Value;
                        string strControlType = ((HiddenField)item.FindControl("hfControlType")).Value;
                        var data = result.Where(x => x.DynamicControlID == dynamicControlID).Single();
                        bool pageOperation = true;
                        if (strControlName == "txtStartPage" || strControlName == "txtEndPage")
                        {
                            pageOperation = false;
                        }
                        if (pageOperation)
                        {
                            if (strControlType == "TextBox")
                            {
                                if (((DropDownList)item.FindControl("tone")).SelectedValue != "-1")
                                {
                                    if (((DropDownList)item.FindControl("tone")).Items.Count > 0)
                                    {
                                        strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                        {
                                            ((DropDownList)item.FindControl("tone")).CssClass += " input-validation-error ";
                                            errorCount++;
                                        }
                                        else
                                        {
                                            ((DropDownList)item.FindControl("tone")).CssClass = ((DropDownList)item.FindControl("tone")).CssClass.Replace("input-validation-error", "");
                                        }
                                    }
                                    else
                                    {
                                        strDynamicControlValue = ((TextBox)item.FindControl(strControlName)).Text;
                                        if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                        {
                                            ((TextBox)item.FindControl(strControlName)).CssClass += " input-validation-error ";
                                            errorCount++;
                                        }
                                        else
                                        {
                                            ((TextBox)item.FindControl(strControlName)).CssClass = ((TextBox)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                        }
                                    }
                                }
                                else
                                {
                                    strDynamicControlValue = ((TextBox)item.FindControl(strControlName)).Text;
                                    if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                    {
                                        ((TextBox)item.FindControl(strControlName)).CssClass += " input-validation-error ";
                                        errorCount++;
                                    }
                                    else
                                    {
                                        ((TextBox)item.FindControl(strControlName)).CssClass = ((TextBox)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                    }
                                }

                            }
                            else if (strControlType == "Calender")
                            {
                                if (((DropDownList)item.FindControl("tone")).SelectedValue != "-1")
                                {
                                    if (((DropDownList)item.FindControl("tone")).Items.Count > 0)
                                    {
                                        strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                        {
                                            ((DropDownList)item.FindControl("tone")).CssClass += " input-validation-error ";
                                            errorCount++;
                                        }
                                        else
                                        {
                                            ((DropDownList)item.FindControl("tone")).CssClass = ((DropDownList)item.FindControl("tone")).CssClass.Replace("input-validation-error", "");
                                        }
                                    }
                                    else
                                    {
                                        strDynamicControlValue = ((TextBox)item.FindControl(strControlName)).Text;
                                        if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                        {
                                            ((TextBox)item.FindControl(strControlName)).CssClass += " input-validation-error ";
                                            errorCount++;
                                        }
                                        else
                                        {
                                            ((TextBox)item.FindControl(strControlName)).CssClass = ((TextBox)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                        }
                                    }
                                }
                                else
                                {
                                    strDynamicControlValue = ((TextBox)item.FindControl(strControlName)).Text;
                                    strDynamicControlValue = DateTime.ParseExact(strDynamicControlValue, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");

                                    if (strDynamicControlValue != data.DynamicControlValueText)
                                    {
                                        ((TextBox)item.FindControl(strControlName)).CssClass += " input-validation-error ";
                                        errorCount++;
                                    }
                                    else
                                    {
                                        ((TextBox)item.FindControl(strControlName)).CssClass = ((TextBox)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                    }
                                }

                            }
                            else if (strControlType == "CheckBox")
                            {
                                showError = true;
                                if (((DropDownList)item.FindControl("tone")).SelectedValue != "-1")
                                {
                                    if (((DropDownList)item.FindControl("tone")).Items.Count > 0)
                                    {
                                        strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                        {
                                            ((DropDownList)item.FindControl("tone")).CssClass += " input-validation-error ";
                                            errorCount++;
                                        }
                                        else
                                        {
                                            ((DropDownList)item.FindControl("tone")).CssClass = ((DropDownList)item.FindControl("tone")).CssClass.Replace("input-validation-error", "");
                                        }
                                    }
                                    else
                                    {
                                        strDynamicControlValue = ((CheckBox)item.FindControl(strControlName)).Checked ? "true" : "false";
                                        if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                        {
                                            ((CheckBox)item.FindControl(strControlName)).CssClass += " input-validation-error ";
                                            errorCount++;
                                        }
                                        else
                                        {
                                            ((CheckBox)item.FindControl(strControlName)).CssClass = ((CheckBox)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                        }
                                    }
                                }
                                else
                                {
                                    strDynamicControlValue = ((CheckBox)item.FindControl(strControlName)).Checked ? "true" : "false";
                                    if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                    {
                                        ((CheckBox)item.FindControl(strControlName)).CssClass += " input-validation-error ";
                                        errorCount++;
                                    }
                                    else
                                    {
                                        ((CheckBox)item.FindControl(strControlName)).CssClass = ((CheckBox)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                    }
                                }

                            }
                            else if (strControlType == "RadioButton")
                            {
                                showError = true;
                                if (((DropDownList)item.FindControl("tone")).SelectedValue != "-1")
                                {
                                    if (((DropDownList)item.FindControl("tone")).Items.Count > 0)
                                    {
                                        strDynamicControlValue = ((DropDownList)item.FindControl("tone")).SelectedValue;
                                        if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                        {
                                            ((DropDownList)item.FindControl("tone")).CssClass += " input-validation-error ";
                                            errorCount++;
                                        }
                                        else
                                        {
                                            ((DropDownList)item.FindControl("tone")).CssClass = ((DropDownList)item.FindControl("tone")).CssClass.Replace("input-validation-error", "");
                                        }
                                    }
                                    else
                                    {
                                        strDynamicControlValue = ((RadioButtonList)item.FindControl(strControlName)).SelectedValue;
                                        if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                        {
                                            ((RadioButtonList)item.FindControl(strControlName)).CssClass += " input-validation-error ";
                                            errorCount++;
                                        }
                                        else
                                        {
                                            ((RadioButtonList)item.FindControl(strControlName)).CssClass = ((RadioButtonList)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                        }
                                    }

                                }
                                else
                                {
                                    strDynamicControlValue = ((RadioButtonList)item.FindControl(strControlName)).SelectedValue;
                                    if (strDynamicControlValue.ToLower() != data.DynamicControlValueText.ToLower())
                                    {
                                        ((RadioButtonList)item.FindControl(strControlName)).CssClass += " input-validation-error ";
                                        errorCount++;
                                    }
                                    else
                                    {
                                        ((RadioButtonList)item.FindControl(strControlName)).CssClass = ((RadioButtonList)item.FindControl(strControlName)).CssClass.Replace("input-validation-error", "");
                                    }
                                }

                            }
                        }

                    }

                    var locationData = result.Where(x => x.ControlName == "txtLocationName").SingleOrDefault();
                    if (locationData != null)
                    {
                        if (locationData.DynamicControlValueText != ddlLocation.SelectedValue)
                        {
                            isAllMatched = false;
                            //var locationDynamicId = db.Exception_FormData.Where(x => x.UserId == UserId && x.DynamicControlID == locationData.DynamicControlID && x.CPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountForFaxId).Single();
                            var locationDynamicId = new BLConsumeApi().FindException_FormDataBYUserIdDynamicControlID(UserId, locationData.DynamicControlID, TempCPScreenDataID, CountForFaxId);
                            //hfLocationId.Value = Convert.ToString(locationDynamicId.FormDataID);
                            hfLocationId.Value = Convert.ToString(locationDynamicId[0].FormDataID);
                            ddlLocation.CssClass += " input-validation-error";
                            errorCount++;
                        }
                        else
                        {
                            lblLocatoinError.Visible = false;
                            ddlLocation.CssClass = ddlLocation.CssClass.Replace("input-validation-error", "");

                        }
                    }

                    var startPageData = result.Where(x => x.ControlName == "txtStartPage").SingleOrDefault();
                    if (startPageData != null)
                    {
                        if (startPageData.DynamicControlValueText != txtStartPage.Text)
                        {
                            isAllMatched = false;
                            //var startPageDynamicId = db.Exception_FormData.Where(x => x.UserId == UserId && x.DynamicControlID == startPageData.DynamicControlID && x.CPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountForFaxId).Single();
                            var startPageDynamicId = new BLConsumeApi().FindException_FormDataBYUserIdDynamicControlID(UserId, startPageData.DynamicControlID, TempCPScreenDataID, CountForFaxId);
                            hfStartPageId.Value = Convert.ToString(startPageDynamicId[0].FormDataID);
                            txtStartPage.CssClass += " input-validation-error";
                            errorCount++;
                        }
                        else
                        {

                            txtStartPage.CssClass = txtStartPage.CssClass.Replace("input-validation-error", "");
                        }
                    }

                    var endPageData = result.Where(x => x.ControlName == "txtEndPage").SingleOrDefault();
                    if (endPageData != null)
                    {
                        if (endPageData.DynamicControlValueText != txtEndPage.Text)
                        {
                            isAllMatched = false;
                            var endPageDynamicId = new BLConsumeApi().FindException_FormDataBYUserIdDynamicControlID(UserId, endPageData.DynamicControlID, TempCPScreenDataID, CountForFaxId);
                            //hfEndPageId.Value = Convert.ToString(endPageDynamicId.FormDataID);
                            hfEndPageId.Value = Convert.ToString(endPageDynamicId[0].FormDataID);
                            txtEndPage.CssClass += " input-validation-error";
                            errorCount++;
                        }
                        else
                        {
                            txtEndPage.CssClass = txtEndPage.CssClass.Replace("input-validation-error", "");
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            hfIsReviewed.Value = "1";
            if (errorCount > 0)
                return false;
            else
                return true;
        }

        private void PageLoad()
        {
            try
            {
                //if (Session["UserId"] == null)
                //    Response.Redirect("~/UserLogin.aspx", false);
                //if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                //    Response.Redirect("~/UserLogin.aspx", false);

                //if (Session["UserName"] == null)
                //{
                //    GerUserNameByUserId();
                //}
                //lblUserName.Text = Convert.ToString(Session["UserName"]);



                int UserId = Convert.ToInt32(Request.QueryString["UserId"]);
                // db.uspAutoTaskAssignmentByUserId(UserId);


                if (GetTaskAssignmentUserId())
                {
                    BindCustomerDropDown();

                    if (Session["DocumentTypeReviewList"] != null)
                    {
                        int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        //var rechekData = db.Exception_CPScreenData.Where(x => x.CPScreenDataID == TempCPScreenDataID).SingleOrDefault();
                        var rechekData = new BLConsumeApi().sp_FindException_FormDataBYCPScreenDataID(TempCPScreenDataID);
                        if (rechekData == null)
                        {
                            PerformSubmitOperation();
                        }
                        else
                        {
                            List<DocumentTypeReviewList> documentTypeReviewList = (List<DocumentTypeReviewList>)Session["DocumentTypeReviewList"];
                            DocumentTypeReviewList doc = documentTypeReviewList.Where(x => x.IsReview == false).First();
                            Session["CurrentDocument"] = doc;
                            hfCountForFaxId.Value = doc.CountForFaxId.ToString();
                            //var customerId = db.DocumentTypes.Where(x => x.DocumentTypeID == doc.DocumentTypeId && x.IsActive == true).Select(x => x.CustomerID).First();
                            var customerId = new BLConsumeApi().usp_DocumentTypesListByDocumentTypeId(Convert.ToInt32(doc.DocumentTypeId));
                            //ddlCustomer.SelectedValue = Convert.ToString(customerId.Value);
                            ddlCustomer.SelectedValue = Convert.ToString(customerId[0].CustomerID);
                            Session["CustomerId"] = customerId[0].CustomerID;
                            //BindDocumentTypeDropDown(customerId.Value);
                            BindDocumentTypeDropDown(customerId[0].CustomerID);
                            ddlDocumentType.SelectedValue = Convert.ToString(doc.DocumentTypeId);
                            ddlLocation.Enabled = true;
                            ddlDocumentType.Enabled = true;
                            BindAllDataByDocumentTypeId(doc.DocumentTypeId.Value, doc.CountForFaxId.Value);


                        }

                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private void GetSubmittedTaskByOtherUser(int DocumentTypeId, int CountForFaxId = 0)
        {
            if (!string.IsNullOrEmpty(hfTempCPScreenDataID.Value))
            {
                int tempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int userId = Convert.ToInt32(Request.QueryString["UserId"]);
                //var result = (from dc in db.DynamicControls
                //              join dvc in db.Exception_FormData on dc.DynamicControlID equals dvc.DynamicControlID
                //              where dc.DocumentTypeID == DocumentTypeId && dvc.UserId != userId
                //              && dvc.CPScreenDataID == tempCPScreenDataID && dvc.CountForFaxId == CountForFaxId && dc.IsActive == true
                //              select new DynamicControlResponse
                //              {
                //                  DynamicControlID = dvc.DynamicControlID,
                //                  DynamicControlValueText = dvc.DynamicControlValueText,
                //                  DyanamicControlValueID = dvc.DynamicControlID,
                //                  ControlType = dc.ControlType,
                //                  labelName = dc.labelName,
                //                  ControlName = dc.ControlName,
                //                  DocumentTypeID = DocumentTypeId,
                //                  DropDownValue = dc.DropDownValue
                //              }
                //              ).ToList();
                var result = new BLConsumeApi().GetSubmittedTaskByOtherUser(DocumentTypeId, CountForFaxId, userId, tempCPScreenDataID);

                Session["SubmittedTaskByOtherUser"] = result;
            }
        }

        private void GerUserNameByUserId()
        {
            try
            {
                int UserId = Convert.ToInt32(Request.QueryString["UserId"]);
                //var result = db.UserDetails.Where(x => x.UserDetailsID == UserId).Select(x => new { x.UserFirstName, x.UserLastName }).Single();
                var result = new BLConsumeApi().GetUserNameByUserId(UserId);
                //Session["UserName"] = result.UserFirstName + " " + result.UserLastName;
                Session["UserName"] = result[0].UserFirstName + " " + result[0].UserLastName;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private bool GetTaskAssignmentUserId()
        {
            try
            {
                int UserId = Convert.ToInt32(Request.QueryString["UserId"]);
                int faxId = Convert.ToInt32(Request.QueryString["FaxId"]);
                if (Session["DocumentTypeReviewList"] == null)
                {
                    //var misMatchedDataResult =   db.Exception_CPScreenData.Where(x => x.FaxID == faxId).SingleOrDefault();
                    var misMatchedDataResult = new BLConsumeQCApi().getException_CPScreenData(faxId);
                    if (misMatchedDataResult != null)
                    {
                        var userList = misMatchedDataResult[0].UserId.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                        int userid1 = userList[0];
                        int userid2 = userList[1];


                        var userNameList = new BLConsumeQCApi().getUserList(userid1, userid2); //db.UserDetails.Where(x => userList.Contains(x.UserDetailsID)).Select(x => new UserNameList { UserId = x.UserDetailsID, UserName = x.UserFirstName }).ToList();

                        //var userNameList = (from usr in db.UserDetails
                        //                  join ul in userList on usr.UserDetailsID equals ul.UserId
                        //                    select new  { usr.UserDetailsID,  usr.UserFirstName }).ToList();

                        // var userNameList= db.UserDetails.Where(x=>x.UserDetailsID== userList.)


                        ddlUsers.DataSource = userNameList;
                        ddlUsers.DataTextField = "UserFirstName";
                        ddlUsers.DataValueField = "UserDetailsID";
                        ddlUsers.DataBind();

                        ddlUsers.SelectedValue = Convert.ToString(UserId);

                        int tempCPScreenDataID = misMatchedDataResult[0].CPScreenDataID;
                        hfTempCPScreenDataID.Value = Convert.ToString(misMatchedDataResult[0].CPScreenDataID);
                        lblFaxId.Text = Convert.ToString(misMatchedDataResult[0].FaxID);
                        if (hfTaskOperationId.Value == "0")
                        {
                            MlTaskOperation to = new MlTaskOperation();
                            to.FaxId = misMatchedDataResult[0].FaxID;
                            to.StartTime = DateTime.Now;
                            to.UserId = UserId;
                            //context.TaskOperations.Add(to);
                            new BLConsumeApi().insertTaskOperation(to);
                            //context.SaveChanges();
                            hfTaskOperationId.Value = Convert.ToString(to.TaskOperationId);
                        }

                        int TempCPScreenDataID = misMatchedDataResult[0].CPScreenDataID;
                        //int TotalSubmitedPages = misMatchedDataResult.NoOfPagesCompleted.Value;

                        //var DocumentTypeDataList = db.DocumentTypeEntryDetails.Where(x => x.TempCPScreenDataID == tempCPScreenDataID && x.UserId == UserId).OrderBy(x => x.CountForFaxId).Select(x => new DocumentTypeDetails { DocumentTypeId = x.DocumentTypeId, CountForFaxId = x.CountForFaxId }).ToList();
                        var DocumentTypeDataList = new BLConsumeApi().DocumentTypeEntryDetailByTempCPScreenDataID(tempCPScreenDataID, UserId);
                        List<int> documentTypeIdList = new List<int>();
                        foreach (DocumentTypeDetails doc in DocumentTypeDataList)
                        {
                            int id = Convert.ToInt32(doc.DocumentTypeId);
                            documentTypeIdList.Add(id);
                        }

                        List<DocumentTypeReviewList> documentTypeReviewList = new List<DocumentTypeReviewList>();
                        foreach (var dl in DocumentTypeDataList)
                        {
                            DocumentTypeReviewList doc = new DocumentTypeReviewList();
                            doc.DocumentTypeId = dl.DocumentTypeId;
                            doc.CountForFaxId = dl.CountForFaxId;
                            doc.IsReview = false;
                            documentTypeReviewList.Add(doc);
                        }

                        DocumentTypeReviewList docDetails = documentTypeReviewList.Where(x => x.IsReview == false).First();
                        Session["CurrentDocument"] = docDetails;
                        Session["DocumentTypeReviewList"] = documentTypeReviewList;
                        Session["DocumentTypeDataList"] = DocumentTypeDataList;

                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
            return true;
        }

        private void BindCustomerDropDown()
        {
            try
            {
                //var result = db.CustomerDetails.Where(x => x.IsActive == true).ToList();
                var result = new BLConsumeApi().CustomerDetailsByIsActive();
                ddlCustomer.DataSource = result;
                ddlCustomer.DataTextField = "CompanyName";
                ddlCustomer.DataValueField = "CompanyID";
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, new ListItem("Select Customer", "-1"));
                ddlCustomer.SelectedIndex = 0;
                DefaultValue();


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private void BindLocationAndDocumentTypeByCustomerId(int CustomerId)
        {
            try
            {

                //var locationList = (from lo in db.Locations
                //                    join cus in db.CustomerDetails on lo.CustomerID equals cus.CompanyID
                //                    where cus.CompanyID == CustomerId
                //                    select new { lo.Fadv_LocationID, lo.LocationName }).ToList();
                var locationList = new BLConsumeApi().LocationJoinCustomerDetail(CustomerId);
                ddlLocation.DataSource = locationList;
                ddlLocation.DataTextField = "LocationName";
                ddlLocation.DataValueField = "Fadv_LocationID";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("Select Location", "-1"));

                //var documentTypeList = db.DocumentTypes.Where(x => x.CustomerID == CustomerId && x.IsActive == true).OrderBy(x => x.DocumentTypeName).ToList();
                var documentTypeList = new BLConsumeApi().usp_DocumentTypesListByCustomerID(CustomerId);
                ddlDocumentType.DataSource = documentTypeList;
                ddlDocumentType.DataTextField = "DocumentTypeName";
                ddlDocumentType.DataValueField = "DocumentTypeID";
                ddlDocumentType.DataBind();
                ddlDocumentType.Items.Insert(0, new ListItem("Select Document Type", "-1"));
                //    dvDocumentType.Visible = true;
                //    dvLocation.Visible = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        #endregion
        #region SaveToDbUserControl


        //Save Date for ConvictionDetails
        private void SaveConvictionDetailsData()
        {

            int count = 0;
            int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
            if (Session["ConvictionDetails"] != null)
            {
                count = Convert.ToInt32(Session["ConvictionDetails"]);
                if (Session["DriverConvictionList"] != null)
                {
                    List<DriverConviction> DriverConvictionList = Session["DriverConvictionList"] as List<DriverConviction>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectConvictionsDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.ConvictionDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deleteMlConvictionsDetails(item.ConvictionDetailsId);
                        }
                        //dpfdatabase.ConvictionDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string txt1 = ((TextBox)ConvictionDetails1.FindControl(str1)).Text;
                    // txt1 = DateTime.ParseExact(txt1, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                    string txt2 = ((TextBox)ConvictionDetails1.FindControl(str2)).Text;

                    string txt3 = ((TextBox)ConvictionDetails1.FindControl(str3)).Text;
                    string txt4 = ((TextBox)ConvictionDetails1.FindControl(str4)).Text;

                    MlConvictionDetail cd = new MlConvictionDetail();
                    cd.DateOfConviction = txt1;
                    cd.Offense = txt2;
                    cd.Location = txt3;
                    cd.TypeOfVehicleOperated = txt4;
                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    new BLConsumeApi().insertConvictionsDetails(cd);

                    //dpfdatabase.ConvictionDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)ConvictionDetails1.FindControl("tbl");
                tbl.Rows.Clear();

            }
            else if (Session["DriverConvictionList"] != null)
            {

                if (Session["DriverConvictionList"] != null)
                {
                    List<DriverConviction> DriverConvictionList = Session["DriverConvictionList"] as List<DriverConviction>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectConvictionsDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.ConvictionDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deleteMlConvictionsDetails(item.ConvictionDetailsId);
                        }
                        //dpfdatabase.ConvictionDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string txt1 = ((TextBox)ConvictionDetails1.FindControl(str1)).Text;
                    // txt1 = DateTime.ParseExact(txt1, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                    string txt2 = ((TextBox)ConvictionDetails1.FindControl(str2)).Text;
                    string txt3 = ((TextBox)ConvictionDetails1.FindControl(str3)).Text;
                    string txt4 = ((TextBox)ConvictionDetails1.FindControl(str4)).Text;

                    MlConvictionDetail cd = new MlConvictionDetail();
                    cd.DateOfConviction = txt1;
                    cd.Offense = txt2;
                    cd.Location = txt3;
                    cd.TypeOfVehicleOperated = txt4;
                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;


                    new BLConsumeApi().insertConvictionsDetails(cd);
                    //dpfdatabase.ConvictionDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)ConvictionDetails1.FindControl("tbl");
                tbl.Rows.Clear();

            }

        }
        //Save Date for DriverLicenceDetails
        private void SaveDriverLicenceData()
        {

            int count = 0;
            int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
            if (Session["DriverLicenceCount"] != null)
            {
                count = Convert.ToInt32(Session["DriverLicenceCount"]);
                if (Session["DriverLicenceList"] != null)
                {
                    List<DriverLicence> DriverConvictionList = Session["DriverLicenceList"] as List<DriverLicence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectMlDriverLicenceDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.DriverLicenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deleteMlDriverLicenceDetail(item.DriverLicenceId);
                        }
                        //dpfdatabase.DriverLicenceDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();

                    string txt1 = ((TextBox)TgDriverLicence1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgDriverLicence1.FindControl(str2)).Text;


                    MlDriverLicenceDetail cd = new MlDriverLicenceDetail();
                    cd.Restriction = txt1;
                    cd.Endorsement = txt2;
                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    new BLConsumeApi().insertDriverLicenceDetail(cd);
                    //dpfdatabase.DriverLicenceDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)TgDriverLicence1.FindControl("tbl");
                tbl.Rows.Clear();

            }
            else if (Session["DriverLicenceList"] != null)
            {

                if (Session["DriverLicenceList"] != null)
                {
                    List<DriverLicence> DriverConvictionList = Session["DriverLicenceList"] as List<DriverLicence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectMlDriverLicenceDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.DriverLicenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deleteMlDriverLicenceDetail(item.DriverLicenceId);
                        }
                        //dpfdatabase.DriverLicenceDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();

                    string txt1 = ((TextBox)TgDriverLicence1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgDriverLicence1.FindControl(str2)).Text;


                    MlDriverLicenceDetail cd = new MlDriverLicenceDetail();
                    cd.Restriction = txt1;
                    cd.Endorsement = txt2;

                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    //dpfdatabase.DriverLicenceDetails.Add(cd);
                    //dpfdatabase.SaveChanges();
                    new BLConsumeApi().insertDriverLicenceDetail(cd);

                }
                HtmlTable tbl = (HtmlTable)ConvictionDetails1.FindControl("tbl");
                tbl.Rows.Clear();

            }

        }
        //Save Data for Previous Employeemnt Details
        private void SavePreviousEmployeementData()
        {

            int count = 0;
            int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
            if (Session["PreviousEmployeementCount"] != null)
            {
                count = Convert.ToInt32(Session["PreviousEmployeementCount"]);
                if (Session["PreviousEmployeementList"] != null)
                {
                    List<PreviousEmployeement> DriverConvictionList = Session["PreviousEmployeementList"] as List<PreviousEmployeement>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectPreviousEmploymentDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.PreviousEmploymentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deletePreviousEmploymentDetail(item.PreviousEmploymentDetailId);
                        }
                        //dpfdatabase.PreviousEmploymentDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();

                    string txt1 = ((TextBox)TgPreviousEmployment1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgPreviousEmployment1.FindControl(str2)).Text;
                    // txt2 = DateTime.ParseExact(txt2, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                    string txt3 = ((TextBox)TgPreviousEmployment1.FindControl(str3)).Text;
                    // txt3 = DateTime.ParseExact(txt3, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");


                    MlPreviousEmploymentDetail cd = new MlPreviousEmploymentDetail();
                    cd.EmployerName = txt1;
                    cd.EmploymentStartDate = txt2;
                    cd.EmploymentEndDate = txt3;
                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    new BLConsumeApi().insertPreviousEmploymentDetail(cd);
                    //dpfdatabase.PreviousEmploymentDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
                tbl.Rows.Clear();

            }
            else if (Session["PreviousEmployeementList"] != null)
            {

                if (Session["PreviousEmployeementList"] != null)
                {
                    List<PreviousEmployeement> DriverConvictionList = Session["PreviousEmployeementList"] as List<PreviousEmployeement>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectPreviousEmploymentDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.PreviousEmploymentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deletePreviousEmploymentDetail(item.PreviousEmploymentDetailId);
                        }
                        //dpfdatabase.PreviousEmploymentDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();

                    string txt1 = ((TextBox)TgPreviousEmployment1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgPreviousEmployment1.FindControl(str2)).Text;
                    //  txt2 = DateTime.ParseExact(txt2, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                    string txt3 = ((TextBox)TgPreviousEmployment1.FindControl(str3)).Text;
                    //  txt3 = DateTime.ParseExact(txt3, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");


                    MlPreviousEmploymentDetail cd = new MlPreviousEmploymentDetail();
                    cd.EmployerName = txt1;
                    cd.EmploymentStartDate = txt2;
                    cd.EmploymentEndDate = txt3;
                    cd.CountForFaxId = countForFaxId;

                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);

                    new BLConsumeApi().insertPreviousEmploymentDetail(cd);
                    //dpfdatabase.PreviousEmploymentDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
                tbl.Rows.Clear();

            }

        }
        private void SavePreviousEmployerData()
        {

            int count = 0;
            int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
            if (Session["PreviousEmployerCount"] != null)
            {
                count = Convert.ToInt32(Session["PreviousEmployerCount"]);
                if (Session["PreviousEmployerList"] != null)
                {
                    List<PreviousEmployer> DriverConvictionList = Session["PreviousEmployerList"] as List<PreviousEmployer>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                BLConsumeApi prev = new BLConsumeApi();
                var prevRecord = prev.SelectPreviousEmployerDetailsByAllIds(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);

                //var prevRecord = dpfdatabase.PreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            prev.deletePreviousEmployerDetailId(item.PreviousEmployerDetailId);
                        }
                        //dpfdatabase.PreviousEmployerDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();
                    string str6 = "txt6" + i.ToString();
                    string str7 = "txt7" + i.ToString();
                    string str8 = "txt8" + i.ToString();

                    string txt1 = ((TextBox)TgPreviousEmployer1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgPreviousEmployer1.FindControl(str2)).Text;
                    string txt3 = ((TextBox)TgPreviousEmployer1.FindControl(str3)).Text;
                    string txt4 = ((TextBox)TgPreviousEmployer1.FindControl(str4)).Text;
                    string txt5 = ((TextBox)TgPreviousEmployer1.FindControl(str5)).Text;
                    string txt6 = ((TextBox)TgPreviousEmployer1.FindControl(str6)).Text;
                    string txt7 = ((TextBox)TgPreviousEmployer1.FindControl(str7)).Text;
                    string txt8 = ((TextBox)TgPreviousEmployer1.FindControl(str8)).Text;

                    MlPreviousEmployerDetail cd = new MlPreviousEmployerDetail();
                    cd.PreviousEmployerName = txt1;
                    cd.PreviousEmployerStreetAddress = txt2;
                    cd.PreviousEmployerCity = txt3;
                    cd.PreviousEmployerState = txt4;
                    cd.PreviousEmployerZipcode = txt5;
                    cd.PreviousEmploymentStartDate = txt6;
                    cd.PreviousEmploymentEndDate = txt7;
                    cd.ReasonForLeavingPreviousEmployments = txt8;
                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    BLConsumeApi insertOperation = new BLConsumeApi();
                    insertOperation.insertPreviousEmployerDetailId(cd);


                    //dpfdatabase.PreviousEmployerDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
                tbl.Rows.Clear();

            }
            else if (Session["PreviousEmployerList"] != null)
            {

                if (Session["PreviousEmployerList"] != null)
                {
                    List<PreviousEmployer> DriverConvictionList = Session["PreviousEmployerList"] as List<PreviousEmployer>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                BLConsumeApi prevs = new BLConsumeApi();
                var prevRecord = prevs.SelectPreviousEmployerDetailsByAllIds(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.PreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            prevs.deletePreviousEmployerDetailId(item.PreviousEmployerDetailId);
                        }
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();
                    string str6 = "txt6" + i.ToString();
                    string str7 = "txt7" + i.ToString();
                    string str8 = "txt8" + i.ToString();

                    string txt1 = ((TextBox)TgPreviousEmployer1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgPreviousEmployer1.FindControl(str2)).Text;
                    string txt3 = ((TextBox)TgPreviousEmployer1.FindControl(str3)).Text;
                    string txt4 = ((TextBox)TgPreviousEmployer1.FindControl(str4)).Text;
                    string txt5 = ((TextBox)TgPreviousEmployer1.FindControl(str5)).Text;
                    string txt6 = ((TextBox)TgPreviousEmployer1.FindControl(str6)).Text;
                    string txt7 = ((TextBox)TgPreviousEmployer1.FindControl(str7)).Text;
                    string txt8 = ((TextBox)TgPreviousEmployer1.FindControl(str8)).Text;

                    MlPreviousEmployerDetail cd = new MlPreviousEmployerDetail();
                    cd.PreviousEmployerName = txt1;
                    cd.PreviousEmployerStreetAddress = txt2;
                    cd.PreviousEmployerCity = txt3;
                    cd.PreviousEmployerState = txt4;
                    cd.PreviousEmployerZipcode = txt5;
                    cd.PreviousEmploymentStartDate = txt6;
                    cd.PreviousEmploymentEndDate = txt7;
                    cd.ReasonForLeavingPreviousEmployments = txt8;
                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    BLConsumeApi insertOperation = new BLConsumeApi();
                    insertOperation.insertPreviousEmployerDetailId(cd);
                    //dpfdatabase.PreviousEmployerDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
                tbl.Rows.Clear();

            }

        }
        //Save Data for Previous CED Employer Details
        private void SaveCEDPreviousEmployerData()
        {

            int count = 0;
            int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
            if (Session["CEDPreviousEmployerCount"] != null)
            {
                count = Convert.ToInt32(Session["CEDPreviousEmployerCount"]);
                if (Session["CEDPreviousEmployerList"] != null)
                {
                    List<CEDPreviousEmployer> DriverConvictionList = Session["CEDPreviousEmployerList"] as List<CEDPreviousEmployer>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                var prevRecord = new BLConsumeApi().SelectCEDPreviousEmployerDetailsByAllIds(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.CEDPreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deleteCEDPreviousEmployerDetailId(item.PreviousEmployerDetailId);
                        }
                        //dpfdatabase.CEDPreviousEmployerDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {

                    string str9 = "txt9" + i.ToString();
                    string str10 = "txt10" + i.ToString();
                    string str11 = "txt11" + i.ToString();


                    string txt9 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).Text;
                    //txt9 = DateTime.ParseExact(txt9, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                    string txt10 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).Text;
                    // txt10 = DateTime.ParseExact(txt10, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                    string txt11 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).Text;

                    MlCEDPreviousEmployerDetail cd = new MlCEDPreviousEmployerDetail();

                    cd.EmploymentGapFrom = txt9;
                    cd.EmploymentGapTo = txt10;
                    cd.ReasonForEmploymentGap = txt11;
                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    new BLConsumeApi().insertCedPreviousEmployerDetailId(cd);
                    //dpfdatabase.CEDPreviousEmployerDetails.Add(cd);
                    //dpfdatabase.SaveChanges();
                }
                HtmlTable tbl = (HtmlTable)TgCEDPreviousEmployer1.FindControl("tbl");
                tbl.Rows.Clear();

            }
            else if (Session["CEDPreviousEmployerList"] != null)
            {

                if (Session["CEDPreviousEmployerList"] != null)
                {
                    List<CEDPreviousEmployer> DriverConvictionList = Session["CEDPreviousEmployerList"] as List<CEDPreviousEmployer>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                var prevRecord = new BLConsumeApi().SelectCEDPreviousEmployerDetailsByAllIds(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.CEDPreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deleteCEDPreviousEmployerDetailId(item.PreviousEmployerDetailId);
                        }
                        //dpfdatabase.CEDPreviousEmployerDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {

                    string str9 = "txt9" + i.ToString();
                    string str10 = "txt10" + i.ToString();
                    string str11 = "txt11" + i.ToString();


                    string txt9 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).Text;
                    //  txt9 = DateTime.ParseExact(txt9, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                    string txt10 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).Text;
                    //    txt10 = DateTime.ParseExact(txt10, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                    string txt11 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).Text;

                    MlCEDPreviousEmployerDetail cd = new MlCEDPreviousEmployerDetail();

                    cd.EmploymentGapFrom = txt9;
                    cd.EmploymentGapTo = txt10;
                    cd.ReasonForEmploymentGap = txt11;
                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    new BLConsumeApi().insertCedPreviousEmployerDetailId(cd);

                    //dpfdatabase.CEDPreviousEmployerDetails.Add(cd);
                    // dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)TgCEDPreviousEmployer1.FindControl("tbl");
                tbl.Rows.Clear();

            }

        }
        //Save Data for Previous Employer Details
        private void SaveCurrentResidenceData()
        {

            int count = 0;
            int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
            if (Session["CurrentResidenceCount"] != null)
            {
                count = Convert.ToInt32(Session["CurrentResidenceCount"]);
                if (Session["CurrentResidenceList"] != null)
                {
                    List<CurrentResidence> DriverConvictionList = Session["CurrentResidenceList"] as List<CurrentResidence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectCurrentResidenceDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.CurrentResidenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deleteCurrentResidenceDetail(item.CurrentResidenceDetailID);
                        }
                        //dpfdatabase.CurrentResidenceDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();


                    string txt1 = ((TextBox)TgCurrentResidence1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgCurrentResidence1.FindControl(str2)).Text;
                    string txt3 = ((TextBox)TgCurrentResidence1.FindControl(str3)).Text;
                    string txt4 = ((TextBox)TgCurrentResidence1.FindControl(str4)).Text;
                    string txt5 = ((TextBox)TgCurrentResidence1.FindControl(str5)).Text;


                    MlCurrentResidenceDetails cd = new MlCurrentResidenceDetails();
                    cd.CurrentResidenceStreetAddress = txt1;
                    cd.CurrentResidenceCity = txt2;
                    cd.CurrentResidenceState = txt3;
                    cd.CurrentResidenceZipcode = txt4;
                    cd.CurrentResidenceDuration = txt5;

                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    new BLConsumeApi().insertCurrentResidenceDetail(cd);
                    //dpfdatabase.CurrentResidenceDetails.Add(cd);
                    //dpfdatabase.SaveChanges();
                }
                HtmlTable tbl = (HtmlTable)TgCurrentResidence1.FindControl("tbl");
                tbl.Rows.Clear();

            }
            else if (Session["CurrentResidenceList"] != null)
            {

                if (Session["CurrentResidenceList"] != null)
                {
                    List<CurrentResidence> DriverConvictionList = Session["CurrentResidenceList"] as List<CurrentResidence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectCurrentResidenceDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.CurrentResidenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deleteCurrentResidenceDetail(item.CurrentResidenceDetailID);

                        }
                        //dpfdatabase.CurrentResidenceDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();



                    string txt1 = ((TextBox)TgCurrentResidence1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgCurrentResidence1.FindControl(str2)).Text;
                    string txt3 = ((TextBox)TgCurrentResidence1.FindControl(str3)).Text;
                    string txt4 = ((TextBox)TgCurrentResidence1.FindControl(str4)).Text;
                    string txt5 = ((TextBox)TgCurrentResidence1.FindControl(str5)).Text;


                    MlCurrentResidenceDetails cd = new MlCurrentResidenceDetails();
                    cd.CurrentResidenceStreetAddress = txt1;
                    cd.CurrentResidenceCity = txt2;
                    cd.CurrentResidenceState = txt3;
                    cd.CurrentResidenceZipcode = txt4;
                    cd.CurrentResidenceDuration = txt5;

                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;
                    new BLConsumeApi().insertCurrentResidenceDetail(cd);
                    //dpfdatabase.CurrentResidenceDetails.Add(cd);
                    //dpfdatabase.SaveChanges();
                }
                HtmlTable tbl = (HtmlTable)TgCurrentResidence1.FindControl("tbl");
                tbl.Rows.Clear();
            }

        }
        //Save Data for Previous Employer Details
        private void SavePreviousResidenceData()
        {

            int count = 0;
            int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
            if (Session["PreviousResidenceCount"] != null)
            {
                count = Convert.ToInt32(Session["PreviousResidenceCount"]);
                if (Session["PreviousResidenceList"] != null)
                {
                    List<PreviousResidence> DriverConvictionList = Session["PreviousResidenceList"] as List<PreviousResidence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectMlPreviousResidenceDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.PreviousResidenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deletePreviousResidenceDetail(item.PreviousResidenceDetailID);
                        }

                        //dpfdatabase.PreviousResidenceDetails.RemoveRange(prevRecord);
                        // dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();



                    string txt1 = ((TextBox)TgPreviousResidence1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgPreviousResidence1.FindControl(str2)).Text;
                    string txt3 = ((TextBox)TgPreviousResidence1.FindControl(str3)).Text;
                    string txt4 = ((TextBox)TgPreviousResidence1.FindControl(str4)).Text;
                    string txt5 = ((TextBox)TgPreviousResidence1.FindControl(str5)).Text;


                    MlPreviousResidenceDetail cd = new MlPreviousResidenceDetail();
                    cd.PreviousResidenceStreetAddress = txt1;
                    cd.PreviousResidenceCity = txt2;
                    cd.PreviousResidenceState = txt3;
                    cd.PreviousResidenceZipcode = txt4;
                    cd.PreviousResidenceDuration = txt5;

                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    new BLConsumeApi().insertPreviousResidenceDetail(cd);
                    //dpfdatabase.PreviousResidenceDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)TgPreviousResidence1.FindControl("tbl");
                tbl.Rows.Clear();

            }
            else if (Session["PreviousResidenceList"] != null)
            {

                if (Session["PreviousResidenceList"] != null)
                {
                    List<PreviousResidence> DriverConvictionList = Session["PreviousResidenceList"] as List<PreviousResidence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectMlPreviousResidenceDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.PreviousResidenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deletePreviousResidenceDetail(item.PreviousResidenceDetailID);
                        }
                        //    dpfdatabase.PreviousResidenceDetails.RemoveRange(prevRecord);
                        //    dpfdatabase.SaveChanges();
                        //
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();

                    string txt1 = ((TextBox)TgPreviousResidence1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgPreviousResidence1.FindControl(str2)).Text;
                    string txt3 = ((TextBox)TgPreviousResidence1.FindControl(str3)).Text;
                    string txt4 = ((TextBox)TgPreviousResidence1.FindControl(str4)).Text;
                    string txt5 = ((TextBox)TgPreviousResidence1.FindControl(str5)).Text;


                    MlPreviousResidenceDetail cd = new MlPreviousResidenceDetail();
                    cd.PreviousResidenceStreetAddress = txt1;
                    cd.PreviousResidenceCity = txt2;
                    cd.PreviousResidenceState = txt3;
                    cd.PreviousResidenceZipcode = txt4;
                    cd.PreviousResidenceDuration = txt5;

                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    new BLConsumeApi().insertPreviousResidenceDetail(cd);
                    //dpfdatabase.PreviousResidenceDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)TgPreviousResidence1.FindControl("tbl");
                tbl.Rows.Clear();


            }

        }
        //Save Data for Previous Employer Details
        //private void SaveTypeOfEquipmentResidenceData()
        //{
        //    using (var dpfdatabase = new DQFEntities())
        //    {
        //        int count = 0;
        //        int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
        //        if (Session["TypeOfEquipmentCount"] != null)
        //        {
        //            count = Convert.ToInt32(Session["TypeOfEquipmentCount"]);
        //            if (Session["TypeOfEquipmentList"] != null)
        //            {
        //                List<TypeOfEquipmentClass> DriverConvictionList = Session["TypeOfEquipmentList"] as List<TypeOfEquipmentClass>;
        //                int records = DriverConvictionList.Count;
        //                count = count + records;
        //            }
        //            int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
        //            int UserId = Convert.ToInt32(Session["UserId"]);
        //            int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

        //            var prevRecord = dpfdatabase.TypeOfEquipmentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
        //            if (prevRecord != null)
        //            {
        //                if (prevRecord.Count > 0)
        //                {
        //                    dpfdatabase.TypeOfEquipmentDetails.RemoveRange(prevRecord);
        //                    dpfdatabase.SaveChanges();
        //                }
        //            }

        //            for (int i = 1; i <= count; i++)
        //            {
        //                string str1 = "txt1" + i.ToString();
        //                string str2 = "txt2" + i.ToString();
        //                string str3 = "txt3" + i.ToString();
        //                string str4 = "txt4" + i.ToString();




        //                string txt1 = ((TextBox)TgTypeOfEquipment1.FindControl(str1)).Text;
        //                string txt2 = ((TextBox)TgTypeOfEquipment1.FindControl(str2)).Text;
        //                string txt3 = ((TextBox)TgTypeOfEquipment1.FindControl(str3)).Text;
        //                string txt4 = ((TextBox)TgTypeOfEquipment1.FindControl(str4)).Text;



        //                TypeOfEquipmentDetail cd = new TypeOfEquipmentDetail();
        //                cd.TypeOfEquipment = txt1;
        //                cd.Miles = txt2;
        //                cd.DrivingFrom = txt3;
        //                cd.DrivingTo = txt4;


        //                cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
        //                cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
        //                cd.UserId = Convert.ToInt32(Session["UserId"]);
        //                cd.CountForFaxId = countForFaxId;

        //                dpfdatabase.TypeOfEquipmentDetails.Add(cd);
        //                dpfdatabase.SaveChanges();

        //            }
        //            HtmlTable tbl = (HtmlTable)TgTypeOfEquipment1.FindControl("tbl");
        //            tbl.Rows.Clear();

        //        }
        //        else if (Session["TypeOfEquipmentList"] != null)
        //        {

        //            if (Session["TypeOfEquipmentList"] != null)
        //            {
        //                List<TypeOfEquipmentClass> DriverConvictionList = Session["TypeOfEquipmentList"] as List<TypeOfEquipmentClass>;
        //                int records = DriverConvictionList.Count;
        //                count = count + records;
        //            }
        //            int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
        //            int UserId = Convert.ToInt32(Session["UserId"]);
        //            int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

        //            var prevRecord = dpfdatabase.TypeOfEquipmentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
        //            if (prevRecord != null)
        //            {
        //                if (prevRecord.Count > 0)
        //                {
        //                    dpfdatabase.TypeOfEquipmentDetails.RemoveRange(prevRecord);
        //                    dpfdatabase.SaveChanges();
        //                }
        //            }

        //            for (int i = 1; i <= count; i++)
        //            {
        //                string str1 = "txt1" + i.ToString();
        //                string str2 = "txt2" + i.ToString();
        //                string str3 = "txt3" + i.ToString();
        //                string str4 = "txt4" + i.ToString();




        //                string txt1 = ((TextBox)TgTypeOfEquipment1.FindControl(str1)).Text;
        //                string txt2 = ((TextBox)TgTypeOfEquipment1.FindControl(str2)).Text;
        //                string txt3 = ((TextBox)TgTypeOfEquipment1.FindControl(str3)).Text;
        //                string txt4 = ((TextBox)TgTypeOfEquipment1.FindControl(str4)).Text;



        //                TypeOfEquipmentDetail cd = new TypeOfEquipmentDetail();
        //                cd.TypeOfEquipment = txt1;
        //                cd.Miles = txt2;
        //                cd.DrivingFrom = txt3;
        //                cd.DrivingTo = txt4;


        //                cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
        //                cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
        //                cd.UserId = Convert.ToInt32(Session["UserId"]);
        //                cd.CountForFaxId = countForFaxId;

        //                dpfdatabase.TypeOfEquipmentDetails.Add(cd);
        //                dpfdatabase.SaveChanges();

        //            }
        //            HtmlTable tbl = (HtmlTable)TgTypeOfEquipment1.FindControl("tbl");
        //            tbl.Rows.Clear();


        //        }
        //    }
        //}
        private void SaveTrafficConvictionsData()
        {

            int count = 0;
            int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
            if (Session["TrafficConvictionsCount"] != null)
            {
                count = Convert.ToInt32(Session["TrafficConvictionsCount"]);
                if (Session["TrafficConvictionsList"] != null)
                {
                    List<TrafficConviction> DriverConvictionList = Session["TrafficConvictionsList"] as List<TrafficConviction>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                var prevRecord = new BLConsumeApi().SelectTrafficConvictionsDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                //var prevRecord = dpfdatabase.TrafficConvictionsDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deleteMlTrafficConvictionsDetails(item.TrafficConvictionsDetailID);
                        }
                        //dpfdatabase.TrafficConvictionsDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();



                    string txt1 = ((TextBox)TgTrafficConvictions1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgTrafficConvictions1.FindControl(str2)).Text;
                    string txt3 = ((TextBox)TgTrafficConvictions1.FindControl(str3)).Text;
                    // txt3 = DateTime.ParseExact(txt3, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                    string txt4 = ((TextBox)TgTrafficConvictions1.FindControl(str4)).Text;
                    string txt5 = ((TextBox)TgTrafficConvictions1.FindControl(str5)).Text;


                    MlTrafficConvictionsDetails cd = new MlTrafficConvictionsDetails();
                    cd.Location = txt1;
                    cd.VehicleType = txt2;
                    cd.DateOfConviction = txt3;
                    cd.Charge = txt4;
                    cd.Penalty = txt5;


                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    new BLConsumeApi().insertTrafficConvictionsDetails(cd);

                    //dpfdatabase.TrafficConvictionsDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)TgTrafficConvictions1.FindControl("tbl");
                tbl.Rows.Clear();

            }
            else if (Session["TrafficConvictionsList"] != null)
            {

                if (Session["TrafficConvictionsList"] != null)
                {
                    List<TrafficConviction> DriverConvictionList = Session["TrafficConvictionsList"] as List<TrafficConviction>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);


                var prevRecord = new BLConsumeApi().SelectTrafficConvictionsDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);

                //var prevRecord = dpfdatabase.TrafficConvictionsDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                if (prevRecord != null)
                {
                    if (prevRecord.Count > 0)
                    {
                        foreach (var item in prevRecord)
                        {
                            new BLConsumeApi().deleteMlTrafficConvictionsDetails(item.TrafficConvictionsDetailID);
                        }
                        //dpfdatabase.TrafficConvictionsDetails.RemoveRange(prevRecord);
                        //dpfdatabase.SaveChanges();
                    }
                }

                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();



                    string txt1 = ((TextBox)TgTrafficConvictions1.FindControl(str1)).Text;
                    string txt2 = ((TextBox)TgTrafficConvictions1.FindControl(str2)).Text;
                    string txt3 = ((TextBox)TgTrafficConvictions1.FindControl(str3)).Text;
                    // txt3 = DateTime.ParseExact(txt3, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                    string txt4 = ((TextBox)TgTrafficConvictions1.FindControl(str4)).Text;
                    string txt5 = ((TextBox)TgTrafficConvictions1.FindControl(str5)).Text;


                    MlTrafficConvictionsDetails cd = new MlTrafficConvictionsDetails();
                    cd.Location = txt1;
                    cd.VehicleType = txt2;
                    cd.DateOfConviction = txt3;
                    cd.Charge = txt4;
                    cd.Penalty = txt5;


                    cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    cd.UserId = Convert.ToInt32(Session["UserId"]);
                    cd.CountForFaxId = countForFaxId;

                    new BLConsumeApi().insertTrafficConvictionsDetails(cd);


                    //dpfdatabase.TrafficConvictionsDetails.Add(cd);
                    //dpfdatabase.SaveChanges();

                }
                HtmlTable tbl = (HtmlTable)TgTrafficConvictions1.FindControl("tbl");
                tbl.Rows.Clear();

            }

        }





        #endregion
        #region AddRowsToUserControls
        //Add Row for ConvictionDetails
        private void AddNewRows(HtmlTable tbl, int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

            TextBox txt1 = new TextBox();
            txt1.ID = "txt1" + count.ToString();
            txt1.CssClass += " form-control ";
            txt1.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text1))
            {
                txt1.Text = text1;
                //string converted = DateTime.ParseExact(text1, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                //                   .ToString("yyyy-MM-dd");
                //txt1.Text = converted;
                //txt1.TextMode = TextBoxMode.Date;
            }

            HtmlTableCell tc1 = new HtmlTableCell();
            tc1.Controls.Add(txt1);
            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc1.Controls.Add(hf);


            TextBox txt2 = new TextBox();
            txt2.ID = "txt2" + count.ToString();
            txt2.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text2))
                txt2.Text = text2;
            HtmlTableCell tc2 = new HtmlTableCell();
            tc2.Controls.Add(txt2);

            TextBox txt3 = new TextBox();
            txt3.ID = "txt3" + count.ToString();
            txt3.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text3))
                txt3.Text = text3;
            HtmlTableCell tc3 = new HtmlTableCell();
            tc3.Controls.Add(txt3);

            TextBox txt4 = new TextBox();
            txt4.ID = "txt4" + count.ToString();
            txt4.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text4))
                txt4.Text = text4;
            HtmlTableCell tc4 = new HtmlTableCell();
            tc4.Controls.Add(txt4);

            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);
            rows.Cells.Add(tc4);
            tbl.Rows.Add(rows);

        }

        //Add row for Driver Licence

        private void AddNewRowsToDriverLicence(HtmlTable tbl, int count, string text1 = "", string text2 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

            TextBox txt1 = new TextBox();
            txt1.ID = "txt1" + count.ToString();
            txt1.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text1))
                txt1.Text = text1;

            HtmlTableCell tc1 = new HtmlTableCell();
            tc1.Controls.Add(txt1);
            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc1.Controls.Add(hf);


            TextBox txt2 = new TextBox();
            txt2.ID = "txt2" + count.ToString();
            txt2.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text2))
                txt2.Text = text2;
            HtmlTableCell tc2 = new HtmlTableCell();
            tc2.Controls.Add(txt2);


            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);

            tbl.Rows.Add(rows);

        }

        //Add row for Previous Employeement
        private void AddNewRowsToPreviousEmployeement(HtmlTable tbl, int count, string text1 = "", string text2 = "", string text3 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

            TextBox txt1 = new TextBox();
            txt1.ID = "txt1" + count.ToString();
            txt1.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text1))
                txt1.Text = text1;

            HtmlTableCell tc1 = new HtmlTableCell();
            tc1.Controls.Add(txt1);
            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc1.Controls.Add(hf);


            TextBox txt2 = new TextBox();
            txt2.ID = "txt2" + count.ToString();
            txt2.CssClass += " form-control ";
            txt2.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text2))
                txt2.Text = text2;

            //if (!string.IsNullOrEmpty(text2))
            //{
            //    string converted = DateTime.ParseExact(text2, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            //                       .ToString("yyyy-MM-dd");
            //    txt2.Text = converted;
            //    txt2.TextMode = TextBoxMode.Date;
            //}

            HtmlTableCell tc2 = new HtmlTableCell();
            tc2.Controls.Add(txt2);

            TextBox txt3 = new TextBox();
            txt3.ID = "txt3" + count.ToString();
            txt3.CssClass += " form-control ";
            txt3.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text3))
                txt3.Text = text3;

            //if (!string.IsNullOrEmpty(text3))
            //{
            //    string converted = DateTime.ParseExact(text3, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            //                       .ToString("yyyy-MM-dd");
            //    txt3.Text = converted;
            //    txt3.TextMode = TextBoxMode.Date;
            //}

            HtmlTableCell tc3 = new HtmlTableCell();
            tc3.Controls.Add(txt3);


            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);

            tbl.Rows.Add(rows);

        }


        private void AddNewRowsToPreviousEmployer(HtmlTable tbl, int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

            TextBox txt1 = new TextBox();
            txt1.ID = "txt1" + count.ToString();
            txt1.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text1))
                txt1.Text = text1;

            HtmlTableCell tc1 = new HtmlTableCell();
            tc1.Controls.Add(txt1);
            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc1.Controls.Add(hf);


            TextBox txt2 = new TextBox();
            txt2.ID = "txt2" + count.ToString();
            txt2.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text2))
                txt2.Text = text2;
            HtmlTableCell tc2 = new HtmlTableCell();
            tc2.Controls.Add(txt2);

            TextBox txt3 = new TextBox();
            txt3.ID = "txt3" + count.ToString();
            txt3.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text3))
                txt3.Text = text3;
            HtmlTableCell tc3 = new HtmlTableCell();
            tc3.Controls.Add(txt3);

            TextBox txt4 = new TextBox();
            txt4.ID = "txt4" + count.ToString();
            txt4.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text4))
                txt4.Text = text4;
            HtmlTableCell tc4 = new HtmlTableCell();
            tc4.Controls.Add(txt4);

            TextBox txt5 = new TextBox();
            txt5.ID = "txt5" + count.ToString();
            txt5.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text5))
                txt5.Text = text5;
            HtmlTableCell tc5 = new HtmlTableCell();
            tc5.Controls.Add(txt5);

            TextBox txt6 = new TextBox();
            txt6.ID = "txt6" + count.ToString();
            txt6.CssClass += " form-control-Small ";
            txt6.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text6))
                txt6.Text = text6;
            HtmlTableCell tc6 = new HtmlTableCell();
            tc6.Controls.Add(txt6);

            TextBox txt7 = new TextBox();
            txt7.ID = "txt7" + count.ToString();
            txt7.CssClass += " form-control-Small ";
            txt7.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text7))
                txt7.Text = text7;
            HtmlTableCell tc7 = new HtmlTableCell();
            tc7.Controls.Add(txt7);

            TextBox txt8 = new TextBox();
            txt8.ID = "txt8" + count.ToString();
            txt8.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text8))
                txt8.Text = text8;
            HtmlTableCell tc8 = new HtmlTableCell();
            tc8.Controls.Add(txt8);


            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);
            rows.Cells.Add(tc4);
            rows.Cells.Add(tc5);
            rows.Cells.Add(tc6);
            rows.Cells.Add(tc7);
            rows.Cells.Add(tc8);

            tbl.Rows.Add(rows);

        }


        //Add row for CED Previous Empoyer

        private void AddNewRowsToCEDPreviousEmployer(HtmlTable tbl, int count, string text9 = "", string text10 = "", string text11 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;


            TextBox txt9 = new TextBox();
            txt9.ID = "txt9" + count.ToString();
            txt9.CssClass += " form-control ";
            txt9.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text9))
                txt9.Text = text9;

            //if (!string.IsNullOrEmpty(text9))
            //{
            //    string converted = DateTime.ParseExact(text9, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            //                       .ToString("yyyy-MM-dd");
            //    txt9.Text = converted;
            //    txt9.TextMode = TextBoxMode.Date;
            //}

            HtmlTableCell tc9 = new HtmlTableCell();
            tc9.Controls.Add(txt9);

            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc9.Controls.Add(hf);

            TextBox txt10 = new TextBox();
            txt10.ID = "txt10" + count.ToString();
            txt10.CssClass += " form-control ";
            txt10.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text10))
                txt10.Text = text10;

            //if (!string.IsNullOrEmpty(text10))
            //{
            //    string converted = DateTime.ParseExact(text10, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            //                       .ToString("yyyy-MM-dd");
            //    txt10.Text = converted;
            //    txt10.TextMode = TextBoxMode.Date;
            //}

            HtmlTableCell tc10 = new HtmlTableCell();
            tc10.Controls.Add(txt10);

            TextBox txt11 = new TextBox();
            txt11.ID = "txt11" + count.ToString();
            txt11.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text11))
                txt11.Text = text11;
            HtmlTableCell tc11 = new HtmlTableCell();
            tc11.Controls.Add(txt11);



            rows.Cells.Add(tc9);
            rows.Cells.Add(tc10);
            rows.Cells.Add(tc11);

            tbl.Rows.Add(rows);

        }


        //Add row for Current Residence

        private void AddNewRowsToCurrentResidence(HtmlTable tbl, int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "", string text5 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

            TextBox txt1 = new TextBox();
            txt1.ID = "txt1" + count.ToString();
            txt1.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text1))
                txt1.Text = text1;

            HtmlTableCell tc1 = new HtmlTableCell();
            tc1.Controls.Add(txt1);
            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc1.Controls.Add(hf);


            TextBox txt2 = new TextBox();
            txt2.ID = "txt2" + count.ToString();
            txt2.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text2))
                txt2.Text = text2;
            HtmlTableCell tc2 = new HtmlTableCell();
            tc2.Controls.Add(txt2);

            TextBox txt3 = new TextBox();
            txt3.ID = "txt3" + count.ToString();
            txt3.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text3))
                txt3.Text = text3;
            HtmlTableCell tc3 = new HtmlTableCell();
            tc3.Controls.Add(txt3);

            TextBox txt4 = new TextBox();
            txt4.ID = "txt4" + count.ToString();
            txt4.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text4))
                txt4.Text = text4;
            HtmlTableCell tc4 = new HtmlTableCell();
            tc4.Controls.Add(txt4);

            TextBox txt5 = new TextBox();
            txt5.ID = "txt5" + count.ToString();
            txt5.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text5))
                txt5.Text = text5;
            HtmlTableCell tc5 = new HtmlTableCell();
            tc5.Controls.Add(txt5);



            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);
            rows.Cells.Add(tc4);
            rows.Cells.Add(tc5);


            tbl.Rows.Add(rows);

        }

        //Add row for TypeOf Equipment

        private void AddNewRowsToTypeOfEquipment(HtmlTable tbl, int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

            TextBox txt1 = new TextBox();
            txt1.ID = "txt1" + count.ToString();
            txt1.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text1))
                txt1.Text = text1;

            HtmlTableCell tc1 = new HtmlTableCell();
            tc1.Controls.Add(txt1);
            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc1.Controls.Add(hf);


            TextBox txt2 = new TextBox();
            txt2.ID = "txt2" + count.ToString();
            txt2.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text2))
                txt2.Text = text2;
            HtmlTableCell tc2 = new HtmlTableCell();
            tc2.Controls.Add(txt2);

            TextBox txt3 = new TextBox();
            txt3.ID = "txt3" + count.ToString();
            txt3.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text3))
                txt3.Text = text3;
            HtmlTableCell tc3 = new HtmlTableCell();
            tc3.Controls.Add(txt3);

            TextBox txt4 = new TextBox();
            txt4.ID = "txt4" + count.ToString();
            txt4.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text4))
                txt4.Text = text4;
            HtmlTableCell tc4 = new HtmlTableCell();
            tc4.Controls.Add(txt4);

            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);
            rows.Cells.Add(tc4);
            tbl.Rows.Add(rows);

        }

        //Add row for Previous Residence

        private void AddNewRowsToPreviousResidence(HtmlTable tbl, int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "", string text5 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

            TextBox txt1 = new TextBox();
            txt1.ID = "txt1" + count.ToString();
            txt1.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text1))
                txt1.Text = text1;

            HtmlTableCell tc1 = new HtmlTableCell();
            tc1.Controls.Add(txt1);
            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc1.Controls.Add(hf);


            TextBox txt2 = new TextBox();
            txt2.ID = "txt2" + count.ToString();
            txt2.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text2))
                txt2.Text = text2;
            HtmlTableCell tc2 = new HtmlTableCell();
            tc2.Controls.Add(txt2);

            TextBox txt3 = new TextBox();
            txt3.ID = "txt3" + count.ToString();
            txt3.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text3))
                txt3.Text = text3;
            HtmlTableCell tc3 = new HtmlTableCell();
            tc3.Controls.Add(txt3);

            TextBox txt4 = new TextBox();
            txt4.ID = "txt4" + count.ToString();
            txt4.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text4))
                txt4.Text = text4;
            HtmlTableCell tc4 = new HtmlTableCell();
            tc4.Controls.Add(txt4);

            TextBox txt5 = new TextBox();
            txt5.ID = "txt5" + count.ToString();
            txt5.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text5))
                txt5.Text = text5;
            HtmlTableCell tc5 = new HtmlTableCell();
            tc5.Controls.Add(txt5);



            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);
            rows.Cells.Add(tc4);
            rows.Cells.Add(tc5);


            tbl.Rows.Add(rows);

        }


        private void AddNewRowsToTrafficConviction(HtmlTable tbl, int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "", string text5 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

            TextBox txt1 = new TextBox();
            txt1.ID = "txt1" + count.ToString();
            txt1.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text1))
                txt1.Text = text1;

            HtmlTableCell tc1 = new HtmlTableCell();
            tc1.Controls.Add(txt1);
            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc1.Controls.Add(hf);


            TextBox txt2 = new TextBox();
            txt2.ID = "txt2" + count.ToString();
            txt2.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text2))
                txt2.Text = text2;
            HtmlTableCell tc2 = new HtmlTableCell();
            tc2.Controls.Add(txt2);

            TextBox txt3 = new TextBox();
            txt3.ID = "txt3" + count.ToString();
            txt3.CssClass += " form-control ";
            txt3.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text3))
                txt3.Text = text3;

            //if (!string.IsNullOrEmpty(text3))
            //{
            //    string converted = DateTime.ParseExact(text3, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            //                       .ToString("yyyy-MM-dd");
            //    txt3.Text = converted;
            //    txt3.TextMode = TextBoxMode.Date;
            //}

            HtmlTableCell tc3 = new HtmlTableCell();
            tc3.Controls.Add(txt3);

            TextBox txt4 = new TextBox();
            txt4.ID = "txt4" + count.ToString();
            txt4.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text4))
                txt4.Text = text4;
            HtmlTableCell tc4 = new HtmlTableCell();
            tc4.Controls.Add(txt4);

            TextBox txt5 = new TextBox();
            txt5.ID = "txt5" + count.ToString();
            txt5.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text5))
                txt5.Text = text5;
            HtmlTableCell tc5 = new HtmlTableCell();
            tc5.Controls.Add(txt5);

            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);
            rows.Cells.Add(tc4);
            rows.Cells.Add(tc5);
            tbl.Rows.Add(rows);

        }

        // Add Row For Violation
        private void AddNewRowsViolation(HtmlTable tbl, int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

            TextBox txt1 = new TextBox();
            txt1.ID = "txt1" + count.ToString();
            txt1.CssClass += " form-control ";
            txt1.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text1))
            {
                txt1.Text = text1;
                //string converted = DateTime.ParseExact(text1, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                //                   .ToString("yyyy-MM-dd");
                //txt1.Text = converted;
                //txt1.TextMode = TextBoxMode.Date;
            }

            HtmlTableCell tc1 = new HtmlTableCell();
            tc1.Controls.Add(txt1);
            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc1.Controls.Add(hf);


            TextBox txt2 = new TextBox();
            txt2.ID = "txt2" + count.ToString();
            txt2.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text2))
                txt2.Text = text2;
            HtmlTableCell tc2 = new HtmlTableCell();
            tc2.Controls.Add(txt2);

            TextBox txt3 = new TextBox();
            txt3.ID = "txt3" + count.ToString();
            txt3.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text3))
                txt3.Text = text3;
            HtmlTableCell tc3 = new HtmlTableCell();
            tc3.Controls.Add(txt3);

            TextBox txt4 = new TextBox();
            txt4.ID = "txt4" + count.ToString();
            txt4.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text4))
                txt4.Text = text4;
            HtmlTableCell tc4 = new HtmlTableCell();
            tc4.Controls.Add(txt4);

            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);
            rows.Cells.Add(tc4);
            tbl.Rows.Add(rows);

        }

        private void AddNewRowsToDriverLicenseStatus(HtmlTable tbl, int count, string text1 = "", int ConvictionDetailsId = 0)
        {

            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

            TextBox txt1 = new TextBox();
            txt1.ID = "txt1" + count.ToString();
            txt1.CssClass += " form-control-Small ";
            if (!string.IsNullOrEmpty(text1))
                txt1.Text = text1;

            HtmlTableCell tc1 = new HtmlTableCell();
            tc1.Controls.Add(txt1);
            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count.ToString();
            if (ConvictionDetailsId > 0)
                hf.Value = ConvictionDetailsId.ToString();

            tc1.Controls.Add(hf);

            rows.Cells.Add(tc1);

            tbl.Rows.Add(rows);

        }

        #endregion
        #region LoadUser2DataToUserControl

        private void LoadCurrentEmployerListUser2Data(List<CurrentEmployer> DriverConvictionList, HtmlTable tbl)
        {

            List<CurrentEmployer> PreviousEmployeementListUser2 = Session["CurrentEmployerListUser2"] as List<CurrentEmployer>;
            if (PreviousEmployeementListUser2 != null)
            {
                if (PreviousEmployeementListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (PreviousEmployeementListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].CurrentEmployerName.ToLower() != PreviousEmployeementListUser2[i].CurrentEmployerName.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].CurrentEmployerCity.ToLower() != PreviousEmployeementListUser2[i].CurrentEmployerCity.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].CurrentEmployerState.ToLower() != PreviousEmployeementListUser2[i].CurrentEmployerState.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].CurrentEmployerStreetAddress.ToLower() != PreviousEmployeementListUser2[i].CurrentEmployerStreetAddress.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].CurrentEmployerZipcode.ToLower() != PreviousEmployeementListUser2[i].CurrentEmployerZipcode.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].EmploymentStartDate.ToLower() != PreviousEmployeementListUser2[i].EmploymentStartDate.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void LoadTypeOfEquipmentListUser2Data(List<TypeOfEquipmentClass> DriverConvictionList, HtmlTable tbl)
        {
            List<TypeOfEquipmentClass> DriverConvictionListUser2 = Session["TypeOfEquipmentListUser2"] as List<TypeOfEquipmentClass>;
            if (DriverConvictionListUser2 != null)
            {
                if (DriverConvictionListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (DriverConvictionListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].TypeOfEquipment.ToLower() != DriverConvictionListUser2[i].TypeOfEquipment.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].Miles.ToLower() != DriverConvictionListUser2[i].Miles.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].DrivingFrom.ToLower() != DriverConvictionListUser2[i].DrivingFrom.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].DrivingTo.ToLower() != DriverConvictionListUser2[i].DrivingTo.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }

                        }
                    }
                }
            }
        }

        private void LoadCurrentResidenceListUser2Data(List<CurrentResidence> DriverConvictionList, HtmlTable tbl)
        {
            List<CurrentResidence> PreviousEmployeementListUser2 = Session["CurrentResidenceListUser2"] as List<CurrentResidence>;
            if (PreviousEmployeementListUser2 != null)
            {
                if (PreviousEmployeementListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (PreviousEmployeementListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", " input-validation-error ");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].CurrentResidenceStreetAddress.ToLower() != PreviousEmployeementListUser2[i].CurrentResidenceStreetAddress.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].CurrentResidenceCity.ToLower() != PreviousEmployeementListUser2[i].CurrentResidenceCity.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].CurrentResidenceState.ToLower() != PreviousEmployeementListUser2[i].CurrentResidenceState.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].CurrentResidenceZipcode.ToLower() != PreviousEmployeementListUser2[i].CurrentResidenceZipcode.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].CurrentResidenceDuration.ToLower() != PreviousEmployeementListUser2[i].CurrentResidenceDuration.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }

                        }
                    }
                }
            }
        }

        private void LoadPreviousResidenceListUser2Data(List<PreviousResidence> DriverConvictionList, HtmlTable tbl)
        {
            List<PreviousResidence> PreviousEmployeementListUser2 = Session["PreviousResidenceListUser2"] as List<PreviousResidence>;
            if (PreviousEmployeementListUser2 != null)
            {
                if (PreviousEmployeementListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (PreviousEmployeementListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", " input-validation-error ");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].PreviousResidenceStreetAddress.ToLower() != PreviousEmployeementListUser2[i].PreviousResidenceStreetAddress.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].PreviousResidenceCity.ToLower() != PreviousEmployeementListUser2[i].PreviousResidenceCity.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].PreviousResidenceState.ToLower() != PreviousEmployeementListUser2[i].PreviousResidenceState.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].PreviousResidenceZipcode.ToLower() != PreviousEmployeementListUser2[i].PreviousResidenceZipcode.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].PreviousResidenceDuration.ToLower() != PreviousEmployeementListUser2[i].PreviousResidenceDuration.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }

                        }
                    }
                }
            }
        }

        private void LoadDriverConvictionListUser2Data(List<DriverConviction> DriverConvictionList, HtmlTable tbl)
        {
            List<DriverConviction> DriverConvictionListUser2 = Session["DriverConvictionListUser2"] as List<DriverConviction>;
            if (DriverConvictionListUser2 != null)
            {
                if (DriverConvictionListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (DriverConvictionListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].DateOfConviction.ToLower() != DriverConvictionListUser2[i].DateOfConviction.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].Location.ToLower() != DriverConvictionListUser2[i].Location.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].Offense.ToLower() != DriverConvictionListUser2[i].Offense.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].TypeOfVehicleOperated.ToLower() != DriverConvictionListUser2[i].TypeOfVehicleOperated.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }

                        }
                    }
                }
            }
        }

        private void LoadDriverLicenceListUser2Data(List<DriverLicence> DriverConvictionList, HtmlTable tbl)
        {
            List<DriverLicence> DriverLicenceListUser2 = Session["DriverLicenceListUser2"] as List<DriverLicence>;
            if (DriverLicenceListUser2 != null)
            {
                if (DriverLicenceListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (DriverLicenceListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].Endorsement.ToLower() != DriverLicenceListUser2[i].Endorsement.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].Restriction.ToLower() != DriverLicenceListUser2[i].Restriction.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }

                        }
                    }
                }
            }
        }

        private void LoadPreviousEmployeementListUser2Data(List<PreviousEmployeement> DriverConvictionList, HtmlTable tbl)
        {
            List<PreviousEmployeement> PreviousEmployeementListUser2 = Session["PreviousEmployeementListUser2"] as List<PreviousEmployeement>;
            if (PreviousEmployeementListUser2 != null)
            {
                if (PreviousEmployeementListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (PreviousEmployeementListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].EmployerName.ToLower() != PreviousEmployeementListUser2[i].EmployerName.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].EmploymentStartDate.ToLower() != PreviousEmployeementListUser2[i].EmploymentStartDate.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].EmploymentEndDate.ToLower() != PreviousEmployeementListUser2[i].EmploymentEndDate.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void LoadPreviousEmployerListUser2Data(List<PreviousEmployer> DriverConvictionList, HtmlTable tbl)
        {
            List<PreviousEmployer> PreviousEmployeementListUser2 = Session["PreviousEmployerListUser2"] as List<PreviousEmployer>;
            if (PreviousEmployeementListUser2 != null)
            {
                if (PreviousEmployeementListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (PreviousEmployeementListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].PreviousEmployerName.ToLower() != PreviousEmployeementListUser2[i].PreviousEmployerName.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].PreviousEmployerStreetAddress.ToLower() != PreviousEmployeementListUser2[i].PreviousEmployerStreetAddress.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].EmploymentStartDate.ToLower() != PreviousEmployeementListUser2[i].EmploymentStartDate.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].PreviousEmployerCity.ToLower() != PreviousEmployeementListUser2[i].PreviousEmployerCity.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].PreviousEmployerZipcode.ToLower() != PreviousEmployeementListUser2[i].PreviousEmployerZipcode.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].EmploymentStartDate.ToLower() != PreviousEmployeementListUser2[i].EmploymentStartDate.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].EmploymentEndDate.ToLower() != PreviousEmployeementListUser2[i].EmploymentEndDate.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].ReasonForLeavingPreviousEmployments.ToLower() != PreviousEmployeementListUser2[i].ReasonForLeavingPreviousEmployments.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                        }
                    }
                }
            }

        }

        private void LoadCEDPreviousEmployerListUser2Data(List<CEDPreviousEmployer> DriverConvictionList, HtmlTable tbl)
        {
            List<CEDPreviousEmployer> PreviousEmployeementListUser2 = Session["CEDPreviousEmployerListUser2"] as List<CEDPreviousEmployer>;
            if (PreviousEmployeementListUser2 != null)
            {
                if (PreviousEmployeementListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (PreviousEmployeementListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {

                            if (DriverConvictionList[i].EmploymentGapFrom.ToLower() != PreviousEmployeementListUser2[i].EmploymentGapFrom.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].EmploymentGapTo.ToLower() != PreviousEmployeementListUser2[i].EmploymentGapTo.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                            else if (DriverConvictionList[i].ReasonForEmploymentGap.ToLower() != PreviousEmployeementListUser2[i].ReasonForEmploymentGap.ToLower())
                            {
                                tbl.Attributes.Add("Class", "input-validation-error");
                                break;
                            }
                        }
                    }
                }
            }

        }

        private void LoadDriverLicenseStatusListUser2Data(List<DriverLicenseStatus> DriverConvictionList, HtmlTable tbl)
        {
            List<DriverLicenseStatus> DriverConvictionListUser2 = Session["DriverLicenseStatusListUser2"] as List<DriverLicenseStatus>;
            if (DriverConvictionListUser2 != null)
            {
                if (DriverConvictionListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (DriverConvictionListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].DriverLicenseStatusStatement != DriverConvictionListUser2[i].DriverLicenseStatusStatement)
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }

                        }
                    }
                }
            }
        }

        private void LoadTrafficConvictionListUser2Data(List<TrafficConviction> DriverConvictionList, HtmlTable tbl)
        {
            List<TrafficConviction> DriverConvictionListUser2 = Session["TrafficConvictionsListUser2"] as List<TrafficConviction>;
            if (DriverConvictionListUser2 != null)
            {
                if (DriverConvictionListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (DriverConvictionListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].Location.ToLower() != DriverConvictionListUser2[i].Location.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].VehicleType.ToLower() != DriverConvictionListUser2[i].VehicleType.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].DateOfConviction.ToLower() != DriverConvictionListUser2[i].DateOfConviction.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].Charge.ToLower() != DriverConvictionListUser2[i].Charge.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].Penalty.ToLower() != DriverConvictionListUser2[i].Penalty.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }

                        }
                    }
                }
            }
        }

        private void LoadAccidentRecordListUser2Data(List<AccidentRecord> DriverConvictionList, HtmlTable tbl)
        {
            List<AccidentRecord> DriverConvictionListUser2 = Session["AccidentRecordListUser2"] as List<AccidentRecord>;
            if (DriverConvictionListUser2 != null)
            {
                if (DriverConvictionListUser2.Count > 0 && DriverConvictionList.Count > 0)
                {
                    if (DriverConvictionListUser2.Count != DriverConvictionList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverConvictionList.Count; i++)
                        {
                            if (DriverConvictionList[i].DateOfAccident.ToLower() != DriverConvictionListUser2[i].DateOfAccident.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].NatureOfAccident.ToLower() != DriverConvictionListUser2[i].NatureOfAccident.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].Fatalities.ToLower() != DriverConvictionListUser2[i].Fatalities.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverConvictionList[i].Injuries.ToLower() != DriverConvictionListUser2[i].Injuries.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }

                        }
                    }
                }
            }
        }

        #endregion
        #region BindUserControls

        //private void BindTypeOfEquipmentDetails()
        //{

        //    List<TypeOfEquipmentClass> DriverConvictionList = Session["TypeOfEquipmentList"] as List<TypeOfEquipmentClass>;
        //    HtmlTable tbl = (HtmlTable)TgTypeOfEquipment1.FindControl("tbl");
        //    tbl.Attributes.Add("Class", " table ");
        //    if (DriverConvictionList != null)
        //    {
        //        if (DriverConvictionList.Count > 0)
        //            tbl.Controls.Clear();

        //        HtmlTableRow tr = new HtmlTableRow();
        //        HtmlTableCell t1 = new HtmlTableCell();
        //        t1.InnerText = "Type Of Equipment";
        //        HtmlTableCell t2 = new HtmlTableCell();
        //        t2.InnerText = "Miles";
        //        HtmlTableCell t3 = new HtmlTableCell();
        //        t3.InnerText = "Driving From";
        //        HtmlTableCell t4 = new HtmlTableCell();
        //        t4.InnerText = "Driving To";
        //        tr.Cells.Add(t1);
        //        tr.Cells.Add(t2);
        //        tr.Cells.Add(t3);
        //        tr.Cells.Add(t4);
        //        tr.Attributes.Add("class", " tableth ");
        //        tbl.Rows.Add(tr);
        //        if (Session["DocumentTypeReviewList"] != null)
        //        {
        //            LoadTypeOfEquipmentListUser2Data(DriverConvictionList, tbl);
        //        }

        //        int records = 0;
        //        foreach (var r in DriverConvictionList)
        //        {
        //            records++;
        //            AddNewRowsToTypeOfEquipment(tbl, records, r.TypeOfEquipment, r.Miles, r.DrivingFrom, r.DrivingTo, r.TypeOfEquipmentDetailId);

        //        }
        //    }
        //}

        private void BindViloactionDetails()
        {

            List<DriverVilocation> DriverVilocationList = Session["DriverVilocationList"] as List<DriverVilocation>;
            HtmlTable tbl = (HtmlTable)TgViolationDetails1.FindControl("tbl");
            tbl.Attributes.Add("Class", " table ");
            if (DriverVilocationList != null)
            {
                if (DriverVilocationList.Count > 0)
                    tbl.Controls.Clear();

                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell t1 = new HtmlTableCell();
                t1.InnerText = "Type of Violations";
                HtmlTableCell t2 = new HtmlTableCell();
                t2.InnerText = "Date";
                HtmlTableCell t3 = new HtmlTableCell();
                t3.InnerText = "Location";
                HtmlTableCell t4 = new HtmlTableCell();
                t4.InnerText = "Type Of Vehicle";
                tr.Cells.Add(t1);
                tr.Cells.Add(t2);
                tr.Cells.Add(t3);
                tr.Cells.Add(t4);
                tr.Attributes.Add("class", " tableth ");
                tbl.Rows.Add(tr);
                if (Session["DocumentTypeReviewList"] != null)
                {
                    LoadDriverVilocationListUser2Data(DriverVilocationList, tbl);
                }

                int records = 0;
                foreach (var r in DriverVilocationList)
                {
                    records++;
                    AddNewRowsViolation(tbl, records, r.Typeofviolations, r.Date, r.Location, r.TypeofVehicle, r.VilocationDetailsId);

                }
            }
        }


        private void BindTrafficConvictionDetails()
        {

            List<TrafficConviction> DriverConvictionList = Session["TrafficConvictionsList"] as List<TrafficConviction>;
            HtmlTable tbl = (HtmlTable)TgTrafficConvictions1.FindControl("tbl");
            tbl.Attributes.Add("Class", " table ");
            if (DriverConvictionList != null)
            {
                if (DriverConvictionList.Count > 0)
                    tbl.Controls.Clear();

                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell t1 = new HtmlTableCell();
                t1.InnerText = "Location";
                HtmlTableCell t2 = new HtmlTableCell();
                t2.InnerText = "Vehicle Type";
                HtmlTableCell t3 = new HtmlTableCell();
                t3.InnerText = "Date Of Conviction";
                HtmlTableCell t4 = new HtmlTableCell();
                t4.InnerText = "Charge";
                HtmlTableCell t5 = new HtmlTableCell();
                t5.InnerText = "Penalty";


                tr.Cells.Add(t1);
                tr.Cells.Add(t2);
                tr.Cells.Add(t3);
                tr.Cells.Add(t4);
                tr.Cells.Add(t5);
                tr.Attributes.Add("class", " tableth ");
                tbl.Rows.Add(tr);
                if (Session["DocumentTypeReviewList"] != null)
                {
                    LoadTrafficConvictionListUser2Data(DriverConvictionList, tbl);
                }

                int records = 0;
                foreach (var r in DriverConvictionList)
                {
                    records++;
                    AddNewRowsToTrafficConviction(tbl, records, r.Location, r.VehicleType, r.DateOfConviction, r.Charge, r.Penalty, r.TrafficConvictionsDetailID);

                }
            }
        }

        private void BindCurrentResidence()
        {
            List<CurrentResidence> DriverConvictionList = Session["CurrentResidenceList"] as List<CurrentResidence>;
            HtmlTable tbl = (HtmlTable)TgCurrentResidence1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            if (DriverConvictionList != null)
            {
                if (DriverConvictionList.Count > 0)
                    tbl.Controls.Clear();

                HtmlTableRow tr = new HtmlTableRow();



                HtmlTableCell t1 = new HtmlTableCell();
                t1.InnerText = "Street Address";

                HtmlTableCell t2 = new HtmlTableCell();
                t2.InnerText = "City";

                HtmlTableCell t3 = new HtmlTableCell();
                t3.InnerText = "State";

                HtmlTableCell t4 = new HtmlTableCell();
                t4.InnerText = "Zip";

                HtmlTableCell t5 = new HtmlTableCell();
                t5.InnerText = "Duration";





                tr.Cells.Add(t1);
                tr.Cells.Add(t2);
                tr.Cells.Add(t3);
                tr.Cells.Add(t4);
                tr.Cells.Add(t5);


                tr.Attributes.Add("class", " tableth ");
                tbl.Rows.Add(tr);

                if (Session["DocumentTypeReviewList"] != null)
                {
                    LoadCurrentResidenceListUser2Data(DriverConvictionList, tbl);

                }

                int records = 0;
                foreach (var r in DriverConvictionList)
                {
                    records++;
                    AddNewRowsToCurrentResidence(tbl, records, r.CurrentResidenceStreetAddress, r.CurrentResidenceCity, r.CurrentResidenceState, r.CurrentResidenceZipcode, r.CurrentResidenceDuration, r.CurrentResidenceDetailID);

                }
            }
        }

        private void BindPreviousResidence()
        {
            List<PreviousResidence> DriverConvictionList = Session["PreviousResidenceList"] as List<PreviousResidence>;
            HtmlTable tbl = (HtmlTable)TgPreviousResidence1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            if (DriverConvictionList != null)
            {
                if (DriverConvictionList.Count > 0)
                    tbl.Controls.Clear();

                HtmlTableRow tr = new HtmlTableRow();



                HtmlTableCell t1 = new HtmlTableCell();
                t1.InnerText = "Street Address";

                HtmlTableCell t2 = new HtmlTableCell();
                t2.InnerText = "City";

                HtmlTableCell t3 = new HtmlTableCell();
                t3.InnerText = "State";

                HtmlTableCell t4 = new HtmlTableCell();
                t4.InnerText = "Zip";

                HtmlTableCell t5 = new HtmlTableCell();
                t5.InnerText = "Duration";





                tr.Cells.Add(t1);
                tr.Cells.Add(t2);
                tr.Cells.Add(t3);
                tr.Cells.Add(t4);
                tr.Cells.Add(t5);


                tr.Attributes.Add("class", " tableth ");
                tbl.Rows.Add(tr);

                if (Session["DocumentTypeReviewList"] != null)
                {
                    LoadPreviousResidenceListUser2Data(DriverConvictionList, tbl);

                }

                int records = 0;
                foreach (var r in DriverConvictionList)
                {
                    records++;
                    AddNewRowsToPreviousResidence(tbl, records, r.PreviousResidenceStreetAddress, r.PreviousResidenceCity, r.PreviousResidenceState, r.PreviousResidenceZipcode, r.PreviousResidenceDuration, r.PreviousResidenceDetailID);

                }
            }
        }

        private void BindConvictionDetails()
        {

            List<DriverConviction> DriverConvictionList = Session["DriverConvictionList"] as List<DriverConviction>;
            HtmlTable tbl = (HtmlTable)ConvictionDetails1.FindControl("tbl");
            tbl.Attributes.Add("Class", " table ");
            if (DriverConvictionList != null)
            {
                if (DriverConvictionList.Count > 0)
                    tbl.Controls.Clear();

                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell t1 = new HtmlTableCell();
                t1.InnerText = "Date Of Conviction";
                HtmlTableCell t2 = new HtmlTableCell();
                t2.InnerText = "Offense";
                HtmlTableCell t3 = new HtmlTableCell();
                t3.InnerText = "Location";
                HtmlTableCell t4 = new HtmlTableCell();
                t4.InnerText = "Type Of Vehicle Operated";
                tr.Cells.Add(t1);
                tr.Cells.Add(t2);
                tr.Cells.Add(t3);
                tr.Cells.Add(t4);
                tr.Attributes.Add("class", " tableth ");
                tbl.Rows.Add(tr);
                if (Session["DocumentTypeReviewList"] != null)
                {
                    LoadDriverConvictionListUser2Data(DriverConvictionList, tbl);
                }

                int records = 0;
                foreach (var r in DriverConvictionList)
                {
                    records++;
                    AddNewRows(tbl, records, r.DateOfConviction, r.Offense, r.Location, r.TypeOfVehicleOperated, r.ConvictionDetailsId);

                }
            }
        }

        private void BindDriverLicenceDetails()
        {
            List<DriverLicence> DriverConvictionList = Session["DriverLicenceList"] as List<DriverLicence>;
            HtmlTable tbl = (HtmlTable)TgDriverLicence1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            if (DriverConvictionList != null)
            {
                if (DriverConvictionList.Count > 0)
                    tbl.Controls.Clear();



                HtmlTableRow tr = new HtmlTableRow();

                HtmlTableCell t1 = new HtmlTableCell();
                t1.InnerText = "Restriction";


                HtmlTableCell t2 = new HtmlTableCell();
                t2.InnerText = "Endorsement";


                tr.Cells.Add(t1);
                tr.Cells.Add(t2);

                tr.Attributes.Add("class", " tableth ");
                tbl.Rows.Add(tr);

                if (Session["DocumentTypeReviewList"] != null)
                {
                    LoadDriverLicenceListUser2Data(DriverConvictionList, tbl);

                }

                int records = 0;
                foreach (var r in DriverConvictionList)
                {
                    records++;
                    AddNewRowsToDriverLicence(tbl, records, r.Restriction, r.Endorsement, r.DriverLicenceId);

                }
            }
        }

        private void BindPreviousEmployeement()
        {
            List<PreviousEmployeement> DriverConvictionList = Session["PreviousEmployeementList"] as List<PreviousEmployeement>;
            HtmlTable tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            if (DriverConvictionList != null)
            {
                if (DriverConvictionList.Count > 0)
                    tbl.Controls.Clear();


                HtmlTableRow tr = new HtmlTableRow();

                HtmlTableCell t1 = new HtmlTableCell();
                t1.InnerText = "Employer Name";


                HtmlTableCell t2 = new HtmlTableCell();
                t2.InnerText = "Employment Start Date";

                HtmlTableCell t3 = new HtmlTableCell();
                t3.InnerText = "Employment End Date";


                tr.Cells.Add(t1);
                tr.Cells.Add(t2);
                tr.Cells.Add(t3);

                tr.Attributes.Add("class", " tableth ");
                tbl.Rows.Add(tr);

                if (Session["DocumentTypeReviewList"] != null)
                {
                    LoadPreviousEmployeementListUser2Data(DriverConvictionList, tbl);

                }

                int records = 0;
                foreach (var r in DriverConvictionList)
                {
                    records++;
                    AddNewRowsToPreviousEmployeement(tbl, records, r.EmployerName, r.EmploymentStartDate, r.EmploymentEndDate, r.PreviousEmploymentDetailId);

                }
            }
        }

        private void LoadDriverVilocationListUser2Data(List<DriverVilocation> DriverVilocationList, HtmlTable tbl)
        {
            List<DriverVilocation> DriverVilocationListUser2 = Session["DriverVilocationListUser2"] as List<DriverVilocation>;
            if (DriverVilocationListUser2 != null)
            {
                if (DriverVilocationListUser2.Count > 0 && DriverVilocationList.Count > 0)
                {
                    if (DriverVilocationListUser2.Count != DriverVilocationList.Count)
                    {
                        tbl.Attributes.Add("Class", "input-validation-error");
                    }
                    else
                    {
                        for (int i = 0; i < DriverVilocationList.Count; i++)
                        {
                            if (DriverVilocationList[i].Typeofviolations.ToLower() != DriverVilocationListUser2[i].Typeofviolations.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }
                            else if (DriverVilocationList[i].Date.ToLower() != DriverVilocationListUser2[i].Date.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }

                            else if (DriverVilocationList[i].Location.ToLower() != DriverVilocationListUser2[i].Location.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }

                            else if (DriverVilocationList[i].TypeofVehicle.ToLower() != DriverVilocationListUser2[i].TypeofVehicle.ToLower())
                            {
                                tbl.Attributes.Add("Class", " input-validation-error ");
                                break;
                            }

                        }
                    }
                }
            }
        }


        private void BindPreviousEmployer()
        {
            List<PreviousEmployer> DriverConvictionList = Session["PreviousEmployerList"] as List<PreviousEmployer>;
            HtmlTable tbl = (HtmlTable)TgPreviousEmployer1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            if (DriverConvictionList != null)
            {
                if (DriverConvictionList.Count > 0)
                    tbl.Controls.Clear();

                HtmlTableRow tr = new HtmlTableRow();

                HtmlTableCell t1 = new HtmlTableCell();
                t1.InnerText = "Employer Name";

                HtmlTableCell t2 = new HtmlTableCell();
                t2.InnerText = "Street Address";

                HtmlTableCell t3 = new HtmlTableCell();
                t3.InnerText = "City";

                HtmlTableCell t4 = new HtmlTableCell();
                t4.InnerText = "State";

                HtmlTableCell t5 = new HtmlTableCell();
                t5.InnerText = "Zip";

                HtmlTableCell t6 = new HtmlTableCell();
                t6.InnerText = "Employment Start Date";

                HtmlTableCell t7 = new HtmlTableCell();
                t7.InnerText = "Employment End Date";

                HtmlTableCell t8 = new HtmlTableCell();
                t8.InnerText = "Reason For Leaving";


                tr.Cells.Add(t1);
                tr.Cells.Add(t2);
                tr.Cells.Add(t3);
                tr.Cells.Add(t4);
                tr.Cells.Add(t5);
                tr.Cells.Add(t6);
                tr.Cells.Add(t7);
                tr.Cells.Add(t8);

                tr.Attributes.Add("class", " tableth ");
                tbl.Rows.Add(tr);

                if (Session["DocumentTypeReviewList"] != null)
                {
                    LoadPreviousEmployerListUser2Data(DriverConvictionList, tbl);

                }

                int records = 0;
                foreach (var r in DriverConvictionList)
                {
                    records++;
                    AddNewRowsToPreviousEmployer(tbl, records, r.PreviousEmployerName, r.PreviousEmployerStreetAddress, r.PreviousEmployerCity, r.PreviousEmployerState, r.PreviousEmployerZipcode, r.EmploymentStartDate, r.EmploymentEndDate, r.ReasonForLeavingPreviousEmployments, r.PreviousEmployerDetailId);

                }
            }
        }


        private void BindCEDPreviousEmployer()
        {
            List<CEDPreviousEmployer> DriverConvictionList = Session["CEDPreviousEmployerList"] as List<CEDPreviousEmployer>;
            HtmlTable tbl = (HtmlTable)TgCEDPreviousEmployer1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            if (DriverConvictionList != null)
            {
                if (DriverConvictionList.Count > 0)
                    tbl.Controls.Clear();

                HtmlTableRow tr = new HtmlTableRow();



                HtmlTableCell t9 = new HtmlTableCell();
                t9.InnerText = "Employment Gap From";

                HtmlTableCell t10 = new HtmlTableCell();
                t10.InnerText = "Employment Gap To";

                HtmlTableCell t11 = new HtmlTableCell();
                t11.InnerText = "Reason For Employment Gap";



                tr.Cells.Add(t9);
                tr.Cells.Add(t10);
                tr.Cells.Add(t11);

                tr.Attributes.Add("class", " tableth ");
                tbl.Rows.Add(tr);

                if (Session["DocumentTypeReviewList"] != null)
                {
                    LoadCEDPreviousEmployerListUser2Data(DriverConvictionList, tbl);

                }

                int records = 0;
                foreach (var r in DriverConvictionList)
                {
                    records++;
                    AddNewRowsToCEDPreviousEmployer(tbl, records, r.EmploymentGapFrom, r.EmploymentGapTo, r.ReasonForEmploymentGap, r.PreviousEmployerDetailId);

                }
            }
        }

        #endregion
        #region OtherSupportedMethods

        private void HideAllUserControl()
        {
            ConvictionDetails1.Visible = false;
            TgDriverLicence1.Visible = false;
            TgPreviousEmployment1.Visible = false;
            TgPreviousEmployer1.Visible = false;

            TgCurrentResidence1.Visible = false;
            TgPreviousResidence1.Visible = false;
            //TgTypeOfEquipment1.Visible = false;

            TgTrafficConvictions1.Visible = false;

            TgCEDPreviousEmployer1.Visible = false;

            HtmlTable tbl = (HtmlTable)TgPreviousEmployer1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            //tbl = (HtmlTable)TgTypeOfEquipment1.FindControl("tbl");
            //tbl.Attributes.Add("class", " table ");

            tbl = (HtmlTable)TgTrafficConvictions1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");

            tbl = (HtmlTable)TgCurrentResidence1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            tbl = (HtmlTable)TgPreviousResidence1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            tbl = (HtmlTable)ConvictionDetails1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            tbl = (HtmlTable)TgDriverLicence1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");
            tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");

            tbl = (HtmlTable)TgCEDPreviousEmployer1.FindControl("tbl");
            tbl.Attributes.Add("class", " table ");

        }
        private void PopulateUserControls(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            HideAllUserControl();
            //if (DocumentTypeId == 51 || DocumentTypeId == 60)
            var GetCertificateOfViolationDocID = new BLConsumeApi().GetCertificateOfViolationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.ConvictionDetails");
            if (GetCertificateOfViolationDocID != null)
            {
                foreach (var item in GetCertificateOfViolationDocID)
                {


                    if (DocumentTypeId == Convert.ToInt32(item.DocumentTypeID))
                    {
                        ConvictionDetails1.Visible = true;
                        if (Session["DocumentTypeReviewList"] != null)
                        {
                            ConvictionDetails1.BindConvictionDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                            BindConvictionDetails();
                        }
                    }
                }
            }

            var GetViolationForMclane = new BLConsumeApi().SPGetDocumentTypeMotorVehicleReport(Convert.ToInt32(Session["CustomerID"]), "dbo.ViolationDetails");
            if (GetViolationForMclane != null)
            {
                foreach (var item in GetViolationForMclane)
                {

                    if (DocumentTypeId == Convert.ToInt32(item.DocumentTypeID))
                    {

                        TgViolationDetails1.Visible = true;
                        if (Session["DocumentTypeReviewList"] != null)
                        {
                            TgViolationDetails1.BindViolationDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                            BindViloactionDetails();
                        }
                    }
                }
            }


            var GetViolationForfedEx = new BLConsumeApi().SPGetDocumentTypeMotorVehicleReport(Convert.ToInt32(Session["CustomerID"]), "dbo.ConvictionDetails");

            if (GetViolationForfedEx != null)
            {
                foreach (var item in GetViolationForfedEx)
                {

                    if (DocumentTypeId == Convert.ToInt32(item.DocumentTypeID))
                    {
                        ConvictionDetails1.Visible = true;
                        if (Session["DocumentTypeReviewList"] != null)
                        {
                            ConvictionDetails1.BindConvictionDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                            BindConvictionDetails();
                        }
                    }
                }
            }





            // if (DocumentTypeId == 48 || DocumentTypeId == 57)
            var GetDriversLicenseDocID = new BLConsumeApi().GetDriversLicenseDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.DriverLicenceDetail");
            if (GetDriversLicenseDocID != null)
            {
                foreach (var item in GetDriversLicenseDocID)
                {
                    if (DocumentTypeId == Convert.ToInt32(item.DocumentTypeID))
                    {
                        TgDriverLicence1.Visible = true;
                        if (Session["DocumentTypeReviewList"] != null)
                        {
                            TgDriverLicence1.BindDriverLicenceDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                            BindDriverLicenceDetails();
                        }
                    }
                }
            }

            // if (DocumentTypeId == 49 || DocumentTypeId == 58)
            var GetPreviousEmploymentDocID = new BLConsumeApi().GetPreviousEmploymentDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.PreviousEmploymentDetail");
            if (GetPreviousEmploymentDocID != null)
            {
                foreach (var item in GetPreviousEmploymentDocID)
                {
                    if (DocumentTypeId == Convert.ToInt32(item.DocumentTypeID))
                    {
                        TgPreviousEmployment1.Visible = true;
                        if (Session["DocumentTypeReviewList"] != null)
                        {
                            TgPreviousEmployment1.BindPreviousEmployeementDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                            BindPreviousEmployeement();
                        }
                    }
                }
            }
            // if (DocumentTypeId == 59 || DocumentTypeId == 50)
            var GetDriverApplicationDocID = new BLConsumeApi().GetDriverApplicationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.PreviousEmployerDetail");
            if (GetDriverApplicationDocID != null)
            {
                foreach (var item in GetDriverApplicationDocID)
                {
                    if (DocumentTypeId == Convert.ToInt32(item.DocumentTypeID))
                    {
                        TgPreviousEmployer1.Visible = true;
                        if (Session["DocumentTypeReviewList"] != null)
                        {
                            TgPreviousEmployer1.BindPreviousEmployerDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                            BindPreviousEmployer();
                        }
                    }
                }
            }
            var GetDriverApplicationDocIDs = new BLConsumeApi().GetDriverApplicationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.CEDPreviousEmployerDetail");
            if (GetDriverApplicationDocIDs != null)
            {
                foreach (var item in GetDriverApplicationDocIDs)
                {
                    if (DocumentTypeId == Convert.ToInt32(item.DocumentTypeID))
                    {
                        TgCEDPreviousEmployer1.Visible = true;
                        if (Session["DocumentTypeReviewList"] != null)
                        {
                            TgCEDPreviousEmployer1.BindCEDPreviousEmployerDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                            BindCEDPreviousEmployer();
                        }
                    }
                }
            }
            //if (DocumentTypeId == 50 || DocumentTypeId == 59)
            //{
            //    TgCurrentEmployer1.Visible = true;
            //    if (Session["DocumentTypeReviewList"] != null)
            //    {
            //        TgCurrentEmployer1.BindCurrentEmployerDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
            //        BindCurrentEmployer();
            //    }
            //}
            var GetDriverApplicationDocIDss = new BLConsumeApi().GetDriverApplicationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.CurrentResidenceDetail");
            if (GetDriverApplicationDocIDss != null)
            {
                foreach (var item in GetDriverApplicationDocIDss)
                {
                    if (DocumentTypeId == Convert.ToInt32(item.DocumentTypeID))
                    {
                        TgCurrentResidence1.Visible = true;
                        if (Session["DocumentTypeReviewList"] != null)
                        {
                            TgCurrentResidence1.BindCurrentResidenceDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                            BindCurrentResidence();
                        }
                    }
                }
            }
            var GetDriverApplicationDocI = new BLConsumeApi().GetDriverApplicationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.PreviousResidenceDetail");
            if (GetDriverApplicationDocI != null)
            {
                foreach (var item in GetDriverApplicationDocI)
                {
                    if (DocumentTypeId == Convert.ToInt32(item.DocumentTypeID))
                    {
                        TgPreviousResidence1.Visible = true;
                        if (Session["DocumentTypeReviewList"] != null)
                        {
                            TgPreviousResidence1.BindPreviousResidenceDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                            BindPreviousResidence();
                        }
                    }
                }
            }
            //if (DocumentTypeId == 50 || DocumentTypeId == 59)
            //{
            //    TgTypeOfEquipment1.Visible = true;
            //    if (Session["DocumentTypeReviewList"] != null)
            //    {
            //        TgTypeOfEquipment1.BindTypeOfEquipmentEmployerDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
            //        BindTypeOfEquipmentDetails();
            //    }
            //}
            var GetDriverApplicationDoc = new BLConsumeApi().GetDriverApplicationDocID(Convert.ToInt32(Session["CustomerID"]), "dbo.TrafficConvictionsDetail");
            if (GetDriverApplicationDoc != null)
            {
                foreach (var item in GetDriverApplicationDoc)
                {
                    if (DocumentTypeId == Convert.ToInt32(item.DocumentTypeID))
                    {
                        TgTrafficConvictions1.Visible = true;
                        if (Session["DocumentTypeReviewList"] != null)
                        {
                            TgTrafficConvictions1.BindTrafficConvictionsDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                            BindTrafficConvictionDetails();
                        }
                    }
                }
            }

        }



        private void BindDynamicControlRepeaterByDocumentTypeId(int DocumentTypeId, int CountFaxId = 0)
        {
            try
            {

                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Request.QueryString["UserId"]);
                DynamicControlResponseList list = new DynamicControlResponseList();
                //var result = db.DynamicControls.Where(x => x.DocumentTypeID == DocumentTypeId && x.IsActive == true).Select(x => new DynamicControlResponse { ControlName = x.ControlName, ControlType = x.ControlType, DocumentTypeID = x.DocumentTypeID, DynamicControlID = x.DynamicControlID, labelName = x.labelName, DynamicControlValueText = "", DropDownValue = x.DropDownValue, OrderBy = x.OrderBy }).OrderBy(x => x.OrderBy).ToList();

                var result = new BLConsumeApi().BindDynamicControlRepeaterByDocumentTypeId(DocumentTypeId, 0);

                PopulateUserControls(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                if (Session["DocumentTypeReviewList"] != null)
                {

                    result = PopulateDataFroUpdate(result, UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                }

                rptDynamicControl.DataSource = result;
                rptDynamicControl.DataBind();
                hfRepeaterBound.Value = "1";

                //var documentTypeEntry = db.DocumentTypeEntryDetails.Where(x => x.UserId == UserId && x.DocumentTypeId == DocumentTypeId && x.TempCPScreenDataID == TempCPScreenDataID).OrderBy(x => x.CountForFaxId).ToList();
                var documentTypeEntry = new BLConsumeApi().DocumentTypeEntryDetailByUserIdDocumentTypeId(UserId, DocumentTypeId, TempCPScreenDataID);
                // txtStartPage.Text = "1";
                btnNext.Visible = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private List<DynamicControlResponse> PopulateDataFroUpdate(List<DynamicControlResponse> result, int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId = 0)
        {


            //var DynamicControlValueList = db.Exception_FormData.Where(x => x.UserId == UserId && x.CPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId).ToList();

            var DynamicControlValueList = new BLConsumeApi().qcDynamicControlValueList(UserId, TempCPScreenDataID, CountFaxId);

            var CountResult = (from dc in result
                               join
                                    dcv in DynamicControlValueList on dc.DynamicControlID equals dcv.DynamicControlID
                               select new { dc.DynamicControlID }).ToList();


            foreach (var dc in result)
            {
                foreach (var dcv in DynamicControlValueList)
                {
                    if (dc.DynamicControlID == dcv.DynamicControlID)
                    {
                        dc.DynamicControlValueText = dcv.DynamicControlValueText;
                        dc.DyanamicControlValueID = dcv.FormDataID;
                    }
                }
            }

            foreach (var dc in result)
            {
                foreach (var dcv in dropDownValueList)
                {
                    if (dc.DynamicControlID == dcv.DyanamicControlID)
                    {

                        if (dcv.DynamicControlValueId > 0)
                        {
                            dc.DynamicControlValueText = dcv.Text;
                            dc.DyanamicControlValueID = dcv.DynamicControlValueId;

                        }

                    }
                }
            }

            if (CountResult.Count > 0)
            {

                if (hfDbOperation.Value != "Update")
                {
                    hfDbOperation.Value = "Update";
                    //var dynamicControlIdForLocation = db.DynamicControls.Where(x => x.DocumentTypeID == DocumentTypeId && x.ControlName == "txtLocationName").SingleOrDefault();
                    var dynamicControlIdForLocation = new BLConsumeApi().DynamicControlByDocumentTypeId(DocumentTypeId, "txtLocationName");
                    var LocationDetails = result.Where(x => x.ControlName == "txtLocationName").Single();
                    if (LocationDetails != null)
                    {
                        if (!string.IsNullOrEmpty(LocationDetails.DynamicControlValueText))
                        {
                            ddlLocation.ClearSelection();

                            //  ddlLocation.Items.FindByText(LocationDetails.DynamicControlValueText).Selected = true;
                            ddlLocation.SelectedValue = LocationDetails.DynamicControlValueText;
                            hfLocationId.Value = Convert.ToString(LocationDetails.DyanamicControlValueID);
                        }
                    }
                    var startPageData = result.Where(x => x.ControlName == "txtStartPage").Single();
                    txtStartPage.Text = Convert.ToString(startPageData.DynamicControlValueText);
                    hfStartPageId.Value = Convert.ToString(startPageData.DyanamicControlValueID);
                    var endPageData = result.Where(x => x.ControlName == "txtEndPage").Single();
                    txtEndPage.Text = Convert.ToString(endPageData.DynamicControlValueText);
                    hfEndPageId.Value = Convert.ToString(endPageData.DyanamicControlValueID);
                }
            }
            return result;
        }

        private void ManagerUserControl()
        {
            HideAllUserControl();

            Session["DriverConvictionList"] = null;
            Session["ConvictionDetails"] = null;
            Session["DriverConvictionListUser2"] = null;

            Session["DriverLicenceCount"] = null;
            Session["DriverLicenceList"] = null;
            Session["DriverLicenceListUser2"] = null;

            Session["PreviousEmployeementList"] = null;
            Session["PreviousEmployeementCount"] = null;
            Session["PreviousEmployeementListUser2"] = null;

            Session["PreviousEmployerList"] = null;
            Session["PreviousEmployerCount"] = null;
            Session["PreviousEmployerListUser2"] = null;

            Session["CurrentResidenceList"] = null;
            Session["CurrentResidenceListUser2"] = null;
            Session["CurrentResidenceCount"] = null;

            Session["AccidentRecordList"] = null;
            Session["AccidentRecordListUser2"] = null;
            Session["AccidentRecordCount"] = null;

            Session["CurrentEmployerList"] = null;
            Session["CurrentEmployerListUser2"] = null;
            Session["CurrentEmployerCount"] = null;

            Session["DriverLicenseStatusList"] = null;
            Session["DriverLicenseStatusListUser2"] = null;
            Session["DriverLicenseStatusCount"] = null;


            Session["DriverLicenseStatusList"] = null;
            Session["DriverLicenseStatusListUser2"] = null;
            Session["DriverLicenseStatusCount"] = null;

            Session["PreviousResidenceList"] = null;
            Session["PreviousResidenceListUser2"] = null;
            Session["PreviousResidenceCount"] = null;

            Session["TrafficConvictionsList"] = null;
            Session["TrafficConvictionsListUser2"] = null;
            Session["TrafficConvictionsCount"] = null;

            Session["TypeOfEquipmentList"] = null;
            Session["TypeOfEquipmentListUser2"] = null;
            Session["TypeOfEquipmentCount"] = null;

            Session["CEDPreviousEmployerList"] = null;
            Session["CEDPreviousEmployerListUser2"] = null;
            Session["CEDPreviousEmployerCount"] = null;



        }
        private void DefaultValue()
        {
            try
            {
                ddlCustomer.SelectedIndex = 0;
                ddlDocumentType.Items.Clear();
                ddlLocation.Items.Clear();
                //ddlDocumentType.DataSource = null;
                //ddlDocumentType.DataBind();
                //ddlLocation.DataSource = null;
                //ddlLocation.DataBind();
                rptDynamicControl.DataSource = null;
                rptDynamicControl.DataSourceID = null;
                rptDynamicControl.DataBind();
                // txtStartPage.Text = string.Empty;
                txtEndPage.Text = string.Empty;
                ddlDocumentType.Enabled = false;
                ddlLocation.Enabled = false;
                ddlLocation.CssClass = ddlLocation.CssClass.Replace("input-validation-error", "");
                txtStartPage.CssClass = txtStartPage.CssClass.Replace("input-validation-error", "");
                txtEndPage.CssClass = txtEndPage.CssClass.Replace("input-validation-error", "");

                btnNext.Visible = false;
                //dvLocation.Visible = false;
                // dvDocumentType.Visible = false;
                dvSubmit.Visible = false;
                dvMainContainer.Visible = true;
                hfRepeaterBound.Value = "0";
                lblLocatoinError.Visible = false;
                Session["SubmittedTaskByOtherUser"] = null;
                Session["TableResult"] = null;

                ManagerUserControl();

                txtEndPage.Enabled = true;
                // ddlDocumentType.Enabled = true;
                ddlCustomer.Enabled = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }


        #endregion
        #region ValidateUserControl

        private bool ValidateConvictionDetailsData()
        {
            int countError = 0;
            int count = 0;
            if (Session["ConvictionDetails"] != null)
            {
                count = Convert.ToInt32(Session["ConvictionDetails"]);
                if (Session["DriverConvictionList"] != null)
                {
                    List<DriverConviction> DriverConvictionList = Session["DriverConvictionList"] as List<DriverConviction>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();

                    if (string.IsNullOrEmpty(((TextBox)ConvictionDetails1.FindControl(str1)).Text))
                    {
                        ((TextBox)ConvictionDetails1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)ConvictionDetails1.FindControl(str1)).CssClass = ((TextBox)ConvictionDetails1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)ConvictionDetails1.FindControl(str2)).Text))
                    {
                        ((TextBox)ConvictionDetails1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)ConvictionDetails1.FindControl(str2)).CssClass = ((TextBox)ConvictionDetails1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)ConvictionDetails1.FindControl(str3)).Text))
                    {
                        ((TextBox)ConvictionDetails1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)ConvictionDetails1.FindControl(str3)).CssClass = ((TextBox)ConvictionDetails1.FindControl(str3)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)ConvictionDetails1.FindControl(str4)).Text))
                    {
                        ((TextBox)ConvictionDetails1.FindControl(str4)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)ConvictionDetails1.FindControl(str4)).CssClass = ((TextBox)ConvictionDetails1.FindControl(str4)).CssClass.Replace("input-validation-error", "");


                }


            }
            else if (Session["DriverConvictionList"] != null)
            {

                if (Session["DriverConvictionList"] != null)
                {
                    List<DriverConviction> DriverConvictionList = Session["DriverConvictionList"] as List<DriverConviction>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    if (string.IsNullOrEmpty(((TextBox)ConvictionDetails1.FindControl(str1)).Text))
                    {
                        ((TextBox)ConvictionDetails1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)ConvictionDetails1.FindControl(str1)).CssClass = ((TextBox)ConvictionDetails1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)ConvictionDetails1.FindControl(str2)).Text))
                    {
                        ((TextBox)ConvictionDetails1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)ConvictionDetails1.FindControl(str2)).CssClass = ((TextBox)ConvictionDetails1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)ConvictionDetails1.FindControl(str3)).Text))
                    {
                        ((TextBox)ConvictionDetails1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)ConvictionDetails1.FindControl(str3)).CssClass = ((TextBox)ConvictionDetails1.FindControl(str3)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)ConvictionDetails1.FindControl(str4)).Text))
                    {
                        ((TextBox)ConvictionDetails1.FindControl(str4)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)ConvictionDetails1.FindControl(str4)).CssClass = ((TextBox)ConvictionDetails1.FindControl(str4)).CssClass.Replace("input-validation-error", "");




                }


            }
            if (countError > 0)
                return false;
            else
                return true;
        }


        private bool ValidatePreviousEmployeementData()
        {
            int countError = 0;
            int count = 0;
            if (Session["PreviousEmployeementCount"] != null)
            {
                count = Convert.ToInt32(Session["PreviousEmployeementCount"]);
                if (Session["PreviousEmployeementList"] != null)
                {
                    List<PreviousEmployeement> DriverConvictionList = Session["PreviousEmployeementList"] as List<PreviousEmployeement>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployment1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgPreviousEmployment1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployment1.FindControl(str1)).CssClass = ((TextBox)TgPreviousEmployment1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployment1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgPreviousEmployment1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployment1.FindControl(str2)).CssClass = ((TextBox)TgPreviousEmployment1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployment1.FindControl(str3)).Text))
                    {
                        ((TextBox)TgPreviousEmployment1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployment1.FindControl(str3)).CssClass = ((TextBox)TgPreviousEmployment1.FindControl(str3)).CssClass.Replace("input-validation-error", "");




                }


            }
            else if (Session["PreviousEmployeementList"] != null)
            {

                if (Session["PreviousEmployeementList"] != null)
                {
                    List<PreviousEmployeement> DriverConvictionList = Session["PreviousEmployeementList"] as List<PreviousEmployeement>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);


                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployment1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgPreviousEmployment1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployment1.FindControl(str1)).CssClass = ((TextBox)TgPreviousEmployment1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployment1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgPreviousEmployment1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployment1.FindControl(str2)).CssClass = ((TextBox)TgPreviousEmployment1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployment1.FindControl(str3)).Text))
                    {
                        ((TextBox)TgPreviousEmployment1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployment1.FindControl(str3)).CssClass = ((TextBox)TgPreviousEmployment1.FindControl(str3)).CssClass.Replace("input-validation-error", "");


                }


            }
            if (countError > 0)
                return false;
            else
                return true;
        }

        private bool ValidatePreviousEmployerData()
        {
            int countError = 0;
            int count = 0;
            if (Session["PreviousEmployerCount"] != null)
            {
                count = Convert.ToInt32(Session["PreviousEmployerCount"]);
                if (Session["PreviousEmployerList"] != null)
                {
                    List<PreviousEmployer> DriverConvictionList = Session["PreviousEmployerList"] as List<PreviousEmployer>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();
                    string str6 = "txt6" + i.ToString();
                    string str7 = "txt7" + i.ToString();
                    string str8 = "txt8" + i.ToString();

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str1)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str2)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str3)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str3)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str3)).CssClass.Replace("input-validation-error", "");



                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str4)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str4)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str4)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str4)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str5)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str5)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str5)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str5)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str6)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str6)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str6)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str6)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str7)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str7)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str7)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str7)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str8)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str8)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str8)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str8)).CssClass.Replace("input-validation-error", "");




                }


            }
            else if (Session["PreviousEmployerList"] != null)
            {

                if (Session["PreviousEmployerList"] != null)
                {
                    List<PreviousEmployer> DriverConvictionList = Session["PreviousEmployerList"] as List<PreviousEmployer>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);


                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();
                    string str6 = "txt6" + i.ToString();
                    string str7 = "txt7" + i.ToString();
                    string str8 = "txt8" + i.ToString();

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str1)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str2)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str3)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str3)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str3)).CssClass.Replace("input-validation-error", "");



                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str4)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str4)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str4)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str4)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str5)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str5)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str5)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str5)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str6)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str6)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str6)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str6)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str7)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str7)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str7)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str7)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousEmployer1.FindControl(str8)).Text))
                    {
                        ((TextBox)TgPreviousEmployer1.FindControl(str8)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousEmployer1.FindControl(str8)).CssClass = ((TextBox)TgPreviousEmployer1.FindControl(str8)).CssClass.Replace("input-validation-error", "");



                }


            }
            if (countError > 0)
                return false;
            else
                return true;
        }


        private bool ValidateCEDPreviousEmployerData()
        {
            int countError = 0;
            int count = 0;
            if (Session["CEDPreviousEmployerCount"] != null)
            {
                count = Convert.ToInt32(Session["CEDPreviousEmployerCount"]);
                if (Session["CEDPreviousEmployerList"] != null)
                {
                    List<CEDPreviousEmployer> DriverConvictionList = Session["CEDPreviousEmployerList"] as List<CEDPreviousEmployer>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



                for (int i = 1; i <= count; i++)
                {

                    string str9 = "txt9" + i.ToString();
                    string str10 = "txt10" + i.ToString();
                    string str11 = "txt11" + i.ToString();



                    if (string.IsNullOrEmpty(((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).Text))
                    {
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).CssClass = ((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).Text))
                    {
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).CssClass = ((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).Text))
                    {
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).CssClass = ((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).CssClass.Replace("input-validation-error", "");



                }


            }
            else if (Session["CEDPreviousEmployerList"] != null)
            {

                if (Session["CEDPreviousEmployerList"] != null)
                {
                    List<CEDPreviousEmployer> DriverConvictionList = Session["CEDPreviousEmployerList"] as List<CEDPreviousEmployer>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);


                for (int i = 1; i <= count; i++)
                {

                    string str9 = "txt9" + i.ToString();
                    string str10 = "txt10" + i.ToString();
                    string str11 = "txt11" + i.ToString();



                    if (string.IsNullOrEmpty(((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).Text))
                    {
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).CssClass = ((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).Text))
                    {
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).CssClass = ((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).Text))
                    {
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).CssClass = ((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).CssClass.Replace("input-validation-error", "");

                }


            }
            if (countError > 0)
                return false;
            else
                return true;
        }

        private bool ValidateDriverLicenceData()
        {
            int countError = 0;
            int count = 0;
            if (Session["DriverLicenceCount"] != null)
            {
                count = Convert.ToInt32(Session["DriverLicenceCount"]);
                if (Session["DriverLicenceList"] != null)
                {
                    List<DriverLicence> DriverConvictionList = Session["DriverLicenceList"] as List<DriverLicence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();



                    if (string.IsNullOrEmpty(((TextBox)TgDriverLicence1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgDriverLicence1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgDriverLicence1.FindControl(str1)).CssClass = ((TextBox)TgDriverLicence1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgDriverLicence1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgDriverLicence1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgDriverLicence1.FindControl(str2)).CssClass = ((TextBox)TgDriverLicence1.FindControl(str2)).CssClass.Replace("input-validation-error", "");



                }

            }
            else if (Session["DriverLicenceList"] != null)
            {

                if (Session["DriverLicenceList"] != null)
                {
                    List<DriverLicence> DriverConvictionList = Session["DriverLicenceList"] as List<DriverLicence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();

                    if (string.IsNullOrEmpty(((TextBox)TgDriverLicence1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgDriverLicence1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgDriverLicence1.FindControl(str1)).CssClass = ((TextBox)TgDriverLicence1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgDriverLicence1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgDriverLicence1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgDriverLicence1.FindControl(str2)).CssClass = ((TextBox)TgDriverLicence1.FindControl(str2)).CssClass.Replace("input-validation-error", "");



                }

            }
            if (countError > 0)
                return false;
            else
                return true;
        }



        private bool ValidateCurrentResidenceData()
        {
            int countError = 0;
            int count = 0;
            if (Session["CurrentResidenceCount"] != null)
            {
                count = Convert.ToInt32(Session["CurrentResidenceCount"]);
                if (Session["CurrentResidenceList"] != null)
                {
                    List<CurrentResidence> DriverConvictionList = Session["CurrentResidenceList"] as List<CurrentResidence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();



                    if (string.IsNullOrEmpty(((TextBox)TgCurrentResidence1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgCurrentResidence1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCurrentResidence1.FindControl(str1)).CssClass = ((TextBox)TgCurrentResidence1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgCurrentResidence1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgCurrentResidence1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCurrentResidence1.FindControl(str2)).CssClass = ((TextBox)TgCurrentResidence1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgCurrentResidence1.FindControl(str3)).Text))
                    {
                        ((TextBox)TgCurrentResidence1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCurrentResidence1.FindControl(str3)).CssClass = ((TextBox)TgCurrentResidence1.FindControl(str3)).CssClass.Replace("input-validation-error", "");



                    if (string.IsNullOrEmpty(((TextBox)TgCurrentResidence1.FindControl(str4)).Text))
                    {
                        ((TextBox)TgCurrentResidence1.FindControl(str4)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCurrentResidence1.FindControl(str4)).CssClass = ((TextBox)TgCurrentResidence1.FindControl(str4)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgCurrentResidence1.FindControl(str5)).Text))
                    {
                        ((TextBox)TgCurrentResidence1.FindControl(str5)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCurrentResidence1.FindControl(str5)).CssClass = ((TextBox)TgCurrentResidence1.FindControl(str5)).CssClass.Replace("input-validation-error", "");


                }


            }
            else if (Session["CurrentResidenceList"] != null)
            {

                if (Session["CurrentResidenceList"] != null)
                {
                    List<CurrentResidence> DriverConvictionList = Session["CurrentResidenceList"] as List<CurrentResidence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);


                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();



                    if (string.IsNullOrEmpty(((TextBox)TgCurrentResidence1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgCurrentResidence1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCurrentResidence1.FindControl(str1)).CssClass = ((TextBox)TgCurrentResidence1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgCurrentResidence1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgCurrentResidence1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCurrentResidence1.FindControl(str2)).CssClass = ((TextBox)TgCurrentResidence1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgCurrentResidence1.FindControl(str3)).Text))
                    {
                        ((TextBox)TgCurrentResidence1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCurrentResidence1.FindControl(str3)).CssClass = ((TextBox)TgCurrentResidence1.FindControl(str3)).CssClass.Replace("input-validation-error", "");



                    if (string.IsNullOrEmpty(((TextBox)TgCurrentResidence1.FindControl(str4)).Text))
                    {
                        ((TextBox)TgCurrentResidence1.FindControl(str4)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCurrentResidence1.FindControl(str4)).CssClass = ((TextBox)TgCurrentResidence1.FindControl(str4)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgCurrentResidence1.FindControl(str5)).Text))
                    {
                        ((TextBox)TgCurrentResidence1.FindControl(str5)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgCurrentResidence1.FindControl(str5)).CssClass = ((TextBox)TgCurrentResidence1.FindControl(str5)).CssClass.Replace("input-validation-error", "");


                }


            }
            if (countError > 0)
                return false;
            else
                return true;
        }

        private bool ValidatePreviousResidenceData()
        {
            int countError = 0;
            int count = 0;
            if (Session["PreviousResidenceCount"] != null)
            {
                count = Convert.ToInt32(Session["PreviousResidenceCount"]);
                if (Session["PreviousResidenceList"] != null)
                {
                    List<PreviousResidence> DriverConvictionList = Session["PreviousResidenceList"] as List<PreviousResidence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();



                    if (string.IsNullOrEmpty(((TextBox)TgPreviousResidence1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgPreviousResidence1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousResidence1.FindControl(str1)).CssClass = ((TextBox)TgPreviousResidence1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgPreviousResidence1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgPreviousResidence1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousResidence1.FindControl(str2)).CssClass = ((TextBox)TgPreviousResidence1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousResidence1.FindControl(str3)).Text))
                    {
                        ((TextBox)TgPreviousResidence1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousResidence1.FindControl(str3)).CssClass = ((TextBox)TgPreviousResidence1.FindControl(str3)).CssClass.Replace("input-validation-error", "");



                    if (string.IsNullOrEmpty(((TextBox)TgPreviousResidence1.FindControl(str4)).Text))
                    {
                        ((TextBox)TgPreviousResidence1.FindControl(str4)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousResidence1.FindControl(str4)).CssClass = ((TextBox)TgPreviousResidence1.FindControl(str4)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousResidence1.FindControl(str5)).Text))
                    {
                        ((TextBox)TgPreviousResidence1.FindControl(str5)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousResidence1.FindControl(str5)).CssClass = ((TextBox)TgPreviousResidence1.FindControl(str5)).CssClass.Replace("input-validation-error", "");


                }


            }
            else if (Session["PreviousResidenceList"] != null)
            {

                if (Session["PreviousResidenceList"] != null)
                {
                    List<PreviousResidence> DriverConvictionList = Session["PreviousResidenceList"] as List<PreviousResidence>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);


                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();



                    if (string.IsNullOrEmpty(((TextBox)TgPreviousResidence1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgPreviousResidence1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousResidence1.FindControl(str1)).CssClass = ((TextBox)TgPreviousResidence1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgPreviousResidence1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgPreviousResidence1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousResidence1.FindControl(str2)).CssClass = ((TextBox)TgPreviousResidence1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousResidence1.FindControl(str3)).Text))
                    {
                        ((TextBox)TgPreviousResidence1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousResidence1.FindControl(str3)).CssClass = ((TextBox)TgPreviousResidence1.FindControl(str3)).CssClass.Replace("input-validation-error", "");



                    if (string.IsNullOrEmpty(((TextBox)TgPreviousResidence1.FindControl(str4)).Text))
                    {
                        ((TextBox)TgPreviousResidence1.FindControl(str4)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousResidence1.FindControl(str4)).CssClass = ((TextBox)TgPreviousResidence1.FindControl(str4)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgPreviousResidence1.FindControl(str5)).Text))
                    {
                        ((TextBox)TgPreviousResidence1.FindControl(str5)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgPreviousResidence1.FindControl(str5)).CssClass = ((TextBox)TgPreviousResidence1.FindControl(str5)).CssClass.Replace("input-validation-error", "");


                }


            }
            if (countError > 0)
                return false;
            else
                return true;
        }

        //private bool ValidateTypeOfEquipmentData()
        //{
        //    int countError = 0;
        //    int count = 0;
        //    if (Session["TypeOfEquipmentCount"] != null)
        //    {
        //        count = Convert.ToInt32(Session["TypeOfEquipmentCount"]);
        //        if (Session["TypeOfEquipmentList"] != null)
        //        {
        //            List<TypeOfEquipmentClass> DriverConvictionList = Session["TypeOfEquipmentList"] as List<TypeOfEquipmentClass>;
        //            int records = DriverConvictionList.Count;
        //            count = count + records;
        //        }
        //        int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
        //        int UserId = Convert.ToInt32(Session["UserId"]);
        //        int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



        //        for (int i = 1; i <= count; i++)
        //        {
        //            string str1 = "txt1" + i.ToString();
        //            string str2 = "txt2" + i.ToString();
        //            string str3 = "txt3" + i.ToString();
        //            string str4 = "txt4" + i.ToString();




        //            if (string.IsNullOrEmpty(((TextBox)TgTypeOfEquipment1.FindControl(str1)).Text))
        //            {
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str1)).CssClass += " input-validation-error ";
        //                countError++;
        //            }
        //            else
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str1)).CssClass = ((TextBox)TgTypeOfEquipment1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


        //            if (string.IsNullOrEmpty(((TextBox)TgTypeOfEquipment1.FindControl(str2)).Text))
        //            {
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str2)).CssClass += " input-validation-error ";
        //                countError++;
        //            }
        //            else
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str2)).CssClass = ((TextBox)TgTypeOfEquipment1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

        //            if (string.IsNullOrEmpty(((TextBox)TgTypeOfEquipment1.FindControl(str3)).Text))
        //            {
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str3)).CssClass += " input-validation-error ";
        //                countError++;
        //            }
        //            else
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str3)).CssClass = ((TextBox)TgTypeOfEquipment1.FindControl(str3)).CssClass.Replace("input-validation-error", "");



        //            if (string.IsNullOrEmpty(((TextBox)TgTypeOfEquipment1.FindControl(str4)).Text))
        //            {
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str4)).CssClass += " input-validation-error ";
        //                countError++;
        //            }
        //            else
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str4)).CssClass = ((TextBox)TgTypeOfEquipment1.FindControl(str4)).CssClass.Replace("input-validation-error", "");


        //        }


        //    }
        //    else if (Session["TypeOfEquipmentList"] != null)
        //    {

        //        if (Session["TypeOfEquipmentList"] != null)
        //        {
        //            List<TypeOfEquipmentClass> DriverConvictionList = Session["TypeOfEquipmentList"] as List<TypeOfEquipmentClass>;
        //            int records = DriverConvictionList.Count;
        //            count = count + records;
        //        }
        //        int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
        //        int UserId = Convert.ToInt32(Session["UserId"]);
        //        int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);


        //        for (int i = 1; i <= count; i++)
        //        {
        //            string str1 = "txt1" + i.ToString();
        //            string str2 = "txt2" + i.ToString();
        //            string str3 = "txt3" + i.ToString();
        //            string str4 = "txt4" + i.ToString();




        //            if (string.IsNullOrEmpty(((TextBox)TgTypeOfEquipment1.FindControl(str1)).Text))
        //            {
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str1)).CssClass += " input-validation-error ";
        //                countError++;
        //            }
        //            else
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str1)).CssClass = ((TextBox)TgTypeOfEquipment1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


        //            if (string.IsNullOrEmpty(((TextBox)TgTypeOfEquipment1.FindControl(str2)).Text))
        //            {
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str2)).CssClass += " input-validation-error ";
        //                countError++;
        //            }
        //            else
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str2)).CssClass = ((TextBox)TgTypeOfEquipment1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

        //            if (string.IsNullOrEmpty(((TextBox)TgTypeOfEquipment1.FindControl(str3)).Text))
        //            {
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str3)).CssClass += " input-validation-error ";
        //                countError++;
        //            }
        //            else
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str3)).CssClass = ((TextBox)TgTypeOfEquipment1.FindControl(str3)).CssClass.Replace("input-validation-error", "");



        //            if (string.IsNullOrEmpty(((TextBox)TgTypeOfEquipment1.FindControl(str4)).Text))
        //            {
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str4)).CssClass += " input-validation-error ";
        //                countError++;
        //            }
        //            else
        //                ((TextBox)TgTypeOfEquipment1.FindControl(str4)).CssClass = ((TextBox)TgTypeOfEquipment1.FindControl(str4)).CssClass.Replace("input-validation-error", "");



        //        }


        //    }
        //    if (countError > 0)
        //        return false;
        //    else
        //        return true;
        //}

        private bool ValidateTrafficConvictionsData()
        {
            int countError = 0;
            int count = 0;
            if (Session["TrafficConvictionsCount"] != null)
            {
                count = Convert.ToInt32(Session["TrafficConvictionsCount"]);
                if (Session["TrafficConvictionsList"] != null)
                {
                    List<TrafficConviction> DriverConvictionList = Session["TrafficConvictionsList"] as List<TrafficConviction>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);



                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();



                    if (string.IsNullOrEmpty(((TextBox)TgTrafficConvictions1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgTrafficConvictions1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgTrafficConvictions1.FindControl(str1)).CssClass = ((TextBox)TgTrafficConvictions1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgTrafficConvictions1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgTrafficConvictions1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgTrafficConvictions1.FindControl(str2)).CssClass = ((TextBox)TgTrafficConvictions1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgTrafficConvictions1.FindControl(str3)).Text))
                    {
                        ((TextBox)TgTrafficConvictions1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgTrafficConvictions1.FindControl(str3)).CssClass = ((TextBox)TgTrafficConvictions1.FindControl(str3)).CssClass.Replace("input-validation-error", "");



                    if (string.IsNullOrEmpty(((TextBox)TgTrafficConvictions1.FindControl(str4)).Text))
                    {
                        ((TextBox)TgTrafficConvictions1.FindControl(str4)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgTrafficConvictions1.FindControl(str4)).CssClass = ((TextBox)TgTrafficConvictions1.FindControl(str4)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgTrafficConvictions1.FindControl(str5)).Text))
                    {
                        ((TextBox)TgTrafficConvictions1.FindControl(str5)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgTrafficConvictions1.FindControl(str5)).CssClass = ((TextBox)TgTrafficConvictions1.FindControl(str5)).CssClass.Replace("input-validation-error", "");


                }


            }
            else if (Session["TrafficConvictionsList"] != null)
            {

                if (Session["TrafficConvictionsList"] != null)
                {
                    List<TrafficConviction> DriverConvictionList = Session["TrafficConvictionsList"] as List<TrafficConviction>;
                    int records = DriverConvictionList.Count;
                    count = count + records;
                }
                int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                int UserId = Convert.ToInt32(Session["UserId"]);
                int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);


                for (int i = 1; i <= count; i++)
                {
                    string str1 = "txt1" + i.ToString();
                    string str2 = "txt2" + i.ToString();
                    string str3 = "txt3" + i.ToString();
                    string str4 = "txt4" + i.ToString();
                    string str5 = "txt5" + i.ToString();



                    if (string.IsNullOrEmpty(((TextBox)TgTrafficConvictions1.FindControl(str1)).Text))
                    {
                        ((TextBox)TgTrafficConvictions1.FindControl(str1)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgTrafficConvictions1.FindControl(str1)).CssClass = ((TextBox)TgTrafficConvictions1.FindControl(str1)).CssClass.Replace("input-validation-error", "");


                    if (string.IsNullOrEmpty(((TextBox)TgTrafficConvictions1.FindControl(str2)).Text))
                    {
                        ((TextBox)TgTrafficConvictions1.FindControl(str2)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgTrafficConvictions1.FindControl(str2)).CssClass = ((TextBox)TgTrafficConvictions1.FindControl(str2)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgTrafficConvictions1.FindControl(str3)).Text))
                    {
                        ((TextBox)TgTrafficConvictions1.FindControl(str3)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgTrafficConvictions1.FindControl(str3)).CssClass = ((TextBox)TgTrafficConvictions1.FindControl(str3)).CssClass.Replace("input-validation-error", "");



                    if (string.IsNullOrEmpty(((TextBox)TgTrafficConvictions1.FindControl(str4)).Text))
                    {
                        ((TextBox)TgTrafficConvictions1.FindControl(str4)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgTrafficConvictions1.FindControl(str4)).CssClass = ((TextBox)TgTrafficConvictions1.FindControl(str4)).CssClass.Replace("input-validation-error", "");

                    if (string.IsNullOrEmpty(((TextBox)TgTrafficConvictions1.FindControl(str5)).Text))
                    {
                        ((TextBox)TgTrafficConvictions1.FindControl(str5)).CssClass += " input-validation-error ";
                        countError++;
                    }
                    else
                        ((TextBox)TgTrafficConvictions1.FindControl(str5)).CssClass = ((TextBox)TgTrafficConvictions1.FindControl(str5)).CssClass.Replace("input-validation-error", "");



                }


            }
            if (countError > 0)
                return false;
            else
                return true;
        }



        #endregion

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefaultLoadForUserChange();
            string FaxId = Request.QueryString["FaxId"];
            string UserId = "UserId=" + ddlUsers.SelectedValue;
            string url = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("UserId="));
            url = url + UserId;
            Response.Redirect(url);
        }
    }
}