using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using MiddleLayer;
using System.Data;


namespace DataAccessLayer
{
    public class cls_LoginAudit
    {
        private string GetConnectionString()
        {

            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }
        public List<LoginAudit> getLoginAuditReport(int UserID)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<LoginAudit> lstLoginAudit = new List<LoginAudit>();

            SqlCommand command = new SqlCommand("SP_GetLoginAuditDetails", SqlConn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@UserId", SqlDbType.VarChar).Value = UserID;
            try
            {
                SqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    LoginAudit loginAudit = new LoginAudit();
                    loginAudit.UserFirstName = Convert.ToString(reader["UserFirstName"]);
                    loginAudit.UserLastName = Convert.ToString(reader["UserLastName"]);
                    loginAudit.EmailId = Convert.ToString(reader["EmailId"]);
                    loginAudit.DateOfLogin = Convert.ToString(reader["DateOfLogin"]);
                    loginAudit.DateOfLogOut = Convert.ToString(reader["DateOfLogOut"]);


                    lstLoginAudit.Add(loginAudit);



                }
                return lstLoginAudit;
            }
            catch (Exception Ex)
            {
                return lstLoginAudit;
                throw new Exception(Ex.Message);
            }
            finally
            {
                SqlConn.Close();
            }

        }
        public List<LoginAudit> getRebotProcessingReport()
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<LoginAudit> lstLoginAudit = new List<LoginAudit>();

            SqlCommand command = new SqlCommand("SP_GetRoboActivityWithScreenData", SqlConn);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    LoginAudit loginAudit = new LoginAudit();
                    loginAudit.StartTime = Convert.ToString(reader["StartTime"]);
                    loginAudit.EndTime = Convert.ToString(reader["EndTime"]);
                    loginAudit.Activity = Convert.ToString(reader["Activity"]);
                    loginAudit.TotalNumberOfPages = Convert.ToString(reader["TotalNumberOfPages"]);
                    loginAudit.Comment = Convert.ToString(reader["Comment"]);
                    loginAudit.Labelling = Convert.ToString(reader["Labelling"]);


