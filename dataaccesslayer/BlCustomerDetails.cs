using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;
using System.Data;
using RestSharp;
using Newtonsoft.Json;

namespace DataAccessLayer
{


    public class BlCustomerDetails
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }
        public List<MlCustomerDetails> getCustomerDetails()
        {
            List<MlCustomerDetails> lstUserDetails = new List<MlCustomerDetails>();
            List<MlCustomerDetails> reader = new BLConsumeApi().getCustomerDetailsAll();
            foreach (var r in reader)
            {
                MlCustomerDetails setData = new MlCustomerDetails();

                setData.CompanyName = r.CompanyName;

                setData.FADV_CustomerID = r.FADV_CustomerID;
                setData.CompanyID = r.CompanyID;
                setData.DateofCreation = r.DateDisplayCreation;             
               

                lstUserDetails.Add(setData);
            }
            return lstUserDetails;
            //MlCustomerDetails account = JsonConvert.DeserializeObject<MlCustomerDetails>(response.Content);


            //setData.CompanyName = Convert.ToString(reader["CompanyName"]);

            //setData.FADV_CustomerID = Convert.ToInt32(reader["FADV_CustomerID"]);

            //setData.CompanyID = Convert.ToInt32(reader["CompanyID"]);

            //lstCustomerDetails.Add(setData);

         
            
            
        }
        public DataTable getFadvFaxidexit(string FadvFaxid)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"Select FADV_CustomerID from CustomerDetails where FADV_CustomerID='" + FadvFaxid + "' and IsActive=1";


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
        public DataTable getCompanyexit(string Companyexit)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            DataTable lstUserDetails = new DataTable();
            string SqlQuery = null;
            SqlQuery = @"Select CompanyName from CustomerDetails where CompanyName='" + Companyexit + "' and IsActive=1";


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
        public bool addCustomerDetails(MlCustomerDetails Customername)
        {
            BLConsumeApi consume = new BLConsumeApi();
            consume.setcustomerDetails(Customername.FADV_CustomerID,Customername.CompanyName);
            return true;
        }
        public bool updateCustomerDetails(MlCustomerDetails setData)
        {
            BLConsumeApi consume = new BLConsumeApi();
            consume.updatecustomerDetails(setData.FADV_CustomerID, setData.CompanyName, setData.CompanyID);
            return true;
        }
        public bool DeleteCustomerDetails(MlCustomerDetails deldata)
        {
            BLConsumeApi consume = new BLConsumeApi();
            consume.datelecustomerDetails(deldata.CompanyID);
            return true;
        }
    }
}
