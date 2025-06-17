using Microsoft.AspNetCore.Http.HttpResults;
using VIP_Villa.Models;
using VIP_Villa.Models.Dto;
using VIP_Villa.Models.VM;
using VIP_Villa.Services.IServices;
using VIPVIlla_Utility;

namespace VIP_Villa.Services
{
    public class VillaNumberService : BaseServices, IVillaNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string _VillaUrl;
        public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration):base(clientFactory) 
        {
            _clientFactory = clientFactory;
            _VillaUrl = configuration.GetValue<string>("ServiceUrl:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaNumberCreate CreateDto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = CreateDto,
                Url = _VillaUrl + "/api/VillaNumber"
            });           
        }

       
        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = _VillaUrl + "/api/VillaNumber/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _VillaUrl + "/api/VillaNumber"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _VillaUrl + "/api/VillaNumber/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdate UpdateDto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = UpdateDto,
                Url = _VillaUrl + "/api/VillaNumber"
            });
        }
    }
}
