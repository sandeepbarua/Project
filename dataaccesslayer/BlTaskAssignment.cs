using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;
using System.Data;

namespace DataAccessLayer
{
   public class BlTaskAssignment
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }
        public List<MlTempTaskAssignment> getfaxDetails()
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlTempTaskAssignment> lstFieldDetails = new List<MlTempTaskAssignment>();
             string SqlQuery = null;
            SqlQuery = @"SELECT TOP (1) [TempTaskAssignmentID]
                      ,[FaxID]
                      ,[UserId]
                      ,[DateOfCreation]
                      ,[DateOfModification]
                      ,[TempCPScreenDataID]
                      ,[isProcessed]
                  FROM [dbo].[TempTaskAssignment] where [UserId]=@UserId and [isProcessed]=0";
            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.Parameters.AddWithValue("@UserId", Config.userId);
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlTempTaskAssignment setData = new MlTempTaskAssignment();
                        setData.TempTaskAssignmentID = Convert.ToInt32(reader["TempTaskAssignmentID"]);
                        Config.TempTaskAssignmentID = setData.TempTaskAssignmentID;


                        setData.FaxID = Convert.ToInt32(reader["FaxID"]);
                        Config.FaxId = setData.FaxID;

                        setData.UserID = Convert.ToInt32(reader["UserId"]);


                        setData.DateOfCreation = Convert.ToDateTime(reader["DateOfCreation"]);
                        setData.DateOfModification = Convert.ToDateTime(reader["DateOfModification"]);
                        setData.TempCPScreenDataID = Convert.ToInt32(reader["TempCPScreenDataID"]);
                        Config.TempCPScreenDataID = setData.TempCPScreenDataID;
                        lstFieldDetails.Add(setData);

                    }
                    SqlConn.Close();
                    return lstFieldDetails;
                }
                catch (Exception e)
                {
                    return lstFieldDetails;

                }

            }
        }
        public static bool updateIsProcessed()
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();


            string SqlQuery = null;

            SqlQuery = @"UPDATE [dbo].[TempTaskAssignment]
                           SET
                           isProcessed = 1
                         WHERE [TempTaskAssignmentID]=@TempTaskAssignmentID";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();

                    SqlCom.Parameters.AddWithValue("@TempTaskAssignmentID", Config.TempTaskAssignmentID);
                    
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
    }
}
