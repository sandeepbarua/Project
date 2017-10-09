using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using PresentationLayer.Model;
using PresentationLayer.App_Code;
using Newtonsoft.Json;
using RestSharp;
using MiddleLayer;
using System.Xml;
using System.IO;
using System.Configuration;
using DataAccessLayer;
using System.Data;

namespace PresentationLayer.UserControls
{
    public partial class UsrTypeOfEquipmentDetail : System.Web.UI.UserControl
    {
        string str = null;
        
       
        public void BindTypeOfEquipmentEmployerDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            try
            {
                //var client = new RestClient(ExecuteQueryUrl);
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", authorizationHeader);
                //request.AddHeader("content-type", contentTypeHeader);
                //request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_TypeOfEquipmentList\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+ DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+ UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+ TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+ CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);
                // List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
                var TypeOfEquipmentLists = new BLConsumeAPI_UserControl().GetTypeOfEquipmentEmployerDetails(UserId,DocumentTypeId, TempCPScreenDataID,CountFaxId);
                var TypeOfEquipmentListUser2 = new BLConsumeAPI_UserControl().GetTypeOfEquipmentEmployerDetails2(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                // List<TypeOfEquipmentClass> TypeOfEquipmentList = db1.TypeOfEquipmentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //.Select(x => new TypeOfEquipmentClass
                //{
                //    TypeOfEquipment = x.TypeOfEquipment,
                //    DrivingFrom = x.DrivingFrom,
                //    DrivingTo = x.DrivingTo,
                //    Miles = x.Miles,
                //    TypeOfEquipmentDetailId = x.TypeOfEquipmentDetailId,
                //    TempCPScreenDataID = x.TempCPScreenDataID,
                //    UserId = x.UserId
                //}).OrderBy(x => x.TypeOfEquipmentDetailId).ToList();
                if (TypeOfEquipmentLists != null)
                {
                    if (TypeOfEquipmentLists.Count > 0)
                    {
                        Session["TypeOfEquipmentList"] = TypeOfEquipmentLists;
                    }
                }

                //List<TypeOfEquipmentClass> TypeOfEquipmentListUser2 = db1.TypeOfEquipmentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId != UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //   .Select(x => new TypeOfEquipmentClass
                //   {
                //       TypeOfEquipment = x.TypeOfEquipment,
                //       DrivingFrom = x.DrivingFrom,
                //       DrivingTo = x.DrivingTo,
                //       Miles = x.Miles,
                //       TypeOfEquipmentDetailId = x.TypeOfEquipmentDetailId,
                //       TempCPScreenDataID = x.TempCPScreenDataID,
                //       UserId = x.UserId
                //   }).OrderBy(x => x.TypeOfEquipmentDetailId).ToList();
                if (TypeOfEquipmentListUser2 != null)
                {
                    if (TypeOfEquipmentListUser2.Count > 0)
                    {
                        Session["TypeOfEquipmentListUser2"] = TypeOfEquipmentListUser2;
                    }
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }
        //string authorizationHeader;
        //string contentTypeHeader;
        //string ExecuteQueryUrl;
        log4net.ILog logger1 = log4net.LogManager.GetLogger(typeof(UsrTypeOfEquipmentDetail));
        DQFEntities db1 = new DQFEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //XmlDataDocument xmldoc = new XmlDataDocument();
            //XmlNodeList xmlnode;
            //int i = 0; 
            //FileStream fs = new FileStream("D:/DQF 16-06-2017/PresentationLayer/myData.xml", FileMode.Open, FileAccess.Read);
            //xmldoc.Load(fs); xmlnode = xmldoc.GetElementsByTagName("Token");
            //for (i = 0; i <= xmlnode.Count - 1; i++)
            //{
            //    xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
            //    str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
            //    // Label1.Text = str;
            //    //MessageBox.Show(str); 
            //}

           
            // authorizationHeader = str;
            // contentTypeHeader = "application/json";
            //ExecuteQueryUrl = ConfigurationManager.AppSettings["URL"];

            try
            {
                if (Session["TypeOfEquipmentList"] == null)
                {
                    if (Session["TypeOfEquipmentCount"] == null)
                    {
                        //Session["TypeOfEquipmentCount"] = 1;
                        //ShowRows(1, 1);
                    }
                    else
                    {
                        int count = 0;
                        count = Convert.ToInt32(Session["TypeOfEquipmentCount"]);
                        if (count == 1)
                        {
                            Session["TypeOfEquipmentCount"] = 1;
                            ShowRows(1, 1);
                        }
                        else if (count > 1)
                        {
                            ShowRows(1, count);
                        }
                    }
                }
                else
                {
                    List<TypeOfEquipmentClass> TypeOfEquipmentList = Session["TypeOfEquipmentList"] as List<TypeOfEquipmentClass>;
                    int records = TypeOfEquipmentList.Count; ;

                    int count = 0;
                    count = Convert.ToInt32(Session["TypeOfEquipmentCount"]);
                    if (count >= 1)
                    {
                        count = count + records;
                        records++;
                        ShowRows(records, count);
                    }

                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }

        }

        private void ShowRows(int start = 0, int end = 0)
        {
            for (int i = start; i <= end; i++)
            {
                AddNewRows(i);
            }
        }

        private void AddNewRows(int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "")
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

        protected void btnAddRows_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (Session["TypeOfEquipmentCount"] != null)
                {
                    count = Convert.ToInt32(Session["TypeOfEquipmentCount"]);
                }

                if (Session["TypeOfEquipmentList"] != null)
                {
                    List<TypeOfEquipmentClass> TypeOfEquipmentList = Session["TypeOfEquipmentList"] as List<TypeOfEquipmentClass>;
                    int records = TypeOfEquipmentList.Count;
                    if (records > 0)
                    {
                        count = count + 1;
                        Session["TypeOfEquipmentCount"] = count;

                        count = count + records;

                        AddNewRows(count);
                    }
                    else
                    {
                        count++;
                        Session["TypeOfEquipmentCount"] = count;
                        AddNewRows(count);
                    }

                }
                else
                {
                    count++;
                    Session["TypeOfEquipmentCount"] = count;
                    AddNewRows(count);
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmDriverApplication();", true);
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }

        private void DeleteRow(int count)
        {
            string rowId = "row" + count;
            HtmlTableRow row = (HtmlTableRow)tbl.FindControl(rowId);
            tbl.Rows.Remove(row);

        }

        protected void btnRemoveRows_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (Session["TypeOfEquipmentCount"] != null)
                {
                    count = Convert.ToInt32(Session["TypeOfEquipmentCount"]);
                }

                if (Session["TypeOfEquipmentList"] != null)
                {
                    List<TypeOfEquipmentClass> TypeOfEquipmentList = Session["TypeOfEquipmentList"] as List<TypeOfEquipmentClass>;
                    int records = TypeOfEquipmentList.Count; ;
                    if (count > 0)
                    {
                        int tmpCount = count - 1;
                        Session["TypeOfEquipmentCount"] = tmpCount;
                        if (records > 0)
                        {
                            count = count + records;
                        }
                        // count--;

                        DeleteRow(count);
                    }
                    else if (records > 0)
                    {

                        string strHfId = "hf" + records.ToString();
                        string strId = ((HiddenField)tbl.FindControl(strHfId)).Value;
                        if (!string.IsNullOrEmpty(strId))
                        {
                            int id = Convert.ToInt32(strId);
                            
                           // var cd = new BLConsumeAPI_UserControl().GetTypeOfEquipmentEmployerDetailsid(id);
                            
                            //TypeOfEquipmentDetail cd = db1.TypeOfEquipmentDetails.Where(x => x.TypeOfEquipmentDetailId == id).SingleOrDefault();
                            //if (cd != null)
                            //{
                                // db1.TypeOfEquipmentDetails.Remove(cd);
                                // db1.SaveChanges();
                                BLConsumeAPI_UserControl delete = new BLConsumeAPI_UserControl();
                                delete.DeleteTypeOfEquipmentEmployerDetailsid(id);
                                 TypeOfEquipmentClass dcn = TypeOfEquipmentList.Where(x => x.TypeOfEquipmentDetailId == id).Single();
                               // var dcn = new BLConsumeAPI_UserControl().GetTypeOfEquipmentEmployerDetailsid(id);
                                TypeOfEquipmentList.Remove(dcn);
                                Session["TypeOfEquipmentList"] = TypeOfEquipmentList;
                                DeleteRow(records);
                           // }
                        }

                    }

                }
                else
                {

                    DeleteRow(count);
                    count--;
                    Session["TypeOfEquipmentCount"] = count;
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }
    }
}