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
   
    public  class BLConsumeAPI_UserControl
    {
        string ContentType = ConfigurationManager.AppSettings["ContentType"].ToString();
        string ClientExecuteQueryUrl = ConfigurationManager.AppSettings["ClientExecuteQueryUrl"].ToString();
        string UserId = ConfigurationManager.AppSettings["UserId"].ToString();
        string Password = ConfigurationManager.AppSettings["Password"].ToString();
        public string email;
        public int emailcount;
        public string authorizationHeader;
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
        //string URL = System.Configuration.ConfigurationManager.AppSettings["RestApiUrl"];
        public string generateToken(string grant_type, string username, string password)
        {

            var client = new RestClient("http://localhost:49427/token");
            var request = new RestRequest(Method.POST);
            //ServicePointManager.Expect100Continue = false;
            //ServicePointManager.MaxServicePointIdleTime = 0;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=" + grant_type + "&username=" + username + "&password=" + password + "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                var clients = new RestClient("http://localhost:49427/token");
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
        //-------------------
        public List<TypeOfEquipmentDetails> GetTypeOfEquipmentEmployerDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_TypeOfEquipmentList\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if(response.StatusCode!=System.Net.HttpStatusCode.OK)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_TypeOfEquipmentList\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<TypeOfEquipmentDetails>>(responses.Content);
                return objResponse;
            }

            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<TypeOfEquipmentDetails>>(response.Content);
            return objResponse1;
        }

        public List<TypeOfEquipmentDetails> GetTypeOfEquipmentEmployerDetails2(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_TypeOfEquipmentList2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_TypeOfEquipmentList2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<TypeOfEquipmentDetails>>(responses.Content);
                return objResponse;
            }

            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<TypeOfEquipmentDetails>>(response.Content);
            return objResponse1;
        }
        public List<TypeOfEquipmentDetails> DeleteTypeOfEquipmentEmployerDetailsid(int TypeOfEquipmentDetailId)
        {
            SessionToken();
            //authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteTypeOfEquipmentDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@TypeOfEquipmentDetailId\",\"ParamVal\":\""+TypeOfEquipmentDetailId+"\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteTypeOfEquipmentDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@TypeOfEquipmentDetailId\",\"ParamVal\":\""+ TypeOfEquipmentDetailId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<TypeOfEquipmentDetails>>(responses.Content);
                return objResponse;
            }

            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<TypeOfEquipmentDetails>>(response.Content);
            return objResponse1;
        }
  //------------------
        public List<TrafficConvictionsDetail> GetTrafficConvictionsDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
           // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_TrafficConvictionsDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+ DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_TrafficConvictionsDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<TrafficConvictionsDetail>>(responses.Content);
                return objResponse;
            }
                //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
                var objResponse1 = JsonConvert.DeserializeObject<List<TrafficConvictionsDetail>>(response.Content);
            return objResponse1;
        }
        public List<TrafficConvictionsDetail> GetTrafficConvictionsDetails2(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_TrafficConvictionsDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_TrafficConvictionsDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<TrafficConvictionsDetail>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<TrafficConvictionsDetail>>(response.Content);
            return objResponse1;
        }
        public List<TrafficConvictionsDetail> DeleteTrafficConvictionsDetailsid(int TrafficConvictionsDetailID)
        {
            SessionToken();
            //authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteTrafficConvictionsDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@TrafficConvictionsDetailID\",\"ParamVal\":\""+ TrafficConvictionsDetailID + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteTrafficConvictionsDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@TrafficConvictionsDetailID\",\"ParamVal\":\""+ TrafficConvictionsDetailID + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<TrafficConvictionsDetail>>(responses.Content);
                return objResponse;
            }

            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<TrafficConvictionsDetail>>(response.Content);
            return objResponse1;
        }
  //------------------
        public List<PreviousEmployeement> GetPreviousEmploymentDetail(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            //authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousEmploymentDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousEmploymentDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<PreviousEmployeement>>(responses.Content);
                return objResponse;

            }
                //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
                var objResponse1 = JsonConvert.DeserializeObject<List<PreviousEmployeement>>(response.Content);
            return objResponse1;
        }

        public List<PreviousEmploymentDetail> GetPreviousEmploymentDetail2(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            //authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousEmploymentDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousEmploymentDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<PreviousEmploymentDetail>>(responses.Content);
                return objResponse;

            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<PreviousEmploymentDetail>>(response.Content);
            return objResponse1;
        }
        public List<PreviousEmploymentDetail> DeletePreviousEmploymentDetailid(int PreviousEmploymentDetailId)
        {
            SessionToken();
            //authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeletePreviousEmploymentDetailid\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmploymentDetailId\",\"ParamVal\":\""+ PreviousEmploymentDetailId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeletePreviousEmploymentDetailid\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmploymentDetailId\",\"ParamVal\":\""+ PreviousEmploymentDetailId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<PreviousEmploymentDetail>>(responses.Content);
                return objResponse;
            }

            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<PreviousEmploymentDetail>>(response.Content);
            return objResponse1;
        }

    //--------------------------
        public List<PreviousResidenceDetail> GetPreviousResidenceDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
           // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousResidenceDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousResidenceDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<PreviousResidenceDetail>>(responses.Content);
                return objResponse;
            }
                //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
                var objResponse1 = JsonConvert.DeserializeObject<List<PreviousResidenceDetail>>(response.Content);
            return objResponse1;
        }

        public List<PreviousResidenceDetail> GetPreviousResidenceDetails2(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousResidenceDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousResidenceDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<PreviousResidenceDetail>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<PreviousResidenceDetail>>(response.Content);
            return objResponse1;
        }
        public List<PreviousResidenceDetail> DeletePreviousResidenceDetailsid(int PreviousResidenceDetailID)
        {
            SessionToken();
            //authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeletePreviousResidenceDetailid\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousResidenceDetailID\",\"ParamVal\":\""+ PreviousResidenceDetailID + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeletePreviousResidenceDetailid\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousResidenceDetailID\",\"ParamVal\":\""+ PreviousResidenceDetailID + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<PreviousResidenceDetail>>(responses.Content);
                return objResponse;
            }

            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<PreviousResidenceDetail>>(response.Content);
            return objResponse1;
        }
   //------------------------

        public List<PreviousEmployer> GetPreviousEmployerDetail(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
           // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousEmployerDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousEmployerDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<PreviousEmployer>>(responses.Content);
                return objResponse;
            }
                //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
                var objResponse1 = JsonConvert.DeserializeObject<List<PreviousEmployer>>(response.Content);
            return objResponse1;
        }

        public List<PreviousEmployer> GetPreviousEmployerDetail2(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousEmployerDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_PreviousEmployerDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<PreviousEmployer>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<PreviousEmployer>>(response.Content);
            return objResponse1;
        }
        public List<PreviousEmployer> DeletePreviousEmployerDetailid(int PreviousResidenceDetailID)
        {
            SessionToken();
            //authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeletePreviousEmployerDetailid\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmployerDetailId\",\"ParamVal\":\""+ PreviousResidenceDetailID + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeletePreviousEmployerDetailid\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmployerDetailId\",\"ParamVal\":\""+ PreviousResidenceDetailID + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<PreviousEmployer>>(responses.Content);
                return objResponse;
            }

            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<PreviousEmployer>>(response.Content);
            return objResponse1;
        }
   //---------------------------
        public List<DriverLicence> GetDriverLicenceList(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
           // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DriverLicenceDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DriverLicenceDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<DriverLicence>>(responses.Content);
                return objResponse;
            }
                //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
                var objResponse1 = JsonConvert.DeserializeObject<List<DriverLicence>>(response.Content);
            return objResponse1;
        }
        public List<DriverLicence> GetDriverLicenceList2(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DriverLicenceDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DriverLicenceDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<DriverLicence>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<DriverLicence>>(response.Content);
            return objResponse1;
        }
        public List<DriverLicence> DeleteDriverLicenceListid(int DriverLicenceId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteDriverLicenceDetailid\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DriverLicenceId\",\"ParamVal\":\""+ DriverLicenceId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteDriverLicenceDetailid\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DriverLicenceId\",\"ParamVal\":\""+ DriverLicenceId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<DriverLicence>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<DriverLicence>>(response.Content);
            return objResponse1;
        }
   //------------------------
        public List<CurrentResidence> GetCurrentResidenceDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CurrentResidenceDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CurrentResidenceDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<CurrentResidence>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<CurrentResidence>>(response.Content);
            return objResponse1;
        }
        public List<CurrentResidence> GetCurrentResidenceDetails2(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CurrentResidenceDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CurrentResidenceDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<CurrentResidence>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<CurrentResidence>>(response.Content);
            return objResponse1;
        }
        public List<CurrentResidence> DeleteCurrentResidenceDetailsid(int CurrentResidenceDetailID)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteCurrentResidenceDetailID\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CurrentResidenceDetailID\",\"ParamVal\":\""+ CurrentResidenceDetailID + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteCurrentResidenceDetailID\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CurrentResidenceDetailID\",\"ParamVal\":\""+ CurrentResidenceDetailID + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<CurrentResidence>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<CurrentResidence>>(response.Content);
            return objResponse1;
        }
   //----------------------
        public List<CurrentEmployer> GetCurrentEmployerDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CurrentEmployerDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CurrentEmployerDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<CurrentEmployer>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<CurrentEmployer>>(response.Content);
            return objResponse1;
        }
        public List<CurrentEmployer> GetCurrentEmployerDetails2(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CurrentEmployerDetailList2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CurrentEmployerDetailList2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<CurrentEmployer>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<CurrentEmployer>>(response.Content);
            return objResponse1;
        }
        public List<CurrentEmployer> DeleteCurrentEmployerDetailsid(int CurrentEmployerDetailID)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteCurrentEmployerDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CurrentEmployerDetailID\",\"ParamVal\":\""+ CurrentEmployerDetailID + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteCurrentEmployerDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@CurrentEmployerDetailID\",\"ParamVal\":\""+ CurrentEmployerDetailID + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<CurrentEmployer>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<CurrentEmployer>>(response.Content);
            return objResponse1;
        }
  //---------------------
        public List<DriverConviction> GetConvictionDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_ConvictionDetails\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_ConvictionDetails\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+TempCPScreenDataID+"\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<DriverConviction>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<DriverConviction>>(response.Content);
            return objResponse1;
        }
        public List<DriverConviction> GetConvictionDetailsNotInUserId(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_ConvictionDetailsNotUserId\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_ConvictionDetailsNotUserId\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + TempCPScreenDataID + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<DriverConviction>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<DriverConviction>>(response.Content);
            return objResponse1;
        }

        public List<DriverConviction> DeleteConvictionDetails(int ConvictionDetailsId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteConvictionDetails\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@ConvictionDetailsId\",\"ParamVal\":\""+ ConvictionDetailsId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteConvictionDetails\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@ConvictionDetailsId\",\"ParamVal\":\""+ ConvictionDetailsId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<DriverConviction>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<DriverConviction>>(response.Content);
            return objResponse1;
        }
  //-----------------
        public List<CEDPreviousEmployer> GetCEDPreviousEmployerDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CEDPreviousEmployerDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CEDPreviousEmployerDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+TempCPScreenDataID+"\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<CEDPreviousEmployer>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<CEDPreviousEmployer>>(response.Content);
            return objResponse1;
        }

        public List<CEDPreviousEmployer> GetCEDPreviousEmployerDetails2(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CEDPreviousEmployerDetailList2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_CEDPreviousEmployerDetailList2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + TempCPScreenDataID + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<CEDPreviousEmployer>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<CEDPreviousEmployer>>(response.Content);
            return objResponse1;
        }
        public List<CEDPreviousEmployer> DeleteCEDPreviousEmployerDetails(int PreviousEmployerDetailId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteCEDPreviousEmployerDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmployerDetailId\",\"ParamVal\":\""+ PreviousEmployerDetailId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteCEDPreviousEmployerDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@PreviousEmployerDetailId\",\"ParamVal\":\""+ PreviousEmployerDetailId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<CEDPreviousEmployer>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<CEDPreviousEmployer>>(response.Content);
            return objResponse1;
        }
  //-------------------
        public List<AccidentRecordDetail> GetAccidentRecordDetail(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_AccidentRecordDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_AccidentRecordDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+TempCPScreenDataID+"\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<AccidentRecordDetail>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<AccidentRecordDetail>>(response.Content);
            return objResponse1;
        }
        public List<AccidentRecordDetail> DeleteAccidentRecordDetail(int AccidentRecordDetailId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteAccidentRecordDetailId\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@AccidentRecordDetailId\",\"ParamVal\":\""+ AccidentRecordDetailId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteAccidentRecordDetailId\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@AccidentRecordDetailId\",\"ParamVal\":\""+ AccidentRecordDetailId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<AccidentRecordDetail>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<AccidentRecordDetail>>(response.Content);
            return objResponse1;
        }
  //---------------------
        public List<DriverLicenseStatusDetail> GetDriverLicenseStatusDetail(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DriverLicenseStatusDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+CountFaxId+"\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DriverLicenseStatusDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\""+DocumentTypeId+"\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\""+UserId+"\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\""+TempCPScreenDataID+"\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\""+TempCPScreenDataID+"\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<DriverLicenseStatusDetail>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<DriverLicenseStatusDetail>>(response.Content);
            return objResponse1;
        }
        public List<DriverLicenseStatusDetail> DeleteDriverLicenseStatusDetail(int DriverLicenseStatusId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
                        var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteDriverLicenseStatusDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DriverLicenseStatusId\",\"ParamVal\":\""+ DriverLicenseStatusId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                 var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteDriverLicenseStatusDetail\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DriverLicenseStatusId\",\"ParamVal\":\""+ DriverLicenseStatusId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<DriverLicenseStatusDetail>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<DriverLicenseStatusDetail>>(response.Content);
            return objResponse1;
        }
        //-----------------------

        public List<ViolationDetail> GetViolationDetails(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetViolationDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetViolationDetail\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + TempCPScreenDataID + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<ViolationDetail>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<ViolationDetail>>(response.Content);
            return objResponse1;
        }

        public List<ViolationDetail> GetViolationDetails2(int UserId, int DocumentTypeId, int TempCPScreenDataID, int CountFaxId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetViolationDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + CountFaxId + "\" }]\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_GetViolationDetail2\",\r\n\t\"OperationType\":\"select\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@DocumentTypeId\",\"ParamVal\":\"" + DocumentTypeId + "\" },\r\n\t\t{ \"ParamName\":\"@UserId\",\"ParamVal\":\"" + UserId + "\" },\r\n\t\t{ \"ParamName\":\"@TempCPScreenDataID\",\"ParamVal\":\"" + TempCPScreenDataID + "\" },\r\n\t\t{ \"ParamName\":\"@CountForFaxId\",\"ParamVal\":\"" + TempCPScreenDataID + "\" }]\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<ViolationDetail>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<ViolationDetail>>(response.Content);
            return objResponse1;
        }

        public List<DriverConviction> DeleteViolationDetails(int voilationDetailId)
        {
            SessionToken();
            // authorizationHeader = "bearer " + generateToken("password", "neeraj%40fadv.com", "India%40123");
            var client = new RestClient(ClientExecuteQueryUrl);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", authorizationHeader);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteViolationDetails\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@ViolationDetailID\",\"ParamVal\":\"" + voilationDetailId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                getToken();
                var clients = new RestClient(ClientExecuteQueryUrl);
                var requests = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "73e00915-1546-b6c2-ad7a-227f0f75337e");
                requests.AddHeader("cache-control", "no-cache");
                requests.AddHeader("authorization", authorizationHeader);
                requests.AddHeader("content-type", "application/json");
                requests.AddParameter("application/json", "{\r\n\"OperationName\":\"SP_DeleteViolationDetails\",\r\n\t\"OperationType\":\"delete\",\r\n\t\"ParameterList\": [{ \"ParamName\":\"@ViolationDetailID\",\"ParamVal\":\"" + voilationDetailId + "\" }]\r\n\t\r\n }", ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                var objResponse = JsonConvert.DeserializeObject<List<DriverConviction>>(responses.Content);
                return objResponse;
            }
            //List<MlCustomerDetails> lstCustomerDetail = new List<MlCustomerDetails>();
            var objResponse1 = JsonConvert.DeserializeObject<List<DriverConviction>>(response.Content);
            return objResponse1;
        }
    }
}
