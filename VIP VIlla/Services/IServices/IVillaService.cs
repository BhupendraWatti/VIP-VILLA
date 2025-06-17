using VIP_Villa.Models.Dto;

namespace VIP_Villa.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);

        Task<T> CreateAsync<T>(VillaCreateDto CreateDto);
        Task<T> UpdateAsync<T>(VillaUpdateDto UpdateDto);
        Task<T> DeleteAsync<T>(int id);

    }
}
