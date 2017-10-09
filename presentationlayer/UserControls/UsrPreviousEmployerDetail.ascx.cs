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
    public partial class UsrPreviousEmployerDetail : System.Web.UI.UserControl
    {
        log4net.ILog logger1 = log4net.LogManager.GetLogger(typeof(UsrPreviousEmployerDetail));
        DQFEntities db1 = new DQFEntities();

        public void BindPreviousEmployerDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            try
            {
                var PreviousEmployerList = new BLConsumeAPI_UserControl().GetPreviousEmployerDetail(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                var PreviousEmployerListUser2 = new BLConsumeAPI_UserControl().GetPreviousEmployerDetail2(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);

                //List<PreviousEmployer> PreviousEmployerList = db1.PreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //    .Select(x => new PreviousEmployer
                //    {
                //        PreviousEmployerName = x.PreviousEmployerName,
                //        PreviousEmployerStreetAddress = x.PreviousEmployerStreetAddress,
                //        PreviousEmployerCity = x.PreviousEmployerCity,
                //        PreviousEmployerState = x.PreviousEmployerState,
                //        PreviousEmployerZipcode = x.PreviousEmployerZipcode,
                //        EmploymentEndDate = x.PreviousEmploymentEndDate,
                //        EmploymentStartDate = x.PreviousEmploymentStartDate,
                //        PreviousEmployerDetailId = x.PreviousEmployerDetailId,
                //        ReasonForLeavingPreviousEmployments = x.ReasonForLeavingPreviousEmployments,
                //        TempCPScreenDataID = x.TempCPScreenDataID,
                //        UserId = x.UserId
                //    }).OrderBy(x => x.PreviousEmployerDetailId).ToList();
                if (PreviousEmployerList != null)
                {
                    if (PreviousEmployerList.Count > 0)
                    {
                        Session["PreviousEmployerList"] = PreviousEmployerList;
                    }
                }

                //List<PreviousEmployer> PreviousEmployerListUser2 = db1.PreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId != UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //   .Select(x => new PreviousEmployer
                //   {
                //       PreviousEmployerName = x.PreviousEmployerName,
                //       PreviousEmployerStreetAddress = x.PreviousEmployerStreetAddress,
                //       PreviousEmployerCity = x.PreviousEmployerCity,
                //       PreviousEmployerState = x.PreviousEmployerState,
                //       PreviousEmployerZipcode = x.PreviousEmployerZipcode,
                //       EmploymentEndDate = x.PreviousEmploymentEndDate,
                //       EmploymentStartDate = x.PreviousEmploymentStartDate,
                //       PreviousEmployerDetailId = x.PreviousEmployerDetailId,
                //       ReasonForLeavingPreviousEmployments = x.ReasonForLeavingPreviousEmployments,
                //       TempCPScreenDataID = x.TempCPScreenDataID,
                //       UserId = x.UserId
                //   }).OrderBy(x => x.PreviousEmployerDetailId).ToList();
                if (PreviousEmployerListUser2 != null)
                {
                    if (PreviousEmployerListUser2.Count > 0)
                    {
                        Session["PreviousEmployerListUser2"] = PreviousEmployerListUser2;
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
                if (Session["PreviousEmployerList"] == null)
                {
                    if (Session["PreviousEmployerCount"] == null)
                    {
                       // Session["PreviousEmployerCount"] = 1;
                        //ShowRows(1, 1);
                    }
                    else
                    {
                        int count = 0;
                        count = Convert.ToInt32(Session["PreviousEmployerCount"]);
                        if (count == 1)
                        {
                            Session["PreviousEmployerCount"] = 1;
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
                    List<PreviousEmployer> PreviousEmployerList = Session["PreviousEmployerList"] as List<PreviousEmployer>;
                    int records = PreviousEmployerList.Count; ;

                    int count = 0;
                    count = Convert.ToInt32(Session["PreviousEmployerCount"]);
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

        private void AddNewRows(int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "")
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
            txt6.CssClass += " form-control-Small ";
            txt6.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text6))
                txt6.Text = text6;
            else
                txt6.Text = "MM/DD/YYYY";

            HtmlTableCell tc6 = new HtmlTableCell();
            tc6.Controls.Add(txt6);

            TextBox txt7 = new TextBox();
            txt7.ID = "txt7" + count.ToString();
            txt7.CssClass += " form-control-Small ";
            txt7.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text7))
                txt7.Text = text7;
            else
                txt7.Text = "MM/DD/YYYY";

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

       

        private void DeleteRow(int count)
        {
            string rowId = "row" + count;
            HtmlTableRow row = (HtmlTableRow)tbl.FindControl(rowId);
            tbl.Rows.Remove(row);

        }

      

        protected void btnAddRow2_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (Session["PreviousEmployerCount"] != null)
                {
                    count = Convert.ToInt32(Session["PreviousEmployerCount"]);
                }

                if (Session["PreviousEmployerList"] != null)
                {
                    List<PreviousEmployer> PreviousEmployerList = Session["PreviousEmployerList"] as List<PreviousEmployer>;
                    int records = PreviousEmployerList.Count;
                    if (records > 0)
                    {
                        count = count + 1;
                        Session["PreviousEmployerCount"] = count;

                        count = count + records;

                        AddNewRows(count);
                    }
                    else
                    {
                        count++;
                        Session["PreviousEmployerCount"] = count;
                        AddNewRows(count);
                    }

                }
                else
                {
                    count++;
                    Session["PreviousEmployerCount"] = count;
                    AddNewRows(count);
                }

                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmPreviousEmployer();", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmDriverApplication();", true);
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }

        protected void btnRemoveRow2_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (Session["PreviousEmployerCount"] != null)
                {
                    count = Convert.ToInt32(Session["PreviousEmployerCount"]);
                }

                if (Session["PreviousEmployerList"] != null)
                {
                    List<PreviousEmployer> PreviousEmployerList = Session["PreviousEmployerList"] as List<PreviousEmployer>;
                    int records = PreviousEmployerList.Count; ;
                    if (count > 0)
                    {
                        int tmpCount = count - 1;
                        Session["PreviousEmployerCount"] = tmpCount;
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
                           // var cd = new BLConsumeAPI_UserControl().GetPreviousEmployerDetailid(id);
                            // PreviousEmployerDetail cd = db1.PreviousEmployerDetails.Where(x => x.PreviousEmployerDetailId == id).SingleOrDefault();
                           // if (cd != null)
                            //{
                                //db1.PreviousEmployerDetails.Remove(cd);
                                //db1.SaveChanges();
                                BLConsumeAPI_UserControl delete = new BLConsumeAPI_UserControl();
                                delete.DeletePreviousEmployerDetailid(id);
                                PreviousEmployer dcn = PreviousEmployerList.Where(x => x.PreviousEmployerDetailId == id).Single();
                                PreviousEmployerList.Remove(dcn);
                                Session["PreviousEmployerList"] = PreviousEmployerList;
                                DeleteRow(records);
                            //}
                        }

                    }

                }
                else
                {

                    DeleteRow(count);
                    count--;
                    Session["PreviousEmployerCount"] = count;
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }
    }
}