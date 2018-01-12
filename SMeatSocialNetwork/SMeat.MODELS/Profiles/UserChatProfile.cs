using AutoMapper;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.Profiles {
    public class UserChatProfile : Profile {
        public UserChatProfile () {
            CreateMap<UserChat, UserChatDTO>().MaxDepth(1);
        }
    }
}