                    lstLoginAudit.Add(loginAudit);



                }
                return lstLoginAudit;
            }
            catch (Exception Ex)
            {
                return lstLoginAudit;
                throw new Exception(Ex.Message);
            }
            finally
            {
                SqlConn.Close();
            }

        }

        public List<LoginAudit> getLoginAuditReportDateWise(int UserID, string FromDate, string ToDate)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<LoginAudit> lstLoginAudit = new List<LoginAudit>();
            string SqlString = "SELECT UserDetails.UserFirstName, UserDetails.UserLastName, UserDetails.EmailId, LoginDetails.DateOfLogin, LoginDetails.DateOfLogOut FROM   UserDetails INNER JOIN LoginDetails ON UserDetails.UserDetailsID = LoginDetails.UserId where CONVERT(VARCHAR(10), LoginDetails.DateOfLogin, 110) between CONVERT(VARCHAR(10), @FromDate, 110) and CONVERT(VARCHAR(10), @ToDate, 110) order by LoginDetails.DateOfLogOut desc  ";

            using (SqlCommand SqlCom = new SqlCommand(SqlString, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.Parameters.AddWithValue("@UserID", UserID);
                    SqlCom.Parameters.AddWithValue("@FromDate", FromDate);
                    SqlCom.Parameters.AddWithValue("@ToDate", ToDate);
                    SqlCom.CommandType = CommandType.Text;
                    SqlDataReader reader = SqlCom.ExecuteReader();

                    while (reader.Read())
                    {
                        LoginAudit loginAudit = new LoginAudit();

                        loginAudit.UserFirstName = Convert.ToString(reader["UserFirstName"]);
                        loginAudit.UserLastName = Convert.ToString(reader["UserLastName"]);
                        loginAudit.EmailId = Convert.ToString(reader["EmailId"]);
                        loginAudit.DateOfLogin = Convert.ToString(reader["DateOfLogin"]);
                        loginAudit.DateOfLogOut = Convert.ToString(reader["DateOfLogOut"]);

                        lstLoginAudit.Add(loginAudit);

                    }
                    return lstLoginAudit;
                }
                catch (Exception Ex)
                {
                    return lstLoginAudit;
                    throw new Exception(Ex.Message);
                }
                finally
                {
                    SqlConn.Close();
                }
            }


        }

        public List<LoginAudit> getRoboProcessingReportDateWise(string FromDate, string ToDate)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<LoginAudit> lstLoginAudit = new List<LoginAudit>();
            string SqlString = "select RoboActivity.StartTime,RoboActivity.EndTime,RoboActivity.Activity,CMS_CPScreenData.TotalNumberOfPages,CMS_CPScreenData.Comment,CMS_CPScreenData.Labelling from RoboActivity INNER JOIN CMS_CPScreenData ON RoboActivity.RoboActivitiesID = CMS_CPScreenData.RoboActivityID where CONVERT(VARCHAR(10), RoboActivity.StartTime, 110) between CONVERT(VARCHAR(10), @FromDate, 110) and CONVERT(VARCHAR(10), @ToDate, 110) order by CMS_CPScreenData.DateofCreation desc";

            using (SqlCommand SqlCom = new SqlCommand(SqlString, SqlConn))
            {
                try
                {
                    SqlConn.Open();

                    SqlCom.Parameters.AddWithValue("@FromDate", FromDate);
                    SqlCom.Parameters.AddWithValue("@ToDate", ToDate);
                    SqlCom.CommandType = CommandType.Text;
                    SqlDataReader reader = SqlCom.ExecuteReader();

                    while (reader.Read())
                    {
                        LoginAudit loginAudit = new LoginAudit();
                        loginAudit.StartTime = Convert.ToString(reader["StartTime"]);
                        loginAudit.EndTime = Convert.ToString(reader["EndTime"]);
                        loginAudit.Activity = Convert.ToString(reader["Activity"]);
                        loginAudit.TotalNumberOfPages = Convert.ToString(reader["TotalNumberOfPages"]);
                        loginAudit.Comment = Convert.ToString(reader["Comment"]);
                        loginAudit.Labelling = Convert.ToString(reader["Labelling"]);

                        lstLoginAudit.Add(loginAudit);

                    }
                    return lstLoginAudit;
                }
                catch (Exception Ex)
                {
                    return lstLoginAudit;
                    throw new Exception(Ex.Message);
                }
                finally
                {
                    SqlConn.Close();
                }
            }


        }

        public DataTable getExceptionDataDetails()
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;

            SqlQuery = @"SELECT FaxId,CMS_CPScreenDocumentdTypeId,UserId,ReceiveDate,DateofCreation,TotalNumberOfPages,convert(varchar(5),DateDiff(s, ReceiveDate, DateofCreation)/3600)+':'+convert(varchar(5),DateDiff(s, ReceiveDate, DateofCreation)%3600/60)+':'+convert(varchar(5),(DateDiff(s, ReceiveDate, DateofCreation)%60)) as [hh:mm:ss] from Exception_CPScreenData";
            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(SqlCom);
                    da.Fill(lstUserDetails);

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }
        }

        public List<LoginAudit> getExceptionDataDetails2(int faxid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<LoginAudit> lstUserDetails = new List<LoginAudit>();
            string SqlQuery = null;
            //SqlQuery = @"SELECT t.DynamicControlID,t.DynamicControlValueText,t.UserId,FaxID FROM Exception_FormData t join Exception_CPScreenData ON t.CPScreenDataID = Exception_CPScreenData.CPScreenDataID";
            SqlQuery = @"SELECT distinct DocumentTypeName,UserFirstName,UserLastName,FaxID,UserDetailsID
        FROM Exception_FormData t join Exception_CPScreenData 
        ON t.CPScreenDataID = Exception_CPScreenData.CPScreenDataID inner join DynamicControl DC on  DC.DynamicControlID=t.DynamicControlID 
        inner join UserDetails u on u.UserDetailsID=t.UserId inner join DocumentType DT on DT.DocumentTypeID=DC.DocumentTypeID where FaxID=" + faxid + "";
            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    // SqlCom.Parameters.AddWithValue("@UserDetailsId", Config.userId);
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        LoginAudit setData = new LoginAudit();



                        setData.UserDetailsID = Convert.ToString(reader["UserDetailsID"]);
                        setData.FaxID = Convert.ToString(reader["FaxID"]);
                        setData.DocumentTypeName = Convert.ToString(reader["DocumentTypeName"]);
                        setData.UserFirstName = Convert.ToString(reader["UserFirstName"]);
                        setData.UserLastName = Convert.ToString(reader["UserLastName"]);

                        lstUserDetails.Add(setData);

                    }

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }
        }

        public DataTable getuserid(string faxid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"SELECT UserId from  Exception_CPScreenData 
            where FAXID=" + faxid + "";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(SqlCom);
                    da.Fill(lstUserDetails);
                    SqlDataReader reader = SqlCom.ExecuteReader();

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }

        }
        public DataTable getUserName(string userid)
        {

            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"select distinct UserFirstName,UserLastName from UserDetails where UserDetailsID IN(" + userid + ")";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(SqlCom);
                    da.Fill(lstUserDetails);
                    SqlDataReader reader = SqlCom.ExecuteReader();

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }
        }

        public DataTable GetDynamicControlID(string faxid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"select max(DynamicControlID) as DynamicControlID from Exception_FormData  EFD inner join Exception_CPScreenData ECPSD on EFD.CPScreenDataID=ECPSD.CPScreenDataID where FaxId=" + faxid + " group by CountForFaxId";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(SqlCom);
                    da.Fill(lstUserDetails);
                    SqlDataReader reader = SqlCom.ExecuteReader();

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }
        }
        public DataTable GetDocumentID(string documentid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"select DocumentTypeId from DynamicControl where DynamicControlID IN(" + documentid + ")";


            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(SqlCom);
                    da.Fill(lstUserDetails);
                    SqlDataReader reader = SqlCom.ExecuteReader();

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }
        }
        public DataTable GetDocumentType(string documentid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"select DocumentTypeID,DocumentTypeName from DocumentType where DocumentTypeId IN(" + documentid + ")";


            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(SqlCom);
                    da.Fill(lstUserDetails);
                    SqlDataReader reader = SqlCom.ExecuteReader();

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }
        }
        public DataTable GetExceptionData(string Faxid, string Docid, string Userid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"select DC.DynamicControlID,EFD.CPScreenDataID,EFD.CountForFaxId,labelName,DynamicControlValueText from DynamicControl DC
               inner join Exception_FormData EFD on DC.DynamicControlID=EFD.DynamicControlID inner join
               Exception_CPScreenData ECPSD on ECPSD.CPScreenDataID=EFD.CPScreenDataID
               inner join UserDetails U on U.UserDetailsID= EFD.UserId where FaxId=" + Faxid + " and DocumentTypeID=" + Docid + " and UserDetailsID IN(" + Userid + ")";


            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(SqlCom);
                    da.Fill(lstUserDetails);
                    SqlDataReader reader = SqlCom.ExecuteReader();

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }
        }



        public DataTable getExcepationCPDcreenData(string faxid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"select [FaxID]
      ,[ReceiveDate]
      ,[CustomerID]
      ,[SourceFile]
      ,[TotalNumberOfPages]
      ,[DateofCreation]
      ,[DateOfModification]
      ,[Comment]
      ,[RoboActivityID]
      ,[CMS_CPScreenDocumentdTypeId]
      ,[Labelling]
      ,[UserId]
      ,[IsLabellingDone]
      ,[ProcessingStarted]
      ,[ProcessingEnd]
      ,[PassbyQA]
      FROM Exception_CPScreenData where FaxId=" + faxid + "";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(SqlCom);
                    da.Fill(lstUserDetails);
                    SqlDataReader reader = SqlCom.ExecuteReader();

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }
        }
        public void insertmaincpscreendata(DataTable dt, string documentid, string CustId)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();

            string SqlQuery = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlQuery = SqlQuery + "insert into CMS_CPScreenData ([FaxID],[ReceiveDate],[CustomerID],[SourceFile],[TotalNumberOfPages],[DateofCreation],[DateOfModification],[Comment] ,[RoboActivityID],[CMS_CPScreenDocumentdTypeId],[Labelling],[UserId] ,[IsLabellingDone],[ProcessingStarted],[ProcessingEnd],[IsFomException]) values('"
                      + dt.Rows[i]["FaxID"].ToString().Trim() + "','"
                      + dt.Rows[i]["ReceiveDate"].ToString().Trim() + "','"
                      + CustId + "','"
                      + dt.Rows[i]["SourceFile"].ToString().Trim() + "','"
                      + dt.Rows[i]["TotalNumberOfPages"].ToString().Trim() + "','"
                      + dt.Rows[i]["DateofCreation"].ToString().Trim() + "','"
                      + dt.Rows[i]["DateOfModification"].ToString().Trim() + "','"
                      + dt.Rows[i]["Comment"].ToString().Trim() + "','"
                      + dt.Rows[i]["RoboActivityID"].ToString().Trim() + "','"
                      + documentid + "','"
                      + dt.Rows[i]["Labelling"].ToString().Trim() + "','"
                      + dt.Rows[i]["UserId"].ToString().Trim() + "','"

                       + dt.Rows[i]["IsLabellingDone"].ToString().Trim() + "','"
                      + DateTime.Now.ToString() + "','"
                      + DateTime.Now.ToString() + "','"

                      + 1 + "')";

            }



            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.ExecuteNonQuery();

                    SqlConn.Close();


                }
                catch (Exception e)
                {

                    e.ToString();
                }

            }
        }
        public DataTable getCPScreenDataID(string faxid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"SELECT CPScreenDataID from CMS_CPScreenData where FaxID =" + faxid + "";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(SqlCom);
                    da.Fill(lstUserDetails);

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }
        }
        public int insertFormdata(string CPScreenDataID, string DynamicControlID, string DynamicControlValueText, string CountForFaxId)
        {

            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            string SqlString = "insert into FormData(CPScreenDataID,DynamicControlID,DynamicControlValueText,CountForFaxId) values(" + CPScreenDataID + "," + DynamicControlID + ",'" + DynamicControlValueText + "','" + CountForFaxId + "')";

            using (SqlCommand SqlCom = new SqlCommand(SqlString, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.ExecuteNonQuery();
                    SqlConn.Close();
                }
                catch (Exception Ex)
                {
                    Ex.ToString();

                    return 0;

                }
                finally
                {
                    SqlConn.Close();
                }
            }
            return 1;


        }
        public void deletefaxidmaintabledata(string faxid)
        {

            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            string SqlString = "delete from [Exception_CPScreenData] where FaxId=" + faxid + "";

            using (SqlCommand SqlCom = new SqlCommand(SqlString, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.ExecuteNonQuery();
                    SqlConn.Close();
                }
                catch (Exception Ex)
                {
                    Ex.ToString();

                }
                finally
                {
                    SqlConn.Close();
                }
            }

        }

        public void deletedatafrom(string CPScreenDataID)
        {

            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            string SqlString = "delete from [Exception_FormData] where CPScreenDataID=" + CPScreenDataID + "";

            using (SqlCommand SqlCom = new SqlCommand(SqlString, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.ExecuteNonQuery();
                    SqlConn.Close();
                }
                catch (Exception Ex)
                {
                    Ex.ToString();

                }
                finally
                {
                    SqlConn.Close();
                }
            }

        }
        public DataTable GetCustID(string docid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"select customerid from documenttype where documenttypeid IN(" + docid + ")";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(SqlCom);
                    da.Fill(lstUserDetails);

                    SqlConn.Close();
                    return lstUserDetails;

                }
                catch (Exception e)
                {
                    return lstUserDetails;

                }

            }

        }
    }
}
