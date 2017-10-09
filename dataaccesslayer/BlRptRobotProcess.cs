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
    public class BlRptRobotProcess
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }



        public List<MlRptRobotProcess> getResult()
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlRptRobotProcess> lstMlRptRobotProcess = new List<MlRptRobotProcess>();
            string SqlQuery = null;
            SqlQuery = @"select RoboActivitiesID, StartTime, EndTime, count(CPScreenDataID) as 'Total Records Process' 
from dbo.RoboActivity RA join
CMS_CPScreenData CPD on
RA.RoboActivitiesID = CPD.RoboActivityID
group by RoboActivitiesID, StartTime, EndTime";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlRptRobotProcess setData = new MlRptRobotProcess();

                        setData.RobotActivityID = Convert.ToInt32(reader["RoboActivitiesID"]);
                        setData.StartTime = Convert.ToString(reader["StartTime"]);
                        setData.EndTime = Convert.ToString(reader["EndTime"]);
                        setData.TotalProcess = Convert.ToString(reader["Total Records Process"]);
                        lstMlRptRobotProcess.Add(setData);

                    }

                    SqlConn.Close();
                    return lstMlRptRobotProcess;

                }
                catch (Exception e)
                {
                    return lstMlRptRobotProcess;

                }

            }
        }

        public List<MlRptRobotProcess> getResult(string stdate, string enddate)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlRptRobotProcess> lstMlRptRobotProcess = new List<MlRptRobotProcess>();
            string SqlQuery = null;
            SqlQuery = @"select RoboActivitiesID, StartTime, EndTime, count(CPScreenDataID) as 'Total Records Process' 
from dbo.RoboActivity RA join
CMS_CPScreenData CPD on
RA.RoboActivitiesID = CPD.RoboActivityID
where([StartTime] >= @startdate OR @startdate IS NULL)  AND 
						  ([EndTime] <= DATEADD(s,-1,DATEADD(d,1,@enddate)) OR @enddate IS NULL) 
group by RoboActivitiesID, StartTime, EndTime";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.Parameters.AddWithValue("@startdate", stdate);

                    SqlCom.Parameters.AddWithValue("@enddate", enddate);

                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlRptRobotProcess setData = new MlRptRobotProcess();

                        setData.RobotActivityID = Convert.ToInt32(reader["RoboActivitiesID"]);
                        setData.StartTime = Convert.ToString(reader["StartTime"]);
                        setData.EndTime = Convert.ToString(reader["EndTime"]);
                        setData.TotalProcess = Convert.ToString(reader["Total Records Process"]);
                        lstMlRptRobotProcess.Add(setData);

                    }

                    SqlConn.Close();
                    return lstMlRptRobotProcess;

                }
                catch (Exception e)
                {
                    return lstMlRptRobotProcess;

                }

            }
        }

        public List<MlRptRobotProcess> getDetailResult(int RecId)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlRptRobotProcess> lstMlRptRobotDetailProcess = new List<MlRptRobotProcess>();
            string SqlQuery = null;
            SqlQuery = @"
select CPScreenDataID, CPD.FaxID, ReceiveDate, CompanyName,TotalNumberofPages,Comment,[LabellingText], dbo.ReturnUser(userid) as 'User'
from dbo.CMS_CPScreenData CPD join
CustomerDetails CD on CPD.CustomerID = CD.CompanyID
left join LebellingLog LG on CPD.FaxID = LG.FaxID 
where RoboActivityID = " + RecId + "";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlRptRobotProcess setData = new MlRptRobotProcess();

                        setData.CPScreenDataID = Convert.ToInt32(reader["CPScreenDataID"]);
                        setData.FaxID = Convert.ToInt32(reader["FaxID"]);
                        setData.ReceiveDate = Convert.ToString(reader["ReceiveDate"]);
                        setData.CompanyName = Convert.ToString(reader["CompanyName"]);
                        setData.TotalNumberofPages = Convert.ToString(reader["TotalNumberofPages"]);
                        setData.Comment = Convert.ToString(reader["Comment"]);
                        setData.LabellingText = Convert.ToString(reader["LabellingText"]);
                        setData.User = Convert.ToString(reader["User"]);




                        lstMlRptRobotDetailProcess.Add(setData);

                    }

                    SqlConn.Close();
                    return lstMlRptRobotDetailProcess;

                }
                catch (Exception e)
                {
                    return lstMlRptRobotDetailProcess;

                }

            }
        }

    }
}
