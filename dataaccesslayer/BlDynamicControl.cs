using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MiddleLayer;

using System.Configuration;
using System.Web.UI.WebControls;

namespace DataAccessLayer
{
    public class BlDynamicControl
    {
        public static string GetConnectionString()
        {
             return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }

//        public List<MlDynamicControl> getDynamicControlsBydocIdCustomerId(int docId, int customerId)
//        {
//            SqlConnection SqlConn = new SqlConnection();
//            SqlConn.ConnectionString = GetConnectionString();
//            List<MlDynamicControl> lstUserDetails = new List<MlDynamicControl>();
//            string SqlQuery = null;
//            SqlQuery = @"SELECT DynamicControl.DocumentTypeID, DynamicControl.labelName, DynamicControl.ControlType, DocumentType.DocumentTypeName, DocumentType.CustomerID, CustomerDetails.CompanyName
//FROM            DynamicControl INNER JOIN
//                         DocumentType ON DynamicControl.DocumentTypeID = DocumentType.DocumentTypeID INNER JOIN
//                         CustomerDetails ON DocumentType.CustomerID = CustomerDetails.CompanyID where DocumentType.DocumentTypeID='" + docId + "' and CustomerID='" + customerId + "' and DynamicControl.IsActive=1";

//            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
//            {
//                try
//                {
//                    SqlConn.Open();
//                    SqlCom.Parameters.AddWithValue("@UserDetailsId", Config.userId);
//                    SqlDataReader reader = SqlCom.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        MlDynamicControl setData = new MlDynamicControl();


//                        setData.DynamicControlID = Convert.ToInt32(reader["DynamicControlID"]);
//                        setData.DocumentTypeID = Convert.ToInt32(reader["DocumentTypeID"]);
//                        setData.DocumentTypeName = Convert.ToString(reader["DocumentTypeName"]);
//                        setData.labelName = Convert.ToString(reader["labelName"]);
//                        setData.ControlName = Convert.ToString(reader["ControlName"]);
//                        setData.ControlType = Convert.ToString(reader["ControlType"]);
//                        setData.CompanyName = Convert.ToString(reader["CompanyName"]);
//                        lstUserDetails.Add(setData);

//                    }

//                    SqlConn.Close();
//                    return lstUserDetails;

//                }
//                catch (Exception e)
//                {
//                    return lstUserDetails;

//                }

//            }
//        }
        public List<MlDynamicControl> getDynamicControls()
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlDynamicControl> lstUserDetails = new List<MlDynamicControl>();
            string SqlQuery = null;
            SqlQuery = @"SELECT DynamicControl.DynamicControlID, DynamicControl.DocumentTypeID, DynamicControl.labelName, DynamicControl.ControlName, DynamicControl.ControlType, DocumentType.DocumentTypeName
FROM            DynamicControl INNER JOIN
                         DocumentType ON DynamicControl.DocumentTypeID = DocumentType.DocumentTypeID where DynamicControl.IsActive=1";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.Parameters.AddWithValue("@UserDetailsId", Config.userId);
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlDynamicControl setData = new MlDynamicControl();


                        setData.DynamicControlID = Convert.ToInt32(reader["DynamicControlID"]);
                        setData.DocumentTypeID = Convert.ToInt32(reader["DocumentTypeID"]);
                        setData.DocumentTypeName = Convert.ToString(reader["DocumentTypeName"]);
                        setData.labelName = Convert.ToString(reader["labelName"]);
                        setData.ControlName = Convert.ToString(reader["ControlName"]);
                        setData.ControlType = Convert.ToString(reader["ControlType"]);
                        if (setData.ControlType == "TextBox")
                        {
                            setData.ControlTypeID = 1;
                        }
                        if (setData.ControlType == "CheckBox")
                        {
                            setData.ControlTypeID = 2;
                        }
                        if (setData.ControlType == "Calender")
                        {
                            setData.ControlTypeID = 3;
                        }
                        if (setData.ControlType == "Table")
                        {
                            setData.ControlTypeID = 4;
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
        public bool setDynamicControl(MlDynamicControl Detail)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();

            string SqlQuery = null;
            SqlQuery = @"INSERT INTO [dbo].[DynamicControl]
           ([DocumentTypeID]
           ,[labelName]
           ,[ControlName]
           ,[ControlType])
     VALUES (@DocumentTypeID,@labelName,@ControlName,@ControlType)";


            using (SqlCommand SqlComm = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    MlDynamicControl SetDetails = new MlDynamicControl();
                    SqlComm.Parameters.AddWithValue("@DocumentTypeID", Detail.DocumentTypeID);
                    SqlComm.Parameters.AddWithValue("@labelName", Detail.labelName);
                    SqlComm.Parameters.AddWithValue("@ControlName", Detail.ControlName);
                    SqlComm.Parameters.AddWithValue("@ControlType", Detail.ControlType);

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
        public bool updateDynamicControl(MlDynamicControl setData)
        {
            new BLConsumeApi().updateDynamicControl(setData);
            return true;
        }
        public bool DeleteUserDetails(MlDynamicControl deldata)
        {
            new BLConsumeApi().deleteDynamicControlValue(deldata.DynamicControlID);
            return true;
        }
        public int UpdateOrderno(int id, int orderno)
        {

            try
            {
                new BLConsumeApi().UpdateOrderno(id,orderno);
                return 1;
            }
            catch (Exception e)
            {
                e.ToString();
                return 0;

            }

         
        }

        

        public static void ddldocumenttypeByID(DropDownList ddlCustID,int customerId)
        {
            ddlCustID.Items.Clear();
            DataTable dtddlCustomerName;
            ListItem li = new ListItem();
            li.Text = "<--Select Document Name-->";
            li.Value = "0";
            ddlCustID.Items.Add(li);
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(new BLConsumeApi().getAllDocumentTypeByCustomerId(customerId));
            dtddlCustomerName = dt;
            for (int i = 0; i < dtddlCustomerName.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtddlCustomerName.Rows[i]["DocumentTypeID"].ToString();
                li.Text = dtddlCustomerName.Rows[i]["DocumentTypeName"].ToString();
                ddlCustID.Items.Add(li);
            }
        }

    }
}
