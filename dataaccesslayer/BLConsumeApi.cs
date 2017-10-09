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
    public class BLConsumeApi
    {

        string ContentType = ConfigurationManager.AppSettings["ContentType"].ToString();
        string ClientExecuteQueryUrl = ConfigurationManager.AppSettings["ClientExecuteQueryUrl"].ToString();
        string UserId = ConfigurationManager.AppSettings["UserId"].ToString();
        string Password = ConfigurationManager.AppSettings["Password"].ToString();
        public string email;
        public int emailcount;
        public string authorizationHeader;

        string ClientTokenUrl = ConfigurationManager.AppSettings["ClienTokenUrl"].ToString();
        public void SessionToken() {
            authorizationHeader = HttpContext.Current.Session["authorization"].ToString();
            // authorizationHeader = authorizationHeaders.ToString();
        }
        public void getToken() {

            HttpContext.Current.Session["authorizationHeader"] = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var authorizationHeaderSession = HttpContext.Current.Session["authorizationHeader"];
            authorizationHeader = authorizationHeaderSession.ToString();
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
                int lastindexs = response.Content.IndexOf(",", ii);
                string de = response.Content.Substring(ii + 2, (lastindexs - 3) - ii);
                return de;
            }
            int i = response.Content.IndexOf(":");
            int lastindex = response.Content.IndexOf(",", i);
            string d = response.Content.Substring(i + 2, (lastindex - 3) - i);
            return d;
        }

        public void updateChangePassword(int UserDetailsId, string password)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"updateChangePassword\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + UserDetailsId + " },\r\n\t\t{ \"ParamName\":\"@password\",\"ParamVal\":\"" + password + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"updateChangePassword\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + UserDetailsId + " },\r\n\t\t{ \"ParamName\":\"@password\",\"ParamVal\":\"" + password + "\" }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw response.ErrorException;
                }

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);
        }


        public List<MlCustomerDetails> getCustomerDetails()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"uspGetAllCustomers\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"uspGetAllCustomers\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(response.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(response.Content);
            return objResponse1;
        }
        public List<MIDocumentType> usp_DocumentTypesListByCustomerID(Int32 CustomerId)
        {
            SessionToken();
            //var client = new RestClient(ClientExecuteQueryUrl);
            //var request = new RestRequest(Method.POST);
            ////request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", authorizationHeader);
            //request.AddHeader("content-type", "application/json");
            //request.AddParameter("application/json", "{\r\n\"OperationName\":\"uspGetAllCustomers\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            ////List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(response.Content);
            //return objResponse1;

            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"usp_DocumentTypesListByCustomerID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\":\"" + CustomerId + "\" }\r\n\t\t]\r\n\t\r\n\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"usp_DocumentTypesListByCustomerID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\":\"" + CustomerId + "\" }\r\n\t\t]\r\n\t\r\n\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

            return documentTypeList;
        }
        public List<MlLogin> getUserDetails()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_SelectUserDetails\",\r\n\"OperationType\":\"select\"\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                //ServicePointManager.Expect100Continue = false;
                //ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_SelectUserDetails\",\r\n\"OperationType\":\"select\"\r\n\r\n }\r\n", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlLogin>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);
            return objResponse1;
        }
        public List<MlUserLoginDetail> getUserDetailszeroidwise()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"SP_GetUserDetailzeroidwise\",\r\n\"OperationType\":\"select\"\r\n\r\n }\r\n", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                //ServicePointManager.Expect100Continue = false;
                //ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"SP_GetUserDetailzeroidwise\",\r\n\"OperationType\":\"select\"\r\n\r\n }\r\n", ParameterType.RequestBody);

                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlUserLoginDetail>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlUserLoginDetail>>(response.Content);
            return objResponse1;
        }
        public List<MlUserLoginDetail> getUserDetailsidwise(int Userid)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"SP_GetUserDetailidwise\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + Userid + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            //  request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"SP_GetUserDetailidwise\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + Userid + "\" }\r\n\t\t]\r\n\t\r\n\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                //ServicePointManager.Expect100Continue = false;
                //ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"SP_GetUserDetailidwise\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + Userid + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlUserLoginDetail>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlUserLoginDetail>>(response.Content);
            return objResponse1;
        }
        public void setUserDetails(string UserFirstName, string UserLastName, int FADVUserID, int RoleID, string Password, string Email, String RoleName)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_InsertUserDetails\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserFirstName\",\"ParamVal\":\"" + UserFirstName + "\" },\r\n\t{ \"ParamName\":\"@UserLastName\",\"ParamVal\":\"" + UserLastName + "\" },\r\n\t{ \"ParamName\":\"@FADVUserID\",\"ParamVal\":" + FADVUserID + " },\r\n\t{ \"ParamName\":\"@RoleID\",\"ParamVal\":" + RoleID + " },\r\n\t{ \"ParamName\":\"@Password\",\"ParamVal\":\"" + Password + "\" },\r\n\t{ \"ParamName\":\"@Email\",\"ParamVal\":\"" + Email + "\" },\r\n\t{ \"ParamName\":\"@RoleName\",\"ParamVal\":\"" + RoleName + "\" }]\r\n }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_InsertUserDetails\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserFirstName\",\"ParamVal\":\"" + UserFirstName + "\" },\r\n\t{ \"ParamName\":\"@UserLastName\",\"ParamVal\":\"" + UserLastName + "\" },\r\n\t{ \"ParamName\":\"@FADVUserID\",\"ParamVal\":" + FADVUserID + " },\r\n\t{ \"ParamName\":\"@RoleID\",\"ParamVal\":" + RoleID + " },\r\n\t{ \"ParamName\":\"@Password\",\"ParamVal\":\"" + Password + "\" },\r\n\t{ \"ParamName\":\"@Email\",\"ParamVal\":\"" + Email + "\" },\r\n\t{ \"ParamName\":\"@RoleName\",\"ParamVal\":\"" + RoleName + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void updateUserDetails(string UserFirstName, string UserLastName, int UserDetailsId, string RoleName, string Email)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_UpdateUserDetails\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserFirstName\",\"ParamVal\":\"" + UserFirstName + "\" },\r\n\t\t{ \"ParamName\":\"@UserLastName\",\"ParamVal\":\"" + UserLastName + "\" },\r\n\t\t{ \"ParamName\":\"@RoleName\",\"ParamVal\":\"" + RoleName + "\" },\r\n\t\t{ \"ParamName\":\"@UserDetailsId\",\"ParamVal\": " + UserDetailsId + " },\r\n\t\t{ \"ParamName\":\"@EmailId\",\"ParamVal\":\"" + Email + "\" }]\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_UpdateUserDetails\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserFirstName\",\"ParamVal\":\"" + UserFirstName + "\" },\r\n\t\t{ \"ParamName\":\"@UserLastName\",\"ParamVal\":\"" + UserLastName + "\" },\r\n\t\t{ \"ParamName\":\"@RoleName\",\"ParamVal\":\"" + RoleName + "\" },\r\n\t\t{ \"ParamName\":\"@UserDetailsId\",\"ParamVal\": " + UserDetailsId + " },\r\n\t\t{ \"ParamName\":\"@EmailId\",\"ParamVal\":\"" + Email + "\" }]\r\n }\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw response.ErrorException;
                }

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);
        }
        public void LockUserDetails(int UserDetailsId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_UpdateUserDetailsID\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserDetailsId\",\"ParamVal\": " + UserDetailsId + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_UpdateUserDetailsID\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserDetailsId\",\"ParamVal\": " + UserDetailsId + " }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw response.ErrorException;
                }

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);
        }
        public void UnlockLockUserDetails(int UserDetailsId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_UpdateLoginAttempt\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserDetailsId\",\"ParamVal\": " + UserDetailsId + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");

                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_UpdateLoginAttempt\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserDetailsId\",\"ParamVal\": " + UserDetailsId + " }]\r\n }", ParameterType.RequestBody); IRestResponse responses = client.Execute(request);
                response = clients.Execute(requests);
                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw response.ErrorException;
                }

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);
        }
        public List<MIDocumentType> GetDataEntry(int USERID)
        {
            SessionToken();


            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"DataEntryReportOfUser\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + USERID + "\" }\r\n\t\t]\r\n\t\r\n\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"DataEntryReportOfUser\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + USERID + "\" }\r\n\t\t]\r\n\t\r\n\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

            return documentTypeList;
        }
        public void deleteUserDetails(int UserDetailsId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_UpdateUserDetailsbyID\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + UserDetailsId + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_UpdateUserDetailsbyID\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + UserDetailsId + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw response.ErrorException;
                }

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);
        }
        public List<MlCustomerDetails> getCustomerDetailsAll()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_getCustomerDetailAll\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_getCustomerDetailAll\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(response.Content);
            return objResponse1;
        }
        public List<MlLogin> getUserDetailsUserIdWise(int UserDetailsID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_GetUserDetailsIdWise\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\":" + UserDetailsID + " }]\r\n\t\t\t\t\t\t\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                //ServicePointManager.Expect100Continue = false;
                //ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_GetUserDetailsIdWise\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\":" + UserDetailsID + " }]\r\n\t\t\t\t\t\t\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlLogin>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);
            return objResponse1;
        }
        public void setcustomerDetails(int FADV_CustomerID, string CompanyName)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_InsertcustomerDetails\",\r\n\"OperationType\":\"insert\",\r\n\"ParameterList\": [{ \"ParamName\":\"@FADV_CustomerID\",\"ParamVal\":\"" + FADV_CustomerID + "\" },\r\n\t\t{ \"ParamName\":\"@CompanyName\",\"ParamVal\":\"" + CompanyName + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_InsertcustomerDetails\",\r\n\"OperationType\":\"insert\",\r\n\"ParameterList\": [{ \"ParamName\":\"@FADV_CustomerID\",\"ParamVal\":\"" + FADV_CustomerID + "\" },\r\n\t\t{ \"ParamName\":\"@CompanyName\",\"ParamVal\":\"" + CompanyName + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void updatecustomerDetails(int FADV_CustomerID, string CompanyName, int CompanyID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");

            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateCompNameID\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CompanyName\",\"ParamVal\":\"" + CompanyName + "\" },\r\n\t\t{ \"ParamName\":\"@FADV_CustomerID\",\"ParamVal\": " + FADV_CustomerID + " },\r\n\t\t{ \"ParamName\":\"@CompanyID\",\"ParamVal\": " + CompanyID + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateCompNameID\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CompanyName\",\"ParamVal\":\"" + CompanyName + "\" },\r\n\t\t{ \"ParamName\":\"@FADV_CustomerID\",\"ParamVal\": " + FADV_CustomerID + " },\r\n\t\t{ \"ParamName\":\"@CompanyID\",\"ParamVal\": " + CompanyID + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void datelecustomerDetails(int CompanyID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");

            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateCustomerIsActive\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CompanyID\",\"ParamVal\":\"" + CompanyID + "\"}]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");

                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateCustomerIsActive\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CompanyID\",\"ParamVal\":\"" + CompanyID + "\"}]\r\n}\r\n\r\n", ParameterType.RequestBody);

                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public List<MlLocation> getCustomerLocationDetailsAll()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_SelectLocactionbyCustomerdetails\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_SelectLocactionbyCustomerdetails\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlLocation>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlLocation>>(response.Content);
            return objResponse1;
        }
        public List<MlCustomerDetails> bindDropDowngetCustomerDetailsAll()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_getCustomerDetailAll\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_getCustomerDetailAll\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(response.Content);
            return objResponse1;
        }
        public List<MlLocation> getCustomerLocationDetailsByCustomerID(int customerId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_SelectLocationbyCustomerID\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + customerId + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_SelectLocationbyCustomerID\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + customerId + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlLocation>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlLocation>>(response.Content);
            return objResponse1;
        }
        public void setLocationDetails(string FADV_LocationID, int CustomerID, string LocationName)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_InsertLocations\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Fadv_LocationID\",\"ParamVal\": \"" + FADV_LocationID + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + CustomerID + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@LocationName\",\"ParamVal\": \"" + LocationName + "\" }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_InsertLocations\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Fadv_LocationID\",\"ParamVal\": \"" + FADV_LocationID + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + CustomerID + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@LocationName\",\"ParamVal\": \"" + LocationName + "\" }]\r\n}\r\n\r\n", ParameterType.RequestBody);

                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void updateLocationDetails(string FADV_LocationID, int CustomerID, string LocationName, int LocationId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateLocation\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Fadv_LocationID\",\"ParamVal\": \"" + FADV_LocationID + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + CustomerID + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@LocationName\",\"ParamVal\": \"" + LocationName + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@LocationId\",\"ParamVal\": " + LocationId + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateLocation\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Fadv_LocationID\",\"ParamVal\": \"" + FADV_LocationID + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + CustomerID + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@LocationName\",\"ParamVal\": \"" + LocationName + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@LocationId\",\"ParamVal\": " + LocationId + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);

                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void deleteLocationDetails(int LocationId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateLocationID\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@LocationId\",\"ParamVal\": " + LocationId + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateLocationID\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@LocationId\",\"ParamVal\": " + LocationId + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public List<MlCustomerDetails> bindDropDowngetCustomerDetailsByID(int CUstomerID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_getCustomerDetailByCustomerID\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CompanyId\",\"ParamVal\": " + CUstomerID + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_getCustomerDetailByCustomerID\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CompanyId\",\"ParamVal\": " + CUstomerID + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(response.Content);
            return objResponse1;
        }

        public List<MlCustomerDetails> bindDropDownCustomerAll()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_selectCompanyAll\",\r\n\t\"OperationType\":\"select\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_selectCompanyAll\",\r\n\t\"OperationType\":\"select\"\r\n}", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(response.Content);
            return objResponse1;
        }


        public List<MlCustomerDetails> bindDropDownDocTypeAll()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_selectCompanyAll\",\r\n\t\"OperationType\":\"select\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_selectCompanyAll\",\r\n\t\"OperationType\":\"select\"\r\n}", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(response.Content);
            return objResponse1;
        }
        public List<MIDocumentType> getAllDocumentType()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_SelectDocTypeisActive\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_SelectDocTypeisActive\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);
            return objResponse1;
        }

        public List<MIDocumentType> getAllDocumentTypeByCustomerId(int customerId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_selectDocTypeByCustId\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + customerId + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_selectDocTypeByCustId\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + customerId + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);
            return objResponse1;
        }
        public List<MIDocumentType> getAllDocumentTypeByCustomerID(int CustomerId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_SelectDocumentType\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CompanyId\",\"ParamVal\": " + CustomerId + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_SelectDocumentType\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CompanyId\",\"ParamVal\": " + CustomerId + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);
            return objResponse1;
        }
        public void setDocumentDynamicDetails(int DocumentTypeID, string labelName, string ControlName, string ControlType, string DropDownValue)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_InsertDynamicControl\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + "  },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@labelName\",\"ParamVal\": \"" + labelName + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@ControlName\",\"ParamVal\": \"" + ControlName + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@ControlType\",\"ParamVal\": \"" + ControlType + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@DropDownValue\",\"ParamVal\": \"" + DropDownValue + "\" },]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_InsertDynamicControl\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + "  },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@labelName\",\"ParamVal\": \"" + labelName + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@ControlName\",\"ParamVal\": \"" + ControlName + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@ControlType\",\"ParamVal\": \"" + ControlType + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@DropDownValue\",\"ParamVal\": \"" + DropDownValue + "\" },]\r\n}\r\n\r\n", ParameterType.RequestBody);

                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public List<MIDocumentType> getTopDocumentTypeByCustomerID()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_SelectTopDocumentType\",\r\n\t\"OperationType\":\"select\"\r\n}\r\n\r\n", ParameterType.RequestBody); IRestResponse response = client.Execute(request);
            response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_SelectTopDocumentType\",\r\n\t\"OperationType\":\"select\"\r\n}\r\n\r\n", ParameterType.RequestBody); IRestResponse responses = client.Execute(request);
                response = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);
            return objResponse1;
        }
        public void setDocumentTypeDetails(string DocumentTypeName, int CustomerID, string DocumentDescription, int UserID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_InsertDocumentType\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeName\",\"ParamVal\":\"" + DocumentTypeName + "\"  },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + CustomerID + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@DocumentDescription\",\"ParamVal\": \"" + DocumentDescription + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@UserID\",\"ParamVal\": " + UserID + " }]\r\n}\r\n\r\n", ParameterType.RequestBody); IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_InsertDocumentType\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeName\",\"ParamVal\":\"" + DocumentTypeName + "\"  },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + CustomerID + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@DocumentDescription\",\"ParamVal\": \"" + DocumentDescription + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@UserID\",\"ParamVal\": " + UserID + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void updateDocumentTypeDetails(string DocumentTypeName, int CustomerID, string DocumentDescription, int UserID, int DocumentTypeID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateDocumentType\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeName\",\"ParamVal\":\"" + DocumentTypeName + "\"  },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + CustomerID + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@DocumentDescription\",\"ParamVal\": \"" + DocumentDescription + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@UserID\",\"ParamVal\": " + UserID + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateDocumentType\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeName\",\"ParamVal\":\"" + DocumentTypeName + "\"  },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + CustomerID + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@DocumentDescription\",\"ParamVal\": \"" + DocumentDescription + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@UserID\",\"ParamVal\": " + UserID + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void deleteDocumentTypeDetails(int DocumentTypeID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateDocumenttypeID\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + "  }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateDocumenttypeID\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + "  }]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void ReferenceDeleteDocumentType(int DocumentTypeID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateDynamiccontroldoctypeid\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\":" + DocumentTypeID + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_UpdateDynamiccontroldoctypeid\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + "}]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public string getemailidexit(int UserID)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_getemailidexit\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + UserID + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_getemailidexit\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + UserID + "}]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponses = JsonConvert.DeserializeObject<List<MlLogin>>(responses.Content);
                foreach (var getEmails in objResponses)
                {
                    email = getEmails.EmailId;
                    return email;
                }

            }
            var objResponse = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);
            foreach (var getEmail in objResponse) {
                email = getEmail.EmailId;
                return email;
            }
            return email;


        }
        public int getExistingemailid(int UserID, string existingEmail)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_getExistingEmailid\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + UserID + " },\r\n\t\t\t\t\t\t\t{ \"ParamName\":\"@EmailId\",\"ParamVal\": \"" + existingEmail + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_getExistingEmailid\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + UserID + " },\r\n\t\t\t\t\t\t\t{ \"ParamName\":\"@EmailId\",\"ParamVal\": \"" + existingEmail + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponses = JsonConvert.DeserializeObject<List<MlLogin>>(responses.Content);
                foreach (var item in objResponses)
                {
                    int currentValue = item.Column1;
                    emailcount = Convert.ToInt32(currentValue);
                    return emailcount;

                }


            }
            var objResponse = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);
            foreach (var item in objResponse)
            {
                int currentValue = item.Column1;
                emailcount = Convert.ToInt32(currentValue);
                return emailcount;
            }
            return emailcount;
        }
        public List<MlPreviousEmployerDetail> SelectPreviousEmployerDetailsByAllIds(int DocumentId, int UserId, int TempCPScreenDataID, int CountForFaxId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_SelectPreviousEmployerDetailsByAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_SelectPreviousEmployerDetailsByAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlPreviousEmployerDetail>>(responses.Content);
                return objResponse;
            }
            var objResponse1 = JsonConvert.DeserializeObject<List<MlPreviousEmployerDetail>>(response.Content);
            return objResponse1;


        }
        public void deletePreviousEmployerDetailId(int PreviousEmployerDetailId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_deletePreviousEmployerDetailsById\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmployerDetailId\",\"ParamVal\": " + PreviousEmployerDetailId + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_deletePreviousEmployerDetailsById\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmployerDetailId\",\"ParamVal\": " + PreviousEmployerDetailId + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }

        public void insertPreviousEmployerDetailId(MlPreviousEmployerDetail mlPreviousEmployerDetail)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertPreviousEmployerDetails\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmployerName\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmployerName + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmployerStreetAddress\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmployerStreetAddress + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmployerCity\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmployerCity + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmployerState\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmployerState + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmployerZipcode\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmployerZipcode + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmploymentStartDate\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmploymentStartDate + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmploymentEndDate\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmploymentEndDate + "\" },\r\n\t\t{ \"ParamName\":\"@ReasonForLeavingPreviousEmployments\",\"ParamVal\": \"" + mlPreviousEmployerDetail.ReasonForLeavingPreviousEmployments + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlPreviousEmployerDetail.UserId + " },\r\n\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlPreviousEmployerDetail.DocumentTypeId + " },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlPreviousEmployerDetail.TempCPScreenDataID + " },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlPreviousEmployerDetail.CountForFaxId + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != 0)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertPreviousEmployerDetails\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmployerName\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmployerName + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmployerStreetAddress\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmployerStreetAddress + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmployerCity\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmployerCity + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmployerState\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmployerState + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmployerZipcode\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmployerZipcode + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmploymentStartDate\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmploymentStartDate + "\" },\r\n\t\t{ \"ParamName\":\"@PreviousEmploymentEndDate\",\"ParamVal\": \"" + mlPreviousEmployerDetail.PreviousEmploymentEndDate + "\" },\r\n\t\t{ \"ParamName\":\"@ReasonForLeavingPreviousEmployments\",\"ParamVal\": \"" + mlPreviousEmployerDetail.ReasonForLeavingPreviousEmployments + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlPreviousEmployerDetail.UserId + " },\r\n\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlPreviousEmployerDetail.DocumentTypeId + " },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlPreviousEmployerDetail.TempCPScreenDataID + " },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlPreviousEmployerDetail.CountForFaxId + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }

        public List<MlCEDPreviousEmployerDetail> SelectCEDPreviousEmployerDetailsByAllIds(int DocumentId, int UserId, int TempCPScreenDataID, int CountForFaxId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_CEDPreviousEmployerListAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_CEDPreviousEmployerListAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlCEDPreviousEmployerDetail>>(responses.Content);
                return objResponse;
            }
            var objResponse1 = JsonConvert.DeserializeObject<List<MlCEDPreviousEmployerDetail>>(response.Content);
            return objResponse1;
        }
        public void insertCedPreviousEmployerDetailId(MlCEDPreviousEmployerDetail mlCEDPreviousEmployerDetail)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_InsertCEDPreviousEmployerListAllIds\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlCEDPreviousEmployerDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlCEDPreviousEmployerDetail.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlCEDPreviousEmployerDetail.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlCEDPreviousEmployerDetail.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@EmploymentGapFrom\",\"ParamVal\": \"" + mlCEDPreviousEmployerDetail.EmploymentGapFrom + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@EmploymentGapTo\",\"ParamVal\": \"" + mlCEDPreviousEmployerDetail.EmploymentGapTo + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@ReasonForEmploymentGap\",\"ParamVal\": \"" + mlCEDPreviousEmployerDetail.ReasonForEmploymentGap + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != 0)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_InsertCEDPreviousEmployerListAllIds\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlCEDPreviousEmployerDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlCEDPreviousEmployerDetail.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlCEDPreviousEmployerDetail.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlCEDPreviousEmployerDetail.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@EmploymentGapFrom\",\"ParamVal\": \"" + mlCEDPreviousEmployerDetail.EmploymentGapFrom + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@EmploymentGapTo\",\"ParamVal\": \"" + mlCEDPreviousEmployerDetail.EmploymentGapTo + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@ReasonForEmploymentGap\",\"ParamVal\": \"" + mlCEDPreviousEmployerDetail.ReasonForEmploymentGap + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public void deleteCEDPreviousEmployerDetailId(int PreviousEmployerDetailId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_DeleteCEDPreviousEmployerListAllIds\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmployerDetailId\",\"ParamVal\": " + PreviousEmployerDetailId + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_DeleteCEDPreviousEmployerListAllIds\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmployerDetailId\",\"ParamVal\": " + PreviousEmployerDetailId + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public List<MlCurrentResidenceDetails> SelectCurrentResidenceDetail(int DocumentId, int UserId, int TempCPScreenDataID, int CountForFaxId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_CurrentResidenceDetailAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_CurrentResidenceDetailAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlCurrentResidenceDetails>>(responses.Content);
                return objResponse;
            }
            var objResponse1 = JsonConvert.DeserializeObject<List<MlCurrentResidenceDetails>>(response.Content);
            return objResponse1;
        }
        public void deleteCurrentResidenceDetail(int CurrentResidenceDetailID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_deleteCurrentResidenceDetailById\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CurrentResidenceDetailID\",\"ParamVal\": " + @CurrentResidenceDetailID + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_deleteCurrentResidenceDetailById\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CurrentResidenceDetailID\",\"ParamVal\": " + @CurrentResidenceDetailID + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public void insertCurrentResidenceDetail(MlCurrentResidenceDetails mlCurrentResidenceDetails)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertCurrentResidenceDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlCurrentResidenceDetails.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlCurrentResidenceDetails.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlCurrentResidenceDetails.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlCurrentResidenceDetails.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CurrentResidenceStreetAddress\",\"ParamVal\": \"" + mlCurrentResidenceDetails.CurrentResidenceStreetAddress + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@CurrentResidenceCity\",\"ParamVal\": \"" + mlCurrentResidenceDetails.CurrentResidenceCity + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@CurrentResidenceState\",\"ParamVal\": \"" + mlCurrentResidenceDetails.CurrentResidenceState + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@CurrentResidenceZipcode\",\"ParamVal\": \"" + mlCurrentResidenceDetails.CurrentResidenceZipcode + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@CurrentResidenceDuration\",\"ParamVal\": \"" + mlCurrentResidenceDetails.CurrentResidenceDuration + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != 0)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertCurrentResidenceDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlCurrentResidenceDetails.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlCurrentResidenceDetails.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlCurrentResidenceDetails.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlCurrentResidenceDetails.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CurrentResidenceStreetAddress\",\"ParamVal\": \"" + mlCurrentResidenceDetails.CurrentResidenceStreetAddress + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@CurrentResidenceCity\",\"ParamVal\": \"" + mlCurrentResidenceDetails.CurrentResidenceCity + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@CurrentResidenceState\",\"ParamVal\": \"" + mlCurrentResidenceDetails.CurrentResidenceState + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@CurrentResidenceZipcode\",\"ParamVal\": \"" + mlCurrentResidenceDetails.CurrentResidenceZipcode + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@CurrentResidenceDuration\",\"ParamVal\": \"" + mlCurrentResidenceDetails.CurrentResidenceDuration + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public List<MlPreviousResidenceDetail> SelectMlPreviousResidenceDetail(int DocumentId, int UserId, int TempCPScreenDataID, int CountForFaxId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_PreviousResidenceDetailsAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_PreviousResidenceDetailsAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlPreviousResidenceDetail>>(responses.Content);
                return objResponse;
            }
            var objResponse1 = JsonConvert.DeserializeObject<List<MlPreviousResidenceDetail>>(response.Content);
            return objResponse1;
        }
        public void deletePreviousResidenceDetail(int PreviousResidenceDetail)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_PreviousResidenceDetailAllIds\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousResidenceDetailID\",\"ParamVal\": " + PreviousResidenceDetail + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_PreviousResidenceDetailAllIds\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousResidenceDetailID\",\"ParamVal\": " + PreviousResidenceDetail + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public void insertPreviousResidenceDetail(MlPreviousResidenceDetail mlMlPreviousResidenceDetail)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertPreviousResidenceDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlMlPreviousResidenceDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlMlPreviousResidenceDetail.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlMlPreviousResidenceDetail.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlMlPreviousResidenceDetail.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@PreviousResidenceStreetAddress\",\"ParamVal\": \"" + mlMlPreviousResidenceDetail.PreviousResidenceStreetAddress + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@PreviousResidenceCity\",\"ParamVal\": \"" + mlMlPreviousResidenceDetail.PreviousResidenceCity + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@PreviousResidenceState\",\"ParamVal\": \"" + mlMlPreviousResidenceDetail.PreviousResidenceState + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@PreviousResidenceZipcode\",\"ParamVal\": \"" + mlMlPreviousResidenceDetail.PreviousResidenceZipcode + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@PreviousResidenceDuration\",\"ParamVal\": \"" + mlMlPreviousResidenceDetail.PreviousResidenceDuration + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != 0)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertPreviousResidenceDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlMlPreviousResidenceDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlMlPreviousResidenceDetail.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlMlPreviousResidenceDetail.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlMlPreviousResidenceDetail.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@PreviousResidenceStreetAddress\",\"ParamVal\": \"" + mlMlPreviousResidenceDetail.PreviousResidenceStreetAddress + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@PreviousResidenceCity\",\"ParamVal\": \"" + mlMlPreviousResidenceDetail.PreviousResidenceCity + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@PreviousResidenceState\",\"ParamVal\": \"" + mlMlPreviousResidenceDetail.PreviousResidenceState + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@PreviousResidenceZipcode\",\"ParamVal\": \"" + mlMlPreviousResidenceDetail.PreviousResidenceZipcode + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@PreviousResidenceDuration\",\"ParamVal\": \"" + mlMlPreviousResidenceDetail.PreviousResidenceDuration + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public List<MlTrafficConvictionsDetails> SelectTrafficConvictionsDetails(int DocumentId, int UserId, int TempCPScreenDataID, int CountForFaxId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_PreviousResidenceDetailsAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_TrafficConvictionsDetailsAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlTrafficConvictionsDetails>>(responses.Content);
                return objResponse;
            }
            var objResponse1 = JsonConvert.DeserializeObject<List<MlTrafficConvictionsDetails>>(response.Content);
            return objResponse1;
        }
        public void deleteMlTrafficConvictionsDetails(int mlTrafficConvictionsDetails)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_DeleteTrafficConvictionsDetailID\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@TrafficConvictionsDetailID\",\"ParamVal\": " + mlTrafficConvictionsDetails + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_DeleteTrafficConvictionsDetailID\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@TrafficConvictionsDetailID\",\"ParamVal\": " + mlTrafficConvictionsDetails + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public void insertTrafficConvictionsDetails(MlTrafficConvictionsDetails mlTrafficConvictionsDetails)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertTrafficConvictionsDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlTrafficConvictionsDetails.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlTrafficConvictionsDetails.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlTrafficConvictionsDetails.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlTrafficConvictionsDetails.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@Location\",\"ParamVal\": \"" + mlTrafficConvictionsDetails.Location + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@VechileType\",\"ParamVal\": \"" + mlTrafficConvictionsDetails.VehicleType + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@DateOfConviction\",\"ParamVal\": \"" + mlTrafficConvictionsDetails.DateOfConviction + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Charge\",\"ParamVal\": \"" + mlTrafficConvictionsDetails.Charge + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Penalty\",\"ParamVal\": \"" + mlTrafficConvictionsDetails.Penalty + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != 0)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertTrafficConvictionsDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlTrafficConvictionsDetails.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlTrafficConvictionsDetails.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlTrafficConvictionsDetails.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlTrafficConvictionsDetails.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@Location\",\"ParamVal\": \"" + mlTrafficConvictionsDetails.Location + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@VechileType\",\"ParamVal\": \"" + mlTrafficConvictionsDetails.VehicleType + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@DateOfConviction\",\"ParamVal\": \"" + mlTrafficConvictionsDetails.DateOfConviction + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Charge\",\"ParamVal\": \"" + mlTrafficConvictionsDetails.Charge + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Penalty\",\"ParamVal\": \"" + mlTrafficConvictionsDetails.Penalty + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }



        public List<MlConvictionDetail> SelectConvictionsDetails(int DocumentId, int UserId, int TempCPScreenDataID, int CountForFaxId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_ConvictionsDetails\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_TrafficConvictionsDetailsAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlConvictionDetail>>(responses.Content);
                return objResponse;
            }
            var objResponse1 = JsonConvert.DeserializeObject<List<MlConvictionDetail>>(response.Content);
            return objResponse1;
        }
        public void deleteMlConvictionsDetails(int ConvictionsDetails)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_ConvictionsDetailID\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@ConvictionDetailsId\",\"ParamVal\": " + ConvictionsDetails + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_ConvictionsDetailID\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@ConvictionDetailsId\",\"ParamVal\": " + ConvictionsDetails + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public void insertConvictionsDetails(MlConvictionDetail mlConvictionDetail)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Keep-Alive", "false");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertConvictionsDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlConvictionDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlConvictionDetail.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlConvictionDetail.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlConvictionDetail.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DateOfConviction\",\"ParamVal\": \"" + mlConvictionDetail.DateOfConviction + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Offence\",\"ParamVal\": \"" + mlConvictionDetail.Offense + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Location\",\"ParamVal\": \"" + mlConvictionDetail.Location + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@TypeOfVechileOperated\",\"ParamVal\": \"" + mlConvictionDetail.TypeOfVehicleOperated + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode!=0)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertConvictionsDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlConvictionDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlConvictionDetail.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlConvictionDetail.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlConvictionDetail.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DateOfConviction\",\"ParamVal\": \"" + mlConvictionDetail.DateOfConviction + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Offence\",\"ParamVal\": \"" + mlConvictionDetail.Offense + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Location\",\"ParamVal\": \"" + mlConvictionDetail.Location + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@TypeOfVechileOperated\",\"ParamVal\": \"" + mlConvictionDetail.TypeOfVehicleOperated + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }

        public List<MlDriverLicenceDetail> SelectMlDriverLicenceDetails(int DocumentId, int UserId, int TempCPScreenDataID, int CountForFaxId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_SelectDriverLicenceDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_TrafficConvictionsDetailsAllIds\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlDriverLicenceDetail>>(responses.Content);
                return objResponse;
            }
            var objResponse1 = JsonConvert.DeserializeObject<List<MlDriverLicenceDetail>>(response.Content);
            return objResponse1;
        }
        public void deleteMlDriverLicenceDetail(int DriverLicence)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_deleteDriverLicenceDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DriverLicenceId\",\"ParamVal\": " + DriverLicence + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_deleteDriverLicenceDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DriverLicenceId\",\"ParamVal\": " + DriverLicence + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public void insertDriverLicenceDetail(MlDriverLicenceDetail mlDriverLicenceDetail)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertDriverLicenseDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlDriverLicenceDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlDriverLicenceDetail.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlDriverLicenceDetail.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlDriverLicenceDetail.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@Restriction\",\"ParamVal\": \"" + mlDriverLicenceDetail.Restriction + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Endorsement\",\"ParamVal\": \"" + mlDriverLicenceDetail.Endorsement + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != 0)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertDriverLicenseDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlDriverLicenceDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlDriverLicenceDetail.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlDriverLicenceDetail.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlDriverLicenceDetail.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@Restriction\",\"ParamVal\": \"" + mlDriverLicenceDetail.Restriction + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Endorsement\",\"ParamVal\": \"" + mlDriverLicenceDetail.Endorsement + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }



        public List<MlPreviousEmploymentDetail> SelectPreviousEmploymentDetail(int DocumentId, int UserId, int TempCPScreenDataID, int CountForFaxId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_SelectPreviousEmploymentDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_SelectPreviousEmploymentDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + "},\r\n\t\t\t\t\t  { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + "}]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlPreviousEmploymentDetail>>(responses.Content);
                return objResponse;
            }
            var objResponse1 = JsonConvert.DeserializeObject<List<MlPreviousEmploymentDetail>>(response.Content);
            return objResponse1;
        }
        public void deletePreviousEmploymentDetail(int delPreviousEmploymentDetail)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_deletePreviousEmployment\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmploymentDetailId\",\"ParamVal\": " + delPreviousEmploymentDetail + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_deletePreviousEmployment\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmploymentDetailId\",\"ParamVal\": " + delPreviousEmploymentDetail + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public void insertPreviousEmploymentDetail(MlPreviousEmploymentDetail mlPreviousEmploymentDetail)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertPreviousEmploymentDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlPreviousEmploymentDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlPreviousEmploymentDetail.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlPreviousEmploymentDetail.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlPreviousEmploymentDetail.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@EmployerName\",\"ParamVal\": \"" + mlPreviousEmploymentDetail.EmployerName + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@EmploymentStartDate\",\"ParamVal\": \"" + mlPreviousEmploymentDetail.EmploymentStartDate + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@EmploymentEndDate\",\"ParamVal\": \"" + mlPreviousEmploymentDetail.EmploymentEndDate + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != 0)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertPreviousEmploymentDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlPreviousEmploymentDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlPreviousEmploymentDetail.DocumentTypeId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + mlPreviousEmploymentDetail.TempCPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlPreviousEmploymentDetail.CountForFaxId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@EmployerName\",\"ParamVal\": \"" + mlPreviousEmploymentDetail.EmployerName + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@EmploymentStartDate\",\"ParamVal\": \"" + mlPreviousEmploymentDetail.EmploymentStartDate + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@EmploymentEndDate\",\"ParamVal\": \"" + mlPreviousEmploymentDetail.EmploymentEndDate + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }

        public void insertLoginDetail(MlUserLoginDetail mlUserLoginDetail)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertLoginDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlUserLoginDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DateOfLogin\",\"ParamVal\": \"" + mlUserLoginDetail.DateOfLogin + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@IPAddress\",\"ParamVal\": \"" + mlUserLoginDetail.IPAddress + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Event\",\"ParamVal\": \"" + mlUserLoginDetail.Event + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Attempts\",\"ParamVal\": " + mlUserLoginDetail.Attempts + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertLoginDetail\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlUserLoginDetail.UserId + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@DateOfLogin\",\"ParamVal\": \"" + mlUserLoginDetail.DateOfLogin + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@IPAddress\",\"ParamVal\": \"" + mlUserLoginDetail.IPAddress + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Event\",\"ParamVal\": \"" + mlUserLoginDetail.Event + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Attempts\",\"ParamVal\": " + mlUserLoginDetail.Attempts + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public void updateLoginattempt(int userid)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_updateLoginAttempt\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + userid + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_updateLoginAttempt\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + userid + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }

        public List<MlUserDetail> SelectUserDetailLogin(string emailId, string password)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_SelectUserDetailLogin\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@EmailId\",\"ParamVal\": \"" + emailId + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Password\",\"ParamVal\": \"" + password + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_SelectUserDetailLogin\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@EmailId\",\"ParamVal\": \"" + emailId + "\" },\r\n\t\t\t\t\t{ \"ParamName\":\"@Password\",\"ParamVal\": \"" + password + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlUserDetail>>(responses.Content);
                return objResponse;
            }
            var objResponse1 = JsonConvert.DeserializeObject<List<MlUserDetail>>(response.Content);
            return objResponse1;
        }


        public List<StringDataMamber> sp_dropDownValueTextTableList()
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_dropDownValueTextTableList\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_dropDownValueTextTableList\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<StringDataMamber>>(response.Content);
            return dropDownValueTextTableList;

        }
        public List<MlUserDetails> GetEmailIdandPassword(string Emailid)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetEmailIdandPassword\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@EmailId\",\"ParamVal\":\"" + Emailid + "\" }]\r\n\t \r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetEmailIdandPassword\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@EmailId\",\"ParamVal\":\"" + Emailid + "\" }]\r\n\t \r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
            return objResponse1;
        }
        public List<MlUserDetails> GetAuthenticationEmail(string Emailvalue)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetAuthenticationEmail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Emailvalue\",\"ParamVal\":\"" + Emailvalue + "\" }]\r\n\t \r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetAuthenticationEmail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Emailvalue\",\"ParamVal\":\"" + Emailvalue + "\" }]\r\n\t \r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
            return objResponse1;
        }
        public List<MlUserDetails> GetEmailvalues(string Emailvalue)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_SendEmail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Emailvalue\",\"ParamVal\":\"" + Emailvalue + "\" }]\r\n\t \r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_SendEmail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Emailvalue\",\"ParamVal\":\"" + Emailvalue + "\" }]\r\n\t \r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
            return objResponse1;
        }
        public void ResetPassword(string password, string email)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"SP_ResetPassword\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@password\",\"ParamVal\":\"" + password + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@Emailid\",\"ParamVal\":\"" + email + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"SP_ResetPassword\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@password\",\"ParamVal\":\"" + password + "\" },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@Emailid\",\"ParamVal\":\"" + email + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
                // var objResponse = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
                //  return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            // var objResponse1 = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
            // return objResponse1;
        }
        public List<DynamicControlResponse> BindDynamicControlRepeaterByDocumentTypeId(int DocumentTypeId, int CountFaxId = 0)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DynamicControlsByDocumentTypeId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeId + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DynamicControlsByDocumentTypeId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeId + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DynamicControlResponse>>(response.Content);
            return dropDownValueTextTableList;

        }
        public List<DynamicControlValueNew> PopulateDataFroUpdate_DynamicControlValueList(int UserId, int TempCPScreenDataID, int CountForFaxId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"PopulateDataFroUpdate_DynamicControlValueList\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountForFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"PopulateDataFroUpdate_DynamicControlValueList\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountForFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DynamicControlValueNew>>(response.Content);
            return dropDownValueTextTableList;

        }
        public List<DynamicControl> DynamicControlByDocumentTypeId(int DocumentTypeID, string ControlName)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"DynamicControlByDocumentTypeId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + " },\r\n\t\t{ \"ParamName\":\"@ControlName\",\"ParamVal\":\"" + ControlName + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"DynamicControlByDocumentTypeId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + " },\r\n\t\t{ \"ParamName\":\"@ControlName\",\"ParamVal\":\"" + ControlName + "\" }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DynamicControl>>(response.Content);
            return dropDownValueTextTableList;

        }
        public List<DocumentTypeEntryDetailNew> DocumentTypeEntryDetailByUserIdDocumentTypeId(int UserId, int DocumentTypeId, int TempCPScreenDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"DocumentTypeEntryDetailByUserIdDocumentTypeId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"DocumentTypeEntryDetailByUserIdDocumentTypeId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },\r\n\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DocumentTypeEntryDetailNew>>(response.Content);
            return dropDownValueTextTableList;

        }
        public void LoginDetailsLogOut(int LoginDetailsId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_LoginDetailsLogOut\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@LoginDetailsId\",\"ParamVal\": " + LoginDetailsId + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_LoginDetailsLogOut\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@LoginDetailsId\",\"ParamVal\": " + LoginDetailsId + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }


        }
        public List<CMS_CPScreenData_Temp> CMS_CPScreenData_TempByTempCPScreenDataID(int TempCPScreenDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_CMS_CPScreenData_TempByTempCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_CMS_CPScreenData_TempByTempCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<CMS_CPScreenData_Temp>>(response.Content);
            return dropDownValueTextTableList;




        }
        public List<DynamicControlValueNew> DynamicControlValueByID(int UserId, int DynamicControlID, int TempCPScreenDataID, int CountForFaxId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DynamicControlValueByID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@DynamicControlID\",\"ParamVal\": " + DynamicControlID + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " },{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DynamicControlValueByID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@DynamicControlID\",\"ParamVal\": " + DynamicControlID + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " },{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DynamicControlValueNew>>(response.Content);
            return dropDownValueTextTableList;


        }
        public List<DynamicControlResponse> GetSubmittedTaskByOtherUser(int DocumentTypeId, int CountForFaxId, int UserId, int TempCPScreenDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"usp_GetSubmittedTaskByOtherUser\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentTypeId + " },{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + " },{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"usp_GetSubmittedTaskByOtherUser\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentTypeId + " },{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + " },{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DynamicControlResponse>>(response.Content);
            return dropDownValueTextTableList;


        }
        public List<DynamicControlResponse> GetSubmittedTaskByOtherUserDataEntry(int DocumentTypeId, int CountForFaxId, int UserId, int TempCPScreenDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"usp_GetSubmittedTaskByOtherUserDataEntry\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentTypeId + " },{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + " },{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"usp_GetSubmittedTaskByOtherUserDataEntry\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + DocumentTypeId + " },{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + " },{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DynamicControlResponse>>(response.Content);
            return dropDownValueTextTableList;


        }

        public void uspAutoTaskAssignmentByUserId(int UserId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"uspAutoTaskAssignmentByUserId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + @UserId + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"uspAutoTaskAssignmentByUserId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + @UserId + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }


        }
        public List<UserDetails> GetUserNameByUserId(int UserDetailsID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"usp_GetUserNameByUserId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + UserDetailsID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"usp_GetUserNameByUserId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserDetailsID\",\"ParamVal\": " + UserDetailsID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<UserDetails>>(response.Content);
            return dropDownValueTextTableList;


        }
        public List<TempTaskAssignmentNew> sp_TempTaskAssignmentByUserId(int UserId, string IsProcessed, int ProcessStatus)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentByUserId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@IsProcessed\",\"ParamVal\": " + IsProcessed + " },{ \"ParamName\":\"@ProcessStatus\",\"ParamVal\": " + ProcessStatus + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentByUserId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@IsProcessed\",\"ParamVal\": " + IsProcessed + " },{ \"ParamName\":\"@ProcessStatus\",\"ParamVal\": " + ProcessStatus + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<TempTaskAssignmentNew>>(response.Content);
            return dropDownValueTextTableList;


        }
        public List<DocumentTypeDetails> DocumentTypeEntryDetailByTempCPScreenDataID(int TempCPScreenDataID, int UserId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DocumentTypeEntryDetailByTempCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " },{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DocumentTypeEntryDetailByTempCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " },{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DocumentTypeDetails>>(response.Content);
            return dropDownValueTextTableList;


        }
        public List<TempTaskAssignmentNew> TempTaskAssignmentByIsProcessed(int UserId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);


            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 30000;

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_TempTaskAssignmentByIsProcessed\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\":" + UserId + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_TempTaskAssignmentByIsProcessed\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\":" + UserId + " }]\r\n}", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<TempTaskAssignmentNew>>(response.Content);
            return dropDownValueTextTableList;


        }
        public List<DocumentTypeEntryDetailNew> DocumentTypeEntryDetail(int UserId, int TempCPScreenDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DocumentTypeEntryDetail\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DocumentTypeEntryDetail\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DocumentTypeEntryDetailNew>>(response.Content);
            return dropDownValueTextTableList;


        }
        public List<CMS_CPScreenData_Temp> sp_CMS_CPScreenData_Temp(int TempCPScreenDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_CMS_CPScreenData_Temp\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_CMS_CPScreenData_Temp\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<CMS_CPScreenData_Temp>>(response.Content);
            return dropDownValueTextTableList;


        }
        //public List<CMS_CPScreenData_Temp> sp_CustomerDetailsByIsActive(int TempCPScreenDataID)
        //{
        //    SessionToken();

        //    var client = new RestClient(ClientExecuteQueryUrl);
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("authorization", authorizationHeader);
        //    request.AddHeader("content-type", ContentType);
        //    request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_CustomerDetailsByIsActive\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
        //    {
        //        getToken();
        //        var clients = new RestClient(ClientExecuteQueryUrl);
        //        var requests = new RestRequest(Method.POST);
        //        request.AddHeader("cache-control", "no-cache");
        //        request.AddHeader("authorization", authorizationHeader);
        //        request.AddHeader("content-type", ContentType);
        //        request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_CustomerDetailsByIsActive\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
        //        response = clients.Execute(requests);


        //    }

        //    var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<CMS_CPScreenData_Temp>>(response.Content);
        //    return dropDownValueTextTableList;


        //}
        public List<MlCustomerDetails> CustomerDetailsByIsActive()
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_CustomerDetailsByIsActive\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"sp_CustomerDetailsByIsActive\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);


            }

            var objResponse = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(response.Content);
            return objResponse;


        }
        public List<LocationJoinCustomerDetail> LocationJoinCustomerDetail(int CompanyID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_LocationJoinCustomerDetail\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@CompanyID\",\"ParamVal\": " + CompanyID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_LocationJoinCustomerDetail\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@CompanyID\",\"ParamVal\": " + CompanyID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<LocationJoinCustomerDetail>>(response.Content);
            return dropDownValueTextTableList;


        }
        public List<DocumentTypeEntryDetailNew> sp_TempTaskAssignmentByTempCPScreenDataID(int TempCPScreenDataID, int UserId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentByTempCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " },{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentByTempCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DocumentTypeEntryDetailNew>>(response.Content);
            return dropDownValueTextTableList;


        }
        public List<MlTempTaskAssignment> TempTaskAssignmentBy_TempCPScreenDataID_UserId(int TempCPScreenDataID, int UserId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentBy_TempCPScreenDataID_UserId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " },{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentBy_TempCPScreenDataID_UserId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<MlTempTaskAssignment>>(response.Content);
            return dropDownValueTextTableList;


        }
        public void TempTaskAssignmentUpdateIsProcessed(int ProcessStatus, int IsProcessed, int TempTaskAssignmentID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentUpdateIsProcessed\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@ProcessStatus\",\"ParamVal\":\"" + ProcessStatus + "\" },\r\n\t\t{ \"ParamName\":\"@IsProcessed\",\"ParamVal\":\"" + IsProcessed + "\" },\r\n\t\t{ \"ParamName\":\"@TempTaskAssignmentID\",\"ParamVal\": " + TempTaskAssignmentID + " }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentUpdateIsProcessed\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@ProcessStatus\",\"ParamVal\":\"" + ProcessStatus + "\" },\r\n\t\t{ \"ParamName\":\"@IsProcessed\",\"ParamVal\":\"" + IsProcessed + "\" },\r\n\t\t{ \"ParamName\":\"@TempTaskAssignmentID\",\"ParamVal\": " + TempTaskAssignmentID + " }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public List<MlTempTaskAssignment> FindTempTaskAssignmentByTempCPScreenDataID(int TempCPScreenDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindTempTaskAssignmentByTempCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindTempTaskAssignmentByTempCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);


            }

            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<MlTempTaskAssignment>>(response.Content);
            return dropDownValueTextTableList;


        }
        public void TempTaskAssignmentUpdateProcessStatus(int ProcessStatus, int TempTaskAssignmentID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentUpdateProcessStatus\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@ProcessStatus\",\"ParamVal\":\"" + ProcessStatus + "\" },\r\n\t\t{ \"ParamName\":\"@TempTaskAssignmentID\",\"ParamVal\":\"" + TempTaskAssignmentID + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentUpdateProcessStatus\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@ProcessStatus\",\"ParamVal\":\"" + ProcessStatus + "\" },\r\n\t\t{ \"ParamName\":\"@TempTaskAssignmentID\",\"ParamVal\":\"" + TempTaskAssignmentID + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void uspMoveDataToPermanentTable()
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"uspMoveDataToPermanentTable\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"uspMoveDataToPermanentTable\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }

        public List<TaskOperationNew> sp_FindTaskOperationByTaskOperationId_UserId(int TaskOperationId, int UserId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindTaskOperationByTaskOperationId_UserId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TaskOperationId\",\"ParamVal\":\"" + TaskOperationId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindTaskOperationByTaskOperationId_UserId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TaskOperationId\",\"ParamVal\":\"" + TaskOperationId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<TaskOperationNew>>(response.Content);

            return objResponse1;

        }

        public void TaskOperationUpdateEndTime(DateTime EndTime, int TaskOperationId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TaskOperationUpdateEndTime\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@EndTime\",\"ParamVal\":\"" + EndTime + "\" },\r\n\t\t{ \"ParamName\":\"@TaskOperationId\",\"ParamVal\":\"" + TaskOperationId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TaskOperationUpdateEndTime\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@EndTime\",\"ParamVal\":\"" + EndTime + "\" },\r\n\t\t{ \"ParamName\":\"@TaskOperationId\",\"ParamVal\":\"" + TaskOperationId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<TaskOperationNew>>(response.Content);

            //return objResponse1;

        }

        public void DynamicControlValueInsert(int TempCPScreenDataID, int DynamicControlID, string DynamicControlValueText, int UserId, int CountForFaxId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DynamicControlValueInsert\",\r\n\"OperationType\":\"insert\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@DynamicControlID\",\"ParamVal\":\"" + DynamicControlID + "\" },\r\n\t\t{ \"ParamName\":\"@DynamicControlValueText\",\"ParamVal\":\"" + DynamicControlValueText + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountForFaxId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DynamicControlValueInsert\",\r\n\"OperationType\":\"insert\",\r\n\"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@DynamicControlID\",\"ParamVal\":\"" + DynamicControlID + "\" },\r\n\t\t{ \"ParamName\":\"@DynamicControlValueText\",\"ParamVal\":\"" + DynamicControlValueText + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountForFaxId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<TaskOperationNew>>(response.Content);

            //return objResponse1;

        }



        public void TempTaskAssignmentUpdateNoOfPagesCompleted(int NoOfPagesCompleted, int TempTaskAssignmentID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentUpdateNoOfPagesCompleted\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@NoOfPagesCompleted\",\"ParamVal\":\"" + NoOfPagesCompleted + "\" },\r\n\t\t{ \"ParamName\":\"@TempTaskAssignmentID\",\"ParamVal\":\"" + TempTaskAssignmentID + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_TempTaskAssignmentUpdateNoOfPagesCompleted\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@NoOfPagesCompleted\",\"ParamVal\":\"" + NoOfPagesCompleted + "\" },\r\n\t\t{ \"ParamName\":\"@TempTaskAssignmentID\",\"ParamVal\":\"" + TempTaskAssignmentID + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }



        public void DocumentTypeEntryDetailsInsert(int CountForFaxId, int DocumentTypeId, int UserId, int TempCPScreenDataID, int StartPage, int EndPage)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DocumentTypeEntryDetailsInsert\",\r\n\"OperationType\":\"insert\",\r\n\"ParameterList\": [{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountForFaxId + "\" },\r\n\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@StartPage\",\"ParamVal\":\"" + StartPage + "\" },\r\n\t\t{ \"ParamName\":\"@EndPage\",\"ParamVal\":\"" + EndPage + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DocumentTypeEntryDetailsInsert\",\r\n\"OperationType\":\"insert\",\r\n\"ParameterList\": [{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountForFaxId + "\" },\r\n\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@StartPage\",\"ParamVal\":\"" + StartPage + "\" },\r\n\t\t{ \"ParamName\":\"@EndPage\",\"ParamVal\":\"" + EndPage + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<TaskOperationNew>>(response.Content);

            //return objResponse1;

        }




        public List<DynamicControlValueNew> DynamicControlValueByDyanamicControlValueIDSelect(int DyanamicControlValueID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DynamicControlValueByDyanamicControlValueIDSelect\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DyanamicControlValueID\",\"ParamVal\": " + DyanamicControlValueID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DynamicControlValueByDyanamicControlValueIDSelect\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DyanamicControlValueID\",\"ParamVal\": " + DyanamicControlValueID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<DynamicControlValueNew>>(response.Content);
            return dropDownValueTextTableList;

        }



        public void DynamicControlValueUpdateDynamicControlValueText(string DynamicControlValueText, int DyanamicControlValueID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DynamicControlValueUpdateDynamicControlValueText\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DynamicControlValueText\",\"ParamVal\":\"" + DynamicControlValueText + "\" },\r\n\t\t{ \"ParamName\":\"@DyanamicControlValueID\",\"ParamVal\":\"" + DyanamicControlValueID + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_DynamicControlValueUpdateDynamicControlValueText\",\r\n\"OperationType\":\"update\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DynamicControlValueText\",\"ParamVal\":\"" + DynamicControlValueText + "\" },\r\n\t\t{ \"ParamName\":\"@DyanamicControlValueID\",\"ParamVal\":\"" + DyanamicControlValueID + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }

        public List<uspGetAllEnteredPageNo_ResultNew> uspGetAllEnteredPageNo(int UserId, int TempCPScreenDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"uspGetAllEnteredPageNo\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"uspGetAllEnteredPageNo\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + TempCPScreenDataID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<uspGetAllEnteredPageNo_ResultNew>>(response.Content);
            return dropDownValueTextTableList;

        }

        public List<MIDocumentType> usp_DocumentTypesListByDocumentTypeId(Int32 DocumentTypeId)
        {
            SessionToken();
            //var client = new RestClient(ClientExecuteQueryUrl);
            //var request = new RestRequest(Method.POST);
            ////request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", authorizationHeader);
            //request.AddHeader("content-type", ContentType);
            //request.AddParameter("application/json", "{\r\n\"OperationName\":\"uspGetAllCustomers\",\r\n\"OperationType\":\"select\"\r\n}\r\n", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            ////List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlCustomerDetails>>(response.Content);
            //return objResponse1;

            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"usp_DocumentTypesListByDocumentTypeId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\":\"" + DocumentTypeId + "\" }\r\n\t\t]\r\n\t\r\n\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"usp_DocumentTypesListByDocumentTypeId\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\":\"" + DocumentTypeId + "\" }\r\n\t\t]\r\n\t\r\n\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

            return documentTypeList;
        }


        public void insertTaskOperation(MlTaskOperation mlTaskOperation)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertTaskOperation\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Faxid\",\"ParamVal\": " + mlTaskOperation.FaxId + " },{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlTaskOperation.UserId + " },{ \"ParamName\":\"@StartTime\",\"ParamVal\": \"" + mlTaskOperation.StartTime + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_insertTaskOperation\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Faxid\",\"ParamVal\": " + mlTaskOperation.FaxId + " },{ \"ParamName\":\"@UserId\",\"ParamVal\": " + mlTaskOperation.UserId + " },{ \"ParamName\":\"@StartTime\",\"ParamVal\": \"" + mlTaskOperation.StartTime + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }

        }

        public List<MlUserDetail> SelectUserDetailAttemts(string emailId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_UpdateLoginAttem\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Email\",\"ParamVal\": \"" + emailId + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_UpdateLoginAttem\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Email\",\"ParamVal\": \"" + emailId + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlUserDetail>>(responses.Content);
                return objResponse;
            }
            var objResponse1 = JsonConvert.DeserializeObject<List<MlUserDetail>>(response.Content);
            return objResponse1;
        }
        public void UpdateLockAttempts(int attempts, string email)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_UpdeteCountBy1\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@LoginAttempt\",\"ParamVal\":" + attempts + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@Email\",\"ParamVal\":\"" + email + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_UpdeteCountBy1\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@LoginAttempt\",\"ParamVal\":" + attempts + " },\r\n\t\t\t\t\t\t{ \"ParamName\":\"@Email\",\"ParamVal\":\"" + email + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
                // var objResponse = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
                //  return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            // var objResponse1 = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
            // return objResponse1;
        }
        public void LocktheAccount(string email)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_LockTheAccount\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Email\",\"ParamVal\":\"" + email + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_LockTheAccount\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Email\",\"ParamVal\":\"" + email + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
                // var objResponse = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
                //  return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            // var objResponse1 = JsonConvert.DeserializeObject<List<MlUserDetails>>(response.Content);
            // return objResponse1;
        }
        #region Love Pal New

        public List<MlDynamicControl> ddlDynamicControlLabelName(int docid)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ddlDynamicControlLabelName\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocId\",\"ParamVal\": " + docid + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ddlDynamicControlLabelName\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocId\",\"ParamVal\": " + docid + " }]\r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlDynamicControl>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlDynamicControl>>(response.Content);
            return objResponse1;
        }
        public void sp_InsertAuditRuleClassificationData(int AuditQuestionid, int DynamicControlId, string RuleExpression, string RuleType, string DynamicControlLabel)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_InsertAuditRuleClassificationData\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@AuditQuestionid\",\"ParamVal\":" + AuditQuestionid + " },\r\n\t{ \"ParamName\":\"@DynamicControlId\",\"ParamVal\":" + DynamicControlId + " },\r\n\t{ \"ParamName\":\"@RuleExpression\",\"ParamVal\":\"" + RuleExpression + "\" },\r\n\t{ \"ParamName\":\"@RuleType\",\"ParamVal\":\"" + RuleType + "\" },\r\n\t{ \"ParamName\":\"@DynamicControlLabel\",\"ParamVal\":\"" + DynamicControlLabel + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_InsertAuditRuleClassificationData\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@AuditQuestionid\",\"ParamVal\":" + AuditQuestionid + " },\r\n\t{ \"ParamName\":\"@DynamicControlId\",\"ParamVal\":" + DynamicControlId + " },\r\n\t{ \"ParamName\":\"@RuleExpression\",\"ParamVal\":\"" + RuleExpression + "\" },\r\n\t{ \"ParamName\":\"@RuleType\",\"ParamVal\":\"" + RuleType + "\" },\r\n\t{ \"ParamName\":\"@DynamicControlLabel\",\"ParamVal\":\"" + DynamicControlLabel + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public List<MlAuditRule> Sp_GetAuditRulesClassificationByDocId(int DocumentID, int QuestionID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_GetAuditRulesClassificationByDocId\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentID\",\"ParamVal\":" + DocumentID + " },\r\n\t{ \"ParamName\":\"@QuestionID\",\"ParamVal\":" + QuestionID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_GetAuditRulesClassificationByDocId\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentID\",\"ParamVal\":" + DocumentID + " },\r\n\t{ \"ParamName\":\"@QuestionID\",\"ParamVal\":" + QuestionID + " }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MlAuditRule>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlAuditRule>>(response.Content);
            return objResponse1;
        }
        public void SP_DeleteAuditRuleClassification(int Auditid)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"SP_DeleteAuditRuleClassification\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Auditid\",\"ParamVal\":" + Auditid + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"SP_DeleteAuditRuleClassification\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Auditid\",\"ParamVal\":" + Auditid + " }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public List<MIDocumentType> Sp_CheckIsTempalteExist(string TemplateName)
        {
            SessionToken();
            
            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_CheckIsTempalteExist\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeAlias\",\"ParamVal\":\""+ TemplateName + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_CheckIsTempalteExist\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeAlias\",\"ParamVal\":\"" + TemplateName + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

            return documentTypeList;
        }

        public void Sp_DeleteAuditQuestion(int AuditQuestionsID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_DeleteAuditQuestion\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@AuditQuestionsID\",\"ParamVal\":" + AuditQuestionsID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_DeleteAuditQuestion\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@AuditQuestionsID\",\"ParamVal\":" + AuditQuestionsID + " }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public List<AuditRuleClassification> Sp_GetRuleType()
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_GetRuleType\",\r\n\t\"OperationType\":\"select\"\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                //ServicePointManager.Expect100Continue = false;
                //ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_GetRuleType\",\r\n\t\"OperationType\":\"select\"\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<AuditRuleClassification>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<AuditRuleClassification>>(response.Content);
            return objResponse1;
        }
        public void Sp_UpdateAuditQuestion(string AuditQuestion, int CustomerID, int DocumentID, int AuditQuestionID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_UpdateAuditQuestion\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@AuditQuestion\",\"ParamVal\":\"" + AuditQuestion + "\" },\r\n\t{ \"ParamName\":\"@CustomerID\",\"ParamVal\":" + CustomerID + " },\r\n\t{ \"ParamName\":\"@DocumentID\",\"ParamVal\":" + DocumentID + " },\r\n\t{ \"ParamName\":\"@AuditQuestionID\",\"ParamVal\":" + AuditQuestionID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_UpdateAuditQuestion\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@AuditQuestion\",\"ParamVal\":\"" + AuditQuestion + "\" },\r\n\t{ \"ParamName\":\"@CustomerID\",\"ParamVal\":" + CustomerID + " },\r\n\t{ \"ParamName\":\"@DocumentID\",\"ParamVal\":" + DocumentID + " },\r\n\t{ \"ParamName\":\"@AuditQuestionID\",\"ParamVal\":" + AuditQuestionID + " }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public void Sp_UpdateAuditRuleClassification(string RuleExpression, string RuleType, string DynamicControlLabel, int AuditRuleClassification)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_UpdateAuditRuleClassification\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@RuleExpression\",\"ParamVal\":\"" + RuleExpression + "\" },\r\n\t{ \"ParamName\":\"@RuleType\",\"ParamVal\":\"" + RuleType + "\" },\r\n\t{ \"ParamName\":\"@DynamicControlLabel\",\"ParamVal\":\"" + DynamicControlLabel + "\" },\r\n\t{ \"ParamName\":\"@AuditRuleClassification\",\"ParamVal\":" + AuditRuleClassification + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_UpdateAuditRuleClassification\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@RuleExpression\",\"ParamVal\":\"" + RuleExpression + "\" },\r\n\t{ \"ParamName\":\"@RuleType\",\"ParamVal\":\"" + RuleType + "\" },\r\n\t{ \"ParamName\":\"@DynamicControlLabel\",\"ParamVal\":\"" + DynamicControlLabel + "\" },\r\n\t{ \"ParamName\":\"@AuditRuleClassification\",\"ParamVal\":" + AuditRuleClassification + " }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public List<MlAuditQuestion> Sp_GetAuditQuestion(int CustomerId, int DocumentID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_GetAuditQuestion\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\":" + CustomerId + " },\r\n\t{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\":" + DocumentID + " }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_GetAuditQuestion\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\":" + CustomerId + " },\r\n\t{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\":" + DocumentID + " }]\r\n }", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var dropDownValueTextTableList = JsonConvert.DeserializeObject<List<MlAuditQuestion>>(response.Content);
            return dropDownValueTextTableList;

        }
        public void Sp_DeleteLocationByCustID(int CustomerID)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_DeleteLocationByCustID\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\":"+CustomerID+" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"Sp_DeleteLocationByCustID\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\":" + CustomerID + " }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }

        #endregion


        #region LovePal

        public void spinsertNewDocumentType(string documenttype_name, string document_description, int customerID, string labelName, string ControlName, string ControlType, string DropDownValue, int OrderBy, bool IsActive, int UserId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");

            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_insertNewDocumentType\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@documenttype_name\",\"ParamVal\":\"" + documenttype_name + "\" },\r\n\t\t{ \"ParamName\":\"@document_description\",\"ParamVal\":\"" + document_description + "\" },\r\n\t\t{ \"ParamName\":\"@customerID\",\"ParamVal\": " + customerID + " },\r\n\t\t{ \"ParamName\":\"@labelName\",\"ParamVal\":\"" + labelName + "\" },\r\n\t\t{ \"ParamName\":\"@ControlName\",\"ParamVal\":\"" + ControlName + "\" },\r\n\t\t{ \"ParamName\":\"@ControlType\",\"ParamVal\":\"" + ControlType + "\" },\r\n\t\t{ \"ParamName\":\"@DropDownValue\",\"ParamVal\":\"" + DropDownValue + "\" },\r\n\t\t{ \"ParamName\":\"@OrderBy\",\"ParamVal\": " + OrderBy + " },\r\n\t\t{ \"ParamName\":\"@IsActive\",\"ParamVal\":\"" + IsActive + "\" },\r\n\t\t{ \"ParamName\":\"@userID\",\"ParamVal\":" + UserId + "}]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_insertNewDocumentType\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@documenttype_name\",\"ParamVal\":\"" + documenttype_name + "\" },\r\n\t\t{ \"ParamName\":\"@document_description\",\"ParamVal\":\"" + document_description + "\" },\r\n\t\t{ \"ParamName\":\"@customerID\",\"ParamVal\": " + customerID + " },\r\n\t\t{ \"ParamName\":\"@labelName\",\"ParamVal\":\"" + labelName + "\" },\r\n\t\t{ \"ParamName\":\"@ControlName\",\"ParamVal\":\"" + ControlName + "\" },\r\n\t\t{ \"ParamName\":\"@ControlType\",\"ParamVal\":\"" + ControlType + "\" },\r\n\t\t{ \"ParamName\":\"@DropDownValue\",\"ParamVal\":\"" + DropDownValue + "\" },\r\n\t\t{ \"ParamName\":\"@OrderBy\",\"ParamVal\": " + OrderBy + " },\r\n\t\t{ \"ParamName\":\"@IsActive\",\"ParamVal\":\"" + IsActive + "\" },\r\n\t\t{ \"ParamName\":\"@userID\",\"ParamVal\":" + UserId + "}]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void SpInsertAuditRuleClassification(int CustomerId, int DocumentId, string AuditQuestion, int DynamicControlId, string RuleExpression, string RuleType, string DynamicControlLabel, int UserId)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_insertAuditRuleClassification\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\":" + CustomerId + " },\r\n\t{ \"ParamName\":\"@DocumentID\",\"ParamVal\":" + DocumentId + " },\r\n\t{ \"ParamName\":\"@AuditQuestion\",\"ParamVal\":\"" + AuditQuestion + "\" },\r\n\t{ \"ParamName\":\"@DynamicControlId\",\"ParamVal\":" + DynamicControlId + " },\r\n\t{ \"ParamName\":\"@RuleExpression\",\"ParamVal\":\"" + RuleExpression + "\" },\r\n\t{ \"ParamName\":\"@RuleType\",\"ParamVal\":\"" + RuleType + "\" },\r\n\t{ \"ParamName\":\"@DynamicControlLabel\",\"ParamVal\":\"" + DynamicControlLabel + "\" },\r\n\t{ \"ParamName\":\"@UserID\",\"ParamVal\":" + UserId + "}]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_insertAuditRuleClassification\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\":" + CustomerId + " },\r\n\t{ \"ParamName\":\"@DocumentID\",\"ParamVal\":" + DocumentId + " },\r\n\t{ \"ParamName\":\"@AuditQuestion\",\"ParamVal\":\"" + AuditQuestion + "\" },\r\n\t{ \"ParamName\":\"@DynamicControlId\",\"ParamVal\":" + DynamicControlId + " },\r\n\t{ \"ParamName\":\"@RuleExpression\",\"ParamVal\":\"" + RuleExpression + "\" },\r\n\t{ \"ParamName\":\"@RuleType\",\"ParamVal\":\"" + RuleType + "\" },\r\n\t{ \"ParamName\":\"@DynamicControlLabel\",\"ParamVal\":\"" + DynamicControlLabel + "\" },\r\n\t{ \"ParamName\":\"@UserID\",\"ParamVal\":" + UserId + "}]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }
        public void usp_CopyCompany2(int PreviousCustomerId, string CustomerName, int NewCustomerID, int DocumentTypeID, string DocumentName, string SourceType, string DocumentCode)
        {

            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"usp_CopyCompany2\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@previous_customer\",\"ParamVal\":" + PreviousCustomerId + " },\r\n\t\t{ \"ParamName\":\"@new_customer\",\"ParamVal\":\"" + CustomerName + "\" },\r\n\t\t{ \"ParamName\":\"@newcustomer_id\",\"ParamVal\": " + NewCustomerID + " },\r\n\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":" + DocumentTypeID + " },\r\n\t\t{ \"ParamName\":\"@new_DocumentName\",\"ParamVal\":\"" + DocumentName + "\" },\r\n\t\t{ \"ParamName\":\"@sourceType\",\"ParamVal\":\"" + SourceType + "\" },\r\n\t\t{ \"ParamName\":\"@DucumentCode\",\"ParamVal\": \"" + DocumentCode + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"usp_CopyCompany2\",\r\n\t\"OperationType\":\"insert\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@previous_customer\",\"ParamVal\":" + PreviousCustomerId + " },\r\n\t\t{ \"ParamName\":\"@new_customer\",\"ParamVal\":\"" + CustomerName + "\" },\r\n\t\t{ \"ParamName\":\"@newcustomer_id\",\"ParamVal\": " + NewCustomerID + " },\r\n\t\t{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":" + DocumentTypeID + " },\r\n\t\t{ \"ParamName\":\"@new_DocumentName\",\"ParamVal\":\"" + DocumentName + "\" },\r\n\t\t{ \"ParamName\":\"@sourceType\",\"ParamVal\":\"" + SourceType + "\" },\r\n\t\t{ \"ParamName\":\"@DucumentCode\",\"ParamVal\": \"" + DocumentCode + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }
        }
        public List<MIAuditQuestionFunction> sp_SelectAuditFunction(string Type)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_SelectAuditFunction\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Type\",\"ParamVal\":\"" + Type + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                //ServicePointManager.Expect100Continue = false;
                //ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"sp_SelectAuditFunction\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@Type\",\"ParamVal\":\"" + Type + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<MIAuditQuestionFunction>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MIAuditQuestionFunction>>(response.Content);
            return objResponse1;
        }


        #endregion

        #region ArqamQc
        public List<MlException_FormData> qcExceptionFormDataById(int FormDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_QcSelectException_FormDataById\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@FormDataID\",\"ParamVal\": " + FormDataID + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_QcSelectException_FormDataById\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@FormDataID\",\"ParamVal\": " + FormDataID + " }]\r\n}", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var qcExceptionFormDataById = JsonConvert.DeserializeObject<List<MlException_FormData>>(response.Content);
            return qcExceptionFormDataById;
        }

        public void qcUpdateExceptionFormData(int FormDataID, string dyanamicCOntrolValueText)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_QcUpdateException_FormDynamicControlValueText\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@FormDataID\",\"ParamVal\": " + FormDataID + " },{ \"ParamName\":\"@DynamicControlValueText\",\"ParamVal\": \"" + dyanamicCOntrolValueText + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_QcUpdateException_FormDynamicControlValueText\",\r\n\t\"OperationType\":\"update\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@FormDataID\",\"ParamVal\": " + FormDataID + " },{ \"ParamName\":\"@DynamicControlValueText\",\"ParamVal\": \"" + dyanamicCOntrolValueText + "\" }]\r\n}", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var qcExceptionFormDataById = JsonConvert.DeserializeObject<List<MlException_FormData>>(response.Content);
            //return qcExceptionFormDataById;
        }

        public List<MlException_FormData> qcDynamicControlValueList(int UserId, int CPScreenDataID, int CountForFaxId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", ContentType);
            request.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_QcDynamicControlValueList\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@CPScreenDataID\",\"ParamVal\": " + CPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", ContentType);
                requests.AddParameter("application/json", "{\r\n\t\"OperationName\":\"ssp_QcDynamicControlValueList\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\": " + UserId + " },{ \"ParamName\":\"@CPScreenDataID\",\"ParamVal\": " + CPScreenDataID + " },\r\n\t\t\t\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + CountForFaxId + " }]\r\n}", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var qcDynamicControlValueList = JsonConvert.DeserializeObject<List<MlException_FormData>>(response.Content);
            return qcDynamicControlValueList;
        }
        #endregion

        #region QC API



        public List<MlException_FormData> FindException_FormDataBYUserIdCPScreenDataID(int UserId, int CPScreenDataID, int CountForFaxId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindException_FormDataBYUserIdCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@CPScreenDataID\",\"ParamVal\":\"" + CPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountForFaxId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindException_FormDataBYUserIdCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@CPScreenDataID\",\"ParamVal\":\"" + CPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountForFaxId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlException_FormData>>(response.Content);

            return objResponse1;

        }




        public void uspMoveDataFromExceptionToCPScreenTable(int FaxId, int UserId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"uspMoveDataFromExceptionToCPScreenTable\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@FaxId\",\"ParamVal\":\"" + FaxId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"uspMoveDataFromExceptionToCPScreenTable\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@FaxId\",\"ParamVal\":\"" + FaxId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            //var objResponse1 = JsonConvert.DeserializeObject<List<MlLogin>>(response.Content);

        }



        public List<MlException_FormData> FindException_FormDataBYUserIdDynamicControlID(int UserId, int DynamicControlID, int CPScreenDataID, int CountForFaxId)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindException_FormDataBYUserIdDynamicControlID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@DynamicControlID\",\"ParamVal\":\"" + DynamicControlID + "\" },\r\n\t\t{ \"ParamName\":\"@CPScreenDataID\",\"ParamVal\":\"" + CPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountForFaxId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindException_FormDataBYUserIdDynamicControlID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@DynamicControlID\",\"ParamVal\":\"" + DynamicControlID + "\" },\r\n\t\t{ \"ParamName\":\"@CPScreenDataID\",\"ParamVal\":\"" + CPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountForFaxId + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlException_FormData>>(response.Content);

            return objResponse1;

        }

        public List<MlException_FormData> sp_FindException_FormDataBYCPScreenDataID(int CPScreenDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindException_FormDataBYCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@CPScreenDataID\",\"ParamVal\":\"" + CPScreenDataID + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindException_FormDataBYCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@CPScreenDataID\",\"ParamVal\":\"" + CPScreenDataID + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlException_FormData>>(response.Content);

            return objResponse1;

        }

        public List<MLException_CPScreenData> sp_FindException_CPScreenDataByCPScreenDataID(int CPScreenDataID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindException_CPScreenDataByCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@CPScreenDataID\",\"ParamVal\":\"" + CPScreenDataID + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\r\n\"OperationName\":\"sp_FindException_CPScreenDataByCPScreenDataID\",\r\n\"OperationType\":\"select\",\r\n\"ParameterList\": [{ \"ParamName\":\"@CPScreenDataID\",\"ParamVal\":\"" + CPScreenDataID + "\" }]\r\n\r\n }\r\n", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MLException_CPScreenData>>(response.Content);

            return objResponse1;

        }

        #endregion

        #region extraSpByArqam
        public List<MlDynamicControl> dynamicControlList(int DocumentTypeID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_BindingdynamicControlList\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_BindingdynamicControlList\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + " }]\r\n}", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlDynamicControl>>(response.Content);

            return objResponse1;

        }
        public void deleteDynamicControlValue(int DynamicControlId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_deleteDynamicControl\",\r\n  \"OperationType\":\"update\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@DynamicControlID\",\"ParamVal\": " + DynamicControlId + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_deleteDynamicControl\",\r\n  \"OperationType\":\"update\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@DynamicControlID\",\"ParamVal\": " + DynamicControlId + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public List<MlDynamicControl> CountForRadioButtonDynamicControl(int DocumentTypeID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_CountForRbInDynamicControl\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_CountForRbInDynamicControl\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + " }]\r\n}", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlDynamicControl>>(response.Content);

            return objResponse1;

        }
        public List<MlDynamicControl> sspcountforsequenceDynamicControl(int DocumentTypeID)
        {
            SessionToken();

            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_countforsequenceDynamicControl\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_countforsequenceDynamicControl\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + DocumentTypeID + " }]\r\n}", ParameterType.RequestBody);
                response = clients.Execute(requests);
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<MlDynamicControl>>(response.Content);

            return objResponse1;

        }
        public void insertIntoDynamicControl(MlDynamicControl mlDynamicControl)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_insertIntoDynamicControl\",\r\n  \"OperationType\":\"insert\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + mlDynamicControl.DocumentTypeID + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@labelName\",\"ParamVal\": \"" + mlDynamicControl.labelName + "\" },\r\n\t\t\t\t\t  { \"ParamName\":\"@ControlName\",\"ParamVal\": \"" + mlDynamicControl.ControlName + "\" },\r\n\t\t\t\t\t  { \"ParamName\":\"@ControlType\",\"ParamVal\": \"" + mlDynamicControl.ControlType + "\" },\r\n\t\t\t\t\t  { \"ParamName\":\"@DropDownValue\",\"ParamVal\": \"" + mlDynamicControl.DropDownValue + "\" },\r\n\t\t\t\t\t  { \"ParamName\":\"@OrderBy\",\"ParamVal\": " + mlDynamicControl.OrderBy + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@IsActive\",\"ParamVal\": \"" + mlDynamicControl.IsActive + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_insertIntoDynamicControl\",\r\n  \"OperationType\":\"insert\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@DocumentTypeID\",\"ParamVal\": " + mlDynamicControl.DocumentTypeID + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@labelName\",\"ParamVal\": \"" + mlDynamicControl.labelName + "\" },\r\n\t\t\t\t\t  { \"ParamName\":\"@ControlName\",\"ParamVal\": \"" + mlDynamicControl.ControlName + "\" },\r\n\t\t\t\t\t  { \"ParamName\":\"@ControlType\",\"ParamVal\": \"" + mlDynamicControl.ControlType + "\" },\r\n\t\t\t\t\t  { \"ParamName\":\"@DropDownValue\",\"ParamVal\": \"" + mlDynamicControl.DropDownValue + "\" },\r\n\t\t\t\t\t  { \"ParamName\":\"@OrderBy\",\"ParamVal\": " + mlDynamicControl.OrderBy + " },\r\n\t\t\t\t\t  { \"ParamName\":\"@IsActive\",\"ParamVal\": \"" + mlDynamicControl.IsActive + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
            }

        }

        public void UpdateOrderno(int id, int orderno)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_UpdateOrderno\",\r\n  \"OperationType\":\"update\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@OrderBy\",\"ParamVal\": " + orderno + " },\r\n  { \"ParamName\":\"@DynamicControlID\",\"ParamVal\": " + id + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_UpdateOrderno\",\r\n  \"OperationType\":\"update\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@OrderBy\",\"ParamVal\": " + orderno + " },\r\n  { \"ParamName\":\"@DynamicControlID\",\"ParamVal\": " + id + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(request);
            }

        }

        public void updateDynamicControl(MlDynamicControl mlDynamicControl)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_updateDynamicControl\",\r\n  \"OperationType\":\"update\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@labelName\",\"ParamVal\": \"" + mlDynamicControl.labelName + "\" },\r\n  { \"ParamName\":\"@DropDownValue\",\"ParamVal\": \"" + mlDynamicControl.DropDownValues + "\" },\r\n  { \"ParamName\":\"@DynamicControlID\",\"ParamVal\": " + mlDynamicControl.DynamicControlID + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_updateDynamicControl\",\r\n  \"OperationType\":\"update\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@labelName\",\"ParamVal\": \"" + mlDynamicControl.labelName + "\" },\r\n  { \"ParamName\":\"@DropDownValue\",\"ParamVal\": \"" + mlDynamicControl.DropDownValues + "\" },\r\n  { \"ParamName\":\"@DynamicControlID\",\"ParamVal\": " + mlDynamicControl.DynamicControlID + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }

        #endregion

        #region Suresh Sir code

        public List<MIDocumentType> SPGetDocumentTypeMotorVehicleReport(int CustomerId, string tableName)
        {
            SessionToken();


            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"SP_GetDocumentTypeMotorVehicleReport\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@CustomerId\",\"ParamVal\": " + CustomerId + " },\r\n\t\t\t\t { \"ParamName\":\"@TableName\",\"ParamVal\": \"" + tableName + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"SP_GetDocumentTypeMotorVehicleReport\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@CustomerId\",\"ParamVal\": " + CustomerId + " },\r\n\t\t\t\t { \"ParamName\":\"@TableName\",\"ParamVal\": \"" + tableName + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

            return documentTypeList;
        }

        public List<MIDocumentType> GetCertificateOfViolationDocID(int CustomerId, string tableName)
        {
            SessionToken();


            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"[dbo].[SP_GetDocumentTypeID]\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + CustomerId + " },\r\n\t\t\t\t { \"ParamName\":\"@TableName\",\"ParamVal\": \"" + tableName + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"[dbo].[SP_GetDocumentTypeID]\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@CustomerID\",\"ParamVal\": " + CustomerId + " },\r\n\t\t\t\t { \"ParamName\":\"@TableName\",\"ParamVal\": \"" + tableName + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

            return documentTypeList;
        }
        public List<MIDocumentType> GetDriversLicenseDocID(int CustomerId, string tablename)
        {
            SessionToken();


            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"SP_GetDriversLicenseDocID\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@CustomerId\",\"ParamVal\": " + CustomerId + " },\r\n\t\t\t\t { \"ParamName\":\"@TableName\",\"ParamVal\": \"" + tablename + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"SP_GetDriversLicenseDocID\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@CustomerId\",\"ParamVal\": " + CustomerId + " },\r\n\t\t\t\t { \"ParamName\":\"@TableName\",\"ParamVal\": \"" + tablename + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

            return documentTypeList;
        }
        public List<MIDocumentType> GetPreviousEmploymentDocID(int CustomerId, string tablename)
        {
            SessionToken();


            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"SP_GetPreviousEmploymentDocID\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@CustomerId\",\"ParamVal\": " + CustomerId + " },\r\n\t\t\t\t { \"ParamName\":\"@TableName\",\"ParamVal\": \"" + tablename + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"SP_GetPreviousEmploymentDocID\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@CustomerId\",\"ParamVal\": " + CustomerId + " },\r\n\t\t\t\t { \"ParamName\":\"@TableName\",\"ParamVal\": \"" + tablename + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

            return documentTypeList;
        }
        public List<MIDocumentType> GetDriverApplicationDocID(int CustomerId, string tablename)
        {
            SessionToken();


            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"SP_GetDriverApplicationDocID\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@CustomerId\",\"ParamVal\": " + CustomerId + " },\r\n\t\t\t\t { \"ParamName\":\"@TableName\",\"ParamVal\": \"" + tablename + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"SP_GetDriverApplicationDocID\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@CustomerId\",\"ParamVal\": " + CustomerId + " },\r\n\t\t\t\t { \"ParamName\":\"@TableName\",\"ParamVal\": \"" + tablename + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MIDocumentType>>(responses.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MIDocumentType>>(response.Content);

            return documentTypeList;
        }
        #endregion
        #region DocumentRelationTemp

        public void insertDocumentRelationTemp(MlDocumentRelationTemp mlDocumentRelationTemp)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"insertDocumentRelationTemp\",\r\n  \"OperationType\":\"insert\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@FaxId\",\"ParamVal\": " + mlDocumentRelationTemp.FaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@UserId\",\"ParamVal\": " + mlDocumentRelationTemp.UserId + " },\r\n\t\t\t\t { \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlDocumentRelationTemp.DocumentTypeId + " },\r\n\t\t\t\t { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlDocumentRelationTemp.CountForFaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@IsReview\",\"ParamVal\": \"" + mlDocumentRelationTemp.IsReview + "\" },]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"insertDocumentRelationTemp\",\r\n  \"OperationType\":\"insert\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@FaxId\",\"ParamVal\": " + mlDocumentRelationTemp.FaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@UserId\",\"ParamVal\": " + mlDocumentRelationTemp.UserId + " },\r\n\t\t\t\t { \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlDocumentRelationTemp.DocumentTypeId + " },\r\n\t\t\t\t { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlDocumentRelationTemp.CountForFaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@IsReview\",\"ParamVal\": \"" + mlDocumentRelationTemp.IsReview + "\" },]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        public List<MlDocumentRelationTemp> getFaxIdForDocumentRelationTemp(MlDocumentRelationTemp MlDocumentRelationTemp)
        {
            SessionToken();


            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_getFaxIdForDocumentRelationTemp\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@FaxId\",\"ParamVal\": " + MlDocumentRelationTemp.FaxId + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_getFaxIdForDocumentRelationTemp\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@FaxId\",\"ParamVal\": " + MlDocumentRelationTemp.FaxId + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MlDocumentRelationTemp>>(responses.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MlDocumentRelationTemp>>(response.Content);

            return documentTypeList;
        }
        public List<MlCPScreenData> ssp_getFaxIdFromCpScreenTemp(int MlCPScreenData)
        {
            SessionToken();


            var client = new RestClient(ClientExecuteQueryUrl);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_getFaxIdFromCpScreenTemp\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + MlCPScreenData + " }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();

                var clients = new RestClient(ClientExecuteQueryUrl);
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 0;
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_getFaxIdFromCpScreenTemp\",\r\n  \"OperationType\":\"select\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\": " + MlCPScreenData + " }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
                var documentTypeLists = JsonConvert.DeserializeObject<List<MlCPScreenData>>(responses.Content);

                return documentTypeLists;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var documentTypeList = JsonConvert.DeserializeObject<List<MlCPScreenData>>(response.Content);

            return documentTypeList;
        }


        public void updateDocumentRelationTemp(MlDocumentRelationTemp mlDocumentRelationTemp)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_UpdationDocumentRelationTemp\",\r\n  \"OperationType\":\"update\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@FaxId\",\"ParamVal\": " + mlDocumentRelationTemp.FaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@TotalNumberOfPages\",\"ParamVal\": " + mlDocumentRelationTemp.TotalNumberOfPages + " },\r\n\t\t\t\t { \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlDocumentRelationTemp.DocumentTypeId + " },\r\n\t\t\t\t { \"ParamName\":\"@UserId\",\"ParamVal\": " + mlDocumentRelationTemp.UserId + " },\r\n\t\t\t\t { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlDocumentRelationTemp.CountForFaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@IsReview\",\"ParamVal\": \"" + mlDocumentRelationTemp.IsReview + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"ssp_UpdationDocumentRelationTemp\",\r\n  \"OperationType\":\"update\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@FaxId\",\"ParamVal\": " + mlDocumentRelationTemp.FaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@TotalNumberOfPages\",\"ParamVal\": " + mlDocumentRelationTemp.TotalNumberOfPages + " },\r\n\t\t\t\t { \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlDocumentRelationTemp.DocumentTypeId + " },\r\n\t\t\t\t { \"ParamName\":\"@UserId\",\"ParamVal\": " + mlDocumentRelationTemp.UserId + " },\r\n\t\t\t\t { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlDocumentRelationTemp.CountForFaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@IsReview\",\"ParamVal\": \"" + mlDocumentRelationTemp.IsReview + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }

        public void usp_insertTotalMismatch_DocumentRelation(MlDocumentRelationTemp mlDocumentRelationTemp)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"OperationName\":\"usp_insertTotalMismatch_DocumentRelation\",\r\n  \"OperationType\":\"update\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@FaxId\",\"ParamVal\": " + mlDocumentRelationTemp.FaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlDocumentRelationTemp.DocumentTypeId + " },\r\n\t\t\t\t { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlDocumentRelationTemp.CountForFaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@IsReview\",\"ParamVal\": \"" + mlDocumentRelationTemp.IsReview + "\" }]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n  \"OperationName\":\"usp_insertTotalMismatch_DocumentRelation\",\r\n  \"OperationType\":\"update\",\r\n  \"ParameterList\": [{ \"ParamName\":\"@FaxId\",\"ParamVal\": " + mlDocumentRelationTemp.FaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@DocumentTypeId\",\"ParamVal\": " + mlDocumentRelationTemp.DocumentTypeId + " },\r\n\t\t\t\t { \"ParamName\":\"@CountForFaxId\",\"ParamVal\": " + mlDocumentRelationTemp.CountForFaxId + " },\r\n\t\t\t\t { \"ParamName\":\"@IsReview\",\"ParamVal\": \"" + mlDocumentRelationTemp.IsReview + "\" }]\r\n}", ParameterType.RequestBody);
                IRestResponse responses = client.Execute(requests);
            }

        }
        #endregion
    }
}
