using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace PresentationLayer.Admin
{
    public partial class ExcelErrorLog : System.Web.UI.Page
    {
        log4net.ILog logger1 = log4net.LogManager.GetLogger(typeof(ExcelErrorLog));
        OleDbConnection Econ;
        SqlConnection con;

        string constr, Query, sqlconn;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    if (FileUpload1.FileContent.Length > 0)
                    {
                        string Foldername;
                        string Extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());


                        if (Extension == ".XLS" || Extension == ".XLSX" || Extension == ".xls" || Extension == ".xlsx")
                        {

                            Foldername = Server.MapPath("~/UploadExcel/");

                            FileUpload1.PostedFile.SaveAs(Foldername + filename.ToString());

                            String conStr = "";
                            switch (Extension)
                            {
                                case ".xls": //Excel 97-03
                                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                    "Data Source=" + Foldername + "//" + filename + ";" +
                                    "Extended Properties=Excel 8.0;";
                                    break;

                                case ".xlsx": //Excel 07
                                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                   "Data Source=" + Foldername + "//" + filename + ";" +
                                   "Extended Properties=Excel 8.0;";
                                    break;
                            }
                            Econ = new OleDbConnection();

                            Econ.ConnectionString = conStr;
                            //------
                            //  OleDbCommand ocmd = new OleDbCommand("select * from [Sheet1$]", Econ);
                            Econ.Open();
                            // OleDbDataReader odr = ocmd.ExecuteReader();

                            DataSet ds = new DataSet();
                            OleDbDataAdapter oda = new OleDbDataAdapter("select * from [owssvr$]", Econ);
                            Econ.Close();
                            oda.Fill(ds);
                            DataTable Exceldt = ds.Tables[0];


                            List<Model.ExcelErrorLog> errorLogList = new List<Model.ExcelErrorLog>();

                            foreach (DataRow dr in Exceldt.Rows)
                            {
                                Model.ExcelErrorLog log = new Model.ExcelErrorLog();
                                log.DocumentName = Convert.ToString(dr[0]);
                                log.Error = Convert.ToString(dr[1]);
                                log.OtherReason = Convert.ToString(dr[2]);
                                log.DateProcessed = Convert.ToString(dr[3]);
                                log.DateFound = Convert.ToString(dr[4]);
                                log.DriverId = Convert.ToString(dr[5]);
                                log.ClientAccount = Convert.ToString(dr[6]);
                                log.ProcessorWhoMadeError = Convert.ToString(dr[7]);
                                log.ProcessorWhoCaughtError = Convert.ToString(dr[8]);
                                log.FirstName = Convert.ToString(dr[9]);
                                log.LastName = Convert.ToString(dr[10]);
                                log.OutCome = Convert.ToString(dr[11]);
                                log.ItemType = Convert.ToString(dr[12]);
                                log.Path = Convert.ToString(dr[13]);
                                errorLogList.Add(log);
                            }
                            Model.DQFEntities dq = new Model.DQFEntities();
                            dq.ExcelErrorLogs.AddRange(errorLogList);
                            dq.SaveChanges();



                        }
                        else
                        {
                            lblmsg.Text = "Select only Excel File ....!!";
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Upload Excel File ......";

                }
            }
            catch(Exception ex)
            {
                logger1.Error(ex.Message);
            }
        }
    
    }
}