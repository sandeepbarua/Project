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
    public class BlCheckTempFieldInfoId
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }

        public bool getfaxDetails(int faxid, int docid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            string SqlQuery = null;
            SqlQuery = @"SELECT [TempFieldInfoID]
      ,[TempFAXIDDumpId]
      ,[FaxID]
      ,[UserID]
      ,[DocumentTypeID]
      ,[FieldNameId]
      ,[FieldValue]
      ,[DateOfCreation]
      ,[DateOfModification]
      ,[IsMismatchFound]
      ,[IsReviewed]
  FROM [dbo].[TempFieldInfo] where FaxID=@Faid and DocumentTypeID=@DocID";
            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();

                    SqlCom.Parameters.AddWithValue("@Faid", faxid);
                    SqlCom.Parameters.AddWithValue("@DocID", docid);

                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {

                        return true;

                    }
                    SqlConn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;

                }

            }
        }
        public List<MlDynamicControlValue> StoringInSession(MlDynamicControlValue checkError)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlDynamicControlValue> lstdataDetails = new List<MlDynamicControlValue>();


            string SqlQuery = null;
            SqlQuery = @"SELECT [DyanamicControlValueID]
                              ,[TempCPScreenDataID]
                              ,[DynamicControlID]
                              ,[DynamicControlValue]
                          FROM [dbo].[DynamicControlValue] where [TempCPScreenDataID]=@TempCPScreenDataID";

         using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.Parameters.AddWithValue("@TempCPScreenDataID", checkError.TempCPScreenDataID);

                    SqlCom.CommandType = CommandType.Text;

                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlDynamicControlValue previousData = new MlDynamicControlValue();
                        previousData.DyanamicControlValueID = Convert.ToInt32(reader["DyanamicControlValueID"]);
                        previousData.TempCPScreenDataID = Convert.ToInt32(reader["TempCPScreenDataID"]);
                        previousData.DynamicControlID = Convert.ToInt32(reader["DynamicControlID"]);
                        previousData.DynamicControlValue = Convert.ToString(reader["DynamicControlValue"]);
                        lstdataDetails.Add(previousData);
                    }
                    SqlConn.Close();
                    return lstdataDetails;

                }

                catch (Exception e)
                {
                    return lstdataDetails;
                }

            }

        }

    }

   

}

