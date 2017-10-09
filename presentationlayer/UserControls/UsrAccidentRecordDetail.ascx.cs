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

namespace PresentationLayer.UserControls
{
    public partial class UsrAccidentRecordDetail : System.Web.UI.UserControl
    {
        log4net.ILog logger1 = log4net.LogManager.GetLogger(typeof(UsrAccidentRecordDetail));
        DQFEntities db1 = new DQFEntities();

        public void BindAccidentRecordDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            try
            {
                var AccidentRecordList = new BLConsumeAPI_UserControl().GetAccidentRecordDetail(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                var AccidentRecordListUser2 = new BLConsumeAPI_UserControl().GetAccidentRecordDetail(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);

                //List<AccidentRecord> AccidentRecordList = db1.AccidentRecordDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //    .Select(x => new AccidentRecord
                //    {
                //        AccidentRecordDetailId = x.AccidentRecordDetailId,
                //        DateOfAccident = x.DateOfAccident,
                //        Fatalities = x.Fatalities,
                //        Injuries = x.Injuries,
                //        NatureOfAccident = x.NatureOfAccident,
                //        TempCPScreenDataID = x.TempCPScreenDataID,
                //        UserId = x.UserId,
                //        CountForFaxId = x.CountForFaxId
                //    }).OrderBy(x => x.AccidentRecordDetailId).ToList();
                if (AccidentRecordList != null)
                {
                    if (AccidentRecordList.Count > 0)
                    {
                        Session["AccidentRecordList"] = AccidentRecordList;
                    }
                }

                //List<AccidentRecord> AccidentRecordListUser2 = db1.AccidentRecordDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId != UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //   .Select(x => new AccidentRecord
                //   {
                //       AccidentRecordDetailId = x.AccidentRecordDetailId,
                //       DateOfAccident = x.DateOfAccident,
                //       Fatalities = x.Fatalities,
                //       Injuries = x.Injuries,
                //       NatureOfAccident = x.NatureOfAccident,
                //       TempCPScreenDataID = x.TempCPScreenDataID,
                //       UserId = x.UserId,
                //       CountForFaxId = x.CountForFaxId
                //   }).OrderBy(x => x.AccidentRecordDetailId).ToList();
                if (AccidentRecordListUser2 != null)
                {
                    if (AccidentRecordListUser2.Count > 0)
                    {
                        Session["AccidentRecordListUser2"] = AccidentRecordListUser2;
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
                if (Session["AccidentRecordList"] == null)
                {
                    if (Session["AccidentRecordCount"] == null)
                    {
                        Session["AccidentRecordCount"] = 1;
                        ShowRows(1, 1);
                    }
                    else
                    {
                        int count = 0;
                        count = Convert.ToInt32(Session["AccidentRecordCount"]);
                        if (count == 1)
                        {
                            Session["AccidentRecordCount"] = 1;
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
                    List<AccidentRecord> AccidentRecordList = Session["AccidentRecordList"] as List<AccidentRecord>;
                    int records = AccidentRecordList.Count; ;

                    int count = 0;
                    count = Convert.ToInt32(Session["AccidentRecordCount"]);
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
            else
                txt2.Text = "MM/DD/YYYY";

            txt2.TextMode = TextBoxMode.Date;

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
                if (Session["AccidentRecordCount"] != null)
                {
                    count = Convert.ToInt32(Session["AccidentRecordCount"]);
                }

                if (Session["AccidentRecordList"] != null)
                {
                    List<AccidentRecord> AccidentRecordList = Session["AccidentRecordList"] as List<AccidentRecord>;
                    int records = AccidentRecordList.Count;
                    if (records > 0)
                    {
                        count = count + 1;
                        Session["AccidentRecordCount"] = count;

                        count = count + records;

                        AddNewRows(count);
                    }
                    else
                    {
                        count++;
                        Session["AccidentRecordCount"] = count;
                        AddNewRows(count);
                    }

                }
                else
                {
                    count++;
                    Session["AccidentRecordCount"] = count;
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
                if (Session["AccidentRecordCount"] != null)
                {
                    count = Convert.ToInt32(Session["AccidentRecordCount"]);
                }

                if (Session["AccidentRecordList"] != null)
                {
                    List<AccidentRecord> AccidentRecordList = Session["AccidentRecordList"] as List<AccidentRecord>;
                    int records = AccidentRecordList.Count; ;
                    if (count > 0)
                    {
                        int tmpCount = count - 1;
                        Session["AccidentRecordCount"] = tmpCount;
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
                            //AccidentRecordDetail cd = db1.AccidentRecordDetails.Where(x => x.AccidentRecordDetailId == id).SingleOrDefault();
                            //if (cd != null)
                            //{
                            //db1.AccidentRecordDetails.Remove(cd);
                            // db1.SaveChanges();
                            BLConsumeAPI_UserControl delete = new BLConsumeAPI_UserControl();
                            delete.DeleteAccidentRecordDetail(id);
                            AccidentRecord dcn = AccidentRecordList.Where(x => x.AccidentRecordDetailId == id).Single();
                                AccidentRecordList.Remove(dcn);
                                Session["AccidentRecordList"] = AccidentRecordList;
                                DeleteRow(records);
                           // }
                        }

                    }

                }
                else
                {

                    DeleteRow(count);
                    count--;
                    Session["AccidentRecordCount"] = count;
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }

        }
    }
}