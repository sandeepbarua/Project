using System;
using System.Web.UI.WebControls;
using MiddleLayer;
using DataAccessLayer;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace PresentationLayer
{
    public partial class Dashboard : System.Web.UI.Page
    {
        int docID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BlTaskAssignment taskA = new BlTaskAssignment();
                taskA.getfaxDetails();
                string firstName = Convert.ToString(Session["firstName"]);
                string lastName = Convert.ToString(Session["lastname"]);
                string space = " ";
                string UserName = firstName + space + lastName;
                lblAgentName.Text = UserName;
                //ClsCommon.ddlGetDocType(ddlDoctype);
                lblFxId.Text = Convert.ToString(Config.FaxId);
                if (Config.FaxId == 0)
                {
                    lblNoTaskAssigned.Visible = true;
                    lblNotAvailaible.Visible = true;
                    ddlDoctype.Enabled = false;
                }
                else
                {
                    lblNoTaskAssigned.Visible = false;
                    lblNotAvailaible.Visible = false;
                    ddlDoctype.Enabled = true;
                }


            }
            if (!string.IsNullOrEmpty(hfDllDocType.Value))
            {
                docID = Convert.ToInt32(hfDllDocType.Value);
                Rept.DataSource = new BlFieldGenerate().DynamicControls(docID);
                Rept.DataBind();
            }
        }

        protected void ddlDoctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlCheckTempFieldInfoId checking = new BlCheckTempFieldInfoId();
            MlDynamicControlValue previousData = new MlDynamicControlValue();
            previousData.TempCPScreenDataID = Config.TempCPScreenDataID;
            hfDllDocType.Value = ddlDoctype.SelectedValue;
            docID = Convert.ToInt32(ddlDoctype.SelectedValue);
               List<MlDynamicControlValue> mLTempFileInfo = checking.StoringInSession(previousData);
                Session["MlDynamicControlValue"] = mLTempFileInfo;
                 string field = Convert.ToString(Session["FieldValue"]);
            
            Rept.DataSource = new BlFieldGenerate().DynamicControls(docID);
            Rept.DataBind();
            btnNext.Visible = true;
            Button2.Visible = true;
        }

        protected void Rept_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int i = 0;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfFieldTitle = (HiddenField)e.Item.FindControl("hfFieldTitle");
                Label lbl = new Label();
                lbl.ID = Convert.ToString(i);
                lbl.Text = hfFieldTitle.Value;
                HtmlTableCell tdTitle = (HtmlTableCell)e.Item.FindControl("tdTitle");
                tdTitle.Controls.Add(lbl);
                tdTitle.Align = "right";
                //e.Item.Controls.Add(lbl);

                HiddenField hfFieldId = (HiddenField)e.Item.FindControl("hfFieldId");

                HiddenField hfFieldType = (HiddenField)e.Item.FindControl("hfFieldType");

                HiddenField hfFieldControlId = (HiddenField)e.Item.FindControl("DynamicControlID");

                HtmlTableCell tdField = (HtmlTableCell)e.Item.FindControl("tdField");



                if (hfFieldType.Value == "TextBox")
                {
                    // declare a textbox
                    TextBox t = new TextBox();
                    t.ID = hfFieldId.Value;
                    tdField.Controls.Add(t);
                    tdField.Align = "left";
                    tdField.Width = "300";

                    //e.Item.Controls.Add(t);

                }
                else if (hfFieldType.Value == "CheckBox")
                {
                    // declare a textbox
                    CheckBox t = new CheckBox();
                    t.ID = hfFieldId.Value;
                    //e.Item.Controls.Add(t);
                    tdField.Controls.Add(t);
                }

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            dataEntryToDataBase();
            BlTaskAssignment.updateIsProcessed();
            
                Config.FaxId = 0;
                BlTaskAssignment taskA = new BlTaskAssignment();
                taskA.getfaxDetails();
                lblFxId.Text = Convert.ToString(Config.FaxId);
               
                if (Config.FaxId == 0)
                {
                    lblNoTaskAssigned.Visible = true;
                    lblNotAvailaible.Visible = true;
                    ddlDoctype.Enabled = false;
                    btnNext.Visible = false;
                    Button2.Visible = false;
                }
                else
                {
                    lblNoTaskAssigned.Visible = false;
                    lblNotAvailaible.Visible = false;
                    ddlDoctype.Enabled = true;
                    btnNext.Visible = true;
                    Button2.Visible = true;
                }
            
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            dataEntryToDataBase();
            
            resetAll();


            btnNext.Visible = false;
            Button2.Visible = false;
            if (Config.FaxId == 0)
            {
                lblNoTaskAssigned.Visible = true;
                lblNotAvailaible.Visible = true;
                ddlDoctype.Enabled = false;
            }
            else
            {
                lblNoTaskAssigned.Visible = false;
                lblNotAvailaible.Visible = false;
                ddlDoctype.Enabled = true;
            }

        }
        public void resetAll()
        {
            ddlDoctype.ClearSelection();
            Rept.DataSource = new BlFieldGenerate().DynamicControls(0);
            Rept.DataBind();
            if (Config.FaxId == 0)
            {
                lblNoTaskAssigned.Visible = true;
                lblNotAvailaible.Visible = true;
            }
            else
            {
                lblNoTaskAssigned.Visible = false;
                lblNotAvailaible.Visible = false;

            }
        }

        public void dataEntryToDataBase()
        {
            BlTaskAssignment taskA = new BlTaskAssignment();
            taskA.getfaxDetails();
            foreach (RepeaterItem item in Rept.Items)
            {
                MlDynamicControlValue data = new MlDynamicControlValue();

                    HiddenField hfFieldType = (HiddenField)item.FindControl("hfFieldType");

                if (hfFieldType.Value == "TextBox")
                {
                    // declare a textbox

                    HiddenField hf1 = (HiddenField)item.FindControl("hfFieldId");
                    HiddenField hf2 = (HiddenField)item.FindControl("hfFieldType");
                    HiddenField hfFieldControlId = (HiddenField)item.FindControl("hfFieldControlId");
                    TextBox txtData = (TextBox)item.FindControl(hf1.Value);


                    //TextBox txtDataNameId = (TextBox)item.FindControl(hf1.Value);
                    data.DynamicControlID = Convert.ToInt32(hfFieldControlId.Value);

                    data.TempCPScreenDataID = Config.TempCPScreenDataID;

                    data.DynamicControlValue = txtData.Text;
                }
                else if (hfFieldType.Value == "CheckBox")
                {
                    // declare a textbox

                    HiddenField hf1 = (HiddenField)item.FindControl("hfFieldId");
                    CheckBox chkData = (CheckBox)item.FindControl(hf1.Value);
                    data.DynamicControlID = Convert.ToInt32(Session["DynamicControlId"]);

                    data.TempCPScreenDataID = Config.TempCPScreenDataID;

                    data.DynamicControlValue = chkData.Checked ? "True" : "False";
                }

                BlFieldInsert setData = new BlFieldInsert();

                if (setData.addFieldDetails(data))
                {
                    List<MlDynamicControlValue> check = (List<MlDynamicControlValue>)Session["MlDynamicControlValue"];
                   
                    HiddenField hf1 = (HiddenField)item.FindControl("hfFieldId");

                    TextBox txtData = (TextBox)item.FindControl(hf1.Value);
                    //txtData.Text = "";

                    
                    HiddenField hfFieldTypes = (HiddenField)item.FindControl("hfFieldType");

                    if (hfFieldTypes.Value == "TextBox")
                    {
                        HiddenField hf11 = (HiddenField)item.FindControl("hfFieldId");
                        HiddenField hf22 = (HiddenField)item.FindControl("hfFieldType");
                        HiddenField hfFieldControlId = (HiddenField)item.FindControl("hfFieldControlId");
                        
                        foreach (var matching in check)
                        {
                            if (matching.DynamicControlValue.Equals(txtData.Text))
                            {
                                Label labelText = (Label)item.FindControl("lblMatchFound");
                                labelText.Visible = true;
                                labelText.Text = "Match Found";
                            }

                            else
                            {
                                Label labelText = (Label)item.FindControl("lblMatchFound");
                                labelText.Visible = false;

                            }
                        }



                    }
                    else if (hfFieldType.Value == "CheckBox")
                    {
                        HiddenField hf13 = (HiddenField)item.FindControl("hfFieldId");
                        CheckBox chkData = (CheckBox)item.FindControl(hf13.Value);
                        foreach (var matching in check)
                        {
                            if (matching.DynamicControlValue.Equals(chkData.Text))
                            {
                                Label labelText = (Label)item.FindControl("lblMatchFound");
                                labelText.Visible = true;
                                labelText.Text = "Match Found";

                            }

                            else
                            {
                                Label labelText = (Label)item.FindControl("lblMatchFound");
                                labelText.Visible = false;

                            }

                        }


                    }

                }
            }

        }
    }
}