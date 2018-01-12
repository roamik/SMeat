using AutoMapper;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.Profiles {
    public class UserProfile : Profile {
        public UserProfile () {
            CreateMap<User, UserDTO>().MaxDepth(1);
        }
    }
}