using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExampleApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ExampleApp
{
    public class HmrcCtcClient : HttpClient
    {
        private string _token;

        public HmrcCtcClient(string root, string token)
        {
            BaseAddress = new Uri(root);
            _token = token;
        }

        #region Util

        private AuthenticationHeaderValue AddToken() => new AuthenticationHeaderValue("Bearer", _token);

        private async Task<T> GetElement<T>(string uri)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Authorization = AddToken();
                request.Headers.Add("Accept", "application/vnd.hmrc.1.0+json");
                var response = await SendAsync(request);
                var contentString = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<T>(contentString);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region GetArrivals
        public async Task<IList<Arrival>> GetUserArrivals()
        {
            return await GetElement<IList<Arrival>>("/customs/transits/movements/arrivals");
        }

        public async Task<Arrival> GetArrivalById(string arrivalId)
        {
            return await GetElement<Arrival>($"/customs/transits/movements/arrivals/{arrivalId}");
        }

        public async Task<ArrivalWithMessages> GetArrivalMessages(string arrivalId)
        {
            return await GetElement<ArrivalWithMessages>($"/customs/transits/movements/arrivals/{arrivalId}/messages");
        }

        public async Task<Message> GetArrivalMessage(string arrivalId, string messageId)
        {
            return await GetElement<Message>($"/customs/transits/movements/arrivals/{arrivalId}/messages/{messageId}");
        }

        #endregion

        #region POST/PUTArrivals
        public async Task<string> SendArrivalNotification()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "/customs/transits/movements/arrivals");
                request.Headers.Authorization = AddToken();

                request.Content = new StringContent(DefaultXml.ArrivalNotification);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

                var response = await SendAsync(request);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location.ToString();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<string> ResubmitArrivalNotification(string arrivalId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"/customs/transits/movements/arrivals/{arrivalId}");
                request.Headers.Authorization = AddToken();
                request.Content = new StringContent(DefaultXml.ArrivalNotification);

                var response = await SendAsync(request);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region GetDepartures
        public async Task<IList<Departure>> GetUserDepartures()
        {
            return await GetElement<IList<Departure>>($"/customs/transits/movements/departures/");
        }

        public async Task<Departure> GetDepartureById(string departureId)
        {
            return await GetElement<Departure>($"/customs/transits/movements/departures/{departureId}");
        }

        public async Task<DepartureWithMessages> GetDepartureMessages(string departureId)
        {
            return await GetElement<DepartureWithMessages>($"/customs/transits/movements/departures/{departureId}/messages");
        }

        public async Task<Message> GetDepartureMessage(string departureId, string messageId)
        {
            return await GetElement<Message>($"/customs/transits/movements/departures/{departureId}/messages/{messageId}");
        }
        #endregion
    }
}
