using AutoMapper;
using Villa_Services.Models;
using VIP_Villa.Models.Dto;


namespace VIP_Villa
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDto>().ReverseMap();
            CreateMap<VillaNumberDto, VillaNumberCreate>().ReverseMap();
            CreateMap<VillaNumberDto, VillaNumberUpdate>().ReverseMap();
            // This goes inside your MappingConfig constructor:
            //CreateMap<Villa_Services.Models.Dto.VillaNumberCreate, VIP_Villa.Models.Dto.VillaNumberCreate>().ReverseMap();


        }
    }
}
