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
    public partial class UsrCEDPreviousEmployerDetail : System.Web.UI.UserControl
    {
        log4net.ILog logger1 = log4net.LogManager.GetLogger(typeof(UsrCEDPreviousEmployerDetail));
        DQFEntities db1 = new DQFEntities();

        public void BindCEDPreviousEmployerDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            try
            {
                var CEDPreviousEmployerList = new BLConsumeAPI_UserControl().GetCEDPreviousEmployerDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                var CEDPreviousEmployerListUser2 = new BLConsumeAPI_UserControl().GetCEDPreviousEmployerDetails2(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);

                //List<CEDPreviousEmployer> CEDPreviousEmployerList = db1.CEDPreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //    .Select(x => new CEDPreviousEmployer
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
                //        UserId = x.UserId,
                //        EmploymentGapFrom = x.EmploymentGapFrom,
                //        EmploymentGapTo = x.EmploymentGapTo,
                //        ReasonForEmploymentGap = x.ReasonForEmploymentGap
                //    }).OrderBy(x => x.PreviousEmployerDetailId).ToList();
                if (CEDPreviousEmployerList != null)
                {
                    if (CEDPreviousEmployerList.Count > 0)
                    {
                        Session["CEDPreviousEmployerList"] = CEDPreviousEmployerList;
                    }
                }

                //List<CEDPreviousEmployer> CEDPreviousEmployerListUser2 = db1.CEDPreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId != UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //   .Select(x => new CEDPreviousEmployer
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
                //       UserId = x.UserId,
                //       EmploymentGapFrom = x.EmploymentGapFrom,
                //       EmploymentGapTo = x.EmploymentGapTo,
                //       ReasonForEmploymentGap = x.ReasonForEmploymentGap
                //   }).OrderBy(x => x.PreviousEmployerDetailId).ToList();
                if (CEDPreviousEmployerListUser2 != null)
                {
                    if (CEDPreviousEmployerListUser2.Count > 0)
                    {
                        Session["CEDPreviousEmployerListUser2"] = CEDPreviousEmployerListUser2;
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
                if (Session["CEDPreviousEmployerList"] == null)
                {
                    if (Session["CEDPreviousEmployerCount"] == null)
                    {
                        //Session["CEDPreviousEmployerCount"] = 1;
                        //ShowRows(1, 1);
                    }
                    else
                    {
                        int count = 0;
                        count = Convert.ToInt32(Session["CEDPreviousEmployerCount"]);
                        if (count == 1)
                        {
                            Session["CEDPreviousEmployerCount"] = 1;
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
                    List<CEDPreviousEmployer> CEDPreviousEmployerList = Session["CEDPreviousEmployerList"] as List<CEDPreviousEmployer>;
                    int records = CEDPreviousEmployerList.Count; ;

                    int count = 0;
                    count = Convert.ToInt32(Session["CEDPreviousEmployerCount"]);
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

        private void AddNewRows(int count,  string text9 = "", string text10 = "", string text11 = "")
        {
            HtmlTableRow rows = new HtmlTableRow();
            rows.ID = "row" + count;

           

            TextBox txt9 = new TextBox();
            txt9.ID = "txt9" + count.ToString();
            txt9.CssClass += " form-control ";
            txt9.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text9))
                txt9.Text = text9;
            else
                txt9.Text = "MM/DD/YYYY";

           // txt9.TextMode = TextBoxMode.Date;

            HtmlTableCell tc9 = new HtmlTableCell();
            tc9.Controls.Add(txt9);

            TextBox txt10 = new TextBox();
            txt10.ID = "txt10" + count.ToString();
            txt10.CssClass += " form-control ";
            txt10.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text10))
                txt10.Text = text10;
            else
                txt10.Text = "MM/DD/YYYY";

          //  txt10.TextMode = TextBoxMode.Date;

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
                if (Session["CEDPreviousEmployerCount"] != null)
                {
                    count = Convert.ToInt32(Session["CEDPreviousEmployerCount"]);
                }

                if (Session["CEDPreviousEmployerList"] != null)
                {
                    List<CEDPreviousEmployer> CEDPreviousEmployerList = Session["CEDPreviousEmployerList"] as List<CEDPreviousEmployer>;
                    int records = CEDPreviousEmployerList.Count;
                    if (records > 0)
                    {
                        count = count + 1;
                        Session["CEDPreviousEmployerCount"] = count;

                        count = count + records;

                        AddNewRows(count);
                    }
                    else
                    {
                        count++;
                        Session["CEDPreviousEmployerCount"] = count;
                        AddNewRows(count);
                    }

                }
                else
                {
                    count++;
                    Session["CEDPreviousEmployerCount"] = count;
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
                if (Session["CEDPreviousEmployerCount"] != null)
                {
                    count = Convert.ToInt32(Session["CEDPreviousEmployerCount"]);
                }

                if (Session["CEDPreviousEmployerList"] != null)
                {
                    List<CEDPreviousEmployer> CEDPreviousEmployerList = Session["CEDPreviousEmployerList"] as List<CEDPreviousEmployer>;
                    int records = CEDPreviousEmployerList.Count; ;
                    if (count > 0)
                    {
                        int tmpCount = count - 1;
                        Session["CEDPreviousEmployerCount"] = tmpCount;
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
                            //CEDPreviousEmployerDetail cd = db1.CEDPreviousEmployerDetails.Where(x => x.PreviousEmployerDetailId == id).SingleOrDefault();
                            //if (cd != null)
                            //{
                            // db1.CEDPreviousEmployerDetails.Remove(cd);
                            // db1.SaveChanges();
                            BLConsumeAPI_UserControl delete = new BLConsumeAPI_UserControl();
                            delete.DeleteCEDPreviousEmployerDetails(id);
                            CEDPreviousEmployer dcn = CEDPreviousEmployerList.Where(x => x.PreviousEmployerDetailId == id).Single();
                                CEDPreviousEmployerList.Remove(dcn);
                                Session["CEDPreviousEmployerList"] = CEDPreviousEmployerList;
                                DeleteRow(records);
                            }
                        }

                    }

               // }
                else
                {

                    DeleteRow(count);
                    count--;
                    Session["CEDPreviousEmployerCount"] = count;
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }
    }
}