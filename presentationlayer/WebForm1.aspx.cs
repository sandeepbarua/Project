using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestSharp;
using System.Configuration;
using System.Net;
using FADVCustomLibrary;
using System.Data;
using System.IO;

namespace PresentationLayer
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string UserId = ConfigurationManager.AppSettings["UserId"].ToString();
        string Password = ConfigurationManager.AppSettings["Password"].ToString();
        string grantType = ConfigurationManager.AppSettings["GrantType"].ToString();

        string ClientExecuteQueryUrl = ConfigurationManager.AppSettings["ClientExecuteQueryUrl"].ToString();

        string ClientTokenUrl = ConfigurationManager.AppSettings["ClienTokenUrl"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           string tokenId = generateToken(grantType, UserId, Password);
            Session["authorization"] = tokenId;

            string filename = Path.GetFileName(FileUpload1.FileName);



            FileUpload1.SaveAs(Server.MapPath("~/ExcelLoad") + filename);

            UploadExcelLocation(Path.GetFileName(filename), Server.MapPath("~/ExcelLoad") , "Location", "Sheet1$");
            
        }

        private  void UploadExcelLocation(string FileName, string FilePath, string DbTableName, string SheetName)
        {
            DQFBuldLoadRequest req = new DQFBuldLoadRequest();
            req.FileName = FileName;
            req.FileLocation = FilePath;
            req.SheetName = SheetName;
            req.Token = Convert.ToString(Session["authorization"]);
            
            req.TableName = DbTableName;


       
            BulkLoad(req);

        }

        public static void BulkLoad( DQFBuldLoadRequest req)
        {

            try
            {
                DQFController obj = new DQFController();
                DataTable DT = obj.ExecuteBulkLoad(req);
              
              
            }
            catch (Exception ex)
            {

            }
        }

        public string generateToken(string grant_type, string username, string password)
        {

            var client = new RestClient(ClientTokenUrl);
            var request = new RestRequest(Method.POST);
            //ServicePointManager.Expect100Continue = false;
            //ServicePointManager.MaxServicePointIdleTime = 0;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=" + grant_type + "&username=" + username + "&password=" + password + "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                var clients = new RestClient(ClientTokenUrl);
                var requests = new RestRequest(Method.POST);
                //ServicePointManager.Expect100Continue = false;
                //ServicePointManager.MaxServicePointIdleTime = 0;
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("content-type", "application/x-www-form-urlencoded");
                requests.AddParameter("application/x-www-form-urlencoded", "grant_type=" + grant_type + "&username=" + username + "&password=" + password + "", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                int ii = responses.Content.IndexOf(":");
                int lastindexs = responses.Content.IndexOf(",", ii);
                string de = responses.Content.Substring(ii + 2, (lastindexs - 3) - ii);
                return de;
            }
            int i = response.Content.IndexOf(":");
            int lastindex = response.Content.IndexOf(",", i);
            string d = response.Content.Substring(i + 2, (lastindex - 3) - i);
            return d;
        }
    }
}