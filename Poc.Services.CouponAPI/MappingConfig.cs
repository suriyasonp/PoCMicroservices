using AutoMapper;
using Poc.Services.CouponAPI.Models;

namespace Poc.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponModel, CouponGetDto>().ReverseMap();
                config.CreateMap<CouponModel, CouponPostDto>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
