using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace UoW_MultipleDBContext.Web.Core
{
    public interface IApiHelper
    {
        Task ApiLogin(string userName, string password);
        Task RefreshToken(string refreshToken = "");
        void SetToken(Token token);
        Task<HttpResponseMessage> GetDataFromAPi(string url, IEnumerable<KeyValuePair<string, string>> data = null);
        Task<HttpResponseMessage> PostDataToAPi(string url, IEnumerable<KeyValuePair<string, string>> data);
        Task<HttpResponseMessage> PostDataToAPi(string url, string data);
        Task<HttpResponseMessage> DeleteDataFromAPi(string url, int id);
    }

    public class ApiHelper : IApiHelper
    {
        HttpClient _client;
        IApiPath _apiPath;

        public ApiHelper(IApiPath apiPath)
        {
            _apiPath = apiPath;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_apiPath.TokenUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task ApiLogin(string userName, string password)
        {
            var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("clientId", "self")
                });
            var result = await _client.PostAsync(_apiPath.TokenUrl, content);
            string resultContent = await result.Content.ReadAsStringAsync();
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var error = JsonConvert.DeserializeObject<ApiError>(resultContent);
                throw new HttpRequestValidationException(error.error_description);
            }
            var token = JsonConvert.DeserializeObject<Token>(resultContent);
            SetToken(token);
        }

        public async Task RefreshToken(string refreshToken = "")
        {
            var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "refresh_token"),
                        new KeyValuePair<string, string>("refresh_token", !string.IsNullOrEmpty(refreshToken) ? refreshToken :
                            System.Web.HttpContext.Current.Session["RefreshToken"].ToString()),
                        new KeyValuePair<string, string>("clientId", "self")
                    });
            HttpResponseMessage response = await _client.PostAsync(_apiPath.TokenUrl, content);
            string resultContent = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Token>(resultContent);
            SetToken(token);
        }

        /// <summary>
        /// To get data from API
        /// </summary>
        /// <param name="url">Get Data URL</param>
        /// <param name="data">
        /// Param collection e.g. { "paramName": "paramData" }. 
        /// Param data can be string or serialized string. To serialize use JSONConvert.Serialize
        /// </param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetDataFromAPi(string url, IEnumerable<KeyValuePair<string, string>> data = null)
        {
            await RefreshToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
                System.Web.HttpContext.Current.Session["AccessToken"].ToString());
            if (data != null)
            {
                string parameters = "";
                data.ToList().ForEach(x => parameters = parameters + (x.Key + "=" + x.Value + "&"));
                parameters = parameters.Substring(0, parameters.Length - 1);
                url = $"{url}{"?"}{parameters}";
            }
            HttpResponseMessage response = await _client.GetAsync(url);
            return response;
        }

        /// <summary>
        /// To post data to API
        /// </summary>
        /// <param name="url">Post Data URL</param>
        /// <param name="data">
        /// Param collection e.g. { "paramName": "paramData" }
        /// Param data can be string or serialized string. To serialize use JSONConvert.Serialize
        /// </param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostDataToAPi(string url, IEnumerable<KeyValuePair<string, string>> data)
        {
            await RefreshToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
                System.Web.HttpContext.Current.Session["AccessToken"].ToString());
            var content = new FormUrlEncodedContent(data);
            HttpResponseMessage response = await _client.PostAsync(url, content);
            return response;
        }

        /// <summary>
        /// To post data to API
        /// </summary>
        /// <param name="url">Post Data URL</param>
        /// <param name="data">
        /// Param data e.g. { "paramData" }
        /// Param data can be string or serialized string. To serialize use JSONConvert.Serialize
        /// </param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostDataToAPi(string url, string data)
        {
            await RefreshToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
                System.Web.HttpContext.Current.Session["AccessToken"].ToString());
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(url, content);
            return response;
        }

        /// <summary>
        /// To Delete data from API
        /// </summary>
        /// <param name="url">Delete Data URL</param>
        /// <param name="data">
        /// Param collection e.g. { "paramName": "paramData" }. 
        /// Param data can be string or serialized string. To serialize use JSONConvert.Serialize
        /// </param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteDataFromAPi(string url, int id)
        {
            await RefreshToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
                System.Web.HttpContext.Current.Session["AccessToken"].ToString());
            HttpResponseMessage response = await _client.DeleteAsync(url + "/id/" + id);
            return response;
        }

        public void SetToken(Token token)
        {
            System.Web.HttpContext.Current.Session["AccessToken"] = token.access_token;
            System.Web.HttpContext.Current.Session["RefreshToken"] = token.refresh_token;

        }
    }

    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    public class ApiError
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
