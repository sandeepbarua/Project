using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class BlCPScreenData
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }
        public List<MlCPScreenData> getUserDetails()
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlCPScreenData> lstUserDetails = new List<MlCPScreenData>();
            string SqlQuery = null;
            SqlQuery = @"SELECT        CMS_CPScreenData.CPScreenDataID, CMS_CPScreenData.FaxID, CMS_CPScreenData.CustomerID, 
                         CustomerDetails.CompanyName, CMS_CPScreenData.ReceiveDate, CMS_CPScreenData.TotalNumberOfPages, 
                         CMS_CPScreenData.DateofCreation,  CMS_CPScreenData.Labelling,  
                         CMS_CPScreenData.ProcessingStarted, CMS_CPScreenData.ProcessingEnd
FROM            CMS_CPScreenData INNER JOIN
                        
                         CustomerDetails ON CMS_CPScreenData.CustomerID = CustomerDetails.CompanyID order by ProcessingEnd";
            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.Parameters.AddWithValue("@UserDetailsId", Config.userId);
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlCPScreenData setData = new MlCPScreenData();

                        setData.CPScreenDataID = Convert.ToInt32(reader["CPScreenDataID"] == DBNull.Value ? "-" : reader["CPScreenDataID"]);

                        setData.FaxID = Convert.ToInt32(reader["FaxID"] == DBNull.Value ? "-" : reader["FaxID"]);
                        setData.CustomerName = Convert.ToString(reader["CompanyName"] == DBNull.Value ? "-" : reader["CompanyName"]);
                        setData.TotalNumberOfPages = Convert.ToInt32(reader["TotalNumberOfPages"] == DBNull.Value ? "-" : reader["TotalNumberOfPages"]);


                        setData.DateofCreation = Convert.ToString(reader["DateofCreation"] == DBNull.Value ? '-' : reader["DateofCreation"]);
                        setData.Labelling = Convert.ToString(reader["Labelling"] == DBNull.Value ? "-" : reader["Labelling"]);

                        setData.ProcessingStarted = Convert.ToDateTime(reader["ProcessingStarted"] == DBNull.Value ? null : reader["ProcessingStarted"]);


                        //setData.ProcessingEnd = Convert.ToDateTime(reader["ProcessingEnd"] == DBNull.Value ? null : reader["ProcessingEnd"]);

                        setData.ProcessingEnd = Convert.ToDateTime(reader["ProcessingEnd"] == DBNull.Value ? null : reader["ProcessingEnd"]);

                        setData.stringProcessingStarted = Convert.ToString(reader["ProcessingStarted"] == DBNull.Value ? "-" : reader["ProcessingStarted"]);
                        setData.stringProcessingEnd = Convert.ToString(reader["ProcessingEnd"] == DBNull.Value ? "-" : reader["ProcessingEnd"]);
                        if (setData.stringProcessingEnd == "-" && setData.stringProcessingStarted == "-")
                        {
                            setData.Processed = "To be Process";
                            setData.TotalProcessingStarted = "-";
                        }
                        else if (setData.stringProcessingEnd == "-" && setData.stringProcessingStarted != "-")
                        {
                            setData.Processed = "Processing";
                            setData.TotalProcessingStarted = "-";
                        }
                        else
                        {

                            setData.Processed = "Processed";
                            DateTime startTime = setData.ProcessingStarted;

                            DateTime endTime = setData.ProcessingEnd;

                            TimeSpan span = endTime.Subtract(startTime);
                            setData.TotalProcessingStarted = (span.Hours.ToString() + ":" + span.Minutes.ToString() + ":" + span.Seconds.ToString());
                        }





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
    }
}
