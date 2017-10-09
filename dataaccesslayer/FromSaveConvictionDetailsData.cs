using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class FromSaveConvictionDetailsData
    {
        private void SaveConvictionDetailsData()
        {
            using (var dpfdatabase = new DQFEntities())
            {
                int count = 0;
                int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
                if (Session["ConvictionDetails"] != null)
                {
                    count = Convert.ToInt32(Session["ConvictionDetails"]);
                    if (Session["DriverConvictionList"] != null)
                    {
                        List<DriverConviction> DriverConvictionList = Session["DriverConvictionList"] as List<DriverConviction>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = new BLConsumeApi().SelectConvictionsDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.ConvictionDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deleteMlConvictionsDetails(item.ConvictionDetailsId);
                            }
                            //dpfdatabase.ConvictionDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();
                        string str4 = "txt4" + i.ToString();
                        string txt1 = ((TextBox)ConvictionDetails1.FindControl(str1)).Text;
                        // txt1 = DateTime.ParseExact(txt1, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                        string txt2 = ((TextBox)ConvictionDetails1.FindControl(str2)).Text;

                        string txt3 = ((TextBox)ConvictionDetails1.FindControl(str3)).Text;
                        string txt4 = ((TextBox)ConvictionDetails1.FindControl(str4)).Text;

                        ConvictionDetail cd = new ConvictionDetail();
                        cd.DateOfConviction = txt1;
                        cd.Offense = txt2;
                        cd.Location = txt3;
                        cd.TypeOfVehicleOperated = txt4;
                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        dpfdatabase.ConvictionDetails.Add(cd);
                        dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)ConvictionDetails1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
                else if (Session["DriverConvictionList"] != null)
                {

                    if (Session["DriverConvictionList"] != null)
                    {
                        List<DriverConviction> DriverConvictionList = Session["DriverConvictionList"] as List<DriverConviction>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = dpfdatabase.ConvictionDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            dpfdatabase.ConvictionDetails.RemoveRange(prevRecord);
                            dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();
                        string str4 = "txt4" + i.ToString();
                        string txt1 = ((TextBox)ConvictionDetails1.FindControl(str1)).Text;
                        // txt1 = DateTime.ParseExact(txt1, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                        string txt2 = ((TextBox)ConvictionDetails1.FindControl(str2)).Text;

                        string txt3 = ((TextBox)ConvictionDetails1.FindControl(str3)).Text;
                        string txt4 = ((TextBox)ConvictionDetails1.FindControl(str4)).Text;

                        MlConvictionDetail cd = new MlConvictionDetail();
                        cd.DateOfConviction = txt1;
                        cd.Offense = txt2;
                        cd.Location = txt3;
                        cd.TypeOfVehicleOperated = txt4;
                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;


                        new BLConsumeApi().insertConvictionsDetails(cd);
                        //dpfdatabase.ConvictionDetails.Add(cd);
                        //dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)ConvictionDetails1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
            }
        }
        //Save Date for DriverLicenceDetails
        private void SaveDriverLicenceData()
        {
            using (var dpfdatabase = new DQFEntities())
            {
                int count = 0;
                int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
                if (Session["DriverLicenceCount"] != null)
                {
                    count = Convert.ToInt32(Session["DriverLicenceCount"]);
                    if (Session["DriverLicenceList"] != null)
                    {
                        List<DriverLicence> DriverConvictionList = Session["DriverLicenceList"] as List<DriverLicence>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = new BLConsumeApi().SelectMlDriverLicenceDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.DriverLicenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deleteMlDriverLicenceDetail(item.DriverLicenceId);
                            }
                            //dpfdatabase.DriverLicenceDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();

                        string txt1 = ((TextBox)TgDriverLicence1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgDriverLicence1.FindControl(str2)).Text;


                        MlDriverLicenceDetail cd = new MlDriverLicenceDetail();
                        cd.Restriction = txt1;
                        cd.Endorsement = txt2;
                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        new BLConsumeApi().insertDriverLicenceDetail(cd);
                        //dpfdatabase.DriverLicenceDetails.Add(cd);
                        //dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)TgDriverLicence1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
                else if (Session["DriverLicenceList"] != null)
                {

                    if (Session["DriverLicenceList"] != null)
                    {
                        List<DriverLicence> DriverConvictionList = Session["DriverLicenceList"] as List<DriverLicence>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = new BLConsumeApi().SelectMlDriverLicenceDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.DriverLicenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deleteMlDriverLicenceDetail(item.DriverLicenceId);
                            }
                            //dpfdatabase.DriverLicenceDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();

                        string txt1 = ((TextBox)TgDriverLicence1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgDriverLicence1.FindControl(str2)).Text;


                        MlDriverLicenceDetail cd = new MlDriverLicenceDetail();
                        cd.Restriction = txt1;
                        cd.Endorsement = txt2;

                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        //dpfdatabase.DriverLicenceDetails.Add(cd);
                        //dpfdatabase.SaveChanges();
                        new BLConsumeApi().insertDriverLicenceDetail(cd);

                    }
                    HtmlTable tbl = (HtmlTable)ConvictionDetails1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
            }
        }
        //Save Data for Previous Employeemnt Details
        private void SavePreviousEmployeementData()
        {
            using (var dpfdatabase = new DQFEntities())
            {
                int count = 0;
                int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
                if (Session["PreviousEmployeementCount"] != null)
                {
                    count = Convert.ToInt32(Session["PreviousEmployeementCount"]);
                    if (Session["PreviousEmployeementList"] != null)
                    {
                        List<PreviousEmployeement> DriverConvictionList = Session["PreviousEmployeementList"] as List<PreviousEmployeement>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = new BLConsumeApi().SelectPreviousEmploymentDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.PreviousEmploymentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deletePreviousEmploymentDetail(item.PreviousEmploymentDetailId);
                            }
                            //dpfdatabase.PreviousEmploymentDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();

                        string txt1 = ((TextBox)TgPreviousEmployment1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgPreviousEmployment1.FindControl(str2)).Text;
                        // txt2 = DateTime.ParseExact(txt2, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                        string txt3 = ((TextBox)TgPreviousEmployment1.FindControl(str3)).Text;
                        // txt3 = DateTime.ParseExact(txt3, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");


                        MlPreviousEmploymentDetail cd = new MlPreviousEmploymentDetail();
                        cd.EmployerName = txt1;
                        cd.EmploymentStartDate = txt2;
                        cd.EmploymentEndDate = txt3;
                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        new BLConsumeApi().insertPreviousEmploymentDetail(cd);
                        //dpfdatabase.PreviousEmploymentDetails.Add(cd);
                        //dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
                else if (Session["PreviousEmployeementList"] != null)
                {

                    if (Session["PreviousEmployeementList"] != null)
                    {
                        List<PreviousEmployeement> DriverConvictionList = Session["PreviousEmployeementList"] as List<PreviousEmployeement>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = new BLConsumeApi().SelectPreviousEmploymentDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.PreviousEmploymentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deletePreviousEmploymentDetail(item.PreviousEmploymentDetailId);
                            }
                            //dpfdatabase.PreviousEmploymentDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();

                        string txt1 = ((TextBox)TgPreviousEmployment1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgPreviousEmployment1.FindControl(str2)).Text;
                        //  txt2 = DateTime.ParseExact(txt2, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                        string txt3 = ((TextBox)TgPreviousEmployment1.FindControl(str3)).Text;
                        //  txt3 = DateTime.ParseExact(txt3, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");


                        MlPreviousEmploymentDetail cd = new MlPreviousEmploymentDetail();
                        cd.EmployerName = txt1;
                        cd.EmploymentStartDate = txt2;
                        cd.EmploymentEndDate = txt3;
                        cd.CountForFaxId = countForFaxId;

                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);

                        new BLConsumeApi().insertPreviousEmploymentDetail(cd);
                        //dpfdatabase.PreviousEmploymentDetails.Add(cd);
                        //dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
            }
        }
        private void SavePreviousEmployerData()
        {
            using (var dpfdatabase = new DQFEntities())
            {
                int count = 0;
                int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
                if (Session["PreviousEmployerCount"] != null)
                {
                    count = Convert.ToInt32(Session["PreviousEmployerCount"]);
                    if (Session["PreviousEmployerList"] != null)
                    {
                        List<PreviousEmployer> DriverConvictionList = Session["PreviousEmployerList"] as List<PreviousEmployer>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    BLConsumeApi prev = new BLConsumeApi();
                    var prevRecord = prev.SelectPreviousEmployerDetailsByAllIds(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);

                    //var prevRecord = dpfdatabase.PreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                prev.deletePreviousEmployerDetailId(item.PreviousEmployerDetailId);
                            }
                            //dpfdatabase.PreviousEmployerDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();
                        string str4 = "txt4" + i.ToString();
                        string str5 = "txt5" + i.ToString();
                        string str6 = "txt6" + i.ToString();
                        string str7 = "txt7" + i.ToString();
                        string str8 = "txt8" + i.ToString();

                        string txt1 = ((TextBox)TgPreviousEmployer1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgPreviousEmployer1.FindControl(str2)).Text;
                        string txt3 = ((TextBox)TgPreviousEmployer1.FindControl(str3)).Text;
                        string txt4 = ((TextBox)TgPreviousEmployer1.FindControl(str4)).Text;
                        string txt5 = ((TextBox)TgPreviousEmployer1.FindControl(str5)).Text;
                        string txt6 = ((TextBox)TgPreviousEmployer1.FindControl(str6)).Text;
                        string txt7 = ((TextBox)TgPreviousEmployer1.FindControl(str7)).Text;
                        string txt8 = ((TextBox)TgPreviousEmployer1.FindControl(str8)).Text;

                        MlPreviousEmployerDetail cd = new MlPreviousEmployerDetail();
                        cd.PreviousEmployerName = txt1;
                        cd.PreviousEmployerStreetAddress = txt2;
                        cd.PreviousEmployerCity = txt3;
                        cd.PreviousEmployerState = txt4;
                        cd.PreviousEmployerZipcode = txt5;
                        cd.PreviousEmploymentStartDate = txt6;
                        cd.PreviousEmploymentEndDate = txt7;
                        cd.ReasonForLeavingPreviousEmployments = txt8;
                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        BLConsumeApi insertOperation = new BLConsumeApi();
                        insertOperation.insertPreviousEmployerDetailId(cd);


                        //dpfdatabase.PreviousEmployerDetails.Add(cd);
                        //dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
                else if (Session["PreviousEmployerList"] != null)
                {

                    if (Session["PreviousEmployerList"] != null)
                    {
                        List<PreviousEmployer> DriverConvictionList = Session["PreviousEmployerList"] as List<PreviousEmployer>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    BLConsumeApi prevs = new BLConsumeApi();
                    var prevRecord = prevs.SelectPreviousEmployerDetailsByAllIds(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.PreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                prevs.deletePreviousEmployerDetailId(item.PreviousEmployerDetailId);
                            }
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();
                        string str4 = "txt4" + i.ToString();
                        string str5 = "txt5" + i.ToString();
                        string str6 = "txt6" + i.ToString();
                        string str7 = "txt7" + i.ToString();
                        string str8 = "txt8" + i.ToString();

                        string txt1 = ((TextBox)TgPreviousEmployer1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgPreviousEmployer1.FindControl(str2)).Text;
                        string txt3 = ((TextBox)TgPreviousEmployer1.FindControl(str3)).Text;
                        string txt4 = ((TextBox)TgPreviousEmployer1.FindControl(str4)).Text;
                        string txt5 = ((TextBox)TgPreviousEmployer1.FindControl(str5)).Text;
                        string txt6 = ((TextBox)TgPreviousEmployer1.FindControl(str6)).Text;
                        string txt7 = ((TextBox)TgPreviousEmployer1.FindControl(str7)).Text;
                        string txt8 = ((TextBox)TgPreviousEmployer1.FindControl(str8)).Text;

                        MlPreviousEmployerDetail cd = new MlPreviousEmployerDetail();
                        cd.PreviousEmployerName = txt1;
                        cd.PreviousEmployerStreetAddress = txt2;
                        cd.PreviousEmployerCity = txt3;
                        cd.PreviousEmployerState = txt4;
                        cd.PreviousEmployerZipcode = txt5;
                        cd.PreviousEmploymentStartDate = txt6;
                        cd.PreviousEmploymentEndDate = txt7;
                        cd.ReasonForLeavingPreviousEmployments = txt8;
                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        BLConsumeApi insertOperation = new BLConsumeApi();
                        insertOperation.insertPreviousEmployerDetailId(cd);
                        //dpfdatabase.PreviousEmployerDetails.Add(cd);
                        //dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)TgPreviousEmployment1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
            }
        }
        //Save Data for Previous CED Employer Details
        private void SaveCEDPreviousEmployerData()
        {
            using (var dpfdatabase = new DQFEntities())
            {
                int count = 0;
                int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
                if (Session["CEDPreviousEmployerCount"] != null)
                {
                    count = Convert.ToInt32(Session["CEDPreviousEmployerCount"]);
                    if (Session["CEDPreviousEmployerList"] != null)
                    {
                        List<CEDPreviousEmployer> DriverConvictionList = Session["CEDPreviousEmployerList"] as List<CEDPreviousEmployer>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    var prevRecord = new BLConsumeApi().SelectCEDPreviousEmployerDetailsByAllIds(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.CEDPreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deleteCEDPreviousEmployerDetailId(item.PreviousEmployerDetailId);
                            }
                            //dpfdatabase.CEDPreviousEmployerDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {

                        string str9 = "txt9" + i.ToString();
                        string str10 = "txt10" + i.ToString();
                        string str11 = "txt11" + i.ToString();


                        string txt9 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).Text;
                        //txt9 = DateTime.ParseExact(txt9, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                        string txt10 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).Text;
                        // txt10 = DateTime.ParseExact(txt10, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                        string txt11 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).Text;

                        MlCEDPreviousEmployerDetail cd = new MlCEDPreviousEmployerDetail();

                        cd.EmploymentGapFrom = txt9;
                        cd.EmploymentGapTo = txt10;
                        cd.ReasonForEmploymentGap = txt11;
                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        new BLConsumeApi().insertCedPreviousEmployerDetailId(cd);
                        //dpfdatabase.CEDPreviousEmployerDetails.Add(cd);
                        //dpfdatabase.SaveChanges();
                    }
                    HtmlTable tbl = (HtmlTable)TgCEDPreviousEmployer1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
                else if (Session["CEDPreviousEmployerList"] != null)
                {

                    if (Session["CEDPreviousEmployerList"] != null)
                    {
                        List<CEDPreviousEmployer> DriverConvictionList = Session["CEDPreviousEmployerList"] as List<CEDPreviousEmployer>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    var prevRecord = new BLConsumeApi().SelectCEDPreviousEmployerDetailsByAllIds(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.CEDPreviousEmployerDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deleteCEDPreviousEmployerDetailId(item.PreviousEmployerDetailId);
                            }
                            //dpfdatabase.CEDPreviousEmployerDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {

                        string str9 = "txt9" + i.ToString();
                        string str10 = "txt10" + i.ToString();
                        string str11 = "txt11" + i.ToString();


                        string txt9 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str9)).Text;
                        //  txt9 = DateTime.ParseExact(txt9, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                        string txt10 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str10)).Text;
                        //    txt10 = DateTime.ParseExact(txt10, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                        string txt11 = ((TextBox)TgCEDPreviousEmployer1.FindControl(str11)).Text;

                        MlCEDPreviousEmployerDetail cd = new MlCEDPreviousEmployerDetail();

                        cd.EmploymentGapFrom = txt9;
                        cd.EmploymentGapTo = txt10;
                        cd.ReasonForEmploymentGap = txt11;
                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        new BLConsumeApi().insertCedPreviousEmployerDetailId(cd);

                        //dpfdatabase.CEDPreviousEmployerDetails.Add(cd);
                        // dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)TgCEDPreviousEmployer1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
            }
        }
        //Save Data for Previous Employer Details
        private void SaveCurrentResidenceData()
        {
            using (var dpfdatabase = new DQFEntities())
            {
                int count = 0;
                int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
                if (Session["CurrentResidenceCount"] != null)
                {
                    count = Convert.ToInt32(Session["CurrentResidenceCount"]);
                    if (Session["CurrentResidenceList"] != null)
                    {
                        List<CurrentResidence> DriverConvictionList = Session["CurrentResidenceList"] as List<CurrentResidence>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = new BLConsumeApi().SelectCurrentResidenceDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.CurrentResidenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deleteCurrentResidenceDetail(item.CurrentResidenceDetailID);
                            }
                            //dpfdatabase.CurrentResidenceDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();
                        string str4 = "txt4" + i.ToString();
                        string str5 = "txt5" + i.ToString();


                        string txt1 = ((TextBox)TgCurrentResidence1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgCurrentResidence1.FindControl(str2)).Text;
                        string txt3 = ((TextBox)TgCurrentResidence1.FindControl(str3)).Text;
                        string txt4 = ((TextBox)TgCurrentResidence1.FindControl(str4)).Text;
                        string txt5 = ((TextBox)TgCurrentResidence1.FindControl(str5)).Text;


                        MlCurrentResidenceDetails cd = new MlCurrentResidenceDetails();
                        cd.CurrentResidenceStreetAddress = txt1;
                        cd.CurrentResidenceCity = txt2;
                        cd.CurrentResidenceState = txt3;
                        cd.CurrentResidenceZipcode = txt4;
                        cd.CurrentResidenceDuration = txt5;

                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        new BLConsumeApi().insertCurrentResidenceDetail(cd);
                        //dpfdatabase.CurrentResidenceDetails.Add(cd);
                        //dpfdatabase.SaveChanges();
                    }
                    HtmlTable tbl = (HtmlTable)TgCurrentResidence1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
                else if (Session["CurrentResidenceList"] != null)
                {

                    if (Session["CurrentResidenceList"] != null)
                    {
                        List<CurrentResidence> DriverConvictionList = Session["CurrentResidenceList"] as List<CurrentResidence>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = new BLConsumeApi().SelectCurrentResidenceDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.CurrentResidenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deleteCurrentResidenceDetail(item.CurrentResidenceDetailID);

                            }
                            //dpfdatabase.CurrentResidenceDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();
                        string str4 = "txt4" + i.ToString();
                        string str5 = "txt5" + i.ToString();



                        string txt1 = ((TextBox)TgCurrentResidence1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgCurrentResidence1.FindControl(str2)).Text;
                        string txt3 = ((TextBox)TgCurrentResidence1.FindControl(str3)).Text;
                        string txt4 = ((TextBox)TgCurrentResidence1.FindControl(str4)).Text;
                        string txt5 = ((TextBox)TgCurrentResidence1.FindControl(str5)).Text;


                        MlCurrentResidenceDetails cd = new MlCurrentResidenceDetails();
                        cd.CurrentResidenceStreetAddress = txt1;
                        cd.CurrentResidenceCity = txt2;
                        cd.CurrentResidenceState = txt3;
                        cd.CurrentResidenceZipcode = txt4;
                        cd.CurrentResidenceDuration = txt5;

                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;
                        new BLConsumeApi().insertCurrentResidenceDetail(cd);
                        //dpfdatabase.CurrentResidenceDetails.Add(cd);
                        //dpfdatabase.SaveChanges();
                    }
                    HtmlTable tbl = (HtmlTable)TgCurrentResidence1.FindControl("tbl");
                    tbl.Rows.Clear();
                }
            }
        }
        //Save Data for Previous Employer Details
        private void SavePreviousResidenceData()
        {
            using (var dpfdatabase = new DQFEntities())
            {
                int count = 0;
                int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
                if (Session["PreviousResidenceCount"] != null)
                {
                    count = Convert.ToInt32(Session["PreviousResidenceCount"]);
                    if (Session["PreviousResidenceList"] != null)
                    {
                        List<PreviousResidence> DriverConvictionList = Session["PreviousResidenceList"] as List<PreviousResidence>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = new BLConsumeApi().SelectMlPreviousResidenceDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.PreviousResidenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deletePreviousResidenceDetail(item.PreviousResidenceDetailID);
                            }

                            //dpfdatabase.PreviousResidenceDetails.RemoveRange(prevRecord);
                            // dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();
                        string str4 = "txt4" + i.ToString();
                        string str5 = "txt5" + i.ToString();



                        string txt1 = ((TextBox)TgPreviousResidence1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgPreviousResidence1.FindControl(str2)).Text;
                        string txt3 = ((TextBox)TgPreviousResidence1.FindControl(str3)).Text;
                        string txt4 = ((TextBox)TgPreviousResidence1.FindControl(str4)).Text;
                        string txt5 = ((TextBox)TgPreviousResidence1.FindControl(str5)).Text;


                        MlPreviousResidenceDetail cd = new MlPreviousResidenceDetail();
                        cd.PreviousResidenceStreetAddress = txt1;
                        cd.PreviousResidenceCity = txt2;
                        cd.PreviousResidenceState = txt3;
                        cd.PreviousResidenceZipcode = txt4;
                        cd.PreviousResidenceDuration = txt5;

                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        new BLConsumeApi().insertPreviousResidenceDetail(cd);
                        //dpfdatabase.PreviousResidenceDetails.Add(cd);
                        //dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)TgPreviousResidence1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
                else if (Session["PreviousResidenceList"] != null)
                {

                    if (Session["PreviousResidenceList"] != null)
                    {
                        List<PreviousResidence> DriverConvictionList = Session["PreviousResidenceList"] as List<PreviousResidence>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = new BLConsumeApi().SelectMlPreviousResidenceDetail(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.PreviousResidenceDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deletePreviousResidenceDetail(item.PreviousResidenceDetailID);
                            }
                            //    dpfdatabase.PreviousResidenceDetails.RemoveRange(prevRecord);
                            //    dpfdatabase.SaveChanges();
                            //
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();
                        string str4 = "txt4" + i.ToString();
                        string str5 = "txt5" + i.ToString();

                        string txt1 = ((TextBox)TgPreviousResidence1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgPreviousResidence1.FindControl(str2)).Text;
                        string txt3 = ((TextBox)TgPreviousResidence1.FindControl(str3)).Text;
                        string txt4 = ((TextBox)TgPreviousResidence1.FindControl(str4)).Text;
                        string txt5 = ((TextBox)TgPreviousResidence1.FindControl(str5)).Text;


                        MlPreviousResidenceDetail cd = new MlPreviousResidenceDetail();
                        cd.PreviousResidenceStreetAddress = txt1;
                        cd.PreviousResidenceCity = txt2;
                        cd.PreviousResidenceState = txt3;
                        cd.PreviousResidenceZipcode = txt4;
                        cd.PreviousResidenceDuration = txt5;

                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        new BLConsumeApi().insertPreviousResidenceDetail(cd);
                        //dpfdatabase.PreviousResidenceDetails.Add(cd);
                        //dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)TgPreviousResidence1.FindControl("tbl");
                    tbl.Rows.Clear();


                }
            }
        }
        //Save Data for Previous Employer Details
        //private void SaveTypeOfEquipmentResidenceData()
        //{
        //    using (var dpfdatabase = new DQFEntities())
        //    {
        //        int count = 0;
        //        int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
        //        if (Session["TypeOfEquipmentCount"] != null)
        //        {
        //            count = Convert.ToInt32(Session["TypeOfEquipmentCount"]);
        //            if (Session["TypeOfEquipmentList"] != null)
        //            {
        //                List<TypeOfEquipmentClass> DriverConvictionList = Session["TypeOfEquipmentList"] as List<TypeOfEquipmentClass>;
        //                int records = DriverConvictionList.Count;
        //                count = count + records;
        //            }
        //            int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
        //            int UserId = Convert.ToInt32(Session["UserId"]);
        //            int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

        //            var prevRecord = dpfdatabase.TypeOfEquipmentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
        //            if (prevRecord != null)
        //            {
        //                if (prevRecord.Count > 0)
        //                {
        //                    dpfdatabase.TypeOfEquipmentDetails.RemoveRange(prevRecord);
        //                    dpfdatabase.SaveChanges();
        //                }
        //            }

        //            for (int i = 1; i <= count; i++)
        //            {
        //                string str1 = "txt1" + i.ToString();
        //                string str2 = "txt2" + i.ToString();
        //                string str3 = "txt3" + i.ToString();
        //                string str4 = "txt4" + i.ToString();




        //                string txt1 = ((TextBox)TgTypeOfEquipment1.FindControl(str1)).Text;
        //                string txt2 = ((TextBox)TgTypeOfEquipment1.FindControl(str2)).Text;
        //                string txt3 = ((TextBox)TgTypeOfEquipment1.FindControl(str3)).Text;
        //                string txt4 = ((TextBox)TgTypeOfEquipment1.FindControl(str4)).Text;



        //                TypeOfEquipmentDetail cd = new TypeOfEquipmentDetail();
        //                cd.TypeOfEquipment = txt1;
        //                cd.Miles = txt2;
        //                cd.DrivingFrom = txt3;
        //                cd.DrivingTo = txt4;


        //                cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
        //                cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
        //                cd.UserId = Convert.ToInt32(Session["UserId"]);
        //                cd.CountForFaxId = countForFaxId;

        //                dpfdatabase.TypeOfEquipmentDetails.Add(cd);
        //                dpfdatabase.SaveChanges();

        //            }
        //            HtmlTable tbl = (HtmlTable)TgTypeOfEquipment1.FindControl("tbl");
        //            tbl.Rows.Clear();

        //        }
        //        else if (Session["TypeOfEquipmentList"] != null)
        //        {

        //            if (Session["TypeOfEquipmentList"] != null)
        //            {
        //                List<TypeOfEquipmentClass> DriverConvictionList = Session["TypeOfEquipmentList"] as List<TypeOfEquipmentClass>;
        //                int records = DriverConvictionList.Count;
        //                count = count + records;
        //            }
        //            int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
        //            int UserId = Convert.ToInt32(Session["UserId"]);
        //            int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

        //            var prevRecord = dpfdatabase.TypeOfEquipmentDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
        //            if (prevRecord != null)
        //            {
        //                if (prevRecord.Count > 0)
        //                {
        //                    dpfdatabase.TypeOfEquipmentDetails.RemoveRange(prevRecord);
        //                    dpfdatabase.SaveChanges();
        //                }
        //            }

        //            for (int i = 1; i <= count; i++)
        //            {
        //                string str1 = "txt1" + i.ToString();
        //                string str2 = "txt2" + i.ToString();
        //                string str3 = "txt3" + i.ToString();
        //                string str4 = "txt4" + i.ToString();




        //                string txt1 = ((TextBox)TgTypeOfEquipment1.FindControl(str1)).Text;
        //                string txt2 = ((TextBox)TgTypeOfEquipment1.FindControl(str2)).Text;
        //                string txt3 = ((TextBox)TgTypeOfEquipment1.FindControl(str3)).Text;
        //                string txt4 = ((TextBox)TgTypeOfEquipment1.FindControl(str4)).Text;



        //                TypeOfEquipmentDetail cd = new TypeOfEquipmentDetail();
        //                cd.TypeOfEquipment = txt1;
        //                cd.Miles = txt2;
        //                cd.DrivingFrom = txt3;
        //                cd.DrivingTo = txt4;


        //                cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
        //                cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
        //                cd.UserId = Convert.ToInt32(Session["UserId"]);
        //                cd.CountForFaxId = countForFaxId;

        //                dpfdatabase.TypeOfEquipmentDetails.Add(cd);
        //                dpfdatabase.SaveChanges();

        //            }
        //            HtmlTable tbl = (HtmlTable)TgTypeOfEquipment1.FindControl("tbl");
        //            tbl.Rows.Clear();


        //        }
        //    }
        //}
        private void SaveTrafficConvictionsData()
        {
            using (var dpfdatabase = new DQFEntities())
            {
                int count = 0;
                int countForFaxId = Convert.ToInt32(hfCountForFaxId.Value);
                if (Session["TrafficConvictionsCount"] != null)
                {
                    count = Convert.ToInt32(Session["TrafficConvictionsCount"]);
                    if (Session["TrafficConvictionsList"] != null)
                    {
                        List<TrafficConviction> DriverConvictionList = Session["TrafficConvictionsList"] as List<TrafficConviction>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

                    var prevRecord = new BLConsumeApi().SelectTrafficConvictionsDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);
                    //var prevRecord = dpfdatabase.TrafficConvictionsDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deleteMlTrafficConvictionsDetails(item.TrafficConvictionsDetailID);
                            }
                            //dpfdatabase.TrafficConvictionsDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();
                        string str4 = "txt4" + i.ToString();
                        string str5 = "txt5" + i.ToString();



                        string txt1 = ((TextBox)TgTrafficConvictions1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgTrafficConvictions1.FindControl(str2)).Text;
                        string txt3 = ((TextBox)TgTrafficConvictions1.FindControl(str3)).Text;
                        // txt3 = DateTime.ParseExact(txt3, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                        string txt4 = ((TextBox)TgTrafficConvictions1.FindControl(str4)).Text;
                        string txt5 = ((TextBox)TgTrafficConvictions1.FindControl(str5)).Text;


                        MlTrafficConvictionsDetails cd = new MlTrafficConvictionsDetails();
                        cd.Location = txt1;
                        cd.VehicleType = txt2;
                        cd.DateOfConviction = txt3;
                        cd.Charge = txt4;
                        cd.Penalty = txt5;


                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        new BLConsumeApi().insertTrafficConvictionsDetails(cd);

                        //dpfdatabase.TrafficConvictionsDetails.Add(cd);
                        //dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)TgTrafficConvictions1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
                else if (Session["TrafficConvictionsList"] != null)
                {

                    if (Session["TrafficConvictionsList"] != null)
                    {
                        List<TrafficConviction> DriverConvictionList = Session["TrafficConvictionsList"] as List<TrafficConviction>;
                        int records = DriverConvictionList.Count;
                        count = count + records;
                    }
                    int TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);


                    var prevRecord = new BLConsumeApi().SelectTrafficConvictionsDetails(DocumentTypeId, UserId, TempCPScreenDataID, countForFaxId);

                    //var prevRecord = dpfdatabase.TrafficConvictionsDetails.Where(x => x.DocumentTypeId == DocumentTypeId && x.UserId == UserId && x.TempCPScreenDataID == TempCPScreenDataID && x.CountForFaxId == countForFaxId).ToList();
                    if (prevRecord != null)
                    {
                        if (prevRecord.Count > 0)
                        {
                            foreach (var item in prevRecord)
                            {
                                new BLConsumeApi().deleteMlTrafficConvictionsDetails(item.TrafficConvictionsDetailID);
                            }
                            //dpfdatabase.TrafficConvictionsDetails.RemoveRange(prevRecord);
                            //dpfdatabase.SaveChanges();
                        }
                    }

                    for (int i = 1; i <= count; i++)
                    {
                        string str1 = "txt1" + i.ToString();
                        string str2 = "txt2" + i.ToString();
                        string str3 = "txt3" + i.ToString();
                        string str4 = "txt4" + i.ToString();
                        string str5 = "txt5" + i.ToString();



                        string txt1 = ((TextBox)TgTrafficConvictions1.FindControl(str1)).Text;
                        string txt2 = ((TextBox)TgTrafficConvictions1.FindControl(str2)).Text;
                        string txt3 = ((TextBox)TgTrafficConvictions1.FindControl(str3)).Text;
                        // txt3 = DateTime.ParseExact(txt3, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                        string txt4 = ((TextBox)TgTrafficConvictions1.FindControl(str4)).Text;
                        string txt5 = ((TextBox)TgTrafficConvictions1.FindControl(str5)).Text;


                        MlTrafficConvictionsDetails cd = new MlTrafficConvictionsDetails();
                        cd.Location = txt1;
                        cd.VehicleType = txt2;
                        cd.DateOfConviction = txt3;
                        cd.Charge = txt4;
                        cd.Penalty = txt5;


                        cd.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                        cd.TempCPScreenDataID = Convert.ToInt32(hfTempCPScreenDataID.Value);
                        cd.UserId = Convert.ToInt32(Session["UserId"]);
                        cd.CountForFaxId = countForFaxId;

                        new BLConsumeApi().insertTrafficConvictionsDetails(cd);


                        //dpfdatabase.TrafficConvictionsDetails.Add(cd);
                        //dpfdatabase.SaveChanges();

                    }
                    HtmlTable tbl = (HtmlTable)TgTrafficConvictions1.FindControl("tbl");
                    tbl.Rows.Clear();

                }
            }
        }
    }
}
