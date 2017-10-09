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
    public partial class UsrCurrentResidenceDetail : System.Web.UI.UserControl
    {
        log4net.ILog logger1 = log4net.LogManager.GetLogger(typeof(UsrCurrentResidenceDetail));
        DQFEntities db1 = new DQFEntities();

        public void BindCurrentResidenceDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            try
            {
                var CurrentResidenceList = new BLConsumeAPI_UserControl().GetCurrentResidenceDetails(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                var CurrentResidenceListUser2 = new BLConsumeAPI_UserControl().GetCurrentResidenceDetails2(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);

                //List<CurrentResidence> CurrentResidenceList = db1.CurrentResidenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //    .Select(x => new CurrentResidence
                //    {
                //        CurrentResidenceCity = x.CurrentResidenceCity,
                //        CurrentResidenceDuration = x.CurrentResidenceDuration,
                //        CurrentResidenceDetailID = x.CurrentResidenceDetailID,
                //        CurrentResidenceState = x.CurrentResidenceState,
                //        CurrentResidenceStreetAddress = x.CurrentResidenceStreetAddress,
                //        CurrentResidenceZipcode = x.CurrentResidenceZipcode,
                //        TempCPScreenDataID = x.TempCPScreenDataID,
                //        UserId = x.UserId
                //    }).OrderBy(x => x.CurrentResidenceDetailID).ToList();
                if (CurrentResidenceList != null)
                {
                    if (CurrentResidenceList.Count > 0)
                    {
                        Session["CurrentResidenceList"] = CurrentResidenceList;
                    }
                }

                //List<CurrentResidence> CurrentResidenceListUser2 = db1.CurrentResidenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId != UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //   .Select(x => new CurrentResidence
                //   {
                //       CurrentResidenceCity = x.CurrentResidenceCity,
                //       CurrentResidenceDuration = x.CurrentResidenceDuration,
                //       CurrentResidenceDetailID = x.CurrentResidenceDetailID,
                //       CurrentResidenceState = x.CurrentResidenceState,
                //       CurrentResidenceStreetAddress = x.CurrentResidenceStreetAddress,
                //       CurrentResidenceZipcode = x.CurrentResidenceZipcode,
                //       TempCPScreenDataID = x.TempCPScreenDataID,
                //       UserId = x.UserId
                //   }).OrderBy(x => x.CurrentResidenceDetailID).ToList();
                if (CurrentResidenceListUser2 != null)
                {
                    if (CurrentResidenceListUser2.Count > 0)
                    {
                        Session["CurrentResidenceListUser2"] = CurrentResidenceListUser2;
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
                if (Session["CurrentResidenceList"] == null)
                {
                    if (Session["CurrentResidenceCount"] == null)
                    {
                       // Session["CurrentResidenceCount"] = 1;
                        //ShowRows(1, 1);
                    }
                    else
                    {
                        int count = 0;
                        count = Convert.ToInt32(Session["CurrentResidenceCount"]);
                        if (count == 1)
                        {
                            Session["CurrentResidenceCount"] = 1;
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
                    List<CurrentResidence> CurrentResidenceList = Session["CurrentResidenceList"] as List<CurrentResidence>;
                    int records = CurrentResidenceList.Count; ;

                    int count = 0;
                    count = Convert.ToInt32(Session["CurrentResidenceCount"]);
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

        private void AddNewRows(int count, string text1 = "", string text2 = "", string text3 = "", string text4 = "", string text5 = "")
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

           


            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
            rows.Cells.Add(tc3);
            rows.Cells.Add(tc4);
            rows.Cells.Add(tc5);
         


            tbl.Rows.Add(rows);

        }

        protected void btnAddRows_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (Session["CurrentResidenceCount"] != null)
                {
                    count = Convert.ToInt32(Session["CurrentResidenceCount"]);
                }

                if (Session["CurrentResidenceList"] != null)
                {
                    List<CurrentResidence> CurrentResidenceList = Session["CurrentResidenceList"] as List<CurrentResidence>;
                    int records = CurrentResidenceList.Count;
                    if (records > 0)
                    {
                        count = count + 1;
                        Session["CurrentResidenceCount"] = count;

                        count = count + records;

                        AddNewRows(count);
                    }
                    else
                    {
                        count++;
                        Session["CurrentResidenceCount"] = count;
                        AddNewRows(count);
                    }

                }
                else
                {
                    count++;
                    Session["CurrentResidenceCount"] = count;
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
                if (Session["CurrentResidenceCount"] != null)
                {
                    count = Convert.ToInt32(Session["CurrentResidenceCount"]);
                }

                if (Session["CurrentResidenceList"] != null)
                {
                    List<CurrentResidence> CurrentResidenceList = Session["CurrentResidenceList"] as List<CurrentResidence>;
                    int records = CurrentResidenceList.Count; ;
                    if (count > 0)
                    {
                        int tmpCount = count - 1;
                        Session["CurrentResidenceCount"] = tmpCount;
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
                            //var cd = new BLConsumeAPI_UserControl().GetCurrentResidenceDetailsid(id);
                            // CurrentResidenceDetail cd = db1.CurrentResidenceDetails.Where(x => x.CurrentResidenceDetailID == id).SingleOrDefault();
                           // if (cd != null)
                           // {
                                // db1.CurrentResidenceDetails.Remove(cd);
                                // db1.SaveChanges();
                                BLConsumeAPI_UserControl delete = new BLConsumeAPI_UserControl();
                                delete.DeleteCurrentResidenceDetailsid(id);
                                CurrentResidence dcn = CurrentResidenceList.Where(x => x.CurrentResidenceDetailID == id).Single();
                                CurrentResidenceList.Remove(dcn);
                                Session["CurrentResidenceList"] = CurrentResidenceList;
                                DeleteRow(records);
                           // }
                        }

                    }

                }
                else
                {

                    DeleteRow(count);
                    count--;
                    Session["CurrentResidenceCount"] = count;
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }

        }
    }
}