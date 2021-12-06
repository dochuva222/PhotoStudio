using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioMob.Services
{
    public static class APIManager
    {
        private static readonly string site = "http://nurfixgun-001-site1.ftempurl.com/api/";
        private static readonly HttpClient httpClient = new HttpClient();

        public async static Task<T> GetData<T>(string controller)
        {
            var response = await httpClient.GetAsync(site + controller);
            var data = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return data;
        }

        public async static Task<HttpResponseMessage> PostData<T>(string controller, T data)
        {
            var jsonString = JsonConvert.SerializeObject(data);
            var response = await httpClient.PostAsync(site + controller, new StringContent(jsonString, Encoding.UTF8, "application/json"));
            return response;
        }

        public async static Task<HttpResponseMessage> PutData<T>(string controller, T data)
        {
            var jsonString = JsonConvert.SerializeObject(data);
            var response = await httpClient.PutAsync(site + controller, new StringContent(jsonString, Encoding.UTF8, "application/json"));
            return response;
        }

        public async static Task<HttpResponseMessage> DeleteData(string controller)
        {
            var response = await httpClient.DeleteAsync(site + controller);
            return response;
        }
    }
}
