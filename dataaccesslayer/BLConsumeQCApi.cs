using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using MiddleLayer;
using System.Threading;


using System.Web;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Configuration;

namespace DataAccessLayer
{
   public class BLConsumeQCApi
    {
        string ContentType = ConfigurationManager.AppSettings["ContentType"].ToString();
        string ClientExecuteQueryUrl = ConfigurationManager.AppSettings["ClientExecuteQueryUrl"].ToString();
        string UserId = ConfigurationManager.AppSettings["UserId"].ToString();
        string Password = ConfigurationManager.AppSettings["Password"].ToString();
        public string email;
        public int emailcount;
        public string authorizationHeader;

        string ClientTokenUrl = ConfigurationManager.AppSettings["ClienTokenUrl"].ToString();

        public void SessionToken()
        {
            authorizationHeader = HttpContext.Current.Session["authorization"].ToString();
            // authorizationHeader = authorizationHeaders.ToString();
        }
        public void getToken()
        {

            HttpContext.Current.Session["authorizationHeader"] = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var authorizationHeaderSession = HttpContext.Current.Session["authorizationHeader"];
            authorizationHeader = authorizationHeaderSession.ToString();
        }
        // string URL = System.Configuration.ConfigurationManager.AppSettings["RestApiUrl"];
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
                int lastindexs = response.Content.IndexOf(",", ii);
                string de = response.Content.Substring(ii + 2, (lastindexs - 3) - ii);
                return de;
            }
            int i = response.Content.IndexOf(":");
            int lastindex = response.Content.IndexOf(",", i);
            string d = response.Content.Substring(i + 2, (lastindex - 3) - i);
            return d;
        }


        public List<MlQC> getExceptionDataDetails()
        {
            SessionToken();
            //authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
           var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetExceptionDataDetails\",\r\n\t\"OperationType\":\"select\"\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
             if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
               // getToken();
               var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetExceptionDataDetails\",\r\n\t\"OperationType\":\"select\"\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlQC>>(responses.Content);
                return objResponse;
            }

            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlQC>>(response.Content);
            return objResponse1;
        }
        public List<MlUserDetails> getUserList(int Userid, int Userid1)
        {
            SessionToken();
            //authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
           var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetUserList\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserID\",\"ParamVal\":"+Userid+" },\r\n\t { \"ParamName\":\"@UserID1\",\"ParamVal\":"+Userid1+" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
             if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
               // getToken();
               var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetUserList\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserID\",\"ParamVal\":"+Userid+" },\r\n\t { \"ParamName\":\"@UserID1\",\"ParamVal\":"+Userid1+" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlUserDetails>>(responses.Content);
                return objResponse;
            }

            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
            return objResponse1;
        }
        public List<MlQC> getException_CPScreenData(int FaxId)
        {
            SessionToken();
            //authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
           var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetException_CPScreenDataFaxid\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@FaxID\",\"ParamVal\":"+FaxId+" }]\r\n\t \r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
             if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                //getToken();
               var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetException_CPScreenDataFaxid\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@FaxID\",\"ParamVal\":"+FaxId+" }]\r\n\t \r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlQC>>(responses.Content);
                return objResponse;
            }

            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlQC>>(response.Content);
            return objResponse1;
        }
    }
}
