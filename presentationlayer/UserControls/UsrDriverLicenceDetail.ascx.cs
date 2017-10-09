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
    public partial class UsrDriverLicenceDetail : System.Web.UI.UserControl
    {
        log4net.ILog logger1 = log4net.LogManager.GetLogger(typeof(UsrDriverLicenceDetail));
        DQFEntities db1 = new DQFEntities();

        public void BindDriverLicenceDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            try
            {
                var DriverLicenceList = new BLConsumeAPI_UserControl().GetDriverLicenceList(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                var DriverLicenceListUser2 = new BLConsumeAPI_UserControl().GetDriverLicenceList(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);

                //List<DriverLicence> DriverLicenceList = db1.DriverLicenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //    .Select(x => new DriverLicence
                //    {
                //        DriverLicenceId = x.DriverLicenceId,
                //        Endorsement = x.Endorsement,
                //        Restriction = x.Restriction,
                //        TempCPScreenDataID = x.TempCPScreenDataID,
                //        UserId = x.UserId
                //    }).OrderBy(x => x.DriverLicenceId).ToList();
                if (DriverLicenceList != null)
                {
                    if (DriverLicenceList.Count > 0)
                    {
                        Session["DriverLicenceList"] = DriverLicenceList;
                    }
                }

                //List<DriverLicence> DriverLicenceListUser2 = db1.DriverLicenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId != UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //   .Select(x => new DriverLicence
                //   {
                //       DriverLicenceId = x.DriverLicenceId,
                //       Endorsement = x.Endorsement,
                //       Restriction = x.Restriction,
                //       TempCPScreenDataID = x.TempCPScreenDataID,
                //       UserId = x.UserId
                //   }).OrderBy(x => x.DriverLicenceId).ToList();
                if (DriverLicenceListUser2 != null)
                {
                    if (DriverLicenceListUser2.Count > 0)
                    {
                        Session["DriverLicenceListUser2"] = DriverLicenceListUser2;
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
                if (Session["DriverLicenceList"] == null)
                {
                    if (Session["DriverLicenceCount"] == null)
                    {
                       // Session["DriverLicenceCount"] = 1;
                        //ShowRows(1, 1);
                    }
                    else
                    {
                        int count = 0;
                        count = Convert.ToInt32(Session["DriverLicenceCount"]);
                        if (count == 1)
                        {
                            Session["DriverLicenceCount"] = 1;
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
                    List<DriverLicence> DriverLicenceList = Session["DriverLicenceList"] as List<DriverLicence>;
                    int records = DriverLicenceList.Count; ;

                    int count = 0;
                    count = Convert.ToInt32(Session["DriverLicenceCount"]);
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
        
        private void AddNewRows(int count, string text1 = "", string text2 = "")
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
          

            rows.Cells.Add(tc1);
            rows.Cells.Add(tc2);
          
            tbl.Rows.Add(rows);

        }

        protected void btnAddRows_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (Session["DriverLicenceCount"] != null)
                {
                    count = Convert.ToInt32(Session["DriverLicenceCount"]);
                }

                if (Session["DriverLicenceList"] != null)
                {
                    List<DriverLicence> DriverLicenceList = Session["DriverLicenceList"] as List<DriverLicence>;
                    int records = DriverLicenceList.Count;
                    if (records > 0)
                    {
                        count = count + 1;
                        Session["DriverLicenceCount"] = count;

                        count = count + records;

                        AddNewRows(count);
                    }
                    else
                    {
                        count++;
                        Session["DriverLicenceCount"] = count;
                        AddNewRows(count);
                    }

                }
                else
                {
                    count++;
                    Session["DriverLicenceCount"] = count;
                    AddNewRows(count);
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmDriverLicenceDetails();", true);
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
                if (Session["DriverLicenceCount"] != null)
                {
                    count = Convert.ToInt32(Session["DriverLicenceCount"]);
                }

                if (Session["DriverLicenceList"] != null)
                {
                    List<DriverLicence> DriverLicenceList = Session["DriverLicenceList"] as List<DriverLicence>;
                    int records = DriverLicenceList.Count; ;
                    if (count > 0)
                    {
                        int tmpCount = count - 1;
                        Session["DriverLicenceCount"] = tmpCount;
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
                           // var cd = new BLConsumeAPI_UserControl().GetDriverLicenceListid(id);
                           // DriverLicenceDetail cd = db1.DriverLicenceDetails.Where(x => x.DriverLicenceId == id).SingleOrDefault();
                           // if (cd != null)
                           // {
                                //db1.DriverLicenceDetails.Remove(cd);
                                //db1.SaveChanges();
                                BLConsumeAPI_UserControl delete = new BLConsumeAPI_UserControl();
                                delete.DeleteDriverLicenceListid(id);

                                DriverLicence dcn = DriverLicenceList.Where(x => x.DriverLicenceId == id).Single();
                                DriverLicenceList.Remove(dcn);
                                Session["DriverLicenceList"] = DriverLicenceList;
                                DeleteRow(records);
                           // }
                        }

                    }

                }
                else
                {

                    DeleteRow(count);
                    count--;
                    Session["DriverLicenceCount"] = count;
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }
    }
}