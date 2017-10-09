using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MiddleLayer;
namespace DataAccessLayer
{
    public class BlFieldGenerate
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }
        public List<MlDynamicControl> DynamicControls(int DocTypeId)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlDynamicControl> lstFieldDetails = new List<MlDynamicControl>();

            string SqlQuery = null;


            SqlQuery = @"SELECT [DynamicControlID],[DocumentTypeID],[labelName],[ControlName],[ControlType] FROM [dbo].[DynamicControl] where DocumentTypeID='" + DocTypeId + "'";
            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {

                        MlDynamicControl setData = new MlDynamicControl();
                        setData.DynamicControlID = Convert.ToInt32(reader["DynamicControlID"]);
                        setData.DocumentTypeID = Convert.ToInt32(reader["DocumentTypeID"]);
                        setData.labelName = Convert.ToString(reader["labelName"]);
                        setData.ControlName = Convert.ToString(reader["ControlName"]);
                        setData.ControlType = Convert.ToString(reader["ControlType"]);
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
    }
}
