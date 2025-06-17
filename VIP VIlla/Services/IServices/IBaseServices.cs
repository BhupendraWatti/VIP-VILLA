using VIP_Villa.Models;

namespace VIP_Villa.Services.IServices
{
    public interface IBaseServices
    {
        APIResponse  responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
