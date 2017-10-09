using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using PresentationLayer.Model;
using PresentationLayer.App_Code;
using DataAccessLayer;
using MiddleLayer;
namespace PresentationLayer.UserControls
{
    public partial class UsrCurrentEmployerDetail : System.Web.UI.UserControl
    {
        log4net.ILog logger1 = log4net.LogManager.GetLogger(typeof(UsrCurrentEmployerDetail));
        DQFEntities db1 = new DQFEntities();

        public void BindCurrentEmployerDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            try
            {
                var CurrentEmployerList = new BLConsumeAPI_UserControl().GetCurrentEmployerDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                var CurrentEmployerListUser2 = new BLConsumeAPI_UserControl().GetCurrentEmployerDetails2(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);

                //List<CurrentEmployer> CurrentEmployerList = db1.CurrentEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //    .Select(x => new CurrentEmployer
                //    {
                //        CurrentEmployerName = x.CurrentEmployerName,
                //        CurrentEmployerStreetAddress = x.CurrentEmployerStreetAddress,
                //        CurrentEmployerCity = x.CurrentEmployerCity,
                //        CurrentEmployerState = x.CurrentEmployerState,
                //        CurrentEmployerZipcode = x.CurrentEmployerZipcode,
                //        EmploymentStartDate = x.CurrentEmploymentStartDate,
                //        CurrentEmployerDetailId = x.CurrentEmployerDetailID,
                //        TempCPScreenDataID = x.TempCPScreenDataID,
                //        UserId = x.UserId
                //    }).OrderBy(x => x.CurrentEmployerDetailId).ToList();
                if (CurrentEmployerList != null)
                {
                    if (CurrentEmployerList.Count > 0)
                    {
                        Session["CurrentEmployerList"] = CurrentEmployerList;
                    }
                }

                //List<CurrentEmployer> CurrentEmployerListUser2 = db1.CurrentEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId != UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //    .Select(x => new CurrentEmployer
                //    {
                //        CurrentEmployerName = x.CurrentEmployerName,
                //        CurrentEmployerStreetAddress = x.CurrentEmployerStreetAddress,
                //        CurrentEmployerCity = x.CurrentEmployerCity,
                //        CurrentEmployerState = x.CurrentEmployerState,
                //        CurrentEmployerZipcode = x.CurrentEmployerZipcode,
                //        EmploymentStartDate = x.CurrentEmploymentStartDate,
                //        CurrentEmployerDetailId = x.CurrentEmployerDetailID,
                //        TempCPScreenDataID = x.TempCPScreenDataID,
                //        UserId = x.UserId
                //    }).OrderBy(x => x.CurrentEmployerDetailId).ToList();
                if (CurrentEmployerListUser2 != null)
                {
                    if (CurrentEmployerListUser2.Count > 0)
                    {
                        Session["CurrentEmployerListUser2"] = CurrentEmployerListUser2;
                    }
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CurrentEmployerList"] == null)
                {
                    if (Session["CurrentEmployerCount"] == null)
                    {
                        //Session["CurrentEmployerCount"] = 1;
                        //ShowRows(1, 1);
                    }
                    else
                    {
                        int count = 0;
                        count = Convert.ToInt32(Session["CurrentEmployerCount"]);
                        if (count == 1)
                        {
                            Session["CurrentEmployerCount"] = 1;
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
                    List<CurrentEmployer> CurrentEmployerList = Session["CurrentEmployerList"] as List<CurrentEmployer>;
                    int records = CurrentEmployerList.Count; ;

                    int count = 0;
                    count = Convert.ToInt32(Session["CurrentEmployerCount"]);
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

        private void AddNewRows(int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "")
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
            txt6.CssClass += " form-control ";
            if (!string.IsNullOrEmpty(text6))
                txt6.Text = text6;
            else
                txt6.Text = "MM/DD/YYYY";

            txt6.TextMode = TextBoxMode.Date;

            HtmlTableCell tc6 = new HtmlTableCell();
            tc6.Controls.Add(txt6);

           


            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);
            rows.Cells.Add(tc4);
            rows.Cells.Add(tc5);
            rows.Cells.Add(tc6);
         
            tbl.Rows.Add(rows);

        }
       

        private void DeleteRow(int count)
        {
            string rowId = "row" + count;
            HtmlTableRow row = (HtmlTableRow)tbl.FindControl(rowId);
            tbl.Rows.Remove(row);

        }

     
        protected void btnAddNewRow1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (Session["CurrentEmployerCount"] != null)
                {
                    count = Convert.ToInt32(Session["CurrentEmployerCount"]);
                }

                if (Session["CurrentEmployerList"] != null)
                {
                    List<CurrentEmployer> CurrentEmployerList = Session["CurrentEmployerList"] as List<CurrentEmployer>;
                    int records = CurrentEmployerList.Count;
                    if (records > 0)
                    {
                        count = count + 1;
                        Session["CurrentEmployerCount"] = count;

                        count = count + records;

                        AddNewRows(count);
                    }
                    else
                    {
                        count++;
                        Session["CurrentEmployerCount"] = count;
                        AddNewRows(count);
                    }

                }
                else
                {
                    count++;
                    Session["CurrentEmployerCount"] = count;
                    AddNewRows(count);
                }

                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmCurrentEmployer();", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmDriverApplication();", true);

            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }

        protected void btnRemoveRow1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (Session["CurrentEmployerCount"] != null)
                {
                    count = Convert.ToInt32(Session["CurrentEmployerCount"]);
                }

                if (Session["CurrentEmployerList"] != null)
                {
                    List<CurrentEmployer> CurrentEmployerList = Session["CurrentEmployerList"] as List<CurrentEmployer>;
                    int records = CurrentEmployerList.Count; ;
                    if (count > 0)
                    {
                        int tmpCount = count - 1;
                        Session["CurrentEmployerCount"] = tmpCount;
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
                            // CurrentEmployerDetail cd = db1.CurrentEmployerDetails.Where(x => x.CurrentEmployerDetailID == id).SingleOrDefault();
                            // if (cd != null)
                            //{
                            // db1.CurrentEmployerDetails.Remove(cd);
                            // db1.SaveChanges();
                            BLConsumeAPI_UserControl delete = new BLConsumeAPI_UserControl();
                            delete.DeleteCurrentEmployerDetailsid(id);
                            CurrentEmployer dcn = CurrentEmployerList.Where(x => x.CurrentEmployerDetailId == id).Single();
                                CurrentEmployerList.Remove(dcn);
                                Session["CurrentEmployerList"] = CurrentEmployerList;
                                DeleteRow(records);
                            //}
                        }

                    }

                }
                else
                {

                    DeleteRow(count);
                    count--;
                    Session["CurrentEmployerCount"] = count;
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }
    }
}