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
  public  class BlUserLogin
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        }
        private static string Encrypt(string plainText)
        {
            string EncryptionKey = "3a5c1c4e81d7eb133a5c1c4e81d7eb13";
            System.Security.Cryptography.Rijndael tdes = System.Security.Cryptography.Rijndael.Create();
            tdes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            tdes.Mode = System.Security.Cryptography.CipherMode.ECB;
            tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            System.Security.Cryptography.ICryptoTransform crypt = tdes.CreateEncryptor();
            byte[] plain = Encoding.UTF8.GetBytes(plainText);
            byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
            plainText = Convert.ToBase64String(cipher);
            return plainText;
        }

        public List<MlLogin> signIN(MlLogin login)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlLogin> lstUserDetails = new List<MlLogin>();


            string SqlQuery = null;
            SqlQuery = @"SELECT [UserDetailsID]
      ,[UserFirstName]
      ,[UserLastName]
      ,[FADVUserID]
      ,[RoleId]
      ,[Password]
      ,[DateOfCreation]
      ,[DateOfModification]
  FROM [dbo].[UserDetails] where [FADVUserID]=@FADVUserID and [Password]=@Password";
            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.Parameters.AddWithValue("@FADVUserID", login.FADVUserID);
                    string encryted=Encrypt(login.Password);
                    SqlCom.Parameters.AddWithValue("@Password", encryted);

                    SqlCom.CommandType = CommandType.Text;
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlLogin authentication = new MlLogin();
                        authentication.UserDetailsID = Convert.ToInt32(reader["UserDetailsID"]);
                        authentication.UserFirstName = Convert.ToString(reader["UserFirstName"]);
                        authentication.UserLastName = Convert.ToString(reader["UserLastName"]);
                        authentication.FADVUserID = Convert.ToInt32(reader["FADVUserID"]);
                        authentication.RoleId = Convert.ToInt32(reader["RoleId"]);
                        authentication.Password = Convert.ToString(reader["Password"]);
                        authentication.DateOfCreation = Convert.ToString(reader["DateOfCreation"]);
                        authentication.DateOfModification = Convert.ToDateTime(reader["DateOfModification"]);
                        lstUserDetails.Add(authentication);
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
        public void loginAttempt(int UserDetailsID)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            string SqlQuery = null;
            SqlQuery = @"update [dbo].[UserDetails] set LoginAttempt=0  WHERE [UserDetailsID]=" + UserDetailsID + "";

            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlCom.CommandType = CommandType.Text;
                    try
                    {
                        SqlCom.ExecuteNonQuery();
                        SqlConn.Close();
                        //return true;
                    }
                    catch (Exception e)
                    {
                        e.ToString();
                        //return false;

                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                    //return false;
                }


            }

        }
    }
}
