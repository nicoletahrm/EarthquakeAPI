using AutoMapper;
using Earthquake.API.Models;

namespace Earthquake.API.Profiles
{
    public class EarthquakeProfile : Profile
    {
        public EarthquakeProfile()
        {
            CreateMap<Earthquake, EarthquakeResponse>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Features.First().Id))
                .ForMember(dest => dest.Magnitude, opt=> opt.MapFrom(src => src.Features.First().Properties.Magnitude))
                .ForMember(dest => dest.Place, opt=> opt.MapFrom(src => src.Features.First().Properties.Place))
                .ForMember(dest => dest.Type, opt=> opt.MapFrom(src => src.Features.First().Properties.Type))
                .ForMember(dest => dest.Coordinates, opt=> opt.MapFrom(src => src.Features.First().Geometry.Coordinates));

            CreateMap<Feature, EarthquakeEntity>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties))
                .ForMember(dest => dest.Geometry, opt => opt.MapFrom(src => src.Geometry));

            CreateMap<EarthquakeEntity, EarthquakeResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Magnitude, opt => opt.MapFrom(src => src.Properties.Magnitude))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Properties.Place))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Properties.Type))
                .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(src => src.Geometry.Coordinates));
        }
    }
}
