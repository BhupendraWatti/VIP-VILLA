using VIP_Villa.Models.Dto;
using VIP_Villa.Models.VM;

namespace VIP_Villa.Services.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);

        Task<T> CreateAsync<T>(VillaNumberCreate CreateDto);
        Task<T> UpdateAsync<T>(VillaNumberUpdate UpdateDto);
        Task<T> DeleteAsync<T>(int id);
       
    }
}
