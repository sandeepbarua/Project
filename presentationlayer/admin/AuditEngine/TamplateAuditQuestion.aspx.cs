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

namespace PresentationLayer.Admin_New
{
    public partial class TamplateAuditQuestion : System.Web.UI.Page
    {
        BlCustomerDetails cust = new BlCustomerDetails();
        DQFEntities db = new DQFEntities();
        ClsCommon clsCommonObj = new ClsCommon();
        BLConsumeApi objApi = new BLConsumeApi();
        string DynamicControlLabel = string.Empty;
        int DynamicControlId = 0;
        int DocumentId;

        private DataTable dtFuctionList
        {
            get
            {
                if (ViewState["functionList"] == null)
                {
                    return new DataTable();
                }
                return (DataTable)ViewState["functionList"]; ;
            }
            set
            {
                ViewState["functionList"] = value;
            }
        }
        int DummyCompanyID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DummyCompany"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl tabContact = Master.FindControl("AccessUserAuditEngine") as HtmlGenericControl;
            tabContact.Attributes.Add("class", "box_active");
            if (!IsPostBack)
            {
                BindDocumentType(ddlDocumentType);
                DivShowHide.Visible = false;
                if (Request.QueryString["DocumentId"] !=null)
                {
                    ddlDocumentType.SelectedIndex = ddlDocumentType.Items.IndexOf(ddlDocumentType.Items.FindByValue(Request.QueryString["DocumentId"]));
                    BindgridView(DummyCompanyID, Convert.ToInt32(ddlDocumentType.SelectedValue));                    
                }
                Rb_Calculation.Checked = false;
                Rb_Logical.Checked = false;
            }
        }

        private void BindDocumentType(DropDownList ddlDocType)
        {
            ddlDocType.Items.Clear();
            DataTable dtddlCustomerName;
            ListItem li = new ListItem();
            li.Text = "--Template Name--";
            li.Value = "0";
            ddlDocType.Items.Add(li);
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(new BLConsumeApi().getAllDocumentTypeByCustomerId(DummyCompanyID));
            dtddlCustomerName = dt;
            for (int i = 0; i < dtddlCustomerName.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtddlCustomerName.Rows[i]["DocumentTypeID"].ToString();
                li.Text = dtddlCustomerName.Rows[i]["DocumentTypeName"].ToString();
                ddlDocType.Items.Add(li);
            }
            ddlDocType.SelectedIndex = 0;
            ddlDocType.Enabled = true;
            
        }       

