using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DataAccessLayer
{
   public class BlUserLoginDetail
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }

        public List<MlUserLoginDetail> getUserDetails(int id)
        {

            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlUserLoginDetail> lstUserDetails = new List<MlUserLoginDetail>();
            string SqlQuery = null;
            if (id == 0)
            {
                SqlQuery = @"SELECT        LoginDetails.LoginDetailsId, LoginDetails.UserId, UserDetails.UserFirstName, UserDetails.UserLastName, UserDetails.EmailId, convert(varchar(11), LoginDetails.DateOfLogin) as [Date], convert(varchar(8), convert(time,LoginDetails.DateOfLogin )) as [TimeOfLogin] ,  convert(varchar(8), convert(time,LoginDetails.DateOfLogOut )) as [TimeOfLogOut] , LoginDetails.IPAddress, LoginDetails.Event, LoginDetails.Attempts
FROM            UserDetails INNER JOIN
                         LoginDetails ON UserDetails.UserDetailsID = LoginDetails.UserId order by LoginDetails.LoginDetailsId desc";
            }
            else
            {
                SqlQuery = @"SELECT        LoginDetails.LoginDetailsId, LoginDetails.UserId, UserDetails.UserFirstName, UserDetails.UserLastName, UserDetails.EmailId, convert(varchar(11), LoginDetails.DateOfLogin) as [Date], convert(varchar(8), convert(time,LoginDetails.DateOfLogin )) as [TimeOfLogin] ,  convert(varchar(8), convert(time,LoginDetails.DateOfLogOut )) as [TimeOfLogOut] , LoginDetails.IPAddress, LoginDetails.Event, LoginDetails.Attempts
FROM            UserDetails INNER JOIN
                         LoginDetails ON UserDetails.UserDetailsID = LoginDetails.UserId where UserId='" + id + "'  order by LoginDetails.LoginDetailsId desc";


            }
            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.Parameters.AddWithValue("@UserDetailsId", Config.userId);
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlUserLoginDetail setData = new MlUserLoginDetail();


                        setData.LoginDetailsId = Convert.ToInt32(reader["LoginDetailsId"] == DBNull.Value ? "N/a" : reader["LoginDetailsId"]);
                        setData.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? "N/a" : reader["UserId"]);
                        string fistName = Convert.ToString(reader["UserFirstName"] == DBNull.Value ? "N/a" : reader["UserFirstName"]);
                        string LastName = Convert.ToString(reader["UserLastName"] == DBNull.Value ? "N/a" : reader["UserLastName"]);
                        setData.name = fistName + " " + LastName;

                        setData.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? "N/a" : reader["EmailId"]);
                        setData.NewDate = Convert.ToString(reader["Date"] == DBNull.Value ? "N/a" : reader["Date"]);
                        setData.DateOfLogin = Convert.ToString(reader["TimeOfLogin"] == DBNull.Value ? "N/a" : reader["TimeOfLogin"]);
                        setData.DateOfLogOut = Convert.ToString(reader["TimeOfLogOut"] == DBNull.Value ? "N/a" : reader["TimeOfLogOut"]);
                        setData.IPAddress = Convert.ToString(reader["IPAddress"] == DBNull.Value ? "N/a" : reader["IPAddress"]);
                        setData.Event = Convert.ToString(reader["Event"] == DBNull.Value ? "N/a" : reader["Event"]);
                        setData.Attempts = Convert.ToInt32(reader["Attempts"] == DBNull.Value ? 0 : reader["Attempts"]);


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
