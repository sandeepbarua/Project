using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Data;
using MiddleLayer;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace PresentationLayer.Admin
{
    public partial class FaxID_Data : System.Web.UI.Page
    {
        string docid; string docname; string CountForFaxId; string CountForFaxId2; string CustID;
        string DynamicControlID; string DynamicControlID2; string FormDataID2; string UserId;
        string DynamicControlValueText; string DynamicControlValueText2;
        string CPScreenDataID; string CPScreenDataID2; List<string> value1;
        int _ProductID; string _doctypeid; string _Userid; string _Userid1; string userid;
        DataTable dtData = new DataTable();
        DataTable dtData2 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindrept();
            }
        }
        public void bindrept()
        {
            if (Session["FAXID"] != null)
            {
                cls_LoginAudit data = new cls_LoginAudit();
                _ProductID = Convert.ToInt32(Session["FAXID"].ToString());

                DataTable dtuserid = new DataTable();
                dtuserid = data.getuserid(Convert.ToString(_ProductID));
                if (dtuserid.Rows.Count != 0)
                {
                    userid = dtuserid.Rows[0]["UserId"].ToString();
                }
                DataTable dtusername = new DataTable();
                dtusername = (DataTable)data.getUserName(userid);
                if (dtusername.Rows.Count != 0)
                {
                    txtusername1.Text = dtusername.Rows[0]["UserFirstName"].ToString() + "  " + dtusername.Rows[0]["UserLastName"].ToString();
                    txtusername2.Text = dtusername.Rows[1]["UserFirstName"].ToString() + "  " + dtusername.Rows[1]["UserLastName"].ToString();
                }

                DataTable dtdynamicid = new DataTable();
                dtdynamicid = data.GetDynamicControlID(Convert.ToString(_ProductID));

                if (dtdynamicid.Rows.Count == 1)
                {
                    docid = dtdynamicid.Rows[0]["DynamicControlID"].ToString();
                }
                if (dtdynamicid.Rows.Count == 2)
                {
                    docid = dtdynamicid.Rows[0]["DynamicControlID"].ToString() + "," + dtdynamicid.Rows[1]["DynamicControlID"].ToString();
                }
                if (dtdynamicid.Rows.Count == 3)
                {
                    docid = dtdynamicid.Rows[0]["DynamicControlID"].ToString() + "," + dtdynamicid.Rows[1]["DynamicControlID"].ToString() + "," + dtdynamicid.Rows[2]["DynamicControlID"].ToString();
                }

                DataTable dtdocid = new DataTable();
                dtdocid = data.GetDocumentID(docid);
                if (dtdocid.Rows.Count == 1)
                {
                    docname = dtdocid.Rows[0]["DocumentTypeId"].ToString();
                }
                if (dtdocid.Rows.Count == 2)
                {
                    docname = dtdocid.Rows[0]["DocumentTypeId"].ToString() + "," + dtdocid.Rows[1]["DocumentTypeId"].ToString();
                }
                if (dtdocid.Rows.Count == 3)
                {
                    docname = dtdocid.Rows[0]["DocumentTypeId"].ToString() + "," + dtdocid.Rows[1]["DocumentTypeId"].ToString() + "," + dtdocid.Rows[2]["DocumentTypeId"].ToString();
                }
                DataTable DocumentName = new DataTable();
                DocumentName = data.GetDocumentType(docname);

                DataTable dtcustID = new DataTable();
                dtcustID = data.GetCustID(docname);
                if (dtcustID.Rows.Count == 1)
                {
                    CustID = dtcustID.Rows[0]["customerid"].ToString();
                }
                if (dtcustID.Rows.Count == 2)
                {
                    CustID = dtcustID.Rows[0]["customerid"].ToString() + "," + dtcustID.Rows[1]["customerid"].ToString();
                }
                if (dtcustID.Rows.Count == 3)
                {
                    CustID = dtcustID.Rows[0]["customerid"].ToString() + "," + dtcustID.Rows[1]["customerid"].ToString() + "," + dtcustID.Rows[2]["customerid"].ToString();
                }

                Session["Documenttypeid"] = docname;
                Session["CustID"] = CustID;
                Repeater1.DataSource = DocumentName;
                Repeater1.DataBind();

            }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HiddenField Docid = (HiddenField)e.Item.FindControl("hdndocid");

                Repeater innerRepeater = (Repeater)e.Item.FindControl("innerRepeater");
                Repeater innerRepeater2 = (Repeater)e.Item.FindControl("innerRepeater2");

                string DocumentTypeId = Docid.Value;
                if (Session["UId"] != null)
                {
                    string s = Convert.ToString(Session["UId"]);

                    string[] words = s.Split(',');
                    foreach (string word in words)
                    {
                        words.ToString();
                    }
                    _Userid = words[0].ToString();
                    _Userid1 = words[1].ToString();
                }
                cls_LoginAudit data = new cls_LoginAudit();

                dtData = (DataTable)data.GetExceptionData(Convert.ToString(_ProductID), DocumentTypeId, _Userid);
                dtData2 = (DataTable)data.GetExceptionData(Convert.ToString(_ProductID), DocumentTypeId, _Userid1);

                if (dtData.Rows.Count != 0)
                {

                    innerRepeater.DataSource = dtData;
                    innerRepeater.DataBind();
                }




                // cls_LoginAudit datas = new cls_LoginAudit();
                // DataTable dts = new DataTable();
                // dts = (DataTable)datas.getDocumentType(_doctypeid);
                if (dtData2.Rows.Count != 0)
                {

                    //    DataTable dt1 = new DataTable();
                    //    dt1 = (DataTable)data.getdoctypeoruseridbasedata2(_ProductID, _Userid, DocumentTypeId);
                    innerRepeater2.DataSource = dtData2;
                    innerRepeater2.DataBind();
                }

                TextBox value = (TextBox)innerRepeater.FindControl("txtvalue");
                // Repeater innerRepeater2 = (Repeater)e.Item.FindControl("innerRepeater2");
                TextBox value2 = (TextBox)innerRepeater2.FindControl("txtvalue");


            }
        }

        protected void innerRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater innerRepeater1 = (Repeater)e.Item.FindControl("innerRepeater");
                Label id = (Label)e.Item.FindControl("lblid");
                // HiddenField value = (HiddenField)e.Item.FindControl("hndvalue");
                //value1.Add(value.Value);
            }

        }
        protected void innerRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater innerRepeater2 = (Repeater)e.Item.FindControl("innerRepeater2");
                Label id = (Label)e.Item.FindControl("lblid2");
                TextBox value = (TextBox)e.Item.FindControl("txtvalue");
                // string str=value1;
            }
        }

        public void SaveFaxId()
        {
            try
            {
                DataTable dtlog = new DataTable();
                dtlog.Columns.Add("DynamicControlID");
                dtlog.Columns.Add("DynamicControlValueText");
                dtlog.Columns.Add("CPScreenDataID");
                dtlog.Columns.Add("CountForFaxId");

                foreach (RepeaterItem item in Repeater1.Items)
                {
                    Repeater innerRepeater = (Repeater)item.FindControl("innerRepeater");


                    foreach (RepeaterItem item1 in innerRepeater.Items)
                    {

                        Label DynamicControlID = ((Label)item1.FindControl("DynamicControlID"));
                        TextBox DynamicControlValueText = ((TextBox)item1.FindControl("txtvalue"));
                        Label CPScreenDataID = ((Label)item1.FindControl("CPScreenDataID"));
                        Label CountForFaxId = ((Label)item1.FindControl("CountForFaxId"));

                        DataRow rw10 = dtlog.NewRow();
                        rw10["DynamicControlID"] = DynamicControlID.Text;
                        rw10["DynamicControlValueText"] = DynamicControlValueText.Text;
                        rw10["CPScreenDataID"] = CPScreenDataID.Text;
                        rw10["CountForFaxId"] = CountForFaxId.Text;
                        dtlog.Rows.Add(rw10);
                    }
                }

                Session["FaxIDD"] = dtlog;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void SaveFaxID2()
        {

            try
            {
                DataTable dtlog = new DataTable();
                dtlog.Columns.Add("DynamicControlID2");
                dtlog.Columns.Add("DynamicControlValueText");
                dtlog.Columns.Add("CPScreenDataID");
                dtlog.Columns.Add("CountForFaxId");
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    Repeater innerRepeater = (Repeater)item.FindControl("innerRepeater2");


                    foreach (RepeaterItem item1 in innerRepeater.Items)
                    {

                        Label DynamicControlID2 = ((Label)item1.FindControl("DynamicControlID2"));
                        TextBox DynamicControlValueText = ((TextBox)item1.FindControl("txtvalue"));
                        Label CPScreenDataID = ((Label)item1.FindControl("CPScreenDataID"));
                        Label CountForFaxId = ((Label)item1.FindControl("CountForFaxId"));

                        DataRow rw10 = dtlog.NewRow();
                        rw10["DynamicControlID2"] = DynamicControlID2.Text;
                        rw10["DynamicControlValueText"] = DynamicControlValueText.Text;
                        rw10["CPScreenDataID"] = CPScreenDataID.Text;
                        rw10["CountForFaxId"] = CountForFaxId.Text;
                        dtlog.Rows.Add(rw10);
                    }
                }




                Session["FaxIDD2"] = dtlog;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SaveFaxId();
            int str = 0;


            if (Session["FaxIDD"] != null)

            {
                cls_LoginAudit data = new cls_LoginAudit();
                string id = Session["FaxID"].ToString();
                DataTable dtcpscrrendata = new DataTable();
                dtcpscrrendata = (DataTable)data.getExcepationCPDcreenData(Convert.ToString(id));
                string docid = Session["Documenttypeid"].ToString();
                string CustId = Session["CustID"].ToString();

                data.insertmaincpscreendata(dtcpscrrendata, docid, CustId);
                DataTable getcpscreenid = new DataTable();

                getcpscreenid = (DataTable)data.getCPScreenDataID(Convert.ToString(id));
                string dymiccontroid = getcpscreenid.Rows[0]["CPScreenDataID"].ToString();

                System.Data.DataTable MyItem = (System.Data.DataTable)Session["FaxIDD"];
                DataView item = MyItem.DefaultView;
                int Dir = 0;
                foreach (DataRowView drv in item)
                {

                    if (drv.Row["DynamicControlID"].ToString() != "")
                    {
                        DynamicControlID = Convert.ToString(drv.Row["DynamicControlID"].ToString());
                        if (drv.Row["DynamicControlID"].ToString() != "" || drv.Row["DynamicControlValueText"].ToString() != "")
                            DynamicControlValueText = Convert.ToString(drv.Row["DynamicControlValueText"].ToString());

                        if (drv.Row["CPScreenDataID"].ToString() != "")
                            CPScreenDataID = Convert.ToString(drv.Row["CPScreenDataID"].ToString());
                        if (drv.Row["CountForFaxId"].ToString() != "")
                            CountForFaxId = Convert.ToString(drv.Row["CountForFaxId"].ToString());
                    }


                    str = data.insertFormdata(dymiccontroid, DynamicControlID, DynamicControlValueText, CountForFaxId);
                    ++Dir;

                }
            }
            string faxid = Session["FaxID"].ToString();
            cls_LoginAudit data2 = new cls_LoginAudit();
            data2.deletefaxidmaintabledata(faxid);
            data2.deletedatafrom(CPScreenDataID);
            int count = 0;
            if (count == 0)
            {
                string message = "Data Updated Successfully";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            }
            bindrept();

            Response.Redirect("ExceptionData.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            int str = 0;
            SaveFaxID2();


            if (Session["FaxIDD2"] != null)

            {
                cls_LoginAudit data = new cls_LoginAudit();
                string id = Session["FaxID"].ToString();
                DataTable dtcpscrrendata = new DataTable();
                dtcpscrrendata = (DataTable)data.getExcepationCPDcreenData(Convert.ToString(id));
                string docid = Session["Documenttypeid"].ToString();
                string CustId = Session["CustID"].ToString();

                data.insertmaincpscreendata(dtcpscrrendata, docid, CustId);
                DataTable getcpscreenid = new DataTable();

                getcpscreenid = (DataTable)data.getCPScreenDataID(Convert.ToString(id));
                string dymiccontroid = getcpscreenid.Rows[0]["CPScreenDataID"].ToString();

                System.Data.DataTable MyItem = (System.Data.DataTable)Session["FaxIDD2"];
                DataView item = MyItem.DefaultView;
                int Dir = 0;
                foreach (DataRowView drv in item)
                {

                    if (drv.Row["DynamicControlID2"].ToString() != "")
                    {
                        DynamicControlID2 = Convert.ToString(drv.Row["DynamicControlID2"].ToString());
                        if (drv.Row["DynamicControlID2"].ToString() != "" || drv.Row["DynamicControlValueText"].ToString() != "")
                            DynamicControlValueText = Convert.ToString(drv.Row["DynamicControlValueText"].ToString());
                        if (drv.Row["CPScreenDataID"].ToString() != "")
                            CPScreenDataID2 = Convert.ToString(drv.Row["CPScreenDataID"].ToString());
                        if (drv.Row["CountForFaxId"].ToString() != "")
                            CountForFaxId2 = Convert.ToString(drv.Row["CountForFaxId"].ToString());

                    }


                    str = data.insertFormdata(dymiccontroid, DynamicControlID2, DynamicControlValueText, CountForFaxId2);
                    ++Dir;

                }
            }
            string faxid = Session["FaxID"].ToString();
            cls_LoginAudit data2 = new cls_LoginAudit();
            data2.deletefaxidmaintabledata(faxid);
            data2.deletedatafrom(CPScreenDataID2);

            string message = "Data Updated Successfully";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            bindrept();
            Response.Redirect("ExceptionData.aspx");
        }
        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExceptionData.aspx");
        }


    }
}