        protected void ddlDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDocumentType.SelectedIndex > 0)
            {
                DivShowHide.Visible = false;
                txtExpression.Text = string.Empty;
                ddlSelectFunction.ClearSelection();
                BindgridView(DummyCompanyID, Convert.ToInt32(ddlDocumentType.SelectedValue));
            }
            else
            {
                txtNewAuditQuestion.Text = string.Empty;
                DivShowHide.Visible = false;               
                txtExpression.Text = string.Empty;
                DivShowHide.Visible = false;
                ddlSelectFunction.ClearSelection();
            }
        }

        private void BindgridView(int CustomerId,int DocumentId)
        {
            List<MlAuditQuestion> lstOfAuditQuestion = objApi.Sp_GetAuditQuestion(CustomerId, DocumentId);
            BlAuditQuestion Auditobj = new BlAuditQuestion();          
            ReptUse.DataSource = lstOfAuditQuestion;
            ReptUse.DataBind();
            ViewState["vsUser"] = lstOfAuditQuestion;        
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            HiddenField hd2 = (HiddenField)item.FindControl("HiddenField4");
            txtNewAuditQuestion.Text = Convert.ToString(hd2.Value).Trim();
        }

        protected void OnDelete_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            BlAuditQuestion setData = new BlAuditQuestion();
            MlAuditQuestion Doc = new MlAuditQuestion();
            Doc.AuditQuestionsID = Convert.ToInt32(hd.Value);
            setData.DeleteAuditQuestion(Doc);
            Doc.CustomerID = DummyCompanyID;
            Doc.DocumentID = Convert.ToInt32(ddlDocumentType.SelectedValue);
            BindgridView(DummyCompanyID, Convert.ToInt32(ddlDocumentType.SelectedValue));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string stringtxtDynamicControlID = String.Format("{0}", Request.Form["hiddenDynamicControlID"]);

            MlAuditQuestion Doc = new MlAuditQuestion();
            Doc.AuditQuestion = Convert.ToString(txtEditAuditQuestion.Text);
            Doc.CustomerID = DummyCompanyID;
            Doc.DocumentID = Convert.ToInt32(ddlEditDocumentType.SelectedValue);
            Doc.UserID = Convert.ToInt32(Session["UserId"]);
            Doc.AuditQuestionsID = Convert.ToInt32(stringtxtDynamicControlID);
            BlAuditQuestion setData = new BlAuditQuestion();
            setData.updateAuditQuestion(Doc);
            MlAuditQuestion Doc1 = new MlAuditQuestion();
            Doc1.CustomerID = DummyCompanyID;// Convert.ToInt32(ddlCustomer.SelectedValue);
            Doc1.DocumentID = Convert.ToInt32(ddlDocumentType.SelectedValue);
            BindgridView(Doc1.CustomerID, Doc1.DocumentID);
            DivShowHide.Visible = false;
            txtExpression.Text = string.Empty;

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            DivShowHide.Visible = false;
            txtExpression.Text = string.Empty;        
            txtNewAuditQuestion.Text = string.Empty;
            ddlSelectFunction.ClearSelection();
            Rb_Calculation.Checked = false;
            Rb_Logical.Checked = false;
        }

        protected void BtnCreateuser_Click(object sender, EventArgs e)
        {
            MlAuditQuestion Doc = new MlAuditQuestion();
            Doc.AuditQuestion = Convert.ToString(txtAuditQuestion.Text);
            Doc.DocumentID = Convert.ToInt32(ddlAddDocumentType.SelectedValue);
            Doc.CustomerID = DummyCompanyID;
            Doc.UserID = Convert.ToInt32(Session["UserId"]);
            BlAuditQuestion Aq = new BlAuditQuestion();
            Aq.setAuditQuestion(Doc);
            MlAuditQuestion Doc1 = new MlAuditQuestion();
            Doc1.CustomerID = DummyCompanyID;
            Doc1.DocumentID = Convert.ToInt32(ddlDocumentType.SelectedValue);
            BindgridView(Doc1.CustomerID, Doc1.DocumentID);
        }
        
        protected void Rb_Logical_CheckedChanged(object sender, EventArgs e)
        {
            ddlSelectFunction.Enabled = true;
            dtFuctionList = BlAuditQuestion.ddlFunctionList(ddlSelectFunction, Rb_Logical.Text);
        }
        protected void Rb_Calculation_CheckedChanged(object sender, EventArgs e)
        {
            ddlSelectFunction.Enabled = true;
            dtFuctionList = BlAuditQuestion.ddlFunctionList(ddlSelectFunction, Rb_Calculation.Text);
        }
        protected void ddlSelectFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectFunction.SelectedIndex > 0)
            {
                DivShowHide.Visible = true;
                DataRow[] row = dtFuctionList.Select("Id='" + ddlSelectFunction.SelectedValue + "'");
                if (row.Length > 0)
                {
                    int? noOfParameter = Convert.ToInt32(row[0]["NoOfParameters"]);
                    DataRow[] LabelRows = dtFuctionList.Select("FuntionName='" + ddlSelectFunction.SelectedItem.ToString() + "'");
                    string documentName = ddlDocumentType.SelectedItem.Text;
                    DataTable result = new DataTable();
                    result.Columns.Add("ParameterName", typeof(string));
                    if (noOfParameter.HasValue)
                    {
                        foreach (DataRow dr in LabelRows)
                        {
                            
                             result.Rows.Add(Convert.ToString(dr["LabelName"]));
                            
                        }
                    }
                    rptDynamicControl.DataSource = result;
                    rptDynamicControl.DataBind();
                }
            }
            else
            {
                DivShowHide.Visible = false;
            }

        }
        protected void rptDynamicControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddl_Parameters");
                if (int.TryParse(ddlDocumentType.SelectedValue, out DynamicControlId))
                {
                    clsCommonObj.ddlDynamicControl(ddl, DynamicControlId);
                }

            }
        }
        protected void ReptUse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfValueYouNeed = (HiddenField)e.Item.FindControl("HiddenField1");
                HiddenField hfValueYouNeed2 = (HiddenField)e.Item.FindControl("HiddenField2");

                Repeater innerRepeater = (Repeater)e.Item.FindControl("Reptrule");
                HiddenField hd = (HiddenField)e.Item.FindControl("HiddenField1");
                innerRepeater.DataSource = clsCommonObj.getAuditRule(hfValueYouNeed2.Value, hfValueYouNeed.Value);
                innerRepeater.DataBind();
            }
        }

        protected void BtnSubmitExpression_Click(object sender, EventArgs e)
        {
            try
            {
                string AuditQuention = txtNewAuditQuestion.Text; 
                string RuleExpression = txtExpression.Text.Replace("'", "''");
                string RuleType = ddlSelectFunction.SelectedItem.ToString();
                int DocTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                int DynamicControlId = Convert.ToInt32(Session["DynamicControlId"]);
                string DynamicControlLabel = Convert.ToString(Session["DynamicControlLabel"]);
                int UserId = Convert.ToInt32(Session["UserId"]);                
                BLConsumeApi ObjBLConsumeApi = new BLConsumeApi();
                ObjBLConsumeApi.SpInsertAuditRuleClassification(DummyCompanyID, DocTypeId, AuditQuention, DynamicControlId, RuleExpression, RuleType, DynamicControlLabel, UserId);
                MlAuditQuestion Doc = new MlAuditQuestion();
                Doc.CustomerID = DummyCompanyID;
                Doc.DocumentID = Convert.ToInt32(ddlDocumentType.SelectedValue);
                BindgridView(Doc.CustomerID, Doc.DocumentID);
                DivShowHide.Visible = false;
                txtExpression.Text = string.Empty;
                txtNewAuditQuestion.Text = string.Empty;
                ddlSelectFunction.ClearSelection();
                Rb_Calculation.Checked = false;
                Rb_Logical.Checked = false;

            }
            catch (Exception ex)
            {
                throw new Exception("Create rule failed, Exception Details:" + ex.ToString(), ex);
            }
            finally
            {
            }
        }
        protected void myDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList myDropdown = sender as DropDownList;
            if (myDropdown.SelectedIndex >= 0)
            {
                string SeletedField = ddlSelectFunction.SelectedItem.Text.Trim();
                DataRow[] row = dtFuctionList.Select("FuntionName='" + SeletedField + "'");
                if (row.Length > 0)
                {
                    txtExpression.Text = Convert.ToString(row[0]["Expression"]);
                }
                else
                {
                    txtExpression.Text = string.Empty;
                }
                Session["DynamicControlLabel"] = Convert.ToString(myDropdown.SelectedItem);
                Session["DynamicControlId"] = Convert.ToInt32(myDropdown.SelectedValue);
            }

        }
    }
}