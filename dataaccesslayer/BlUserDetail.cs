using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace DataAccessLayer
{
    public class BlUserDetail
    {
        public static string Pass, FromEmailid, HostAdd;
    
        public static void SendEmail(string fname, string lname, string Email, string BccEmailAddress, string EmailBody, string EmailSubject, string EmailNotificationPath, string SMTPHost, string SmtpPort, string SMTPCredentialFromAddress, string SMTPCedentialFromPassword, string time)
        {
            try
            {
                //string strPassword = Decryptdata(password);
                string useremail = Encryptusername(Email);
                string datetime = Encryptusername(time);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(SMTPCredentialFromAddress);
                mail.To.Add(Email);
                mail.Bcc.Add(BccEmailAddress);
                mail.Subject = EmailSubject;
                string FILENAME = EmailBody;
                string strText = FILENAME;

                strText = strText.Replace("qLPEnFBaxM", fname + " " + lname);
                strText = strText.Replace("8soD3dGF4s", Email);
                strText = strText.Replace("B85FRtyuyV", EmailNotificationPath + useremail + "&Date=" + DateTime.Now.ToShortDateString());
                strText = strText.Replace("##WEBSITELINK##", "http://192.168.1.17/index.aspx");
                strText = strText.Replace("S582TyhnyE", "DQF@fadv.com");
                mail.Body = strText;
                mail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(SMTPCredentialFromAddress, SMTPCedentialFromPassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                // MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
        }


        public static string Encryptusername(string plainText)
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
        public List<MlLogin> getUserDetails()
        {
            List<MlLogin> lstUserDetails = new List<MlLogin>();
            List<MlLogin> reader = new BLConsumeApi().getUserDetails();
            foreach (var r in reader)
            {
                MlLogin setData = new MlLogin();

                setData.UserDetailsID = r.UserDetailsID;

                setData.UserFirstName = r.UserFirstName;
                setData.UserLastName = r.UserLastName;
                setData.FADVUserID = r.FADVUserID;
                setData.RoleName = r.RoleName;
                setData.RoleId = r.RoleId;
                if (setData.RoleName == "Admin")
                {
                    setData.RoleName = "Admin";
                }
                if (setData.RoleName == "Data Entry Operator")
                {
                    setData.RoleName = "Data Entry Operator";
                }
                if (setData.RoleName == "QC Manager")
                {
                    setData.RoleName = "QC Manager";
                }
                if (setData.RoleName == "Audit Manager")
                {
                    setData.RoleName = "Audit Manager";
                }
                if (setData.RoleName == "Template Manager")
                {
                    setData.RoleName = "Template Manager";
                }
                if (setData.RoleName == "Client Manager")
                {
                    setData.RoleName = "Client Manager";
                }
                setData.LocktheAccount = r.LocktheAccount;
                if (setData.LocktheAccount.ToLower() == "false")
                {
                    setData.LocktheAccount = Convert.ToString("Active");
                }
                else
                {
                    setData.LocktheAccount = Convert.ToString("InActive");
                }
                setData.LoginAttempt = r.LoginAttempt;
                //  setData.LocktheAccount = r.LocktheAccount;
                setData.Password = r.Password;
                setData.EmailId = r.EmailId;
                setData.DateOfCreation = r.DateOfCreation;
                lstUserDetails.Add(setData);
            }
            return lstUserDetails;

        }
        
        private string Encrypt(string plainText)
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

        public bool setUserDetails(MlLogin Detail)
        {
            string subject = "Fadv Password";
            Random rdn = new Random();
            int random = rdn.Next(1000, 9000);
            string password = "fadv" + Convert.ToString(random);
            string enpassword = Encrypt(password);
            BLConsumeApi consume = new BLConsumeApi();
            consume.setUserDetails(Detail.UserFirstName, Detail.UserLastName, Detail.FADVUserID, Detail.RoleId, enpassword, Detail.EmailId, Detail.RoleName);
            //string body = "Hi '" + Detail.UserFirstName + "' '" + Environment.NewLine + "'The password for your FADV Account '" + Detail.EmailId + "' is '" + password + "' '" + Environment.NewLine + "'With Regards '" + Environment.NewLine + "'FADV Team";
            //body = body.Replace("@", "@" + System.Environment.NewLine);
            //Email_Without_Attachment(Detail.EmailId, subject, body);
            var emailvalue = new BLConsumeApi().GetEmailvalues("Password");
            SendEmailFornot(Detail.UserFirstName, Detail.UserLastName, Detail.EmailId, password, emailvalue[0].BccEmailAddress, emailvalue[0].EmailBody, emailvalue[0].EmailSubject, emailvalue[0].EmailNotificationPath, emailvalue[0].SMTPHost, emailvalue[0].SmtpPort, emailvalue[0].SMTPCredentialFromAddress, emailvalue[0].SMTPCedentialFromPassword);
            return true;
        }
        public void SendEmailFornot(string fname, string lname, string Email, string password, string BccEmailAddress, string EmailBody, string EmailSubject, string EmailNotificationPath, string SMTPHost, string SmtpPort, string SMTPCredentialFromAddress, string SMTPCedentialFromPassword)
        {
            try
            {
                //string strPassword = Decryptdata(password);
                string useremail = Encryptusername(Email);
                //string datetime = Encryptusername(time);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(SMTPCredentialFromAddress);
                mail.To.Add(Email);
                //mail.Bcc.Add();
                mail.Subject = EmailSubject;
                string FILENAME = EmailBody;
                string strText = FILENAME;

                strText = strText.Replace("qLPEnFBaxM", fname + " " + lname);
                strText = strText.Replace("9soD3dGF4s", Email);
                strText = strText.Replace("8soD3dGF4s", password);
                strText = strText.Replace("B85FRtyuyV", EmailNotificationPath + Email + "&Date=" + DateTime.Now.ToShortDateString());
                strText = strText.Replace("##WEBSITELINK##", "http://192.168.1.17/index.aspx");
                strText = strText.Replace("S582TyhnyE", "DQF@fadv.com");
                mail.Body = strText;
                mail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(SMTPCredentialFromAddress, SMTPCedentialFromPassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                // MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
        }
    

        public void updateUserDetails(MlLogin setData)
        {
            BLConsumeApi consume = new BLConsumeApi();
            consume.updateUserDetails(setData.UserFirstName, setData.UserLastName, setData.UserDetailsID, setData.RoleName, setData.EmailId);
        }
        public void updateLockUserDetails(MlLogin setData)
        {

            //string SqlQuery = null;
            if (setData.LocktheAccount == "Active")
            {
                BLConsumeApi consume = new BLConsumeApi();

                consume.LockUserDetails(setData.UserDetailsID);
            }
            if (setData.LocktheAccount == "InActive")
            {

                BLConsumeApi consume = new BLConsumeApi();
                consume.UnlockLockUserDetails(setData.UserDetailsID);
            }
        }
        public void updateLockUserDetailsToZero(MlLogin setData)
        {
            BLConsumeApi consume = new BLConsumeApi();
            //consume.LockUserDetails(setData.UserDetailsID);
        }
        public void DeleteUserDetails(MlLogin deldata)
        {
            BLConsumeApi consume = new BLConsumeApi();
            consume.deleteUserDetails(deldata.UserDetailsID);
        }
        public void changepassword(int UserDetailsID, string newpassword)
        {
            string enpassword = Encrypt(newpassword);
            new BLConsumeApi().updateChangePassword(UserDetailsID, enpassword);
        }
    }
}


