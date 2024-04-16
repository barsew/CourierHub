using Azure.Core;
using Courier.React.Models.ExternalAPIModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace Courier.React.ExternalAPIClients
{
    public class ExternalAPIClient
    {
        private static ExternalAPIClient _instance;

        private ExternalAPIClient() 
        {
            _client = new RestClient("https://mini.currier.api.snet.com.pl");
        }

        public static ExternalAPIClient GetInstance()
        {
            if(_instance == null) 
            { 
                _instance = new ExternalAPIClient();
            }
            return _instance;
        }

        private RestClient _client;
        private AccessToken _accessToken;

        private void FetchJWTToken()
        {
            string url = "https://indentitymanager.snet.com.pl";
            string endpoint = "connect/token";
            string clientId = "team3e";
            string clientSecret = "4778FFF6-D59B-461E-BB36-22884AC6035E";
            string grantType = "client_credentials";
            string scope = "";

            var identityClient = new RestClient(url);
            var identityRequest = new RestRequest(endpoint, Method.Post);
            identityRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
            identityRequest.AddParameter("application/x-www-form-urlencoded", 
                $"client_id={clientId}&client_secret={clientSecret}&grant_type={grantType}&Scope={scope}", ParameterType.RequestBody);

            var identityResponse = identityClient.Execute(identityRequest);
            var token = JsonConvert.DeserializeObject<IdentityResponse>(identityResponse.Content);
            _accessToken = token.MakeAccessToken();
        }

        private void CheckAndFetchJWTToken()
        {
            if(_accessToken == null || _accessToken.ExpireDate < DateTime.Now)
                FetchJWTToken();
        }

        async public Task<InquiriesResponse?> MakeInquiriesCallAsync(InquiriesRequest inquiriesRequest)
        {
            CheckAndFetchJWTToken();
            var request = new RestRequest("Inquires", Method.Post);

            request.AddHeader("Authorization", $"{_accessToken.TokenType} {_accessToken.JWT}");
            request.AddJsonBody(inquiriesRequest);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var inquiriesResponse = JsonConvert.DeserializeObject<InquiriesResponse>(response.Content);
            return inquiriesResponse;
            
        }
    }
}
