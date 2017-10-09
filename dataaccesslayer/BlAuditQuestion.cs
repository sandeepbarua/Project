using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace DataAccessLayer
{
    public class BlAuditQuestion
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }



        public static void ddlCustomerName(DropDownList ddlCustomerName)
        {
            ddlCustomerName.Items.Clear();
            DataTable dtddlCustomerName;
            ListItem li = new ListItem();
            li.Text = "Select Customer";
            li.Value = "0";
            ddlCustomerName.Items.Add(li);
            dtddlCustomerName = ExecuteSP(@"
  SELECT distinct [CompanyID]
      ,[FADV_CustomerID]
      ,[CompanyName]
  FROM [dbo].[CustomerDetails]
  join documenttype
  on CustomerDetails.CompanyID = documenttype.CustomerID where CustomerDetails.IsActive=1");
            for (int i = 0; i < dtddlCustomerName.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtddlCustomerName.Rows[i]["CompanyID"].ToString();
                li.Text = dtddlCustomerName.Rows[i]["CompanyName"].ToString();
                ddlCustomerName.Items.Add(li);
            }
        }
        public static bool ddlCustomerAddName(DropDownList ddlCustomerAddName, int CustomerID)
        {
            ddlCustomerAddName.Items.Clear();
            DataTable dtddlCustomerAddName;
            ListItem li = new ListItem();
            ddlCustomerAddName.Items.Add(li);
            dtddlCustomerAddName = ExecuteSP(@"SELECT [CompanyID]
                                              ,[FADV_CustomerID]
                                              ,[CompanyName]
                                          FROM [dbo].[CustomerDetails] where CompanyID='" + CustomerID + "' and IsActive=1 ");
            for (int i = 0; i < dtddlCustomerAddName.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtddlCustomerAddName.Rows[i]["CompanyID"].ToString();
                li.Text = dtddlCustomerAddName.Rows[i]["CompanyName"].ToString();
                ddlCustomerAddName.Items.Add(li);
                return true;
            }
            return true;
        }


        public static void ddlCustomerID(DropDownList ddlCustID)
        {
            ddlCustID.Items.Clear();
            DataTable dtddlDocType;
            ListItem li = new ListItem();
            li.Text = "Select Customer";
            li.Value = "0";
            ddlCustID.Items.Add(li);
            dtddlDocType = ExecuteSP(@"SELECT [CompanyID]
      ,[FADV_CustomerID]
      ,[CompanyName]
    
  FROM [dbo].[CustomerDetails] where IsActive=1");
            for (int i = 0; i < dtddlDocType.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtddlDocType.Rows[i]["CompanyID"].ToString();
                li.Text = dtddlDocType.Rows[i]["CompanyName"].ToString();
                ddlCustID.Items.Add(li);
            }
        }
        public static void ddlGetDocType(DropDownList ddlDocType)
        {
            ddlDocType.Items.Clear();
            DataTable dtddlDocType;
            ListItem li = new ListItem();
            li.Text = "Select Document Type";
            li.Value = "0";
            ddlDocType.Items.Add(li);
            dtddlDocType = ExecuteSP(@"SELECT [DocumentTypeID],[DocumentTypeName],[DocumentDescription][DateOfCreation],[DateOfModification] FROM [dbo].[DocumentType] where IsActive=1");
            for (int i = 0; i < dtddlDocType.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtddlDocType.Rows[i]["DocumentTypeID"].ToString();
                li.Text = dtddlDocType.Rows[i]["DocumentTypeName"].ToString();
                ddlDocType.Items.Add(li);
            }
        }



        public List<MlAuditQuestion> getAuditQuestion()
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlAuditQuestion> lstMlAuditQuestion = new List<MlAuditQuestion>();
            string SqlQuery = null;
            SqlQuery = @"SELECT   AuditQuestion.AuditQuestionsID, AuditQuestion.AuditQuestion, 
  DocumentType.DocumentTypeName, 
  DocumentType.DocumentDescription, 
  UserDetails.UserFirstName, 
  UserDetails.UserLastName, 
  CustomerDetails.CompanyName,
   AuditQuestion.CustomerID, 
   AuditQuestion.DocumentID,
AuditQuestion.UserID
FROM AuditQuestion INNER JOIN
                         UserDetails ON AuditQuestion.UserID = UserDetails.UserDetailsID INNER JOIN
                         CustomerDetails ON AuditQuestion.CustomerID = CustomerDetails.CompanyID  JOIN
                         Documenttype On AuditQuestion.DocumentID = Documenttype.DocumentTypeID where AuditQuestion.IsActive=1";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlAuditQuestion setData = new MlAuditQuestion();

                        setData.AuditQuestionsID = Convert.ToInt32(reader["AuditQuestionsID"]);
                        setData.AuditQuestion = Convert.ToString(reader["AuditQuestion"]);
                        setData.DocumentID = Convert.ToInt32(reader["DocumentID"]);
                        setData.DocumentTypeName = Convert.ToString(reader["DocumentTypeName"]);
                        setData.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                        setData.DocumentDescription = Convert.ToString(reader["DocumentDescription"]);
                        setData.UserID = Convert.ToInt32(reader["UserID"]);
                        setData.UserFirstName = Convert.ToString(reader["UserFirstName"]);
                        setData.UserLastName = Convert.ToString(reader["UserLastName"]);
                        setData.UserName = setData.UserFirstName + " " + setData.UserLastName;
                        setData.CompanyName = Convert.ToString(reader["CompanyName"]);
                        lstMlAuditQuestion.Add(setData);

                    }

                    SqlConn.Close();
                    return lstMlAuditQuestion;

                }
                catch (Exception e)
                {
                    return lstMlAuditQuestion;

                }

            }
        }


        public List<MlAuditQuestion> getAuditQuestion(MlAuditQuestion Adq)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlAuditQuestion> lstMlAuditQuestion = new List<MlAuditQuestion>();
            string SqlQuery = null;
            if (Adq.DocumentID.ToString() == "0")
            {
                SqlQuery = @"SELECT   AuditQuestion.AuditQuestionsID, AuditQuestion.AuditQuestion, 
  DocumentType.DocumentTypeName, 
  DocumentType.DocumentDescription, 
  UserDetails.UserFirstName, 
  UserDetails.UserLastName, 
  CustomerDetails.CompanyName,
   AuditQuestion.CustomerID, 
   AuditQuestion.DocumentID,
AuditQuestion.UserID
FROM AuditQuestion INNER JOIN
                         UserDetails ON AuditQuestion.UserID = UserDetails.UserDetailsID INNER JOIN
                         CustomerDetails ON AuditQuestion.CustomerID = CustomerDetails.CompanyID  JOIN
                         Documenttype On AuditQuestion.DocumentID = Documenttype.DocumentTypeID WHERE AuditQuestion.CustomerID = " + Adq.CustomerID + " and AuditQuestion.IsActive=1";

            }
            else
            {
                SqlQuery = @"SELECT   AuditQuestion.AuditQuestionsID, AuditQuestion.AuditQuestion, 
  DocumentType.DocumentTypeName, 
  DocumentType.DocumentDescription, 
  UserDetails.UserFirstName, 
  UserDetails.UserLastName, 
  CustomerDetails.CompanyName,
   AuditQuestion.CustomerID, 
   AuditQuestion.DocumentID,
AuditQuestion.UserID
FROM AuditQuestion INNER JOIN
                         UserDetails ON AuditQuestion.UserID = UserDetails.UserDetailsID INNER JOIN
                         CustomerDetails ON AuditQuestion.CustomerID = CustomerDetails.CompanyID  JOIN
                         Documenttype On AuditQuestion.DocumentID = Documenttype.DocumentTypeID WHERE AuditQuestion.CustomerID = " + Adq.CustomerID + " and AuditQuestion.DocumentID = " + Adq.DocumentID + " and AuditQuestion.IsActive=1";

            }


            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlAuditQuestion setData = new MlAuditQuestion();

                        setData.AuditQuestionsID = Convert.ToInt32(reader["AuditQuestionsID"]);
                        setData.AuditQuestion = Convert.ToString(reader["AuditQuestion"]);
                        setData.DocumentID = Convert.ToInt32(reader["DocumentID"]);
                        setData.DocumentTypeName = Convert.ToString(reader["DocumentTypeName"]);
                        setData.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                        setData.DocumentDescription = Convert.ToString(reader["DocumentDescription"]);
                        setData.UserID = Convert.ToInt32(reader["UserID"]);
                        setData.UserFirstName = Convert.ToString(reader["UserFirstName"]);
                        setData.UserLastName = Convert.ToString(reader["UserLastName"]);
                        setData.UserName = setData.UserFirstName + " " + setData.UserLastName;
                        setData.CompanyName = Convert.ToString(reader["CompanyName"]);
                        lstMlAuditQuestion.Add(setData);

                    }

                    SqlConn.Close();
                    return lstMlAuditQuestion;

                }
                catch (Exception e)
                {
                    return lstMlAuditQuestion;

                }

            }
        }


        public bool setAuditQuestion(MlAuditQuestion Adq)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();

            string SqlQuery = null, MQuery = null;
            SqlQuery = @"INSERT INTO [dbo].[AuditQuestion]
           ([AuditQuestion]
           ,[CustomerID]
           ,[DocumentID]
           ,[UserID])
             VALUES    (@AuditQuestion,@CustomerID,@DocumentID,@UserID)";

            using (SqlCommand SqlComm = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    MIDocumentType Doctype = new MIDocumentType();
                    SqlComm.Parameters.AddWithValue("@AuditQuestion", Adq.AuditQuestion);
                    SqlComm.Parameters.AddWithValue("@CustomerID", Adq.CustomerID);
                    SqlComm.Parameters.AddWithValue("@DocumentID", Adq.DocumentID);
                    SqlComm.Parameters.AddWithValue("@UserID", Adq.UserID);

                    SqlComm.CommandType = CommandType.Text;
                    try
                    {
                        SqlComm.ExecuteNonQuery();
                        SqlConn.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public bool updateAuditQuestion(MlAuditQuestion Adq)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();

            string SqlQuery = null;

            SqlQuery = @"UPDATE [dbo].[AuditQuestion]
                           SET [AuditQuestion] = @AuditQuestion
                              ,[CustomerID] = @CustomerID
                              ,[DocumentID] = @DocumentID
                              ,[UserID] = @UserID
                        where [AuditQuestionsID]=@AuditQuestionID";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.Parameters.AddWithValue("@AuditQuestionID", Adq.AuditQuestionsID);
                    SqlCom.Parameters.AddWithValue("@AuditQuestion", Adq.AuditQuestion);
                    SqlCom.Parameters.AddWithValue("@CustomerID", Adq.CustomerID);
                    SqlCom.Parameters.AddWithValue("@DocumentID", Adq.DocumentID);
                    SqlCom.Parameters.AddWithValue("@UserID", Adq.UserID);

                    SqlCom.CommandType = CommandType.Text;
                    try
                    {
                        SqlCom.ExecuteNonQuery();
                        SqlConn.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public bool DeleteAuditQuestion(MlAuditQuestion Adq)
        {
            BLConsumeApi objApi = new BLConsumeApi();
            try
            {
                objApi.Sp_DeleteAuditQuestion(Adq.AuditQuestionsID);
                return true;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
         }       


        public static DataTable ExecuteSP(string sqlQuery)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            using (SqlCommand SqlCom = new SqlCommand(sqlQuery, SqlConn))
            {
                SqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(SqlCom.ExecuteReader());
                //SqlConn.Close();
                return dt;

            }

        }
        public static DataTable ddlFunctionList(DropDownList ddlFunction, string Type)
        {
            DataTable DtAuditFuction = new DataTable();
            DtAuditFuction.Columns.Add("Id");
            DtAuditFuction.Columns.Add("FuntionName");
            DtAuditFuction.Columns.Add("FunctionType");
            DtAuditFuction.Columns.Add("NoOfParameters");
            DtAuditFuction.Columns.Add("Expression");
            DtAuditFuction.Columns.Add("LabelName");
            try
            {
                ListItem li = new ListItem();
                li.Text = "Select Function";
                li.Value = "0";
                ddlFunction.Items.Add(li);
                List<MIAuditQuestionFunction> AuditQuestionFunction = new List<MIAuditQuestionFunction>();
                List<MIAuditQuestionFunction> reader = new BLConsumeApi().sp_SelectAuditFunction(Type);
                AuditQuestionFunction = reader.Distinct().ToList();
                foreach (var r in AuditQuestionFunction)
                {
                    DataRow dr = DtAuditFuction.NewRow();
                    li = new ListItem();
                    li.Value = Convert.ToString(r.id);
                    li.Text = r.FuntionName;
                    ddlFunction.Items.Add(li);
                    dr["Id"] = Convert.ToString(r.id);
                    dr["FuntionName"] = Convert.ToString(r.FuntionName);
                    dr["FunctionType"] = Convert.ToString(r.FunctionType);
                    dr["NoOfParameters"] = Convert.ToString(r.NoOfParameters);
                    dr["Expression"] = Convert.ToString(r.Expression);
                    dr["LabelName"] = Convert.ToString(r.LabelName);
                    DtAuditFuction.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return DtAuditFuction;
        }


    }
}