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
    public partial class UsrPreviousEmploymentDetail : System.Web.UI.UserControl
    {
        log4net.ILog logger1 = log4net.LogManager.GetLogger(typeof(UsrPreviousEmploymentDetail));
        DQFEntities db1 = new DQFEntities();

        public void BindPreviousEmployeementDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            try
            {
                var PreviousEmployeementList = new BLConsumeAPI_UserControl().GetPreviousEmploymentDetail(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                var PreviousEmployeementListUser2 = new BLConsumeAPI_UserControl().GetPreviousEmploymentDetail2(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);

                //List<PreviousEmployeement> PreviousEmployeementList = db1.PreviousEmploymentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //    .Select(x => new PreviousEmployeement
                //    {
                //        EmployerName = x.EmployerName,
                //        EmploymentEndDate = x.EmploymentEndDate,
                //        EmploymentStartDate = x.EmploymentStartDate,
                //        PreviousEmploymentDetailId = x.PreviousEmploymentDetailId,
                //        TempCPScreenDataID = x.TempCPScreenDataID,
                //        UserId = x.UserId
                //    }).OrderBy(x => x.PreviousEmploymentDetailId).ToList();
                if (PreviousEmployeementList != null)
                {
                    if (PreviousEmployeementList.Count > 0)
                    {
                        Session["PreviousEmployeementList"] = PreviousEmployeementList;
                    }
                }

                //List<PreviousEmployeement> PreviousEmployeementListUser2 = db1.PreviousEmploymentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId != UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //   .Select(x => new PreviousEmployeement
                //   {
                //       EmployerName = x.EmployerName,
                //       EmploymentEndDate = x.EmploymentEndDate,
                //       EmploymentStartDate = x.EmploymentStartDate,
                //       PreviousEmploymentDetailId = x.PreviousEmploymentDetailId,
                //       TempCPScreenDataID = x.TempCPScreenDataID,
                //       UserId = x.UserId
                //   }).OrderBy(x => x.PreviousEmploymentDetailId).ToList();
                if (PreviousEmployeementListUser2 != null)
                {
                    if (PreviousEmployeementListUser2.Count > 0)
                    {
                        Session["PreviousEmployeementListUser2"] = PreviousEmployeementListUser2;
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
                if (Session["PreviousEmployeementList"] == null)
                {
                    if (Session["PreviousEmployeementCount"] == null)
                    {
                        //Session["PreviousEmployeementCount"] = 1;
                        //ShowRows(1, 1);
                    }
                    else
                    {
                        int count = 0;
                        count = Convert.ToInt32(Session["PreviousEmployeementCount"]);
                        if (count == 1)
                        {
                            Session["PreviousEmployeementCount"] = 1;
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
                    List<PreviousEmployeement> PreviousEmployeementList = Session["PreviousEmployeementList"] as List<PreviousEmployeement>;
                    int records = PreviousEmployeementList.Count; ;

                    int count = 0;
                    count = Convert.ToInt32(Session["PreviousEmployeementCount"]);
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

        private void AddNewRows(int count, string text1 = "", string text2 = "", string text3 = "")
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
            txt2.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text2))
                txt2.Text = text2;
            else
                txt2.Text = "MM/DD/YYYY";

           // txt2.TextMode = TextBoxMode.Date;

            HtmlTableCell tc2 = new HtmlTableCell();
            tc2.Controls.Add(txt2);

            TextBox txt3 = new TextBox();
            txt3.ID = "txt3" + count.ToString();
            txt3.CssClass += " form-control ";
            txt3.CssClass += " myclass ";
            if (!string.IsNullOrEmpty(text3))
                txt3.Text = text3;
            else
                txt3.Text = "MM/DD/YYYY";

           // txt3.TextMode = TextBoxMode.Date;

            HtmlTableCell tc3 = new HtmlTableCell();
            tc3.Controls.Add(txt3);


            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);

            tbl.Rows.Add(rows);

        }

        protected void btnAddRows_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (Session["PreviousEmployeementCount"] != null)
                {
                    count = Convert.ToInt32(Session["PreviousEmployeementCount"]);
                }

                if (Session["PreviousEmployeementList"] != null)
                {
                    List<PreviousEmployeement> PreviousEmployeementList = Session["PreviousEmployeementList"] as List<PreviousEmployeement>;
                    int records = PreviousEmployeementList.Count;
                    if (records > 0)
                    {
                        count = count + 1;
                        Session["PreviousEmployeementCount"] = count;

                        count = count + records;

                        AddNewRows(count);
                    }
                    else
                    {
                        count++;
                        Session["PreviousEmployeementCount"] = count;
                        AddNewRows(count);
                    }

                }
                else
                {
                    count++;
                    Session["PreviousEmployeementCount"] = count;
                    AddNewRows(count);
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmPreviousEmploymentDetails();", true);
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
                if (Session["PreviousEmployeementCount"] != null)
                {
                    count = Convert.ToInt32(Session["PreviousEmployeementCount"]);
                }

                if (Session["PreviousEmployeementList"] != null)
                {
                    List<PreviousEmployeement> PreviousEmployeementList = Session["PreviousEmployeementList"] as List<PreviousEmployeement>;
                    int records = PreviousEmployeementList.Count; ;
                    if (count > 0)
                    {
                        int tmpCount = count - 1;
                        Session["PreviousEmployeementCount"] = tmpCount;
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
                            //var cd = new BLConsumeAPI_UserControl().GetPreviousEmploymentDetailid(id);
                            // PreviousEmploymentDetail cd = db1.PreviousEmploymentDetails.Where(x => x.PreviousEmploymentDetailId == id).SingleOrDefault();
                            //if (cd != null)
                            //{
                                //db1.PreviousEmploymentDetails.Remove(cd);
                                //db1.SaveChanges();
                                BLConsumeAPI_UserControl delete = new BLConsumeAPI_UserControl();
                                delete.DeletePreviousEmploymentDetailid(id);
                                PreviousEmployeement dcn = PreviousEmployeementList.Where(x => x.PreviousEmploymentDetailId == id).Single();
                                PreviousEmployeementList.Remove(dcn);
                                Session["PreviousEmployeementList"] = PreviousEmployeementList;
                                DeleteRow(records);
                           // }
                        }

                    }

                }
                else
                {

                    DeleteRow(count);
                    count--;
                    Session["PreviousEmployeementCount"] = count;
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }
    }
}