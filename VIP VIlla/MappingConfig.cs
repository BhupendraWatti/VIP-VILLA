using AutoMapper;
using Villa_Services.Models;
using VIP_Villa.Models.Dto;
using VIP_Villa.Services;


namespace VIP_Villa
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDto>().ReverseMap();

            CreateMap<VillaNumberDto, VillaNumberUpdate>().ReverseMap();

            CreateMap<VIP_Villa.Models.Dto.VillaNumberCreate, Villa_Services.Models.Dto.VillaNumberCreate>().ReverseMap();
            CreateMap<VIP_Villa.Models.Dto.VillaNumberUpdate, Villa_Services.Models.Dto.VillaNumberUpdate>().ReverseMap();

            //CreateMap<VillaNumber, VillaNumberCreate>().ReverseMap(); // already there

        }
    }

}
