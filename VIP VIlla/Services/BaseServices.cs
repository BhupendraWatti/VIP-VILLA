using Newtonsoft.Json;
using System.Text;
//using Villa_Services.Models;
using VIP_Villa.Models;
using VIP_Villa.Services.IServices;
using VIPVIlla_Utility;

namespace VIP_Villa.Services
{
    public class BaseServices : IBaseServices
    {
        public APIResponse responseModel { get; set; }

        public IHttpClientFactory httpClient { get; set; }
        public BaseServices(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var Client = httpClient.CreateClient("VillaServices");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;

                apiResponse = await Client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    APIResponse _apirespon = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (_apirespon.StatusCode ==System.Net.HttpStatusCode.BadRequest|| _apirespon.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        _apirespon.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        _apirespon.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(_apirespon);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }
                }
                catch(Exception ex)
                {
                    var expectionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return expectionResponse;
                }
                var  _apiRespon = JsonConvert.DeserializeObject<T>(apiContent);
                return _apiRespon;
            }
            catch (Exception ex)
            {
                var dto = new Models.APIResponse
                {
                    ErrorMessage = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }

        }
    }

}