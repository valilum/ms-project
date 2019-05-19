using Newtonsoft.Json;
using Raspberry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Services
{
    public class RaspberryService : IRaspberryService
    {
        private string _readUrl = "http://192.168.0.102:80/read";
        private string _turnOnUrl = "http://192.168.0.102:80/on";
        private string _cancelUrl = "http://192.168.0.102:80/off";

        public async Task<RealTimeData> GetTemperature()
        {
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;


                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(_readUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var product = await response.Content.ReadAsAsync<RealTimeData>();
                        return product;
                    }
                }
                return null;
            }
        }

        public async Task<HttpResponseMessage> TurnHeatOn(DesiredTemperature desired)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                var jsonObject = JsonConvert.SerializeObject(desired);
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(_turnOnUrl, content);
                    var c2 = await response.Content.ReadAsStringAsync();
                     return response;
                }
            }
        }

        public async Task CancelHeating()
        {
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(_cancelUrl, content);
                }
            }

        }

        
    }
}
