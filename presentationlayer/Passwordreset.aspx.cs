using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;

namespace PresentationLayer
{
    public partial class Passwordreset : System.Web.UI.Page
    {
        string email; string date;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
            
        }
        private static string Decrypt(string encrypttext)
        {
            string EncryptionKey = "3a5c1c4e81d7eb133a5c1c4e81d7eb13";

            System.Security.Cryptography.Rijndael tdes = System.Security.Cryptography.Rijndael.Create();
            tdes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            tdes.Mode = System.Security.Cryptography.CipherMode.ECB;
            tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            System.Security.Cryptography.ICryptoTransform crypt = tdes.CreateDecryptor();


            //byte[] plain = Encoding.UTF8.GetBytes(encrypttext);
            byte[] decryptedByteValue = Convert.FromBase64String(encrypttext);
            byte[] cipher = crypt.TransformFinalBlock(decryptedByteValue, 0, decryptedByteValue.Length);
            string clearText = System.Text.Encoding.UTF8.GetString(cipher);
            return clearText;
        }
        public static string pass(string plainText)
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
        protected void btnloginfordefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            email = (Request.QueryString["Email"]);
            date = (Request.QueryString["Date"]);
            string emaildec = Decrypt(email);
           // string datedec = Decrypt(date);
            string time = DateTime.Now.ToShortDateString();
            if (date == time)
            {
                string passworden = pass(txtPassword.Text);
               new BLConsumeApi().ResetPassword(passworden, emaildec);
                string message = "Reset Password SucessFully";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                lblMessage.Text = "";
                
            }
        }
    }
}