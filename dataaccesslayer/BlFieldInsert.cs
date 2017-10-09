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
    public    class BlFieldInsert
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }
        public bool addFieldDetails(MlDynamicControlValue setData)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
          
            string SqlQuery = null;

            SqlQuery = @"INSERT INTO [dbo].[DynamicControlValue]
           ([TempCPScreenDataID]
           ,[DynamicControlID]
           ,[DynamicControlValue])
     VALUES
           (@TempCPScreenDataID
           ,@DynamicControlID
           ,@DynamicControlValue)";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();

                    SqlCom.Parameters.AddWithValue("@TempCPScreenDataID", setData.TempCPScreenDataID);
                    SqlCom.Parameters.AddWithValue("@DynamicControlID", setData.DynamicControlID);
                    SqlCom.Parameters.AddWithValue("@DynamicControlValue", setData.DynamicControlValue);
                    

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

        public bool addFieldSubmitDetails(MlDynamicControlValue setData)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();

            string SqlQuery = null;
           

            SqlQuery = @"INSERT INTO [dbo].[DynamicControlValue]
           ([TempCPScreenDataID]
           ,[DynamicControlID]
           ,[DynamicControlValue])
     VALUES
           (@TempCPScreenDataID
           ,@DynamicControlID
           ,@DynamicControlValue)";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();

                    SqlCom.Parameters.AddWithValue("@TempCPScreenDataID", setData.TempCPScreenDataID);
                    SqlCom.Parameters.AddWithValue("@DynamicControlID", setData.DynamicControlID);
                    SqlCom.Parameters.AddWithValue("@DynamicControlValue", setData.DynamicControlValue);


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
