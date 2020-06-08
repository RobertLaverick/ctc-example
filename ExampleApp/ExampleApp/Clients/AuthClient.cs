using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExampleApp.Models;
using Newtonsoft.Json;

namespace ExampleApp.Clients
{
    public class AuthClient : HttpClient
    {
        private const string clientId = "y24eoPeZbqPLlbNH8LenfIk1a61k";
        private const string clientSecret = "1f0ebb97-04cd-435f-8170-caca2538d747";

        public AuthClient() { }

        public async Task<TokenDetails> GetAuthToken(string code)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("client_secret", clientSecret);
            parameters.Add("client_id", clientId);
            parameters.Add("grant_type", "authorization_code");
            parameters.Add("redirect_uri", "https://localhost:5001/Home/CodeHook");
            parameters.Add("code", code);


            var result = await PostAsync("https://api.development.tax.service.gov.uk/oauth/token", new FormUrlEncodedContent(parameters));
            var json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenDetails>(json);
        }
    }
}
