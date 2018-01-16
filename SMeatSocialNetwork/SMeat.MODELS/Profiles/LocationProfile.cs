using AutoMapper;
using SMeat.MODELS.BindingModels;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.Profiles {
    public class LocationProfile : Profile {
        public LocationProfile () {
            CreateMap<Location, LocationDTO>().MaxDepth(1);
            CreateMap<LocationCreateBindingModel, Location>().MaxDepth(1);
        }
    }
}