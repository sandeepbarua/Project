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
    public partial class UsrDriverLicenseStatus : System.Web.UI.UserControl
    {
        log4net.ILog logger1 = log4net.LogManager.GetLogger(typeof(UsrDriverLicenseStatus));
        DQFEntities db1 = new DQFEntities();

        public void BindDriverLicenseStatusDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            try
            {
                var DriverLicenseStatusList = new BLConsumeAPI_UserControl().GetDriverLicenseStatusDetail(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);
                var DriverLicenseStatusListUser2 = new BLConsumeAPI_UserControl().GetDriverLicenseStatusDetail(UserId, DocumentTypeId, TempCPScreenDataID, CountFaxId);

                //List<DriverLicenseStatus> DriverLicenseStatusList = db1.DriverLicenseStatusDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //    .Select(x => new DriverLicenseStatus
                //    {
                //        DriverLicenseStatusId = x.DriverLicenseStatusId,
                //        DriverLicenseStatusStatement = x.DriverLicenseStatusStatement,
                //        TempCPScreenDataID = x.TempCPScreenDataID,
                //        UserId = x.UserId,
                //        CountForFaxId = x.CountForFaxId
                //    }).OrderBy(x => x.DriverLicenseStatusId).ToList();
                if (DriverLicenseStatusList != null)
                {
                    if (DriverLicenseStatusList.Count > 0)
                    {
                        Session["DriverLicenseStatusList"] = DriverLicenseStatusList;
                    }
                }

                //List<DriverLicenseStatus> DriverLicenseStatusListUser2 = db1.DriverLicenseStatusDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId != UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == CountFaxId)
                //   .Select(x => new DriverLicenseStatus
                //   {
                //       DriverLicenseStatusId = x.DriverLicenseStatusId,
                //       DriverLicenseStatusStatement = x.DriverLicenseStatusStatement,
                //       TempCPScreenDataID = x.TempCPScreenDataID,
                //       UserId = x.UserId,
                //       CountForFaxId = x.CountForFaxId
                //   }).OrderBy(x => x.DriverLicenseStatusId).ToList();
                if (DriverLicenseStatusListUser2 != null)
                {
                    if (DriverLicenseStatusListUser2.Count > 0)
                    {
                        Session["DriverLicenseStatusListUser2"] = DriverLicenseStatusListUser2;
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
                if (Session["DriverLicenseStatusList"] == null)
                {
                    if (Session["DriverLicenseStatusCount"] == null)
                    {
                        Session["DriverLicenseStatusCount"] = 1;
                        ShowRows(1, 1);
                    }
                    else
                    {
                        int count = 0;
                        count = Convert.ToInt32(Session["DriverLicenseStatusCount"]);
                        if (count == 1)
                        {
                            Session["DriverLicenseStatusCount"] = 1;
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
                    List<DriverLicenseStatus> DriverLicenseStatusList = Session["DriverLicenseStatusList"] as List<DriverLicenseStatus>;
                    int records = DriverLicenseStatusList.Count; ;

                    int count = 0;
                    count = Convert.ToInt32(Session["DriverLicenseStatusCount"]);
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




        private void AddNewRows(int count, string text1 = "")
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

            rows.Cells.Add(tc1);
            
            tbl.Rows.Add(rows);

        }

        protected void btnAddRows_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (Session["DriverLicenseStatusCount"] != null)
                {
                    count = Convert.ToInt32(Session["DriverLicenseStatusCount"]);
                }

                if (Session["DriverLicenseStatusList"] != null)
                {
                    List<DriverLicenseStatus> DriverLicenseStatusList = Session["DriverLicenseStatusList"] as List<DriverLicenseStatus>;
                    int records = DriverLicenseStatusList.Count;
                    if (records > 0)
                    {
                        count = count + 1;
                        Session["DriverLicenseStatusCount"] = count;

                        count = count + records;

                        AddNewRows(count);
                    }
                    else
                    {
                        count++;
                        Session["DriverLicenseStatusCount"] = count;
                        AddNewRows(count);
                    }

                }
                else
                {
                    count++;
                    Session["DriverLicenseStatusCount"] = count;
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
                if (Session["DriverLicenseStatusCount"] != null)
                {
                    count = Convert.ToInt32(Session["DriverLicenseStatusCount"]);
                }

                if (Session["DriverLicenseStatusList"] != null)
                {
                    List<DriverLicenseStatus> DriverLicenseStatusList = Session["DriverLicenseStatusList"] as List<DriverLicenseStatus>;
                    int records = DriverLicenseStatusList.Count; ;
                    if (count > 0)
                    {
                        int tmpCount = count - 1;
                        Session["DriverLicenseStatusCount"] = tmpCount;
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
                            //DriverLicenseStatusDetail cd = db1.DriverLicenseStatusDetails.Where(x => x.DriverLicenseStatusId == id).SingleOrDefault();
                            //if (cd != null)
                            //{
                            //    db1.DriverLicenseStatusDetails.Remove(cd);
                            //    db1.SaveChanges();
                            BLConsumeAPI_UserControl delete = new BLConsumeAPI_UserControl();
                            delete.DeleteDriverLicenseStatusDetail(id);
                            DriverLicenseStatus dcn = DriverLicenseStatusList.Where(x => x.DriverLicenseStatusId == id).Single();
                                DriverLicenseStatusList.Remove(dcn);
                                Session["DriverLicenseStatusList"] = DriverLicenseStatusList;
                                DeleteRow(records);
                           // }
                        }

                    }

                }
                else
                {

                    DeleteRow(count);
                    count--;
                    Session["DriverLicenseStatusCount"] = count;
                }
            }
            catch (Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }
    }
}