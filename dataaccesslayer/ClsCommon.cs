using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web;
using MiddleLayer;


namespace DataAccessLayer
{
   public class ClsCommon
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }
        public static void ddlCustomerID(DropDownList ddlCustID)
        {
            ddlCustID.Items.Clear();
            DataTable dtddlCustomerName;
            ListItem li = new ListItem();
            li.Text = "<--Select Customer Name-->";
            li.Value = "0";
            ddlCustID.Items.Add(li);
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(new BLConsumeApi().bindDropDownCustomerAll());
            dtddlCustomerName = dt;
            for (int i = 0; i < dtddlCustomerName.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtddlCustomerName.Rows[i]["CompanyID"].ToString();
                li.Text = dtddlCustomerName.Rows[i]["CompanyName"].ToString();
                ddlCustID.Items.Add(li);
            }
        }
        public static void CreateExcelFile(DataTable Excel, string Name)
        {

            HttpContext.Current.Response.Clear();

            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            //Clears all content output from the buffer stream. 
            HttpContext.Current.Response.ClearContent();
            //Adds HTTP header to the output stream 
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename=" + Name + ""));

            // Gets or sets the HTTP MIME type of the output stream 
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.Charset = "utf-8";

            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table  bgColor='#ffffff' " +
    "borderColor='#000000' cellSpacing='2' cellPadding='2' " + "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
            string[] words = Name.Split('.');
            HttpContext.Current.Response.Write("<Tr  ><td style='font-size:13.0pt;' colspan='3' >" + words[0] + " Report</td></tr><tr><td>&nbsp</td></tr>");
            HttpContext.Current.Response.Write("</br > ");
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
     "borderColor='#000000' cellSpacing='0' cellPadding='0' " + "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
            string space = "";

            HttpContext.Current.Response.Write("<TR  >");
            foreach (DataColumn dcolumn in Excel.Columns)
            {

                HttpContext.Current.Response.Write("<Td style='font-size:10.0pt; font-family:Calibri; background:orange;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(space + dcolumn.ColumnName);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");


                space = "\t";
            }
            HttpContext.Current.Response.Write("</TR>");
            HttpContext.Current.Response.Write("\n");
            int countcolumn;
            foreach (DataRow dr in Excel.Rows)
            {
                space = "";
                HttpContext.Current.Response.Write("<TR>");
                for (countcolumn = 0; countcolumn < Excel.Columns.Count; countcolumn++)
                {
                    //HttpContext.Current.Response.Write("<TR>");
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(space + dr[countcolumn].ToString());
                    HttpContext.Current.Response.Write("</Td>");

                    space = "\t";

                }

                HttpContext.Current.Response.Write("</TR>");

                HttpContext.Current.Response.Write("\n");

            }
            //HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        public DataTable getAuditRule(string docid, string questionid)
        {
            int DocId;
            int QuesId;
            DataTable dt = new DataTable();
            try
            {
                if (int.TryParse(docid, out DocId) && int.TryParse(questionid, out QuesId))
                {
                    ListtoDataTableConverter converter = new ListtoDataTableConverter();
                    dt = converter.ToDataTable(new BLConsumeApi().Sp_GetAuditRulesClassificationByDocId(DocId, QuesId));
                }
            }
            catch { }
            return dt;            
        }
        public void ddlDynamicControl(DropDownList ddlDynamicControl, int docid)
        {
            ddlDynamicControl.Items.Clear();
            DataTable dtddlroleName;
            ListItem li = new ListItem();
            li.Text = "<------Select------->";
            li.Value = "0";
            ddlDynamicControl.Items.Add(li);
            string Label = string.Empty;
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(new BLConsumeApi().ddlDynamicControlLabelName(docid));
            if (dt.Rows.Count > 0)
            {
                dtddlroleName = dt;
                for (int i = 0; i < dtddlroleName.Rows.Count; i++)
                {
                    Label = Convert.ToString(dtddlroleName.Rows[i]["labelname"]).Trim().ToUpper();
                    if (Label != "START PAGE" && Label != "END PAGE" && Label != "LOCATION")
                    {
                        li = new ListItem();
                        //li.Text = dtddlbusinessGroup.Rows[i]["BusinessGrpId"].ToString();
                        //li.Value = dtddlbusinessGroup.Rows[i]["BusinessGrpName"].ToString();
                        li.Value = dtddlroleName.Rows[i]["DynamicControlID"].ToString();
                        li.Text = dtddlroleName.Rows[i]["labelname"].ToString();
                        ddlDynamicControl.Items.Add(li);
                    }
                }
            }
        }
        public void ddlRuletype(DropDownList ddlDynamicControl)
        {
            BLConsumeApi objApi = new DataAccessLayer.BLConsumeApi();
            ddlDynamicControl.Items.Clear();
            DataTable dtddlroleName;
            ListItem li = new ListItem();
            li.Text = "<------Select------->";
            li.Value = "0";
            ddlDynamicControl.Items.Add(li);
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            dtddlroleName = converter.ToDataTable(new BLConsumeApi().Sp_GetRuleType());
            for (int i = 0; i < dtddlroleName.Rows.Count; i++)
            {
                li = new ListItem();              
                li.Value = dtddlroleName.Rows[i]["RuleType"].ToString();
                li.Text = dtddlroleName.Rows[i]["RuleType"].ToString();
                ddlDynamicControl.Items.Add(li);
            }
        }
        public void DeleteAuditRuleClassification(int Auditid)
        {
            try
            {
                BLConsumeApi objApi = new BLConsumeApi();
                objApi.SP_DeleteAuditRuleClassification(Auditid);
            }
            catch { }

        }
        public static string getDateFormat(string strDate)
        {
            string NewDate = string.Empty;
            try
            {
                DateTime date = new DateTime();
                if (DateTime.TryParse(strDate, out date))
                {
                    NewDate= date.ToString("MM-dd-yyyy");
                }
                else
                {
                    NewDate= strDate;
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return NewDate;

        }
        public static void BindDynamicControlLabel(DropDownList ddlDataField, int DocumentID)
        {
            try
            {
                BLConsumeApi obj = new BLConsumeApi();
                ddlDataField.Items.Clear();
                ListItem li = new ListItem();
                li.Text = "<--Select Label-->";
                li.Value = "0";
                ddlDataField.Items.Add(li);
                List<MlDynamicControl> DynamicControlList = DynamicControlList = obj.dynamicControlList(DocumentID);
                List<MlDynamicControl> distinctDynamicControlList = DynamicControlList.GroupBy(label => label.labelName).Select(g => g.First()).ToList();

                foreach (var r in distinctDynamicControlList)
                {
                    li = new ListItem();
                    li.Value = Convert.ToString(r.DynamicControlID);
                    li.Text = r.labelName;
                    ddlDataField.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

    }
}
